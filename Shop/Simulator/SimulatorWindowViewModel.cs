using Shop.BL.Model;
using Shop.Commands.Base;
using System;
using System.Collections.Concurrent;
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

        private string _checkString;
        public string CheckString
        {
            get { return _checkString; }
            set { Set(ref _checkString, value); }
        }

        object locker = new();  // объект-заглушка

        private int _sleepEnqueue = 5000;
        public int SleepEnqueue
        {
            get { return _sleepEnqueue; }
            set { Set(ref _sleepEnqueue, value); }
        }
        private int _sleepDequeue = 24000;
        public int SleepDequeue
        {
            get { return _sleepDequeue; }
            set { Set(ref _sleepDequeue, value); }
        }

        //private List<string> _checkstrings = new List<string>();
        //public List<string> Checkstrings
        //{
        //    get { return _checkstrings; }
        //    set { Set(ref _checkstrings, value); }
        //}

        private ObservableCollection<string> _checkStringsVM;
        public ObservableCollection<string> CheckStringsVM
        {
            get { return _checkStringsVM; }
            set { Set(ref _checkStringsVM, value); }
        }


        public SimulatorWindowViewModel()
        {
            StartCommand = new LambdaCommand(OnStartExecuted, CanStart);
            StopCommand = new LambdaCommand(OnStopExecuted, CanStop);
            AddCashBoxCommand = new LambdaCommand(OnAddCashBoxExecuted, CanAddCashBox);
            DeleteCashBoxCommand = new LambdaCommand(OnDeleteCashBoxExecuted, CanDeleteCashBox);
        }

        private Notifications notifications;
        
        //private BlockingCollection<string> _strings;
        //public List<string> ChckStrngs
        //{
        //    get { return _strings == null ? new List<string>() : _strings.ToList(); }
        //    //set { Set(ref _strings, value); }
        //}


        private ObservableCollection<CashBoxTable> _cashBoxesTable;
        public ObservableCollection<CashBoxTable> CashBoxesTable
        {
            get { return _cashBoxesTable; }
            set { Set(ref _cashBoxesTable, value); }
        }
        private CashBoxTable[] _cashBoxTablesArray { get; set; }

        #region Start sim click
        /// <summary>
        /// Команда "Старт".
        /// </summary>
        public ICommand StartCommand { get; }
        private bool CanStart(object p) => true;
        private void OnStartExecuted(object p)
        {
            shopModel.StartShop();
            shopModel.CashBoxChanged += new EventHandler<CashBox>(CashBoxChanged);
            notifications = new();
            //_strings = notifications.CheckStrings;
            Task.Run(() =>
            {
                CashBoxes = shopModel.CashBoxes;
                _cashBoxTablesArray = new CashBoxTable[shopModel.CashBoxes.Count];
                for (int i = 0; i < _cashBoxTablesArray.Length; i++)
                {
                    _cashBoxTablesArray[i] = new CashBoxTable(CashBoxes[i]);
                }

                while (shopModel.IsWorking)
                {
                    shopModel.SleepEnqueue = SleepEnqueue;
                    shopModel.SleepDequeue = SleepDequeue;
                    CashBoxesTable = new ObservableCollection<CashBoxTable>(_cashBoxTablesArray);
                    CheckStringsVM = notifications.GetCheckStrings();
                }
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

        #region Add cashbox click
        /// <summary>
        /// Команда "Добавить кассу".
        /// </summary>
        public ICommand AddCashBoxCommand { get; }
        private bool CanAddCashBox(object p) => true;
        private void OnAddCashBoxExecuted(object p)
        {
            shopModel.AddCashBox();
            CashBoxes = shopModel.CashBoxes;
            _cashBoxTablesArray = new CashBoxTable[shopModel.CashBoxes.Count];
            
            for (int i = 0; i < _cashBoxTablesArray.Length; i++)
            {
                _cashBoxTablesArray[i] = new CashBoxTable(CashBoxes[i]);
                _cashBoxTablesArray[i].CashBoxProfit = shopModel.CashList[i];
            }
        }
        #endregion

        #region Delete cashbox click
        /// <summary>
        /// Команда "Удалить кассу".
        /// </summary>
        public ICommand DeleteCashBoxCommand { get; }
        private bool CanDeleteCashBox(object p) => true;
        private void OnDeleteCashBoxExecuted(object p)
        {
            shopModel.DeleteCashBox();
            CashBoxes = shopModel.CashBoxes;
            _cashBoxTablesArray = new CashBoxTable[shopModel.CashBoxes.Count];
            for (int i = 0; i < _cashBoxTablesArray.Length; i++)
            {
                _cashBoxTablesArray[i] = new CashBoxTable(CashBoxes[i]);
            }
        }
        #endregion

        private void CashBoxChanged(object? sender, CashBox cashBox)
        {
            var check = cashBox.CurrentCheck;
            
            CheckString = $"{check.Customer} купил у продавца {check.Seller} товаров на сумму {check.CheckSum}, время {check.Created:dd.MM, hh:mm:ss}";
            notifications.AddCheckString(CheckString);
            CurrentCheck = check;
            _cashBoxTablesArray[cashBox.Number - 1].CashBoxProfit = shopModel.CashList[cashBox.Number - 1];
        }
    }
}
