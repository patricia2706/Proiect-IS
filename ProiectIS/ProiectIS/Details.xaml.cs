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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Details : Window
    {
        AppDbContext _db = new AppDbContext();
        User user = new User();
        public Details(User u)
        {
            InitializeComponent();
            emailTxt.Text = u.Email;
            statusTxt.Text = u.Status.ToString();
            user = u;

            switch (u.Status) {
                case UserStatus.Seller:
                    deleteBtn.Visibility = Visibility.Visible;
                    acceptBtn.Visibility = Visibility.Collapsed;
                    break;

                case UserStatus.Admin:
                    deleteBtn.Visibility = Visibility.Visible;
                    acceptBtn.Visibility = Visibility.Collapsed;
                    break;

                case UserStatus.Buyer:
                    deleteBtn.Visibility = Visibility.Visible;
                    acceptBtn.Visibility = Visibility.Collapsed;
                    break;

                case UserStatus.PendingSeller:
                    deleteBtn.Visibility = Visibility.Visible;
                    acceptBtn.Visibility = Visibility.Visible;
                    break;
            }



        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Close();
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            user.Status = UserStatus.Canceled;
            _db.Users.Update(user);
            _db.SaveChanges();
        }

        private void acceptBtn_Click(object sender, RoutedEventArgs e)
        {
            if(user.Status == UserStatus.PendingSeller)
            {
                user.Status = UserStatus.Seller;
            }
            _db.Users.Update(user);
            _db.SaveChanges();
            Details det = new Details(user);
            this.Close();
            det.Show();
            
        }
    }
}
