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
using 饥荒百科全书CSharp.MyUserControl;

namespace 饥荒百科全书CSharp.View.Details
{
    /// <summary>
    /// GoodMaterialDetail.xaml 的交互逻辑
    /// </summary>
    public partial class GoodMaterialDetail : Page
    {
        private int _loadedTime;

        private string console;

        public void LoadCompleted(object sender, NavigationEventArgs e)
        {
            if (e.ExtraData == null || _loadedTime != 0) return;
            _loadedTime++;
            LoadData((GoodMaterial)e.ExtraData);
            if (Global.FontFamily != null)
            {
                FontFamily = Global.FontFamily;
            }
            GoodMaterialLeftScrollViewer.FontWeight = Global.FontWeight;
        }

        public GoodMaterialDetail()
        {
            InitializeComponent();
            Global.GoodLeftFrame.NavigationService.LoadCompleted += LoadCompleted;
            Global.ConsoleSendKey = Console_Click;
        }

        private void LoadData(GoodMaterial c)
        {
            GoodImage.Source = new BitmapImage(new Uri(c.Picture, UriKind.Relative));
            GoodName.Text = c.Name;
            GoodEnName.Text = c.EnName;
            // 可制作科技/来源于生物
            var thickness = new Thickness(5, 0, 0, 0);
            if (c.Science == null || c.Science.Count == 0)
            {
                GoodScienceTextBlock.Visibility = Visibility.Collapsed;
                GoodScienceWrapPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                foreach (var picPath in c.Science)
                {
                    var picButton = new PicButton
                    {
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = thickness,
                        Source = StringProcess.GetGameResourcePath(picPath)
                    };
                    picButton.Click += Good_Jump_Click;
                    GoodScienceWrapPanel.Children.Add(picButton);
                }
            }
            if (c.SourceCreature == null || c.SourceCreature.Count == 0)
            {
                GoodSourceCreatureTextBlock.Visibility = Visibility.Collapsed;
                GoodSourceCreatureWrapPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                foreach (var picPath in c.SourceCreature)
                {
                    var picButton = new PicButton
                    {
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = thickness,
                        Source = StringProcess.GetGameResourcePath(picPath)
                    };
                    picButton.Click += Good_Jump_Click;
                    GoodSourceCreatureWrapPanel.Children.Add(picButton);
                }
            }
            // 介绍
            GoodIntroduction.Text = c.Introduction;
            // 控制台
            ConsolePre.Text = $"c_give(\"{c.Console}\",";
            console = c.Console;
        }


        private static void Good_Jump_Click(object sender, RoutedEventArgs e)
        {
            var picturePath = Global.ButtonToPicButton((Button)sender).Source;
            var shortName = StringProcess.GetFileName(picturePath);
            Global.SetAutoSuggestBoxItem();
            foreach (var suggestBoxItem in Global.AutoSuggestBoxItemSource)
            {
                if (picturePath != suggestBoxItem.Picture) continue;
                var picHead = shortName.Substring(0, 1);
                string[] extraData = { suggestBoxItem.SourcePath, suggestBoxItem.Picture };
                switch (picHead)
                {
                    case "S":
                        Global.PageJump(4, extraData);
                        return;
                    case "A":
                        Global.PageJump(5, extraData);
                        return;
                }
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
