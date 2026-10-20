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
    public partial class MainWindow : Window
    {
        public static Users CurrentUser = null;

        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new PageLogin());
        }

        public void ShowMain(string title)
        {
            TopPanel.Visibility = Visibility.Visible;
            TxtUser.Text = title;
            MainFrame.Navigate(new PageCatalog());
        }

        public void ShowAuth()
        {
            CurrentUser = null;
            TopPanel.Visibility = Visibility.Collapsed;
            MainFrame.Navigate(new PageLogin());
        }

        private void BtnCatalog_Click(object sender, RoutedEventArgs e) => MainFrame.Navigate(new PageCatalog());
        private void BtnOrders_Click(object sender, RoutedEventArgs e) => MainFrame.Navigate(new PageOrders());
        private void BtnLogout_Click(object sender, RoutedEventArgs e) => ShowAuth();
    }
}