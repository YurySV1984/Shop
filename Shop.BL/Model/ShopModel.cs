using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.BL.Model
{
    public class ShopModel
    {
        
        ContentGenerator _contentGenerator = new();
        const int numberOfCashBoxes = 2;
        const int sleep = 25000;
        public Queue<Seller>? Sellers { get; set; }
        public ObservableCollection<Customer>? Customers { get; set; }
        public ObservableCollection<CashBox>? CashBoxes { get; set; }

        public decimal[] CashList = new decimal[numberOfCashBoxes];
        public bool IsWorking { get; set; } = false;
        Random random = new();
        public ShopModel()
        {
            var sellersToQueue = _contentGenerator.GenerateSellers(numberOfCashBoxes);
            _contentGenerator.GenerateProducts(10);
            //_contentGenerator.GenerateCustomers(10);
            //Продавцы на кассы.
            Sellers = new Queue<Seller>();
            //Кассы.
            CashBoxes = new ObservableCollection<CashBox>();
            foreach (var seller in sellersToQueue)
            {
                Sellers.Enqueue(seller);
            }
            //Создание касс.
            for (int i = 0; i < numberOfCashBoxes; i++)
            {
                CashBoxes.Add(new CashBox(CashBoxes.Count + 1, Sellers.Dequeue()));
            }
        }

        public void StartShop()
        {
            IsWorking = true;
            Task.Run(() => CreateCarts(7));            
            var tasks = CashBoxes.Select(cb => new Task(() => CashBoxDequeue(cb)));
            foreach (var task in tasks)
            {
                task.Start();
            }
        }
        public void StopShop()
        {
            IsWorking = false;
        }

        public void CreateCarts(int numberOfCustomers)
        {
            while (IsWorking)
            {
                Customers = _contentGenerator.GenerateCustomers(numberOfCustomers);
                foreach (var customer in Customers)
                {
                    var cart = new Cart(customer);
                    var productsToAdd = _contentGenerator.GetRandomProducts(5, 30);
                    foreach (Product product in productsToAdd)
                    {
                        cart.Add(product);
                    }

                    var minQueueOfAllCashBoxes = CashBoxes?.Min(box => box.Queue.Count);
                    var cashBoxWithMinQueue = CashBoxes?.FirstOrDefault(box => box.Queue.Count == minQueueOfAllCashBoxes);
                    cashBoxWithMinQueue?.Enqueue(cart);
                    Thread.Sleep(random.Next(1000, sleep/5));
                }
                

            }
        }
        private void CashBoxDequeue(CashBox cashBox)
        {
            
            while (IsWorking)
            {
                if (cashBox.Queue.Count > 0)
                {
                    var cashOfCashBox = cashBox.Dequeue();
                    CashList[cashBox.Number - 1] += cashOfCashBox;
                    Thread.Sleep(random.Next(7000, sleep));
                }
            }
        }

    }
}
