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
using 饥荒百科全书CSharp.MyUserControl;

namespace 饥荒百科全书CSharp.View.Details
{
    /// <summary>
    /// GoodCreaturesDetail.xaml 的交互逻辑
    /// </summary>
    public partial class GoodCreaturesDetail : Page
    {
        private int _loadedTime;

        public void LoadCompleted(object sender, NavigationEventArgs e)
        {
            if (e.ExtraData == null || _loadedTime != 0) return;
            _loadedTime++;
            LoadData((GoodCreatures)e.ExtraData);
            if (Global.FontFamily != null)
            {
                FontFamily = Global.FontFamily;
            }
        }

        public GoodCreaturesDetail()
        {
            InitializeComponent();
            Global.GoodLeftFrame.NavigationService.LoadCompleted += LoadCompleted;
        }


        private void LoadData(GoodCreatures c)
        {
            GoodImage.Source = new BitmapImage(new Uri(c.Picture, UriKind.Relative));
            GoodName.Text = c.Name;
            GoodEnName.Text = c.EnName;
            // 保鲜
            if (c.Fresh == 0)
            {
                GoodFresh.Visibility = Visibility.Collapsed;
            }
            else
            {
                GoodFresh.Value = c.Fresh;
                GoodFresh.BarColor = Global.ColorBlue;
            }
            // 杀害后获得
            if (c.Goods.Count == 0)
            {
                GoodGoodsTextBlock.Visibility = Visibility.Collapsed;
                GoodGoodsWrapPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                var thickness = new Thickness(20, 0, 0, 0);
                foreach (var str in c.Goods)
                {
                    var breakPosition = str.IndexOf('|');
                    var goodSource = str.Substring(0, breakPosition);
                    var goodText = str.Substring(breakPosition + 1);
                    var picButton = new PicButton
                    {
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = thickness,
                        Source = StringProcess.GetGameResourcePath(goodSource),
                        Text = goodText
                    };
                    picButton.Click += Good_Jump_Click;
                    GoodGoodsWrapPanel.Children.Add(picButton);
                }
            }
            ConsolePre.Text = $"c_give(\"{c.Console}\",";
        }

        private static void Good_Jump_Click(object sender,RoutedEventArgs e)
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
                    case "G":
                        rightFrame.NavigationService.Navigate(new GoodPage(), extraData);
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
            Clipboard.SetText(ConsolePre.Text + ConsoleNum.Text + ")");
        }
    }
}
