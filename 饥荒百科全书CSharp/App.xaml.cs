using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace 饥荒百科全书CSharp
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
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
                var resourceDictionaries = new Collection<ResourceDictionary>
                {
                    new ResourceDictionary
                    {
                        Source = new Uri(
                            "pack://application:,,,/饥荒百科全书CSharp;component/Dictionary/CheckBoxDictionary.xaml",
                            UriKind.Absolute)
                    },
                    new ResourceDictionary
                    {
                        Source = new Uri(
                            "pack://application:,,,/饥荒百科全书CSharp;component/Dictionary/ComboBoxDictionary.xaml",
                            UriKind.Absolute)
                    },
                    new ResourceDictionary
                    {
                        Source = new Uri(
                            "pack://application:,,,/饥荒百科全书CSharp;component/Dictionary/CursorDictionary.xaml",
                            UriKind.Absolute)
                    },
                    new ResourceDictionary
                    {
                        Source = new Uri(
                            "pack://application:,,,/饥荒百科全书CSharp;component/Dictionary/ExpanderDictionary.xaml",
                            UriKind.Absolute)
                    },
                    new ResourceDictionary
                    {
                        Source = new Uri(
                            "pack://application:,,,/饥荒百科全书CSharp;component/Dictionary/GridSplitter.xaml",
                            UriKind.Absolute)
                    },
                    new ResourceDictionary
                    {
                        Source = new Uri(
                            "pack://application:,,,/饥荒百科全书CSharp;component/Dictionary/ScrollViewerDictionary.xaml",
                            UriKind.Absolute)
                    },
                    new ResourceDictionary
                    {
                        Source = new Uri(
                            "pack://application:,,,/饥荒百科全书CSharp;component/Dictionary/SliderDictionary.xaml",
                            UriKind.Absolute)
                    },
                    new ResourceDictionary
                    {
                        Source = new Uri(
                            "pack://application:,,,/饥荒百科全书CSharp;component/Dictionary/SwitchButtonDictionary.xaml",
                            UriKind.Absolute)
                    },
                    new ResourceDictionary
                    {
                        Source = new Uri(
                            "pack://application:,,,/饥荒百科全书CSharp;component/Dictionary/WindowDictionary.xaml",
                            UriKind.Absolute)
                    },
                    new ResourceDictionary
                    {
                        Source = new Uri(
                            "pack://application:,,,/饥荒百科全书CSharp;component/Dictionary/DedicatedServer/DediLeftPanelRadioButtonDictionary.xaml",
                            UriKind.Absolute)
                    },
                    new ResourceDictionary
                    {
                        Source = new Uri(
                            "pack://application:,,,/饥荒百科全书CSharp;component/Dictionary/DedicatedServer/DediScrollViewerDictionary.xaml",
                            UriKind.Absolute)
                    },
                    new ResourceDictionary
                    {
                        Source = new Uri(
                            "pack://application:,,,/饥荒百科全书CSharp;component/Dictionary/DedicatedServer/DediRightPanelButtonDictionary.xaml",
                            UriKind.Absolute)
                    },
                    new ResourceDictionary
                    {
                        Source = new Uri(
                            "pack://application:,,,/饥荒百科全书CSharp;component/Dictionary/DedicatedServer/DediRightPanelRadioButtonDictionary.xaml",
                            UriKind.Absolute)
                    },
                    new ResourceDictionary
                    {
                        Source = new Uri(
                            "pack://application:,,,/饥荒百科全书CSharp;component/Dictionary/DedicatedServer/DediRightPanelTextBoxDictionary.xaml",
                            UriKind.Absolute)
                    }
                };

                foreach (var t in resourceDictionaries)
                {
                    Current.Resources.MergedDictionaries.Add(t);
                }

                var splashWindow = new SplashScreen();
                splashWindow.InitializeComponent();
                splashWindow.Show();
            }
        }

        /// <summary>
        /// Entry point class to handle single instance of the application
        /// </summary>
        public static Semaphore SingleInstanceWatcher { get; private set; }

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
                    SingleInstanceWatcher = new Semaphore(
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
                }
                else
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
