using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CAProjectV2.Data;
using CAProjectV2.Models;
using CAProjectV2.Logic;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace CAProjectV2.Controllers
{
    public class OrderDetailsController : Controller
    {
        private readonly WebsiteContext _context;

        public OrderDetailsController(WebsiteContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Checkout(decimal id)
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("isLogin")))
            {

                string userId = HttpContext.Session.GetString("Userid");
                var products = _context.ShoppingCartItem
                    .Where(x => x.UserId == userId);

                List<OrderDetails> orderDetails = new List<OrderDetails>();
                Order order = new Order();
                DateTime localDate = DateTime.Now;

                order.Id = IdGenerator.ID();
                order.Date = localDate.Date;
                order.UserId = userId;
                order.TotalAmount = id;
                order.OrderDetailsId = IdGenerator.ID();

                foreach (var product in products)
                {
                    orderDetails.Add(new OrderDetails
                    {
                        ActivationCode = IdGenerator.ID(),
                        ProductId = product.ProductId,
                        Quantity = 1,
                        IndivProductPrice = product.Product.Price,
                        Order = order
                    });

                }
                await _context.OrderDetails.AddRangeAsync(orderDetails);
                await _context.SaveChangesAsync();

                foreach (var product in products)
                {
                    _context.Remove(product);
                }
                _context.SaveChanges();


                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction("Index", "LogIn");
            }
        }


        // GET: OrderDetails
        public async Task<IActionResult> Index()
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("isLogin")))
            {
                string userId = HttpContext.Session.GetString("Userid");


                var websiteContext = _context.OrderDetails.Include(o => o.Order).Include(o => o.Product).Where(x => x.Order.UserId == userId).
                    OrderBy(x => x.Product.ProductName).ThenBy(x => x.Order.Date);

                websiteContext = websiteContext.OrderByDescending(x => x.Order.Date);

                IQueryable<DataView> lookup = websiteContext.Select(x => new DataView { Date = x.Order.Date, ProductId = x.ProductId }).OrderBy(x => x.Date).Distinct();
                lookup = lookup.OrderByDescending(x => x.Date);

                List<DataView> UniqueList = new List<DataView>();
                foreach (var item in lookup)
                {
                    DataView dataViews = new DataView { Date = item.Date, ProductId = item.ProductId };
                    UniqueList.Add(dataViews);

                }
                ViewData["UniqueList"] = UniqueList;

                return View(await websiteContext.ToListAsync());
            }
            else
            {
                return RedirectToAction("Index", "LogIn");
            }

        }

        // GET: OrderDetails/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetails = await _context.OrderDetails
                .Include(o => o.Order)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderDetails == null)
            {
                return NotFound();
            }

            return View(orderDetails);

        }

        // GET: OrderDetails/Create
        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id");
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Id");
            return View();
        }

        // POST: OrderDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ActivationCode,ProductId,Quantity,IndivProductPrice,OrderId")] OrderDetails orderDetails)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", orderDetails.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Id", orderDetails.ProductId);
            return View(orderDetails);
        }

        // GET: OrderDetails/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetails = await _context.OrderDetails.FindAsync(id);
            if (orderDetails == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", orderDetails.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Id", orderDetails.ProductId);
            return View(orderDetails);
        }

        // POST: OrderDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,ActivationCode,ProductId,Quantity,IndivProductPrice,OrderId")] OrderDetails orderDetails)
        {
            if (id != orderDetails.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderDetailsExists(orderDetails.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", orderDetails.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Id", orderDetails.ProductId);
            return View(orderDetails);
        }

        // GET: OrderDetails/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetails = await _context.OrderDetails
                .Include(o => o.Order)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderDetails == null)
            {
                return NotFound();
            }

            return View(orderDetails);
        }

        // POST: OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var orderDetails = await _context.OrderDetails.FindAsync(id);
            _context.OrderDetails.Remove(orderDetails);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderDetailsExists(string id)
        {
            return _context.OrderDetails.Any(e => e.Id == id);
        }
    }
}
