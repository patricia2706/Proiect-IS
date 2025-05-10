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
using System.Linq;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.ComponentModel;

namespace ProiectIS
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    /// 
    public partial class Dashboard : Window
    { 
        AppDbContext _db = new AppDbContext();
        public ObservableCollection<User> Users { get; set; }

        public Dashboard()
        {
            InitializeComponent();
            using (var _db = new AppDbContext())
            {
                Users = new ObservableCollection<User>(_db.Users.ToList());
            }

            usersDataGrid.ItemsSource = Users;
        }

        private void allRadioBtn_Checked(object sender, RoutedEventArgs e)
        {
            usersDataGrid.ItemsSource = Users;
        }

        private void sellerRadioBtn_Checked(object sender, RoutedEventArgs e)
        {
            usersDataGrid.ItemsSource = Users.Where( u => u.Status == UserStatus.Seller ).ToList();
        }

        private void buyerRadioBtn_Checked(object sender, RoutedEventArgs e)
        {
            usersDataGrid.ItemsSource = Users.Where(u => u.Status == UserStatus.Buyer).ToList();
        }

        private void penSellerRadioBtn_Checked(object sender, RoutedEventArgs e)
        {
            usersDataGrid.ItemsSource = Users.Where(u => u.Status == UserStatus.PendingSeller).ToList();
        }
        
        private void cancelledRadioBtn_Checked(object sender, RoutedEventArgs e)
        {
            usersDataGrid.ItemsSource = Users.Where(u => u.Status == UserStatus.Canceled).ToList();
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {       
                var users = Users.ToList();
                MainWindow mainWindow = new MainWindow(users.First(u => u.Status == UserStatus.Admin));
                mainWindow.Show();
                this.Close();
        }

        private void usersDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (usersDataGrid.SelectedItem != null) {
                if (usersDataGrid.SelectedItem is User user) {
                    Details window = new Details(user);
                    window.Show();
                    this.Close();
                }
            }
        }

        
    }
}
