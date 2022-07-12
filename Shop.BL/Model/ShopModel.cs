using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.BL.Model
{
    /// <summary>
    /// Модель магазина.
    /// </summary>
    public class ShopModel
    {
        ContentGenerator _contentGenerator = new();
        public event EventHandler<CashBox> CashBoxChanged;
        public event EventHandler<int> CashBoxDeleted;
        /// <summary>
        /// Число работающих касс.
        /// </summary>
        public int NumberOfCashBoxes { get; set; } = 2;
        /// <summary>
        /// Задержка для задания скорости работы касс.
        /// </summary>
        public int SleepDequeue { get; set; } = 24000;
        /// <summary>
        /// Задержка для задания скорости генерации покупателей.
        /// </summary>
        public int SleepEnqueue { get; set; } = 5000;
        /// <summary>
        /// Пауза симулятора.
        /// </summary>
        public bool IsPaused { get; set; } = false;
        /// <summary>
        /// Очередь продавцов на новые кассы.
        /// </summary>
        public Queue<Seller>? Sellers { get; set; }
        /// <summary>
        /// Новые покупатели.
        /// </summary>
        public BlockingCollection<Customer>? Customers { get; set; }
        public BlockingCollection<CashBox>? CashBoxes { get; set; }

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
            CashBoxes = new BlockingCollection<CashBox>();
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
                if (!IsPaused)
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
                        var minQueueOfAllCashBoxes = CashBoxes?.Where(box => !box.IsClosing).Min(box => box.Queue.Count);
                        var listCashBoxWithMinQueue = CashBoxes?.Where(box => box.Queue.Count == minQueueOfAllCashBoxes).ToList();
                        var cashBoxWithMinQueue = listCashBoxWithMinQueue[random.Next(0, listCashBoxWithMinQueue.Count)];
                        cashBoxWithMinQueue?.Enqueue(cart);
                        Thread.Sleep(random.Next(SleepEnqueue / 4, SleepEnqueue));
                    }
                }
            }
        }
        private void CashBoxDequeue(CashBox cashBox)
        {
            while (IsWorking)
            {
                if (!IsPaused)
                {
                    if (!CashBoxes.Contains(cashBox))
                    {
                        break;
                    }
                    Thread.Sleep(10 * random.Next(SleepDequeue / 4, SleepDequeue) / cashBox.Seller.Skills);
                    if (!cashBox.Queue.IsEmpty)
                    {
                        var cashOfCashBox = cashBox.Dequeue();
                        CashList[cashBox.Number - 1] += cashOfCashBox;
                        CashBoxChanged?.Invoke(this, cashBox);
                        Thread.Sleep(500);
                    }
                }
            }
        }

        public void AddCashBox()
        {
            NumberOfCashBoxes += 1;

            var sellerToQueue = _contentGenerator.GenerateSellers(1);
            Sellers = new Queue<Seller>();
            Sellers.Enqueue(sellerToQueue.FirstOrDefault());
            var newCashBox = new CashBox(CashBoxes.Count + 1, Sellers.Dequeue());
            bool tryAdd = false;
            while (!tryAdd)
            {
                tryAdd = CashBoxes.TryAdd(newCashBox);
            }
            

            var tempCashList = CashList;
            CashList = new decimal[NumberOfCashBoxes];
            for (int i = 0; i < tempCashList.Length; i++)
            {
                CashList[i] = tempCashList[i];
            }

            Task.Run(() => CashBoxDequeue(newCashBox));
        }

        public void DeleteCashBox()
        {
            if (NumberOfCashBoxes <= 1)
            {
                return;
            }
            
            Task.Run(() =>
            {
                CashBox cBox = null;
                foreach (var box in CashBoxes)
                {
                    cBox = box;
                }
                cBox.IsClosing = true;
                while (cBox.Count() > 0)
                {
                    Thread.Sleep(200);
                }


                NumberOfCashBoxes -= 1;
                var tempCashList = CashList;
                CashBox[] boxesArray = new CashBox[NumberOfCashBoxes];
                var tryTake = false;
                for (int i = 0; i < boxesArray.Length; i++)
                {
                        boxesArray[i] = CashBoxes.Take();
                }
                CashBoxes = new BlockingCollection<CashBox>();
                foreach (var cashBox in boxesArray)
                {
                        CashBoxes.Add(cashBox);
                }

                CashList = new decimal[NumberOfCashBoxes];
                for (int i = 0; i < CashList.Length; i++)
                {
                    CashList[i] = tempCashList[i];
                }
                CashBoxDeleted?.Invoke(this, NumberOfCashBoxes);
            }
            );

        }

        public List<Product> GetStoreProducts() => _contentGenerator.GetStoreProducts();
        public void IncreaseProducts(int amount)
        {
            _contentGenerator.IncreaseProducts(amount);
        }

    }
}
