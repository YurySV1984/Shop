using Shop.BL.Model;
using Shop.Commands.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Shop.Simulator
{
    internal class SimulatorWindowViewModel : ViewModel
    {
        private ShopModel shopModel = new();
        public Queue<Seller>? Sellers { get; set; }

        private ObservableCollection<Customer> _customers = new ObservableCollection<Customer>();
        public ObservableCollection<Customer> Customers
        {
            get { return _customers; }
            set { Set(ref _customers, value); }
        }

        private ObservableCollection<CashBox>? _cashBoxes;

        public ObservableCollection<CashBox>? CashBoxes
        {
            get { return _cashBoxes; }
            set { Set(ref _cashBoxes, value); }
        }

        private Check _currentCheck;
        public Check CurrentCheck
        {
            get { return _currentCheck; }
            set { Set(ref _currentCheck, value); }
        }

        private ObservableCollection<string> _checksList = new ObservableCollection<string>();
        public ObservableCollection<string> ChecksList
        {
            get { return _checksList; }
            set { Set(ref _checksList, value); }
        }

        private int _selectedCheck;

        public int SelectedCheck
        {
            get { return _selectedCheck; }
            set { Set(ref _selectedCheck, value); }
        }
        //public int SelectedCheck
        //{
        //    get { return _selectedCheck; }
        //    set { Set(ref _selectedCheck, value); }
        //}

        private string _checkString;
        public string CheckString
        {
            get { return _checkString; }
            set { Set(ref _checkString, value); }
        }

        object locker = new();  // объект-заглушка
        //public ObservableCollection<Check>? Checks { get; set; }
        //public ObservableCollection<Sell>? Sells { get; set; }
        //public ObservableCollection<Cart>? Carts { get; set; }

        private ObservableCollection<decimal> _cashList;
        public ObservableCollection<decimal> CashList
        {
            get { return _cashList; }
            set { Set(ref _cashList, value); }
        }

        private List<string> checkstrings = new List<string>();

        public SimulatorWindowViewModel()
        {
            StartCommand = new LambdaCommand(OnStartExecuted, CanStart);
            StopCommand = new LambdaCommand(OnStopExecuted, CanStop);
        }

        #region Start sim click
        /// <summary>
        /// Команда "Старт".
        /// </summary>
        public ICommand StartCommand { get; }
        private bool CanStart(object p) => true;
        private void OnStartExecuted(object p)
        {
            shopModel.StartShop();

            Task.Run(() =>
            {
                Sellers = shopModel.Sellers;
                Customers = shopModel.Customers;

                //CashList = shopModel.CashList;

                CashBoxes = shopModel.CashBoxes;
                foreach (var cashbox in CashBoxes)
                {
                    cashbox.IsSold += new EventHandler<Check>(LoadCheck);
                }
                //while (shopModel.IsWorking)
                //{
                //    //Sellers = shopModel.Sellers;
                //    //Customers = shopModel.Customers;
                    
                //    //CashList = shopModel.CashList;
                    
                //}
            });


        }
        #endregion

        #region Stop sim click
        /// <summary>
        /// Команда "Стоп".
        /// </summary>
        public ICommand StopCommand { get; }
        private bool CanStop(object p) => true;
        private void OnStopExecuted(object p)
        {
            shopModel.StopShop();
            
        }
        #endregion

        private void LoadCheck(object sender, Check check)
        {
            CheckString = $"{check.Customer} купил у продавца {check.Seller} товаров на сумму {check.CheckSum}, время {check.Created:dd.MM, hh:mm:ss}";
            CurrentCheck = check;
            checkstrings.Add(CheckString);
                ChecksList = new ObservableCollection<string>(checkstrings);
                SelectedCheck = ChecksList.Count - 1;
                CashList = new ObservableCollection<decimal>(shopModel.CashList);
           
        }
    }
}
