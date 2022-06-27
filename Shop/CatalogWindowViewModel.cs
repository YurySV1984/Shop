using Shop.BL;
using Shop.BL.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Shop
{
    internal class CatalogWindowViewModel : ViewModel
    {
        private ObservableCollection<Customer> _customers = new ObservableCollection<Customer>();
        public ObservableCollection<Customer> Customers
        {
            get { return _customers; }
            set { Set(ref _customers, value); }
        }






        
    }
}
