using Shop.BL.Model;
using Shop.Commands.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Shop.AddProductWindows
{
    internal class AddProductWindowViewModel
    {
        private string? _productName;
        public string? ProductName
        {
            get { return _productName; }
            set { _productName = value; }
        }

        private decimal? _price;
        public decimal? ProductPrice
        {
            get { return _price; }
            set { _price = value; }
        }

        private string? _productUnit;
        public string? ProductUnit
        {
            get { return _productUnit; }
            set { _productUnit = value; }
        }

        private string? _productArticul;
        public string? ProductArticul
        {
            get { return _productArticul; }
            set { _productArticul = value; }
        }


        private int? _count = null;
        public int? ProductCount
        {
            get { return _count; }
            set { _count = value; }
        }






        public AddProductWindowViewModel()
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
            if (string.IsNullOrWhiteSpace(ProductName) || ProductPrice < 0 || ProductPrice == null || ProductCount.GetValueOrDefault() < 0 || ProductCount == null || string.IsNullOrWhiteSpace(ProductUnit))
            {
                MessageBox.Show("Некорректные данные");
                return;
            }
            var product = new Product(ProductName, (decimal)ProductPrice, ProductUnit, (int)ProductCount);
            using (var context = new ShopContext())
            {
                if (context.Products.Where(p => p.Name == ProductName).FirstOrDefault() == null)
                {
                    context.Products.Add(product);
                    context.SaveChanges();
                    MessageBox.Show($"Продукт \"{ProductName}\" успешно добавлен");
                }
                else { MessageBox.Show($"Продукт \"{ProductName}\" уже есть в базе"); }
                
            }
        }
        #endregion
    }
}
