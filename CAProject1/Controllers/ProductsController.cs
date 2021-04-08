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
    public class ProductsController : Controller
    {
        private readonly WebsiteContext _context;

        public ProductsController(WebsiteContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["CurrentFilter"] = searchString;
            ViewData["ProductID"] = "ProductName";

            var product = from p in _context.Product
                          select p;

            if (!String.IsNullOrEmpty(Convert.ToString(searchString)))
            {
                product = product.Where(p => Convert.ToString(p.ProductName).Contains(Convert.ToString(searchString))
                                       || Convert.ToString(p.ProductName).Contains(Convert.ToString(searchString)));
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
            return View(await product.AsNoTracking().ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }


        private bool ProductExists(string id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
