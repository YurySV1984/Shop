using Shop.BL.Model;
using Shop.Commands.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Shop.AddWindows.AddCustomerWindow
{
    internal class AddCustomerWindowViewModel
    {
        private string? _name;

        public string? CustomerName
        {
            get { return _name; }
            set { _name = value; }
        }

        public AddCustomerWindowViewModel()
        {
            SaveCommand = new LambdaCommand(OnSaveExecuted, CanSave);
        }
        
        #region Save
        /// <summary>
        /// Команда "Сохранить".
        /// </summary>
        public ICommand SaveCommand { get; }
        private bool CanSave(object p) => true;
        private void OnSaveExecuted(object p)
        {
            if (string.IsNullOrWhiteSpace(CustomerName))
            {
                MessageBox.Show("Некорректные данные");
                return;
            }
            var customer = new Customer(CustomerName);
            using (var context = new ShopContext())
            {
                if (context?.Customers?.Where(c => c.Name == CustomerName).FirstOrDefault() == null)
                {
                    context?.Customers?.Add(customer);
                    context?.SaveChanges();
                    MessageBox.Show($"Покупатель \"{CustomerName}\" успешно добавлен");
                }
                else { MessageBox.Show($"Покупатель \"{CustomerName}\" уже есть в базе"); }

            }
        }
        #endregion
    }
}
