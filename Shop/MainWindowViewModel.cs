using Shop.AddWindows;
using Shop.Commands.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            OpenChecksCommand = new LambdaCommand(OnOpenChecksExecuted, CanOpenChecks);
            OpenCustomersCommand = new LambdaCommand(OnOpenCustomersExecuted, CanOpenCustomers);
        }

        #region Products Click
        /// <summary>
        /// Команда "Продукты".
        /// </summary>
        public ICommand OpenProductsCommand { get; }
        private bool CanOpenProducts(object p) => true;
        private void OnOpenProductsExecuted(object p)
        {
            var catalogCustomers = new Catalog("products");
            catalogCustomers.Show();
            catalogCustomers.Owner = App.Current.MainWindow;
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
            addProductWindow.Show();
            addProductWindow.Owner = App.Current.MainWindow;
        }
        #endregion



        #region Sellers Click
        /// <summary>
        /// Команда "Продукты".
        /// </summary>
        public ICommand OpenSellersCommand { get; }
        private bool CanOpenSellers(object p) => true;
        private void OnOpenSellersExecuted(object p)
        {
            var catalogCustomers = new Catalog("sellers");
            catalogCustomers.Show();
            catalogCustomers.Owner = App.Current.MainWindow;
        }
        #endregion
        #region Checks Click
        /// <summary>
        /// Команда "Продукты".
        /// </summary>
        public ICommand OpenChecksCommand { get; }
        private bool CanOpenChecks(object p) => true;
        private void OnOpenChecksExecuted(object p)
        {
            var catalogCustomers = new Catalog("checks");
            catalogCustomers.Show();
            catalogCustomers.Owner = App.Current.MainWindow;
        }
        #endregion
        #region Checks Customers
        /// <summary>
        /// Команда "Продукты".
        /// </summary>
        public ICommand OpenCustomersCommand { get; }
        private bool CanOpenCustomers(object p) => true;
        private void OnOpenCustomersExecuted(object p)
        {
            var catalogCustomers = new Catalog("customers");
            catalogCustomers.Show();
            catalogCustomers.Owner = App.Current.MainWindow;
        }
        #endregion
    }
}
