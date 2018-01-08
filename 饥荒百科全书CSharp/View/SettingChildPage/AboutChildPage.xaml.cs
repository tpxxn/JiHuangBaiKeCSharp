using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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

namespace 饥荒百科全书CSharp.View.SettingChildPage
{
    /// <summary>
    /// AboutChildPage.xaml 的交互逻辑
    /// </summary>
    public partial class AboutChildPage : Page
    {
        public void LoadCompleted(object sender, NavigationEventArgs e)
        {
            if (Global.FontFamily != null)
            {
                FontFamily = Global.FontFamily;
            }
            RootScrollViewer.FontWeight = Global.FontWeight;
        }

        public AboutChildPage()
        {
            InitializeComponent();
            Global.SettingRootFrame.NavigationService.LoadCompleted += LoadCompleted;
            VersionTextBlock.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        //官网
        private void GuanWangButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("http://www.jihuangbaike.com");
        }

        //邮件
        private void EmailButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("mailto:351765204@qq.com");
        }

        //Wiki
        private void WikiButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("http://dontstarve.wikia.com");
        }
        
        //Github
        private void GithubButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/tpxxn/JiHuangBaiKeCSharp/blob/newMaster/LICENSE");
        }

        private void ZfbRadioButton_Click(object sender, RoutedEventArgs e)
        {
            Donation1Image.Source = new BitmapImage(new Uri("/饥荒百科全书CSharp;component/Resources/Pic/QRCode_zfb1.png", UriKind.RelativeOrAbsolute));
            Donation2Image.Source = new BitmapImage(new Uri("/饥荒百科全书CSharp;component/Resources/Pic/QRCode_zfb2.png", UriKind.RelativeOrAbsolute));
            Donation5Image.Source = new BitmapImage(new Uri("/饥荒百科全书CSharp;component/Resources/Pic/QRCode_zfb5.png", UriKind.RelativeOrAbsolute));
        }

        private void WxRadioButton_Click(object sender, RoutedEventArgs e)
        {
            Donation1Image.Source = new BitmapImage(new Uri("/饥荒百科全书CSharp;component/Resources/Pic/QRCode_wx1.png", UriKind.RelativeOrAbsolute));
            Donation2Image.Source = new BitmapImage(new Uri("/饥荒百科全书CSharp;component/Resources/Pic/QRCode_wx2.png", UriKind.RelativeOrAbsolute));
            Donation5Image.Source = new BitmapImage(new Uri("/饥荒百科全书CSharp;component/Resources/Pic/QRCode_wx5.png", UriKind.RelativeOrAbsolute));
        }

    }
}
