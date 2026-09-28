using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MorningCoffee.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        [Required, StringLength(100)] public string Name { get; set; }
        [StringLength(250)] public string Description { get; set; }
        public bool IsActive { get; set; } = true;
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }

    public class Product
    {
        public int ProductId { get; set; }
        [Required, StringLength(150)] public string Name { get; set; }
        [StringLength(500)] public string Description { get; set; }
        [Range(0.01, 999999)] public decimal Price { get; set; }
        public int CategoryId { get; set; }
        [StringLength(500)] public string ImageUrl { get; set; }
        public bool IsAvailable { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public virtual Category Category { get; set; }
    }

    public class Customer
    {
        public int CustomerId { get; set; }
        [Required, StringLength(120)] public string FullName { get; set; }
        [Required, StringLength(150)] public string Email { get; set; }
        [StringLength(20)] public string Phone { get; set; }
        [StringLength(300)] public string Address { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }

    public class AppUser
    {
        [Key]
        public int UserId { get; set; }
        [Required, StringLength(120)] public string FullName { get; set; }
        [Required, StringLength(150)] public string Email { get; set; }
        [Required, StringLength(100)] public string PasswordHash { get; set; }
        [Required, StringLength(20)] public string Role { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        [StringLength(30)] public string Status { get; set; }
        [StringLength(30)] public string PaymentMethod { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public virtual Customer Customer { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }

    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        [StringLength(150)] public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal LineTotal { get; set; }
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
