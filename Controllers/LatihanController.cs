using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PgBookStore.Data;

namespace PgBookStore.Controllers
{
    public class LatihanController : Controller
    {
        private readonly ApplicationDbContext db;

        public LatihanController(ApplicationDbContext dbContext)
        {
            db = dbContext;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Template()
        {
            ViewBag.Authors = db.Authors.ToList();
            return View();
        }
    }
}