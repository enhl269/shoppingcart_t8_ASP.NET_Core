using CAProjectV2.Data;
using CAProjectV2.Logic;
using CAProjectV2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAProjectV2.Controllers
{
    public class LogInController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private readonly WebsiteContext _context;

        public LogInController(WebsiteContext context)
        {
            _context = context;
        }

        public  IActionResult Login(LogInViewModel logInUser)
        {
            
            var password = Encrption.Encrypt(logInUser.Password);
            User result =  _context.User.Where(x => x.UserName == logInUser.UserName && x.Password == password).FirstOrDefault();
            if (result != null)
            {
                HttpContext.Session.SetString("Userid", result.Id);
                HttpContext.Session.SetString("isLogin", result.UserName);
                           
                string guestLogin = Request.Cookies["GuestLogin"];
                var items = _context.ShoppingCartItem.Where(x => x.UserId == guestLogin);
                if (items != null)
                {
                    string userLogin = HttpContext.Session.GetString("Userid");
                    foreach (var item in items)
                    {
                        item.UserId = userLogin;
                    }

                    _context.SaveChanges();
                }
                
                return RedirectToAction("Index", "Products");   // we don't have home page yet
            }

                return Redirect("Index");  // UI : need to create error message/page that user name or pwd is incorrect
            
            //nothing
            

        }

        
    }
}
