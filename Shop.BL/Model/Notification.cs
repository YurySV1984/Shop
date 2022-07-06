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
        public BlockingCollection<string> CheckStrings { get; set; }
        //public string? LastString => CheckStrings[^1];
        public ObservableCollection<string> GetCheckStrings()
        {
            return new ObservableCollection<string>(CheckStrings);
        }
        public void AddCheckString(string checkString)
        {
            CheckStrings.Add(checkString);
        }

        public Notifications()
        {
            CheckStrings = new BlockingCollection<string>();
        }
    }
}
