using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using 饥荒百科全书CSharp.Class;
using 饥荒百科全书CSharp.MyUserControl;

namespace 饥荒百科全书CSharp
{
    /// <summary>
    /// MainWindowCharacter
    /// </summary>
    public partial class MainWindow : Window
    {
        //Character面板Click事件
        private void Character_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string[] BWTTag = (string[])button.Tag;//获取参数
            Character_Click_Handle(BWTTag);
        }

        //WrapPanel_Left_Character控件创建事件
        private void Character_Click_Handle(string[] BWTTag)
        {
            try
            {
                WrapPanel_Left_Character.Children.Clear();//清空WrapPanel_Left_Character
                Grid GI = new Grid();
                GI = PG.GridInterval(20);
                WrapPanel_Left_Character.Children.Add(GI);
                #region "图片 BWTTag[0]"
                Grid gPicture = PG.GridInit(180);
                Image iPicture = new Image();
                iPicture.Height = 160;
                iPicture.Width = 140;
                iPicture.HorizontalAlignment = HorizontalAlignment.Center;
                iPicture.Source = RSN.PictureShortName(BWTTag[0]);
                gPicture.Children.Add(iPicture);
                WrapPanel_Left_Character.Children.Add(gPicture);
                #endregion
                GI = PG.GridInterval(5);
                WrapPanel_Left_Character.Children.Add(GI);
                #region "中文名  BWTTag[1]"
                Grid gName = PG.GridInit();
                TextBlock tbName = new TextBlock();
                tbName.HorizontalAlignment = HorizontalAlignment.Center;
                tbName.Text = BWTTag[1];
                tbName.FontSize = 26.667;
                gName.Children.Add(tbName);
                WrapPanel_Left_Character.Children.Add(gName);
                #endregion
                #region "英文名  BWTTag[2]"
                Grid gEnName = PG.GridInit();
                TextBlock tbEnName = new TextBlock();
                tbEnName.HorizontalAlignment = HorizontalAlignment.Center;
                tbEnName.Text = BWTTag[2];
                tbEnName.FontSize = 26.667;
                Thickness TEnName = new Thickness();
                TEnName.Top = -5;
                tbEnName.Margin = TEnName;
                gEnName.Children.Add(tbEnName);
                WrapPanel_Left_Character.Children.Add(gEnName);
                #endregion
                #region "座右铭  BWTTag[3]"
                if (BWTTag[3] != "")
                {
                    GI = PG.GridInterval(5);
                    WrapPanel_Left_Character.Children.Add(GI);
                    Grid gMotto = PG.GridInit(20);
                    TextBlock tbMotto = new TextBlock();
                    tbMotto.HorizontalAlignment = HorizontalAlignment.Center;
                    tbMotto.Text = BWTTag[3];
                    tbMotto.FontSize = 16;
                    gMotto.Children.Add(tbMotto);
                    WrapPanel_Left_Character.Children.Add(gMotto);
                }
                #endregion
                #region "描述_1  BWTTag[4]"
                if (BWTTag[4] != "")
                {
                    GI = PG.GridInterval(10);
                    WrapPanel_Left_Character.Children.Add(GI);
                    Grid gDescriptions_1 = PG.GridInit();
                    TextBlock tbDescriptions_1 = new TextBlock();
                    tbDescriptions_1.HorizontalAlignment = HorizontalAlignment.Left;
                    tbDescriptions_1.Text = BWTTag[4];
                    tbDescriptions_1.FontSize = 14;
                    Thickness TDescriptions_1 = new Thickness();
                    TDescriptions_1.Left = 25;
                    tbDescriptions_1.Margin = TDescriptions_1;
                    gDescriptions_1.Children.Add(tbDescriptions_1);
                    WrapPanel_Left_Character.Children.Add(gDescriptions_1);
                }
                #endregion
                #region "描述_2  BWTTag[5]"
                if (BWTTag[5] != "")
                {
                    Grid gDescriptions_2 = PG.GridInit();
                    TextBlock tbDescriptions_2 = new TextBlock();
                    tbDescriptions_2.HorizontalAlignment = HorizontalAlignment.Left;
                    tbDescriptions_2.Text = BWTTag[5];
                    tbDescriptions_2.FontSize = 14;
                    Thickness TDescriptions_2 = new Thickness();
                    TDescriptions_2.Left = 25;
                    tbDescriptions_2.Margin = TDescriptions_2;
                    gDescriptions_2.Children.Add(tbDescriptions_2);
                    WrapPanel_Left_Character.Children.Add(gDescriptions_2);
                }
                #endregion
                #region "描述_3  BWTTag[6]"
                if (BWTTag[6] != "")
                {
                    Grid gDescriptions_3 = PG.GridInit();
                    TextBlock tbDescriptions_3 = new TextBlock();
                    tbDescriptions_3.HorizontalAlignment = HorizontalAlignment.Left;
                    tbDescriptions_3.Text = BWTTag[6];
                    tbDescriptions_3.FontSize = 14;
                    Thickness TDescriptions_3 = new Thickness();
                    TDescriptions_3.Left = 25;
                    tbDescriptions_3.Margin = TDescriptions_3;
                    gDescriptions_3.Children.Add(tbDescriptions_3);
                    WrapPanel_Left_Character.Children.Add(gDescriptions_3);
                }
                #endregion
                #region "生命  BWTTag[7]"
                if (BWTTag[7] != "")
                {
                    GI = PG.GridInterval(25);
                    WrapPanel_Left_Character.Children.Add(GI);
                    Grid gHealth = PG.GridInit(15);
                    PropertyBar pbHealth = new PropertyBar();
                    pbHealth.UCTextBlockName.Width = 57;
                    pbHealth.UCTextBlockName.Text = "生命";
                    pbHealth.UCProgressBar.Value = Convert.ToDouble(BWTTag[7]) / 3;
                    pbHealth.UCProgressBar.Foreground = BC.brushConverter(PBCGreen);
                    pbHealth.UCTextBlockValue.Width = 32;
                    pbHealth.UCTextBlockValue.Text = BWTTag[7];
                    gHealth.Children.Add(pbHealth);
                    WrapPanel_Left_Character.Children.Add(gHealth);
                }
                #endregion
                #region "饥饿  BWTTag[8]"
                if (BWTTag[8] != "")
                {
                    GI = PG.GridInterval(10);
                    WrapPanel_Left_Character.Children.Add(GI);
                    Grid gHunger = PG.GridInit(15);
                    PropertyBar pbHunger = new PropertyBar();
                    pbHunger.UCTextBlockName.Width = 57;
                    pbHunger.UCTextBlockName.Text = "饥饿";
                    pbHunger.UCProgressBar.Value = Convert.ToDouble(BWTTag[8]) / 3;
                    pbHunger.UCProgressBar.Foreground = BC.brushConverter(PBCOrange);
                    pbHunger.UCTextBlockValue.Width = 32;
                    pbHunger.UCTextBlockValue.Text = BWTTag[8];
                    gHunger.Children.Add(pbHunger);
                    if (BWTTag[1] == "沃尔夫冈")
                    {
                        Slider WolfgangSlider = new Slider();
                        WolfgangSlider.Style = (Style)FindResource("SliderStyle");
                        WolfgangSlider.Focusable = false;
                        WolfgangSlider.IsSelectionRangeEnabled = true;
                        WolfgangSlider.Value = 200;
                        WolfgangSlider.Maximum = 300;
                        WolfgangSlider.Minimum = 0;
                        WolfgangSlider.SmallChange = 1;
                        WolfgangSlider.LargeChange = 10;
                        WolfgangSlider.ValueChanged += WolfgangSlider_ValueChanged;
                        Thickness TSlider = new Thickness();
                        TSlider.Left = 72;
                        TSlider.Right = 37;
                        WolfgangSlider.Margin = TSlider;
                        gHunger.Children.Add(WolfgangSlider);
                    }
                    WrapPanel_Left_Character.Children.Add(gHunger);
                }
                #endregion
                #region "精神  BWTTag[9]"
                if (BWTTag[9] != "")
                {
                    GI = PG.GridInterval(10);
                    WrapPanel_Left_Character.Children.Add(GI);
                    Grid gSanity = PG.GridInit(15);
                    PropertyBar pbSanity = new PropertyBar();
                    pbSanity.UCTextBlockName.Width = 57;
                    pbSanity.UCTextBlockName.Text = "精神";
                    pbSanity.UCProgressBar.Value = Convert.ToDouble(BWTTag[9]) / 2.5;
                    pbSanity.UCProgressBar.Foreground = BC.brushConverter(PBCRed);
                    pbSanity.UCTextBlockValue.Width = 32;
                    pbSanity.UCTextBlockValue.Text = BWTTag[9];
                    gSanity.Children.Add(pbSanity);
                    WrapPanel_Left_Character.Children.Add(gSanity);
                }
                #endregion
                #region "树木值  BWTTag[10]"
                if (BWTTag[10] != "")
                {
                    GI = PG.GridInterval(10);
                    WrapPanel_Left_Character.Children.Add(GI);
                    Grid gDamage = PG.GridInit(15);
                    PropertyBar pbDamage = new PropertyBar();
                    pbDamage.UCTextBlockName.Width = 57;
                    pbDamage.UCTextBlockName.Text = "树木值";
                    pbDamage.UCProgressBar.Value = Convert.ToDouble(BWTTag[10]);
                    pbDamage.UCProgressBar.Foreground = BC.brushConverter(PBCPink);
                    pbDamage.UCTextBlockValue.Width = 32;
                    pbDamage.UCTextBlockValue.Text = BWTTag[10];
                    gDamage.Children.Add(pbDamage);
                    WrapPanel_Left_Character.Children.Add(gDamage);
                }
                #endregion
                #region "伤害  BWTTag[11]"
                if (BWTTag[11] != "")
                {
                    GI = PG.GridInterval(10);
                    WrapPanel_Left_Character.Children.Add(GI);
                    Grid gDamage = PG.GridInit(15);
                    PropertyBar pbDamage = new PropertyBar();
                    pbDamage.UCTextBlockName.Width = 57;
                    pbDamage.UCTextBlockName.Text = "伤害";
                    pbDamage.UCProgressBar.Value = Convert.ToDouble(BWTTag[11]) * 50;
                    pbDamage.UCProgressBar.Foreground = BC.brushConverter(PBCBlue);
                    pbDamage.UCTextBlockValue.Width = 32;
                    if (BWTTag[1] != "海獭伍迪")
                    {
                        pbDamage.UCTextBlockValue.Text = BWTTag[11] + "X";
                    }
                    else
                    {
                        pbDamage.UCTextBlockValue.Text = BWTTag[11];
                    }
                    gDamage.Children.Add(pbDamage);
                    WrapPanel_Left_Character.Children.Add(gDamage);
                }
                #endregion
                #region "伤害(白天)  BWTTag[12]"
                if (BWTTag[12] != "")
                {
                    GI = PG.GridInterval(10);
                    WrapPanel_Left_Character.Children.Add(GI);
                    Grid gDamage = PG.GridInit(15);
                    PropertyBar pbDamage = new PropertyBar();
                    pbDamage.UCTextBlockName.Width = 57;
                    pbDamage.UCTextBlockName.Text = "伤害(白天)";
                    pbDamage.UCProgressBar.Value = Convert.ToDouble(BWTTag[12]) * 2.5;
                    pbDamage.UCProgressBar.Foreground = BC.brushConverter(PBCBlue);
                    pbDamage.UCTextBlockValue.Width = 32;
                    pbDamage.UCTextBlockValue.Text = BWTTag[12];
                    gDamage.Children.Add(pbDamage);
                    WrapPanel_Left_Character.Children.Add(gDamage);
                }
                #endregion
                #region "伤害(黄昏)  BWTTag[13]"
                if (BWTTag[13] != "")
                {
                    GI = PG.GridInterval(10);
                    WrapPanel_Left_Character.Children.Add(GI);
                    Grid gDamage = PG.GridInit(15);
                    PropertyBar pbDamage = new PropertyBar();
                    pbDamage.UCTextBlockName.Width = 57;
                    pbDamage.UCTextBlockName.Text = "伤害(黄昏)";
                    pbDamage.UCProgressBar.Value = Convert.ToDouble(BWTTag[13]) * 2.5;
                    pbDamage.UCProgressBar.Foreground = BC.brushConverter(PBCBlue);
                    pbDamage.UCTextBlockValue.Width = 32;
                    pbDamage.UCTextBlockValue.Text = BWTTag[13];
                    gDamage.Children.Add(pbDamage);
                    WrapPanel_Left_Character.Children.Add(gDamage);
                }
                #endregion
                #region "伤害(夜晚)  BWTTag[14]"
                if (BWTTag[14] != "")
                {
                    GI = PG.GridInterval(10);
                    WrapPanel_Left_Character.Children.Add(GI);
                    Grid gDamage = PG.GridInit(15);
                    PropertyBar pbDamage = new PropertyBar();
                    pbDamage.UCTextBlockName.Width = 57;
                    pbDamage.UCTextBlockName.Text = "伤害(夜晚)";
                    pbDamage.UCProgressBar.Value = Convert.ToDouble(BWTTag[14]) * 2.5;
                    pbDamage.UCProgressBar.Foreground = BC.brushConverter(PBCBlue);
                    pbDamage.UCTextBlockValue.Width = 32;
                    pbDamage.UCTextBlockValue.Text = BWTTag[14];
                    gDamage.Children.Add(pbDamage);
                    WrapPanel_Left_Character.Children.Add(gDamage);
                }
                #endregion
                #region "介绍  BWTTag[15]"
                GI = PG.GridInterval(20);
                WrapPanel_Left_Character.Children.Add(GI);
                Grid gIntroduce = PG.GridInit();
                TextBlock tbIntroduce = new TextBlock();
                tbIntroduce.HorizontalAlignment = HorizontalAlignment.Left;
                tbIntroduce.TextWrapping = TextWrapping.Wrap;
                tbIntroduce.Text = BWTTag[15];
                tbIntroduce.FontSize = 13;
                Thickness TIntroduce = new Thickness();
                TIntroduce.Left = 15;
                tbIntroduce.Margin = TIntroduce;
                gIntroduce.Children.Add(tbIntroduce);
                WrapPanel_Left_Character.Children.Add(gIntroduce);
                #endregion
                GI = PG.GridInterval(20);
                WrapPanel_Left_Character.Children.Add(GI);
                WrapPanel_Left_Character_SizeChanged(null, null);//调整位置
            }
            catch { }
        }

