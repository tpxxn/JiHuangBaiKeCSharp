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
    /// FoodRecipeDetail.xaml 的交互逻辑
    /// </summary>
    public partial class FoodRecipeDetail : Page
    {

        private int _loadedTime;

        public void LoadCompleted(object sender, NavigationEventArgs e)
        {
            if (e.ExtraData == null || _loadedTime != 0) return;
            _loadedTime++;
            LoadData((FoodRecipe2)e.ExtraData);
            if (Global.FontFamily != null)
            {
                FontFamily = Global.FontFamily;
            }
            FoodRecipeLeftScrollViewer.FontWeight = Global.FontWeight;
        }

        public FoodRecipeDetail()
        {
            this.InitializeComponent();
            Global.FoodLeftFrame.NavigationService.LoadCompleted += LoadCompleted;
            Global.ConsoleSendKey = Console_Click;
        }

        private void LoadData(FoodRecipe2 c)
        {
            FoodRecipeImage.Source = new BitmapImage(new Uri(c.Picture, UriKind.Relative));
            FoodRecipeName.Text = c.Name;
            FoodRecipeEnName.Text = c.EnName;
            if (c.PortableCrockPot)
            {
                FoodRecipePortableCrockPot.Visibility = Visibility.Visible;
                FoodRecipePortableCrockPot.Source = new BitmapImage(new Uri("/Resources/CP_PortableCrockPot.png", UriKind.Relative));
            }
            FoodRecipeHealth.Value = c.Health;
            FoodRecipeHealth.BarColor = Global.ColorGreen;
            FoodRecipeHunger.Value = c.Hunger;
            FoodRecipeHunger.BarColor = Global.ColorKhaki;
            FoodRecipeSanity.Value = c.Sanity;
            FoodRecipeSanity.BarColor = Global.ColorRed;
            FoodRecipePerish.Value = c.Perish;
            FoodRecipePerish.MaxValue = c.Perish > 20 ? 18750 : 20;
            FoodRecipePerish.BarColor = Global.ColorBlue;
            FoodRecipeCooktime.Value = c.Cooktime;
            FoodRecipeCooktime.BarColor = Global.ColorPurple;
            FoodRecipePriority.ShowIfZero = true;
            FoodRecipePriority.Value = c.Priority;
            FoodRecipePriority.BarColor = Global.ColorPink;
            FoodTemperature.Value = c.Temperature;
            FoodTemperature.BarColor = Global.ColorCyan;
            FoodTemperatureDuration.Value = c.TemperatureDuration;
            FoodTemperatureDuration.BarColor = Global.ColorOrange;
            if (c.EnName != "Wet Goop")
            {
                Need1Button.Source = StringProcess.GetGameResourcePath(c.NeedPicture1);
                Need1Button.Text = c.Need1;
            }
            else
            {
                FoodNeedStackPanel.Visibility = Visibility.Collapsed;
                FoodRecommendStackPanel.Visibility = Visibility.Collapsed;
            }
            if (c.NeedOr != null)
            {
                NeedOrButton.Visibility = Visibility.Visible;
                NeedOrButton.Source = StringProcess.GetGameResourcePath(c.NeedPictureOr);
                NeedOrButton.Text = c.NeedOr;
            }
            if (c.Need2 != null)
            {
                Need2Button.Visibility = Visibility.Visible;
                Need2Button.Source = StringProcess.GetGameResourcePath(c.NeedPicture2);
                Need2Button.Text = c.Need2;
            }
            if (c.Need3 != null)
            {
                Need3Button.Visibility = Visibility.Visible;
                Need3Button.Source = StringProcess.GetGameResourcePath(c.NeedPicture3);
                Need3Button.Text = c.Need3;
            }
            #region restrictions
            var restrictions1 = new List<string>();
            var restrictions2 = new List<string>();
            var prePicture = new[] { c.Restrictions1.Text, c.Restrictions2.Text, c.Restrictions3.Text, c.Restrictions4.Text, c.Restrictions5.Text };
            var pre = new[] { c.Restrictions1.Pre, c.Restrictions2.Pre, c.Restrictions3.Pre, c.Restrictions4.Pre, c.Restrictions5.Pre };
            var restrictionsAttributes = StringProcess.StringDelRepeatData(pre);
            if (pre[0] == restrictionsAttributes[0] && prePicture[0] != null)
            {
                restrictions1.Add(prePicture[0]);
            }
            if (pre[1] == restrictionsAttributes[0] && prePicture[1] != null)
            {
                restrictions1.Add(prePicture[1]);
            }
            if (pre[1] == restrictionsAttributes[1] && prePicture[1] != null)
            {
                restrictions2.Add(prePicture[1]);
            }
            if (pre[2] == restrictionsAttributes[0] && prePicture[2] != null)
            {
                restrictions1.Add(prePicture[2]);
            }
            if (pre[2] == restrictionsAttributes[1] && prePicture[2] != null)
            {
                restrictions2.Add(prePicture[2]);
            }
            if (pre[3] == restrictionsAttributes[0] && prePicture[3] != null)
            {
                restrictions1.Add(prePicture[3]);
            }
            if (pre[3] == restrictionsAttributes[1] && prePicture[3] != null)
            {
                restrictions2.Add(prePicture[3]);
            }
            if (pre[4] == restrictionsAttributes[0] && prePicture[4] != null)
            {
                restrictions1.Add(prePicture[4]);
            }
            if (pre[4] == restrictionsAttributes[1] && prePicture[4] != null)
            {
                restrictions2.Add(prePicture[4]);
            }
            if (c.Restrictions1.Pre != null)
            {
                var stackPanel1 = new StackPanel { Orientation = Orientation.Horizontal };
                var stackPanel2 = new StackPanel { Orientation = Orientation.Horizontal };
                if (restrictions1.Count != 0)
                {
                    var textBlock = new TextBlock
                    {
                        Text = restrictionsAttributes[0],
                        Padding = new Thickness(0, 8, 5, 0)
                    };
                    stackPanel1.Children.Add(textBlock);
                    foreach (var str in restrictions1)
                    {
                        var picButton = new PicButton()
                        {
                            Source = $"/Resources/GameResources/Foods/{str}.png"
                        };
                        picButton.Click += Food_Jump_Click;
                        stackPanel1.Children.Add(picButton);
                    }
                    FoodRecipeRestrictionsStackPanel.Children.Add(stackPanel1);
                }
                if (restrictions2.Count != 0)
                {
                    var textBlock = new TextBlock
                    {
                        Text = restrictionsAttributes[1],
                        Padding = new Thickness(0, 8, 5, 0)
                    };
                    stackPanel2.Children.Add(textBlock);
                    foreach (var str in restrictions2)
                    {
                        var picButton = new PicButton()
                        {
                            Source = $"/Resources/GameResources/Foods/{str}.png"
                        };
                        picButton.Click += Food_Jump_Click;
                        stackPanel2.Children.Add(picButton);
                    }
                    FoodRecipeRestrictionsStackPanel.Children.Add(stackPanel2);
                }
            }
            else
            {
                FoodRecipeRestrictionsTextBlock.Visibility = Visibility.Collapsed;
            }
            #endregion
            if (c.Recommend1 != null)
            {
                Recommend1Button.Source = StringProcess.GetGameResourcePath(c.Recommend1);
                Recommend2Button.Source = StringProcess.GetGameResourcePath(c.Recommend2);
                Recommend3Button.Source = StringProcess.GetGameResourcePath(c.Recommend3);
                Recommend4Button.Source = StringProcess.GetGameResourcePath(c.Recommend4);
            }
            FoodRecipeIntroduction.Text = c.Introduce;
            ConsolePre.Text = $"c_give(\"{c.Console}\",";
        }

        private void Food_Jump_Click(object sender, RoutedEventArgs eventArgs)
        {
            var picturePath = Global.ButtonToPicButton((Button)sender).Source;
            var rightFrame = Global.RightFrame;
            var shortName = StringProcess.GetFileName(picturePath);
            Global.SetAutoSuggestBoxItem();
            foreach (var suggestBoxItem in Global.AutoSuggestBoxItemSource)
            {
                if (picturePath != suggestBoxItem.Picture) continue;
                var picHead = shortName.Substring(0, 2);
                var extraData = new[] { suggestBoxItem.SourcePath, suggestBoxItem.Picture };
                switch (picHead)
                {
                    case "F_":
                        rightFrame.NavigationService.Navigate(new FoodPage(), extraData);
                        return;
                    case "FC":
                        // ignore
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
            Global.SetClipboard(ConsolePre.Text + ConsoleNum.Text + ")");
        }

        private void Console_Click(object sender, RoutedEventArgs e)
        {
            SendKey.SendMessage(ConsolePre.Text + ConsoleNum.Text + ConsolePos.Text);
        }
    }
}
