using Shop.BL.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Shop
{
    /// <summary>
    /// Interaction logic for Catalog.xaml
    /// </summary>
    public partial class Catalog : Window
    {
        //private ObservableCollection<Customer>? _customerList;
        ShopContext context;
        public Catalog(string what)
        {
            InitializeComponent();
            context = new ShopContext();
            switch (what)
            {
                case "customers":
                        DataGridView.ItemsSource = new ObservableCollection<Customer>(context.Customers?.ToList());
                    break;
                case "products":
                        DataGridView.ItemsSource = new ObservableCollection<Product>(context.Products?.ToList());
                    break;
                case "sellers":
                        DataGridView.ItemsSource = new ObservableCollection<Seller>(context.Sellers?.ToList());
                    break;
                case "checks":
                        DataGridView.ItemsSource = new ObservableCollection<Check>(context.Checks?.ToList());
                    break;

                default:
                    break;
            }
            
            


        }

    }
}
