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
        /// <summary>
        /// Модель магазина.
        /// </summary>
        private ShopModel shopModel = new();
   
        private Check _currentCheck;
        /// <summary>
        /// Последний чек на всех кассах.
        /// </summary>
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

        object locker = new();

        private int _sleepEnqueue = 5000;
        /// <summary>
        /// Задержка для генерации покупателей, мс.
        /// </summary>
        public int SleepEnqueue
        {
            get { return _sleepEnqueue; }
            set { Set(ref _sleepEnqueue, value); }
        }

        private int _sleepDequeue = 24000;
        /// <summary>
        /// Задержка для работы касс, мс.
        /// </summary>
        public int SleepDequeue
        {
            get { return _sleepDequeue; }
            set { Set(ref _sleepDequeue, value); }
        }

        private bool _isPaused = false;
        /// <summary>
        /// Пауза.
        /// </summary>
        public bool IsPaused
        {
            get { return _isPaused; }
            set { Set(ref _isPaused, value); }
        }
        private int _amountOfProductsToIncrease;

        public int AmountOfProductsToIncrease
        {
            get { return _amountOfProductsToIncrease; }
            set { Set(ref _amountOfProductsToIncrease, value); }
        }

        private ObservableCollection<string> _checkStringsVM;
        /// <summary>
        /// Отображение событий обработки чека в окне.
        /// </summary>
        public ObservableCollection<string> CheckStringsVM
        {
            get { return _checkStringsVM; }
            set { Set(ref _checkStringsVM, value); }
        }

        private ObservableCollection<CashBoxTable> _cashBoxesTable;
        /// <summary>
        /// Отображение касс и их параметров в окне.
        /// </summary>
        public ObservableCollection<CashBoxTable> CashBoxesTable
        {
            get { return _cashBoxesTable; }
            set { Set(ref _cashBoxesTable, value); }
        }

        private ObservableCollection<Product> _storeProducts;

        public ObservableCollection<Product> StoreProducts
        {
            get { return _storeProducts; }
            set { Set(ref _storeProducts, value); }
        }


        public SimulatorWindowViewModel()
        {
            StartCommand = new LambdaCommand(OnStartExecuted, CanStart);
            StopCommand = new LambdaCommand(OnStopExecuted, CanStop);
            AddCashBoxCommand = new LambdaCommand(OnAddCashBoxExecuted, CanAddCashBox);
            DeleteCashBoxCommand = new LambdaCommand(OnDeleteCashBoxExecuted, CanDeleteCashBox);
            IncreaseProductsCommand = new LambdaCommand(OnIncreaseProductsExecuted, CanIncreaseProducts);
        }

        private Notifications notifications;
        private CashBoxTable[] CashBoxTablesArray { get; set; }

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
            Task.Run(() =>
            {
                shopModel.CashBoxDeleted += new EventHandler<int>(RebuildCashBoxTable);
                RebuildCashBoxTable(null, shopModel.CashBoxes.Count);
                //CashBoxTablesArray = new CashBoxTable[shopModel.CashBoxes.Count];
                //foreach (var cashBox in shopModel.CashBoxes)
                //{
                //    CashBoxTablesArray[cashBox.Number - 1] = new CashBoxTable(cashBox);
                //}

                while (shopModel.IsWorking)
                {
                    shopModel.SleepEnqueue = SleepEnqueue;
                    shopModel.SleepDequeue = SleepDequeue;
                    shopModel.IsPaused = IsPaused;
                    CashBoxesTable = new ObservableCollection<CashBoxTable>(CashBoxTablesArray);
                    CheckStringsVM = notifications.GetCheckStrings();
                    StoreProducts = new ObservableCollection<Product>(shopModel.GetStoreProducts());
                    
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
            RebuildCashBoxTable(null, shopModel.CashBoxes.Count);
            //CashBoxTablesArray = new CashBoxTable[shopModel.CashBoxes.Count];
            //foreach (var cashBox in shopModel.CashBoxes)
            //{
            //    var index = cashBox.Number - 1;
            //    CashBoxTablesArray[index] = new CashBoxTable(cashBox);
            //    CashBoxTablesArray[index].CashBoxProfit = shopModel.CashList[index];
            //}
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
        }
        #endregion

        #region Increase products click
        /// <summary>
        /// Команда "Завезти продукты".
        /// </summary>
        public ICommand IncreaseProductsCommand { get; }
        private bool CanIncreaseProducts(object p) => true;
        private void OnIncreaseProductsExecuted(object p)
        {
            shopModel.IncreaseProducts(AmountOfProductsToIncrease);

        }
        #endregion

        private void CashBoxChanged(object? sender, CashBox cashBox)
        {
            var check = cashBox.CurrentCheck;
            CheckString = $"{check.Customer} купил у продавца {check.Seller} (касса {cashBox.Number}) товаров на сумму {check.CheckSum}, время {check.Created:dd.MM, hh:mm:ss}";
            notifications.AddCheckString(CheckString);
            CurrentCheck = check;
            CashBoxTablesArray[cashBox.Number - 1].CashBoxProfit = shopModel.CashList[cashBox.Number - 1];
        }

        private void RebuildCashBoxTable(object? sender, int e)
        {
            CashBoxTablesArray = new CashBoxTable[shopModel.CashBoxes.Count];
            foreach (var cashBox in shopModel.CashBoxes)
            {
                var index = cashBox.Number - 1;
                CashBoxTablesArray[index] = new CashBoxTable(cashBox);
                CashBoxTablesArray[index].CashBoxProfit = shopModel.CashList[index];
            }
        }
    }
}
