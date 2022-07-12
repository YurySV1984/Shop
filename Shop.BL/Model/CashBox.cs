using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.BL.Model
{
    public class CashBox
    {
        /// <summary>
        /// Номер кассы.
        /// </summary>
        public int Number { get; private set; }
        /// <summary>
        /// Продавец на кассе.
        /// </summary>
        public Seller Seller { get; private set; }
        /// <summary>
        /// Очередь на кассе.
        /// </summary>
        public ConcurrentQueue<Cart> Queue { get; private set; }
        /// <summary>
        /// Максимальная длина очереди.
        /// </summary>
        public int MaxLength { get; set; } = 10;
        /// <summary>
        /// Счетчик покупателей, ушедших без покупки.
        /// </summary>
        public int ExitCustomer { get; private set; } = 0;
        public int CustomersCount { get; private set; } = 0;
        public bool IsClosing { get; set; } = false;
        public Check CurrentCheck { get; private set; }

        public event EventHandler<Check> IsSold;
        public int Count() => Queue.Count;
        public bool IsSimulation { get; set; }

        private ShopContext context = new ShopContext();
        public CashBox(int number, Seller seller)
        {
            Number = number;
            Seller = seller;
            Queue = new ConcurrentQueue<Cart>();
            IsSimulation = true;
        }

        public void Enqueue(Cart cart)
        {
            if (Queue.Count < MaxLength)
            {
                Queue.Enqueue(cart);
            }
            else
            {
                ExitCustomer++;
                cart.ReturnProducts();
            }
        }

        public decimal Dequeue()
        {
            decimal result = 0;
            var tryDeq = false;
            Cart Cart = null;
            while (!tryDeq)
            {
                tryDeq = Queue.TryDequeue(out Cart);
            }
            if (Cart != null)
            {
                var check = new Check(DateTime.Now)
                {
                    Seller = this.Seller,
                    SellerId = this.Seller.SellerId,
                    Customer = Cart.Customer,
                    CustomerId = Cart.Customer.CustomerId,
                };
                if (!IsSimulation)
                {
                    check.CheckId = 0;
                }

                //var sells = new List<Sell>();
                foreach (Product product in Cart)
                {
                    if (product.Count > 0)
                    {
                        check.ProductsInCheck.Add(product);
                        var sell = new Sell
                        {
                            CheckId = check.CheckId,
                            Check = check,
                            ProductId = product.ProductId,
                            Product = product
                        };
                        //sells.Add(sell);
                        if (!IsSimulation)
                        {
                            context.Sells.Add(sell);
                        }
                        product.Count--;
                        result += product.Price;
                    }
                }
                check.CheckSum = result;
                CurrentCheck = check;
                CustomersCount++;
                IsSold?.Invoke(this, check);
                if (!IsSimulation)
                {
                    context.Checks.Add(check);
                    context.SaveChanges();
                }
                
            }
            return result;
        }





        ~CashBox()
        {
            context.Dispose();
        }

    }
}
