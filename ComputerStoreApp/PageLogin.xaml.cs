using ComputerStoreApp.Entities;
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

namespace ComputerStoreApp
{
    public partial class PageLogin : Page
    {
        public PageLogin()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            using (ComputerStoreDBEntities db = new ComputerStoreDBEntities())
            {
                var user = db.Users.FirstOrDefault(u => u.Email == TxtEmail.Text && u.PasswordHash == TxtPass.Password);

                if (user == null)
                {
                    TxtError.Text = "Неверный логин или пароль";
                    return;
                }

                MainWindow.CurrentUser = user;
                ((MainWindow)Application.Current.MainWindow).ShowMain(user.FullName + " (ID: " + user.RoleID + ")");
            }
        }

        private void BtnGuest_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.CurrentUser = null;
            ((MainWindow)Application.Current.MainWindow).ShowMain("Гость");
        }

        private void BtnToReg_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).MainFrame.Navigate(new PageRegister());
        }
    }
}
