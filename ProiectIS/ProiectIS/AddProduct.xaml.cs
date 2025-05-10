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
    /// Interaction logic for AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Window
    {
        AppDbContext _db = new AppDbContext();
        User user = null;
        public AddProduct()
        {
            InitializeComponent();
        }

        public AddProduct(User u)
        {
            InitializeComponent();
            this.user = u;
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            if (nameTxtBox.Text == "")
            {
                MessageBox.Show("Completati nume!", "Atentie", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (priceTxtBox.Text == "")
            {
                MessageBox.Show("Completati pret!", "Atentie", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!(priceTxtBox.Text.All(char.IsDigit)))
            {
                MessageBox.Show("Pretul trebuie sa contina doar cifre!", "Atentie", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!(minPriceTxtBox.Text.All(char.IsDigit)))
            {
                MessageBox.Show("Pretul minim trebuie sa contina doar cifre!", "Atentie", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (negociableCheckBox.IsChecked == true)
            {
                if (minPriceTxtBox.Text == "")
                {
                    MessageBox.Show("Completati pretul minim!", "Atentie", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            else
            {
                minPriceTxtBox.Text = "0";
            }

                _db.Products.Add(new Product
                {
                    Name = nameTxtBox.Text,
                    Price = float.Parse(priceTxtBox.Text), 
                    SellerId = user.Id,
                    Description = descriptionTxtBox.Text,
                    IsNegotiable = negociableCheckBox.IsChecked ?? false,
                    MinPrice = float.Parse(minPriceTxtBox.Text)
                    
                   
                });
            _db.SaveChanges();
            MessageBox.Show("Produsul a fost adaugat!", "Atentie", MessageBoxButton.OK, MessageBoxImage.Information);

            MainWindow main = new MainWindow(user);
            main.Show();
            this.Close();
            
        }

       

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (negociableCheckBox.IsChecked == true)
            {
                minPriceGrpBox.Visibility = Visibility.Visible;
            }
            else if(negociableCheckBox.IsChecked == false)  
            {
                minPriceGrpBox.Visibility = Visibility.Collapsed;
            }
                   
        }
    }
}
