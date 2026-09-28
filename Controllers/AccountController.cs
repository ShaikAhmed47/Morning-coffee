using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using MorningCoffee.Models;
using MorningCoffee.ViewModels;

namespace MorningCoffee.Controllers
{
    public class AccountController : Controller
    {
        private readonly MorningCoffeeDbContext db = new MorningCoffeeDbContext();

        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid) return View(model);
            var hash = Hash(model.Password);
            var user = db.Users.FirstOrDefault(x => x.Email == model.Email && x.PasswordHash == hash);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid email or password.");
                return View(model);
            }
            FormsAuthentication.SetAuthCookie(user.Email, model.RememberMe);
            Session["UserId"] = user.UserId;
            Session["UserName"] = user.FullName;
            Session["Role"] = user.Role;
            if (user.Role == "Admin") return RedirectToAction("Dashboard", "Admin");
            return Redirect(returnUrl ?? Url.Action("Index", "Home"));
        }

        public ActionResult Register() { return View(); }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            if (db.Users.Any(x => x.Email == model.Email))
            {
                ModelState.AddModelError("Email", "Email already registered.");
                return View(model);
            }
            var user = new AppUser { FullName = model.FullName, Email = model.Email, PasswordHash = Hash(model.Password), Role = "Customer" };
            db.Users.Add(user);
            db.SaveChanges();
            db.Customers.Add(new Customer { FullName = model.FullName, Email = model.Email, Phone = model.Phone });
            db.SaveChanges();
            FormsAuthentication.SetAuthCookie(user.Email, false);
            Session["UserId"] = user.UserId; Session["UserName"] = user.FullName; Session["Role"] = user.Role;
            return RedirectToAction("Index", "Menu");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public static string Hash(string value)
        {
            using (var sha = SHA256.Create())
                return string.Concat(sha.ComputeHash(Encoding.UTF8.GetBytes(value)).Select(b => b.ToString("x2")));
        }
    }
}
