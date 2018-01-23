using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace 饥荒百科全书CSharp.Class
{
    class SendKey
    {
        private const int KEYEVENTF_KEYDOWN = 0x00;
        private const int KEYEVENTF_KEYUP = 0x02;
        private const int SW_SHOWNORMAL = 1;
        private const int SW_RESTORE = 9;
        private const int SW_SHOWNOACTIVATE = 4;
        private const int VK_L = 0x4C;
        private const int VK_V = 0x56;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;
        private const int WM_CHAR = 0x0102;
        private const int WM_SYSKEYDOWN = 0x0104;
        private const int WM_SYSKEYUP = 0x0105;
        private const int WM_SYSCHAR = 0x0106;
        private const int VK_Enter = 0x0D;
        private const int VK_Shift = 0x10;
        private const int VK_Ctrl = 0x11;
        private const int VK_SPACE = 0x20;
        private const int VK_Console = 0xC0;

        //键盘大小写
        [DllImport("USER32", SetLastError = true)]
        private static extern short GetKeyState(int nVirtKey);

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int X, int Y);
        [DllImport("user32.dll")]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);
        [DllImport("user32.dll")]
        private static extern byte MapVirtualKey(uint uCode, uint uMapType);
        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndlnsertAfter, int X, int Y, int cx, int cy, uint Flags);
        [DllImport("user32.dll")]
        private static extern int PostMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        /// <summary>
        /// 切换输入法(Ctrl+Space)
        /// </summary>
        public static void SetImm()
        {
            keybd_event(VK_Ctrl, MapVirtualKey(VK_Ctrl, 0), KEYEVENTF_KEYDOWN, 0);
            keybd_event(VK_SPACE, MapVirtualKey(VK_SPACE, 0), KEYEVENTF_KEYDOWN, 0);
            System.Threading.Thread.Sleep(20);
            keybd_event(VK_SPACE, MapVirtualKey(VK_SPACE, 0), KEYEVENTF_KEYUP, 0);
            keybd_event(VK_Ctrl, MapVirtualKey(VK_Ctrl, 0), KEYEVENTF_KEYUP, 0);
        }

        /// <summary>
        /// 按下"~"打开控制台
        /// </summary>
        public static void SendConsole()
        {
            keybd_event(VK_Console, MapVirtualKey(VK_Console, 0), KEYEVENTF_KEYDOWN, 0);
            System.Threading.Thread.Sleep(50);
            keybd_event(VK_Console, MapVirtualKey(VK_Console, 0), KEYEVENTF_KEYUP, 0);
        }

        /// <summary>
        /// 输入代码
        /// </summary>
        /// <param name="input">控制台代码</param>
        public static void InputConsoleString(string input)
        {
            foreach (int wParam in Encoding.ASCII.GetBytes(input))
            {
                int actualKeyValue;
                // 小写字母
                if (wParam >= 97 && wParam <= 122)
                {
                    actualKeyValue = wParam - 32;
                    //Debug.WriteLine("byte actualKeyValue: " + (char)actualKeyValue + " " + (byte)actualKeyValue);
                    if (GetKeyState(20) == 1)
                        keybd_event(VK_Shift, MapVirtualKey(VK_Shift, 0), KEYEVENTF_KEYDOWN, 0);
                    keybd_event((byte)actualKeyValue, MapVirtualKey((uint)actualKeyValue, 0), KEYEVENTF_KEYDOWN, 0);
                    System.Threading.Thread.Sleep(20);
                    keybd_event((byte)actualKeyValue, MapVirtualKey((uint)actualKeyValue, 0), KEYEVENTF_KEYUP, 0);
                    if (GetKeyState(20) == 1)
                        keybd_event(VK_Shift, MapVirtualKey(VK_Shift, 0), KEYEVENTF_KEYUP, 0);
                }
                // 大写字母
                else if (wParam >= 65 && wParam <= 90)
                {
                    actualKeyValue = wParam;
                    //Debug.WriteLine("byte actualKeyValue: " + (char)actualKeyValue + " " + (byte)actualKeyValue);
                    if (GetKeyState(20) == 0)
                        keybd_event(VK_Shift, MapVirtualKey(VK_Shift, 0), KEYEVENTF_KEYDOWN, 0);
                    keybd_event((byte)actualKeyValue, MapVirtualKey((uint)actualKeyValue, 0), KEYEVENTF_KEYDOWN, 0);
                    System.Threading.Thread.Sleep(20);
                    keybd_event((byte)actualKeyValue, MapVirtualKey((uint)actualKeyValue, 0), KEYEVENTF_KEYUP, 0);
                    if (GetKeyState(20) == 0)
                        keybd_event(VK_Shift, MapVirtualKey(VK_Shift, 0), KEYEVENTF_KEYUP, 0);
                }
                // 数字
                else if (wParam >= 48 && wParam <= 57)
                {
                    actualKeyValue = wParam;
                    //Debug.WriteLine("byte actualKeyValue: " + (char)actualKeyValue + " " + (byte)actualKeyValue);
                    keybd_event((byte)actualKeyValue, MapVirtualKey((uint)actualKeyValue, 0), KEYEVENTF_KEYDOWN, 0);
                    System.Threading.Thread.Sleep(20);
                    keybd_event((byte)actualKeyValue, MapVirtualKey((uint)actualKeyValue, 0), KEYEVENTF_KEYUP, 0);
                }
                // 引号
                else if (wParam == 34)
                {
                    actualKeyValue = 0xDE;
                    //Debug.WriteLine("byte actualKeyValue: " + (char)actualKeyValue + " " + (byte)actualKeyValue);
                    keybd_event(VK_Shift, MapVirtualKey(VK_Shift, 0), KEYEVENTF_KEYDOWN, 0);
                    keybd_event((byte)actualKeyValue, MapVirtualKey((uint)actualKeyValue, 0), KEYEVENTF_KEYDOWN, 0);
                    System.Threading.Thread.Sleep(20);
                    keybd_event((byte)actualKeyValue, MapVirtualKey((uint)actualKeyValue, 0), KEYEVENTF_KEYUP, 0);
                    keybd_event(VK_Shift, MapVirtualKey(VK_Shift, 0), KEYEVENTF_KEYUP, 0);
                }
                // 左括号
                else if (wParam == 40)
                {
                    actualKeyValue = 0x39;
                    //Debug.WriteLine("byte actualKeyValue: " + (char)actualKeyValue + " " + (byte)actualKeyValue);
                    keybd_event(VK_Shift, MapVirtualKey(VK_Shift, 0), KEYEVENTF_KEYDOWN, 0);
                    keybd_event((byte)actualKeyValue, MapVirtualKey((uint)actualKeyValue, 0), KEYEVENTF_KEYDOWN, 0);
                    System.Threading.Thread.Sleep(20);
                    keybd_event((byte)actualKeyValue, MapVirtualKey((uint)actualKeyValue, 0), KEYEVENTF_KEYUP, 0);
                    keybd_event(VK_Shift, MapVirtualKey(VK_Shift, 0), KEYEVENTF_KEYUP, 0);
                }
                // 右括号
                else if (wParam == 41)
                {
                    actualKeyValue = 0x30;
                    //Debug.WriteLine("byte actualKeyValue: " + (char)actualKeyValue + " " + (byte)actualKeyValue);
                    keybd_event(VK_Shift, MapVirtualKey(VK_Shift, 0), KEYEVENTF_KEYDOWN, 0);
                    keybd_event((byte)actualKeyValue, MapVirtualKey((uint)actualKeyValue, 0), KEYEVENTF_KEYDOWN, 0);
                    System.Threading.Thread.Sleep(20);
                    keybd_event((byte)actualKeyValue, MapVirtualKey((uint)actualKeyValue, 0), KEYEVENTF_KEYUP, 0);
                    keybd_event(VK_Shift, MapVirtualKey(VK_Shift, 0), KEYEVENTF_KEYUP, 0);
                }
                // 逗号
                else if (wParam == 44)
                {
                    actualKeyValue = 0xBC;
                    //Debug.WriteLine("byte actualKeyValue: " + (char)actualKeyValue + " " + (byte)actualKeyValue);
                    keybd_event((byte)actualKeyValue, MapVirtualKey((uint)actualKeyValue, 0), KEYEVENTF_KEYDOWN, 0);
                    System.Threading.Thread.Sleep(20);
                    keybd_event((byte)actualKeyValue, MapVirtualKey((uint)actualKeyValue, 0), KEYEVENTF_KEYUP, 0);
                }
                // 冒号
                else if (wParam == 58)
                {
                    actualKeyValue = 0xBA;
                    //Debug.WriteLine("byte actualKeyValue: " + (char)actualKeyValue + " " + (byte)actualKeyValue);
                    keybd_event(VK_Shift, MapVirtualKey(VK_Shift, 0), KEYEVENTF_KEYDOWN, 0);
                    keybd_event((byte)actualKeyValue, MapVirtualKey((uint)actualKeyValue, 0), KEYEVENTF_KEYDOWN, 0);
                    System.Threading.Thread.Sleep(20);
                    keybd_event((byte)actualKeyValue, MapVirtualKey((uint)actualKeyValue, 0), KEYEVENTF_KEYUP, 0);
                    keybd_event(VK_Shift, MapVirtualKey(VK_Shift, 0), KEYEVENTF_KEYUP, 0);
                }
                // 下划线
                else if (wParam == 95)
                {
                    actualKeyValue = 0xBD;
                    //Debug.WriteLine("byte actualKeyValue: " + (char)actualKeyValue + " " + (byte)actualKeyValue);
                    keybd_event(VK_Shift, MapVirtualKey(VK_Shift, 0), KEYEVENTF_KEYDOWN, 0);
                    keybd_event((byte)actualKeyValue, MapVirtualKey((uint)actualKeyValue, 0), KEYEVENTF_KEYDOWN, 0);
                    System.Threading.Thread.Sleep(20);
                    keybd_event((byte)actualKeyValue, MapVirtualKey((uint)actualKeyValue, 0), KEYEVENTF_KEYUP, 0);
                    keybd_event(VK_Shift, MapVirtualKey(VK_Shift, 0), KEYEVENTF_KEYUP, 0);
                }
                else
                {
                    Debug.WriteLine("未识别键码：" + (char)wParam + wParam);
                }
            }
        }

        /// <summary>
        /// 按下"Enter"执行代码
        /// </summary>
        public static void SendEnter()
        {
            keybd_event(VK_Enter, MapVirtualKey(VK_Enter, 0), KEYEVENTF_KEYDOWN, 0);
            System.Threading.Thread.Sleep(50);
            keybd_event(VK_Enter, MapVirtualKey(VK_Enter, 0), KEYEVENTF_KEYUP, 0);
        }
        /// <summary>
        /// 单机版关闭Log
        /// </summary>
        public static void CloseLog()
        {
            keybd_event(VK_Ctrl, MapVirtualKey(VK_Ctrl, 0), KEYEVENTF_KEYDOWN, 0);
            keybd_event(VK_L, MapVirtualKey(VK_L, 0), KEYEVENTF_KEYDOWN, 0);
            System.Threading.Thread.Sleep(20);
            keybd_event(VK_L, MapVirtualKey(VK_L, 0), KEYEVENTF_KEYUP, 0);
            keybd_event(VK_Ctrl, MapVirtualKey(VK_Ctrl, 0), KEYEVENTF_KEYUP, 0);
        }
        /// <summary>
        /// 给游戏窗口发送控制台代码
        /// </summary>
        /// <param name="message">控制台代码</param>
        public static void SendMessage(string message)
        {
            var processes = FindProcess();
            if (processes == null)
            {
                var copySplashWindow = new CopySplashScreen("未找到\r\n游戏进程");
                copySplashWindow.InitializeComponent();
                copySplashWindow.ContentTextBlock.FontSize = 20;
                copySplashWindow.Show();
            }
            else
            {
                foreach (var process in processes)
                {
                    var title = new StringBuilder(128);
                    GetWindowText(process.MainWindowHandle, title, title.Capacity);
                    var gameVersion = title.ToString() == "Don't Starve Together" ? "DST" : "DS";
                    // 游戏窗口置顶
                    SetForegroundWindow(process.MainWindowHandle);
                    // 切换英文
                    System.Threading.Thread.Sleep(50);
                    SetImm();
                    // 打开控制台
                    System.Threading.Thread.Sleep(50);
                    SendConsole();
                    // 输入代码
                    System.Threading.Thread.Sleep(50);
                    InputConsoleString(message);
                    // 执行代码
                    System.Threading.Thread.Sleep(50);
                    SendEnter();
                    // 关闭Log
                    if (gameVersion == "DS")
                    {
                        System.Threading.Thread.Sleep(50);
                        CloseLog();
                    }
                    // 切换中文
                    System.Threading.Thread.Sleep(50);
                    SetImm();
                }
            }
        }

        /// <summary>
        /// 查找进程
        /// </summary>
        /// <returns></returns>
        private static Process[] FindProcess()
        {
            var processes = Process.GetProcessesByName("dontstarve_steam");
            if (!processes.Any())
            {
                processes = Process.GetProcessesByName("dontstarve_rail");
                if (!processes.Any())
                {
                    return null;
                }
                Debug.WriteLine("WeGame版/QQ游戏大厅版");
                return processes;
            }
            Debug.WriteLine("Steam版");
            return processes;
        }

        public static bool FindWindow(string lpWindowName)
        {
            return FindWindow(null, lpWindowName) != IntPtr.Zero;
        }

        public static void ShowWindows(IntPtr hWnd, int nCmdShow)
        {
            ShowWindow(hWnd, nCmdShow);
        }
    }
}
