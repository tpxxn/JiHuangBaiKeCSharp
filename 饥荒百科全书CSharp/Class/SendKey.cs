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
        public static void InputStr(string input)
        {
            foreach (int wParam in Encoding.ASCII.GetBytes(input))
            {
                int actualKeyValue;
                // 字母
                if (wParam >= 97 && wParam <= 122)
                {
                    actualKeyValue = wParam - 32;
                    //Debug.WriteLine("byte actualKeyValue: " + (char)actualKeyValue + " " + (byte)actualKeyValue);
                    keybd_event((byte)actualKeyValue, MapVirtualKey((uint)actualKeyValue, 0), KEYEVENTF_KEYDOWN, 0);
                    System.Threading.Thread.Sleep(20);
                    keybd_event((byte)actualKeyValue, MapVirtualKey((uint)actualKeyValue, 0), KEYEVENTF_KEYUP, 0);
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
                    actualKeyValue = 222;
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
                    actualKeyValue = 57;
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
                    actualKeyValue = 48;
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
                    actualKeyValue = 188;
                    //Debug.WriteLine("byte actualKeyValue: " + (char)actualKeyValue + " " + (byte)actualKeyValue);
                    keybd_event((byte)actualKeyValue, MapVirtualKey((uint)actualKeyValue, 0), KEYEVENTF_KEYDOWN, 0);
                    System.Threading.Thread.Sleep(20);
                    keybd_event((byte)actualKeyValue, MapVirtualKey((uint)actualKeyValue, 0), KEYEVENTF_KEYUP, 0);
                }
                // 下划线
                else if (wParam == 95)
                {
                    actualKeyValue = 189;
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
                    Debug.WriteLine(title.ToString() == "Don't Starve Together" ? "正在运行饥荒联机版" : "正在运行饥荒单机版");
                    SetForegroundWindow(process.MainWindowHandle);
                    // 切换英文
                    System.Threading.Thread.Sleep(50);
                    SetImm();
                    // 打开控制台
                    System.Threading.Thread.Sleep(50);
                    SendConsole();
                    // 输入代码
                    System.Threading.Thread.Sleep(50);
                    InputStr(message);
                    // 执行代码
                    System.Threading.Thread.Sleep(50);
                    SendEnter();
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

        ////********************************************************************************************
        ////常量定义
        //#region
        //public int Key1, Key2;
        //const uint PROCESS_ALL_ACCESS = 0x001F0FFF;
        //const uint KEYEVENTF_EXTENDEDKEY = 0x1;
        //const uint KEYEVENTF_KEYUP = 0x2;
        //const int SWP_NOSIZE = 0x1; const int SWP_NOMOVE = 0x2; const int SWP_NOZORDER = 0x4; const int SWP_SHOWWINDOW = 0x4;
        //private readonly int MOUSEEVENTF_LEFTDOWN = 0x2;
        //private readonly int MOUSEEVENTF_LEFTUP = 0x4;
        //const uint KBC_KEY_CMD = 0x64;
        //const uint KBC_KEY_DATA = 0x60;
        //private KBDLLHOOKSTRUCT kbdllhs;
        //private WINDOWPLACEMENT lpwndpl;
        //private POINTAPI pt;
        //private Rect rt;
        //private IntPtr iHookHandle = IntPtr.Zero;
        //private GCHandle _hookProcHandle;
        ////********************************************************************************************
        ////钩子事件委托
        //public delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);
        ////********************************************************************************************
        ////********************************************************************************************
        ////api用到的数据类型
        //private const int WH_KEYBOARD = 13;
        //[StructLayout(LayoutKind.Sequential)]
        //public struct KBDLLHOOKSTRUCT { public int vkCode; public int scanCode; public int flags; public int time; public int dwExtraInfo; }
        //[StructLayout(LayoutKind.Sequential)]
        //public struct POINTAPI
        //{
        //    public int x;
        //    public int y;
        //}

        //[StructLayout(LayoutKind.Sequential)]
        //public struct Rect
        //{
        //    public int Left;
        //    public int Right;
        //    public int Top;
        //    public int Button;
        //}

        //[StructLayout(LayoutKind.Sequential)]
        //public struct WINDOWPLACEMENT
        //{
        //    public int Length;
        //    public int flags;
        //    public int showCmd;
        //    public POINTAPI ptMinPosition;
        //    public POINTAPI ptMaxPosition;
        //    public Rect rcNormalPosition;
        //}
        //[StructLayout(LayoutKind.Sequential)]
        //public struct WinFromXY
        //{
        //    public int x;
        //    public int y;
        //    public int Width;
        //    public int Height;

        //}

        //[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        //public struct LUID
        //{

        //    public uint LowPart;

        //    public uint HighPart;

        //};

        //[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        //public struct TOKEN_PRIVILEGES
        //{

        //    public uint PrivilegeCount;

        //    public LUID Luid;

        //    public uint Attributes;

        //};
        ////********************************************************************************************

        ///// <summary>
        ///// 消息值：按下鼠标左键
        ///// </summary>
        //public const int Message_Mouse_LeftButon_Down = 513;

        ///// <summary>
        ///// 消息值：松开鼠标左键
        ///// </summary>
        //public const int Message_Mouse_LeftButon_Up = 514;

        ///// <summary>
        ///// 消息值：获取窗口文本
        ///// </summary>
        //public const int Message_Window_GetText = 13;


        //#endregion
        ////********************************************************************************************


        ////********************************************************************************************
        ////api声明
        //#region
        //[DllImport("user32.dll", EntryPoint = "GetClassName")]
        //public static extern int GetClassName(IntPtr hwnd, StringBuilder lpClassName, int nMaxCount);
        //[DllImport("user32.dll")]
        //public static extern int GetWindowText(int hWnd, StringBuilder lpString, int nMaxCount);
        //[DllImport("user32")]
        //[return: MarshalAs(UnmanagedType.Bool)]
        ////IMPORTANT : LPARAM  must be a pointer (InterPtr) in VS2005, otherwise an exception will be thrown
        //private static extern bool EnumChildWindows(IntPtr window, EnumWindowProc callback, IntPtr i);


        ////the callback function for the EnumChildWindows


        ////********************************************************************************************
        ////用于系统关机等权限操作
        //[DllImport("user32.dll", EntryPoint = "ExitWindowsEx", CharSet = CharSet.Auto)]
        //private static extern int ExitWindowsEx(int uFlags, int dwReserved);
        //[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        //private static extern int GetCurrentProcess();
        //[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        //private static extern int GetLastError();
        //[DllImport("advapi32", CharSet = CharSet.Auto)]
        //private static extern int OpenProcessToken(int ProcessHandle, uint DesiredAccess, ref int TokenHandle);
        //[DllImport("advapi32", CharSet = CharSet.Auto)]
        //private static extern int LookupPrivilegeValue(String lpSystemName, String lpName, ref LUID lpLuid);
        //[DllImport("advapi32", CharSet = CharSet.Auto)]
        //private static extern int AdjustTokenPrivileges(int TokenHandle, bool DisableAllPrivileges, ref TOKEN_PRIVILEGES NewState, int BufferLength, int PreviousState, int ReturnLength);

        ////********************************************************************************************

        //[DllImport("activ.dll", CharSet = CharSet.Auto)]
        //public static extern bool ForceForegroundWindow(int hwnd);
        ////********************************************************************************************
        ////主要用于更改程序标题
        ////这个函数用来置顶显示,参数hwnd为窗口句柄 
        //[DllImport("user32.dll", CharSet = CharSet.Auto)]
        //public static extern bool SetWindowTextA(IntPtr hwn, IntPtr lpString);
        ////********************************************************************************************
        //[DllImport("user32.dll", CharSet = CharSet.Auto)]
        //public static extern void SetForegroundWindow(int hwnd);
        //[DllImport("user32.dll", CharSet = CharSet.Auto)]
        //public static extern bool BringWindowToTop(IntPtr hwnd);

        ////这个函数用来显示窗口,参数hwnd为窗口句柄,nCmdShow是显示类型的枚举 
        //[DllImport("user32.dll")]
        //public static extern bool ShowWindow(int hWnd, nCmdShow nCmdShow);
        //[DllImport("user32.dll")]
        //public static extern bool SetWindowPos(int hwnd, int hWndInsertAfter, int x, int y, int cx, int cy, uint wFlags);
        ////得到窗体句柄的函数,FindWindow函数用来返回符合指定的类名( ClassName )和窗口名( WindowTitle )的窗口句柄
        //[DllImport("user32.dll", CharSet = CharSet.Auto)]
        //public static extern IntPtr FindWindow(
        //string lpClassName, //   pointer   to   class   name   
        //string lpWindowName   //   pointer   to   window   name   
        //);
        ///// <summary>
        ///// 查找子窗口
        ///// </summary>
        ///// <param name="hWnd_Father">父窗口的句柄</param>
        ///// <param name="hWnd_PreChild">上一个兄弟窗口</param>
        ///// <param name="lpszclass">窗口类</param>
        ///// <param name="lpszwindows">窗口标题</param>
        ///// <returns>窗口的句柄（如果查找失败将返回0）</returns>
        //[DllImport("user32.dll", CharSet = CharSet.Auto)]
        //public static extern IntPtr FindWindowEx(IntPtr hWnd_Father, IntPtr hWnd_PreChild, string lpszclass, string lpszwindows);

        //[DllImport("user32.dll", CharSet = CharSet.Auto)]
        //private static extern bool GetWindowPlacement(int hwnd, ref WINDOWPLACEMENT lpwndpl);
        //[DllImport("user32.dll")]
        //private static extern int GetWindowThreadProcessId(int id, int pid);
        //[DllImport("user32.dll")]
        //private static extern bool GetWindowRect(int hwnd, ref Rect lpwndpl);

        //[DllImport("kernel32.dll")]
        //private static extern void CloseHandle
        //(
        // uint hObject //Handle to object
        //);
        ////********************************************************************************************
        ////读取进程内存的函数
        //[DllImport("kernel32.dll")]
        //static extern bool ReadProcessMemory(uint hProcess, int lpBaseAddress,
        //  out int lpBuffer, uint nSize, int lpNumberOfBytesRead);
        //[DllImport("kernel32.dll")]
        //static extern bool ReadProcessMemory(uint hProcess, int lpBaseAddress,
        //  char[] lpBuffer, uint nSize, uint lpNumberOfBytesRead);
        //[DllImport("kernel32.dll")]
        //static extern bool ReadProcessMemory(uint hProcess, int lpBaseAddress,
        //  string lpBuffer, uint nSize, uint lpNumberOfBytesRead);
        //[DllImport("kernel32.dll")]
        //public static extern bool ReadProcessMemory(
        //    uint hProcess,
        //    int lpBaseAddress,
        //    byte[] lpBuffer,
        //    int nSize,
        //    uint lpNumberOfBytesRead
        //);
        ////********************************************************************************************
        //// public static extern bool ReadProcessMemory(int hProcess, int lpBaseAddress, out char lpBuffer, int nSize, int lpNumberOfBytesWritten);
        ////得到目标进程句柄的函数
        //[DllImport("kernel32.dll")]
        //public static extern uint OpenProcess(uint dwDesiredAccess, bool bInheritHandle, int dwProcessId);
        ////鼠标事件声明
        //[DllImport("user32.dll")]
        //static extern bool setcursorpos(int x, int y);
        //[DllImport("user32.dll")]
        //static extern void mouse_event(mouseeventflag flags, int dx, int dy, uint data, UIntPtr extrainfo);
        ////键盘事件声明
        //[DllImport("user32.dll")]
        //static extern byte MapVirtualKey(byte wCode, int wMap);
        //[DllImport("user32.dll")]
        //static extern short GetKeyState(int nVirtKey);
        //[DllImport("user32.dll")]
        //static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);
        ////键盘事件声明winio
        //[DllImport("winio.dll")]
        //public static extern bool InitializeWinIo();
        //[DllImport("winio.dll")]
        //public static extern bool GetPortVal(IntPtr wPortAddr, out int pdwPortVal, byte bSize);
        //[DllImport("winio.dll")]
        //public static extern bool SetPortVal(uint wPortAddr, IntPtr dwPortVal, byte bSize);
        //[DllImport("winio.dll")]
        //public static extern byte MapPhysToLin(byte pbPhysAddr, uint dwPhysSize, IntPtr PhysicalMemoryHandle);
        //[DllImport("winio.dll")]
        //public static extern bool UnmapPhysicalMemory(IntPtr PhysicalMemoryHandle, byte pbLinAddr);
        //[DllImport("winio.dll")]
        //public static extern bool GetPhysLong(IntPtr pbPhysAddr, byte pdwPhysVal);
        //[DllImport("winio.dll")]
        //public static extern bool SetPhysLong(IntPtr pbPhysAddr, byte dwPhysVal);
        //[DllImport("winio.dll")]
        //public static extern void ShutdownWinIo();

        //[DllImport("user32")]
        //private static extern int GetKeyboardState(byte[] pbKeyState);

        ////全局键盘钩子

        ////第一个参数:指定钩子的类型，有WH_MOUSE、WH_KEYBOARD等十多种(具体参见MSDN)
        ////第二个参数:标识钩子函数的入口地址
        ////第三个参数:钩子函数所在模块的句柄；
        ////第四个参数:钩子相关函数的ID用以指定想让钩子去钩哪个线程，为0时则拦截整个系统的消息。
        ////安装在钩子链表中的钩子子程
        //[DllImport("user32.dll", CharSet = CharSet.Auto)]
        //public static extern IntPtr SetWindowsHookEx(int hookid, [MarshalAs(UnmanagedType.FunctionPtr)] HookProc lpfn, IntPtr hinst, int threadid);

        ////移除由SetWindowsHookEx方法安装在钩子链表中的钩子子程
        //[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        //public static extern bool UnhookWindowsHookEx(IntPtr hhook);
        ////对一个事件处理的hook可能有多个，它们成链状，使用CallNextHookEx一级一级地调用。简单解释过来就是“调用下一个HOOK”
        //[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        //public static extern IntPtr CallNextHookEx(IntPtr hhook, int code, IntPtr wparam, IntPtr lparam);
        ////发送系统消息
        //[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        //public static extern bool PostMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        ////发送系统消息
        //[DllImport("user32.dll", CharSet = CharSet.Auto)]
        //public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);
        //[DllImport("user32.dll", CharSet = CharSet.Auto)]
        //public static extern int SendMessage(IntPtr hWnd, int msg, byte[] wParam, int lParam);
        ////取得自身进程的模块地址，句柄
        //[DllImport("kernel32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        //private static extern IntPtr GetModuleHandle(string lpModuleName);
        ////函数功能描述:将一块内存的数据从一个位置复制到另一个位置
        //[DllImport("Kernel32.dll", EntryPoint = "RtlMoveMemory")]
        //public static extern void CopyMemory(ref KBDLLHOOKSTRUCT Source, IntPtr Destination, int Length);
        ////函数功能描述:将一块内存的数据从一个位置复制到另一个位置
        //[DllImport("Kernel32.dll", EntryPoint = "RtlMoveMemory")]
        //public static extern void CopyMemory(KBDLLHOOKSTRUCT Source, IntPtr Destination, int Length);
        ////取得当前线程编号的API
        //[DllImport("kernel32.dll")]
        //static extern int GetCurrentThreadId();


        ////********************************************************************************************
        ////获取屏幕1024*768图像
        //[System.Runtime.InteropServices.DllImport("gdi32.dll")]
        //public static extern int BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, UInt32 dwRop);
        ////创建桌面句柄
        //[System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]
        //public static extern IntPtr CreateDC(string lpszDriver, string lpszDevice, string lpszOutput, int lpInitData);

        //[System.Runtime.InteropServices.DllImport("gdi32.dll")]
        //public static extern IntPtr CreateCompatibleDC(IntPtr hdc);
        ////创建与系统匹配的图像资源
        //[System.Runtime.InteropServices.DllImport("gdi32.dll")]
        //public static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);

        //[System.Runtime.InteropServices.DllImport("gdi32.dll")]
        //public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);
        ////删除用过的资源
        //[System.Runtime.InteropServices.DllImport("gdi32.dll")]
        //public static extern int DeleteDC(IntPtr hdc);
        ////释放用过的句柄等资源
        //[DllImport("user32.dll")]
        //public static extern bool ReleaseDC(
        // IntPtr hwnd, IntPtr hdc
        // );
        ////释放用过的画笔，等图像资源
        //[DllImport("gdi32.dll")]
        //public static extern bool DeleteObject(
        //  IntPtr hdc
        // );
        ////用于像素放大,最后一参数cc0020
        //[DllImport("gdi32.dll")]
        //public static extern bool StretchBlt(IntPtr hdcDest, int xDest, int yDest, int wDest, int hDest, IntPtr hdcSrc, int xSrc, int ySrc, int wSrc, int hSrc, IntPtr rop);
        ////********************************************************************************************
        //#endregion
        ////********************************************************************************************



        ////********************************************************************************************
        ////遍历窗口
        //#region
        //private delegate int EnumWindowProc(IntPtr hWnd, IntPtr parameter, string m_classname);
        //private IntPtr m_hWnd; // HWND if found
        //public IntPtr FoundHandle
        //{
        //    get { return m_hWnd; }
        //}


        //public int FindChildClassHwnd(IntPtr hwndParent, IntPtr lParam, string m_classname)
        //{
        //    EnumWindowProc childProc = new EnumWindowProc(FindChildClassHwnd);
        //    IntPtr hwnd = FindWindowEx(hwndParent, IntPtr.Zero, m_classname, string.Empty);
        //    if (hwnd != IntPtr.Zero)
        //    {
        //        this.m_hWnd = hwnd; // found: save it
        //        return 0; // stop enumerating
        //    }
        //    EnumChildWindows(hwndParent, childProc, IntPtr.Zero); // recurse  redo FindChildClassHwnd
        //    return (int)hwnd;// keep looking
        //}
        //#endregion
        ////********************************************************************************************


        ////********************************************************************************************
        ////图像识别调用方法
        //#region
        //int number = 0;
        ///// <summary>
        ///// 截取屏幕图像
        ///// </summary>
        ///// <param name="Width"></param>
        ///// <param name="Height"></param>
        ///// <returns></returns>
        //public Bitmap fullphoto(int Width, int Height, int x, int y)
        //{
        //    Bitmap bitmap;
        //    //try
        //    //{
        //    IntPtr hScreenDc = CreateDC("DISPLAY", null, null, 0); // 创建桌面句柄
        //    IntPtr hMemDc = CreateCompatibleDC(hScreenDc); // 创建与桌面句柄相关连的内存DC
        //    IntPtr hBitmap = CreateCompatibleBitmap(hScreenDc, Width, Height);
        //    IntPtr hOldBitmap = SelectObject(hMemDc, hBitmap);
        //    BitBlt(hMemDc, 0, 0, Width, Height, hScreenDc, x, y, (UInt32)0xcc0020);
        //    IntPtr map = SelectObject(hMemDc, hOldBitmap);
        //    bitmap = Bitmap.FromHbitmap(map);
        //    ReleaseDC(hBitmap, hScreenDc);
        //    DeleteDC(hScreenDc);//删除用过的对象
        //    DeleteDC(hMemDc);//删除用过的对象
        //    DeleteDC(hOldBitmap);
        //    DeleteObject(hBitmap);


        //    //}
        //    //catch (Exception wx)
        //    //{
        //    //    return null;
        //    //}
        //    // number= number +1;
        //    // bitmap.Save("screen" + number + ".bmp");

        //    return bitmap;
        //}
        ///// <summary>
        ///// 加载图片
        ///// </summary>
        ///// <param name="PathImage"></param>
        ///// <returns></returns>
        //public Bitmap photo(String PathImage)
        //{
        //    Bitmap bp = new Bitmap(PathImage);
        //    return bp;
        //}
        //public Bitmap EnlargePhoto(int Width, int Height, int x, int y, int multiple)
        //{
        //    //Bitmap ph=null;
        //    Bitmap bitmap;

        //    IntPtr hScreenDc = CreateDC("DISPLAY", null, null, 0); // 创建桌面句柄
        //    IntPtr hMemDc = CreateCompatibleDC(hScreenDc); // 创建与桌面句柄相关连的内存DC
        //    IntPtr hBitmap = CreateCompatibleBitmap(hScreenDc, Width, Height);
        //    IntPtr hOldBitmap = SelectObject(hMemDc, hBitmap);
        //    BitBlt(hMemDc, 0, 0, Width, Height, hScreenDc, x, y, (UInt32)0xcc0020);
        //    if (StretchBlt(hMemDc, 0, 0, Width * multiple, Height * multiple, hMemDc, 0, 0, Width, Height, (IntPtr)0xCC0020))
        //    {

        //        // ph.Save("www.bmp");
        //    }
        //    IntPtr map = SelectObject(hMemDc, hOldBitmap);
        //    bitmap = Bitmap.FromHbitmap(map);
        //    ReleaseDC(hBitmap, hScreenDc);
        //    DeleteDC(hScreenDc);//删除用过的对象
        //    DeleteDC(hMemDc);//删除用过的对象
        //    DeleteDC(hOldBitmap);
        //    DeleteObject(hBitmap);
        //    //bitmap.Save("sss.bmp");

        //    return bitmap;
        //}
        ///// <summary>
        ///// 比较指定坐标颜色，是否为期待的
        ///// </summary>
        ///// <param name="PicArray">图片</param>
        ///// <param name="color">颜色</param>
        ///// <param name="x">x坐标</param>
        ///// <param name="y">y坐标</param>
        ///// <returns></returns>
        //public bool XYbit(Bitmap PicArray, int r, int g, int b, int x, int y)
        //{

        //    Color cl = new Color();
        //    try
        //    {
        //        cl = PicArray.GetPixel(x, y);
        //        if ((int)cl.R == r && (int)cl.G == g && (int)cl.B == b)
        //        {
        //            return true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //    return false;
        //}
        //public Color XYbit(Bitmap PicArray, int x, int y)
        //{

        //    Color cl = new Color();
        //    try
        //    {
        //        cl = PicArray.GetPixel(x, y);
        //    }
        //    catch (Exception ss)
        //    { return cl; }
        //    return cl;


        //}





        ////********************************************************************************************
        //// 垃圾算法的图像识别
        ////********************************************************************************************
        ///// <summary>
        ///// 返回相似图片的屏幕坐标
        ///// </summary>
        ///// <param name="PicArray">母图片</param>
        ///// <param name="bp">子图片</param>
        ///// <param name="precise">是否精确匹配</param>
        ///// <param name="level">匹配等级1-5级</param>
        ///// <returns></returns>
        //public int[] XYbitmap(Bitmap PicArray, Bitmap bp, Boolean precise, int level)
        //{
        //    int yes = 0, num = 0, x = 0, y = 0;
        //    int[] XY = new int[2];
        //    int PW = PicArray.Width;
        //    int PH = PicArray.Height;
        //    int H = bp.Height;
        //    int W = bp.Width;
        //    //Color[] cl = new Color[H * W];
        //    //Color[] cl2 = new Color[H * W];
        //    Color cl = new Color();
        //    Color cl2 = new Color();
        //    for (y = 0; y < PH - H; y++)
        //    {
        //        for (x = 0; x < PW - W; x++)
        //        {
        //            cl = PicArray.GetPixel(x, y);
        //            cl2 = bp.GetPixel(0, 0);
        //            if (cl == cl2 && level >= 1)
        //            {
        //                cl = PicArray.GetPixel(x + W - 1, y);
        //                cl2 = bp.GetPixel(W - 1, 0);
        //                if (cl == cl2 && level >= 2)
        //                {
        //                    cl = PicArray.GetPixel(x, y + H - 1);
        //                    cl2 = bp.GetPixel(0, H - 1);
        //                    if (cl == cl2 && level >= 3)
        //                    {
        //                        cl = PicArray.GetPixel(x + W - 1, y + H - 1);
        //                        cl2 = bp.GetPixel(W - 1, H - 1);
        //                        if (cl == cl2 && level >= 4)
        //                        {
        //                            cl = PicArray.GetPixel(x + (W - 1) / 2, y + (H - 1) / 2);
        //                            cl2 = bp.GetPixel((W - 1) / 2, (H - 1) / 2);
        //                            if (cl == cl2 && level >= 5)
        //                            {
        //                                XY[0] = x;
        //                                XY[1] = y;
        //                                var rectangle = new Rectangle(x, y, W, H);
        //                                PicArray = PicArray.Clone(rectangle, PicArray.PixelFormat);//复制小块图

        //                                PicArray.Save(@"image/1/" + x.ToString() + ".bmp");
        //                                return XY;
        //                            }
        //                            XY[0] = x;
        //                            XY[1] = y;
        //                            return XY;
        //                        }
        //                        XY[0] = x;
        //                        XY[1] = y;
        //                        return XY;
        //                    }
        //                    XY[0] = x;
        //                    XY[1] = y;
        //                    return XY;
        //                }


        //            }
        //        }
        //    }
        //    return null;
        //}
        ///// <summary>
        ///// 分割图片
        ///// </summary>
        ///// <param name="bmpobj">图片数据</param>
        ///// <param name="x">x坐标</param>
        ///// <param name="y">y坐标</param>
        ///// <param name="Width">宽</param>
        ///// <param name="Height">高</param>
        ///// <returns></returns>
        //public Bitmap GetSplitPics(Bitmap bmpobj, int Width, int Height, int x, int y)
        //{
        //    Rectangle cloneRect;
        //    cloneRect = new Rectangle(x, y, Width, Height);
        //    try
        //    {
        //        bmpobj = bmpobj.Clone(cloneRect, bmpobj.PixelFormat);//复制小块图
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //    //bmpobj.Save(@"image/1/" + 1+ ".bmp");
        //    //bmpobj.Save("screen.bmp");
        //    return bmpobj;
        //}

        ////********************************************************************************************
        ////********************************************************************************************

        //#endregion
        ////********************************************************************************************

        ////********************************************************************************************
        ////失败的图像识别功能，需要改进
        //#region

        ///// <summary>
        ///// 平均分割图片
        ///// </summary>
        ///// <param name="RowNum">水平上分割数</param>
        ///// <param name="ColNum">垂直上分割数</param>
        ///// <returns>分割好的图片数组</returns>
        //public Bitmap[] GetSplitPics(Bitmap bp, Bitmap bmpobj)
        //{
        //    int RowNum, ColNum;
        //    //Bitmap bp = new Bitmap(PathImage);
        //    RowNum = bp.Width;
        //    ColNum = bp.Height;
        //    if (RowNum == 0 || ColNum == 0)
        //        return null;
        //    int singW = bmpobj.Width / RowNum;
        //    int singH = bmpobj.Height / ColNum;
        //    Bitmap[] PicArray = new Bitmap[singW * singH];
        //    int b = 0;
        //    Rectangle cloneRect;
        //    for (int i = 0; i < singH; i++)      //找有效区
        //    {
        //        for (int j = 0; j < singW; j++)
        //        {
        //            cloneRect = new Rectangle(j * RowNum, i * ColNum, RowNum, ColNum);
        //            PicArray[b] = bmpobj.Clone(cloneRect, bmpobj.PixelFormat);//复制小块图

        //            //PicArray[b].Save(@"image/1/" + b.ToString() + ".bmp");
        //            b++;
        //        }

        //    }
        //    return PicArray;
        //}







        ///// <summary>
        ///// 返回比较后，图片所在屏幕的坐标
        ///// </summary>
        ///// <param name="PicArray"></param>
        ///// <param name="?"></param>
        ///// <returns></returns>
        ////public int[] XYbitmap(Bitmap[] PicArray,Bitmap bp)
        ////{
        ////    int yes = 0,num=0;
        ////    int allP=PicArray.Length;
        ////    int H=bp.Height;
        ////    int W=bp.Width;
        ////    Color[] cl = new Color[H * W];
        ////    Color[] cl2 = new Color[H * W];
        ////    //Color cl = new Color();
        ////    //Color cl2 = new Color();
        ////    for (int a = 0; a < allP;a++ )
        ////    {
        ////        a = 3613;
        ////        //cl=PicArray[a].GetPixel(0, 0);
        ////        for (int i = 0; i < H; i++)
        ////        {
        ////            for (int j = 0; j < W; j++)
        ////            {
        ////                cl[num] = PicArray[a].GetPixel(j, i);
        ////                cl2[num] = bp.GetPixel(j, i);
        ////                num++;
        ////            }

        ////        }

        ////        yes = ColorCompare(cl, cl2);
        ////        num = 0;
        ////        if (yes > 10)
        ////        {
        ////            a = a;
        ////            //return null;
        ////        }

        ////    }

        ////    return null;
        ////}

        ////private int ColorCompare(Color[] cl, Color[] cl2)
        ////{
        ////    int yes = 0;
        ////   int num1= cl.Length;
        ////   int num2= cl2.Length;
        ////   for (int x = 0; x < num1; x++)
        ////   {
        ////       for (int y = 0; y < num2; y++)
        ////       {
        ////           if (cl[x] == cl2[y])
        ////           {
        ////               yes++;

        ////               break;
        ////           }

        ////       }
        ////   }
        ////   double ss = yes * 100 / num1;
        ////   return (int)ss;
        ////}

        ////********************************************************************************************
        //#endregion
        ////********************************************************************************************


        ////********************************************************************************************
        ////对系统操作
        //#region
        //const uint TOKEN_ADJUST_PRIVILEGES = 0x20;
        //const uint TOKEN_QUERY = 0x8;
        //const uint SE_PRIVILEGE_ENABLED = 0x2;
        //public enum EWX : int
        //{
        //    EWX_FORCE = 4,//强迫终止没有响应进程
        //    EWX_LOGOFF = 0,//终止进程，然后注销
        //    EWX_REBOOT = 2,//重新引导系统
        //    EWX_SHUTDOWN = 1//关闭系统
        //}
        //LUID tmpLuid = new LUID();
        //TOKEN_PRIVILEGES tkp = new TOKEN_PRIVILEGES();
        //TOKEN_PRIVILEGES tkpNewButIgnored = new TOKEN_PRIVILEGES();
        //int lBufferNeeded = 0;
        ///// <summary>
        ///// 关闭计算机
        ///// </summary>
        //public void winclose()
        //{
        //    int hdlTokenHandle = 0;
        //    int hdlProcessHandle = GetCurrentProcess();
        //    OpenProcessToken(hdlProcessHandle, (TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY), ref hdlTokenHandle);
        //    LookupPrivilegeValue("", "SeShutdownPrivilege", ref tmpLuid);
        //    tkp.PrivilegeCount = 1;
        //    tkp.Luid = tmpLuid;
        //    tkp.Attributes = SE_PRIVILEGE_ENABLED;
        //    AdjustTokenPrivileges(hdlTokenHandle, false, ref tkp, Marshal.SizeOf(tkp), 0, lBufferNeeded);
        //    if (GetLastError() == 0)
        //    {
        //        ExitWindowsEx((int)EWX.EWX_SHUTDOWN | (int)EWX.EWX_FORCE, 0);
        //    }
        //}
        //#endregion
        ////********************************************************************************************


        ////********************************************************************************************
        ////钩子应用调用方法
        //#region
        ////********************************************************************************************
        //// 钩子应用
        ////********************************************************************************************


        //IntPtr _nextHookPtr; //记录Hook编号
        ///// <summary>
        ///// 执行钩子
        ///// </summary>
        ///// <param name="code"></param>
        ///// <param name="wparam"></param>
        ///// <param name="lparam"></param>
        ///// <returns></returns>

        //public IntPtr MyHookProc(int code, IntPtr wparam, IntPtr lparam)
        //{

        //    CopyMemory(ref kbdllhs, lparam, 20);      //结果就在这里了^_^ 
        //    int iHookCode = kbdllhs.vkCode;

        //    if (code < 0) return CallNextHookEx(_nextHookPtr, code, wparam, lparam);
        //    //System.IO.StreamWriter rs = new System.IO.StreamWriter("key.txt", true);
        //    //rs.WriteLine(iHookCode);
        //    //rs.Flush();
        //    //rs.Close();
        //    if (iHookCode == Key1 && kbdllhs.flags == 0) //如果用户输入的是 b 
        //    {
        //        //winio方式，穿透力较好
        //        //sendwinio();
        //        //MykeyDown(key2);
        //        //System.Threading.Thread.Sleep(10);
        //        //MykeyUp(key2);
        //        Sendkey(Key2, GetState(Key2));//user32方式，一部分游戏不接受

        //        return (IntPtr)1;


        //        // return CallNextHookEx(_nextHookPtr, code, wparam, lparam); //返回，让后面的程序处理该消息

        //    }
        //    else
        //    { return CallNextHookEx(_nextHookPtr, code, wparam, lparam); }

        //    return (IntPtr)0;

        //}



        ///// <summary>
        ///// 安装钩子
        ///// </summary>
        //public void SetHookKey(int kk1, int kk2)
        //{

        //    Key1 = kk1;
        //    Key2 = kk2;
        //    if (_nextHookPtr != IntPtr.Zero) //已经勾过了
        //        return;


        //    HookProc myhookProc = new HookProc(MyHookProc); //声明一个自己的Hook实现函数的委托对象



        //    _nextHookPtr = SetWindowsHookEx((int)HookType.KeyboardLL, myhookProc, GetModuleHandle("sendkey.dll"), 0); //加到Hook链中

        //}


        ///// <summary>
        ///// 卸载钩子
        ///// </summary>
        //public void UnHook()
        //{

        //    if (_nextHookPtr != IntPtr.Zero)
        //    {

        //        UnhookWindowsHookEx(_nextHookPtr); //从Hook链中取消



        //        _nextHookPtr = IntPtr.Zero;

        //    }

        //}




        ////********************************************************************************************
        ////
        ////********************************************************************************************


        //#endregion
        ////********************************************************************************************


        ////********************************************************************************************
        ////窗口操作
        //#region
        ///// <summary>
        ///// 改变窗口大小
        ///// </summary>
        ///// <param name="EC"></param>
        ///// <param name="Height"></param>
        ///// <param name="Width"></param>
        ///// <returns></returns>
        //public bool setwinform(String EC, int Height, int Width)
        //{
        //    int pid = 0;
        //    pid = (int)FindWindow(null, EC);
        //    if (SetWindowPos(pid, -1, 0, 0, Width, Height, SWP_NOMOVE | SWP_SHOWWINDOW))
        //    {
        //        return true;
        //    }
        //    return false;
        //}
        //public bool setwinform(int id, int Height, int Width)
        //{

        //    if (SetWindowPos(id, -1, 0, 0, Width, Height, SWP_NOMOVE | SWP_SHOWWINDOW))
        //    {
        //        return true;
        //    }
        //    return false;
        //}
        ///// <summary>
        ///// 查找窗口句柄
        ///// </summary>
        ///// <returns></returns>
        //public int findwin(string EC)
        //{
        //    int pid = 0, id = 0;
        //    pid = (int)FindWindow(null, EC);
        //    //id = (int)FindWindowEx((IntPtr)pid, IntPtr.Zero, "Edit", "");

        //    return pid;
        //}
        ////********************************************************************************************
        ////获取窗口信息x,y坐标和宽，高
        ////********************************************************************************************
        ///// <summary>
        ///// 获取窗口信息x,y坐标和宽，高
        ///// </summary>
        ///// <param name="pid">进程pid</param>
        ///// <returns></returns>
        //public WinFromXY findform(String EC)
        //{
        //    int pid = 0;
        //    WinFromXY wf = new WinFromXY();
        //    pid = (int)FindWindow(null, EC);
        //    //uint hProcess = OpenProcess(PROCESS_ALL_ACCESS, false, pid);
        //    //if (GetWindowPlacement((int)hProcess,ref lpwndpl))
        //    //{

        //    //    GetWindowRect((int)hProcess,ref rt);
        //    //   // rt = lpwndpl.rcNormalPosition;
        //    //    wf.x = rt.Left;
        //    //    wf.y = rt.Right;
        //    //    CloseHandle(hProcess);
        //    //    return wf;
        //    //}
        //    //CloseHandle(hProcess);

        //    if (GetWindowPlacement((int)pid, ref lpwndpl))
        //    {



        //        rt = lpwndpl.rcNormalPosition;
        //        wf.x = rt.Left;
        //        wf.y = rt.Right;
        //        wf.Height = rt.Button - rt.Right;
        //        wf.Width = rt.Top - rt.Left;
        //        // wf.Height = rt.Top;
        //        // wf.Width = rt.Button;
        //        return wf;
        //    }

        //    return wf;
        //}
        ///// <summary>
        ///// 使指定窗口在屏幕最上方
        ///// </summary>
        ///// <param name="EC"></param>
        //public void showform(String EC)
        //{
        //    int pid = 0;
        //    bool bb = false;
        //    //  uint hProcess = OpenProcess(PROCESS_ALL_ACCESS, false, pid);
        //    //这个函数用来置顶显示,参数hwnd为窗口句柄 nCmdShow.SW_SHOWMINNOACTIVE 
        //    // bb = ForceForegroundWindow((int)pid);
        //    pid = (int)FindWindow(null, EC);
        //    //SetForegroundWindow((int)pid);
        //    ShowWindow((int)pid, nCmdShow.SW_SHOWMINNOACTIVE);

        //    // IntPtr hScreenDc = CreateDC("DISPLAY", null, null, 0);
        //    //if (SetWindowPos((int)pid, -1, 0, 0, 0, 0, SWP_NOSIZE | SWP_NOMOVE | SWP_SHOWWINDOW))
        //    //{

        //    //}


        //    // CloseHandle(hProcess);
        //}
        //public void showform(IntPtr id)
        //{

        //    //  uint hProcess = OpenProcess(PROCESS_ALL_ACCESS, false, pid);
        //    //这个函数用来置顶显示,参数hwnd为窗口句柄 nCmdShow.SW_SHOWMINNOACTIVE 
        //    // bb = ForceForegroundWindow((int)pid);

        //    SetForegroundWindow((int)id);
        //    // ShowWindow((int)id, nCmdShow.SW_SHOWMINNOACTIVE);

        //    // IntPtr hScreenDc = CreateDC("DISPLAY", null, null, 0);
        //    SetWindowPos((int)id, -10, 0, 0, 0, 0, SWP_NOSIZE | SWP_NOMOVE | SWP_SHOWWINDOW);
        //    //{

        //    //}


        //    // CloseHandle(hProcess);
        //}
        ///// <summary>
        ///// 更改指定窗体标题栏
        ///// </summary>
        ///// <param name="EC">窗口名</param>
        ///// <param name="Text">更改名</param>
        ///// <returns></returns>
        //public bool winText(String EC, String Text)
        //{
        //    IntPtr pid = FindWindow(null, EC);
        //    IntPtr t = Marshal.StringToHGlobalAnsi(Text);
        //    SetWindowTextA(pid, t);
        //    return true;
        //}

        //#endregion
        ////********************************************************************************************

        //#region 内存操作
        ///// <summary>
        ///// 获取进程pid
        ///// </summary>
        ///// <param name="name">进程名</param>
        ///// <returns></returns>
        //public int GetPid(String name)
        //{
        //    try
        //    {
        //        var oQuery = new ObjectQuery("select * from Win32_Process where Name='" + name + "'");
        //        var oSearcher = new ManagementObjectSearcher(oQuery);
        //        var oReturnCollection = oSearcher.Get();

        //        var pid = "";
        //        string cmdLine;
        //        var sb = new StringBuilder();
        //        foreach (var oReturn in oReturnCollection)
        //        {
        //            pid = oReturn.GetPropertyValue("ProcessId").ToString();
        //            //cmdLine = (string)oReturn.GetPropertyValue("CommandLine");

        //            //string pattern = "-ap \"(.*)\"";
        //            //Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
        //            // Match match = regex.Match(cmdLine);
        //            //string appPoolName = match.Groups[1].ToString();
        //            //sb.AppendFormat("W3WP.exe PID: {0}   AppPoolId:{1}\r\n", pid, appPoolName);
        //        }
        //        return Convert.ToInt32(pid);
        //    }
        //    catch (Exception ss)
        //    { return 0; }

        //}

        //public int GetPid(int id)
        //{
        //    var pid = 0;
        //    pid = GetWindowThreadProcessId(id, pid);
        //    return pid;
        //}

        ////public String getread(String QEC,String EC, IntPtr dizhi, uint size)
        ////{
        ////    Byte bt = new Byte();
        ////    IntPtr id=FindWindow(QEC, EC);
        ////    uint hProcess = OpenProcess(PROCESS_ALL_ACCESS, false, pid(id));
        ////    IntPtr fanhui = new IntPtr();
        ////    String gg = null;
        ////    if (hProcess == 0)
        ////    {
        ////       // gg = ReadProcessMemory(hProcess, dizhi, fanhui, size, 0);
        ////       // CloseHandle(hProcess);


        ////    }
        ////    return gg;
        ////}

        ///// <summary>
        ///// 读取内存值
        ///// </summary>
        ///// <param name="pid">进程pid</param>
        ///// <param name="EC">""随便写一个</param>
        ///// <param name="dizhi">内存地址</param>
        ///// <param name="size">写4</param>
        ///// <returns></returns>
        //public int getread(int pid, int dizhi)
        //{
        //    byte[] vBuffer = new byte[4];
        //    // IntPtr vBytesAddress = Marshal.UnsafeAddrOfPinnedArrayElement(vBuffer, 0); // 得到缓冲区的地址 
        //    int vBytesAddress = new int();
        //    uint vNumberOfBytesRead = 0;
        //    Byte bt = new Byte();
        //    //IntPtr id = FindWindow(QEC, EC);
        //    uint hProcess = OpenProcess(PROCESS_ALL_ACCESS, false, pid);
        //    //pid(0);
        //    IntPtr fanhui = new IntPtr();
        //    String gg = null;
        //    //if (hProcess == 0)
        //    //{
        //    if (ReadProcessMemory(hProcess, dizhi, out vBytesAddress, (uint)vBuffer.Length, 0))
        //    {
        //        CloseHandle(hProcess);
        //    }
        //    else
        //    {
        //        CloseHandle(hProcess);
        //    }

        //    // }
        //    // int vInt = Marshal.ReadInt32(vBytesAddress);
        //    return vBytesAddress;
        //}

        ///// <summary>
        ///// 读取内存值
        ///// </summary>
        ///// <param name="pid">进程pid</param>
        ///// <param name="dizhi">内存地址</param>
        ///// <param name="size">写255</param>
        ///// <returns></returns>
        //public String getread(int pid, int dizhi, uint size)
        //{


        //    char vBytesAddress = new char();
        //    uint vNumberOfBytesRead = 0;
        //    Byte bt = new Byte();

        //    uint hProcess = OpenProcess(PROCESS_ALL_ACCESS, false, pid);

        //    string[] r;
        //    string temp = "";
        //    byte[] b = new byte[255];
        //    //char[] b = new char[255];
        //    try
        //    {

        //        ReadProcessMemory(hProcess, dizhi, b, 255, 0);
        //        //读出的byte[]要按Unicode编码为字符串


        //        temp = System.Text.Encoding.Unicode.GetString(b);
        //        //截取第一段字符串.Encoding.GetEncoding("gb2312")

        //        System.IO.Stream ss = new System.IO.MemoryStream(b);
        //        //ss.Read(b, 0, 255);
        //        System.IO.StreamReader rs = new System.IO.StreamReader(ss, System.Text.Encoding.GetEncoding("gb2312"));
        //        temp = rs.ReadToEnd();

        //        r = temp.Split('\0');
        //        // System.Text.Encoding.GetEncoding("gb2312");
        //        CloseHandle(hProcess);
        //        return r[0];
        //    }
        //    catch (Exception ex)
        //    {
        //        return "error";
        //    }







        //}





        ////********************************************************************************************
        ////
        ////********************************************************************************************






        //#endregion
        ////********************************************************************************************



        ////********************************************************************************************
        ////键盘和鼠标模拟操作
        //#region

        ///// <summary>
        ///// 发送系统消息模拟键盘鼠标
        ///// </summary>
        ///// <param name="id">句柄</param>
        ///// <param name="wMsG">第2参数标识（1）比如：键盘按下WM_KEYDOWN =0x0100 ,WM_KEYUP =0x0101 ,</param>
        ///// <param name="three">第3参数</param>
        ///// <returns></returns>
        //public bool sendMessageKEY(IntPtr id, int wMsG, int three, int four)
        //{
        //    SendMessage(id, wMsG, three, four);
        //    return true;
        //}
        //public bool sendMessageKEY(IntPtr id, int wMsG, byte[] three, int four)
        //{
        //    SendMessage(id, wMsG, three, four);
        //    return true;
        //}
        ///// <summary>
        ///// 获取键盘状态
        ///// </summary>
        ///// <param name="Key"></param>
        ///// <returns></returns>
        //public bool GetState(int Key)
        //{
        //    return (GetKeyState((int)Key) == 1);
        //}

        ///// <summary>
        ///// 获取键盘状态
        ///// </summary>
        ///// <param name="Key"></param>
        ///// <returns></returns>
        //public bool GetState(VirtualKeys Key)
        //{
        //    return (GetKeyState((int)Key) == 1);
        //}
        //public bool GetState2(int Key)
        //{
        //    return (GetKeyState((int)Key) == 1);
        //}
        ///// <summary>
        ///// 发送键盘事件
        ///// </summary>
        ///// <returns></returns>
        //public void Sendkey(VirtualKeys Key, bool State)
        //{
        //    if (State != GetState(Key))
        //    {
        //        byte a = MapVirtualKey((byte)Key, 0);
        //        keybd_event((byte)Key, MapVirtualKey((byte)Key, 0), 0, 0);
        //        System.Threading.Thread.Sleep(10);
        //        keybd_event((byte)Key, MapVirtualKey((byte)Key, 0), KEYEVENTF_KEYUP, 0);
        //    }

        //}
        ///// <summary>
        ///// 发送键盘事件
        ///// </summary>
        ///// <returns></returns>
        //public void Sendkey(int Key, bool State)
        //{
        //    if (State == GetState2(Key))
        //    {
        //        byte a = MapVirtualKey((byte)Key, 0);
        //        keybd_event((byte)Key, a, 0, 0);
        //        System.Threading.Thread.Sleep(10);
        //        keybd_event((byte)Key, a, KEYEVENTF_KEYUP, 0);
        //    }
        //}
        ///// <summary>
        ///// 初始化winio
        ///// </summary>
        //public void sendwinio()
        //{
        //    if (InitializeWinIo())
        //    {
        //        KBCWait4IBE();
        //    }
        //}
        //private void KBCWait4IBE() //等待键盘缓冲区为空
        //{
        //    //int[] dwVal = new int[] { 0 };
        //    int dwVal = 0;
        //    do
        //    {
        //        //这句表示从&H64端口读取一个字节并把读出的数据放到变量dwVal中
        //        //GetPortVal函数的用法是GetPortVal 端口号,存放读出数据的变量,读入的长度
        //        bool flag = GetPortVal((IntPtr)0x64, out dwVal, 1);
        //    }
        //    while ((dwVal & 0x2) > 0);
        //}
        ///// <summary>
        ///// 模拟键盘标按下
        ///// </summary>
        ///// <param name="vKeyCoad">键盘编码</param>
        //public void MykeyDown(int vKeyCoad)
        //{
        //    int btScancode = 0;

        //    btScancode = MapVirtualKey((byte)vKeyCoad, 0);
        //    // btScancode = vKeyCoad;

        //    KBCWait4IBE(); // '发送数据前应该先等待键盘缓冲区为空
        //    SetPortVal(KBC_KEY_CMD, (IntPtr)0xD2, 1);// '发送键盘写入命令
        //    //SetPortVal函数用于向端口写入数据，它的用法是SetPortVal 端口号,欲写入的数据，写入数据的长度
        //    KBCWait4IBE();
        //    SetPortVal(KBC_KEY_DATA, (IntPtr)0xe2, 1);// '写入按键信息,按下键
        //    KBCWait4IBE(); // '发送数据前应该先等待键盘缓冲区为空
        //    SetPortVal(KBC_KEY_CMD, (IntPtr)0xD2, 1);// '发送键盘写入命令
        //    //SetPortVal函数用于向端口写入数据，它的用法是SetPortVal 端口号,欲写入的数据，写入数据的长度
        //    KBCWait4IBE();
        //    SetPortVal(KBC_KEY_DATA, (IntPtr)btScancode, 1);// '写入按键信息,按下键

        //}
        ///// <summary>
        ///// 模拟键盘弹出
        ///// </summary>
        ///// <param name="vKeyCoad">键盘编码</param>
        //public void MykeyUp(int vKeyCoad)
        //{
        //    int btScancode = 0;
        //    btScancode = MapVirtualKey((byte)vKeyCoad, 0);
        //    //btScancode = vKeyCoad;

        //    KBCWait4IBE(); // '发送数据前应该先等待键盘缓冲区为空
        //    SetPortVal(KBC_KEY_CMD, (IntPtr)0xD2, 1); //'发送键盘写入命令
        //    KBCWait4IBE();
        //    SetPortVal(KBC_KEY_DATA, (IntPtr)0xe0, 1);// '写入按键信息，释放键
        //    KBCWait4IBE(); // '发送数据前应该先等待键盘缓冲区为空
        //    SetPortVal(KBC_KEY_CMD, (IntPtr)0xD2, 1); //'发送键盘写入命令
        //    KBCWait4IBE();
        //    SetPortVal(KBC_KEY_DATA, (IntPtr)btScancode, 1);// '写入按键信息，释放键
        //}
        ///// <summary>
        ///// 模拟鼠标按下
        ///// </summary>
        ///// <param name="vKeyCoad">鼠标编码</param>
        //public void MyMouseDown(int vKeyCoad)
        //{
        //    int btScancode = 0;

        //    btScancode = MapVirtualKey((byte)vKeyCoad, 0);
        //    //btScancode = vKeyCoad;

        //    KBCWait4IBE(); // '发送数据前应该先等待键盘缓冲区为空
        //    SetPortVal(KBC_KEY_CMD, (IntPtr)0xD3, 1);// '发送键盘写入命令
        //    //SetPortVal函数用于向端口写入数据，它的用法是SetPortVal 端口号,欲写入的数据，写入数据的长度
        //    KBCWait4IBE();
        //    SetPortVal(KBC_KEY_DATA, (IntPtr)(btScancode | 0x80), 1);// '写入按键信息,按下键

        //}
        ///// <summary>
        ///// 模拟鼠标弹出
        ///// </summary>
        ///// <param name="vKeyCoad">鼠标编码</param>
        //public void MyMouseUp(int vKeyCoad)
        //{
        //    int btScancode = 0;
        //    btScancode = MapVirtualKey((byte)vKeyCoad, 0);
        //    // btScancode = vKeyCoad;

        //    KBCWait4IBE(); // '发送数据前应该先等待键盘缓冲区为空
        //    SetPortVal(KBC_KEY_CMD, (IntPtr)0xD3, 1); //'发送键盘写入命令
        //    KBCWait4IBE();
        //    SetPortVal(KBC_KEY_DATA, (IntPtr)(btScancode | 0x80), 1);// '写入按键信息，释放键
        //}
        ///// <summary>
        ///// 发送鼠标事件
        ///// </summary>
        ///// <returns></returns>
        //public void SendMouse()
        //{

        //}
        //#endregion
        ////********************************************************************************************



        ////********************************************************************************************
        ////键盘和鼠标 等 枚举
        //#region

        //internal enum HookType //枚举，钩子的类型
        //{

        //    //MsgFilter     = -1,

        //    //JournalRecord    = 0,

        //    //JournalPlayback  = 1,

        //    Keyboard = 2,

        //    GetMessage = 3,

        //    CallWndProc = 4,

        //    //CBT              = 5,

        //    SysMsgFilter = 6,

        //    //Mouse            = 7,

        //    Hardware = 8,

        //    //Debug            = 9,

        //    //Shell           = 10,

        //    //ForegroundIdle  = 11,

        //    CallWndProcRet = 12,

        //    KeyboardLL = 13,

        //    //MouseLL           = 14,

        //};


        //public enum wMsG : int
        //{
        //    WM_NULL = 0x0000,
        //    WM_CREATE = 0x0001,
        //    WM_DESTROY = 0x0002,
        //    WM_MOVE = 0x0003,
        //    WM_SIZE = 0x0005,
        //    WM_ACTIVATE = 0x0006,
        //    WA_INACTIVE = 0,
        //    WA_ACTIVE = 1,
        //    WA_CLICKACTIVE = 2,

        //    WM_SETFOCUS = 0x0007,
        //    WM_KILLFOCUS = 0x0008,
        //    WM_ENABLE = 0x000A,
        //    WM_SETREDRAW = 0x000B,
        //    WM_SETTEXT = 0x000C,
        //    WM_GETTEXT = 0x000D,
        //    WM_GETTEXTLENGTH = 0x000E,
        //    WM_PAINT = 0x000F,
        //    WM_CLOSE = 0x0010,

        //    WM_QUERYENDSESSION = 0x0011,
        //    WM_QUERYOPEN = 0x0013,
        //    WM_ENDSESSION = 0x0016,
        //    WM_QUIT = 0x0012,
        //    WM_ERASEBKGND = 0x0014,
        //    WM_SYSCOLORCHANGE = 0x0015,
        //    WM_SHOWWINDOW = 0x0018,
        //    WM_WININICHANGE = 0x001A,
        //    WM_DEVMODECHANGE = 0x001B,
        //    WM_ACTIVATEAPP = 0x001C,
        //    WM_FONTCHANGE = 0x001D,
        //    WM_TIMECHANGE = 0x001E,
        //    WM_CANCELMODE = 0x001F,
        //    WM_SETCURSOR = 0x0020,
        //    WM_MOUSEACTIVATE = 0x0021,
        //    WM_CHILDACTIVATE = 0x0022,
        //    WM_QUEUESYNC = 0x0023,
        //    WM_GETMINMAXINFO = 0x0024,

        //    WM_KEYFIRST = 0x0100,
        //    WM_KEYDOWN = 0x0100,
        //    WM_KEYUP = 0x0101,
        //    WM_CHAR = 0x0102,
        //    WM_DEADCHAR = 0x0103,
        //    WM_SYSKEYDOWN = 0x0104,
        //    WM_SYSKEYUP = 0x0105,
        //    WM_SYSCHAR = 0x0106,
        //    WM_SYSDEADCHAR = 0x0107,

        //    WM_MOUSEFIRST = 0x0200,
        //    WM_MOUSEMOVE = 0x0200,
        //    // 移动鼠标
        //    WM_LBUTTONDOWN = 0x0201,
        //    //按下鼠标左键
        //    WM_LBUTTONUP = 0x0202,
        //    //释放鼠标左键
        //    WM_LBUTTONDBLCLK = 0x0203,
        //    //双击鼠标左键
        //    WM_RBUTTONDOWN = 0x0204,
        //    //按下鼠标右键
        //    WM_RBUTTONUP = 0x0205,
        //    //释放鼠标右键
        //    WM_RBUTTONDBLCLK = 0x0206,
        //    //双击鼠标右键
        //    WM_MBUTTONDOWN = 0x0207,
        //    //按下鼠标中键 
        //    WM_MBUTTONUP = 0x0208,
        //    //释放鼠标中键
        //    WM_MBUTTONDBLCLK = 0x0209,
        //    //双击鼠标中键
        //    WM_MOUSEWHEEL = 0x020A,
        //}

        ///// <summary>
        ///// 鼠标动作枚举
        ///// </summary>
        //public enum mouseeventflag : uint
        //{
        //    move = 0x0001,
        //    leftdown = 0x0002,
        //    leftup = 0x0004,
        //    rightdown = 0x0008,
        //    rightup = 0x0010,
        //    middledown = 0x0020,
        //    middleup = 0x0040,
        //    xdown = 0x0080,
        //    xup = 0x0100,
        //    wheel = 0x0800,
        //    virtualdesk = 0x4000,
        //    absolute = 0x8000
        //}
        ///// <summary>
        ///// 键盘动作枚举
        ///// </summary>
        //public enum VirtualKeys : byte
        //{
        //    //VK_NUMLOCK = 0x90, //数字锁定键
        //    //VK_SCROLL = 0x91,  //滚动锁定
        //    //VK_CAPITAL = 0x14, //大小写锁定
        //    //VK_A = 62,         //键盘A
        //    VK_LBUTTON = 1,      //鼠标左键 
        //    VK_RBUTTON = 2,　    //鼠标右键 
        //    VK_CANCEL = 3,　　　 //Ctrl+Break(通常不需要处理) 
        //    VK_MBUTTON = 4,　　  //鼠标中键 
        //    VK_BACK = 8, 　　　  //Backspace 
        //    VK_TAB = 9,　　　　  //Tab 
        //    VK_CLEAR = 12,　　　 //Num Lock关闭时的数字键盘5 
        //    VK_RETURN = 13,　　　//Enter(或者另一个) 
        //    VK_SHIFT = 16,　　　 //Shift(或者另一个) 
        //    VK_CONTROL = 17,　　 //Ctrl(或者另一个） 
        //    VK_MENU = 18,　　　　//Alt(或者另一个) 
        //    VK_PAUSE = 19,　　　 //Pause 
        //    VK_CAPITAL = 20,　　 //Caps Lock 
        //    VK_ESCAPE = 27,　　　//Esc 
        //    VK_SPACE = 32,　　　 //Spacebar 
        //    VK_PRIOR = 33,　　　 //Page Up 
        //    VK_NEXT = 34,　　　　//Page Down 
        //    VK_END = 35,　　　　 //End 
        //    VK_HOME = 36,　　　　//Home 
        //    VK_LEFT = 37,　　　  //左箭头 
        //    VK_UP = 38,　　　　  //上箭头 
        //    VK_RIGHT = 39,　　　 //右箭头 
        //    VK_DOWN = 40,　　　  //下箭头 
        //    VK_SELECT = 41,　　  //可选 
        //    VK_PRINT = 42,　　　 //可选 
        //    VK_EXECUTE = 43,　　 //可选 
        //    VK_SNAPSHOT = 44,　　//Print Screen 
        //    VK_INSERT = 45,　　　//Insert 
        //    VK_DELETE = 46,　　  //Delete 
        //    VK_HELP = 47,　　    //可选 
        //    VK_NUM0 = 48,        //0
        //    VK_NUM1 = 49,        //1
        //    VK_NUM2 = 50,        //2
        //    VK_NUM3 = 51,        //3
        //    VK_NUM4 = 52,        //4
        //    VK_NUM5 = 53,        //5
        //    VK_NUM6 = 54,        //6
        //    VK_NUM7 = 55,        //7
        //    VK_NUM8 = 56,        //8
        //    VK_NUM9 = 57,        //9
        //    VK_A = 65,           //A
        //    VK_B = 66,           //B
        //    VK_C = 67,           //C
        //    VK_D = 68,           //D
        //    VK_E = 69,           //E
        //    VK_F = 70,           //F
        //    VK_G = 71,           //G
        //    VK_H = 72,           //H
        //    VK_I = 73,           //I
        //    VK_J = 74,           //J
        //    VK_K = 75,           //K
        //    VK_L = 76,           //L
        //    VK_M = 77,           //M
        //    VK_N = 78,           //N
        //    VK_O = 79,           //O
        //    VK_P = 80,           //P
        //    VK_Q = 81,           //Q
        //    VK_R = 82,           //R
        //    VK_S = 83,           //S
        //    VK_T = 84,           //T
        //    VK_U = 85,           //U
        //    VK_V = 86,           //V
        //    VK_W = 87,           //W
        //    VK_X = 88,           //X
        //    VK_Y = 89,           //Y
        //    VK_Z = 90,           //Z
        //    VK_NUMPAD0 = 96,     //0
        //    VK_NUMPAD1 = 97,     //1
        //    VK_NUMPAD2 = 98,     //2
        //    VK_NUMPAD3 = 99,     //3
        //    VK_NUMPAD4 = 100,    //4
        //    VK_NUMPAD5 = 101,    //5
        //    VK_NUMPAD6 = 102,    //6
        //    VK_NUMPAD7 = 103,    //7
        //    VK_NUMPAD8 = 104,    //8
        //    VK_NUMPAD9 = 105,    //9
        //    VK_NULTIPLY = 106,　 //数字键盘上的* 
        //    VK_ADD = 107,　　　　//数字键盘上的+ 
        //    VK_SEPARATOR = 108,　//可选 
        //    VK_SUBTRACT = 109,　 //数字键盘上的- 
        //    VK_DECIMAL = 110,　　//数字键盘上的. 
        //    VK_DIVIDE = 111,　　 //数字键盘上的/
        //    VK_F1 = 112,
        //    VK_F2 = 113,
        //    VK_F3 = 114,
        //    VK_F4 = 115,
        //    VK_F5 = 116,
        //    VK_F6 = 117,
        //    VK_F7 = 118,
        //    VK_F8 = 119,
        //    VK_F9 = 120,
        //    VK_F10 = 121,
        //    VK_F11 = 122,
        //    VK_F12 = 123,
        //    VK_NUMLOCK = 144,　　//Num Lock 
        //    VK_SCROLL = 145 　   // Scroll Lock 
        //}
        //public enum nCmdShow : uint
        //{
        //    SW_FORCEMINIMIZE = 0x0,
        //    SW_HIDE = 0x1,
        //    SW_MAXIMIZE = 0x2,
        //    SW_MINIMIZE = 0x3,
        //    SW_RESTORE = 0x4,
        //    SW_SHOW = 0x5,
        //    SW_SHOWDEFAULT = 0x6,
        //    SW_SHOWMAXIMIZED = 0x7,
        //    SW_SHOWMINIMIZED = 0x8,
        //    SW_SHOWMINNOACTIVE = 0x9,
        //    SW_SHOWNA = 0xA,
        //    SW_SHOWNOACTIVATE = 0xB,
        //    SW_SHOWNORMAL = 0xC,
        //    WM_CLOSE = 0x10,
        //}
        //#endregion
        ////********************************************************************************************
    }
}
