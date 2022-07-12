using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop
{
    public class CartVM
    {
        public string ProductName { get; set; }
        public int ProductCount { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal ProductSumPrice { get { return ProductPrice * ProductCount; } } 
        public CartVM(string productName, int productCount, decimal productPrice)
        {
            ProductName = productName;
            ProductCount = productCount;
            ProductPrice = productPrice;
        }
    }
}
