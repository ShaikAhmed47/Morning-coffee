using System;
using System.Linq;
using System.Web.Mvc;
using MorningCoffee.Models;
using MorningCoffee.ViewModels;

namespace MorningCoffee.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly MorningCoffeeDbContext db = new MorningCoffeeDbContext();

        private Customer CurrentCustomer()
        {
            var email = User.Identity.Name;
            return db.Customers.FirstOrDefault(x => x.Email == email);
        }

        public ActionResult Checkout()
        {
            var customer = CurrentCustomer();
            var cart = CartController.GetCart(Session);
            if (customer == null) return RedirectToAction("Register", "Account");
            if (!cart.Any()) return RedirectToAction("Index", "Cart");
            return View(new CheckoutViewModel { FullName=customer.FullName, Email=customer.Email, Phone=customer.Phone, Address=customer.Address });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Checkout(CheckoutViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var customer = CurrentCustomer();
            var cart = CartController.GetCart(Session);
            var products = db.Products.Where(x => cart.Keys.Contains(x.ProductId)).ToList();
            if (!products.Any()) return RedirectToAction("Index", "Cart");

            customer.FullName=model.FullName; customer.Phone=model.Phone; customer.Address=model.Address;
            var order = new Order { CustomerId=customer.CustomerId, Status="Pending", PaymentMethod=model.PaymentMethod };
            foreach (var p in products)
                order.OrderDetails.Add(new OrderDetail { ProductId=p.ProductId, ProductName=p.Name, UnitPrice=p.Price, Quantity=cart[p.ProductId], LineTotal=p.Price*cart[p.ProductId] });
            order.Subtotal = order.OrderDetails.Sum(x => x.LineTotal);
            order.Tax = decimal.Round(order.Subtotal * 0.05m, 2);
            order.Total = order.Subtotal + order.Tax;
            db.Orders.Add(order);
            db.SaveChanges();
            CartController.Clear(Session);
            return RedirectToAction("Confirmation", new { id = order.OrderId });
        }

        public ActionResult Confirmation(int id)
        {
            var customer=CurrentCustomer();
            var order=db.Orders.Include("OrderDetails").FirstOrDefault(x=>x.OrderId==id && x.CustomerId==customer.CustomerId);
            if(order==null) return HttpNotFound();
            return View(order);
        }

        public ActionResult MyOrders()
        {
            var customer=CurrentCustomer();
            return View(db.Orders.Where(x=>x.CustomerId==customer.CustomerId).OrderByDescending(x=>x.OrderDate).ToList());
        }
    }
}
