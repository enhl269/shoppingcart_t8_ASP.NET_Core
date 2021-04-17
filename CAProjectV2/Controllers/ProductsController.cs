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
using Microsoft.AspNetCore.Http;

namespace CAProjectV2.Controllers
{
    public class ProductsController : Controller
    {


        private readonly WebsiteContext _context;

        public ProductsController(WebsiteContext context)
        {
            _context = context;
        }

        //Embedding session into user 
        public async Task<IActionResult> FirstPage()
        {
            string guest = IdGenerator.ID();
            Response.Cookies.Append("GuestLogin", guest);
            User visitor = new User
            {
                Id = guest,
                sessionId = guest.Clone().ToString() 
            };

            await _context.User.AddAsync(visitor);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Products");

        }
        
        // GET: Products

        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("isLogin")))
            {    //checking session is null or not which means checking user log in or out

                var id = HttpContext.Session.GetString("Userid");
                User user = _context.User.AsNoTracking()
                                        .Where(x => x.Id == id).FirstOrDefault();
                ProfileViewModel profile = new ProfileViewModel(user.UserImageUrl, user.FirstName, user.LastName, user.UserName, user.Email, user.PhoneNumber, "", "");
                ViewData["profile"] = profile;

            }

            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["CurrentFilter"] = searchString;
            ViewData["ProductID"] = "ProductName";

            var product = from p in _context.Product
                          select p;

            if (!String.IsNullOrEmpty(Convert.ToString(searchString)))
            {
                product = product.Where(p => Convert.ToString(p.ProductName).Contains(Convert.ToString(searchString))
                                       || Convert.ToString(p.Description).Contains(Convert.ToString(searchString)));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    product = product.OrderByDescending(p => p.ProductName);
                    break;
                default:
                    product = product.OrderBy(p => p.ProductName);
                    break;
            }
            string guestLogin = HttpContext.Session.GetString("isLogin");
            ViewData["isLogin"] = guestLogin;

            return View(await product.AsNoTracking().ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
                return NotFound();

            var product = await _context.Product
                                        .FirstOrDefaultAsync(m => m.Id == id);
            
            if (product == null)
                return NotFound();

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductName,Description,Price,ImageUrl,tag")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
                return NotFound();

            var product = await _context.Product.FindAsync(id);
            if (product == null)
                return NotFound();

            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,ProductName,Description,Price,ImageUrl,tag")] Product product)
        {
            if (id != product.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
                return NotFound();


            var product = await _context.Product
                                        .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
                return NotFound();

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(string id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
