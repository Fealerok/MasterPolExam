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
using MasterPol.Database;

namespace MasterPol.Windows
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private Admins _admin;
        private ChaplyginDBEntities3 _db;
        public AdminWindow(Admins admin)
        {
            InitializeComponent();
            _db = new ChaplyginDBEntities3();
            _admin = admin;

            UpdateTable();
        }

        private void AcceptPartnerOrder(object sender, RoutedEventArgs e)
        {
            PartnerProductsOrders orderPartner = AllOrdersDataGrid.SelectedItem as PartnerProductsOrders;

            _db.PartnerProductsOrders.Remove(orderPartner);
            _db.SaveChanges();
            MessageBox.Show("Заказ партнера подтвержден и удалён из общего списка заказов");

            UpdateTable();
        }

        private void UpdateTable()
        {
            AllOrdersDataGrid.ItemsSource = null;
            AllOrdersDataGrid.ItemsSource = _db.PartnerProductsOrders.ToList();
        }
    }
}
