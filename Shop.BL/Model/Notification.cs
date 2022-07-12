using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.BL.Model
{
    public class Notifications
    {
        private object lockObj = new object();
        private List<string> CheckStrings { get; set; } = new List<string>();
        public ObservableCollection<string> GetCheckStrings()
        {
            lock (lockObj)
            {
                return new ObservableCollection<string>(CheckStrings);
            }
        }
        public void AddCheckString(string checkString)
        {
            lock (lockObj)
            {
                CheckStrings.Add(checkString);
            }
        }

        public Notifications()
        { }
    }
}
