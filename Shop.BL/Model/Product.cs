using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.BL.Model
{
    public class Product
    {
        public int ProductId { get; set; }
        public virtual ICollection<Sell>? Sells { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? Articul { get; set; }
        public string? Unit { get; set; }
        public int Count { get; set; }
        public override string ToString()
        {
            return Name ?? " ";
        }
        public Product(string? name, decimal price, string? unit, int count, string? articul = "")
        {            
            Name = name;
            Price = price;
            Count = count;
            Unit = unit;
            Articul = articul;

        }
        public Product()
        {

        }
    }
}
