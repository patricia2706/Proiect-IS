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
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        AppDbContext _db = new AppDbContext();
        public SignUp()
        {
            InitializeComponent();
        }

        

        private void signUpBtn_Click(object sender, RoutedEventArgs e)
        { 
            if(!(emailTxtBox.Text.ToLower().EndsWith("@email.com") && emailTxtBox.Text.Contains("@") && emailTxtBox.Text.IndexOf("@") > 0))
            {
                MessageBox.Show("Email incorect.", "Atentie", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            bool exists = _db.Users.Any(u => u.Email==emailTxtBox.Text);

            if (exists) {
                MessageBox.Show("Acest email este deja folosit de alt cont.","Atentie",MessageBoxButton.OK,MessageBoxImage.Warning);
                return;
            }

           

            if (!passwordTxtBox.Password.Equals(confPasswordTxtBox.Password))
            {
                MessageBox.Show("Parola incorecta.", "Atentie", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _db.Add(new User { Email = emailTxtBox.Text, Password = passwordTxtBox.Password });
            _db.SaveChanges();


            MainWindow mainWindow = new MainWindow(_db.Users.FirstOrDefault( u => u.Email == emailTxtBox.Text));
            mainWindow.Show();

            this.Close();
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
