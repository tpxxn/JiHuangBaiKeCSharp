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
    /// GoodTurfDetail.xaml 的交互逻辑
    /// </summary>
    public partial class GoodTurfDetail : Page
    {
        private int _loadedTime;

        private string console;

        public void LoadCompleted(object sender, NavigationEventArgs e)
        {
            if (e.ExtraData == null || _loadedTime != 0) return;
            _loadedTime++;
            LoadData((GoodTurf)e.ExtraData);
            if (Global.FontFamily != null)
            {
                FontFamily = Global.FontFamily;
            }
            GoodTurfLeftScrollViewer.FontWeight = Global.FontWeight;
        }

        public GoodTurfDetail()
        {
            InitializeComponent();
            Global.GoodLeftFrame.NavigationService.LoadCompleted += LoadCompleted;
            Global.ConsoleSendKey = Console_Click;
        }

        private void LoadData(GoodTurf c)
        {
            GoodImage.Source = new BitmapImage(new Uri(c.Picture, UriKind.Relative));
            GoodName.Text = c.Name;
            GoodEnName.Text = c.EnName;
            // 制作科技
            if (string.IsNullOrEmpty(c.Make))
            {
                GoodMakeStackPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                GoodMakePicButton.Source = StringProcess.GetGameResourcePath(c.Make);
            }
            //草皮纹理
            GoodSourceTextureWrapPanel.Source = new BitmapImage(new Uri(StringProcess.GetGameResourcePath(c.Texture), UriKind.Relative));
            GoodIntroduction.Text = c.Introduction;

            if (c.Console != null)
            {
                ConsolePre.Text = $"c_give(\"{c.Console}\",";
                console = c.Console;
            }
            else
            {
                CopyGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void Good_Jump_Click(object sender, RoutedEventArgs e)
        {
            var picturePath = Global.ButtonToPicButton((Button)sender).Source;
            Global.SetAutoSuggestBoxItem();
            foreach (var suggestBoxItem in Global.AutoSuggestBoxItemSource)
            {
                if (picturePath != suggestBoxItem.Picture) continue;
                string[] extraData = { suggestBoxItem.SourcePath, suggestBoxItem.Picture }; ;
                Global.PageJump(3, extraData);
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
            Global.SetClipboard(Settings.CopySelfMode == false ? $"{ConsolePre.Text}{ConsoleNum.Text})" : $"{console}");
        }

        private void Console_Click(object sender, RoutedEventArgs e)
        {
            SendKey.SendMessage(ConsolePre.Text + ConsoleNum.Text + ConsolePos.Text);
        }
    }
}
