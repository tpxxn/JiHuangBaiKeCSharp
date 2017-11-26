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
    /// GoodSaplingDetail.xaml 的交互逻辑
    /// </summary>
    public partial class GoodSaplingDetail : Page
    {
        private int _loadedTime;

        public void LoadCompleted(object sender, NavigationEventArgs e)
        {
            if (e.ExtraData == null || _loadedTime != 0) return;
            _loadedTime++;
            LoadData((GoodSapling)e.ExtraData);
            if (Global.FontFamily != null)
            {
                FontFamily = Global.FontFamily;
            }
        }

        public GoodSaplingDetail()
        {
            InitializeComponent();
            Global.GoodLeftFrame.NavigationService.LoadCompleted += LoadCompleted;
        }

        private void LoadData(GoodSapling c)
        {
            GoodImage.Source = new BitmapImage(new Uri(c.Picture, UriKind.Relative));
            GoodName.Text = c.Name;
            GoodEnName.Text = c.EnName;
            // 来源于植物
            var thickness = new Thickness(5, 0, 0, 0);
            if (c.Plants == null || c.Plants.Count == 0)
            {
                GoodSourcePlantsTextBlock.Visibility = Visibility.Collapsed;
                GoodSourcePlantsWrapPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                foreach (var picPath in c.Plants)
                {
                    var picButton = new PicButton
                    {
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = thickness,
                        Source = StringProcess.GetGameResourcePath(picPath),
                        PictureSize = 100
                    };
                    GoodSourcePlantsWrapPanel.Children.Add(picButton);
                }
            }
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
            if (string.IsNullOrEmpty(ConsoleNum.Text) || double.Parse(ConsoleNum.Text) == 0)
            {
                ConsoleNum.Text = "1";
            }
            Clipboard.SetText(ConsolePre.Text + ConsoleNum.Text + ")");
        }
    }
}
