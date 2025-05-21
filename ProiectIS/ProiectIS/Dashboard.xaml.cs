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
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;

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
        private DashboardType dashboardType;
        User u = null;

        public Dashboard()
        {
            InitializeComponent();

        }

        public Dashboard(DashboardType type, User u)
        {
            InitializeComponent();
            this.u = u;
            dashboardType = type;
            SetupDashboard();

        }

        private void allRadioBtn_Checked(object sender, RoutedEventArgs e)
        {
            elementsDataGrid.ItemsSource = Users;
        }

        private void sellerRadioBtn_Checked(object sender, RoutedEventArgs e)
        {
            elementsDataGrid.ItemsSource = Users.Where(u => u.Status == UserStatus.Seller).ToList();
        }

        private void buyerRadioBtn_Checked(object sender, RoutedEventArgs e)
        {
            elementsDataGrid.ItemsSource = Users.Where(u => u.Status == UserStatus.Buyer).ToList();
        }

        private void penSellerRadioBtn_Checked(object sender, RoutedEventArgs e)
        {
            elementsDataGrid.ItemsSource = Users.Where(u => u.Status == UserStatus.PendingSeller).ToList();
        }

        private void cancelledRadioBtn_Checked(object sender, RoutedEventArgs e)
        {
            elementsDataGrid.ItemsSource = Users.Where(u => u.Status == UserStatus.Canceled).ToList();
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            Options mainWindow = new Options(u);
            mainWindow.Show();
            this.Close();
        }

        private void elementsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dashboardType == DashboardType.Users)
            {
                if (elementsDataGrid.SelectedItem != null)
                {
                    if (elementsDataGrid.SelectedItem is User user)
                    {
                        Details window = new Details(user);
                        window.Show();
                        this.Close();
                    }
                }
            }
            else if (dashboardType == DashboardType.SellerOffers)
            {
                if (elementsDataGrid.SelectedItem != null)
                {
                    if (elementsDataGrid.SelectedItem is Offer offer)
                    {
                        acceptBtn.Visibility = Visibility.Visible;
                        dismissBtn.Visibility = Visibility.Visible;
                    }
                }
            }
            else if (dashboardType == DashboardType.SellerSales)
            {
                if (elementsDataGrid.SelectedItem != null)
                {
                    if (elementsDataGrid.SelectedItem is Sale sale)
                    {
                        acceptBtn.Visibility = Visibility.Visible;
                        dismissBtn.Visibility = Visibility.Visible;
                    }
                }
            }
        }

        private void SetupDashboard()
        {
            if (u.Status == UserStatus.Admin)
            {
                switch (dashboardType)
                {
                    case DashboardType.Users:
                        titleTxt.Text = "Users";
                        var users = _db.Users.ToList();
                        elementsDataGrid.ItemsSource = users;
                        break;

                    case DashboardType.Products:
                        titleTxt.Text = "Products";
                        var products = _db.Products.ToList();
                        elementsDataGrid.ItemsSource = products;
                        HideAdminFilters();
                        break;

                    case DashboardType.Offers:
                        titleTxt.Text = "Offers";
                        var offers = _db.Offers.ToList();
                        elementsDataGrid.ItemsSource = offers;
                        HideAdminFilters();
                        break;

                    case DashboardType.Sales:
                        titleTxt.Text = "Sales";
                        var sales = _db.Sales.ToList();
                        elementsDataGrid.ItemsSource = sales;
                        HideAdminFilters();
                        break;

                }
            }
            else if (u.Status == UserStatus.Seller)

                switch (dashboardType)
                {
                    case DashboardType.SellerSales:
                        titleTxt.Text = "Vanzarile tale";
                        var salesSeller = _db.Sales.Where(s => s.Product.Seller.Id == u.Id).ToList();
                        elementsDataGrid.ItemsSource = salesSeller;
                        HideAdminFilters();
                        break;

                    case DashboardType.SellerOffers:
                        titleTxt.Text = "Ofertele pentru produsele tale";
                        var offersSeller = _db.Offers.Where(o => o.Product.Seller.Id == u.Id).ToList();
                        elementsDataGrid.ItemsSource = offersSeller;
                        HideAdminFilters();
                        break;

                }
        }


        private void HideAdminFilters()
        {
            allRadioBtn.Visibility = Visibility.Collapsed;
            sellerRadioBtn.Visibility = Visibility.Collapsed;
            buyerRadioBtn.Visibility = Visibility.Collapsed;
            penSellerRadioBtn.Visibility = Visibility.Collapsed;
            cancelledRadioBtn.Visibility = Visibility.Collapsed;
        }

        private void acceptBtn_Click(object sender, RoutedEventArgs e)
        {
            if (dashboardType == DashboardType.SellerSales)
            {
                Sale sale = (Sale)elementsDataGrid.SelectedItem;


                sale.Status = SaleStatus.Approved;
                _db.Sales.Update(sale);
                _db.SaveChanges();

                Dashboard dashboard = new Dashboard(dashboardType, u);
                dashboard.Show();
                this.Close();

            }
            else if (dashboardType == DashboardType.SellerOffers)
            {
                var offer = (Offer)elementsDataGrid.SelectedItem;

                var sale = new Sale
                {
                    ProductId = offer.ProductId,
                    BuyerId = offer.BuyerId,
                    FinalPrice = offer.OfferedPrice,
                    SaleDate = DateTime.UtcNow,
                    Status = SaleStatus.Approved,
                };

                var offers = _db.Offers.Where(o => o.ProductId == offer.ProductId).ToList();

                foreach (var item in offers)
                {
                    _db.Offers.Remove(item);
                }

                _db.Sales.Add(sale);
                _db.SaveChanges();
                Dashboard dashboard = new Dashboard(dashboardType, u);
                dashboard.Show();
                this.Close();

            }

        }

        private void dismissBtn_Click(object sender, RoutedEventArgs e)
        {
            if (dashboardType == DashboardType.SellerSales)
            {
                Sale sale = (Sale)elementsDataGrid.SelectedItem;

                sale.Status = SaleStatus.Canceled;
                _db.Sales.Update(sale);
                _db.SaveChanges();

                Dashboard dashboard = new Dashboard(dashboardType, u);
                dashboard.Show();
                this.Close();

            }
            else if (dashboardType == DashboardType.SellerOffers)
            {
                var offer = (Offer)elementsDataGrid.SelectedItem;

                offer.Status = OfferStatus.Rejected;
                _db.Offers.Update(offer);
                _db.SaveChanges();

                Dashboard dashboard = new Dashboard(dashboardType, u);
                dashboard.Show();
                this.Close();

            }
        }
    }

    public enum DashboardType
    {
        Users,
        Sales,
        Products,
        Offers,
        SellerOffers,
        SellerSales
    }
}
