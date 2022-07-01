using Shop.AddWindows;
using Shop.AddWindows.AddCheckWindow;
using Shop.AddWindows.AddCustomerWindow;
using Shop.AddWindows.AddProductWindow;
using Shop.AddWindows.AddSellerWindow;
using Shop.BL.Model;
using Shop.Commands.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Shop
{
    internal class MainWindowViewModel : ViewModel
    {
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

    }
}
