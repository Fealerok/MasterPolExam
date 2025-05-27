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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MasterPol.Database;
using MasterPol.Windows;

namespace MasterPol
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private ChaplyginDBEntities3 _db;
        public MainWindow()
        {
            InitializeComponent();

            _db = new ChaplyginDBEntities3();
        }

        private void AuthHandle(object sender, RoutedEventArgs e)
        {
            string login = LoginTB.Text;
            string password = PasswordTB.Password;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введены пустой логин или пароль");
                return;
            }

            List<Partners> foundPartnersList = _db.Partners.Where(p => p.Login == login && p.Password == password).ToList();


            if (foundPartnersList.Count == 0)
            {

                List<Admins> foundAdminsList = _db.Admins.Where(a => a.Login == login && a.Password == password).ToList();

                if (foundAdminsList.Count == 0)
                {
                    MessageBox.Show("Пользователь не найден");
                    return;
                }

                else
                {
                    Admins admin = foundAdminsList[0];
                    AdminWindow adminWindow = new AdminWindow(admin);
                    adminWindow.Show();
                    MessageBox.Show("Вы авторизовались как администратор");
                    this.Close();
                }
            }

            else
            {
                Partners foundPartner = foundPartnersList[0];

                ProductsWindow productsWindow = new ProductsWindow(foundPartner);
                productsWindow.Show();

                MessageBox.Show("Вы авторизовались как партнер");
                this.Close();
            }


        }
    }
}
