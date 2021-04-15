using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAProjectV2.Controllers
{
    public class LogOutController : Controller
    {
        public IActionResult Index()
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("isLogin")))
            {
                HttpContext.Session.Clear();
            }
            else
            {

                return RedirectToAction("Index", "LogIn");
            }

            
            return View();
        }
    }
}
