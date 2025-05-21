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
    /// Interaction logic for Options.xaml
    /// </summary>
    public partial class Options : Window
    {
        User user = null;
        AppDbContext _db = new AppDbContext();
        public Options()
        {
            InitializeComponent();
        }

        public Options(User u)
        {
            InitializeComponent();
            this.user = u;

            switch (user.Status) { 
                case UserStatus.Admin:
                    sellBtn.Visibility = Visibility.Collapsed;
                    dashboardOffersSellerBtn.Visibility = Visibility.Collapsed;
                    dashboardSalesSellerBtn.Visibility = Visibility.Collapsed;

                    dashboardSalesBtn.Visibility = Visibility.Visible;
                    dashboardUsersBtn.Visibility = Visibility.Visible;
                    dashboardOffersBtn.Visibility = Visibility.Visible;
                    dashboardProductsBtn.Visibility = Visibility.Visible;
                    break;

                case UserStatus.Seller:
                    sellBtn.Visibility = Visibility.Visible;
                    dashboardOffersSellerBtn.Visibility = Visibility.Visible;
                    dashboardSalesSellerBtn.Visibility = Visibility.Visible;

                    dashboardSalesBtn.Visibility = Visibility.Collapsed;
                    dashboardUsersBtn.Visibility = Visibility.Collapsed;
                    dashboardOffersBtn.Visibility = Visibility.Collapsed;
                    dashboardProductsBtn.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(user);
            mainWindow.Show();
            this.Close();

        }

        private void sellBtn_Click(object sender, RoutedEventArgs e)
        {
            AddProduct window = new AddProduct(user);
            window.Show();
            this.Close();
        }

        private void dashboardProductsBtn_Click(object sender, RoutedEventArgs e)
        {
            Dashboard window = new Dashboard(DashboardType.Products,user);
            window.Show();
            this.Close();
        }

        private void dashboardUsersBtn_Click(object sender, RoutedEventArgs e)
        {
            Dashboard window = new Dashboard(DashboardType.Users, user);
            window.Show();
            this.Close();
        }

        private void dashboardSalesBtn_Click(object sender, RoutedEventArgs e)
        {
            Dashboard window = new Dashboard(DashboardType.Sales, user);
            window.Show();
            this.Close();

        }

        private void dashboardOffersBtn_Click(object sender, RoutedEventArgs e)
        {
            Dashboard window = new Dashboard(DashboardType.Offers, user);
            window.Show();
            this.Close();

        }

        private void dashboardOffersSellerBtn_Click(object sender, RoutedEventArgs e)
        {
            Dashboard window = new Dashboard(DashboardType.SellerOffers, user);
            window.Show();
            this.Close();
        }

        private void dashboardSalesSellerBtn_Click(object sender, RoutedEventArgs e)
        {
            Dashboard window = new Dashboard(DashboardType.SellerSales, user);
            window.Show();
            this.Close();
        }
    }
    
}
