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
        public ObservableCollection<Product> Prod { get; set; }
        public Products()
        {
            InitializeComponent();
            using (var _db = new AppDbContext())
            {
                Prod = new ObservableCollection<Product>(_db.Products.ToList());
            }

            prodItemsControl.ItemsSource = Prod;

            
        }
    }
}
