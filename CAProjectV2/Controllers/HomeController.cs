using CAProjectV2.Data;
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
    public class HomeController : Controller
    {
        private readonly WebsiteContext _context;
        public HomeController(WebsiteContext context)
        {
            _context = context;
        }
        public IActionResult Index()

        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("isLogin")))
            {    //checking session is null or not which means checking user log in or out

                var id = HttpContext.Session.GetString("Userid");
                User user = _context.User.AsNoTracking().Where(x => x.Id == id).FirstOrDefault();
                ProfileViewModel profile = new ProfileViewModel(user.UserImageUrl, user.FirstName, user.LastName, user.UserName, user.Email, user.PhoneNumber, "", "");
                ViewData["profile"] = profile;
             
            }
            return View();
        }
    }
}