using Microsoft.AspNetCore.Mvc;
using Quiz2.Data;
using Quiz2.Models;

namespace Quiz2.Controllers;
public class NoteController : Controller
{
    private readonly ApplicationDbContext _db;

    public NoteController(ApplicationDbContext db)
    {
        _db = db;
    }

    public IActionResult Index()
    {
        if (HttpContext.Session.GetString("Name") != null)
        {
            IEnumerable<Note> notes = _db.Notes;
            ViewBag.Name = HttpContext.Session.GetString("Name");
            return View(notes);
        }else
        {
            return RedirectToAction("Index", "Login", new { area = "" });
        }
        
    }

    //GET
    public IActionResult Create()
    {
        if (HttpContext.Session.GetString("Name") != null)
        {
            ViewBag.Name = HttpContext.Session.GetString("Name");
            return View();
        }
        else
        {
            return RedirectToAction("Index", "Login", new { area = "" });
        }


    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    //POST
    public IActionResult Create(Note obj)
    {
        if (ModelState.IsValid)
        {
            _db.Notes.Add(obj);
            _db.SaveChanges();
            TempData["success"] = "Note created succesfully.";
            return RedirectToAction("Index");
        }
        return View(obj);
    }

    //GET
    public IActionResult Edit(int? id)
    {
        if (HttpContext.Session.GetString("Name") != null)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var noteFromDb = _db.Notes.FirstOrDefault(obj => obj.Id == id);

            if (noteFromDb == null)
            {
                return NotFound();
            }
            ViewBag.Name = HttpContext.Session.GetString("Name");
            return View(noteFromDb);
        }
        else
        {
            return RedirectToAction("Index", "Login", new { area = "" });
        }
            
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    //POST
    public IActionResult Edit(Note obj)
    {
        if (ModelState.IsValid)
        {
            _db.Notes.Update(obj);
            _db.SaveChanges();
            TempData["success"] = "Note updated succesfully.";
            ViewBag.Name = HttpContext.Session.GetString("Name");
            return RedirectToAction("Index");
        }
        return View(obj);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    //POST
    public IActionResult Delete(int? id)
    {
        var obj = _db.Notes.Find(id);
        if(obj == null)
        {
            return NotFound();
        }

        _db.Notes.Remove(obj);
        _db.SaveChanges();
        TempData["success"] = "Note deleted succesfully.";
        return RedirectToAction("Index");
    }
}
