using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectIS
{
    public class Sale
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int BuyerId { get; set; }
        public float FinalPrice { get; set; }
        public DateTime SaleDate { get; set; }

        public SaleStatus Status { get; set; }
        public Product Product { get; set; }
        public User Buyer { get; set; }
    }

    public enum SaleStatus
    {
        Pending,
        Approved,
        Canceled
    }
}
