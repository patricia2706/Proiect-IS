using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectIS
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserStatus Status { get; set; } = UserStatus.Buyer;

        public List<Product> Products { get; set; }

        public List<Sale> Sales { get; set; }
        public List<Offer> Offers { get; set; }

    }

    public enum UserStatus
    {
        Admin,
        Buyer,
        Seller,
        PendingSeller,
        Canceled
    }

}
