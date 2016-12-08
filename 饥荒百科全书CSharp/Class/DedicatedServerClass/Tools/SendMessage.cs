using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
namespace ServerTools
{
    // 发送消息
    class mySendMessage
    {
        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int X, int Y);
        [DllImport("user32.dll")]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);
        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndlnsertAfter, int X, int Y, int cx, int cy, uint Flags);
        [DllImport("user32.dll")]
        private static extern int PostMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        //ShowWindow参数
        private const int SW_SHOWNORMAL = 1;
        private const int SW_RESTORE = 9;
        private const int SW_SHOWNOACTIVATE = 4;
        //SendMessage参数
        private const int WM_KEYDOWN = 0X100;
        private const int WM_KEYUP = 0X101;
        private const int WM_SYSCHAR = 0X106;
        private const int WM_SYSKEYUP = 0X105;
        private const int WM_SYSKEYDOWN = 0X104;
        private const int WM_CHAR = 0X102;
        private const int VK_RETURN = 0X0D;


        /// <summary>
        　/// 发送一个字符串
        　/// </summary>
        　/// <param name="myIntPtr">窗口句柄</param>
        　/// <param name="Input">字符串</param>
        public   void InputStr(IntPtr myIntPtr, string Input)
        {
            byte[] ch = (ASCIIEncoding.ASCII.GetBytes(Input));
            for (int i = 0; i < ch.Length; i++)
            {
                SendMessage(myIntPtr, WM_CHAR, ch[i], 0);
            }
        }

        /// <summary>
        /// 根据窗口的标题得到句柄
        /// </summary>
        /// <param name="windowName"></param>
        /// <returns></returns>
        public IntPtr getIntPtr(string windowName) {
            //  1、根据窗口的标题得到句柄
            IntPtr myIntPtr = FindWindow(null, windowName); //null为类名，可以用Spy++得到，也可以为空
           // ShowWindow(myIntPtr, SW_RESTORE); //将窗口还原
           // SetForegroundWindow(myIntPtr); //如果没有ShowWindow，此方法不能设置最小化的窗口
            return myIntPtr;


        }

        // 发送回车
        public void sendEnter(IntPtr myIntPtr) {
            PostMessage(myIntPtr, WM_SYSKEYDOWN, VK_RETURN, 0); //输入ENTER（0x0d）
            PostMessage(myIntPtr, WM_SYSKEYUP, VK_RETURN, 0);

   

        }


        public void test() {

            //  1、根据窗口的标题得到句柄
            IntPtr myIntPtr = getIntPtr("cmd");


            // 2 利用发送消息API（SendMessage）向窗口发送数据
            InputStr(myIntPtr, "3333"); //输入游戏ID
            InputStr(myIntPtr, "44444"); //输入游戏密码
            SendMessage(myIntPtr, WM_SYSKEYDOWN, 0X0D, 0); //输入ENTER（0x0d）
            SendMessage(myIntPtr, WM_SYSKEYUP, 0X0D, 0);


            Console.Read();

        }

    }
}
