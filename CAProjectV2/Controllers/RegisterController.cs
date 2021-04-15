using CAProjectV2.Data;
using CAProjectV2.Logic;
using CAProjectV2.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CAProjectV2.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private readonly WebsiteContext _context;

        public RegisterController(WebsiteContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(User user, IFormFile UserImageUrl)  // IFormFile, provided by .net framework -> when files are uploaded, they are saved in temporary storage, from that storage, IFormFile take those fils and put into wwwroot/
            {
            var files = HttpContext.Request.Form.Files;
            if (UserImageUrl != null && UserImageUrl.Length > 0)
            {
                var fileName = Path.GetFileName(UserImageUrl.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await UserImageUrl.CopyToAsync(fileStream);
                }
                user.UserImageUrl = fileName;
            }

            user.Id = IdGenerator.ID();
            user.Password = Encrption.Encrypt(user.Password);
            var validateUser = _context.User.Where(x => x.UserName == user.UserName || x.Email == user.Email).FirstOrDefault();
            if (validateUser == null)
            {
                await _context.User.AddAsync(user);
                 _context.SaveChanges();
                
            }
            else {
                return Redirect("Error");    // UI : need to show error message/page that user alr exit
            }
            return RedirectToAction("Index", "LogIn");
        }
    }
}
