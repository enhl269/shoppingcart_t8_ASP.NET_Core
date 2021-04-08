using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CAProject1.Data;
using CAProject1.Models;

namespace CAProject1.Controllers
{
    public class ShoppingCartItemsController : Controller
    {
        private readonly WebsiteContext _context;

        public ShoppingCartItemsController(WebsiteContext context)
        {
            _context = context;
        }

        // GET: ShoppingCartItems
        public async Task<IActionResult> Index()
        {
            var websiteContext = _context.ShoppingCartItem.Include(s => s.ShoppingCart);
            return View(await websiteContext.ToListAsync());
        }

        public IActionResult Adding(ShoppingCartItem shoppingCartItems)
        {
            _context.Add(shoppingCartItems);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: ShoppingCartItems/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingCartItem = await _context.ShoppingCartItem
                .Include(s => s.ShoppingCart)
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
            ViewData["ShoppingCartId"] = new SelectList(_context.ShoppingCart, "Id", "Id");
            return View();
        }

        // POST: ShoppingCartItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ShoppingCartId,ShoppingCartItemEachProductId,ProductId,Quantity")] ShoppingCartItem shoppingCartItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shoppingCartItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ShoppingCartId"] = new SelectList(_context.ShoppingCart, "Id", "Id", shoppingCartItem.ShoppingCartId);
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
            ViewData["ShoppingCartId"] = new SelectList(_context.ShoppingCart, "Id", "Id", shoppingCartItem.ShoppingCartId);
            return View(shoppingCartItem);
        }

        // POST: ShoppingCartItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,ShoppingCartId,ShoppingCartItemEachProductId,ProductId,Quantity")] ShoppingCartItem shoppingCartItem)
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
            ViewData["ShoppingCartId"] = new SelectList(_context.ShoppingCart, "Id", "Id", shoppingCartItem.ShoppingCartId);
            return View(shoppingCartItem);
        }

        // GET: ShoppingCartItems/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingCartItem = await _context.ShoppingCartItem
                .Include(s => s.ShoppingCart)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shoppingCartItem == null)
            {
                return NotFound();
            }

            return View(shoppingCartItem);
        }

        // POST: ShoppingCartItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var shoppingCartItem = await _context.ShoppingCartItem.FindAsync(id);
            _context.ShoppingCartItem.Remove(shoppingCartItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShoppingCartItemExists(string id)
        {
            return _context.ShoppingCartItem.Any(e => e.Id == id);
        }
    }
}
