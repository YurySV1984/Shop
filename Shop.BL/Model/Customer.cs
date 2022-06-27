using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.BL.Model
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string? Name { get; set; }
        public virtual ICollection<Check>? Checks { get; set; }
        public override string ToString()
        {
            return Name ?? "";
        }
    }
}
