using System;
using System.Collections;
using System.Diagnostics;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using 饥荒百科全书CSharp.View;
using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;

namespace 饥荒百科全书CSharp.Class
{
    ///<summary>    
    /// 直接构造类实例即可注册
    /// 自动完成注销
    /// 注意注册时会抛出异常
    /// 注册系统热键类
    /// 热键会随着程序结束自动解除,不会写入注册表
    ///</summary>
    public class HotKey
    {
        //VK值: http://msdn.microsoft.com/en-us/library/windows/desktop/dd375731(v=vs.85).aspx

        #region Member

        private int KeyId;         //热键编号
        private IntPtr Handle;     //窗体句柄
        private Window Window;     //热键所在窗体

        public delegate void OnHotKeyEventHandler();     //热键事件委托

        private static readonly Hashtable KeyPair = new Hashtable();         //热键哈希表
        private const int WM_HOTKEY = 0x0312;       // 热键消息编号

        #endregion

        ///<summary>
        /// 构造函数
        ///</summary>
        ///<param name="window">注册窗体</param>
        ///<param name="keyModifiers">控制键</param>
        ///<param name="key">主键</param>
        public HotKey(Window window, Global.KeyModifiers keyModifiers, Keys key)
        {
            SetupHotKey(window, keyModifiers, key);
        }

        private static string KeyToString(Keys key)
        {
            //字母 || F1-F12 || 小键盘区的数字 || 空格?
            if ((key >= Keys.A && key <= Keys.Z) || (key >= Keys.F1 && key <= Keys.F12) || (key >= Keys.NumPad0 && key <= Keys.NumPad9) || (key == Keys.Space))
            {
                return key.ToString();
            }
            //字母区上面的数字
            if (key >= Keys.D0 && key <= Keys.D9)
            {
                return key.ToString().Replace("D", "");
            }
            return "";
        }

        private static string ControlKeyToString(double controlKeyValue)
        {
            switch (controlKeyValue)
            {
                case 1:
                    return "Alt + ";
                case 2:
                    return "Ctrl + ";
                case 3:
                    return "Ctrl + Alt + ";
                case 4:
                    return "Shift + ";
                case 5:
                    return "Alt + Shift + ";
                case 6:
                    return "Ctrl + Shift + ";
                case 7:
                    return "Ctrl + Alt + Shift + ";
                default:
                    return "";
            }
        }

        public void SetupHotKey(Window window, Global.KeyModifiers keyModifiers, Keys key)
        {
            
            Handle = new WindowInteropHelper(window).Handle;
            Window = window;
            var controlKey = (uint)keyModifiers;
            var mainKey = (uint)key;
            KeyId = (int)controlKey + (int)mainKey * 10;
            if (KeyPair.ContainsKey(KeyId))
            {
                MessageBox.Show("热键已被注册！");
            }
            //注册热键
            var RegisterHotKeyResult = RegisterHotKey(Handle, KeyId, controlKey, mainKey);
            if (RegisterHotKeyResult == false)
            {
                var controlKeysString = ControlKeyToString(controlKey);
                Debug.WriteLine("mainKey：" + mainKey);
                var keyString = controlKeysString + KeyToString(key);
                MessageBox.Show(keyString + "热键注册失败，可能和其他软件冲突，请重新设置！");
                var mainWindow = (MainWindow) Application.Current.MainWindow;
                // ReSharper disable once PossibleNullReferenceException
                mainWindow.DedicatedServerFrame.Visibility = Visibility.Collapsed;
                mainWindow.RightFrame.Visibility = Visibility.Visible;
                mainWindow.RightFrame.NavigationService.Navigate(Global.PageManager[8]);
                mainWindow.SidebarSetting.IsChecked = true;
            }
            //消息挂钩只能连接一次!!
            if (KeyPair.Count == 0)
            {
                if (false == InstallHotKeyHook(this))
                {
                    throw new Exception("消息挂钩连接失败!");
                }
            }
            //添加这个热键索引
            KeyPair.Add(KeyId, this);
        }

        public void UnSetupHotKey(IntPtr hWnd, int id)
        {
            UnregisterHotKey(hWnd, id);
            KeyPair.Remove(id);
        }

        /// <summary>
        /// 析构函数,解除热键
        /// </summary>
        ~HotKey()
        {
            UnregisterHotKey(Handle, KeyId);
        }

        #region core
        /// <summary>
        /// 注册热键
        /// </summary>
        [System.Runtime.InteropServices.DllImport("user32")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint controlKey, uint virtualKey);
        /// <summary>
        /// 注销热键
        /// </summary>
        [System.Runtime.InteropServices.DllImport("user32")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        /// <summary>
        /// 安装热键处理挂钩
        /// </summary>
        /// <param name="hotKey"></param>
        /// <returns></returns>
        private static bool InstallHotKeyHook(HotKey hotKey)
        {
            if (hotKey.Window == null || hotKey.Handle == IntPtr.Zero)
            {
                return false;
            }
            //获得消息源
            var source = HwndSource.FromHwnd(hotKey.Handle);
            if (source == null)
            {
                return false;
            }
            //挂接事件            
            source.AddHook(HotKeyHook);
            return true;
        }

        /// <summary>
        /// 热键事件
        /// </summary>
        public event OnHotKeyEventHandler OnHotKey;   

        /// <summary>
        /// 热键处理过程
        /// </summary>
        private static IntPtr HotKeyHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == WM_HOTKEY)
            {
                var hotKey = (HotKey)KeyPair[(int)wParam];
                hotKey.OnHotKey?.Invoke();
            }
            return IntPtr.Zero;
        }

        #endregion
    }
}