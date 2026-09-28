using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MorningCoffee.Models;
using MorningCoffee.ViewModels;

namespace MorningCoffee.Controllers
{
    public class CartController : Controller
    {
        private readonly MorningCoffeeDbContext db = new MorningCoffeeDbContext();
        private const string Key = "MorningCoffeeCart";

        private Dictionary<int,int> Cart
        {
            get { return Session[Key] as Dictionary<int,int> ?? new Dictionary<int,int>(); }
            set { Session[Key] = value; }
        }

        public ActionResult Index()
        {
            var cart = Cart;
            var products = db.Products.Where(x => cart.Keys.Contains(x.ProductId)).ToList();
            var vm = new CartViewModel();
            foreach (var p in products)
                vm.Items.Add(new CartLine { ProductId=p.ProductId, Name=p.Name, UnitPrice=p.Price, Quantity=cart[p.ProductId] });
            vm.Subtotal = vm.Items.Sum(x => x.LineTotal);
            vm.Tax = decimal.Round(vm.Subtotal * 0.05m, 2);
            vm.Total = vm.Subtotal + vm.Tax;
            return View(vm);
        }

        [HttpPost]
        public ActionResult Add(int id, int quantity = 1)
        {
            var p = db.Products.Find(id);
            if (p == null || !p.IsAvailable) return HttpNotFound();
            var cart = Cart;
            if (cart.ContainsKey(id)) cart[id] += quantity; else cart[id] = quantity;
            Cart = cart;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Update(int id, int quantity)
        {
            var cart = Cart;
            if (quantity <= 0) cart.Remove(id); else cart[id] = quantity;
            Cart = cart;
            return RedirectToAction("Index");
        }

        public ActionResult Remove(int id)
        {
            var cart = Cart; cart.Remove(id); Cart = cart;
            return RedirectToAction("Index");
        }

        public static Dictionary<int,int> GetCart(System.Web.HttpSessionStateBase session)
        {
            return session[Key] as Dictionary<int,int> ?? new Dictionary<int,int>();
        }

        public static void Clear(System.Web.HttpSessionStateBase session)
        {
            session[Key] = new Dictionary<int,int>();
        }
    }
}
