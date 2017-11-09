using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Windows;

namespace 饥荒百科全书CSharp
{
    internal class AppRun
    {

        /// <summary>
        /// Interaction logic for App.xaml
        /// </summary>
        private class App_Run : Application
        {
            public App_Run()
            {
                Debug.WriteLine("App constructor");
                Startup += App_Startup;
            }

            private static void App_Startup(object sender, StartupEventArgs e)
            {
                Debug.WriteLine("App_Startup");
                var splashWindow = new SplashScreen();
                splashWindow.InitializeComponent();
                splashWindow.Show();
            }
        }

        /// <summary>
        /// Entry point class to handle single instance of the application
        /// </summary>
        private static Semaphore _singleInstanceWatcher;
        private static bool _createdNew;

        public static class EntryPoint
        {
            [STAThread]
            public static void Main(string[] args)
            {
                if (args.Length == 0)
                {
                    //Console.WriteLine("Main");
                    //Console.ReadLine();
                    // 确保不存在程序的其他实例
                    _singleInstanceWatcher = new Semaphore(
                        0, // Initial count.
                        1, // Maximum count.
                        Assembly.GetExecutingAssembly().GetName().Name, out _createdNew);
                    if (_createdNew)
                    {
                        var app = new App_Run();
                        app.Run();
                    }
                    else
                    {
                        MessageBox.Show("请不要重复运行(ノ｀Д)ノ");
                        Environment.Exit(-2);
                    }
                }else
                {
                    if (args[0] == "-clear")
                    {
                        //MessageBox.Show("清除设置");
                        Environment.Exit(0);
                    }
                }
            }
        }

    }
}
