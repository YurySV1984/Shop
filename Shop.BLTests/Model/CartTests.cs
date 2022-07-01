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
    public class CartTests
    {
        [TestMethod()]
        public void CartTest()
        {            
            //Arrange
            var customer = new Customer("test customer");
            var product1 = new Product("test product 1", 150, "test unit 1", 10);
            product1.ProductId = 1;
            var product2 = new Product("test product 2", 250, "test unit 2", 20);
            product2.ProductId = 2;
            var cart = new Cart(customer);

            var expectedResult = new List<Product>()
            {
                product1, product1, product1, product2
            };
            List<Product> cartResult;
            //Act
            cart.Add(product1);
            cart.Add(product1);
            cart.Add(product2);
            cart.Add(product1);
            cartResult = cart.GetProductsList();
            //Assert
            Assert.AreEqual(expectedResult.Count, cartResult.Count);
            for (int i = 0; i < expectedResult.Count; i++)
            {
                Assert.AreEqual(expectedResult[i], cartResult[i]);
            }
        } 
    }
}