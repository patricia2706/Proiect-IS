using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProiectIS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private User user = null;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void logInBtn_Click(object sender, RoutedEventArgs e)
        {
            LogIn window = new LogIn();
            window.Show();

            this.Close();
        }

        private void signUpBtn_Click(object sender, RoutedEventArgs e)
        {
            SignUp window = new SignUp();
            window.Show();
            this.Close();
        }

        public MainWindow(User u)
        {
            InitializeComponent();
            this.user = u;

            switch (u.Status)
            {
                case UserStatus.Admin:
                    logInBtn.Visibility = Visibility.Collapsed;
                    signUpBtn.Visibility = Visibility.Collapsed;
                    dashboardBtn.Visibility = Visibility.Visible;
                    profileBtn.Visibility = Visibility.Visible;
                    break;

                case UserStatus.Buyer:
                    logInBtn.Visibility = Visibility.Collapsed;
                    signUpBtn.Visibility = Visibility.Collapsed;
                    profileBtn.Visibility = Visibility.Visible;
                    break ;

                case UserStatus.Seller:
                    logInBtn.Visibility = Visibility.Collapsed;
                    signUpBtn.Visibility = Visibility.Collapsed;
                    profileBtn.Visibility = Visibility.Visible;
                    sellBtn.Visibility = Visibility.Visible;
                    break;

                case UserStatus.PendingSeller:
                    logInBtn.Visibility = Visibility.Collapsed;
                    signUpBtn.Visibility = Visibility.Collapsed;
                    profileBtn.Visibility = Visibility.Visible;
                    break ;


            }
        }

        private void profileBtn_Click(object sender, RoutedEventArgs e)
        {
            Profile window = new Profile(user);
            window.Show();
            this.Close();
        }

        private void dashboardBtn_Click(object sender, RoutedEventArgs e)
        {
            Dashboard window = new Dashboard();
            window.Show();
            this.Close();
        }

        private void sellBtn_Click(object sender, RoutedEventArgs e)
        {
            AddProduct window = new AddProduct();
            window.Show();
            this.Close();
        }

        private void viewBtn_Click(object sender, RoutedEventArgs e)
        {
            Products window = new Products();
            window.Show();
            this.Close();
        }
    }
}