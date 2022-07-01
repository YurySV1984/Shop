using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.BL.Model
{
    public class ShopModel
    {
        ContentGenerator _contentGenerator = new();

        public Queue<Seller> Sellers { get; set; }
        public List<CashBox> CashBoxes { get; set; }
        public List<Check> Checks { get; set; }
        public List<Sell> Sells { get; set; }
        public List<Cart> Carts { get; set; }
        public ShopModel()
        {
            var sellersToQueue = _contentGenerator.GenerateSellers(5);
            _contentGenerator.GenerateProducts(500);
            _contentGenerator.GenerateCustomers(100);
            //Продавцы на кассы.
            Sellers = new Queue<Seller>();
            CashBoxes = new List<CashBox>();
            foreach (var seller in sellersToQueue)
            {
                Sellers.Enqueue(seller);
            }
            //Создание касс.
            for (int i = 0; i < 5; i++)
            {
                CashBoxes.Add(new CashBox(CashBoxes.Count + 1, Sellers.Dequeue()));
            }
        }

        public async void StartShop()
        {
            var customers = _contentGenerator.GenerateCustomers(15);
            var carts = new Queue<Cart>();
            foreach (var customer in customers)
            {
                var cart = new Cart(customer);
                var productsToAdd = _contentGenerator.GetRandomProducts(1, 15);
                foreach (var product in productsToAdd)
                {
                    cart.Add(product);
                }
                carts.Enqueue(cart);
            }


            while (carts.Count > 0)
            {
                var minQueueCashBoxCount = CashBoxes.Min(box => box.Queue.Count);
                var cashBox = CashBoxes.FirstOrDefault(box => box.Queue.Count == minQueueCashBoxCount);
                cashBox.Enqueue(carts.Dequeue());
            }

            var cashlist = new List<(int, decimal)>();
            foreach (var cashbox in CashBoxes)
            {
                var cashKey = await WorkOneCashBox(cashbox);
                var intVal = cashbox.Number;
                cashlist.Add((intVal, cashKey));
            }

        }

        private async Task<decimal> WorkOneCashBox(CashBox cashBox)
        {
            var rnd = new Random();
            decimal result = 0;
            Thread.Sleep(rnd.Next(25,500));
            result += cashBox.Dequeue();


            return result;
        }
        
    }
}
