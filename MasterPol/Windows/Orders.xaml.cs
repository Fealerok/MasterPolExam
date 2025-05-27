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
    /// Логика взаимодействия для Orders.xaml
    /// </summary>
    public partial class Orders : Window
    {
        private ChaplyginDBEntities3 _db;
        private Partners _partner;
        public Orders(Partners partner)
        {
            InitializeComponent();

            _db = new ChaplyginDBEntities3();
            _partner = partner;

            OrdersDataGrid.ItemsSource = _db.PartnerProductsOrders.Where(o => o.IdPartner == _partner.Id).ToList();
        }

        private void CloseOrders(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
