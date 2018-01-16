using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;
using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;

namespace 饥荒百科全书CSharp.Class
{
    public class KeyboardHandler : IDisposable
    {
        //VK值: http://msdn.microsoft.com/en-us/library/windows/desktop/dd375731(v=vs.85).aspx
        
        public const int WmHotkey = 0x0312;
        public const int VkB = 0x42;

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, Global.KeyModifiers fsModifiers, int vk);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private readonly WindowInteropHelper _host;

        public KeyboardHandler(Window mainWindow)
        {
            _host = new WindowInteropHelper(mainWindow);
            ComponentDispatcher.ThreadPreprocessMessage += ComponentDispatcher_ThreadPreprocessMessage;
        }
        
        void ComponentDispatcher_ThreadPreprocessMessage(ref MSG msg, ref bool handled)
        {
            if (msg.message == WmHotkey)
            {
                var mainWindow = (MainWindow)Application.Current.MainWindow;
                // ReSharper disable once PossibleNullReferenceException
                if (mainWindow.NotifyIcon.Visible)
                {
                    mainWindow.MwVisivility = false;
                    mainWindow.NotifyIcon.Visible = false;
                }
                else
                {
                    mainWindow.MwVisivility = true;
                    mainWindow.NotifyIcon.Visible = true;
                }
            }
        }

        public void SetupHotKey(IntPtr handle, Global.KeyModifiers keyModifiers, int vk)
        {
            RegisterHotKey(handle, GetType().GetHashCode(), keyModifiers, vk);
        }

        public void Dispose()
        {
            UnregisterHotKey(_host.Handle, GetType().GetHashCode());
        }
    }
}