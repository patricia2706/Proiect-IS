using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace ProiectIS.Helpers
{
    public static class ValidationIS
    {
        public static bool emailValidation(string str)
        {
            if(str.ToLower().EndsWith("@email.com") && str.Contains("@") && str.IndexOf("@") > 0)
            {
                return true;
            }
            return false;
        }

        public static bool priceValidation(string str, float minPrice)
        {
            if(float.Parse(str) < 0 || minPrice < 0)
            {
                return false;
            }
            if(float.Parse(str) > minPrice)
            {
                return true;
            }
            return false;
        }

        public static bool statusAdminValidation(UserStatus s) {

            if (s == UserStatus.Admin) {
                return true;
            }
            return false;
        }

        public static List<Product> listSalesApproved(List<Product> allProducts,List<Sale> sales){
            var products = new List<Product>();
            foreach (var item in allProducts) 
            {
                var found = sales.FirstOrDefault(x => x.ProductId == item.Id && x.Status.Equals(SaleStatus.Approved));

                if (found is null)
                {
                    products.Add(item);
                }
            }
            return products;
        }

        public static void removeOffers(AppDbContext _db, List<Offer> offers) {
            foreach (var item in offers) {
                _db.Offers.Remove(item);
            }

        }
    }
}
