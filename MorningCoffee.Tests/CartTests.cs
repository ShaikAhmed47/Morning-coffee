using NUnit.Framework;
using MorningCoffee.ViewModels;
using System.Linq;

namespace MorningCoffee.Tests
{
    [TestFixture]
    public class CartTests
    {
        [Test]
        public void CartLine_Calculates_LineTotal_Correctly()
        {
            // Arrange
            var line = new CartLine
            {
                ProductId = 1,
                Name = "Espresso",
                UnitPrice = 3.50m,
                Quantity = 2
            };

            // Act
            decimal total = line.LineTotal;

            // Assert
            Assert.AreEqual(7.00m, total);
        }

        [Test]
        public void CartViewModel_Initialization_Is_Empty()
        {
            // Arrange & Act
            var cartVm = new CartViewModel();

            // Assert
            Assert.IsNotNull(cartVm.Items);
            Assert.AreEqual(0, cartVm.Items.Count);
            Assert.AreEqual(0m, cartVm.Subtotal);
            Assert.AreEqual(0m, cartVm.Tax);
            Assert.AreEqual(0m, cartVm.Total);
        }
    }
}
