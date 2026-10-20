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
    public partial class PageRegister : Page
    {
        public PageRegister()
        {
            InitializeComponent();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            using (ComputerStoreDBEntities db = new ComputerStoreDBEntities())
            {
                if (db.Users.Any(u => u.Email == TxtEmail.Text))
                {
                    TxtError.Text = "Email уже занят";
                    return;
                }

                var newUser = new Users();
                newUser.FullName = TxtName.Text;
                newUser.Email = TxtEmail.Text;
                newUser.Phone = TxtPhone.Text;
                newUser.PasswordHash = TxtPass.Password;
                newUser.RoleID = 3;

                db.Users.Add(newUser);
                db.SaveChanges();

                MessageBox.Show("Успешно! Теперь войдите.");
                ((MainWindow)Application.Current.MainWindow).MainFrame.Navigate(new PageLogin());
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).MainFrame.Navigate(new PageLogin());
        }
    }
}
