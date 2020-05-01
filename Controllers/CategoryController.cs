using Microsoft.AspNetCore.Mvc;
using PgBookStore.Data;
using PgBookStore.Models;
using System.Linq;

namespace PgBookStore.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext db;

        public CategoryController(ApplicationDbContext dbContext)
        {
            db = dbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var items = db.Catetories.Select(p => p).OrderBy(p => p.CategoryID);
            return View(items);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category item)
        {
            if(ModelState.IsValid) {
                db.Add(item);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var item = db.Catetories.SingleOrDefault(p => p.CategoryID.Equals(id));
            return View(item);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("CategoryID,Name")] Category item)
        {
            if(ModelState.IsValid) {
                db.Update(item);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if(ModelState.IsValid) {
                var item = db.Catetories.Find(id);
                db.Catetories.Remove(item);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View();
        }
    }
}