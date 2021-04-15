using CAProjectV2.Data;
using CAProjectV2.Logic;
using CAProjectV2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAProjectV2.Controllers
{
    //test update and merge
    public class WishItemController : Controller
    {
        private readonly WebsiteContext _context;
        public WishItemController(WebsiteContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> WishIt(WishList item, string details)
        {
            item.Id = IdGenerator.ID();

            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("isLogin")))
            {
                item.UserId = HttpContext.Session.GetString("Userid");
            }
            else
            {
                item.UserId = Request.Cookies["GuestLogin"];
            }

            var _exists = _context.WishList
                .Where(x => x.UserId == item.UserId
                && x.ProductId == item.ProductId)
                .FirstOrDefault();


            if (_exists == null)
            {
                await _context.AddAsync(item);
            }
            else
            {
                _context.WishList.Remove(_exists);
            }
            await _context.SaveChangesAsync();
            string redirectionroute = "/Products/Index";
            if (details == "details")
            {
                redirectionroute = "/Products/details/" + details;
            }
            else if (details == "WishList")
            {
                redirectionroute = "/WishItem/WishList/";
            }

            return LocalRedirect(redirectionroute);

        }


        [HttpPost]
        public IActionResult UserWishList()
        {
            string CurrentUserId;
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("isLogin")))
            {
                CurrentUserId = HttpContext.Session.GetString("Userid");
            }
            else
            {
                CurrentUserId = Request.Cookies["GuestLogin"];
            }

            var wishitemperuser = _context.WishList
                                  .Where(x => x.UserId == CurrentUserId)
                                  .OrderBy(x => x.ProductId);

            string returnstring = "";

            foreach (WishList itema in wishitemperuser)
            {
                returnstring = returnstring + itema.ProductId.ToString() + " ";
            }
            string rs = returnstring.Trim(' ');
            return Json(new { success = rs });
        }



        public IActionResult WishList()
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

            var websiteContext = _context.WishList.Include(w => w.Product)
                .Where(x => x.UserId == currentLogin)
                .OrderBy(x => x.Product.ProductName)
                .ToList();

            ViewData["Data"] = websiteContext;
            return View();
        }

    }
}