        //沃尔夫冈根据饥饿变化调整属性值
        private void WolfgangSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            List<Grid> grid = new List<Grid>();
            List<PropertyBar> propertyBar = new List<PropertyBar>();
            foreach (UIElement element in WrapPanel_Left_Character.Children)
            {
                if (element.GetType() == typeof(Grid))
                {
                    grid.Add((Grid)element);
                }
            }
            foreach (Grid element in grid)
            {
                foreach (UIElement pb in element.Children)
                {
                    if (pb.GetType() == typeof(PropertyBar))
                    {
                        propertyBar.Add((PropertyBar)pb);
                    }
                }
            }
            Slider s = (Slider)sender;
            int value = (int)(s.Value);
            double dvalue = s.Value;
            propertyBar[1].UCProgressBar.Value = value / 3;
            propertyBar[1].UCTextBlockValue.Text = value.ToString();
            if (value > 220 && value <= 300)
            {
                propertyBar[0].UCProgressBar.Value = ((int)(1.25 * value - 75)) / 3;
                propertyBar[0].UCTextBlockValue.Text = ((int)(1.25 * value - 75)).ToString();
                propertyBar[3].UCProgressBar.Value = 0.46875 * value - 40.625;
                propertyBar[3].UCTextBlockValue.Text = (Math.Round((0.009375 * value - 0.8125), 2)).ToString() + "X";
            }
            else if (value > 100 && value <= 220)
            {
                propertyBar[0].UCProgressBar.Value = 200 / 3;
                propertyBar[0].UCTextBlockValue.Text = "200";
                propertyBar[3].UCProgressBar.Value = 50;
                propertyBar[3].UCTextBlockValue.Text = "1X";
            }
            else if (value >= 0 && value <= 100)
            {
                propertyBar[0].UCProgressBar.Value = ((int)(0.5 * value + 150)) / 3;
                propertyBar[0].UCTextBlockValue.Text = ((int)(0.5 * value + 150)).ToString();
                propertyBar[3].UCProgressBar.Value = 0.125 * value + 25;
                propertyBar[3].UCTextBlockValue.Text = (Math.Round((0.0025 * value + 0.5), 2)).ToString() + "X";
            }
        }

        //WrapPanel_Left_Character内Grid.Width设置为WrapPanel_Left_Character.ActualWidth
        private void WrapPanel_Left_Character_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            foreach (Grid grid in WrapPanel_Left_Character.Children)
            {
                grid.Width = (int)WrapPanel_Left_Character.ActualWidth;
            }
        }
    }
}
