using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MorningCoffee.Models;

namespace MorningCoffee.ViewModels
{
    public class LoginViewModel
    {
        [Required, EmailAddress] public string Email { get; set; }
        [Required, DataType(DataType.Password)] public string Password { get; set; }
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required, StringLength(120)] public string FullName { get; set; }
        [Required, EmailAddress] public string Email { get; set; }
        [Required, StringLength(100, MinimumLength = 6), DataType(DataType.Password)] public string Password { get; set; }
        [Required, Compare("Password"), DataType(DataType.Password)] public string ConfirmPassword { get; set; }
        [StringLength(20)] public string Phone { get; set; }
    }

    public class CartLine
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal LineTotal { get { return UnitPrice * Quantity; } }
    }

    public class CartViewModel
    {
        public List<CartLine> Items { get; set; } = new List<CartLine>();
        public decimal Subtotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
    }

    public class CheckoutViewModel
    {
        [Required, StringLength(120)] public string FullName { get; set; }
        [Required, EmailAddress] public string Email { get; set; }
        [Required, StringLength(20)] public string Phone { get; set; }
        [Required, StringLength(300)] public string Address { get; set; }
        [Required] public string PaymentMethod { get; set; }
    }

    public class DashboardViewModel
    {
        public int ProductCount { get; set; }
        public int CustomerCount { get; set; }
        public int OrderCount { get; set; }
        public decimal Revenue { get; set; }
        public List<Order> RecentOrders { get; set; } = new List<Order>();
    }
}
