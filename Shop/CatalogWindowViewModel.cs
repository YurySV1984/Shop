using Shop.BL;
using Shop.BL.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Shop
{
    internal class CatalogWindowViewModel : ViewModel
    {
        public bool IsProducts { get; set; }
        private ObservableCollection<Product> _products = new ObservableCollection<Product>();
        public ObservableCollection<Product> Products
        {
            get { return _products; }
            set { Set(ref _products, value); }
        }

        private ObservableCollection<Seller> _sellers = new ObservableCollection<Seller>();
        public ObservableCollection<Seller> Sellers
        {
            get { return _sellers; }
            set { Set(ref _sellers, value); }
        }

        private ObservableCollection<Customer> _customers = new ObservableCollection<Customer>();
        public ObservableCollection<Customer> Customers
        {
            get { return _customers; }
            set { Set(ref _customers, value); }
        }

        private ObservableCollection<Check> _checks = new ObservableCollection<Check>();
        public ObservableCollection<Check> Checks
        {
            get { return _checks; }
            set { Set(ref _checks, value); }
        }

        public CatalogWindowViewModel()
        {
            using (var context = new ShopContext())
            {
                if (IsProducts)
                {
                    _products = new ObservableCollection<Product>(context.Products.ToList());

                }

            }
        }





    }
}
