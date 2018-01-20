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
using 饥荒百科全书CSharp.Class.JsonDeserialize;

namespace 饥荒百科全书CSharp.View.Details
{
    /// <summary>
    /// ScienceDetail.xaml 的交互逻辑
    /// </summary>
    public partial class ScienceDetail : Page
    {
        private int _loadedTime;

        public void LoadCompleted(object sender, NavigationEventArgs e)
        {
            if (e.ExtraData == null || _loadedTime != 0) return;
            _loadedTime++;
            LoadData((Science)e.ExtraData);
            if (Global.FontFamily != null)
            {
                FontFamily = Global.FontFamily;
            }
            ScienceLeftScrollViewer.FontWeight = Global.FontWeight;
        }

        public ScienceDetail()
        {
            InitializeComponent();
            Global.ScienceLeftFrame.NavigationService.LoadCompleted += LoadCompleted;
            Global.ConsoleSendKey = Console_Click;
        }

        private void LoadData(Science c)
        {
            ScienceImage.Source = new BitmapImage(new Uri(c.Picture,UriKind.Relative));
            ScienceName.Text = c.Name;
            ScienceEnName.Text = c.EnName;
            Need1PicButton.Source = StringProcess.GetGameResourcePath(c.Need1);
            Need1PicButton.Text = $"×{c.Need1Value}";
            if (c.Need2 != null)
            {
                Need2PicButton.Source = StringProcess.GetGameResourcePath(c.Need2);
                Need2PicButton.Text = $"×{c.Need2Value}";
                Need2PicButton.Visibility = Visibility.Visible;
            }
            if (c.Need3 != null)
            {
                Need3PicButton.Source = StringProcess.GetGameResourcePath(c.Need3);
                Need3PicButton.Text = $"×{c.Need3Value}";
                Need3PicButton.Visibility = Visibility.Visible;
            }
            if (c.Unlock == null && c.UnlockCharcter == null && c.UnlockBlueprint == null)
            {
                ScienceUnlockStackPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                if (c.Unlock != null && c.Unlock.Count > 0)
                {
                    UnlockPicButton.Visibility = Visibility.Visible;
                    UnlockPicButton.Source = StringProcess.GetGameResourcePath(c.Unlock[0]);
                    if (c.Unlock.Count == 2)
                    {
                        Unlock2PicButton.Visibility = Visibility.Visible;
                        Unlock2PicButton.Source = StringProcess.GetGameResourcePath(c.Unlock[1]);
                    }
                }
                if (c.UnlockCharcter != null)
                {
                    UnlockCharcterButton.Visibility = Visibility.Visible;
                    UnlockCharcterImage.Source = new BitmapImage(new Uri(StringProcess.GetGameResourcePath(c.UnlockCharcter),UriKind.Relative));
                    _unlockCharcter = StringProcess.GetGameResourcePath(c.UnlockCharcter);
                }
                if (c.UnlockBlueprint != null)
                {
                    UnlockBlueprintPicButton.Visibility = Visibility.Visible;
                    UnlockBlueprintPicButton.Source = StringProcess.GetGameResourcePath(c.UnlockBlueprint);
                }
            }
            ScienceIntroduction.Text = c.Introduction;
            ConsolePre.Text = $"c_give(\"{c.Console}\",";
        }

        private string _unlockCharcter;

        private void Science_Jump_Click(object sender, RoutedEventArgs e)
        {
            var picturePath = Global.ButtonToPicButton((Button)sender).Source;
            var rightFrame = Global.RightFrame;
            var shortName = StringProcess.GetFileName(picturePath);
            Global.SetAutoSuggestBoxItem();
            foreach (var suggestBoxItem in Global.AutoSuggestBoxItemSource)
            {
                if (picturePath != suggestBoxItem.Picture) continue;
                var picHead = shortName.Substring(0, 1);
                string[] extraData = { suggestBoxItem.SourcePath, suggestBoxItem.Picture }; ;
                switch (picHead)
                {
                    case "F":
                        Global.PageJump(2);
                        rightFrame.NavigationService.Navigate(new FoodPage(), extraData);
                        return;
                    case "S":
                        rightFrame.NavigationService.Navigate(new SciencePage(), extraData);
                        return;
                    case "G":
                        Global.PageJump(7);
                        rightFrame.NavigationService.Navigate(new GoodPage(), extraData);
                        return;
                }
            }
        }

        private void Science_CharacterJump_Click(object sender, RoutedEventArgs e)
        {
            var picturePath = _unlockCharcter;
            var rightFrame = Global.RightFrame;
            Global.SetAutoSuggestBoxItem();
            foreach (var suggestBoxItem in Global.AutoSuggestBoxItemSource)
            {
                if (picturePath != suggestBoxItem.Picture) continue;
                string[] extraData = { suggestBoxItem.SourcePath, suggestBoxItem.Picture }; ;
                Global.PageJump(1);
                rightFrame.NavigationService.Navigate(new CharacterPage(), extraData);
            }
        }

        private void ConsoleNum_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textbox = (TextBox)sender;
            StringProcess.ConsoleNumTextCheck(textbox);
        }

        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(ConsoleNum.Text) || double.Parse(ConsoleNum.Text) == 0)
            {
                ConsoleNum.Text = "1";
            }
            Global.SetClipboard(ConsolePre.Text + ConsoleNum.Text + ")");
        }

        private void Console_Click(object sender, RoutedEventArgs e)
        {
            SendKey.SendMessage(ConsolePre.Text + ConsoleNum.Text + ConsolePos.Text);
        }
    }
}
