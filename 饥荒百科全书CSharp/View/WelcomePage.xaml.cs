using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using 饥荒百科全书CSharp.Class;

namespace 饥荒百科全书CSharp.View
{
    /// <summary>
    /// WelcomePage.xaml 的交互逻辑
    /// </summary>
    public partial class WelcomePage : Page
    {
        //private int _loadedTime;

        public void LoadCompleted(object sender, NavigationEventArgs e)
        {
            //if (_loadedTime != 0) return;
            //_loadedTime++;
            if (Global.FontFamily != null)
            {
                FontFamily = Global.FontFamily;
            }
            foreach (var obj in StackPanel.Children)
            {
                if (obj.GetType() == typeof(Button))
                {
                    ((Button)obj).FontWeight = Global.FontWeight;
                }
            }
            TipTextBlock.FontWeight = Global.FontWeight;
        }

        public WelcomePage()
        {
            InitializeComponent();
            Global.RightFrame.NavigationService.LoadCompleted += LoadCompleted;
        }

        #region "主页面板"
        // 官网
        private void W_button_official_website_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("http://www.jihuangbaike.com");
        }

        // 饥荒盒子
        private void W_button_jihuanghezi_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("http://www.fireleaves.cn");
        }
        
        // Mod
        private void W_button_Mods_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("http://steamcommunity.com/sharedfiles/filedetails/?id=635215011");
        }

        // DS新闻
        private void W_button_DSNews_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("http://store.steampowered.com/news/?appids=219740");
        }

        // DST新闻
        private void W_button_DSTNewS_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("http://store.steampowered.com/news/?appids=322330");
        }

        // 群二维码
        private void W_button_QRCode_Qun_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("tencent://groupwpa/?subcmd=all&param=7B2267726F757055696E223A3538303333323236382C2274696D655374616D70223A313437303132323238337D0A");
        }

        // SteamCommunity
        private void W_button_SteamCommunity_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://pan.baidu.com/s/1pMcy1U7");
        }
        #endregion
    }
}
