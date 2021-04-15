using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using testingproject3.Data;
using testingproject3.Models;

namespace testingproject3.Controllers
{
    public class PurchaseHistoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PurchaseHistoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PurchaseHistory
        public async Task<IActionResult> Index(int id)
        {
            return View(await _context.PurchaseHistory.Where(i=>i.GroupId==id).ToListAsync());
        }

        private bool PurchaseHistoryExists(int id)
        {
            return _context.PurchaseHistory.Any(e => e.Id == id);
        }
    }
}
