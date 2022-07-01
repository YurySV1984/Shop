using Shop.BL.Model;
using Shop.Commands.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Shop.CatalogWindows.CatalogCustomersWindow
{
    internal class CatalogCustomresViewModel : ViewModel
    {
        private ShopContext context;
        private ObservableCollection<Customer> _customers = new ObservableCollection<Customer>();
        public ObservableCollection<Customer> Customers
        {
            get { return _customers; }
            set { Set(ref _customers, value); }
        }
        public CatalogCustomresViewModel()
        {
            context = new ShopContext();
            Customers = new ObservableCollection<Customer>(context.Customers.ToList());
            DeleteCustomerCommand = new LambdaCommand(OnDeleteCustomerExecuted, CanDeleteCustomer);
            ChangeCustomerCommand = new LambdaCommand(OnChangeCustomerExecuted, CanChangeCustomer);
        }

        private Customer _selectedCustomer;
        public Customer SelectedCustomer
        {
            get { return _selectedCustomer; }
            set { _selectedCustomer = value; }
        }



        #region Delete customer click
        /// <summary>
        /// Команда "Удалить".
        /// </summary>
        public ICommand DeleteCustomerCommand { get; }
        private bool CanDeleteCustomer(object p) => true;
        private void OnDeleteCustomerExecuted(object p)
        {
            if (p is Customer customer)
            {
                Customers.Remove(customer);
                context.Customers.Remove(customer);
                context.SaveChanges();
            }
        }
        #endregion
        #region Change customer click
        /// <summary>
        /// Команда "Изменить".
        /// </summary>
        public ICommand ChangeCustomerCommand { get; }
        private bool CanChangeCustomer(object p) => true;
        private void OnChangeCustomerExecuted(object p)
        {
            if (p is Customer customer)
            {
                var customerToChange = context.Customers.Where(s => s.CustomerId == customer.CustomerId).FirstOrDefault();
                if (customerToChange == null)
                {
                    context.Customers.Add(customer);
                    MessageBox.Show($"Продукт {customer.Name} добавлен");
                }
                else MessageBox.Show($"Продукт {customer.Name} изменен");
                context.SaveChanges();
            }
        }
        #endregion


        ~CatalogCustomresViewModel()
        {
            context.Dispose();
        }
    }
}
