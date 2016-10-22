using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Controls;
using System.Threading;
using System.Reflection;

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
                SplashScreen splashWindow = new SplashScreen();
                //MainWindow = mainWindow;
                splashWindow.InitializeComponent();
                splashWindow.Show();
            }
        }

        /// <summary>
        /// Entry point class to handle single instance of the application
        /// </summary>
        private static Semaphore singleInstanceWatcher;
        private static bool createdNew;

        public static class EntryPoint
        {
            [STAThread]
            public static void Main(string[] args)
            {
                Console.WriteLine("Main");
                Console.ReadLine();
                // 确保不存在程序的其他实例
                singleInstanceWatcher = new Semaphore(
                    0, // Initial count.
                    1, // Maximum count.
                    Assembly.GetExecutingAssembly().GetName().Name, out createdNew);
                if (createdNew)
                {
                    App_Run app = new App_Run();
                    app.Run();
                }
                else
                {
                    MessageBox.Show("程序已在运行中");
                    Environment.Exit(-2);
                }
            }
        }
       
    }
}
