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

namespace Shop.CatalogWindows.CatalogProductsWindow
{
    internal class CatalogProductsViewModel : ViewModel
    {
        private ShopContext context;
        private ObservableCollection<Product> _products = new ObservableCollection<Product>();
        public ObservableCollection<Product> Products
        {
            get { return _products; }
            set { Set(ref _products, value); }
        }
        public CatalogProductsViewModel()
        {
            context = new ShopContext();
            Products = new ObservableCollection<Product>(context.Products.ToList());
            DeleteProductCommand = new LambdaCommand(OnDeleteProductExecuted, CanDeleteProduct);
            ChangeProductCommand = new LambdaCommand(OnChangeProductExecuted, CanChangeProduct);
        }

        private Product _selectedProduct;
        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set { _selectedProduct = value; }
        }



        #region Delete product click
        /// <summary>
        /// Команда "Удалить".
        /// </summary>
        public ICommand DeleteProductCommand { get; }
        private bool CanDeleteProduct(object p) => true;
        private void OnDeleteProductExecuted(object p)
        {
            if (p is Product product)
            {
                Products.Remove(product);
                context.Products.Remove(product);
                context.SaveChanges();
            }
        }
        #endregion
        #region Change product click
        /// <summary>
        /// Команда "Изменить".
        /// </summary>
        public ICommand ChangeProductCommand { get; }
        private bool CanChangeProduct(object p) => true;
        private void OnChangeProductExecuted(object p)
        {
            if (p is Product product)
            {
                var prodToChange = context.Products.Where(x => x.ProductId == product.ProductId).FirstOrDefault();
                if (prodToChange == null)
                {
                    context.Products.Add(product);
                    MessageBox.Show($"Продукт {product.Name} добавлен");
                }
                else MessageBox.Show($"Продукт {product.Name} изменен");
                context.SaveChanges();
                
            }

        }
        #endregion


        ~CatalogProductsViewModel()
        { 
            context.Dispose();
        }
    }
}
