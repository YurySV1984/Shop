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
    public class ShopModelTests
    {
        [TestMethod()]
        public void StartShopTest()
        {
            //Arrange
            var shop = new ShopModel();
            //Act
            shop.StartShop();
            //Assert

        }
    }
}