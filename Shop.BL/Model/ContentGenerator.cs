using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.BL.Model
{
    public class ContentGenerator
    {
        private List<Product> Products = new();
        Random rnd = new();
        public BlockingCollection<Customer> GenerateCustomers(int amountToGenerate)
        {
            var result = new BlockingCollection<Customer>();
            //Customers.Clear();
            for (int i = 0; i < amountToGenerate; i++)
            {
                var customer = new Customer("customer " + (Guid.NewGuid().ToString()).Substring(0, 5))
                {
                    //CustomerId = Customers.Count
                };
                //Customers.Add(customer);
                result.Add(customer);
            }   
            return result;
        }

        public List<Seller> GenerateSellers(int amountToGenerate)
        {
            var result = new List<Seller>();
            for (int i = 0; i < amountToGenerate; i++)
            {
                var seller = new Seller("seller " + (Guid.NewGuid().ToString()).Substring(0,5), rnd.Next(4,11))
                {
                    //SellerId = Sellers.Count
                };
                //Sellers.Add(seller);
                result.Add(seller);
            }
            return result;
        }

        public List<Product> GenerateProducts(int amountToGenerate)
        {
            var result = new List<Product>();
            for (int i = 0; i < amountToGenerate; i++)
            {
                var product = new Product("товар " + (Guid.NewGuid().ToString()).Substring(0, 5), rnd.Next(50, 1001), "ед.изм", rnd.Next(100, 1001))
                {
                    //ProductId = Products.Count
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
            var resultCount = rnd.Next(min, max + 1);
            for (int i = 0; i < resultCount; i++)
            {
                result.Add(Products[rnd.Next(Products.Count)]);
            }            
            return result;
        }

        public List<Product> GetStoreProducts()
        {
            return Products;
        }

        public void IncreaseProducts(int amount)
        {
            foreach (var product in Products)
            {

            }
            var productsToIncrease = Products.Where(product => product.Count < amount);
            foreach (var product in productsToIncrease)
            {
                product.Count += amount;
            }
        }
    }
}
