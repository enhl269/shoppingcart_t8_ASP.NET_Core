using CAProjectV2.Data;
using CAProjectV2.Logic;
using CAProjectV2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CAProjectV2.Controllers
{
    public class ProfileController : Controller
    {
        

        private readonly WebsiteContext _context;

        public ProfileController(WebsiteContext context)
        {
            _context = context;
        }

        public IActionResult RenderProfile()
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("isLogin"))){    //checking session is null or not which means checking user log in or out

                var id = HttpContext.Session.GetString("Userid");
                User user = _context.User.AsNoTracking().Where(x => x.Id == id).FirstOrDefault();
                ProfileViewModel pf = new ProfileViewModel(user.UserImageUrl, user.FirstName, user.LastName, user.UserName, user.Email, user.PhoneNumber, "", "");
                return View(pf);
            }
            else
            {

                return RedirectToAction("Index", "LogIn");                           //if not log in, render login page
            }
        }

        public IActionResult Edit()
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("isLogin")))
            {    //checking session is null or not which means checking user log in or out

                var id = HttpContext.Session.GetString("Userid");
                User user = _context.User.AsNoTracking().Where(x => x.Id == id).FirstOrDefault();
                ProfileViewModel pf = new ProfileViewModel(user.UserImageUrl, user.FirstName, user.LastName, user.UserName, user.Email, user.PhoneNumber, "", "");
                return View(pf);
            }
            else
            {

                return RedirectToAction("Index", "LogIn");                           //if not log in, render login page
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(ProfileViewModel profile, IFormFile UserImageUrl)
        {

            if (UserImageUrl != null && UserImageUrl.Length > 0)
            {
                var fileName = Path.GetFileName(UserImageUrl.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);
                using (var fileSteam = new FileStream(filePath, FileMode.Create))
                {
                    await UserImageUrl.CopyToAsync(fileSteam);
                }
                profile.UserImageUrl = fileName;
            }

            var oldPassword = Encrption.Encrypt(profile.OldPassword);
            User result = _context.User.Where(x => x.UserName == profile.UserName && x.Password == oldPassword).FirstOrDefault();
            if (result != null) {
                var id = HttpContext.Session.GetString("Userid");                //validate recent login user by calling session id
                User user = _context.User.Where(x => x.Id == id).FirstOrDefault();
                user.UserName = profile.UserName;
                user.FirstName = profile.FirstName;
                user.LastName = profile.LastName;
                user.Email = profile.Email;     
                user.PhoneNumber = profile.PhoneNumber;
                if (!string.IsNullOrEmpty(profile.UserImageUrl))
                {
                    user.UserImageUrl = profile.UserImageUrl;
                }
                if (!string.IsNullOrEmpty(profile.NewPassword)) {
                    user.Password = Encrption.Encrypt(profile.NewPassword);
                }
                _context.SaveChanges();

                return RedirectToAction("RenderProfile");
            }
            return RedirectToAction("Error");
        }


    }
}
