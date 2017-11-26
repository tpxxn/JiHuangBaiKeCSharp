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
    /// FoodDetail.xaml 的交互逻辑
    /// </summary>
    public partial class FoodDetail : Page
    {
        private int _loadedTime;

        public void LoadCompleted(object sender, NavigationEventArgs e)
        {
            if (e.ExtraData == null || _loadedTime != 0) return;
            _loadedTime++;
            LoadData((Food)e.ExtraData);
            if (Global.FontFamily != null)
            {
                FontFamily = Global.FontFamily;
            }
        }

        public FoodDetail()
        {
            InitializeComponent();
            Global.FoodLeftFrame.NavigationService.LoadCompleted += LoadCompleted;
        }


        private void LoadData(Food c)
        {
            FoodImage.Source = new BitmapImage(new Uri(c.Picture, UriKind.Relative));
            FoodName.Text = c.Name;
            FoodEnName.Text = c.EnName;
            FoodHealth.Value = c.Health;
            FoodHealth.BarColor = Global.ColorGreen;
            FoodHunger.Value = c.Hunger;
            FoodHunger.BarColor = Global.ColorKhaki;
            FoodSanity.Value = c.Sanity;
            FoodSanity.BarColor = Global.ColorRed;
            FoodPerish.Value = c.Perish;
            FoodPerish.BarColor = Global.ColorBlue;
            Attribute1PicButton.Source = $"/Resources/GameResources/Foods/{c.Attribute}.png";
            Attribute1PicButton.Text = c.AttributeValue ?? c.Attribute;
            if (c.AttributeValue2 != null)
            {
                Attribute2PicButton.Source = $"/Resources/GameResources/Foods/{c.Attribute2}.png";
                Attribute2PicButton.Text = c.AttributeValue2;
                Attribute2PicButton.Visibility = Visibility.Visible;
            }
            FoodIntroduction.Text = c.Introduce;
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
