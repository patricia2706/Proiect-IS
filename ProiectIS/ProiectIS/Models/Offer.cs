using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectIS
{
    public class Offer
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int BuyerId { get; set; }
        public float OfferedPrice { get; set; }
        public OfferStatus Status { get; set; }

        public Product Product { get; set; }
        public User Buyer { get; set; }
    }

    public enum OfferStatus
    {
        Pending,
        Accepted,
        Rejected
    }
}
