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
        public event EventHandler<CashBox> CashBoxChanged;
        //const int numberOfCashBoxes = 2;
        public int NumberOfCashBoxes { get; set; } = 2;
        public int SleepDequeue { get; set; } = 24000;
        public int SleepEnqueue { get; set; } = 5000;
        public Queue<Seller>? Sellers { get; set; }
        public ObservableCollection<Customer>? Customers { get; set; }
        public ObservableCollection<CashBox>? CashBoxes { get; set; }

        public decimal[] CashList;
        public bool IsWorking { get; set; } = false;
        Random random = new();
        public ShopModel()
        {
            var sellersToQueue = _contentGenerator.GenerateSellers(NumberOfCashBoxes);
            CashList = new decimal[NumberOfCashBoxes];
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
            for (int i = 0; i < NumberOfCashBoxes; i++)
            {
                CashBoxes.Add(new CashBox(CashBoxes.Count + 1, Sellers.Dequeue()));
            }
        }

        public void StartShop()
        {
            IsWorking = true;
            Task.Run(() => CreateCarts(4));            
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

                Thread.Sleep(500);
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
                    Thread.Sleep(random.Next(SleepEnqueue / 4, SleepEnqueue)); ;
                }
            }
        }
        private void CashBoxDequeue(CashBox cashBox)
        {
            while (IsWorking)
            {
                if (!CashBoxes.Contains(cashBox))
                {
                    break;
                }
                Thread.Sleep(random.Next(SleepDequeue / 4, SleepDequeue));
                if (cashBox.Queue.Count > 0)
                {
                    var cashOfCashBox = cashBox.Dequeue();
                    CashList[cashBox.Number - 1] += cashOfCashBox;
                    CashBoxChanged?.Invoke(this, cashBox);
                    Thread.Sleep(500);
                }
            }
        }

        public void AddCashBox()
        {
            NumberOfCashBoxes += 1;

            var sellerToQueue = _contentGenerator.GenerateSellers(1);
            Sellers = new Queue<Seller>();
            Sellers.Enqueue(sellerToQueue.FirstOrDefault());
            CashBoxes.Add(new CashBox(CashBoxes.Count + 1, Sellers.Dequeue()));

            var tempCashList = CashList;

            CashList = new decimal[NumberOfCashBoxes];
            for (int i = 0; i < tempCashList.Length; i++)
            {
                CashList[i] = tempCashList[i];
            }
            Task.Run(() => CashBoxDequeue(CashBoxes[^1]));
        }

        public void DeleteCashBox()
        {
            if (NumberOfCashBoxes <= 1)
            {
                return;
            }
            NumberOfCashBoxes -= 1;
            var tempCashList = CashList;
            var indexToRemove = CashBoxes.Max(cb => cb.Number) - 1;
            CashBoxes.RemoveAt(indexToRemove);
            CashList = new decimal[NumberOfCashBoxes];
            for (int i = 0; i < CashList.Length; i++)
            {
                CashList[i] = tempCashList[i];
            }
        }

    }
}
