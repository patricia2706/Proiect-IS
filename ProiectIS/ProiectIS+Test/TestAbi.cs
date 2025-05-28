using ProiectIS.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectIS_Test
{
    public class TestAbi
    {
        [Fact]
        public void ValidEmail_ReturnsTrue()
        {
            var result = ValidationIS.emailValidation("user@email.com");
            Assert.True(result);
        }

        [Fact]
        public void MissingAtSymbol_ReturnsFalse()
        {
            var result = ValidationIS.emailValidation("useremail.com");
            Assert.False(result);
        }

        [Fact]
        public void WrongDomain_ReturnsFalse()
        {
            var result = ValidationIS.emailValidation("user@other.com");
            Assert.False(result);
        }

        [Fact]
        public void UpperCaseEmail_ReturnsTrue()
        {
            var result = ValidationIS.emailValidation("USER@EMAIL.COM");
            Assert.True(result);
        }

        [Fact]
        public void StartsWithAtSymbol_ReturnsFalse()
        {
            var result = ValidationIS.emailValidation("@email.com");
            Assert.False(result);
        }
    }
}
