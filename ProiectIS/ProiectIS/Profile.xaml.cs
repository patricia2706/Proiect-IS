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
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class Profile : Window
    {
        AppDbContext _db = new AppDbContext();
        User user = new User();
        public Profile(User u) // asta
        {
            InitializeComponent();
            user = u;
            emailTxt.Text = user.Email;
            statusTxt.Text = user.Status.ToString();

            switch (u.Status)
            {
                case UserStatus.Admin: 
                    disconnectBtn.Visibility = Visibility.Visible;
                    pendingSellerBtn.Visibility = Visibility.Collapsed;
                    break;

                case UserStatus.Seller:
                    disconnectBtn.Visibility = Visibility.Visible;
                    pendingSellerBtn.Visibility = Visibility.Collapsed;
                    break;

                case UserStatus.Buyer:
                    disconnectBtn.Visibility = Visibility.Visible;
                    pendingSellerBtn.Visibility = Visibility.Visible;
                    break;

                case UserStatus.PendingSeller:
                    disconnectBtn.Visibility = Visibility.Visible;
                    pendingSellerBtn.Visibility = Visibility.Collapsed;

                    break;
            }
        }

        private void disconnectBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }

        private void pendingSellerBtn_Click(object sender, RoutedEventArgs e) // asta
        {
            User? user1 = _db.Users.FirstOrDefault(u => u.Id == user.Id);

            if (user1 != null)
            {
                if (user1.Status == UserStatus.Buyer)
                {
                    user1.Status = UserStatus.PendingSeller;
                    _db.Users.Update(user1);
                    _db.SaveChanges();
                    Profile profile = new Profile(user1);
                    this.Close();
                    profile.Show();
                }
            }

            return;
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(user);
            mainWindow.Show();
            this.Close();

        }
    }
}
