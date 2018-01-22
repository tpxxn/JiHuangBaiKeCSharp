using System;
using System.Collections;
using System.Diagnostics;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;

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
        public event OnHotKeyEventHandler OnHotKey;   //热键事件

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

        public void SetupHotKey(Window window, Global.KeyModifiers keyModifiers, Keys key)
        {
            
            Handle = new WindowInteropHelper(window).Handle;
            Window = window;
            var controlKey = (uint)keyModifiers;
            var mainKey = (uint)key;
            KeyId = (int)controlKey + (int)mainKey * 10;
            if (KeyPair.ContainsKey(KeyId))
            {
                throw new Exception("热键已经被注册!");
            }
            //注册热键
            if (RegisterHotKey(Handle, KeyId, controlKey, mainKey) == false)
            {
                throw new Exception("热键注册失败!");
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