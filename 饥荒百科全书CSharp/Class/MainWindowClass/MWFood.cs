using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using 饥荒百科全书CSharp.Class;
using 饥荒百科全书CSharp.MyUserControl;

namespace 饥荒百科全书CSharp
{
    /// <summary>
    /// MainWindowFood
    /// </summary>
    public partial class MainWindow : Window
    {
        //Food面板Click事件(食谱)
        private void Food_Recipe_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var bwtTag = (string[])button.Tag;//获取参数
            Food_Recipe_Click_Handle(bwtTag);
        }
        //WrapPanel_Left_Food控件创建事件(食谱)
        private void Food_Recipe_Click_Handle(string[] bwtTag)
        {
            #region "Restrictions初始化"
            var restrictions1 = new List<string>();
            var restrictions2 = new List<string>();
            var preRes = new[] { bwtTag[18], bwtTag[20], bwtTag[22], bwtTag[24], bwtTag[26] };
            var preResAtt = new[] { bwtTag[19], bwtTag[21], bwtTag[23], bwtTag[25], bwtTag[27] };
            var restrictionsAttributes = Adrd.DelRepeatData(preResAtt);
            if (preResAtt[0] == restrictionsAttributes[0] && preRes[0] != "")
            {
                restrictions1.Add(preRes[0]);
            }
            if (preResAtt[1] == restrictionsAttributes[0] && preRes[1] != "")
            {
                restrictions1.Add(preRes[1]);
            }
            if (preResAtt[1] == restrictionsAttributes[1] && preRes[1] != "")
            {
                restrictions2.Add(preRes[1]);
            }
            if (preResAtt[2] == restrictionsAttributes[0] && preRes[2] != "")
            {
                restrictions1.Add(preRes[2]);
            }
            if (preResAtt[2] == restrictionsAttributes[1] && preRes[2] != "")
            {
                restrictions2.Add(preRes[2]);
            }
            if (preResAtt[3] == restrictionsAttributes[0] && preRes[3] != "")
            {
                restrictions1.Add(preRes[3]);
            }
            if (preResAtt[3] == restrictionsAttributes[1] && preRes[3] != "")
            {
                restrictions2.Add(preRes[3]);
            }
            if (preResAtt[4] == restrictionsAttributes[0] && preRes[4] != "")
            {
                restrictions1.Add(preRes[4]);
            }
            if (preResAtt[4] == restrictionsAttributes[1] && preRes[4] != "")
            {
                restrictions2.Add(preRes[4]);
            }
            #endregion
            const string resourceDir = "GameResources/Food/";
            try
            {
                WrapPanelLeftFood.Children.Clear();//清空WrapPanel_Left_Food
                var gi = Pg.GridInterval(10);
                WrapPanelLeftFood.Children.Add(gi);
                #region "图片 BWTTag[0]"
                var gPicture = Pg.GridInit(64);
                var iPicture = new Image
                {
                    Height = 64,
                    Width = 64,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Source = RSN.PictureShortName(bwtTag[0])
                };
                #region "便携式烹饪锅  BWTTag[3]"
                if (bwtTag[3] != "")
                {
                    var iPortableCrockPot = new Image
                    {
                        Height = 32,
                        Width = 32,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Top,
                        Source = RSN.PictureShortName(RSN.ShortName("CP_PortableCrockPot"))
                    };
                    var tPortableCrockPot = new Thickness
                    {
                        Top = 10,
                        Left = 10
                    };
                    iPortableCrockPot.Margin = tPortableCrockPot;
                    gPicture.Children.Add(iPortableCrockPot);
                }
                #endregion
                gPicture.Children.Add(iPicture);
                WrapPanelLeftFood.Children.Add(gPicture);
                #endregion
                gi = Pg.GridInterval(5);
                WrapPanelLeftFood.Children.Add(gi);
                #region "中文名  BWTTag[1]"
                var gName = Pg.GridInit();
                var tbName = new TextBlock
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Text = bwtTag[1],
                    FontSize = 16
                };
                gName.Children.Add(tbName);
                WrapPanelLeftFood.Children.Add(gName);
                #endregion
                #region "英文名  BWTTag[2]"
                var gEnName = Pg.GridInit();
                var tbEnName = new TextBlock
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Text = bwtTag[2],
                    FontSize = 16,
                    Margin = new Thickness(0, -2, 0, 0)
                };
                gEnName.Children.Add(tbEnName);
                WrapPanelLeftFood.Children.Add(gEnName);
                #endregion
                gi = Pg.GridInterval(20);
                WrapPanelLeftFood.Children.Add(gi);
                #region "生命  BWTTag[4]"
                if (bwtTag[4] != "")
                {
                    var propertyHealth = Convert.ToDouble(bwtTag[4]);
                    var gHealth = Pg.GridInit(15);
                    var pbHealth = propertyHealth < 0 ? new PropertyBar(true) : new PropertyBar();
                    pbHealth.UCTextBlockName.Width = 30;
                    pbHealth.UCTextBlockName.Text = "生命";
                    pbHealth.UCProgressBar.Value = Math.Abs(propertyHealth);
                    pbHealth.UCProgressBar.Foreground = Bc.BrushConverter(PbcGreen);
                    pbHealth.UCTextBlockValue.Width = 38;
                    pbHealth.UCTextBlockValue.Text = bwtTag[4];
                    gHealth.Children.Add(pbHealth);
                    WrapPanelLeftFood.Children.Add(gHealth);

                    if (bwtTag[2] == "Jellybeans")
                    {
                        pbHealth.UCTextBlockValue.Text = "2+120";
                        var gHealthJellybeans = Pg.GridInit();
                        var tbJellybeans = new TextBlock()
                        {
                            HorizontalAlignment = HorizontalAlignment.Left,
                            TextWrapping = TextWrapping.Wrap,
                            Text = "食用后立即回复2点生命，之后每2秒回复2点生命，持续2分钟(回复BUFF不叠加)，总共回复122点",
                            Margin = new Thickness(15, 0, 10, 0)
                        };
                        gHealthJellybeans.Children.Add(tbJellybeans);
                        WrapPanelLeftFood.Children.Add(gHealthJellybeans);

                    }
                }
                #endregion
                #region "饥饿  BWTTag[5]"
                if (bwtTag[5] != "")
                {
                    var propertyHunger = Convert.ToDouble(bwtTag[5]);
                    gi = Pg.GridInterval(10);
                    WrapPanelLeftFood.Children.Add(gi);
                    var gHunger = Pg.GridInit(15);
                    var pbHunger = propertyHunger < 0 ? new PropertyBar(true) : new PropertyBar();
                    pbHunger.UCTextBlockName.Width = 30;
                    pbHunger.UCTextBlockName.Text = "饥饿";
                    pbHunger.UCProgressBar.Value = Math.Abs(propertyHunger) / 1.5;
                    pbHunger.UCProgressBar.Foreground = Bc.BrushConverter(PbcOrange);
                    pbHunger.UCTextBlockValue.Width = 38;
                    pbHunger.UCTextBlockValue.Text = bwtTag[5];
                    gHunger.Children.Add(pbHunger);
                    WrapPanelLeftFood.Children.Add(gHunger);
                }
                #endregion
                #region "精神  BWTTag[6]"
                if (bwtTag[6] != "")
                {
                    var propertySanity = Convert.ToDouble(bwtTag[6]);
                    gi = Pg.GridInterval(10);
                    WrapPanelLeftFood.Children.Add(gi);
                    var gSanity = Pg.GridInit(15);
                    var pbSanity = propertySanity < 0 ? new PropertyBar(true) : new PropertyBar();
                    pbSanity.UCTextBlockName.Width = 30;
                    pbSanity.UCTextBlockName.Text = "精神";
                    pbSanity.UCProgressBar.Value = Math.Abs(propertySanity) / 0.5;
                    pbSanity.UCProgressBar.Foreground = Bc.BrushConverter(PbcRed);
                    pbSanity.UCTextBlockValue.Width = 38;
                    pbSanity.UCTextBlockValue.Text = bwtTag[6];
                    gSanity.Children.Add(pbSanity);
                    WrapPanelLeftFood.Children.Add(gSanity);
                }
                #endregion
                #region "保鲜  BWTTag[7]"
                if (bwtTag[7] != "")
                {
                    var propertyPerish = Convert.ToDouble(bwtTag[7]);
                    gi = Pg.GridInterval(10);
                    WrapPanelLeftFood.Children.Add(gi);
                    var gPerish = Pg.GridInit(15);
                    var pbPerish = propertyPerish < 0 ? new PropertyBar(true) : new PropertyBar();
                    pbPerish.UCTextBlockName.Width = 30;
                    pbPerish.UCTextBlockName.Text = "保鲜";
                    pbPerish.UCProgressBar.Value = Math.Abs(propertyPerish) / 0.2;
                    pbPerish.UCProgressBar.Foreground = Bc.BrushConverter(PbcBlue);
                    pbPerish.UCTextBlockValue.Width = 38;
                    pbPerish.UCTextBlockValue.Text = propertyPerish == 1000 ? "∞" : bwtTag[7];
                    gPerish.Children.Add(pbPerish);
                    WrapPanelLeftFood.Children.Add(gPerish);
                }
                #endregion
                #region "烹饪  BWTTag[8]"
                if (bwtTag[8] != "")
                {
                    var propertyCooktime = Convert.ToDouble(bwtTag[8]);
                    gi = Pg.GridInterval(10);
                    WrapPanelLeftFood.Children.Add(gi);
                    var gCooktime = Pg.GridInit(15);
                    var pbCooktime = propertyCooktime < 0 ? new PropertyBar(true) : new PropertyBar();
                    pbCooktime.UCTextBlockName.Width = 30;
                    pbCooktime.UCTextBlockName.Text = "烹饪";
                    pbCooktime.UCProgressBar.Value = Math.Abs(propertyCooktime) / 0.6;
                    pbCooktime.UCProgressBar.Foreground = Bc.BrushConverter(PbcPurple);
                    pbCooktime.UCTextBlockValue.Width = 38;
                    pbCooktime.UCTextBlockValue.Text = bwtTag[8];
                    gCooktime.Children.Add(pbCooktime);
                    WrapPanelLeftFood.Children.Add(gCooktime);
                }
                #endregion
                #region "优先  BWTTag[9]"
                if (bwtTag[9] != "")
                {
                    var propertyPriority = Convert.ToDouble(bwtTag[9]);
                    gi = Pg.GridInterval(10);
                    WrapPanelLeftFood.Children.Add(gi);
                    var gPriority = Pg.GridInit(15);
                    var pbPriority = propertyPriority < 0 ? new PropertyBar(true) : new PropertyBar();
                    pbPriority.UCTextBlockName.Width = 30;
                    pbPriority.UCTextBlockName.Text = "优先";
                    pbPriority.UCProgressBar.Value = Math.Abs(propertyPriority) / 0.3;
                    pbPriority.UCProgressBar.Foreground = Bc.BrushConverter(PbcPink);
                    pbPriority.UCTextBlockValue.Width = 38;
                    pbPriority.UCTextBlockValue.Text = bwtTag[9];
                    gPriority.Children.Add(pbPriority);
                    WrapPanelLeftFood.Children.Add(gPriority);
                }
                #endregion
                gi = Pg.GridInterval(20);
                WrapPanelLeftFood.Children.Add(gi);
                if (bwtTag[1] != "湿腻焦糊")
                {
                    #region "烹饪需求  BWTTag[10-17]"
                    WrapPanelLeftFood.Children.Add(Pg.GridTag("烹饪需求："));
                    var sNeed = new StackPanel {HorizontalAlignment = HorizontalAlignment.Center};
                    var wNeed1 = new WrapPanel();
                    var wNeed2 = new WrapPanel();
                    var wNeed3 = new WrapPanel();
                    if (bwtTag[10] != "")
                    {
                        var bwpNeed1 = new ButtonWithPicture(bwtTag[10], resourceDir) {UCButton = {Tag = bwtTag[10]}};
                        bwpNeed1.UCButton.Click += Food_Jump_Click;
                        wNeed1.Children.Add(bwpNeed1);
                        var tbNeed1 = new TextBlock
                        {
                            Text = bwtTag[11],
                            Padding = new Thickness(0, 8, 0, 0)
                        };
                        wNeed1.Children.Add(tbNeed1);
                        sNeed.Children.Add(wNeed1);
                    }
                    if (bwtTag[12] != "")
                    {
                        var bwpNeedOr = new ButtonWithPicture(bwtTag[12], resourceDir) { UCButton = { Tag = bwtTag[12] } };
                        bwpNeedOr.UCButton.Click += Food_Jump_Click;
                        wNeed1.Children.Add(bwpNeedOr);
                        var tbNeedOr = new TextBlock
                        {
                            Text = bwtTag[13],
                            Padding = new Thickness(0, 8, 0, 0)
                        };
                        if (bwtTag[2] != "Monster Lasagna")
                        {
                            wNeed1.Children.Add(tbNeedOr);
                        }
                        else
                        {
                            if (UiGameversion.SelectedIndex == 2)
                            {
                                wNeed1.Children.Add(tbNeedOr);
                            }
                        }
                    }
                    if (bwtTag[14] != "")
                    {
                        var bwpNeed2 = new ButtonWithPicture(bwtTag[14], resourceDir) { UCButton = { Tag = bwtTag[14] } };
                        bwpNeed2.UCButton.Click += Food_Jump_Click;
                        var tbNeed2 = new TextBlock
                        {
                            Text = bwtTag[15],
                            Padding = new Thickness(0, 8, 0, 0)
                        };
                        if (bwtTag[2] != "Monster Lasagna")
                        {
                            wNeed2.Children.Add(bwpNeed2);
                            wNeed2.Children.Add(tbNeed2);
                            sNeed.Children.Add(wNeed2);
                        }
                        else
                        {
                            if (UiGameversion.SelectedIndex == 2)
                            {
                                wNeed1.Children.Add(bwpNeed2);
                            }
                                wNeed1.Children.Add(tbNeed2);
                        }
                    }
                    if (bwtTag[16] != "")
                    {
                        var bwpNeed3 = new ButtonWithPicture(bwtTag[16], resourceDir) {UCButton = {Tag = bwtTag[16]}};
                        bwpNeed3.UCButton.Click += Food_Jump_Click;
                        wNeed3.Children.Add(bwpNeed3);
                        var tbNeed3 = new TextBlock
                        {
                            Text = bwtTag[17],
                            Padding = new Thickness(0, 8, 0, 0)
                        };
                        wNeed3.Children.Add(tbNeed3);
                        sNeed.Children.Add(wNeed3);
                    }
                    WrapPanelLeftFood.Children.Add(sNeed);
                    #endregion
                    #region "填充限制  BWTTag[18-27]"
                    if (bwtTag[18] != "")
                    {
                        gi = Pg.GridInterval(20);
                        WrapPanelLeftFood.Children.Add(gi);
                        WrapPanelLeftFood.Children.Add(Pg.GridTag("填充限制："));
                        var sRestrictions = new StackPanel {HorizontalAlignment = HorizontalAlignment.Center};
                        var wRestrictions1 = new WrapPanel();
                        var wRestrictions2 = new WrapPanel();
                        if (restrictions1.Count != 0)
                        {
                            var tbRestrictions1 = new TextBlock
                            {
                                Text = restrictionsAttributes[0],
                                Padding = new Thickness(0, 8, 0, 0)
                            };
                            wRestrictions1.Children.Add(tbRestrictions1);
                            foreach (var str in restrictions1)
                            {
                                var bwp = new ButtonWithPicture(str, resourceDir) {UCButton = {Tag = str}};
                                bwp.UCButton.Click += Food_Jump_Click;
                                wRestrictions1.Children.Add(bwp);
                            }
                            sRestrictions.Children.Add(wRestrictions1);
                        }
                        if (restrictions2.Count != 0)
                        {
                            var tbRestrictions2 = new TextBlock
                            {
                                Text = restrictionsAttributes[1],
                                Padding = new Thickness(0, 8, 0, 0)
                            };
                            wRestrictions2.Children.Add(tbRestrictions2);
                            foreach (var str in restrictions2)
                            {
                                var bwp = new ButtonWithPicture(str, resourceDir) {UCButton = {Tag = str}};
                                bwp.UCButton.Click += Food_Jump_Click;
                                wRestrictions2.Children.Add(bwp);
                            }
                            sRestrictions.Children.Add(wRestrictions2);
                        }
                        WrapPanelLeftFood.Children.Add(sRestrictions);
                    }
                    #endregion
                    gi = Pg.GridInterval(20);
                    WrapPanelLeftFood.Children.Add(gi);
                    #region "推荐食谱 BWTTag[28-31]"
                    WrapPanelLeftFood.Children.Add(Pg.GridTag("推荐食谱："));
                    var gRecommendContent = Pg.GridInit(35);
                    var sRecommendContent = new WrapPanel {HorizontalAlignment = HorizontalAlignment.Center};
                    var bwpRecommed1 = new ButtonWithPicture(bwtTag[28], resourceDir) {UCButton = {Tag = bwtTag[28]}};
                    bwpRecommed1.UCButton.Click += Food_Jump_Click;
                    var bwpRecommed2 = new ButtonWithPicture(bwtTag[29], resourceDir) {UCButton = {Tag = bwtTag[29]}};
                    bwpRecommed2.UCButton.Click += Food_Jump_Click;
                    var bwpRecommed3 = new ButtonWithPicture(bwtTag[30], resourceDir) {UCButton = {Tag = bwtTag[30]}};
                    bwpRecommed3.UCButton.Click += Food_Jump_Click;
                    var bwpRecommed4 = new ButtonWithPicture(bwtTag[31], resourceDir) {UCButton = {Tag = bwtTag[31]}};
                    bwpRecommed4.UCButton.Click += Food_Jump_Click;
                    sRecommendContent.Children.Add(bwpRecommed1);
                    sRecommendContent.Children.Add(bwpRecommed2);
                    sRecommendContent.Children.Add(bwpRecommed3);
                    sRecommendContent.Children.Add(bwpRecommed4);
                    gRecommendContent.Children.Add(sRecommendContent);
                    WrapPanelLeftFood.Children.Add(gRecommendContent);
                    #endregion
                }
                gi = Pg.GridInterval(20);
                WrapPanelLeftFood.Children.Add(gi);
                #region "介绍  BWTTag[32]"
                var gIntroduce = Pg.GridInit();
                var tbIntroduce = new TextBlock
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    TextWrapping = TextWrapping.Wrap,
                    Text = bwtTag[32],
                    FontSize = 13
                };
                var tIntroduce = new Thickness {Left = 15};
                tbIntroduce.Margin = tIntroduce;
                gIntroduce.Children.Add(tbIntroduce);
                WrapPanelLeftFood.Children.Add(gIntroduce);
                #endregion
                gi = Pg.GridInterval(20);
                WrapPanelLeftFood.Children.Add(gi);
            }
            catch
            {
                // ignored
            }
            WrapPanel_Left_Food_SizeChanged(null, null);//调整位置
        }

        //Food面板Click事件(食材)
        private void Food_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var bwtTag = (string[])button.Tag;//获取参数
            Food_Click_Handle(bwtTag);
        }
        //WrapPanel_Left_Food控件创建事件(食材)
        private void Food_Click_Handle(IReadOnlyList<string> bwtTag)
        {
            const string resourceDir = "GameResources/Food/";
            try
            {
                WrapPanelLeftFood.Children.Clear();//清空WrapPanel_Left_Food
                var gi = Pg.GridInterval(10);
                WrapPanelLeftFood.Children.Add(gi);
                #region "图片 BWTTag[0]"
                var gPicture = Pg.GridInit(64);
                var iPicture = new Image
                {
                    Height = 64,
                    Width = 64,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Source = RSN.PictureShortName(bwtTag[0])
                };
                gPicture.Children.Add(iPicture);
                WrapPanelLeftFood.Children.Add(gPicture);
                #endregion
                gi = Pg.GridInterval(5);
                WrapPanelLeftFood.Children.Add(gi);
                #region "中文名  BWTTag[1]"
                var gName = Pg.GridInit();
                var tbName = new TextBlock
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Text = bwtTag[1],
                    FontSize = 16
                };
                gName.Children.Add(tbName);
                WrapPanelLeftFood.Children.Add(gName);
                #endregion
                #region "英文名  BWTTag[2]"
                var gEnName = Pg.GridInit();
                var tbEnName = new TextBlock
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Text = bwtTag[2],
                    FontSize = 16,
                    Margin = new Thickness(0, -2, 0, 0)
                };
                gEnName.Children.Add(tbEnName);
                WrapPanelLeftFood.Children.Add(gEnName);
                #endregion
                gi = Pg.GridInterval(20);
                WrapPanelLeftFood.Children.Add(gi);
                #region "生命  BWTTag[3]"
                if (bwtTag[3] != "")
                {
                    var propertyHealth = Convert.ToDouble(bwtTag[3]);
                    var gHealth = Pg.GridInit(15);
                    var pbHealth = propertyHealth < 0 ? new PropertyBar(true) : new PropertyBar();
                    pbHealth.UCTextBlockName.Width = 30;
                    pbHealth.UCTextBlockName.Text = "生命";
                    pbHealth.UCProgressBar.Value = Math.Abs(propertyHealth);
                    pbHealth.UCProgressBar.Foreground = Bc.BrushConverter(PbcGreen);
                    pbHealth.UCTextBlockValue.Width = 39;
                    pbHealth.UCTextBlockValue.Text = bwtTag[3];
                    gHealth.Children.Add(pbHealth);
                    WrapPanelLeftFood.Children.Add(gHealth);
                }
                #endregion
                #region "饥饿  BWTTag[4]"
                if (bwtTag[4] != "")
                {
                    var propertyHunger = Convert.ToDouble(bwtTag[4]);
                    gi = Pg.GridInterval(10);
                    WrapPanelLeftFood.Children.Add(gi);
                    var gHunger = Pg.GridInit(15);
                    var pbHunger = propertyHunger < 0 ? new PropertyBar(true) : new PropertyBar();
                    pbHunger.UCTextBlockName.Width = 30;
                    pbHunger.UCTextBlockName.Text = "饥饿";
                    pbHunger.UCProgressBar.Value = Math.Abs(propertyHunger) / 1.5;
                    pbHunger.UCProgressBar.Foreground = Bc.BrushConverter(PbcOrange);
                    pbHunger.UCTextBlockValue.Width = 39;
                    pbHunger.UCTextBlockValue.Text = bwtTag[4];
                    gHunger.Children.Add(pbHunger);
                    WrapPanelLeftFood.Children.Add(gHunger);
                }
                #endregion
                #region "精神  BWTTag[5]"
                if (bwtTag[5] != "")
                {
                    var propertySanity = Convert.ToDouble(bwtTag[5]);
                    gi = Pg.GridInterval(10);
                    WrapPanelLeftFood.Children.Add(gi);
                    var gSanity = Pg.GridInit(15);
                    var pbSanity = propertySanity < 0 ? new PropertyBar(true) : new PropertyBar();
                    pbSanity.UCTextBlockName.Width = 30;
                    pbSanity.UCTextBlockName.Text = "精神";
                    pbSanity.UCProgressBar.Value = Math.Abs(propertySanity) / 0.5;
                    pbSanity.UCProgressBar.Foreground = Bc.BrushConverter(PbcRed);
                    pbSanity.UCTextBlockValue.Width = 39;
                    pbSanity.UCTextBlockValue.Text = bwtTag[5];
                    gSanity.Children.Add(pbSanity);
                    WrapPanelLeftFood.Children.Add(gSanity);
                }
                #endregion
                #region "保鲜  BWTTag[6]"
                if (bwtTag[6] != "")
                {
                    var propertyPerish = Convert.ToDouble(bwtTag[6]);
                    gi = Pg.GridInterval(10);
                    WrapPanelLeftFood.Children.Add(gi);
                    var gPerish = Pg.GridInit(15);
                    var pbPerish = propertyPerish < 0 ? new PropertyBar(true) : new PropertyBar();
                    pbPerish.UCTextBlockName.Width = 30;
                    pbPerish.UCTextBlockName.Text = "保鲜";
                    pbPerish.UCProgressBar.Value = Math.Abs(propertyPerish) / 0.4;
                    pbPerish.UCProgressBar.Foreground = Bc.BrushConverter(PbcBlue);
                    pbPerish.UCTextBlockValue.Width = 39;
                    pbPerish.UCTextBlockValue.Text = propertyPerish == 1000 ? "∞" : bwtTag[6];
                    gPerish.Children.Add(pbPerish);
                    WrapPanelLeftFood.Children.Add(gPerish);
                }
                #endregion
                gi = Pg.GridInterval(20);
                WrapPanelLeftFood.Children.Add(gi);
                #region "食物属性  BWTTag[7-10]"
                WrapPanelLeftFood.Children.Add(Pg.GridTag("食物属性："));
                var sAttribute = new StackPanel {HorizontalAlignment = HorizontalAlignment.Center};
                var wAttribute = new WrapPanel();
                if (bwtTag[7] != "")
                {
                    var bwpAttribute1 =
                        new ButtonWithPicture(bwtTag[7], resourceDir) {UCButton = {Tag = bwtTag[7]}};
                    bwpAttribute1.UCButton.Click += Food_Jump_Click;
                    wAttribute.Children.Add(bwpAttribute1);
                    var tbAttribute1 = new TextBlock
                    {
                        Text = bwtTag[8],
                        Padding = new Thickness(0, 8, 0, 0)
                    };
                    wAttribute.Children.Add(tbAttribute1);
                }
                else if (bwtTag[2] == "Roasted Birchnut")
                {
                    var tbAttribute1 = new TextBlock
                    {
                        Text = "填充物×1",
                        Padding = new Thickness(0, 8, 0, 0)
                    };
                    wAttribute.Children.Add(tbAttribute1);
                }
                if (bwtTag[9] != "")
                {
                    var bwpAttribute2 = new ButtonWithPicture(bwtTag[9], resourceDir) {UCButton = {Tag = bwtTag[9]}};
                    bwpAttribute2.UCButton.Click += Food_Jump_Click;
                    wAttribute.Children.Add(bwpAttribute2);
                    var tbAttribute2 = new TextBlock
                    {
                        Text = bwtTag[10],
                        Padding = new Thickness(0, 8, 0, 0)
                    };
                    wAttribute.Children.Add(tbAttribute2);
                }
                sAttribute.Children.Add(wAttribute);
                WrapPanelLeftFood.Children.Add(sAttribute);
                #endregion
                gi = Pg.GridInterval(20);
                WrapPanelLeftFood.Children.Add(gi);
                #region "介绍  BWTTag[11]"
                var gIntroduce = Pg.GridInit();
                var tbIntroduce = new TextBlock
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    TextWrapping = TextWrapping.Wrap,
                    Text = bwtTag[11],
                    FontSize = 13
                };
                var introduce = new Thickness {Left = 15};
                tbIntroduce.Margin = introduce;
                gIntroduce.Children.Add(tbIntroduce);
                WrapPanelLeftFood.Children.Add(gIntroduce);
                #endregion
                gi = Pg.GridInterval(20);
                WrapPanelLeftFood.Children.Add(gi);
            }
            catch
            {
                // ignored
            }
            WrapPanel_Left_Food_SizeChanged(null, null);//调整位置
        }

        //Food面板Click事件(非食材)
        private void Food_NoFC_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var bwtTag = (string[])button.Tag;//获取参数
            Food_NoFC_Click_Handle(bwtTag);
        }
        //WrapPanel_Left_Food控件创建事件(非食材)
        private void Food_NoFC_Click_Handle(IReadOnlyList<string> bwtTag)
        {
            try
            {
                WrapPanelLeftFood.Children.Clear();//清空WrapPanel_Left_Food
                var gi = Pg.GridInterval(10);
                WrapPanelLeftFood.Children.Add(gi);
                #region "图片 BWTTag[0]"
                Grid gPicture = Pg.GridInit(64);
                Image iPicture = new Image
                {
                    Height = 64,
                    Width = 64,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Source = RSN.PictureShortName(bwtTag[0])
                };
                gPicture.Children.Add(iPicture);
                WrapPanelLeftFood.Children.Add(gPicture);
                #endregion
                gi = Pg.GridInterval(5);
                WrapPanelLeftFood.Children.Add(gi);
                #region "中文名  BWTTag[1]"
                Grid gName = Pg.GridInit();
                TextBlock tbName = new TextBlock
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Text = bwtTag[1],
                    FontSize = 16
                };
                gName.Children.Add(tbName);
                WrapPanelLeftFood.Children.Add(gName);
                #endregion
                #region "英文名  BWTTag[2]"
                Grid gEnName = Pg.GridInit();
                TextBlock tbEnName = new TextBlock
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Text = bwtTag[2],
                    FontSize = 16,
                    Margin = new Thickness(0, -2, 0, 0)
                };
                gEnName.Children.Add(tbEnName);
                WrapPanelLeftFood.Children.Add(gEnName);
                #endregion
                gi = Pg.GridInterval(20);
                WrapPanelLeftFood.Children.Add(gi);
                #region "生命  BWTTag[3]"
                if (bwtTag[3] != "")
                {
                    double propertyHealth = Convert.ToDouble(bwtTag[3]);
                    Grid gHealth = Pg.GridInit(15);
                    var pbHealth = propertyHealth < 0 ? new PropertyBar(true) : new PropertyBar();
                    pbHealth.UCTextBlockName.Width = 30;
                    pbHealth.UCTextBlockName.Text = "生命";
                    pbHealth.UCProgressBar.Value = Math.Abs(propertyHealth);
                    pbHealth.UCProgressBar.Foreground = Bc.BrushConverter(PbcGreen);
                    pbHealth.UCTextBlockValue.Width = 39;
                    pbHealth.UCTextBlockValue.Text = bwtTag[3];
                    gHealth.Children.Add(pbHealth);
                    WrapPanelLeftFood.Children.Add(gHealth);
                }
                #endregion
                #region "饥饿  BWTTag[4]"
                if (bwtTag[4] != "")
                {
                    double propertyHunger = Convert.ToDouble(bwtTag[4]);
                    gi = Pg.GridInterval(10);
                    WrapPanelLeftFood.Children.Add(gi);
                    Grid gHunger = Pg.GridInit(15);
                    var pbHunger = propertyHunger < 0 ? new PropertyBar(true) : new PropertyBar();
                    pbHunger.UCTextBlockName.Width = 30;
                    pbHunger.UCTextBlockName.Text = "饥饿";
                    pbHunger.UCProgressBar.Value = Math.Abs(propertyHunger) / 1.5;
                    pbHunger.UCProgressBar.Foreground = Bc.BrushConverter(PbcOrange);
                    pbHunger.UCTextBlockValue.Width = 39;
                    pbHunger.UCTextBlockValue.Text = bwtTag[4];
                    gHunger.Children.Add(pbHunger);
                    WrapPanelLeftFood.Children.Add(gHunger);
                }
                #endregion
                #region "精神  BWTTag[5]"
                if (bwtTag[5] != "")
                {
                    double propertySanity = Convert.ToDouble(bwtTag[5]);
                    gi = Pg.GridInterval(10);
                    WrapPanelLeftFood.Children.Add(gi);
                    Grid gSanity = Pg.GridInit(15);
                    var pbSanity = propertySanity < 0 ? new PropertyBar(true) : new PropertyBar();
                    pbSanity.UCTextBlockName.Width = 30;
                    pbSanity.UCTextBlockName.Text = "精神";
                    pbSanity.UCProgressBar.Value = Math.Abs(propertySanity) / 0.5;
                    pbSanity.UCProgressBar.Foreground = Bc.BrushConverter(PbcRed);
                    pbSanity.UCTextBlockValue.Width = 39;
                    pbSanity.UCTextBlockValue.Text = bwtTag[5];
                    gSanity.Children.Add(pbSanity);
                    WrapPanelLeftFood.Children.Add(gSanity);
                }
                #endregion
                #region "保鲜  BWTTag[6]"
                if (bwtTag[6] != "")
                {
                    double propertyPerish = Convert.ToDouble(bwtTag[6]);
                    gi = Pg.GridInterval(10);
                    WrapPanelLeftFood.Children.Add(gi);
                    Grid gPerish = Pg.GridInit(15);
                    PropertyBar pbPerish;
                    if (propertyPerish < 0)
                        pbPerish = new PropertyBar(true);
                    else
                        pbPerish = new PropertyBar();
                    pbPerish.UCTextBlockName.Width = 30;
                    pbPerish.UCTextBlockName.Text = "保鲜";
                    pbPerish.UCProgressBar.Value = Math.Abs(propertyPerish) / 0.4;
                    pbPerish.UCProgressBar.Foreground = Bc.BrushConverter(PbcBlue);
                    pbPerish.UCTextBlockValue.Width = 39;
                    if (propertyPerish == 1000)
                        pbPerish.UCTextBlockValue.Text = "∞";
                    else
                        pbPerish.UCTextBlockValue.Text = bwtTag[6];
                    gPerish.Children.Add(pbPerish);
                    WrapPanelLeftFood.Children.Add(gPerish);
                }
                #endregion
                #region "食物属性  BWTTag[7]"
                if (bwtTag[7] != "")
                {
                    gi = Pg.GridInterval(20);
                    WrapPanelLeftFood.Children.Add(gi);
                    string foodAttribute = "食物属性：" + bwtTag[7];
                    WrapPanelLeftFood.Children.Add(Pg.GridTag(foodAttribute));
                }
                #endregion
                gi = Pg.GridInterval(20);
                WrapPanelLeftFood.Children.Add(gi);
                #region "介绍  BWTTag[8]"
                Grid gIntroduce = Pg.GridInit();
                TextBlock tbIntroduce = new TextBlock();
                tbIntroduce.HorizontalAlignment = HorizontalAlignment.Left;
                tbIntroduce.TextWrapping = TextWrapping.Wrap;
                tbIntroduce.Text = bwtTag[8];
                tbIntroduce.FontSize = 13;
                Thickness introduce = new Thickness();
                introduce.Left = 15;
                tbIntroduce.Margin = introduce;
                gIntroduce.Children.Add(tbIntroduce);
                WrapPanelLeftFood.Children.Add(gIntroduce);
                #endregion
                gi = Pg.GridInterval(20);
                WrapPanelLeftFood.Children.Add(gi);
            }
            catch
            {
                // ignored
            }
            WrapPanel_Left_Food_SizeChanged(null, null);//调整位置
        }

        //跳转按钮事件
        private void Food_Jump_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string bwtTag = (string)button.Tag;
            Food_Jump_Click_Handle(sender, bwtTag);
        }

        private void Food_Jump_Click_Handle(object sender, string bwtTag)
        {
            //获取前缀(F_或FC)
            var prefix = bwtTag.Substring(0, 2);

            if (prefix == "FC")
            {
                WrapPanelFoodAttribute.Children.Clear();

                var esMeatsP5 = new ExpanderStackpanel("肉类×0.5", "../Resources/GameResources/Food/FC_Meats.png") { Width = 185 };
                var esMeats1 = new ExpanderStackpanel("肉类×1", "../Resources/GameResources/Food/FC_Meats.png") { Width = 185 };
                var esMonsterMeats = new ExpanderStackpanel("怪兽类×1", "../Resources/GameResources/Food/FC_Monster_Meats.png") { Width = 185 };
                var esFishesP5 = new ExpanderStackpanel("鱼类×0.5", "../Resources/GameResources/Food/FC_Fishes.png") { Width = 185 };
                var esFishes1 = new ExpanderStackpanel("鱼类×1", "../Resources/GameResources/Food/FC_Fishes.png") { Width = 185 };
                var esFishes2 = new ExpanderStackpanel("鱼类×2", "../Resources/GameResources/Food/FC_Fishes.png") { Width = 185 };
                var esVegetablesP5 = new ExpanderStackpanel("蔬菜类×0.5", "../Resources/GameResources/Food/FC_Vegetables.png") { Width = 185 };
                var esVegetables1 = new ExpanderStackpanel("蔬菜类×1", "../Resources/GameResources/Food/FC_Vegetables.png") { Width = 185 };
                var esFruitP5 = new ExpanderStackpanel("水果类×0.5", "../Resources/GameResources/Food/FC_Fruit.png") { Width = 185 };
                var esFruit1 = new ExpanderStackpanel("水果类×1", "../Resources/GameResources/Food/FC_Fruit.png") { Width = 185 };
                var esEggs1 = new ExpanderStackpanel("蛋类×1", "../Resources/GameResources/Food/FC_Eggs.png") { Width = 185 };
                var esEggs4 = new ExpanderStackpanel("蛋类×4", "../Resources/GameResources/Food/FC_Eggs.png") { Width = 185 };
                var esDairyProduct = new ExpanderStackpanel("乳制品类×1", "../Resources/GameResources/Food/FC_Dairy_product.png") { Width = 185 };
                var esSweetener = new ExpanderStackpanel("甜味剂类×1", "../Resources/GameResources/Food/FC_Sweetener.png") { Width = 185 };
                var esJellyfish = new ExpanderStackpanel("水母类×1", "../Resources/GameResources/Food/FC_Jellyfish.png") { Width = 185 };

                foreach (UIElement expanderStackpanel in WrapPanelRightFood.Children)
                {
                    foreach (UIElement buttonWithText in ((ExpanderStackpanel)expanderStackpanel).UcWrapPanel.Children)
                    {
                        var rightButtonTag = (string[])(((ButtonWithText)buttonWithText).UCButton.Tag);
                        if (rightButtonTag.Length > 10)
                        {
                            var rightButtonTag0 = rightButtonTag[0];
                            rightButtonTag0 = rightButtonTag0.Substring(rightButtonTag0.LastIndexOf('/') + 1, rightButtonTag0.Length - rightButtonTag0.LastIndexOf('/') - 5);
                            var attribute = rightButtonTag[7];
                            var attributeValue = rightButtonTag[8];
                            var attribute2 = rightButtonTag[9];
                            var attributeValue2 = rightButtonTag[10];
                            if (attribute.Length > 2)
                            {
                                if (attribute.Substring(0, 2) == "FC")
                                {
                                    var bwp = new ButtonWithPicture(rightButtonTag0, "GameResources/Food/")
                                        {
                                            UCButton = {Tag = rightButtonTag0}
                                        };
                                    bwp.UCButton.Click += FoodAttribute_Click;
                                    switch (attribute)
                                    {
                                        case "FC_Meats":
                                            if (attributeValue == "×0.5")
                                                esMeatsP5.UcWrapPanel.Children.Add(bwp);
                                            else
                                                esMeats1.UcWrapPanel.Children.Add(bwp);
                                            break;
                                        case "FC_Monster_Meats":
                                            esMonsterMeats.UcWrapPanel.Children.Add(bwp);
                                            break;
                                        case "FC_Fishes":
                                            if (attributeValue == "×0.5")
                                                esFishesP5.UcWrapPanel.Children.Add(bwp);
                                            else if (attributeValue == "×1")
                                                esFishes1.UcWrapPanel.Children.Add(bwp);
                                            else
                                                esFishes2.UcWrapPanel.Children.Add(bwp);
                                            break;
                                        case "FC_Vegetables":
                                            if (attributeValue == "×0.5")
                                                esVegetablesP5.UcWrapPanel.Children.Add(bwp);
                                            else
                                                esVegetables1.UcWrapPanel.Children.Add(bwp);
                                            break;
                                        case "FC_Fruit":
                                            if (attributeValue == "×0.5")
                                                esFruitP5.UcWrapPanel.Children.Add(bwp);
                                            else
                                                esFruit1.UcWrapPanel.Children.Add(bwp);
                                            break;
                                        case "FC_Eggs":
                                            if (attributeValue == "×1")
                                                esEggs1.UcWrapPanel.Children.Add(bwp);
                                            else
                                                esEggs4.UcWrapPanel.Children.Add(bwp);
                                            break;
                                        case "FC_Dairy_product":
                                            esDairyProduct.UcWrapPanel.Children.Add(bwp);
                                            break;
                                        case "FC_Sweetener":
                                            esSweetener.UcWrapPanel.Children.Add(bwp);
                                            break;
                                    }
                                }
                            }

                            if (attribute2.Length > 2)
                            {
                                if (attribute2.Substring(0, 2) == "FC")
                                {
                                    ButtonWithPicture bwp = new ButtonWithPicture(rightButtonTag0, "GameResources/Food/");
                                    bwp.UCButton.Tag = rightButtonTag0;
                                    bwp.UCButton.Click += FoodAttribute_Click;
                                    switch (attribute2)
                                    {
                                        case "FC_Monster_Meats":
                                            esMonsterMeats.UcWrapPanel.Children.Add(bwp);
                                            break;
                                        case "FC_Fishes":
                                            if (attributeValue2 == "×0.5")
                                                esFishesP5.UcWrapPanel.Children.Add(bwp);
                                            else if (attributeValue2 == "×1")
                                                esFishes1.UcWrapPanel.Children.Add(bwp);
                                            else
                                                esFishes2.UcWrapPanel.Children.Add(bwp);
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }

                WrapPanelFoodAttribute.Children.Add(esMeatsP5);
                WrapPanelFoodAttribute.Children.Add(esMeats1);
                WrapPanelFoodAttribute.Children.Add(esMonsterMeats);
                if (esFishesP5.UcWrapPanel.Children.Count != 0)
                    WrapPanelFoodAttribute.Children.Add(esFishesP5);
                WrapPanelFoodAttribute.Children.Add(esFishes1);
                if (esFishes2.UcWrapPanel.Children.Count != 0)
                    WrapPanelFoodAttribute.Children.Add(esFishes2);
                WrapPanelFoodAttribute.Children.Add(esVegetablesP5);
                WrapPanelFoodAttribute.Children.Add(esVegetables1);
                WrapPanelFoodAttribute.Children.Add(esFruitP5);
                WrapPanelFoodAttribute.Children.Add(esFruit1);
                WrapPanelFoodAttribute.Children.Add(esEggs1);
                WrapPanelFoodAttribute.Children.Add(esEggs4);
                WrapPanelFoodAttribute.Children.Add(esDairyProduct);
                WrapPanelFoodAttribute.Children.Add(esSweetener);
                if (UiGameversion.SelectedIndex == 2)
                {
                    ButtonWithPicture bwpJellyfish = new ButtonWithPicture("F_jellyfish", "GameResources/Food/");
                    bwpJellyfish.UCButton.Tag = "F_jellyfish";
                    bwpJellyfish.UCButton.Click += FoodAttribute_Click;
                    ButtonWithPicture bwpDeadJellyFish = new ButtonWithPicture("F_dead_jellyfish", "GameResources/Food/");
                    bwpDeadJellyFish.UCButton.Tag = "F_dead_jellyfish";
                    bwpDeadJellyFish.UCButton.Click += FoodAttribute_Click;
                    ButtonWithPicture bwpCookedJellyfish = new ButtonWithPicture("F_cooked_jellyfish", "GameResources/Food/");
                    bwpCookedJellyfish.UCButton.Tag = "F_cooked_jellyfish";
                    bwpCookedJellyfish.UCButton.Click += FoodAttribute_Click;
                    ButtonWithPicture bwpDriedJellyfish = new ButtonWithPicture("F_dried_jellyfish", "GameResources/Food/");
                    bwpDriedJellyfish.UCButton.Tag = "F_dried_jellyfish";
                    bwpDriedJellyfish.UCButton.Click += FoodAttribute_Click;
                    esJellyfish.UcWrapPanel.Children.Add(bwpJellyfish);
                    esJellyfish.UcWrapPanel.Children.Add(bwpDeadJellyFish);
                    esJellyfish.UcWrapPanel.Children.Add(bwpCookedJellyfish);
                    esJellyfish.UcWrapPanel.Children.Add(bwpDriedJellyfish);
                    WrapPanelFoodAttribute.Children.Add(esJellyfish);
                }
                PopFoodAttribute.PlacementTarget = (Button)sender;
                PopFoodAttribute.UpdateLayout();
                PopFoodAttribute.IsOpen = true;
                Point point;
                switch (bwtTag)
                {
                    case "FC_Meats":
                        point = esMeatsP5.TransformToVisual(WrapPanelFoodAttribute).Transform(new Point(0, 0));
                        ScrollViewerFoodAttribute.ScrollToVerticalOffset(point.Y);
                        break;
                    case "FC_Monster_Meats":
                        point = esMonsterMeats.TransformToVisual(WrapPanelFoodAttribute).Transform(new Point(0, 0));
                        ScrollViewerFoodAttribute.ScrollToVerticalOffset(point.Y);
                        break;
                    case "FC_Fishes":
                        if (esFishesP5.UcWrapPanel.Children.Count != 0)
                        {
                            point = esFishesP5.TransformToVisual(WrapPanelFoodAttribute).Transform(new Point(0, 0));
                            ScrollViewerFoodAttribute.ScrollToVerticalOffset(point.Y);
                        }
                        else
                        {
                            point = esFishes1.TransformToVisual(WrapPanelFoodAttribute).Transform(new Point(0, 0));
                            ScrollViewerFoodAttribute.ScrollToVerticalOffset(point.Y);
                        }
                        break;
                    case "FC_Vegetables":
                        point = esVegetablesP5.TransformToVisual(WrapPanelFoodAttribute).Transform(new Point(0, 0));
                        ScrollViewerFoodAttribute.ScrollToVerticalOffset(point.Y);
                        break;
                    case "FC_Fruit":
                        point = esFruitP5.TransformToVisual(WrapPanelFoodAttribute).Transform(new Point(0, 0));
                        ScrollViewerFoodAttribute.ScrollToVerticalOffset(point.Y);
                        break;
                    case "FC_Eggs":
                        point = esEggs1.TransformToVisual(WrapPanelFoodAttribute).Transform(new Point(0, 0));
                        ScrollViewerFoodAttribute.ScrollToVerticalOffset(point.Y);
                        break;
                    case "FC_Dairy_product":
                        point = esDairyProduct.TransformToVisual(WrapPanelFoodAttribute).Transform(new Point(0, 0));
                        ScrollViewerFoodAttribute.ScrollToVerticalOffset(point.Y);
                        break;
                    case "FC_Sweetener":
                        point = esSweetener.TransformToVisual(WrapPanelFoodAttribute).Transform(new Point(0, 0));
                        ScrollViewerFoodAttribute.ScrollToVerticalOffset(point.Y);
                        break;
                    case "FC_Jellyfish":
                        point = esJellyfish.TransformToVisual(WrapPanelFoodAttribute).Transform(new Point(0, 0));
                        ScrollViewerFoodAttribute.ScrollToVerticalOffset(point.Y);
                        break;
                }
            }
            else
            {
                foreach (UIElement expanderStackpanel in WrapPanelRightFood.Children)
                {
                    foreach (UIElement buttonWithText in ((ExpanderStackpanel)expanderStackpanel).UcWrapPanel.Children)
                    {
                        var rightButtonTag = (string[])(((ButtonWithText)buttonWithText).UCButton.Tag);
                        var rightButtonTag0 = rightButtonTag[0];
                        rightButtonTag0 = rightButtonTag0.Substring(rightButtonTag0.LastIndexOf('/') + 1, rightButtonTag0.Length - rightButtonTag0.LastIndexOf('/') - 5);
                        if (bwtTag != rightButtonTag0) continue;
                        Food_Click(((ButtonWithText)buttonWithText).UCButton, null);
                        var point = ((ButtonWithText)buttonWithText).TransformToVisual(WrapPanelRightFood).Transform(new Point(0, 0));
                        ScrollViewerRightFood.ScrollToVerticalOffset(point.Y);
                    }
                }
            }
        }

        //跳转按钮(食物属性)事件
        private void FoodAttribute_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var bwtTag = (string)button.Tag;
            FoodAttribute_Click_Handle(bwtTag);
        }

        private void FoodAttribute_Click_Handle(string bwtTag)
        {
            foreach (UIElement expanderStackpanel in WrapPanelRightFood.Children)
            {
                foreach (UIElement buttonWithText in ((ExpanderStackpanel)expanderStackpanel).UcWrapPanel.Children)
                {
                    var rightButtonTag = (string[])(((ButtonWithText)buttonWithText).UCButton.Tag);
                    var rightButtonTag0 = rightButtonTag[0];
                    rightButtonTag0 = RSN.GetFileName(rightButtonTag0);
                    if (bwtTag != rightButtonTag0) continue;
                    PopFoodAttribute.IsOpen = false;
                    Food_Click(((ButtonWithText)buttonWithText).UCButton, null);
                    Point point = ((ButtonWithText)buttonWithText).TransformToVisual(WrapPanelRightFood).Transform(new Point(0, 0));
                    ScrollViewerRightFood.ScrollToVerticalOffset(point.Y);
                }
            }
        }

        //WrapPanel_Food_Character内Grid.Width设置为WrapPanel_Food_Character.ActualWidth
        private void WrapPanel_Left_Food_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var leftFoodWidth = (int)WrapPanelLeftFood.ActualWidth;
            foreach (UIElement uielement in WrapPanelLeftFood.Children)
            {
                if (uielement.GetType() == typeof(Grid))
                {
                    ((Grid)uielement).Width = leftFoodWidth;
                }
                if (uielement.GetType() == typeof(WrapPanel))
                {
                    ((WrapPanel)uielement).Width = leftFoodWidth;
                }
            }
        }
        //WrapPanel_Right_Food内Expander.Width设置为WrapPanel_Right_Food.ActualWidth
        private void WrapPanel_Right_Food_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            foreach (ExpanderStackpanel expanderStackpanel in WrapPanelRightFood.Children)
            {
                expanderStackpanel.Width = (int)WrapPanelRightFood.ActualWidth;
            }
        }
    }
}
