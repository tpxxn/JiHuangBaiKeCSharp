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
using System.Windows.Controls;
using System.Windows.Media;
using 饥荒百科全书CSharp.Class;

namespace 饥荒百科全书CSharp
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public class App : Application
    {
        /// <summary>
        /// Interaction logic for App.xaml
        /// </summary>
        private class AppRun : Application
        {
            public AppRun()
            {
                Application.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
                AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
                Startup += App_Startup;
            }

            private static void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
            {
                MessageBox.Show("错误信息：\r\n" + e.Exception, "意外的操作", MessageBoxButton.OK, MessageBoxImage.Information);//这里通常需要给用户一些较为友好的提示，并且后续可能的操作
                e.Handled = true;//使用这一行代码告诉运行时，该异常被处理了，不再作为UnhandledException抛出了。
            }

            private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
            {
                MessageBox.Show("错误信息：\r\n" + e.ExceptionObject, "意外的操作", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            private static void App_Startup(object sender, StartupEventArgs e)
            {
                ////游戏版本
                //double gameVersion = RegeditRw.RegRead("GameVersion");
                //Global.GameVersion = gameVersion;
                //// 设置AutoSuggestBox的数据源
                //Global.SetAutoSuggestBoxItem();

                // 测试模式
                if (_testMode)
                    Global.TestMode = true;

                #region 设置全局字体
                var mainWindowFont = RegeditRw.RegReadString("MainWindowFont");
                Global.FontFamily = !string.IsNullOrEmpty(mainWindowFont) ? new FontFamily(mainWindowFont) : new FontFamily("微软雅黑");
                #endregion

                #region 淡紫色透明光标
                var mainWindowLavenderCursor = RegeditRw.RegReadString("MainWindowLavenderCursor");
                if (string.IsNullOrEmpty(mainWindowLavenderCursor)) { 
                    mainWindowLavenderCursor = "True";
                    RegeditRw.RegWrite("MainWindowLavenderCursor", "True");
                }
                ResourceDictionary CursorDictionary;
                if (mainWindowLavenderCursor == "True")
                {
                    CursorDictionary = new ResourceDictionary
                    {
                        Source = new Uri(
                            "pack://application:,,,/饥荒百科全书CSharp;component/Dictionary/CursorDictionary.xaml",
                            UriKind.Absolute)
                    };
                }
                else
                {
                    CursorDictionary = new ResourceDictionary
                    {
                        Source = new Uri(
                            "pack://application:,,,/饥荒百科全书CSharp;component/Dictionary/DefaultCursorDictionary.xaml",
                            UriKind.Absolute)
                    };
                    Current.Resources.MergedDictionaries.Add(CursorDictionary);
                }
                foreach (var key in CursorDictionary.Keys)
                {
                    var cursor = ((TextBlock)CursorDictionary[key]).Cursor;
                    CursorDictionary.Remove(key);
                    CursorDictionary.Add(key, cursor);
                }
                Current.Resources.MergedDictionaries.Add(CursorDictionary);
                #endregion

                #region 读取资源字典
                var resourceDictionaries = new Collection<ResourceDictionary>
                {
                    new ResourceDictionary
                    {
                        Source = new Uri(
                            "pack://application:,,,/饥荒百科全书CSharp;component/MyUserControl/LoadingIndicators/Colors.xaml",
                            UriKind.Absolute)
                    },
                    new ResourceDictionary
                    {
                        Source = new Uri(
                            "pack://application:,,,/饥荒百科全书CSharp;component/MyUserControl/LoadingIndicators/Styles.xaml",
                            UriKind.Absolute)
                    },
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
                            "pack://application:,,,/饥荒百科全书CSharp;component/Dictionary/ContentMenuDictionary.xaml",
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
                            "pack://application:,,,/饥荒百科全书CSharp;component/Dictionary/ListBoxDictionary.xaml",
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
                            "pack://application:,,,/饥荒百科全书CSharp;component/Dictionary/DedicatedServer/DediComboBoxDictionary.xaml",
                            UriKind.Absolute)
                    },
                    new ResourceDictionary
                    {
                        Source = new Uri(
                            "pack://application:,,,/饥荒百科全书CSharp;component/Dictionary/DedicatedServer/DediComboBoxWithImageDictionary.xaml",
                            UriKind.Absolute)
                    },
                    new ResourceDictionary
                    {
                        Source = new Uri(
                            "pack://application:,,,/饥荒百科全书CSharp;component/Dictionary/DedicatedServer/DediImageButtonDictionary.xaml",
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
                            "pack://application:,,,/饥荒百科全书CSharp;component/Dictionary/DedicatedServer/DediModBoxDictionary.xaml",
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
                            "pack://application:,,,/饥荒百科全书CSharp;component/Dictionary/DedicatedServer/DediSelectBoxDictionary.xaml",
                            UriKind.Absolute)
                    }
                };
                foreach (var resourceDictionary in resourceDictionaries)
                {
                    Current.Resources.MergedDictionaries.Add(resourceDictionary);
                }
                #endregion

                var splashWindow = new SplashScreen();
                splashWindow.InitializeComponent();
                splashWindow.Show();
                //var mainWindowShow = new MainWindow();
                //mainWindowShow.InitializeComponent();
                //mainWindowShow.Show();
                //mainWindowShow.Activate();
            }
        }

        /// <summary>
        /// Entry point class to handle single instance of the application
        /// </summary>
        public static Semaphore SingleInstanceWatcher { get; private set; }

        private static bool _createdNew;
        private static bool _testMode;

        public static class EntryPoint
        {
            [STAThread]
            public static void Main(string[] args)
            {
                if (args.Length == 0 || args[0].ToLower() == "-testmode")
                {
                    // 工程名("饥荒百科全书CSharp")
                    var projectName = Assembly.GetExecutingAssembly().GetName().Name;
                    // 单实例监视
                    SingleInstanceWatcher = new Semaphore(0, 1, projectName, out _createdNew);
                    if (_createdNew)
                    {
                        //加载DLL
                        AppDomain.CurrentDomain.AssemblyResolve += Global.CurrentDomain_AssemblyResolve;
                        //启动
                        if (args.Length != 0 && args[0].ToLower() == "-testmode")
                        {
                            MessageBox.Show("您正在以测试模式运行饥荒百科全书，测试模式可能会导致部分BUG，请酌情使用!!!∑(ﾟДﾟノ)ノ");
                            _testMode = true;
                        }
                        var app = new AppRun();
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
                    if (args[0].ToLower() == "-clear")
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
