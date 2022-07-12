using Shop.AddWindows.AddCheckWindow;
using Shop.AddWindows.AddCustomerWindow;
using Shop.AddWindows.AddProductWindow;
using Shop.AddWindows.AddSellerWindow;
using Shop.BL.Model;
using Shop.Commands.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Shop
{
    public class MainWindowViewModel : ViewModel
    {
        private ShopHandler ShopHandler { get; set; } = new ShopHandler();

        private ObservableCollection<CartVM> _cartVMs = new ObservableCollection<CartVM>();
        public ObservableCollection<CartVM> CartVMs
        {
            get { return _cartVMs; }
            set { Set(ref _cartVMs, value); }
        }

        private ObservableCollection<Seller> _sellersVM;
        public ObservableCollection<Seller> SellersVM
        {
            get { return ShopHandler.GetSellersFromContext(); }
            set { Set(ref _sellersVM, value); }
        }
        private Seller _selectedSeller;
        public Seller SelectedSeller
        {
            get { return _selectedSeller; }
            set { Set(ref _selectedSeller, value); }
        }

        private ObservableCollection<Customer> _customersVM;
        public ObservableCollection<Customer> CustomersVM
        {
            get { return ShopHandler.GetCustomersFromContext(); }
            set { Set(ref _customersVM, value); }
        }
        private Customer _selectedCustomer;
        public Customer SelectedCustomer
        {
            get { return _selectedCustomer; }
            set { Set(ref _selectedCustomer, value); }
        }

        private ObservableCollection<Product> _products;
        public ObservableCollection<Product> Products
        {
            get { return ShopHandler.GetProductsFromContext(); }
            set { Set(ref _products, value); }
        }

        private bool _isEnabledCreateCart = true;
        public bool IsEnabledCreateCart
        {
            get { return _isEnabledCreateCart; }
            set { Set(ref _isEnabledCreateCart, value); }
        }
        private bool _isEnabled = false;
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set { Set(ref _isEnabled, value); }
        }

        private decimal _cartSum;

        public decimal CartSum
        {
            get { return _cartSum; }
            set { Set(ref _cartSum, value); }
        }


        public MainWindowViewModel()
        {
            
            
            OpenProductsCommand = new LambdaCommand(OnOpenProductsExecuted, CanOpenProducts);
            AddProductCommand = new LambdaCommand(OnAddProductExecuted, CanAddProduct);
            OpenSellersCommand = new LambdaCommand(OnOpenSellersExecuted, CanOpenSellers);
            AddSellerCommand = new LambdaCommand(OnAddSellerExecuted, CanAddSeller);            
            OpenCustomersCommand = new LambdaCommand(OnOpenCustomersExecuted, CanOpenCustomers);
            AddCustomerCommand = new LambdaCommand(OnAddCustomerExecuted, CanAddCustomer);
            OpenChecksCommand = new LambdaCommand(OnOpenChecksExecuted, CanOpenChecks);
            AddCheckCommand = new LambdaCommand(OnAddCheckExecuted, CanAddCheck);
            OpenSimulatorCommand = new LambdaCommand(OnOpenSimulatorExecuted, CanOpenSimulator);
            AddProductToCartCommand = new LambdaCommand(OnAddProductToCartExecuted, CanAddProductToCart);
            CreateCartCommand = new LambdaCommand(OnCreateCartExecuted,CanCreateCart);
            CreateCheckCommand = new LambdaCommand(OnCreateCheckExecuted, CanCreateCheck);
        }

        #region Products Click
        /// <summary>
        /// Команда "Продукты".
        /// </summary>
        public ICommand OpenProductsCommand { get; }
        private bool CanOpenProducts(object p) => true;
        private void OnOpenProductsExecuted(object p)
        {
            var catalogProducts = new CatalogWindows.CatalogProductsWindow.CatalogProductsWindow();
            catalogProducts.ShowDialog();
            catalogProducts.Owner = App.Current.MainWindow;
        }
        #endregion
        #region Add product click
        /// <summary>
        /// Команда "Добавить для продукта".
        /// </summary>
        public ICommand AddProductCommand { get; }
        private bool CanAddProduct(object p) => true;
        private void OnAddProductExecuted(object p)
        {
            var addProductWindow = new AddProduct();
            addProductWindow.ShowDialog();
            addProductWindow.Owner = App.Current.MainWindow;
        }
        #endregion

        #region Sellers Click
        /// <summary>
        /// Команда "Продавцы".
        /// </summary>
        public ICommand OpenSellersCommand { get; }
        private bool CanOpenSellers(object p) => true;
        private void OnOpenSellersExecuted(object p)
        {
            var catalogSellers = new CatalogWindows.CatalogSellersWindow.CatalogSellersWindow();
            catalogSellers.ShowDialog();
            catalogSellers.Owner = App.Current.MainWindow;
        }
        #endregion
        #region Add seller click
        /// <summary>
        /// Команда "Добавить для продавца".
        /// </summary>
        public ICommand AddSellerCommand { get; }
        private bool CanAddSeller(object p) => true;
        private void OnAddSellerExecuted(object p)
        {
            var addSellerWindow = new AddSeller();
            addSellerWindow.ShowDialog();
            addSellerWindow.Owner = App.Current.MainWindow;
        }
        #endregion

        #region Customers click
        /// <summary>
        /// Команда "Покупатели".
        /// </summary>
        public ICommand OpenCustomersCommand { get; }
        private bool CanOpenCustomers(object p) => true;
        private void OnOpenCustomersExecuted(object p)
        {
            var catalogCustomers = new CatalogWindows.CatalogCustomersWindow.CatalogCustomersWindow();
            catalogCustomers.ShowDialog();
            catalogCustomers.Owner = App.Current.MainWindow;
        }
        #endregion
        #region Add customer click
        /// <summary>
        /// Команда "Добавить для покупателя".
        /// </summary>
        public ICommand AddCustomerCommand { get; }
        private bool CanAddCustomer(object p) => true;
        private void OnAddCustomerExecuted(object p)
        {
            var addCustomerWindow = new AddCustomer();
            addCustomerWindow.ShowDialog();
            addCustomerWindow.Owner = App.Current.MainWindow;
        }
        #endregion

        #region Checks click
        /// <summary>
        /// Команда "Чеки".
        /// </summary>
        public ICommand OpenChecksCommand { get; }
        private bool CanOpenChecks(object p) => true;
        private void OnOpenChecksExecuted(object p)
        {
            var catalogCustomers = new Catalog();
            catalogCustomers.ShowDialog();
            catalogCustomers.Owner = App.Current.MainWindow;
        }
        #endregion
        #region Add check click
        /// <summary>
        /// Команда "Добавить чек".
        /// </summary>
        public ICommand AddCheckCommand { get; }
        private bool CanAddCheck(object p) => true;
        private void OnAddCheckExecuted(object p)
        {
            var addCheckWindow = new AddCheck();
            addCheckWindow.ShowDialog();
            addCheckWindow.Owner = App.Current.MainWindow;
        }
        #endregion

        #region Simulator click
        /// <summary>
        /// Команда "Симулятор".
        /// </summary>
        public ICommand OpenSimulatorCommand { get; }
        private bool CanOpenSimulator(object p) => true;
        private void OnOpenSimulatorExecuted(object p)
        {
            var simulatorWindow = new Simulator.SimulatorWindow();
            simulatorWindow.ShowDialog();
            simulatorWindow.Owner = App.Current.MainWindow;
        }
        #endregion


        #region Create cart button
        /// <summary>
        /// Команда "Создать корзину".
        /// </summary>
        public ICommand CreateCartCommand { get; }
        private bool CanCreateCart(object p) => true;
        private void OnCreateCartExecuted(object p)
        {
            if (SelectedCustomer == null || SelectedSeller == null)
            {
                return;
            }
            Products = ShopHandler.GetProductsFromContext();
            ShopHandler.SetSeller(SelectedSeller);
            ShopHandler.SetCart(new Cart(SelectedCustomer));
            IsEnabled = true;
            IsEnabledCreateCart = false;
            CartSum = 0;
        }
        #endregion

        #region Create check button
        /// <summary>
        /// Команда "Создать чек".
        /// </summary>
        public ICommand CreateCheckCommand { get; }
        private bool CanCreateCheck(object p) => true;
        private void OnCreateCheckExecuted(object p)
        {
            if (ShopHandler.GetProductsCount() == 0 || string.IsNullOrWhiteSpace(ShopHandler.GetSellerName()) || ShopHandler.GetCart() == null || string.IsNullOrWhiteSpace(ShopHandler.GetCart().Customer.Name))
            {
                return;
            }

            ShopHandler.MakeDeal();
            IsEnabled = false;
            IsEnabledCreateCart = true;
            CartSum = 0;
            CartVMs.Clear();
        }
        #endregion

        #region Add product click
        /// <summary>
        /// Команда "Выбрать покупателя".
        /// </summary>
        public ICommand AddProductToCartCommand { get; }
        private bool CanAddProductToCart(object p) => true;
        private void OnAddProductToCartExecuted(object p)
        {
            if (p is Product product)
            {
                ShopHandler.AddProduct(product);
                CartSum += product.Price;
                CartVMs = new ObservableCollection<CartVM>();
                foreach (var prod in ShopHandler.Products)
                {
                    CartVMs.Add(new CartVM(prod.Key.Name, prod.Value, prod.Key.Price));
                }

            }   
        }
        #endregion







        ~MainWindowViewModel()
        {
            ShopHandler.Dispose();
        }
    }
}
