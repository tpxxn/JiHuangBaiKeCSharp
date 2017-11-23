using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;
using System.Drawing.Text;
using System.Reflection;
using System.Threading;
using System.Windows.Media;
using 饥荒百科全书CSharp.Class;

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
                //游戏版本
                double gameVersion = RegeditRw.RegRead("GameVersion");
                Global.GameVersion = gameVersion;
                // 设置AutoSuggestBox的数据源
                Global.SetAutoSuggestBoxItem();
                //设置全局字体
                var mainWindowFont = RegeditRw.RegReadString("MainWindowFont");
                if (!string.IsNullOrEmpty(mainWindowFont))
                {
                    Global.FontFamily = new FontFamily(mainWindowFont);
                }
                //读取资源字典
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
                        //加载DLL
                        AppDomain.CurrentDomain.AssemblyResolve += (sender, arguments) =>
                        {
                            String projectName = Assembly.GetExecutingAssembly().GetName().Name.ToString();
                            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(projectName + ".Newtonsoft.Json.dll"))
                            {
                                Byte[] bytes = new Byte[stream.Length];
                                stream.Read(bytes, 0, bytes.Length);
                                return Assembly.Load(bytes);
                            }
                        };
                        //启动
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
                        if (MessageBox.Show("警告：您将会删除所有注册表设置，点“确定”立即清除，点“取消”取消清除！", "Σ(っ°Д°;)っ", MessageBoxButton.OKCancel) ==
                            MessageBoxResult.OK)
                        {
                            RegeditRw.ClearReg();
                            MessageBox.Show("清除完毕！", "ヾ(๑╹◡╹)ﾉ");
                        }
                        else
                        {
                            MessageBox.Show("取消清除！", "(～￣▽￣)～");
                        }
                        Environment.Exit(0);
                    }
                }
            }
        }
    }
}
