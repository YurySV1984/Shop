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

namespace Shop.CatalogWindows.CatalogChecksWindow
{
    internal class CatalogChecksViewModel : ViewModel
    {
        private ShopContext Context;
        //private ObservableCollection<Check> _checks = new();
        public ObservableCollection<Check> Checks
        {
            get { return new ObservableCollection<Check>(Context.Checks); }
            //set { Set(ref _checks, value); }
        }
        public CatalogChecksViewModel()
        {
            Context = new ShopContext();
            //Checks = new ObservableCollection<Check>(context.Checks);
            CheckCommand = new LambdaCommand(OnCheckExecuted, CanCheck);
        }

        private Check _selectedCheck;
        public Check SelectedCheck
        {
            get { return _selectedCheck; }
            set { Set(ref _selectedCheck, value); }
        }
        private ObservableCollection<string> _checkReport;
        public ObservableCollection<string> CheckReport
        {
            get { return _checkReport; }
            set { Set(ref _checkReport, value); }
        }



        #region 
        /// <summary>
        /// Команда "Чек".
        /// </summary>
        public ICommand CheckCommand { get; }
        private bool CanCheck(object p) => true;
        private void OnCheckExecuted(object p)
        {
            ShopHandler ShopHandler = new ShopHandler();
            if (p is Check check)
            {
                CheckReport = ShopHandler.GetCheckReport(check);
            }
            ShopHandler.Dispose();
        }
        #endregion


        ~CatalogChecksViewModel()
        {
            Context.Dispose();
            
        }
    }
}
