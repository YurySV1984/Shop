using System;
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
        public int Number { get; set; }
        /// <summary>
        /// Продавец на кассе.
        /// </summary>
        public Seller Seller { get; set; }
        /// <summary>
        /// Очередь на кассе.
        /// </summary>
        public Queue<Cart> Queue { get; set; }
        /// <summary>
        /// Максимальная длина очереди.
        /// </summary>
        public int MaxLength { get; set; } = 10;
        /// <summary>
        /// Счетчик покупателей, ушедших без покупки.
        /// </summary>
        public int ExitCustomer { get; set; }
        public Check CurrentCheck { get; set; }

        public event EventHandler<Check> IsSold;
        public int Count()
        {
            return Queue.Count;
        }
        public bool IsSimulation { get; set; }

        private ShopContext context = new ShopContext();
        public CashBox(int number, Seller seller)
        {
            Number = number;
            Seller = seller;
            Queue = new Queue<Cart>();
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
            }
        }

        public decimal Dequeue()
        {
            decimal result = 0;
            var cart = Queue.Dequeue();
            if (cart != null)
            {
                var check = new Check
                {
                    Seller = this.Seller,
                    SellerId = this.Seller.SellerId,
                    Customer = cart.Customer,
                    CustomerId = cart.Customer.CustomerId,
                    Created = DateTime.Now,
                    //CheckProducts = new List<Product>(),
                    CheckSum = 0
                };
                if (!IsSimulation)
                {
                    check.CheckId = 0;
                }

                var sells = new List<Sell>();
                foreach (Product product in cart)
                {
                    if (product.Count > 0)
                    {
                        var sell = new Sell
                        {
                            CheckId = check.CheckId,
                            Check = check,
                            ProductId = product.ProductId,
                            Product = product
                        };
                        sells.Add(sell);
                        //check.CheckProducts.Add(product);
                        //check.CheckSum += product.Price;
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
