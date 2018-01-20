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
    /// GoodEquipmentDetail.xaml 的交互逻辑
    /// </summary>
    public partial class GoodEquipmentDetail : Page
    {
        private int _loadedTime;

        public void LoadCompleted(object sender, NavigationEventArgs e)
        {
            if (e.ExtraData == null || _loadedTime != 0) return;
            _loadedTime++;
            LoadData((GoodEquipment)e.ExtraData);
            if (Global.FontFamily != null)
            {
                FontFamily = Global.FontFamily;
            }
            GoodEquipmentLeftScrollViewer.FontWeight = Global.FontWeight;
        }

        public GoodEquipmentDetail()
        {
            InitializeComponent();
            Global.GoodLeftFrame.NavigationService.LoadCompleted += LoadCompleted;
            Global.ConsoleSendKey = Console_Click;
        }

        private void LoadData(GoodEquipment c)
        {
            GoodImage.Source = new BitmapImage(new Uri(c.Picture, UriKind.Relative));
            GoodName.Text = c.Name;
            GoodEnName.Text = c.EnName;
            if (c.Attack != 0)
            {
                GoodAttack.Value = c.Attack;
                GoodAttack.BarColor = Global.ColorRed;
            }
            else
            {
                GoodAttack.Visibility = Visibility.Collapsed;
            }
            if (c.MinAttack != 0)
            {
                GoodMinAttack.Value = c.MinAttack;
                GoodMinAttack.BarColor = Global.ColorRed;
            }
            else
            {
                GoodMinAttack.Visibility = Visibility.Collapsed;
            }
            if (c.MaxAttack != 0)
            {
                GoodMaxAttack.Value = c.MaxAttack;
                GoodMaxAttack.BarColor = Global.ColorRed;
            }
            else
            {
                GoodMaxAttack.Visibility = Visibility.Collapsed;
            }
            GoodAttackString.Visibility = string.IsNullOrEmpty(c.AttackString) ? Visibility.Collapsed : Visibility.Visible;
            if (c.AttackOnBoat != 0)
            {
                GoodAttackOnBoat.Value = c.AttackOnBoat;
                GoodAttackOnBoat.BarColor = Global.ColorRed;
            }
            else
            {
                GoodAttackOnBoat.Visibility = Visibility.Collapsed;
            }
            if (c.AttackWet != 0)
            {
                GoodAttackWet.Value = c.AttackWet;
                GoodAttackWet.BarColor = Global.ColorRed;
            }
            else
            {
                GoodAttackWet.Visibility = Visibility.Collapsed;
            }
            if (string.IsNullOrEmpty(c.Durability) == false)
            {
                try
                {
                    var regularExpressionsResultValue = System.Text.RegularExpressions.Regex.Replace(c.Durability, @"[^\d.]+", "");
                    var doubleResult = double.Parse(regularExpressionsResultValue);
                    GoodDurability.Value = doubleResult;
                    var regularExpressionsResultUnit = System.Text.RegularExpressions.Regex.Replace(c.Durability, @"[\d.]+", "");
                    GoodDurability.Unit = regularExpressionsResultUnit;
                    GoodDurability.BarColor = Global.ColorBlue;
                }
                catch
                {
                    //ignore
                }
            }
            else
            {
                GoodDurability.Visibility = Visibility.Collapsed;
            }
            if (c.Wet != 0)
            {
                GoodWet.Value = c.Wet;
                GoodWet.BarColor = Global.ColorCyan;
            }
            else
            {
                GoodWet.Visibility = Visibility.Collapsed;
            }
            if (c.ColdResistance != 0)
            {
                GoodColdResistance.Value = c.ColdResistance;
                GoodColdResistance.BarColor = Global.ColorOrange;
            }
            else
            {
                GoodColdResistance.Visibility = Visibility.Collapsed;
            }
            if (c.HeatResistance != 0)
            {
                GoodHeatResistance.Value = c.HeatResistance;
                GoodHeatResistance.BarColor = Global.ColorOrange;
            }
            else
            {
                GoodHeatResistance.Visibility = Visibility.Collapsed;
            }
            if (c.Sanity != 0)
            {
                GoodSanity.Value = c.Sanity;
                GoodSanity.BarColor = Global.ColorPink;
            }
            else
            {
                GoodSanity.Visibility = Visibility.Collapsed;
            }
            if (c.Hunger != 0)
            {
                GoodHunger.Value = c.Hunger;
                GoodHunger.BarColor = Global.ColorPurple;
            }
            else
            {
                GoodHunger.Visibility = Visibility.Collapsed;
            }
            if (c.Defense != 0)
            {
                GoodDefense.Value = c.Defense;
                GoodDefense.BarColor = Global.ColorYellow;
            }
            else
            {
                GoodDefense.Visibility = Visibility.Collapsed;
            }
            if (c.Speed != 0)
            {
                GoodSpeed.Value = c.Speed;
                GoodSpeed.BarColor = Global.ColorBorderCyan;
            }
            else
            {
                GoodSpeed.Visibility = Visibility.Collapsed;
            }
            var maxTextLength = 0;
            foreach (var uiElement in BarChartStackPanel.Children)
            {
                if (uiElement.GetType() == typeof(BarChart))
                {
                    if (((BarChart)uiElement).Visibility == Visibility.Visible)
                    {
                        var textLength = ((BarChart)uiElement).LabelTextBlock.Text.Length;
                        if (textLength > maxTextLength)
                        {
                            maxTextLength = textLength;
                        }
                    }
                }
            }
            foreach (var uiElement in BarChartStackPanel.Children)
            {
                if (uiElement.GetType() == typeof(BarChart))
                {
                    ((BarChart)uiElement).LabelWidth = maxTextLength * 10 + 15;
                }
            }
            // 特殊能力
            if (c.Ability.Count == 0)
            {
                AbilityTextBlock.Visibility = Visibility.Collapsed;
                AbilityStackPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                foreach (var str in c.Ability)
                {
                    var textBlock = new TextBlock
                    {
                        HorizontalAlignment = HorizontalAlignment.Left,
                        TextWrapping = TextWrapping.Wrap,
                        Text = str
                    };
                    AbilityStackPanel.Children.Add(textBlock);
                }
            }
            // 来源于生物
            if (string.IsNullOrEmpty(c.DropBy))
            {
                GoodSourceStackPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                GoodSourcePicButton.Source = StringProcess.GetGameResourcePath(c.DropBy);
            }
            // 介绍
            GoodIntroduction.Text = c.Introduction;
            // 控制台
            if (c.Console != null)
            {
                ConsolePre.Text = $"c_give(\"{c.Console}\",";
            }
            else
            {
                CopyGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void DropBy_Jump_Click(object sender, RoutedEventArgs e)
        {
            var picturePath = Global.ButtonToPicButton((Button)sender).Source;
            var rightFrame = Global.RightFrame;
            Global.SetAutoSuggestBoxItem();
            foreach (var suggestBoxItem in Global.AutoSuggestBoxItemSource)
            {
                if (picturePath != suggestBoxItem.Picture) continue;
                string[] extraData = { suggestBoxItem.SourcePath, suggestBoxItem.Picture }; ;
                Global.PageJump(4);
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

        private void Console_Click(object sender, RoutedEventArgs e)
        {
            SendKey.SendMessage(ConsolePre.Text + ConsoleNum.Text + ConsolePos.Text);
        }
    }
}
