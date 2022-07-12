using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.BL.Model
{
    public class Seller
    {
        public int SellerId { get; set; }
        public string? Name { get; set; }
        public int Skills { get; set; }
        public virtual ICollection<Check>? Checks { get; set; }
        public override string ToString()
        {
            return Name ?? "";
        }
        public Seller(string? name, int skills)
        {
            Name = name;
            Skills = skills;
        }

        public Seller() { }
    }
}
