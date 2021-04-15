using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using testingproject3.Data;
using testingproject3.Models;

namespace testingproject3.Controllers
{
    public class PurchaseHistoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PurchaseHistoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PurchaseHistories
        public async Task<IActionResult> Index()
        {
            string user = User.FindFirstValue(ClaimTypes.NameIdentifier);//Current User Id
            return View(await _context.PurchaseHistories.Where(i=>i.UserId==user).ToListAsync());
        }

        private bool PurchaseHistoriesExists(int id)
        {
            return _context.PurchaseHistories.Any(e => e.Id == id);
        }
    }
}
