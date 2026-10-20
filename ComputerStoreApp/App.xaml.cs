using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ComputerStoreApp
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Entities.ComputerStoreDBEntities Context { get; } = new Entities.ComputerStoreDBEntities();
    
     protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            this.DispatcherUnhandledException += (s, args) =>
            {
                MessageBox.Show("ОШИБКА:\n" + args.Exception.Message + "\n\n" + (args.Exception.InnerException != null ? args.Exception.InnerException.Message : ""), "Упс");
                args.Handled = true;
            };

            new MainWindow().Show();
        }
    }
}