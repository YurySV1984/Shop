using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.BL.Model
{
    public class ShopHandler
    {
        private ShopContext Context { get; set; } = new ShopContext();
        private Cart Cart { get; set; }
        private Seller Seller { get; set; }
        public Dictionary<Product, int> Products
        {
            get { return Cart.Products; }
            set { Cart.Products = value; }
        }

        public ObservableCollection<Seller> GetSellersFromContext() => new ObservableCollection<Seller>(Context.Sellers);
        public ObservableCollection<Customer> GetCustomersFromContext() => new ObservableCollection<Customer>(Context.Customers);
        public ObservableCollection<Product> GetProductsFromContext() => new ObservableCollection<Product>(Context.Products);
        public void Dispose() => Context.Dispose();
        public void SetSeller(Seller seller)
        {
            if (string.IsNullOrWhiteSpace(seller.Name) || seller == null)
            {
                throw new ArgumentNullException("Name"," name is null or empty");
            }
            Seller = seller;
        }
        public string GetSellerName()
        {
            return Seller.Name;
        }
        public Cart GetCart()
        {
            return Cart;
        }
        public int GetProductsCount() => Cart.Products.Count;
        public void SetCart(Cart cart)
        {
            Cart = cart ?? throw new ArgumentNullException(nameof(cart), "cart is null");
        }
        public void AddProduct(Product product)
        {
            Cart.Add(product);
        }
        public ShopHandler(Customer customer)
        {
            Cart = new Cart(customer);
        }

        public ShopHandler() { }

        public void MakeDeal()
        {
            if (Cart != null)
            {
                var check = new Check(DateTime.Now)
                {
                    Seller = this.Seller,
                    SellerId = this.Seller.SellerId,
                    Customer = Cart.Customer,
                    CustomerId = Cart.Customer.CustomerId,
                };
                var sells = new List<Sell>();
                foreach (Product product in Cart)
                {
                    if (product.Count > 0)
                    {
                        //check.ProductsInCheck.Add(product);

                        var sell = new Sell
                        {
                            CheckId = check.CheckId,
                            Check = check,
                            ProductId = product.ProductId,
                            Product = product
                        };
                        sells.Add(sell);

                        Context.Sells.Add(sell);


                        product.Count--;
                        check.CheckSum += product.Price;
                    }
                }

                Context.Checks.Add(check);
                Context.SaveChanges();

            }
        }

        public object GetSellerReport(Seller seller)
        {
            var id = seller.SellerId;
            //список чеков продавца
            var sellerCheckList = Context.Checks.Where(check => check.SellerId == id).ToArray();
            //сумма чеков продавца
            var sellerCheckSum = sellerCheckList.Select(check => check.CheckSum).Sum();
            //список CheckId продавца
            var sellerCheckIdList = sellerCheckList.Select(check => check.CheckId).ToArray();


            Sell[] sells = new Sell[sellerCheckList.Length]; 
            var i = 0;
            var sellerSells = Context.Sells.Where(sell => sellerCheckIdList.Contains(sell.CheckId)).ToArray();
            foreach (var check in sellerCheckList)
            {
                foreach (var sell in sellerSells)
                {
                    if (check.Sells.Contains(sell))
                    {
                        check.ProductsInCheck.Add(sell.Product);
                    }
                }
            }
            Context.Dispose();
            Context = new ShopContext();
            

            
            return sellerCheckList;
            
            
        }
    }
}
