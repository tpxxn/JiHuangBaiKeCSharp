using System;
using System.Collections.Generic;
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
    /// SettingPage.xaml 的交互逻辑
    /// </summary>
    public partial class SettingPage : Page
    {
        public void LoadCompleted(object sender, NavigationEventArgs e)
        {
            if (Global.FontFamily != null)
            {
                FontFamily = Global.FontFamily;
            }
            TopListBox.FontWeight = Global.FontWeight;
        }

        public SettingPage()
        {
            InitializeComponent();
            Global.SettingRootFrame = RootFrame;
            Global.RightFrame.NavigationService.LoadCompleted += LoadCompleted;
            RootFrame.Navigate(new SettingChildPage.SettingChildPage());
        }

        private void TopListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var listBoxItem = (ListBoxItem) ((ListBox) sender).SelectedItem;
                if (listBoxItem != null)
                {
                    var listBoxItemName = listBoxItem.Name;

                    switch (listBoxItemName)
                    {
                        case "SettingBoxItem":
                            RootFrame.Navigate(new SettingChildPage.SettingChildPage());
                            break;
                        case "ReleaseBoxItem":
                            RootFrame.Navigate(new SettingChildPage.ReleaseChildPage());
                            break;
                        case "FeedbackBoxItem":
                            //RootFrame.Navigate(typeof(SettingChildPage.FeedbackChildPage));
                            break;
                        case "AboutBoxItem":
                            RootFrame.Navigate(new SettingChildPage.AboutChildPage());
                            break;
                        default:
                            break;
                    }
                }
            }
            catch
            {
                // ignored
            }
        }
    }
}
