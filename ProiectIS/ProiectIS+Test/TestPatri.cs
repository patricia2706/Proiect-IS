using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProiectIS;
using ProiectIS.Helpers;

namespace ProiectIS_Test
{
    public class TestPatri
    {
        [Fact]
        public void ReturnsAllProductsWhenNoSales() //  Dacă nu există vânzări, toate produsele sunt returnate.
        {
            var products = new List<Product> { new Product { Id = 1 }, new Product { Id = 2 } };
            var sales = new List<Sale>(); // no sales

            var result = ValidationIS.listSalesApproved(products, sales);

            Assert.Equal(2, result.Count);
            Assert.Contains(result, p => p.Id == 1);
            Assert.Contains(result, p => p.Id == 2);
        }

        [Fact]
        public void ReturnsOnlyProductsWithoutApprovedSales() // Dacă un produs are o vânzare aprobată, el nu apare în listă.
        {
            var products = new List<Product> { new Product { Id = 1 }, new Product { Id = 2 } };
            var sales = new List<Sale> { new Sale { ProductId = 1, Status = SaleStatus.Approved } };

            var result = ValidationIS.listSalesApproved(products, sales);

            Assert.Single(result);
            Assert.Equal(2, result[0].Id);
        }

        [Fact]
        public void ReturnsEmptyListWhenAllProductsHaveApprovedSales() // Dacă toate produsele au vânzări aprobate, lista returnată este goală.
        {
            var products = new List<Product> { new Product { Id = 1 }, new Product { Id = 2 } };
            var sales = new List<Sale> {
            new Sale { ProductId = 1, Status = SaleStatus.Approved },
            new Sale { ProductId = 2, Status = SaleStatus.Approved }
        };

            var result = ValidationIS.listSalesApproved(products, sales);

            Assert.Empty(result);
        }

        [Fact]
        public void IgnoresSalesWithNonApprovedStatus() // Vânzările cu status diferit de Approved sunt ignorate.
        {
            var products = new List<Product> { new Product { Id = 1 } };
            var sales = new List<Sale> {
            new Sale { ProductId = 1, Status = SaleStatus.Pending },
            new Sale { ProductId = 1, Status = SaleStatus.Canceled }
        };

            var result = ValidationIS.listSalesApproved(products, sales);

            Assert.Single(result);
            Assert.Equal(1, result[0].Id);
        }

        [Fact]
        public void ReturnsCorrectProductsWithMixedSales() // Verifică comportamentul când există vânzări mixte, aprobate și neaprobate.
        {
            var products = new List<Product> { new Product { Id = 1 }, new Product { Id = 2 }, new Product { Id = 3 } };
            var sales = new List<Sale> {
            new Sale { ProductId = 1, Status = SaleStatus.Approved },
            new Sale { ProductId = 2, Status = SaleStatus.Pending }
            };

            var result = ValidationIS.listSalesApproved(products, sales);

            Assert.Equal(2, result.Count);
            Assert.Contains(result, p => p.Id == 2);
            Assert.Contains(result, p => p.Id == 3);
        }
    }
}
