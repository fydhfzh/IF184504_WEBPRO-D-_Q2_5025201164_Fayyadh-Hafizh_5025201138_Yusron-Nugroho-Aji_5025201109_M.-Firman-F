using Microsoft.AspNetCore.Mvc;
using Quiz2.Data;
using Quiz2.Models;

namespace Quiz2.Controllers
{
    
    public class RegisterController : Controller
    {
        public readonly ApplicationDbContext _db;

        public RegisterController (ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(User obj)
        {
            if(ModelState.IsValid) 
            { 
                _db.Users.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Your account has successfully created.";
                return RedirectToAction("Index", "Login", new { area = "" });
            }
            return View(obj);
        }
    }
}
