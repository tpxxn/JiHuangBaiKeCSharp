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
    /// GoodUnlockDetail.xaml 的交互逻辑
    /// </summary>
    public partial class GoodUnlockDetail : Page
    {
        private int _loadedTime;

        public void LoadCompleted(object sender, NavigationEventArgs e)
        {
            if (e.ExtraData == null || _loadedTime != 0) return;
            _loadedTime++;
            LoadData((GoodUnlock)e.ExtraData);
        }

        public GoodUnlockDetail()
        {
            InitializeComponent();
            Global.GoodLeftFrame.NavigationService.LoadCompleted += LoadCompleted;
        }

        private void LoadData(GoodUnlock c)
        {
            GoodImage.Source = new BitmapImage(new Uri(c.Picture, UriKind.Relative));
            GoodName.Text = c.Name;
            GoodEnName.Text = c.EnName;
            // 掉落
            var thickness = new Thickness(5, 0, 0, 0);
            if (c.DropBy == null || c.DropBy.Count == 0)
            {
                GoodDropByTextBlock.Visibility = Visibility.Collapsed;
                GoodDropByWrapPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                foreach (var picPath in c.DropBy)
                {
                    var picButton = new PicButton
                    {
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = thickness,
                        Source = StringProcess.GetGameResourcePath(picPath),
                        PictureSize = 75
                    };
                    GoodDropByWrapPanel.Children.Add(picButton);
                }
            }
            // 解锁人物
            GoodUnlockCharacterWrapPicButton.Source = StringProcess.GetGameResourcePath(c.UnlockCharacter);
            GoodIntroduction.Text = c.Introduction;
            ConsolePre.Text = $"c_give(\"{c.Console}\",";
        }

        private void ConsoleNum_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textbox = (TextBox)sender;
            StringProcess.ConsoleNumTextCheck(textbox);
        }

        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(ConsoleNum.Text))
            {
                ConsoleNum.Text = "1";
            }
            Clipboard.SetText(ConsolePre.Text + ConsoleNum.Text + ")");
        }
    }
}
