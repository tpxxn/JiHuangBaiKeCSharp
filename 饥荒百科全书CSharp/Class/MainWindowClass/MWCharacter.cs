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
            var button = (Button)sender;
            var bwtTag = (string[])button.Tag;//获取参数
            Character_Click_Handle(bwtTag);
        }
        //WrapPanel_Left_Character控件创建事件
        private void Character_Click_Handle(string[] BWTTag)
        {
            try
            {
                WrapPanelLeftCharacter.Children.Clear();//清空WrapPanel_Left_Character
                Grid GI = new Grid();
                GI = Pg.GridInterval(20);
                WrapPanelLeftCharacter.Children.Add(GI);
                #region "图片 BWTTag[0]"
                Grid gPicture = Pg.GridInit(180);
                Image iPicture = new Image();
                iPicture.Height = 160;
                iPicture.Width = 140;
                iPicture.HorizontalAlignment = HorizontalAlignment.Center;
                iPicture.Source = RSN.PictureShortName(BWTTag[0]);
                gPicture.Children.Add(iPicture);
                WrapPanelLeftCharacter.Children.Add(gPicture);
                #endregion
                GI = Pg.GridInterval(5);
                WrapPanelLeftCharacter.Children.Add(GI);
                #region "中文名  BWTTag[1]"
                Grid gName = Pg.GridInit();
                TextBlock tbName = new TextBlock();
                tbName.HorizontalAlignment = HorizontalAlignment.Center;
                tbName.Text = BWTTag[1];
                tbName.FontSize = 26.667;
                gName.Children.Add(tbName);
                WrapPanelLeftCharacter.Children.Add(gName);
                #endregion
                #region "英文名  BWTTag[2]"
                Grid gEnName = Pg.GridInit();
                TextBlock tbEnName = new TextBlock();
                tbEnName.HorizontalAlignment = HorizontalAlignment.Center;
                tbEnName.Text = BWTTag[2];
                tbEnName.FontSize = 26.667;
                tbEnName.Margin = new Thickness(-5, 0, 0, 0);
                gEnName.Children.Add(tbEnName);
                WrapPanelLeftCharacter.Children.Add(gEnName);
                #endregion
                #region "座右铭  BWTTag[3]"
                if (BWTTag[3] != "")
                {
                    GI = Pg.GridInterval(5);
                    WrapPanelLeftCharacter.Children.Add(GI);
                    Grid gMotto = Pg.GridInit(20);
                    TextBlock tbMotto = new TextBlock();
                    tbMotto.HorizontalAlignment = HorizontalAlignment.Center;
                    tbMotto.Text = BWTTag[3];
                    tbMotto.FontSize = 16;
                    gMotto.Children.Add(tbMotto);
                    WrapPanelLeftCharacter.Children.Add(gMotto);
                }
                #endregion
                #region "描述_1  BWTTag[4]"
                if (BWTTag[4] != "")
                {
                    GI = Pg.GridInterval(10);
                    WrapPanelLeftCharacter.Children.Add(GI);
                    Grid gDescriptions_1 = Pg.GridInit();
                    TextBlock tbDescriptions_1 = new TextBlock();
                    tbDescriptions_1.HorizontalAlignment = HorizontalAlignment.Left;
                    tbDescriptions_1.Text = BWTTag[4];
                    tbDescriptions_1.FontSize = 14;
                    tbDescriptions_1.Margin = new Thickness(25, 0, 0, 0);
                    gDescriptions_1.Children.Add(tbDescriptions_1);
                    WrapPanelLeftCharacter.Children.Add(gDescriptions_1);
                }
                #endregion
                #region "描述_2  BWTTag[5]"
                if (BWTTag[5] != "")
                {
                    Grid gDescriptions_2 = Pg.GridInit();
                    TextBlock tbDescriptions_2 = new TextBlock();
                    tbDescriptions_2.HorizontalAlignment = HorizontalAlignment.Left;
                    tbDescriptions_2.Text = BWTTag[5];
                    tbDescriptions_2.FontSize = 14;
                    tbDescriptions_2.Margin = new Thickness(25, 0, 0, 0);
                    gDescriptions_2.Children.Add(tbDescriptions_2);
                    WrapPanelLeftCharacter.Children.Add(gDescriptions_2);
                }
                #endregion
                #region "描述_3  BWTTag[6]"
                if (BWTTag[6] != "")
                {
                    Grid gDescriptions_3 = Pg.GridInit();
                    TextBlock tbDescriptions_3 = new TextBlock();
                    tbDescriptions_3.HorizontalAlignment = HorizontalAlignment.Left;
                    tbDescriptions_3.Text = BWTTag[6];
                    tbDescriptions_3.FontSize = 14;
                    tbDescriptions_3.Margin = new Thickness(25, 0, 0, 0);
                    gDescriptions_3.Children.Add(tbDescriptions_3);
                    WrapPanelLeftCharacter.Children.Add(gDescriptions_3);
                }
                #endregion
                #region "生命  BWTTag[7]"
                if (BWTTag[7] != "")
                {
                    GI = Pg.GridInterval(25);
                    WrapPanelLeftCharacter.Children.Add(GI);
                    var gHealth = Pg.GridInit(15);
                    var pbHealth = new PropertyBar
                    {
                        UCTextBlockName =
                        {
                            Width = 57,
                            Text = "生命"
                        },
                        UCProgressBar =
                        {
                            Value = Convert.ToDouble(BWTTag[7]) / 3,
                            Foreground = Bc.BrushConverter(PbcGreen)
                        },
                        UCTextBlockValue =
                        {
                            Width = 32,
                            Text = BWTTag[7]
                        }
                    };
                    gHealth.Children.Add(pbHealth);
                    WrapPanelLeftCharacter.Children.Add(gHealth);
                }
                #endregion
                #region "饥饿  BWTTag[8]"
                if (BWTTag[8] != "")
                {
                    GI = Pg.GridInterval(10);
                    WrapPanelLeftCharacter.Children.Add(GI);
                    Grid gHunger = Pg.GridInit(15);
                    PropertyBar pbHunger = new PropertyBar();
                    pbHunger.UCTextBlockName.Width = 57;
                    pbHunger.UCTextBlockName.Text = "饥饿";
                    pbHunger.UCProgressBar.Value = Convert.ToDouble(BWTTag[8]) / 3;
                    pbHunger.UCProgressBar.Foreground = Bc.BrushConverter(PbcOrange);
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
                    WrapPanelLeftCharacter.Children.Add(gHunger);
                }
                #endregion
                #region "精神  BWTTag[9]"
                if (BWTTag[9] != "")
                {
                    GI = Pg.GridInterval(10);
                    WrapPanelLeftCharacter.Children.Add(GI);
                    Grid gSanity = Pg.GridInit(15);
                    PropertyBar pbSanity = new PropertyBar();
                    pbSanity.UCTextBlockName.Width = 57;
                    pbSanity.UCTextBlockName.Text = "精神";
                    pbSanity.UCProgressBar.Value = Convert.ToDouble(BWTTag[9]) / 2.5;
                    pbSanity.UCProgressBar.Foreground = Bc.BrushConverter(PbcRed);
                    pbSanity.UCTextBlockValue.Width = 32;
                    pbSanity.UCTextBlockValue.Text = BWTTag[9];
                    gSanity.Children.Add(pbSanity);
                    WrapPanelLeftCharacter.Children.Add(gSanity);
                }
                #endregion
                #region "树木值  BWTTag[10]"
                if (BWTTag[10] != "")
                {
                    GI = Pg.GridInterval(10);
                    WrapPanelLeftCharacter.Children.Add(GI);
                    Grid gDamage = Pg.GridInit(15);
                    PropertyBar pbDamage = new PropertyBar();
                    pbDamage.UCTextBlockName.Width = 57;
                    pbDamage.UCTextBlockName.Text = "树木值";
                    pbDamage.UCProgressBar.Value = Convert.ToDouble(BWTTag[10]);
                    pbDamage.UCProgressBar.Foreground = Bc.BrushConverter(PbcPink);
                    pbDamage.UCTextBlockValue.Width = 32;
                    pbDamage.UCTextBlockValue.Text = BWTTag[10];
                    gDamage.Children.Add(pbDamage);
                    WrapPanelLeftCharacter.Children.Add(gDamage);
                }
                #endregion
                #region "伤害  BWTTag[11]"
                if (BWTTag[11] != "")
                {
                    GI = Pg.GridInterval(10);
                    WrapPanelLeftCharacter.Children.Add(GI);
                    Grid gDamage = Pg.GridInit(15);
                    PropertyBar pbDamage = new PropertyBar();
                    pbDamage.UCTextBlockName.Width = 57;
                    pbDamage.UCTextBlockName.Text = "伤害";
                    pbDamage.UCProgressBar.Value = Convert.ToDouble(BWTTag[11]) * 50;
                    pbDamage.UCProgressBar.Foreground = Bc.BrushConverter(PbcBlue);
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
                    WrapPanelLeftCharacter.Children.Add(gDamage);
                }
                #endregion
                #region "伤害(白天)  BWTTag[12]"
                if (BWTTag[12] != "")
                {
                    GI = Pg.GridInterval(10);
                    WrapPanelLeftCharacter.Children.Add(GI);
                    Grid gDamage = Pg.GridInit(15);
                    PropertyBar pbDamage = new PropertyBar();
                    pbDamage.UCTextBlockName.Width = 57;
                    pbDamage.UCTextBlockName.Text = "伤害(白天)";
                    pbDamage.UCProgressBar.Value = Convert.ToDouble(BWTTag[12]) * 2.5;
                    pbDamage.UCProgressBar.Foreground = Bc.BrushConverter(PbcBlue);
                    pbDamage.UCTextBlockValue.Width = 32;
                    pbDamage.UCTextBlockValue.Text = BWTTag[12];
                    gDamage.Children.Add(pbDamage);
                    WrapPanelLeftCharacter.Children.Add(gDamage);
                }
                #endregion
                #region "伤害(黄昏)  BWTTag[13]"
                if (BWTTag[13] != "")
                {
                    GI = Pg.GridInterval(10);
                    WrapPanelLeftCharacter.Children.Add(GI);
                    Grid gDamage = Pg.GridInit(15);
                    PropertyBar pbDamage = new PropertyBar();
                    pbDamage.UCTextBlockName.Width = 57;
                    pbDamage.UCTextBlockName.Text = "伤害(黄昏)";
                    pbDamage.UCProgressBar.Value = Convert.ToDouble(BWTTag[13]) * 2.5;
                    pbDamage.UCProgressBar.Foreground = Bc.BrushConverter(PbcBlue);
                    pbDamage.UCTextBlockValue.Width = 32;
                    pbDamage.UCTextBlockValue.Text = BWTTag[13];
                    gDamage.Children.Add(pbDamage);
                    WrapPanelLeftCharacter.Children.Add(gDamage);
                }
                #endregion
                #region "伤害(夜晚)  BWTTag[14]"
                if (BWTTag[14] != "")
                {
                    GI = Pg.GridInterval(10);
                    WrapPanelLeftCharacter.Children.Add(GI);
                    Grid gDamage = Pg.GridInit(15);
                    PropertyBar pbDamage = new PropertyBar();
                    pbDamage.UCTextBlockName.Width = 57;
                    pbDamage.UCTextBlockName.Text = "伤害(夜晚)";
                    pbDamage.UCProgressBar.Value = Convert.ToDouble(BWTTag[14]) * 2.5;
                    pbDamage.UCProgressBar.Foreground = Bc.BrushConverter(PbcBlue);
                    pbDamage.UCTextBlockValue.Width = 32;
                    pbDamage.UCTextBlockValue.Text = BWTTag[14];
                    gDamage.Children.Add(pbDamage);
                    WrapPanelLeftCharacter.Children.Add(gDamage);
                }
                #endregion
                #region "介绍  BWTTag[15]"
                GI = Pg.GridInterval(20);
                WrapPanelLeftCharacter.Children.Add(GI);
                Grid gIntroduce = Pg.GridInit();
                TextBlock tbIntroduce = new TextBlock();
                tbIntroduce.HorizontalAlignment = HorizontalAlignment.Left;
                tbIntroduce.TextWrapping = TextWrapping.Wrap;
                tbIntroduce.Text = BWTTag[15];
                tbIntroduce.FontSize = 13;
                tbIntroduce.Margin = new Thickness(15, 0, 0, 0);
                gIntroduce.Children.Add(tbIntroduce);
                WrapPanelLeftCharacter.Children.Add(gIntroduce);
                #endregion
                GI = Pg.GridInterval(20);
                WrapPanelLeftCharacter.Children.Add(GI);
                WrapPanel_Left_Character_SizeChanged(null, null);//调整位置
            }
            catch { }
        }

        //沃尔夫冈根据饥饿变化调整属性值
        private void WolfgangSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            List<Grid> grid = new List<Grid>();
            List<PropertyBar> propertyBar = new List<PropertyBar>();
            foreach (UIElement element in WrapPanelLeftCharacter.Children)
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
            foreach (Grid grid in WrapPanelLeftCharacter.Children)
            {
                grid.Width = (int)WrapPanelLeftCharacter.ActualWidth;
            }
        }
    }
}
