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
    /// DedicatedServerPage.xaml 的交互逻辑
    /// </summary>
    public partial class DedicatedServerPage : Page
    {
        public DedicatedServerPage()
        {
            InitializeComponent();
            //服务器面板初始化
            DediButtomPanelInitalize();
        }

        #region "主菜单按钮"
        private void DediTitleSetting_Click(object sender, RoutedEventArgs e)
        {
            DediButtomPanelVisibility("Setting");
        }

        private void DediTitleBaseSet_Click(object sender, RoutedEventArgs e)
        {
            DediButtomPanelVisibility("BaseSet");
        }

        private void DediTitleEditWorld_Click(object sender, RoutedEventArgs e)
        {
            DediButtomPanelVisibility("EditWorld");
        }

        private void DediTitleMod_Click(object sender, RoutedEventArgs e)
        {
            DediButtomPanelVisibility("Mod");
        }

        private void DediTitleRollback_Click(object sender, RoutedEventArgs e)
        {
            DediButtomPanelVisibility("Rollback");
        }

        private void DediTitleBlacklist_Click(object sender, RoutedEventArgs e)
        {
            DediButtomPanelVisibility("Blacklist");
        }
        #endregion

        #region "下侧面板Visibility属性设置"
        private void DediButtomPanelVisibilityInitialize()
        {
            foreach (UIElement vControl in DediButtomBg.Children)
            {
                vControl.Visibility = Visibility.Collapsed;
            }
            Global.UiElementVisibility(Visibility.Visible, DediButtomBorderH1, DediButtomBorderH2, DediButtomBorderV1, DediButtomBorderV4);
        }

        private void DediButtomPanelVisibility(string obj)
        {
            DediButtomPanelVisibilityInitialize();
            switch (obj)
            {
                case "Setting":
                    DediSetting.Visibility = Visibility.Visible;
                    break;
                case "BaseSet":
                    DediBaseSet.Visibility = Visibility.Visible;
                    break;
                case "EditWorld":

                    break;
                case "Mod":

                    break;
                case "Rollback":

                    break;
                case "Blacklist":

                    break;
            }
        }
        #endregion

        private void DediButtomPanelInitalize()
        {
            string[] gameVersion = { "Steam", "TGP", "游侠" };
            DediSettingGameVersionSelect.Init(gameVersion);

            DediButtomPanelVisibilityInitialize();
            string[] noYes = { "否", "是" };
            string[] gamemode = { "生存", "荒野", "无尽" };
            var maxPlayer = new string[64];
            for (var i = 1; i <= 64; i++)
            {
                maxPlayer[i - 1] = i.ToString();
            }
            string[] offline = { "在线", "离线" };
            DediBaseSetGrouponlySelect.Init(noYes);
            DediBaseSetGroupadminsSelect.Init(noYes);
            DediBaseSetGamemodeSelect.Init(gamemode);
            DediBaseSetPvpSelect.Init(noYes);
            DediBaseSetMaxPlayerSelect.Init(maxPlayer, 5);
            DediBaseOfflineSelect.Init(offline);
            DediBaseSet.Visibility = Visibility.Visible;
            DediBaseSetRangeInitalize();
        }

        #region "Intention"
        private void DediIntention_social_Click(object sender, RoutedEventArgs e)
        {
            DediIntention_Click("social");
        }

        private void DediIntention_social_MouseEnter(object sender, MouseEventArgs e)
        {
            DidiIntentionTextBlock.Text = (string)(((Button)sender).Tag);
        }

        private void DediIntention_social_MouseLeave(object sender, MouseEventArgs e)
        {
            DidiIntentionTextBlock.Text = "";
        }

        private void DediIntention_cooperative_Click(object sender, RoutedEventArgs e)
        {
            DediIntention_Click("cooperative");
        }

        private void DediIntention_cooperative_MouseEnter(object sender, MouseEventArgs e)
        {
            DidiIntentionTextBlock.Text = (string)(((Button)sender).Tag);
        }

        private void DediIntention_cooperative_MouseLeave(object sender, MouseEventArgs e)
        {
            DidiIntentionTextBlock.Text = "";
        }

        private void DediIntention_competitive_Click(object sender, RoutedEventArgs e)
        {
            DediIntention_Click("competitive");
        }

        private void DediIntention_competitive_MouseEnter(object sender, MouseEventArgs e)
        {
            DidiIntentionTextBlock.Text = (string)(((Button)sender).Tag);
        }

        private void DediIntention_competitive_MouseLeave(object sender, MouseEventArgs e)
        {
            DidiIntentionTextBlock.Text = "";
        }

        private void DediIntention_madness_Click(object sender, RoutedEventArgs e)
        {
            DediIntention_Click("madness");
        }

        private void DediIntention_madness_MouseEnter(object sender, MouseEventArgs e)
        {
            DidiIntentionTextBlock.Text = (string)(((Button)sender).Tag);
        }

        private void DediIntention_madness_MouseLeave(object sender, MouseEventArgs e)
        {
            DidiIntentionTextBlock.Text = "";
        }

        private void DediIntention_Click(string intention)
        {
            DediButtomPanelVisibilityInitialize();
            DediBaseSet.Visibility = Visibility.Visible;
            switch (intention)
            {
                case "social":
                    DediBaseSetIntentionButton.Content = "交际";
                    break;
                case "cooperative":
                    DediBaseSetIntentionButton.Content = "合作";
                    break;
                case "competitive":
                    DediBaseSetIntentionButton.Content = "竞争";
                    break;
                case "madness":
                    DediBaseSetIntentionButton.Content = "疯狂";
                    break;
            }
        }
        #endregion

        #region "BaseSet"
        private void DediBaseSetIntentionButton_Click(object sender, RoutedEventArgs e)
        {
            DediButtomPanelVisibilityInitialize();
            DediIntention.Visibility = Visibility.Visible;
        }


        private void DediBaseSetRangePublic_Click(object sender, RoutedEventArgs e)
        {
            DediBaseSetRangeInitalize();
        }

        private void DediBaseSetRangeFriendonly_Click(object sender, RoutedEventArgs e)
        {
            DediBaseSetRangeInitalize();
        }

        private void DediBaseSetRangeLocal_Click(object sender, RoutedEventArgs e)
        {
            DediBaseSetRangeInitalize();
        }

        private void DediBaseSetRangeSteamgroup_Click(object sender, RoutedEventArgs e)
        {
            Global.UiElementVisibility(Visibility.Visible, DediBaseSetGroupid, DediBaseSetGrouponly, DediBaseSetGroupadmins);
        }

        private void DediBaseSetRangeInitalize()
        {
            Global.UiElementVisibility(Visibility.Visible, DediBaseSetGroupid, DediBaseSetGrouponly, DediBaseSetGroupadmins);
        }
        #endregion
    }
}
