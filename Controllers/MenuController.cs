using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using MorningCoffee.Models;

namespace MorningCoffee.Controllers
{
    public class MenuController : Controller
    {
        private readonly MorningCoffeeDbContext db = new MorningCoffeeDbContext();

        public ActionResult Index(string search, int? categoryId)
        {
            var q = db.Products.Include(x => x.Category).Where(x => x.IsAvailable);
            if (!string.IsNullOrWhiteSpace(search)) q = q.Where(x => x.Name.Contains(search) || x.Description.Contains(search));
            if (categoryId.HasValue) q = q.Where(x => x.CategoryId == categoryId.Value);
            ViewBag.Categories = db.Categories.Where(x => x.IsActive).ToList();
            return View(q.OrderBy(x => x.Name).ToList());
        }

        public ActionResult Details(int id)
        {
            var p = db.Products.Include(x => x.Category).FirstOrDefault(x => x.ProductId == id);
            if (p == null) return HttpNotFound();
            return View(p);
        }
    }
}
