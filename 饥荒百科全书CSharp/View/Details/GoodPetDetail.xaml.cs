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
    /// GoodPetDetail.xaml 的交互逻辑
    /// </summary>
    public partial class GoodPetDetail : Page
    {
        private int _loadedTime;

        public void LoadCompleted(object sender, NavigationEventArgs e)
        {
            if (e.ExtraData == null || _loadedTime != 0) return;
            _loadedTime++;
            LoadData((GoodPet)e.ExtraData);
            if (Global.FontFamily != null)
            {
                FontFamily = Global.FontFamily;
            }
            GoodPetLeftScrollViewer.FontWeight = Global.FontWeight;
        }

        public GoodPetDetail()
        {
            InitializeComponent();
            Global.GoodLeftFrame.NavigationService.LoadCompleted += LoadCompleted;
        }

        private void LoadData(GoodPet c)
        {
            GoodImage.Source = new BitmapImage(new Uri(c.Picture, UriKind.Relative));
            GoodName.Text = c.Name;
            GoodEnName.Text = c.EnName;
            // 宠物死亡
            GoodDeadWrapPicButton.Source = StringProcess.GetGameResourcePath(c.Dead);
            // 跟随宠物
            var thickness = new Thickness(5, 0, 0, 0);
            if (c.Follow == null || c.Follow.Count == 0)
            {
                GoodFollowTextBlock.Visibility = Visibility.Collapsed;
                GoodFollowWrapPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                foreach (var picPath in c.Follow)
                {
                    var picButton = new PicButton
                    {
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = thickness,
                        Source = StringProcess.GetGameResourcePath(picPath),
                        PictureSize = 90
                    };
                    picButton.Click += Good_Jump_Click;
                    GoodFollowWrapPanel.Children.Add(picButton);
                }
            }
            GoodIntroduction.Text = c.Introduction;
            ConsolePre.Text = $"c_give(\"{c.Console}\",";
        }

        private static void Good_Jump_Click(object sender, RoutedEventArgs e)
        {
            var picturePath = Global.ButtonToPicButton((Button)sender).Source;
            var rightFrame = Global.RightFrame;
            Global.SetAutoSuggestBoxItem();
            foreach (var suggestBoxItem in Global.AutoSuggestBoxItemSource)
            {
                if (picturePath != suggestBoxItem.Picture) continue;
                string[] extraData = { suggestBoxItem.SourcePath, suggestBoxItem.Picture }; ;
                Global.PageJump(5);
                rightFrame.NavigationService.Navigate(new CreaturePage(), extraData);
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
    }
}
