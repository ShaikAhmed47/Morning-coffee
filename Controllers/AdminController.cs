using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using MorningCoffee.Models;
using MorningCoffee.ViewModels;

namespace MorningCoffee.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly MorningCoffeeDbContext db=new MorningCoffeeDbContext();

        private bool IsAdmin { get { return (Session["Role"] as string) == "Admin"; } }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!IsAdmin) filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { controller="Home", action="Index" }));
            base.OnActionExecuting(filterContext);
        }

        public ActionResult Dashboard()
        {
            var vm=new DashboardViewModel {
                ProductCount=db.Products.Count(), CustomerCount=db.Customers.Count(), OrderCount=db.Orders.Count(),
                Revenue=db.Orders.Where(x=>x.Status!="Cancelled").Select(x=>(decimal?)x.Total).Sum() ?? 0,
                RecentOrders=db.Orders.Include(x=>x.Customer).OrderByDescending(x=>x.OrderDate).Take(8).ToList()
            };
            return View(vm);
        }

        public ActionResult Products() { return View(db.Products.Include(x=>x.Category).OrderBy(x=>x.Name).ToList()); }

        public ActionResult ProductForm(int? id)
        {
            ViewBag.Categories=db.Categories.Where(x=>x.IsActive).ToList();
            return View(id.HasValue ? db.Products.Find(id.Value) : new Product());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult ProductForm(Product model)
        {
            if(!ModelState.IsValid) { ViewBag.Categories=db.Categories.Where(x=>x.IsActive).ToList(); return View(model); }
            if(model.ProductId==0) db.Products.Add(model); else { db.Entry(model).State=EntityState.Modified; }
            db.SaveChanges(); return RedirectToAction("Products");
        }

        public ActionResult DeleteProduct(int id)
        {
            var p=db.Products.Find(id); if(p!=null){p.IsAvailable=false;db.SaveChanges();}
            return RedirectToAction("Products");
        }

        public ActionResult Categories(){return View(db.Categories.OrderBy(x=>x.Name).ToList());}

        public ActionResult CategoryForm(int? id){return View(id.HasValue?db.Categories.Find(id.Value):new Category());}

        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult CategoryForm(Category model)
        {
            if(!ModelState.IsValid)return View(model);
            if(model.CategoryId==0)db.Categories.Add(model);else db.Entry(model).State=EntityState.Modified;
            db.SaveChanges();return RedirectToAction("Categories");
        }

        public ActionResult Orders(){return View(db.Orders.Include(x=>x.Customer).OrderByDescending(x=>x.OrderDate).ToList());}

        public ActionResult OrderDetails(int id)
        {
            var o=db.Orders.Include("Customer").Include("OrderDetails").FirstOrDefault(x=>x.OrderId==id);
            if(o==null)return HttpNotFound(); return View(o);
        }

        public ActionResult EditOrderStatus(int id)
        {
            var o=db.Orders.Find(id); if(o==null)return HttpNotFound(); return View(o);
        }

        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult EditOrderStatus(int id,string status)
        {
            var o=db.Orders.Find(id); if(o==null)return HttpNotFound();
            o.Status=status;db.SaveChanges();return RedirectToAction("Orders");
        }

        public ActionResult Users(){return View(db.Users.OrderBy(x=>x.FullName).ToList());}
    }
}
