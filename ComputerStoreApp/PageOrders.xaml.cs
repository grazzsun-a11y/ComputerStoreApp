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
using System.Data.Entity;


namespace ComputerStoreApp
{
    public partial class PageOrders : Page
    {
        public PageOrders()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            using (ComputerStoreDBEntities db = new ComputerStoreDBEntities())
            {
                var query = db.Orders.AsQueryable();

                if (MainWindow.CurrentUser != null && MainWindow.CurrentUser.RoleID == 3)
                {
                    query = query.Where(o => o.UserID == MainWindow.CurrentUser.UserID);
                }
                else if (MainWindow.CurrentUser == null)
                {
                    query = query.Where(o => false);
                }

                GridOrders.ItemsSource = query.ToList();
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            Orders o = GridOrders.SelectedItem as Orders;
            if (o == null) return;

            if (MessageBox.Show("Удалить?", "Вопрос", MessageBoxButton.YesNo) != MessageBoxResult.Yes) return;

            using (ComputerStoreDBEntities db = new ComputerStoreDBEntities())
            {
                var dbOrder = db.Orders.Include(x => x.OrderItems).FirstOrDefault(x => x.OrderID == o.OrderID);
                if (dbOrder != null)
                {
                    db.OrderItems.RemoveRange(dbOrder.OrderItems);
                    db.Orders.Remove(dbOrder);
                    db.SaveChanges();
                }
            }
            LoadData();
        }
    }
}
