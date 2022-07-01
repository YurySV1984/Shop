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

namespace Shop.CatalogWindows.CatalogSellersWindow
{
    internal class CatalogSellersViewModel : ViewModel
    {
        private ShopContext context;
        private ObservableCollection<Seller> _sellers = new ObservableCollection<Seller>();
        public ObservableCollection<Seller> Sellers
        {
            get { return _sellers; }
            set { Set(ref _sellers, value); }
        }
        public CatalogSellersViewModel()
        {
            context = new ShopContext();
            Sellers = new ObservableCollection<Seller>(context.Sellers.ToList());
            DeleteSellerCommand = new LambdaCommand(OnDeleteSellerExecuted, CanDeleteSeller);
            ChangeSellerCommand = new LambdaCommand(OnChangeSellerExecuted, CanChangeSeller);
        }

        private Seller _selectedSeller;
        public Seller SelectedSeller
        {
            get { return _selectedSeller; }
            set { _selectedSeller = value; }
        }



        #region Delete seller click
        /// <summary>
        /// Команда "Удалить".
        /// </summary>
        public ICommand DeleteSellerCommand { get; }
        private bool CanDeleteSeller(object p) => true;
        private void OnDeleteSellerExecuted(object p)
        {
            if (p is Seller seller)
            {
                Sellers.Remove(seller);
                context.Sellers.Remove(seller);
                context.SaveChanges();
            }
        }
        #endregion
        #region Change seller click
        /// <summary>
        /// Команда "Изменить".
        /// </summary>
        public ICommand ChangeSellerCommand { get; }
        private bool CanChangeSeller(object p) => true;
        private void OnChangeSellerExecuted(object p)
        {
            if (p is Seller seller)
            {
                var sellerToChange = context.Sellers.Where(s => s.SellerId == seller.SellerId).FirstOrDefault();
                if (sellerToChange == null)
                {
                    context.Sellers.Add(seller);
                    MessageBox.Show($"Продукт {seller.Name} добавлен");
                }
                else MessageBox.Show($"Продукт {seller.Name} изменен");
                context.SaveChanges();
            }
        }
        #endregion


        ~CatalogSellersViewModel()
        {
            context.Dispose();
        }
    }
}
