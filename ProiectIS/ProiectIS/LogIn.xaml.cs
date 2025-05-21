using Microsoft.EntityFrameworkCore;
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
    /// Interaction logic for LogIn.xaml
    /// </summary>
    public partial class LogIn : Window
    {
        AppDbContext _db = new AppDbContext();
        public LogIn()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var exists = _db.Users.Include(x=>x.Products).Include(x=>x.Sales).Include(x=>x.Offers).FirstOrDefault(u => u.Email == emailTxtBox.Text);

            if (exists == null)
            {
                MessageBox.Show("Nu exista un cont cu acest email.","Atentie",MessageBoxButton.OK,MessageBoxImage.Error); 
                return;
            }

            if(!(exists.Password == passwordTxtBox.Password))
            {
                MessageBox.Show("Parola incorecta.", "Atentie", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if(exists.Status == UserStatus.Canceled)
            {
                MessageBox.Show("Contul a fost sters.", "Atentie", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MainWindow mainWindow = new MainWindow(exists);
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
