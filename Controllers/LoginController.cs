using Microsoft.AspNetCore.Mvc;
using Quiz2.Data;
using Quiz2.Models;
using System.Diagnostics.Eventing.Reader;

namespace Quiz2.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _db;

        public LoginController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Authenticate(User obj)
        {   
            if(ModelState.IsValid) {
                var user = _db.Users.FirstOrDefault(u => u.Email == obj.Email && u.Password == obj.Password);
                if (user != null)
                {
                    HttpContext.Session.SetString("Name", user.Name.ToString());
                    HttpContext.Session.SetString("Email", user.Email.ToString());
                    HttpContext.Session.SetInt32("Id", user.Id);
                    return RedirectToAction("Index", "Home", new { area = "" });
                }
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
