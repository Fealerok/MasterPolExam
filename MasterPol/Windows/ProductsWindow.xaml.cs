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
    /// Логика взаимодействия для ProductsWindow.xaml
    /// </summary>
    public partial class ProductsWindow : Window
    {
        private Partners _partner;
        private ChaplyginDBEntities3 _db;
        public ProductsWindow(Partners partner)
        {
            InitializeComponent();

            _partner = partner; 
            _db = new ChaplyginDBEntities3();

            UpdateTable();
        }

        private void UpdateTable()
        {
            ProductsDataGrid.ItemsSource = null;
            ProductsDataGrid.ItemsSource = _db.Products.ToList();
        }

        private void AcceptOrder(object sender, RoutedEventArgs e)
        {
            Products selectedProduct = ProductsDataGrid.SelectedItem as Products;

            List<PartnerProductsOrders> ordersFromDBList = _db.PartnerProductsOrders.Where(o => o.IdPartner == _partner.Id && o.IdProduct == selectedProduct.Id).ToList();

            if (ordersFromDBList.Count == 0)
            {
                PartnerProductsOrders order = new PartnerProductsOrders();

                order.IdProduct = selectedProduct.Id;
                order.IdPartner = _partner.Id;
                order.Quantity = 1;
                order.Date = DateTime.Now;
                order.Id = _db.PartnerProductsOrders.ToList()[_db.PartnerProductsOrders.ToList().Count - 1].Id + 1;

                _db.PartnerProductsOrders.Add(order);

                _db.SaveChanges();
            }

            else
            {
                PartnerProductsOrders orderFromDB = ordersFromDBList[0];

                orderFromDB.Quantity = orderFromDB.Quantity + 1;

                _db.SaveChanges();
            }
        }

        private void GoToOrders(object sender, RoutedEventArgs e)
        {
            Orders ordersWindow = new Orders(_partner);

            ordersWindow.ShowDialog(); 
        }
    }
}
