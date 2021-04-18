using CAProjectV2.Data;
using CAProjectV2.Logic;
using CAProjectV2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CAProjectV2.Controllers
{
    public class LogInController : Controller
    {
        public IActionResult Index()
        {
            string loginUser = HttpContext.Session.GetString("Userid");

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
            User result =  _context.User.Where(x => x.UserName == logInUser.UserName 
                                               && x.Password == password)
                                        .FirstOrDefault();
            if (result != null)
            {
                HttpContext.Session.SetString("Userid", result.Id);
                HttpContext.Session.SetString("isLogin", result.UserName);
           
                string guestLogin = Request.Cookies["GuestLogin"];
                
                var items = _context.ShoppingCartItem.Where(x => x.UserId == guestLogin)
                                                     .FirstOrDefault();
              
                if (items != null)
                {
                    string userLogin = HttpContext.Session.GetString("Userid");
                    items.UserId = userLogin;

                    _context.SaveChanges();
                }
               
                return RedirectToAction("Index", "Products");  
         

            }

            return Redirect("Index");  
            

        }

        
    }
}
