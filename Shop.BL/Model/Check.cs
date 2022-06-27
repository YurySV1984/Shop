using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.BL.Model
{
    public class Check
    {
        public int CheckId { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }
        public int SellerId { get; set; }
        public virtual Seller? Seller { get; set; }
        public virtual ICollection<Sell>? Sells { get; set; }
        public DateTime Created { get; set; }
        public override string ToString()
        {
            return $"Чек {CheckId} от {Created:dd.mm.yy hh:mm:ss}";
        }
    }
}
