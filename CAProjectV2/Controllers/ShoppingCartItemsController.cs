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
using System.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace CAProjectV2.Controllers
{//testing 
    public class ShoppingCartItemsController : Controller
    {
        private readonly WebsiteContext _context;

        public ShoppingCartItemsController(WebsiteContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Adding(ShoppingCartItem items)
        {
            items.Id = IdGenerator.ID();
            items.ShoppingCartId = IdGenerator.ID();
            items.ShoppingCartItemEachProductId = items.Id.Clone().ToString() + items.ShoppingCartId.Clone().ToString();
            items.Quantity = 1;
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("isLogin")))
            {
                items.UserId = HttpContext.Session.GetString("Userid"); 
            }
            else
            {
                items.UserId = Request.Cookies["GuestLogin"];
            }

            await _context.AddAsync(items);
            await _context.SaveChangesAsync();

            var _exists = _context.WishList
                .Where(x => x.UserId == items.UserId
                && x.ProductId == items.ProductId)
                .FirstOrDefault();

            if (_exists != null)
                _context.WishList.Remove(_exists);

            await _context.SaveChangesAsync();

            return LocalRedirect("/Products/Index");
        }

        public async Task<IActionResult> Addinginwishlist(ShoppingCartItem items)
        {
            items.Id = IdGenerator.ID();
            items.ShoppingCartId = IdGenerator.ID();
            items.ShoppingCartItemEachProductId = items.Id.Clone().ToString() + items.ShoppingCartId.Clone().ToString();
            items.Quantity = 1;
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("isLogin")))
            {
                items.UserId = HttpContext.Session.GetString("Userid");
            }
            else
            {
                items.UserId = Request.Cookies["GuestLogin"];
            }

            await _context.AddAsync(items);
            await _context.SaveChangesAsync();

            var _exists = _context.WishList
                .Where(x => x.UserId == items.UserId
                && x.ProductId == items.ProductId)
                .FirstOrDefault();

            if (_exists != null)
                _context.WishList.Remove(_exists);

            await _context.SaveChangesAsync();

            return LocalRedirect("/WishItem/WishList");
        }

        [HttpPost]
        public async Task<IActionResult> Updating(ShoppingCartItem updatedQty)
        {
            //if the qty is lesser than the update quantity, delete until the qty is equal to the updatedQty

            //list of items in the shopping cart database
            var products = _context.ShoppingCartItem
                .Where(x => x.UserId == updatedQty.UserId 
                    && x.ProductId == updatedQty.ProductId)
                .ToList();

            //find original qty
            var websiteContext = _context.ShoppingCartItem.Where(x => x.UserId == updatedQty.UserId).Count(x =>
            Convert.ToString(x.ProductId) == Convert.ToString(updatedQty.ProductId));

            if (updatedQty.Quantity > 0)
            {
                //generating new productid details for this updatedQty
                updatedQty.Id = IdGenerator.ID();
                updatedQty.Quantity = 1;
                await _context.ShoppingCartItem.AddAsync(updatedQty);
                await _context.SaveChangesAsync();
                
            }
            else
            {
                _context.ShoppingCartItem.Remove(products.FirstOrDefault());
                await _context.SaveChangesAsync();

            }

            return RedirectToAction(nameof(Index));
        }

        //GET: ShoppingCartItems
        //public async Task<IActionResult> Index()
        //{
        //    var websiteContext =  _context.ShoppingCartItem.Include(s => s.Product);

        //    return View(await websiteContext.ToListAsync());
        //}

        public IActionResult Index()
        {

            string currentLogin;
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("isLogin")))
            {
                currentLogin = HttpContext.Session.GetString("Userid");
            }
            else
            {
                currentLogin = Request.Cookies["GuestLogin"];
            }

            //var websiteContext = _context.ShoppingCartItem.Include(s => s.Product).ToList();
            var websiteContext = _context.ShoppingCartItem.Include(s => s.Product)
                .Where(x => x.UserId == currentLogin)
                .OrderBy(x => x.Product.ProductName)
                .ToList();

            ViewData["Data"] = websiteContext;
            string guestLogin = HttpContext.Session.GetString("isLogin");
            ViewData["isLogin"] = guestLogin;
            //var product = from p in _context.ShoppingCartItem
            //              select  p.ProductId.Distinct();
            //ViewData["productcount"] = product;

            return View();
        }

        // GET: ShoppingCartItems/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingCartItem = await _context.ShoppingCartItem
                .Include(s => s.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shoppingCartItem == null)
            {
                return NotFound();
            }

            return View(shoppingCartItem);
        }

        // GET: ShoppingCartItems/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Id");
            return View();
        }

        // POST: ShoppingCartItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ShoppingCartId,ShoppingCartItemEachProductId,UserId,ProductId,Quantity")] ShoppingCartItem shoppingCartItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shoppingCartItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Id", shoppingCartItem.ProductId);
            return View(shoppingCartItem);
        }

        // GET: ShoppingCartItems/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingCartItem = await _context.ShoppingCartItem.FindAsync(id);
            if (shoppingCartItem == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Id", shoppingCartItem.ProductId);
            return View(shoppingCartItem);
        }

        // POST: ShoppingCartItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,ShoppingCartId,ShoppingCartItemEachProductId,UserId,ProductId,Quantity")] ShoppingCartItem shoppingCartItem)
        {
            if (id != shoppingCartItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shoppingCartItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShoppingCartItemExists(shoppingCartItem.Id))
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
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Id", shoppingCartItem.ProductId);
            return View(shoppingCartItem);
        }

        // POST: ShoppingCartItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string userId, string productId)
        {
            var products = _context.ShoppingCartItem
                    .Where(x => x.UserId == userId
                            && x.ProductId == productId);

            foreach (var product in products)
            {
                _context.ShoppingCartItem.Remove(product);
            }
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool ShoppingCartItemExists(string id)
        {
            return _context.ShoppingCartItem.Any(e => e.Id == id);
        }

        //adding action to count the item in shopping cart
        [HttpPost]
        public IActionResult shoppingcartcount()
        {

            string currentLogin;
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("isLogin")))
            {
                currentLogin = HttpContext.Session.GetString("Userid");
            }
            else
            {
                currentLogin = Request.Cookies["GuestLogin"];
            }

            //var websiteContext = _context.ShoppingCartItem.Include(s => s.Product).ToList();
            var websiteContext = _context.ShoppingCartItem.Include(s => s.Product)
                .Where(x => x.UserId == currentLogin)
                .OrderBy(x => x.Product.ProductName)
                .ToList();
            var count = websiteContext.Count();

            return Json(new { success = count });
        }
    }
}
