using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Controls;

namespace 饥荒百科全书CSharp.Class
{
    class AppRun
    {

        /// <summary>
        /// Interaction logic for App.xaml
        /// </summary>
        public partial class App_Run : Application
        {
            public App_Run()
            {
                Debug.WriteLine("App constructor");
                Startup += new StartupEventHandler(App_Startup);
            }

            void App_Startup(object sender, StartupEventArgs e)
            {
                Debug.WriteLine("App_Startup");
                MainWindow mainWindow = new MainWindow();
                //MainWindow = mainWindow;
                mainWindow.InitializeComponent();
                mainWindow.Show();
            }
        }

        /// <summary>
        /// Entry point class to handle single instance of the application
        /// </summary>
        public static class EntryPoint
        {
            [STAThread]
            public static void Main(string[] args)
            {
                Console.WriteLine("Main");
                Console.ReadLine();
                App_Run app = new App_Run();
                app.Run();
            }
        }
    }
}
