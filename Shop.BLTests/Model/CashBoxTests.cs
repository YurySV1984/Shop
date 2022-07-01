using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shop.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.BL.Model.Tests
{
    [TestClass()]
    public class CashBoxTests
    {
        [TestMethod()]
        public void CashBoxTest()
        {
            //Arrange
            var customer1 = new Customer()
            {
                Name = "test customer 1",
                CustomerId = 1
            };
            var customer2 = new Customer()
            {
                Name = "test customer 2",
                CustomerId = 2
            };
            var seller = new Seller()
            {
                Name = "test seller",
                SellerId = 1
            };

            var product1 = new Product("test product 1", 100, "test unit 1", 10);
            product1.ProductId = 1;
            var product2 = new Product("test product 2", 200, "test unit 2", 20);
            product2.ProductId = 2;
            var cart1 = new Cart(customer1);
            var cart2 = new Cart(customer2);
            var cashBox = new CashBox(1, seller);

            var cart1ExpectedResult = 500;
            var cart2ExpectedResult = 600;

            //Act
            cart1.Add(product1);
            cart1.Add(product1);
            cart1.Add(product2);
            cart1.Add(product1);
            cart2.Add(product2);
            cart2.Add(product1);
            cart2.Add(product1);
            cart2.Add(product2);
            cashBox.Enqueue(cart1);
            cashBox.Enqueue(cart2);
            var cart1ActualResult = cashBox.Dequeue();
            var cart2ActualResult = cashBox.Dequeue();
            //Assert
            Assert.AreEqual(cart1ActualResult, cart1ExpectedResult);
            Assert.AreEqual(cart2ActualResult, cart2ExpectedResult);
            Assert.AreEqual(5, product1.Count);
            Assert.AreEqual(17, product2.Count);
        }

        
    }
}