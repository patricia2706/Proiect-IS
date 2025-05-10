using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectIS
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int SellerId { get; set; }
        public User Seller { get; set; }
        public string Description { get; set; }
        public bool IsNegotiable { get; set; }
        public float MinPrice { get; set; }


    }
}
