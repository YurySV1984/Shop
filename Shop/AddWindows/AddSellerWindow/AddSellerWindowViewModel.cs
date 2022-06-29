using Shop.BL.Model;
using Shop.Commands.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Shop.AddWindows.AddSellerWindow
{
    internal class AddSellerWindowViewModel
    {
        private string? _sellerName;
        public string? SellerName
        {
            get { return _sellerName; }
            set { _sellerName = value; }
        }

        public AddSellerWindowViewModel()
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
            if (string.IsNullOrWhiteSpace(SellerName))
            {
                MessageBox.Show("Некорректные данные");
                return;
            }
            var seller = new Seller(SellerName);
            using (var context = new ShopContext())
            {
                if (context?.Sellers?.Where(s => s.Name == SellerName).FirstOrDefault() == null)
                {
                    context?.Sellers?.Add(seller);
                    context?.SaveChanges();
                    MessageBox.Show($"Продавец \"{SellerName}\" успешно добавлен");
                }
                else { MessageBox.Show($"Продавец \"{SellerName}\" уже есть в базе"); }

            }
        }
        #endregion
    }
}
