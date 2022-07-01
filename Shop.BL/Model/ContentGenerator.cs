using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.BL.Model
{
    public class ContentGenerator
    {
        public List<CashBox> CashBoxes = new();
        public List<Customer> Customers = new();
        public List<Seller> Sellers = new();
        public List<Product> Products = new();
        Random rnd = new();
        public List<Customer> GenerateCustomers(int amountToGenerate)
        {
            var result = new List<Customer>();
            for (int i = 0; i < amountToGenerate; i++)
            {
                var customer = new Customer("customer number " + rnd.Next(1, 1000))
                {
                    CustomerId = Customers.Count
                };
                Customers.Add(customer);
                result.Add(customer);
            }   
            return result;
        }

        public List<Seller> GenerateSellers(int amountToGenerate)
        {
            var result = new List<Seller>();
            for (int i = 0; i < amountToGenerate; i++)
            {
                var seller = new Seller("seller number " + rnd.Next(1, 1000))
                {
                    SellerId = Sellers.Count
                };
                Sellers.Add(seller);
                result.Add(seller);
            }
            return result;
        }

        public List<Product> GenerateProducts(int amountToGenerate)
        {
            var result = new List<Product>();
            for (int i = 0; i < amountToGenerate; i++)
            {
                var product = new Product("товар " + rnd.Next(1, 1000), rnd.Next(50, 1000), "ед.изм", rnd.Next(10, 1000))
                {
                    ProductId = Products.Count
                };
                Products.Add(product);
                result.Add(product);
            }
            return result;
        }

        /// <summary>
        /// Получить список произвольное количество товаров от min до max.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public List<Product> GetRandomProducts(int min, int max)
        {
            var result = new List<Product>();
            //var rnd = new Random();
            var resultCount = rnd.Next(min, max);
            for (int i = 0; i < resultCount; i++)
            {
                result.Add(Products[rnd.Next(Products.Count - 1)]);
            }


            return result;
        }
    }
}
