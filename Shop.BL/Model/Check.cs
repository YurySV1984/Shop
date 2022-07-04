﻿using System;
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
        public decimal? CheckSum { get; set; }
        //public List<Product> CheckProducts { get; set; }
        public override string ToString()
        {
            return $"Чек {CheckId} от {Created:dd.MM hh:mm:ss}";
        }
        public Check() { }
        public Check(DateTime created)
        {
            Created = created;
        }
    }
}
