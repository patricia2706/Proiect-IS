using ProiectIS.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProiectIS
{
    /// <summary>
    /// Interaction logic for ProductDetails.xaml
    /// </summary>
    public partial class ProductDetails : Window
    {
        AppDbContext dbContext = new AppDbContext();
        Product prod = null;
        User user = null;
        public ProductDetails()
        {
            InitializeComponent();
        }

        public ProductDetails(Product product)
        {
            InitializeComponent();
            this.prod = product;
            nameTxt.Text = product.Name.ToString();
            priceTxt.Text = product.Price.ToString();
            descriptionTxt.Text = product.Description.ToString();

            if(product.IsNegotiable == true)
            {
                negotiableTxt.Text = "Da";
            }
            else
            {
                negotiableTxt.Text = "Nu";
            }

            sellerEmailTxt.Text = product.Seller.Email.ToString();
            deleteBtn.Visibility = Visibility.Collapsed;

            if (product.IsNegotiable == true)
            {
                offerGrpBox.Visibility = Visibility.Visible;
            }
            else
            {
                offerGrpBox.Visibility = Visibility.Collapsed;
            }

        }

        public ProductDetails(Product product, User u)
        {
            InitializeComponent();
            this.prod = product;
            this.user = u;

            nameTxt.Text = product.Name.ToString();
            priceTxt.Text = product.Price.ToString();
            descriptionTxt.Text = product.Description.ToString();

            if (product.IsNegotiable == true)
            {
                negotiableTxt.Text = "Da";
            }
            else
            {
                negotiableTxt.Text = "Nu";
            }

            sellerEmailTxt.Text = product.Seller.Email.ToString();

            switch (user.Status)
            {
                case UserStatus.Admin:
                    deleteBtn.Visibility = Visibility.Visible;  
                    buyBtn.Visibility = Visibility.Collapsed;
                    offerGrpBox.Visibility= Visibility.Collapsed;
                    break;

                case UserStatus.Seller:
                    if (user.Id == product.SellerId)
                    {
                        deleteBtn.Visibility = Visibility.Visible;
                        buyBtn.Visibility = Visibility.Collapsed;
                        offerGrpBox.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        deleteBtn.Visibility = Visibility.Collapsed;
                        buyBtn.Visibility = Visibility.Visible;
                        if (product.IsNegotiable == true)
                        {
                            offerGrpBox.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            offerGrpBox.Visibility = Visibility.Collapsed;
                        }
                    }
                        break;

                case UserStatus.PendingSeller:
                    deleteBtn.Visibility = Visibility.Collapsed;
                    buyBtn.Visibility = Visibility.Visible;
                    if (product.IsNegotiable == true)
                    {
                        offerGrpBox.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        offerGrpBox.Visibility = Visibility.Collapsed;
                    }

                    break;

                case UserStatus.Buyer:
                    deleteBtn.Visibility = Visibility.Collapsed;
                    buyBtn.Visibility = Visibility.Visible;
                    if (product.IsNegotiable == true)
                    {
                        offerGrpBox.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        offerGrpBox.Visibility = Visibility.Collapsed;
                    }
                    break;
                
            }
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            if (user != null)
            {
                Products window = new Products(user);
                window.Show();
                this.Close();
            }
            else
            {
                Products window = new Products();
                window.Show();
                this.Close();
            }
        }

        private void buyBtn_Click(object sender, RoutedEventArgs e)
        {
            if (user != null) {
                if (!prod.IsNegotiable) {

                    Sale sale = new Sale {
                        ProductId = prod.Id,
                        BuyerId = user.Id,
                        FinalPrice = prod.Price,
                        SaleDate = DateTime.UtcNow,
                        Status = SaleStatus.Approved,
                    };

                    dbContext.Sales.Add(sale);
                    dbContext.SaveChanges();

                }

                else
                {
                    if (ValidationIS.priceValidation(offerTxt.Text, prod.MinPrice)) // ce e in if u asta sa faci ceva ce returneaza true sau false 
                    {
                        Offer offer = new Offer
                        {
                            ProductId = prod.Id,
                            BuyerId = user.Id,
                            OfferedPrice = float.Parse(offerTxt.Text),
                            Status = OfferStatus.Pending,
                        };

                        dbContext.Offers.Add(offer);
                        dbContext.SaveChanges();
                        MessageBox.Show("Oferta a fost creata");
                        Products products = new Products(user);
                        products.Show();
                        this.Close();
                       
                    }
                    else
                    {

                        Offer offer = new Offer
                        {
                            ProductId = prod.Id,
                            BuyerId = user.Id,
                            OfferedPrice = float.Parse(offerTxt.Text),
                            Status = OfferStatus.Rejected,
                        };

                        dbContext.Offers.Add(offer);
                        dbContext.SaveChanges();
                        MessageBox.Show("Oferta este refuzata ");
                    }

                }
            }
        }
    }
}
