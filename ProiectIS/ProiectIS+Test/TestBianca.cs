using ProiectIS.Helpers;
using ProiectIS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectIS_Test
{
    public class TestBianca
    {
        [Fact]
        public void ReturnsTrue_WhenStatusIsAdmin()
        {

            UserStatus status = UserStatus.Admin;


            bool result = ValidationIS.statusAdminValidation(status);


            Assert.True(result);
        }

        [Fact]
        public void ReturnsFalse_WhenStatusIsBuyer()
        {

            UserStatus status = UserStatus.Buyer;


            bool result = ValidationIS.statusAdminValidation(status);


            Assert.False(result);
        }
        [Fact]
        public void ReturnsFalse_WhenStatusIsSeller()
        {

            UserStatus status = UserStatus.Seller;


            bool result = ValidationIS.statusAdminValidation(status);


            Assert.False(result);
        }
        [Fact]
        public void ReturnsFalse_WhenStatusIsPendingSeller()
        {

            UserStatus status = UserStatus.PendingSeller;


            bool result = ValidationIS.statusAdminValidation(status);


            Assert.False(result);
        }

        [Fact]
        public void ReturnsTrueOnlyForAdmin_OtherwiseFalse()
        {
            foreach (UserStatus status in Enum.GetValues(typeof(UserStatus)))
            {
                bool expected = (status == UserStatus.Admin);
                bool actual = ValidationIS.statusAdminValidation(status);
                Assert.Equal(expected, actual);
            }

        }

    }
}
