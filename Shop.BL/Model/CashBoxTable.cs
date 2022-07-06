using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.BL.Model
{
    public class CashBoxTable
    {
        public CashBox CashBox { get; set; }
        
        public int CashBoxNumber => CashBox.Number;

        public int CashBoxQueueCount
        {
            get { return CashBox.Queue.Count; }
            set { CashBoxQueueCount = value; }
        }
        public int CashBoxExitCustomers => CashBox.ExitCustomer;
        public string CashBoxSeller => CashBox.Seller.Name;
        private Check _currentCheck => CashBox.CurrentCheck;
        public decimal? CurrentCheckSum => _currentCheck == null ? 0 : _currentCheck.CheckSum;
        public string CurrentCheckCustomer => _currentCheck == null ? "" : _currentCheck.Customer.Name;
        public DateTime CurrentCheckCreated => _currentCheck == null ? default(DateTime) : _currentCheck.Created;
        public decimal CashBoxProfit { get; set; } = 0;
        public CashBoxTable(CashBox cashBox)
        {
            CashBox = cashBox;
        }
    }
}
