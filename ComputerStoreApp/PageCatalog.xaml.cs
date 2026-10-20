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
    public partial class PageCatalog : Page
    {
        public PageCatalog()
        {
            InitializeComponent();
            using (ComputerStoreDBEntities db = new ComputerStoreDBEntities())
            {
                GridProducts.ItemsSource = db.Products.ToList();
            }
        }

        private void BtnBuy_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.CurrentUser == null)
            {
                MessageBox.Show("Войдите, чтобы купить!");
                return;
            }

            Products p = GridProducts.SelectedItem as Products;
            if (p == null) return;

            int qty = 1;
            if (!int.TryParse(TxtQty.Text, out qty) || qty <= 0)
            {
                MessageBox.Show("Неверное кол-во");
                return;
            }

            using (ComputerStoreDBEntities db = new ComputerStoreDBEntities())
            {
                Orders o = new Orders();
                o.UserID = MainWindow.CurrentUser.UserID;
                o.OrderDate = System.DateTime.Now;
                o.Status = "Новый";

                OrderItems item = new OrderItems();
                item.ProductID = p.ProductID;
                item.Quantity = qty;
                item.UnitPrice = p.Price;

                o.OrderItems.Add(item);
                db.Orders.Add(o);
                db.SaveChanges();
            }

            MessageBox.Show("Куплено!");
        }
    }
}
