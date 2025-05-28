using ProiectIS.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectIS_Test
{
    public class TestAnastasia
    {
        [Fact]
        public void PriceGreaterThanMin_ReturnsTrue()
        {

            string input = "10.5";
            float min = 5.0f;

            bool result = ValidationIS.priceValidation(input, min);

            Assert.True(result);
        }

        [Fact]
        public void PriceLessThanMin_ReturnsFalse()
        {
            string input = "3.2";
            float min = 5.0f;

            bool result = ValidationIS.priceValidation(input, min);

            Assert.False(result);
        }

        [Fact]
        public void PriceEqualToMin_ReturnsFalse()
        {
            string input = "5.0";
            float min = 5.0f;

            bool result = ValidationIS.priceValidation(input, min);

            Assert.False(result);
        }

        [Fact]
        public void InvalidString_ThrowsFormatException()
        {
            string input = "abc";
            float min = 5.0f;

            Assert.Throws<FormatException>(() => ValidationIS.priceValidation(input, min));
        }

        [Fact]
        public void NegativePriceComparedToNegativeMin_ReturnsFalse()
        {
            string input = "-1.0";
            float min = -5.0f;

            bool result = ValidationIS.priceValidation(input, min);

            Assert.False(result);
        }
    }
}
