using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for Products.xaml
    /// </summary>
    public partial class Products : Window
    {

        AppDbContext _db = new AppDbContext();
        public ObservableCollection<Product> Prod {  get; set; }

        User user = null;
       
        public Products()
        {
            InitializeComponent();

            using (var _db = new AppDbContext())
            {
                var list = _db.Products.Include(x => x.Seller).ToList(); // încarcă din DB
                var products = new List<Product>();
                foreach (var item in list)
                {
                    var found = _db.Sales.FirstOrDefault(x => x.ProductId == item.Id && x.Status.Equals(SaleStatus.Approved));

                    if (found is null)
                    {
                        products.Add(item);
                    }
                }
                Prod = new ObservableCollection<Product>(products);
                
            }

            prodItemsControl.ItemsSource = Prod;
        }

        public Products(User u)
        {
            InitializeComponent();

            using (var _db = new AppDbContext())
            {
                var list = _db.Products.Include(x => x.Seller).ToList(); // încarcă din DB
                var products = new List<Product>(); 
                foreach (var item in list)
                {
                    var found = _db.Sales.FirstOrDefault(x => x.ProductId == item.Id && x.Status.Equals(SaleStatus.Approved));

                    if (found is null)
                    {
                        products.Add(item);
                    }
                }
                Prod = new ObservableCollection<Product>(products); // inițializează colecția
            }

            prodItemsControl.ItemsSource = Prod;
            this.user = u;
        }

        private void detailsBtn_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var product = button.DataContext as Product;

            if (product != null) {
                if (user == null)
                {
                    var ProductDetails = new ProductDetails(product);
                    ProductDetails.Show();
                    this.Close();
                }
                else
                {
                    var ProductDetails = new ProductDetails(product,user);
                    ProductDetails.Show();
                    this.Close();
                }
            }
            
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            if (user != null)
            {
                MainWindow mainWindow = new MainWindow(user);
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
        }
    }
}
