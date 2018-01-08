using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using 饥荒百科全书CSharp.Class;
using Control = System.Windows.Forms.Control;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;

namespace 饥荒百科全书CSharp.View.SettingChildPage
{
    /// <summary>
    /// SettingChildPage.xaml 的交互逻辑
    /// </summary>
    public partial class SettingChildPage : Page
    {
        public void LoadCompleted(object sender, NavigationEventArgs e)
        {
            if (Global.FontFamily != null)
            {
                FontFamily = Global.FontFamily;
            }
            RootScrollViewer.FontWeight = Global.FontWeight;
        }

        public SettingChildPage()
        {
            InitializeComponent();
            Global.SettingRootFrame.NavigationService.LoadCompleted += LoadCompleted;
        }

        //老板键
        private void Se_BossKey_Key_KeyDown(object sender, KeyEventArgs e)
        {
            byte pressAlt; //Alt
            byte pressCtrl; //Ctrl
            byte pressShift; //Shift
            string preString; //前面的值
            var mainKey = ""; //主值

            //字母 || F1-F12 || 小键盘区的数字 || 空格?
            if ((e.Key >= Key.A && e.Key <= Key.Z) || (e.Key >= Key.F1 && e.Key <= Key.F12) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || (e.Key == Key.Space))
            {
                e.Handled = true;
                mainKey = e.Key.ToString();
            }
            //字母区上面的数字
            if (e.Key >= Key.D0 && e.Key <= Key.D9)
            {
                e.Handled = true;
                mainKey = e.Key.ToString().Replace("D", "");
            }
            //Alt Ctrl Shift键判断
            if ((Control.ModifierKeys & Keys.Alt) == Keys.Alt)
                pressAlt = 1;
            else
                pressAlt = 0;

            if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                pressCtrl = 2;
            else
                pressCtrl = 0;

            if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
                pressShift = 4;
            else
                pressShift = 0;

            var controlKeys = pressAlt + pressCtrl + pressShift;
            switch (controlKeys)
            {
                case 1:
                    preString = "Alt + ";
                    break;
                case 2:
                    preString = "Ctrl + ";
                    break;
                case 3:
                    preString = "Ctrl + Alt + ";
                    break;
                case 4:
                    preString = "Shift + ";
                    break;
                case 5:
                    preString = "Alt + Shift + ";
                    break;
                case 6:
                    preString = "Ctrl + Shift + ";
                    break;
                case 7:
                    preString = "Ctrl + Alt + Shift + ";
                    break;
                default:
                    preString = "";
                    break;
            }
            //输出值
            if (mainKey != "")
            {
                SeBossKeyKey.Content = preString + mainKey;
            }
            else
            {
                SeBossKeyKey.Content = "Ctrl + Alt + B";
            }
        }
    }
}
