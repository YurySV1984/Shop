using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.BL.Model
{
    public class CashBoxTable
    {
        private CashBox CashBox { get; set; }
        
        public int CashBoxNumber => CashBox.Number;

        public int CashBoxQueueCount
        {
            get { return CashBox.Queue.Count; }
            set { CashBoxQueueCount = value; }
        }
        public int CashBoxExitCustomers => CashBox.ExitCustomer;
        public int CashBoxCustomersCount => CashBox.CustomersCount;
        public string CashBoxSeller => CashBox.Seller.Name;
        public int CashBoxSellerSkills => CashBox.Seller.Skills;
        private Check CurrentCheck => CashBox.CurrentCheck;
        public decimal? CurrentCheckSum => CurrentCheck == null ? 0 : CurrentCheck.CheckSum;
        public string CurrentCheckCustomer => CurrentCheck == null ? "" : CurrentCheck.Customer.Name;
        public DateTime CurrentCheckCreated => CurrentCheck == null ? default : CurrentCheck.Created;
        public decimal CashBoxProfit { get; set; } = 0;
        public CashBoxTable(CashBox cashBox)
        {
            CashBox = cashBox;
        }
    }
}
