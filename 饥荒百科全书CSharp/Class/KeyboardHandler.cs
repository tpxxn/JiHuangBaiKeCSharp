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

namespace 饥荒百科全书CSharp.Class
{
    public class KeyboardHandler : IDisposable
    {
        //VK值: http://msdn.microsoft.com/en-us/library/windows/desktop/dd375731(v=vs.85).aspx
        public enum KeyModifiers
        {
            None = 0,
            Alt = 1,
            Ctrl = 2,
            Shift = 4,
            WindowsKey = 8
        }
        public const int WM_HOTKEY = 0x0312;
        public const int VK_Ctrl = 0x11;
        public const int VK_B = 0x42;

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, KeyModifiers fsModifiers, int vlc);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private readonly Window _mainWindow;
        private readonly WindowInteropHelper _host;

        public KeyboardHandler(Window mainWindow)
        {
            _mainWindow = mainWindow;
            _host = new WindowInteropHelper(_mainWindow);

            SetupHotKey(_host.Handle);
            ComponentDispatcher.ThreadPreprocessMessage += ComponentDispatcher_ThreadPreprocessMessage;
        }

        void ComponentDispatcher_ThreadPreprocessMessage(ref MSG msg, ref bool handled)
        {
            if (msg.message == WM_HOTKEY)
            {
                var mainWindow = (MainWindow)Application.Current.MainWindow;
                mainWindow.MwVisivility = !mainWindow.MwVisivility;
                mainWindow.NotifyIcon.Visible = !mainWindow.NotifyIcon.Visible;
            }
        }

        private void SetupHotKey(IntPtr handle)
        {
            RegisterHotKey(handle, GetType().GetHashCode(),(KeyModifiers)3, VK_B);
        }

        public void Dispose()
        {
            UnregisterHotKey(_host.Handle, GetType().GetHashCode());
        }
    }
}