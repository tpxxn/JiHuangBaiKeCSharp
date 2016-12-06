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
            Button button = (Button)sender;
            string[] BWTTag = (string[])button.Tag;//获取参数
            Food_Recipe_Click_Handle(BWTTag);
        }
        //WrapPanel_Left_Food控件创建事件(食谱)
        private void Food_Recipe_Click_Handle(string[] BWTTag)
        {
            #region "Restrictions初始化"
            List<string> Restrictions_1 = new List<string>();
            List<string> Restrictions_2 = new List<string>();
            string[] preRes = new string[5] { BWTTag[18], BWTTag[20], BWTTag[22], BWTTag[24], BWTTag[26] };
            string[] preResAtt = new string[5] { BWTTag[19], BWTTag[21], BWTTag[23], BWTTag[25], BWTTag[27] };
            string[] RestrictionsAttributes = new string[2];
            RestrictionsAttributes = ADRD.DelRepeatData(preResAtt);
            if (preResAtt[0] == RestrictionsAttributes[0] && preRes[0] != "")
            {
                Restrictions_1.Add(preRes[0]);
            }
            if (preResAtt[1] == RestrictionsAttributes[0] && preRes[1] != "")
            {
                Restrictions_1.Add(preRes[1]);
            }
            if (preResAtt[1] == RestrictionsAttributes[1] && preRes[1] != "")
            {
                Restrictions_2.Add(preRes[1]);
            }
            if (preResAtt[2] == RestrictionsAttributes[0] && preRes[2] != "")
            {
                Restrictions_1.Add(preRes[2]);
            }
            if (preResAtt[2] == RestrictionsAttributes[1] && preRes[2] != "")
            {
                Restrictions_2.Add(preRes[2]);
            }
            if (preResAtt[3] == RestrictionsAttributes[0] && preRes[3] != "")
            {
                Restrictions_1.Add(preRes[3]);
            }
            if (preResAtt[3] == RestrictionsAttributes[1] && preRes[3] != "")
            {
                Restrictions_2.Add(preRes[3]);
            }
            if (preResAtt[4] == RestrictionsAttributes[0] && preRes[4] != "")
            {
                Restrictions_1.Add(preRes[4]);
            }
            if (preResAtt[4] == RestrictionsAttributes[1] && preRes[4] != "")
            {
                Restrictions_2.Add(preRes[4]);
            }
            #endregion
            const string ResourceDir = "GameResources/Food/";
            try
            {
                WrapPanel_Left_Food.Children.Clear();//清空WrapPanel_Left_Food
                Grid GI = new Grid();
                GI = PG.GridInterval(10);
                WrapPanel_Left_Food.Children.Add(GI);
                #region "图片 BWTTag[0]"
                Grid gPicture = PG.GridInit(64);
                Image iPicture = new Image();
                iPicture.Height = 64;
                iPicture.Width = 64;
                iPicture.HorizontalAlignment = HorizontalAlignment.Center;
                iPicture.Source = RSN.PictureShortName(BWTTag[0]);
                #region "便携式烹饪锅  BWTTag[3]"
                if (BWTTag[3] != "")
                {
                    Image iPortableCrockPot = new Image();
                    iPortableCrockPot.Height = 32;
                    iPortableCrockPot.Width = 32;
                    iPortableCrockPot.HorizontalAlignment = HorizontalAlignment.Left;
                    iPortableCrockPot.VerticalAlignment = VerticalAlignment.Top;
                    iPortableCrockPot.Source = RSN.PictureShortName(RSN.ShortName("CP_PortableCrockPot"));
                    Thickness tPortableCrockPot = new Thickness();
                    tPortableCrockPot.Top = 10;
                    tPortableCrockPot.Left = 10;
                    iPortableCrockPot.Margin = tPortableCrockPot;
                    gPicture.Children.Add(iPortableCrockPot);
                }
                #endregion
                gPicture.Children.Add(iPicture);
                WrapPanel_Left_Food.Children.Add(gPicture);
                #endregion
                GI = PG.GridInterval(5);
                WrapPanel_Left_Food.Children.Add(GI);
                #region "中文名  BWTTag[1]"
                Grid gName = PG.GridInit();
                TextBlock tbName = new TextBlock();
                tbName.HorizontalAlignment = HorizontalAlignment.Center;
                tbName.Text = BWTTag[1];
                tbName.FontSize = 16;
                gName.Children.Add(tbName);
                WrapPanel_Left_Food.Children.Add(gName);
                #endregion
                #region "英文名  BWTTag[2]"
                Grid gEnName = PG.GridInit();
                TextBlock tbEnName = new TextBlock();
                tbEnName.HorizontalAlignment = HorizontalAlignment.Center;
                tbEnName.Text = BWTTag[2];
                tbEnName.FontSize = 16;
                tbEnName.Margin = new Thickness(0, -2, 0, 0);
                gEnName.Children.Add(tbEnName);
                WrapPanel_Left_Food.Children.Add(gEnName);
                #endregion
                GI = PG.GridInterval(20);
                WrapPanel_Left_Food.Children.Add(GI);
                #region "生命  BWTTag[4]"
                if (BWTTag[4] != "")
                {
                    double propertyHealth = Convert.ToDouble(BWTTag[4]);
                    Grid gHealth = PG.GridInit(15);
                    PropertyBar pbHealth;
                    if (propertyHealth < 0)
                        pbHealth = new PropertyBar(true);
                    else
                        pbHealth = new PropertyBar();
                    pbHealth.UCTextBlockName.Width = 30;
                    pbHealth.UCTextBlockName.Text = "生命";
                    pbHealth.UCProgressBar.Value = Math.Abs(propertyHealth);
                    pbHealth.UCProgressBar.Foreground = BC.brushConverter(PBCGreen);
                    pbHealth.UCTextBlockValue.Width = 38;
                    pbHealth.UCTextBlockValue.Text = BWTTag[4];
                    gHealth.Children.Add(pbHealth);
                    WrapPanel_Left_Food.Children.Add(gHealth);

                    if (BWTTag[2] == "Jellybeans")
                    {
                        pbHealth.UCTextBlockValue.Text = "2+120";
                        Grid gHealth_Jellybeans = PG.GridInit();
                        TextBlock tbJellybeans = new TextBlock();
                        tbJellybeans.HorizontalAlignment = HorizontalAlignment.Left;
                        tbJellybeans.TextWrapping = TextWrapping.Wrap;
                        tbJellybeans.Text = "食用后立即回复2点生命，之后每2秒回复2点生命，持续2分钟(回复BUFF不叠加)，总共回复122点";
                        tbJellybeans.Margin = new Thickness(15, 0, 10, 0);
                        gHealth_Jellybeans.Children.Add(tbJellybeans);
                        WrapPanel_Left_Food.Children.Add(gHealth_Jellybeans);

                    }
                }
                #endregion
                #region "饥饿  BWTTag[5]"
                if (BWTTag[5] != "")
                {
                    double propertyHunger = Convert.ToDouble(BWTTag[5]);
                    GI = PG.GridInterval(10);
                    WrapPanel_Left_Food.Children.Add(GI);
                    Grid gHunger = PG.GridInit(15);
                    PropertyBar pbHunger;
                    if (propertyHunger < 0)
                        pbHunger = new PropertyBar(true);
                    else
                        pbHunger = new PropertyBar();
                    pbHunger.UCTextBlockName.Width = 30;
                    pbHunger.UCTextBlockName.Text = "饥饿";
                    pbHunger.UCProgressBar.Value = Math.Abs(propertyHunger) / 1.5;
                    pbHunger.UCProgressBar.Foreground = BC.brushConverter(PBCOrange);
                    pbHunger.UCTextBlockValue.Width = 38;
                    pbHunger.UCTextBlockValue.Text = BWTTag[5];
                    gHunger.Children.Add(pbHunger);
                    WrapPanel_Left_Food.Children.Add(gHunger);
                }
                #endregion
                #region "精神  BWTTag[6]"
                if (BWTTag[6] != "")
                {
                    double propertySanity = Convert.ToDouble(BWTTag[6]);
                    GI = PG.GridInterval(10);
                    WrapPanel_Left_Food.Children.Add(GI);
                    Grid gSanity = PG.GridInit(15);
                    PropertyBar pbSanity;
                    if (propertySanity < 0)
                        pbSanity = new PropertyBar(true);
                    else
                        pbSanity = new PropertyBar();
                    pbSanity.UCTextBlockName.Width = 30;
                    pbSanity.UCTextBlockName.Text = "精神";
                    pbSanity.UCProgressBar.Value = Math.Abs(propertySanity) / 0.5;
                    pbSanity.UCProgressBar.Foreground = BC.brushConverter(PBCRed);
                    pbSanity.UCTextBlockValue.Width = 38;
                    pbSanity.UCTextBlockValue.Text = BWTTag[6];
                    gSanity.Children.Add(pbSanity);
                    WrapPanel_Left_Food.Children.Add(gSanity);
                }
                #endregion
                #region "保鲜  BWTTag[7]"
                if (BWTTag[7] != "")
                {
                    double propertyPerish = Convert.ToDouble(BWTTag[7]);
                    GI = PG.GridInterval(10);
                    WrapPanel_Left_Food.Children.Add(GI);
                    Grid gPerish = PG.GridInit(15);
                    PropertyBar pbPerish;
                    if (propertyPerish < 0)
                        pbPerish = new PropertyBar(true);
                    else
                        pbPerish = new PropertyBar();
                    pbPerish.UCTextBlockName.Width = 30;
                    pbPerish.UCTextBlockName.Text = "保鲜";
                    pbPerish.UCProgressBar.Value = Math.Abs(propertyPerish) / 0.2;
                    pbPerish.UCProgressBar.Foreground = BC.brushConverter(PBCBlue);
                    pbPerish.UCTextBlockValue.Width = 38;
                    if (propertyPerish == 1000)
                        pbPerish.UCTextBlockValue.Text = "∞";
                    else
                        pbPerish.UCTextBlockValue.Text = BWTTag[7];
                    gPerish.Children.Add(pbPerish);
                    WrapPanel_Left_Food.Children.Add(gPerish);
                }
                #endregion
                #region "烹饪  BWTTag[8]"
                if (BWTTag[8] != "")
                {
                    double propertyCooktime = Convert.ToDouble(BWTTag[8]);
                    GI = PG.GridInterval(10);
                    WrapPanel_Left_Food.Children.Add(GI);
                    Grid gCooktime = PG.GridInit(15);
                    PropertyBar pbCooktime;
                    if (propertyCooktime < 0)
                        pbCooktime = new PropertyBar(true);
                    else
                        pbCooktime = new PropertyBar();
                    pbCooktime.UCTextBlockName.Width = 30;
                    pbCooktime.UCTextBlockName.Text = "烹饪";
                    pbCooktime.UCProgressBar.Value = Math.Abs(propertyCooktime) / 0.6;
                    pbCooktime.UCProgressBar.Foreground = BC.brushConverter(PBCPurple);
                    pbCooktime.UCTextBlockValue.Width = 38;
                    pbCooktime.UCTextBlockValue.Text = BWTTag[8];
                    gCooktime.Children.Add(pbCooktime);
                    WrapPanel_Left_Food.Children.Add(gCooktime);
                }
                #endregion
                #region "优先  BWTTag[9]"
                if (BWTTag[9] != "")
                {
                    double propertyPriority = Convert.ToDouble(BWTTag[9]);
                    GI = PG.GridInterval(10);
                    WrapPanel_Left_Food.Children.Add(GI);
                    Grid gPriority = PG.GridInit(15);
                    PropertyBar pbPriority;
                    if (propertyPriority < 0)
                        pbPriority = new PropertyBar(true);
                    else
                        pbPriority = new PropertyBar();
                    pbPriority.UCTextBlockName.Width = 30;
                    pbPriority.UCTextBlockName.Text = "优先";
                    pbPriority.UCProgressBar.Value = Math.Abs(propertyPriority) / 0.3;
                    pbPriority.UCProgressBar.Foreground = BC.brushConverter(PBCPink);
                    pbPriority.UCTextBlockValue.Width = 38;
                    pbPriority.UCTextBlockValue.Text = BWTTag[9];
                    gPriority.Children.Add(pbPriority);
                    WrapPanel_Left_Food.Children.Add(gPriority);
                }
                #endregion
                GI = PG.GridInterval(20);
                WrapPanel_Left_Food.Children.Add(GI);
                if (BWTTag[1] != "湿腻焦糊")
                {
                    #region "烹饪需求  BWTTag[10-17]"
                    WrapPanel_Left_Food.Children.Add(PG.GridTag("烹饪需求："));
                    StackPanel sNeed = new StackPanel();
                    sNeed.HorizontalAlignment = HorizontalAlignment.Center;
                    WrapPanel wNeed_1 = new WrapPanel();
                    WrapPanel wNeed_2 = new WrapPanel();
                    WrapPanel wNeed_3 = new WrapPanel();
                    if (BWTTag[10] != "")
                    {
                        ButtonWithPicture bwpNeed_1 = new ButtonWithPicture(BWTTag[10], ResourceDir);
                        bwpNeed_1.UCButton.Tag = BWTTag[10];
                        bwpNeed_1.UCButton.Click += Food_Jump_Click;
                        wNeed_1.Children.Add(bwpNeed_1);
                        TextBlock tbNeed_1 = new TextBlock();
                        tbNeed_1.Text = BWTTag[11];
                        tbNeed_1.Padding = new Thickness(0, 8, 0, 0);
                        wNeed_1.Children.Add(tbNeed_1);
                        sNeed.Children.Add(wNeed_1);
                    }
                    if (BWTTag[12] != "")
                    {
                        ButtonWithPicture bwpNeed_or = new ButtonWithPicture(BWTTag[12], ResourceDir);
                        bwpNeed_or.UCButton.Tag = BWTTag[12];
                        bwpNeed_or.UCButton.Click += Food_Jump_Click;
                        wNeed_1.Children.Add(bwpNeed_or);
                        TextBlock tbNeed_or = new TextBlock();
                        tbNeed_or.Text = BWTTag[13];
                        tbNeed_or.Padding = new Thickness(0, 8, 0, 0);
                        if (BWTTag[2] != "Monster Lasagna")
                        {
                            wNeed_1.Children.Add(tbNeed_or);
                        }
                        else
                        {
                            if (UI_gameversion.SelectedIndex == 2)
                            {
                                wNeed_1.Children.Add(tbNeed_or);
                            }
                        }
                    }
                    if (BWTTag[14] != "")
                    {
                        ButtonWithPicture bwpNeed_2 = new ButtonWithPicture(BWTTag[14], ResourceDir);
                        bwpNeed_2.UCButton.Tag = BWTTag[14];
                        bwpNeed_2.UCButton.Click += Food_Jump_Click;
                        TextBlock tbNeed_2 = new TextBlock();
                        tbNeed_2.Text = BWTTag[15];
                        tbNeed_2.Padding = new Thickness(0, 8, 0, 0);
                        if (BWTTag[2] != "Monster Lasagna")
                        {
                            wNeed_2.Children.Add(bwpNeed_2);
                            wNeed_2.Children.Add(tbNeed_2);
                            sNeed.Children.Add(wNeed_2);
                        }
                        else
                        {
                            if (UI_gameversion.SelectedIndex == 2)
                            {
                                wNeed_1.Children.Add(bwpNeed_2);
                            }
                                wNeed_1.Children.Add(tbNeed_2);
                        }
                    }
                    if (BWTTag[16] != "")
                    {
                        ButtonWithPicture bwpNeed_3 = new ButtonWithPicture(BWTTag[16], ResourceDir);
                        bwpNeed_3.UCButton.Tag = BWTTag[16];
                        bwpNeed_3.UCButton.Click += Food_Jump_Click;
                        wNeed_3.Children.Add(bwpNeed_3);
                        TextBlock tbNeed_3 = new TextBlock();
                        tbNeed_3.Text = BWTTag[17];
                        tbNeed_3.Padding = new Thickness(0, 8, 0, 0);
                        wNeed_3.Children.Add(tbNeed_3);
                        sNeed.Children.Add(wNeed_3);
                    }
                    WrapPanel_Left_Food.Children.Add(sNeed);
                    #endregion
                    #region "填充限制  BWTTag[18-27]"
                    if (BWTTag[18] != "")
                    {
                        GI = PG.GridInterval(20);
                        WrapPanel_Left_Food.Children.Add(GI);
                        WrapPanel_Left_Food.Children.Add(PG.GridTag("填充限制："));
                        StackPanel sRestrictions = new StackPanel();
                        sRestrictions.HorizontalAlignment = HorizontalAlignment.Center;
                        WrapPanel wRestrictions_1 = new WrapPanel();
                        WrapPanel wRestrictions_2 = new WrapPanel();
                        if (Restrictions_1.Count != 0)
                        {
                            TextBlock tbRestrictions_1 = new TextBlock();
                            tbRestrictions_1.Text = RestrictionsAttributes[0];
                            tbRestrictions_1.Padding = new Thickness(0, 8, 0, 0);
                            wRestrictions_1.Children.Add(tbRestrictions_1);
                            foreach (string str in Restrictions_1)
                            {
                                ButtonWithPicture bwp = new ButtonWithPicture(str, ResourceDir);
                                bwp.UCButton.Tag = str;
                                bwp.UCButton.Click += Food_Jump_Click;
                                wRestrictions_1.Children.Add(bwp);
                            }
                            sRestrictions.Children.Add(wRestrictions_1);
                        }
                        if (Restrictions_2.Count != 0)
                        {
                            TextBlock tbRestrictions_2 = new TextBlock();
                            tbRestrictions_2.Text = RestrictionsAttributes[1];
                            tbRestrictions_2.Padding = new Thickness(0, 8, 0, 0);
                            wRestrictions_2.Children.Add(tbRestrictions_2);
                            foreach (string str in Restrictions_2)
                            {
                                ButtonWithPicture bwp = new ButtonWithPicture(str, ResourceDir);
                                bwp.UCButton.Tag = str;
                                bwp.UCButton.Click += Food_Jump_Click;
                                wRestrictions_2.Children.Add(bwp);
                            }
                            sRestrictions.Children.Add(wRestrictions_2);
                        }
                        WrapPanel_Left_Food.Children.Add(sRestrictions);
                    }
                    #endregion
                    GI = PG.GridInterval(20);
                    WrapPanel_Left_Food.Children.Add(GI);
                    #region "推荐食谱 BWTTag[28-31]"
                    WrapPanel_Left_Food.Children.Add(PG.GridTag("推荐食谱："));
                    Grid gRecommendContent = PG.GridInit(35);
                    WrapPanel sRecommendContent = new WrapPanel();
                    sRecommendContent.HorizontalAlignment = HorizontalAlignment.Center;
                    ButtonWithPicture bwpRecommed_1 = new ButtonWithPicture(BWTTag[28], ResourceDir);
                    bwpRecommed_1.UCButton.Tag = BWTTag[28];
                    bwpRecommed_1.UCButton.Click += Food_Jump_Click;
                    ButtonWithPicture bwpRecommed_2 = new ButtonWithPicture(BWTTag[29], ResourceDir);
                    bwpRecommed_2.UCButton.Tag = BWTTag[29];
                    bwpRecommed_2.UCButton.Click += Food_Jump_Click;
                    ButtonWithPicture bwpRecommed_3 = new ButtonWithPicture(BWTTag[30], ResourceDir);
                    bwpRecommed_3.UCButton.Tag = BWTTag[30];
                    bwpRecommed_3.UCButton.Click += Food_Jump_Click;
                    ButtonWithPicture bwpRecommed_4 = new ButtonWithPicture(BWTTag[31], ResourceDir);
                    bwpRecommed_4.UCButton.Tag = BWTTag[31];
                    bwpRecommed_4.UCButton.Click += Food_Jump_Click;
                    sRecommendContent.Children.Add(bwpRecommed_1);
                    sRecommendContent.Children.Add(bwpRecommed_2);
                    sRecommendContent.Children.Add(bwpRecommed_3);
                    sRecommendContent.Children.Add(bwpRecommed_4);
                    gRecommendContent.Children.Add(sRecommendContent);
                    WrapPanel_Left_Food.Children.Add(gRecommendContent);
                    #endregion
                }
                GI = PG.GridInterval(20);
                WrapPanel_Left_Food.Children.Add(GI);
                #region "介绍  BWTTag[32]"
                Grid gIntroduce = PG.GridInit();
                TextBlock tbIntroduce = new TextBlock();
                tbIntroduce.HorizontalAlignment = HorizontalAlignment.Left;
                tbIntroduce.TextWrapping = TextWrapping.Wrap;
                tbIntroduce.Text = BWTTag[32];
                tbIntroduce.FontSize = 13;
                Thickness TIntroduce = new Thickness();
                TIntroduce.Left = 15;
                tbIntroduce.Margin = TIntroduce;
                gIntroduce.Children.Add(tbIntroduce);
                WrapPanel_Left_Food.Children.Add(gIntroduce);
                #endregion
                GI = PG.GridInterval(20);
                WrapPanel_Left_Food.Children.Add(GI);
            }
            catch { }
            WrapPanel_Left_Food_SizeChanged(null, null);//调整位置
        }

        //Food面板Click事件(食材)
        private void Food_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string[] BWTTag = (string[])button.Tag;//获取参数
            Food_Click_Handle(BWTTag);
        }
        //WrapPanel_Left_Food控件创建事件(食材)
        private void Food_Click_Handle(string[] BWTTag)
        {
            const string ResourceDir = "GameResources/Food/";
            try
            {
                WrapPanel_Left_Food.Children.Clear();//清空WrapPanel_Left_Food
                Grid GI = new Grid();
                GI = PG.GridInterval(10);
                WrapPanel_Left_Food.Children.Add(GI);
                #region "图片 BWTTag[0]"
                Grid gPicture = PG.GridInit(64);
                Image iPicture = new Image();
                iPicture.Height = 64;
                iPicture.Width = 64;
                iPicture.HorizontalAlignment = HorizontalAlignment.Center;
                iPicture.Source = RSN.PictureShortName(BWTTag[0]);
                gPicture.Children.Add(iPicture);
                WrapPanel_Left_Food.Children.Add(gPicture);
                #endregion
                GI = PG.GridInterval(5);
                WrapPanel_Left_Food.Children.Add(GI);
                #region "中文名  BWTTag[1]"
                Grid gName = PG.GridInit();
                TextBlock tbName = new TextBlock();
                tbName.HorizontalAlignment = HorizontalAlignment.Center;
                tbName.Text = BWTTag[1];
                tbName.FontSize = 16;
                gName.Children.Add(tbName);
                WrapPanel_Left_Food.Children.Add(gName);
                #endregion
                #region "英文名  BWTTag[2]"
                Grid gEnName = PG.GridInit();
                TextBlock tbEnName = new TextBlock();
                tbEnName.HorizontalAlignment = HorizontalAlignment.Center;
                tbEnName.Text = BWTTag[2];
                tbEnName.FontSize = 16;
                tbEnName.Margin = new Thickness(0, -2, 0, 0);
                gEnName.Children.Add(tbEnName);
                WrapPanel_Left_Food.Children.Add(gEnName);
                #endregion
                GI = PG.GridInterval(20);
                WrapPanel_Left_Food.Children.Add(GI);
                #region "生命  BWTTag[3]"
                if (BWTTag[3] != "")
                {
                    double propertyHealth = Convert.ToDouble(BWTTag[3]);
                    Grid gHealth = PG.GridInit(15);
                    PropertyBar pbHealth;
                    if (propertyHealth < 0)
                        pbHealth = new PropertyBar(true);
                    else
                        pbHealth = new PropertyBar();
                    pbHealth.UCTextBlockName.Width = 30;
                    pbHealth.UCTextBlockName.Text = "生命";
                    pbHealth.UCProgressBar.Value = Math.Abs(propertyHealth);
                    pbHealth.UCProgressBar.Foreground = BC.brushConverter(PBCGreen);
                    pbHealth.UCTextBlockValue.Width = 39;
                    pbHealth.UCTextBlockValue.Text = BWTTag[3];
                    gHealth.Children.Add(pbHealth);
                    WrapPanel_Left_Food.Children.Add(gHealth);
                }
                #endregion
                #region "饥饿  BWTTag[4]"
                if (BWTTag[4] != "")
                {
                    double propertyHunger = Convert.ToDouble(BWTTag[4]);
                    GI = PG.GridInterval(10);
                    WrapPanel_Left_Food.Children.Add(GI);
                    Grid gHunger = PG.GridInit(15);
                    PropertyBar pbHunger;
                    if (propertyHunger < 0)
                        pbHunger = new PropertyBar(true);
                    else
                        pbHunger = new PropertyBar();
                    pbHunger.UCTextBlockName.Width = 30;
                    pbHunger.UCTextBlockName.Text = "饥饿";
                    pbHunger.UCProgressBar.Value = Math.Abs(propertyHunger) / 1.5;
                    pbHunger.UCProgressBar.Foreground = BC.brushConverter(PBCOrange);
                    pbHunger.UCTextBlockValue.Width = 39;
                    pbHunger.UCTextBlockValue.Text = BWTTag[4];
                    gHunger.Children.Add(pbHunger);
                    WrapPanel_Left_Food.Children.Add(gHunger);
                }
                #endregion
                #region "精神  BWTTag[5]"
                if (BWTTag[5] != "")
                {
                    double propertySanity = Convert.ToDouble(BWTTag[5]);
                    GI = PG.GridInterval(10);
                    WrapPanel_Left_Food.Children.Add(GI);
                    Grid gSanity = PG.GridInit(15);
                    PropertyBar pbSanity;
                    if (propertySanity < 0)
                        pbSanity = new PropertyBar(true);
                    else
                        pbSanity = new PropertyBar();
                    pbSanity.UCTextBlockName.Width = 30;
                    pbSanity.UCTextBlockName.Text = "精神";
                    pbSanity.UCProgressBar.Value = Math.Abs(propertySanity) / 0.5;
                    pbSanity.UCProgressBar.Foreground = BC.brushConverter(PBCRed);
                    pbSanity.UCTextBlockValue.Width = 39;
                    pbSanity.UCTextBlockValue.Text = BWTTag[5];
                    gSanity.Children.Add(pbSanity);
                    WrapPanel_Left_Food.Children.Add(gSanity);
                }
                #endregion
                #region "保鲜  BWTTag[6]"
                if (BWTTag[6] != "")
                {
                    double propertyPerish = Convert.ToDouble(BWTTag[6]);
                    GI = PG.GridInterval(10);
                    WrapPanel_Left_Food.Children.Add(GI);
                    Grid gPerish = PG.GridInit(15);
                    PropertyBar pbPerish;
                    if (propertyPerish < 0)
                        pbPerish = new PropertyBar(true);
                    else
                        pbPerish = new PropertyBar();
                    pbPerish.UCTextBlockName.Width = 30;
                    pbPerish.UCTextBlockName.Text = "保鲜";
                    pbPerish.UCProgressBar.Value = Math.Abs(propertyPerish) / 0.4;
                    pbPerish.UCProgressBar.Foreground = BC.brushConverter(PBCBlue);
                    pbPerish.UCTextBlockValue.Width = 39;
                    if (propertyPerish == 1000)
                        pbPerish.UCTextBlockValue.Text = "∞";
                    else
                        pbPerish.UCTextBlockValue.Text = BWTTag[6];
                    gPerish.Children.Add(pbPerish);
                    WrapPanel_Left_Food.Children.Add(gPerish);
                }
                #endregion
                GI = PG.GridInterval(20);
                WrapPanel_Left_Food.Children.Add(GI);
                #region "食物属性  BWTTag[7-10]"
                WrapPanel_Left_Food.Children.Add(PG.GridTag("食物属性："));
                StackPanel sAttribute = new StackPanel();
                sAttribute.HorizontalAlignment = HorizontalAlignment.Center;
                WrapPanel wAttribute = new WrapPanel();
                if (BWTTag[7] != "")
                {
                    ButtonWithPicture bwpAttribute_1 = new ButtonWithPicture(BWTTag[7], ResourceDir);
                    bwpAttribute_1.UCButton.Tag = BWTTag[7];
                    bwpAttribute_1.UCButton.Click += Food_Jump_Click;
                    wAttribute.Children.Add(bwpAttribute_1);
                    TextBlock tbAttribute_1 = new TextBlock();
                    tbAttribute_1.Text = BWTTag[8];
                    tbAttribute_1.Padding = new Thickness(0, 8, 0, 0);
                    wAttribute.Children.Add(tbAttribute_1);
                }
                else if (BWTTag[2] == "Roasted Birchnut")
                {
                    TextBlock tbAttribute_1 = new TextBlock();
                    tbAttribute_1.Text = "填充物×1";
                    tbAttribute_1.Padding = new Thickness(0, 8, 0, 0);
                    wAttribute.Children.Add(tbAttribute_1);
                }
                if (BWTTag[9] != "")
                {
                    ButtonWithPicture bwpAttribute_2 = new ButtonWithPicture(BWTTag[9], ResourceDir);
                    bwpAttribute_2.UCButton.Tag = BWTTag[9];
                    bwpAttribute_2.UCButton.Click += Food_Jump_Click;
                    wAttribute.Children.Add(bwpAttribute_2);
                    TextBlock tbAttribute_2 = new TextBlock();
                    tbAttribute_2.Text = BWTTag[10];
                    tbAttribute_2.Padding = new Thickness(0, 8, 0, 0);
                    wAttribute.Children.Add(tbAttribute_2);
                }
                sAttribute.Children.Add(wAttribute);
                WrapPanel_Left_Food.Children.Add(sAttribute);
                #endregion
                GI = PG.GridInterval(20);
                WrapPanel_Left_Food.Children.Add(GI);
                #region "介绍  BWTTag[11]"
                Grid gIntroduce = PG.GridInit();
                TextBlock tbIntroduce = new TextBlock();
                tbIntroduce.HorizontalAlignment = HorizontalAlignment.Left;
                tbIntroduce.TextWrapping = TextWrapping.Wrap;
                tbIntroduce.Text = BWTTag[11];
                tbIntroduce.FontSize = 13;
                Thickness TIntroduce = new Thickness();
                TIntroduce.Left = 15;
                tbIntroduce.Margin = TIntroduce;
                gIntroduce.Children.Add(tbIntroduce);
                WrapPanel_Left_Food.Children.Add(gIntroduce);
                #endregion
                GI = PG.GridInterval(20);
                WrapPanel_Left_Food.Children.Add(GI);
            }
            catch { }
            WrapPanel_Left_Food_SizeChanged(null, null);//调整位置
        }

        //Food面板Click事件(非食材)
        private void Food_NoFC_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string[] BWTTag = (string[])button.Tag;//获取参数
            Food_NoFC_Click_Handle(BWTTag);
        }
        //WrapPanel_Left_Food控件创建事件(非食材)
        private void Food_NoFC_Click_Handle(string[] BWTTag)
        {
            try
            {
                WrapPanel_Left_Food.Children.Clear();//清空WrapPanel_Left_Food
                Grid GI = new Grid();
                GI = PG.GridInterval(10);
                WrapPanel_Left_Food.Children.Add(GI);
                #region "图片 BWTTag[0]"
                Grid gPicture = PG.GridInit(64);
                Image iPicture = new Image();
                iPicture.Height = 64;
                iPicture.Width = 64;
                iPicture.HorizontalAlignment = HorizontalAlignment.Center;
                iPicture.Source = RSN.PictureShortName(BWTTag[0]);
                gPicture.Children.Add(iPicture);
                WrapPanel_Left_Food.Children.Add(gPicture);
                #endregion
                GI = PG.GridInterval(5);
                WrapPanel_Left_Food.Children.Add(GI);
                #region "中文名  BWTTag[1]"
                Grid gName = PG.GridInit();
                TextBlock tbName = new TextBlock();
                tbName.HorizontalAlignment = HorizontalAlignment.Center;
                tbName.Text = BWTTag[1];
                tbName.FontSize = 16;
                gName.Children.Add(tbName);
                WrapPanel_Left_Food.Children.Add(gName);
                #endregion
                #region "英文名  BWTTag[2]"
                Grid gEnName = PG.GridInit();
                TextBlock tbEnName = new TextBlock();
                tbEnName.HorizontalAlignment = HorizontalAlignment.Center;
                tbEnName.Text = BWTTag[2];
                tbEnName.FontSize = 16;
                tbEnName.Margin = new Thickness(0, -2, 0, 0);
                gEnName.Children.Add(tbEnName);
                WrapPanel_Left_Food.Children.Add(gEnName);
                #endregion
                GI = PG.GridInterval(20);
                WrapPanel_Left_Food.Children.Add(GI);
                #region "生命  BWTTag[3]"
                if (BWTTag[3] != "")
                {
                    double propertyHealth = Convert.ToDouble(BWTTag[3]);
                    Grid gHealth = PG.GridInit(15);
                    PropertyBar pbHealth;
                    if (propertyHealth < 0)
                        pbHealth = new PropertyBar(true);
                    else
                        pbHealth = new PropertyBar();
                    pbHealth.UCTextBlockName.Width = 30;
                    pbHealth.UCTextBlockName.Text = "生命";
                    pbHealth.UCProgressBar.Value = Math.Abs(propertyHealth);
                    pbHealth.UCProgressBar.Foreground = BC.brushConverter(PBCGreen);
                    pbHealth.UCTextBlockValue.Width = 39;
                    pbHealth.UCTextBlockValue.Text = BWTTag[3];
                    gHealth.Children.Add(pbHealth);
                    WrapPanel_Left_Food.Children.Add(gHealth);
                }
                #endregion
                #region "饥饿  BWTTag[4]"
                if (BWTTag[4] != "")
                {
                    double propertyHunger = Convert.ToDouble(BWTTag[4]);
                    GI = PG.GridInterval(10);
                    WrapPanel_Left_Food.Children.Add(GI);
                    Grid gHunger = PG.GridInit(15);
                    PropertyBar pbHunger;
                    if (propertyHunger < 0)
                        pbHunger = new PropertyBar(true);
                    else
                        pbHunger = new PropertyBar();
                    pbHunger.UCTextBlockName.Width = 30;
                    pbHunger.UCTextBlockName.Text = "饥饿";
                    pbHunger.UCProgressBar.Value = Math.Abs(propertyHunger) / 1.5;
                    pbHunger.UCProgressBar.Foreground = BC.brushConverter(PBCOrange);
                    pbHunger.UCTextBlockValue.Width = 39;
                    pbHunger.UCTextBlockValue.Text = BWTTag[4];
                    gHunger.Children.Add(pbHunger);
                    WrapPanel_Left_Food.Children.Add(gHunger);
                }
                #endregion
                #region "精神  BWTTag[5]"
                if (BWTTag[5] != "")
                {
                    double propertySanity = Convert.ToDouble(BWTTag[5]);
                    GI = PG.GridInterval(10);
                    WrapPanel_Left_Food.Children.Add(GI);
                    Grid gSanity = PG.GridInit(15);
                    PropertyBar pbSanity;
                    if (propertySanity < 0)
                        pbSanity = new PropertyBar(true);
                    else
                        pbSanity = new PropertyBar();
                    pbSanity.UCTextBlockName.Width = 30;
                    pbSanity.UCTextBlockName.Text = "精神";
                    pbSanity.UCProgressBar.Value = Math.Abs(propertySanity) / 0.5;
                    pbSanity.UCProgressBar.Foreground = BC.brushConverter(PBCRed);
                    pbSanity.UCTextBlockValue.Width = 39;
                    pbSanity.UCTextBlockValue.Text = BWTTag[5];
                    gSanity.Children.Add(pbSanity);
                    WrapPanel_Left_Food.Children.Add(gSanity);
                }
                #endregion
                #region "保鲜  BWTTag[6]"
                if (BWTTag[6] != "")
                {
                    double propertyPerish = Convert.ToDouble(BWTTag[6]);
                    GI = PG.GridInterval(10);
                    WrapPanel_Left_Food.Children.Add(GI);
                    Grid gPerish = PG.GridInit(15);
                    PropertyBar pbPerish;
                    if (propertyPerish < 0)
                        pbPerish = new PropertyBar(true);
                    else
                        pbPerish = new PropertyBar();
                    pbPerish.UCTextBlockName.Width = 30;
                    pbPerish.UCTextBlockName.Text = "保鲜";
                    pbPerish.UCProgressBar.Value = Math.Abs(propertyPerish) / 0.4;
                    pbPerish.UCProgressBar.Foreground = BC.brushConverter(PBCBlue);
                    pbPerish.UCTextBlockValue.Width = 39;
                    if (propertyPerish == 1000)
                        pbPerish.UCTextBlockValue.Text = "∞";
                    else
                        pbPerish.UCTextBlockValue.Text = BWTTag[6];
                    gPerish.Children.Add(pbPerish);
                    WrapPanel_Left_Food.Children.Add(gPerish);
                }
                #endregion
                #region "食物属性  BWTTag[7]"
                if (BWTTag[7] != "")
                {
                    GI = PG.GridInterval(20);
                    WrapPanel_Left_Food.Children.Add(GI);
                    string FoodAttribute = "食物属性：" + BWTTag[7];
                    WrapPanel_Left_Food.Children.Add(PG.GridTag(FoodAttribute));
                }
                #endregion
                GI = PG.GridInterval(20);
                WrapPanel_Left_Food.Children.Add(GI);
                #region "介绍  BWTTag[8]"
                Grid gIntroduce = PG.GridInit();
                TextBlock tbIntroduce = new TextBlock();
                tbIntroduce.HorizontalAlignment = HorizontalAlignment.Left;
                tbIntroduce.TextWrapping = TextWrapping.Wrap;
                tbIntroduce.Text = BWTTag[8];
                tbIntroduce.FontSize = 13;
                Thickness TIntroduce = new Thickness();
                TIntroduce.Left = 15;
                tbIntroduce.Margin = TIntroduce;
                gIntroduce.Children.Add(tbIntroduce);
                WrapPanel_Left_Food.Children.Add(gIntroduce);
                #endregion
                GI = PG.GridInterval(20);
                WrapPanel_Left_Food.Children.Add(GI);
            }
            catch { }
            WrapPanel_Left_Food_SizeChanged(null, null);//调整位置
        }

        //跳转按钮事件
        private void Food_Jump_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string BWTTag = (string)button.Tag;
            Food_Jump_Click_Handle(sender, BWTTag);
        }
        private void Food_Jump_Click_Handle(object sender, string BWTTag)
        {
            //获取前缀(F_或FC)
            string prefix = BWTTag.Substring(0, 2);

            if (prefix == "FC")
            {
                WrapPanel_FoodAttribute.Children.Clear();

                ExpanderStackpanel ESMeats_p5 = new ExpanderStackpanel("肉类×0.5", "../Resources/GameResources/Food/FC_Meats.png");
                ESMeats_p5.Width = 185;
                ExpanderStackpanel ESMeats_1 = new ExpanderStackpanel("肉类×1", "../Resources/GameResources/Food/FC_Meats.png");
                ESMeats_1.Width = 185;
                ExpanderStackpanel ESMonsterMeats = new ExpanderStackpanel("怪兽类×1", "../Resources/GameResources/Food/FC_Monster_Meats.png");
                ESMonsterMeats.Width = 185;
                ExpanderStackpanel ESFishes_p5 = new ExpanderStackpanel("鱼类×0.5", "../Resources/GameResources/Food/FC_Fishes.png");
                ESFishes_p5.Width = 185;
                ExpanderStackpanel ESFishes_1 = new ExpanderStackpanel("鱼类×1", "../Resources/GameResources/Food/FC_Fishes.png");
                ESFishes_1.Width = 185;
                ExpanderStackpanel ESFishes_2 = new ExpanderStackpanel("鱼类×2", "../Resources/GameResources/Food/FC_Fishes.png");
                ESFishes_2.Width = 185;
                ExpanderStackpanel ESVegetables_p5 = new ExpanderStackpanel("蔬菜类×0.5", "../Resources/GameResources/Food/FC_Vegetables.png");
                ESVegetables_p5.Width = 185;
                ExpanderStackpanel ESVegetables_1 = new ExpanderStackpanel("蔬菜类×1", "../Resources/GameResources/Food/FC_Vegetables.png");
                ESVegetables_1.Width = 185;
                ExpanderStackpanel ESFruit_p5 = new ExpanderStackpanel("水果类×0.5", "../Resources/GameResources/Food/FC_Fruit.png");
                ESFruit_p5.Width = 185;
                ExpanderStackpanel ESFruit_1 = new ExpanderStackpanel("水果类×1", "../Resources/GameResources/Food/FC_Fruit.png");
                ESFruit_1.Width = 185;
                ExpanderStackpanel ESEggs_1 = new ExpanderStackpanel("蛋类×1", "../Resources/GameResources/Food/FC_Eggs.png");
                ESEggs_1.Width = 185;
                ExpanderStackpanel ESEggs_4 = new ExpanderStackpanel("蛋类×4", "../Resources/GameResources/Food/FC_Eggs.png");
                ESEggs_4.Width = 185;
                ExpanderStackpanel ESDairyProduct = new ExpanderStackpanel("乳制品类×1", "../Resources/GameResources/Food/FC_Dairy_product.png");
                ESDairyProduct.Width = 185;
                ExpanderStackpanel ESSweetener = new ExpanderStackpanel("甜味剂类×1", "../Resources/GameResources/Food/FC_Sweetener.png");
                ESSweetener.Width = 185;
                ExpanderStackpanel ESJellyfish = new ExpanderStackpanel("水母类×1", "../Resources/GameResources/Food/FC_Jellyfish.png");
                ESJellyfish.Width = 185;

                foreach (UIElement expanderStackpanel in WrapPanel_Right_Food.Children)
                {
                    foreach (UIElement buttonWithText in ((ExpanderStackpanel)expanderStackpanel).UCWrapPanel.Children)
                    {
                        string[] RightButtonTag = (string[])(((ButtonWithText)buttonWithText).UCButton.Tag);
                        if (RightButtonTag.Length > 10)
                        {
                            string RightButtonTag0 = RightButtonTag[0];
                            RightButtonTag0 = RightButtonTag0.Substring(RightButtonTag0.LastIndexOf('/') + 1, RightButtonTag0.Length - RightButtonTag0.LastIndexOf('/') - 5);
                            string Attribute = RightButtonTag[7];
                            string AttributeValue = RightButtonTag[8];
                            string Attribute_2 = RightButtonTag[9];
                            string AttributeValue_2 = RightButtonTag[10];
                            if (Attribute.Length > 2)
                            {
                                if (Attribute.Substring(0, 2) == "FC")
                                {
                                    ButtonWithPicture bwp = new ButtonWithPicture(RightButtonTag0, "GameResources/Food/");
                                    bwp.UCButton.Tag = RightButtonTag0;
                                    bwp.UCButton.Click += FoodAttribute_Click;
                                    switch (Attribute)
                                    {
                                        case "FC_Meats":
                                            if (AttributeValue == "×0.5")
                                                ESMeats_p5.UCWrapPanel.Children.Add(bwp);
                                            else
                                                ESMeats_1.UCWrapPanel.Children.Add(bwp);
                                            break;
                                        case "FC_Monster_Meats":
                                            ESMonsterMeats.UCWrapPanel.Children.Add(bwp);
                                            break;
                                        case "FC_Fishes":
                                            if (AttributeValue == "×0.5")
                                                ESFishes_p5.UCWrapPanel.Children.Add(bwp);
                                            else if (AttributeValue == "×1")
                                                ESFishes_1.UCWrapPanel.Children.Add(bwp);
                                            else
                                                ESFishes_2.UCWrapPanel.Children.Add(bwp);
                                            break;
                                        case "FC_Vegetables":
                                            if (AttributeValue == "×0.5")
                                                ESVegetables_p5.UCWrapPanel.Children.Add(bwp);
                                            else
                                                ESVegetables_1.UCWrapPanel.Children.Add(bwp);
                                            break;
                                        case "FC_Fruit":
                                            if (AttributeValue == "×0.5")
                                                ESFruit_p5.UCWrapPanel.Children.Add(bwp);
                                            else
                                                ESFruit_1.UCWrapPanel.Children.Add(bwp);
                                            break;
                                        case "FC_Eggs":
                                            if (AttributeValue == "×1")
                                                ESEggs_1.UCWrapPanel.Children.Add(bwp);
                                            else
                                                ESEggs_4.UCWrapPanel.Children.Add(bwp);
                                            break;
                                        case "FC_Dairy_product":
                                            ESDairyProduct.UCWrapPanel.Children.Add(bwp);
                                            break;
                                        case "FC_Sweetener":
                                            ESSweetener.UCWrapPanel.Children.Add(bwp);
                                            break;
                                    }
                                }
                            }

                            if (Attribute_2.Length > 2)
                            {
                                if (Attribute_2.Substring(0, 2) == "FC")
                                {
                                    ButtonWithPicture bwp = new ButtonWithPicture(RightButtonTag0, "GameResources/Food/");
                                    bwp.UCButton.Tag = RightButtonTag0;
                                    bwp.UCButton.Click += FoodAttribute_Click;
                                    switch (Attribute_2)
                                    {
                                        case "FC_Monster_Meats":
                                            ESMonsterMeats.UCWrapPanel.Children.Add(bwp);
                                            break;
                                        case "FC_Fishes":
                                            if (AttributeValue_2 == "×0.5")
                                                ESFishes_p5.UCWrapPanel.Children.Add(bwp);
                                            else if (AttributeValue_2 == "×1")
                                                ESFishes_1.UCWrapPanel.Children.Add(bwp);
                                            else
                                                ESFishes_2.UCWrapPanel.Children.Add(bwp);
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }

                WrapPanel_FoodAttribute.Children.Add(ESMeats_p5);
                WrapPanel_FoodAttribute.Children.Add(ESMeats_1);
                WrapPanel_FoodAttribute.Children.Add(ESMonsterMeats);
                if (ESFishes_p5.UCWrapPanel.Children.Count != 0)
                    WrapPanel_FoodAttribute.Children.Add(ESFishes_p5);
                WrapPanel_FoodAttribute.Children.Add(ESFishes_1);
                if (ESFishes_2.UCWrapPanel.Children.Count != 0)
                    WrapPanel_FoodAttribute.Children.Add(ESFishes_2);
                WrapPanel_FoodAttribute.Children.Add(ESVegetables_p5);
                WrapPanel_FoodAttribute.Children.Add(ESVegetables_1);
                WrapPanel_FoodAttribute.Children.Add(ESFruit_p5);
                WrapPanel_FoodAttribute.Children.Add(ESFruit_1);
                WrapPanel_FoodAttribute.Children.Add(ESEggs_1);
                WrapPanel_FoodAttribute.Children.Add(ESEggs_4);
                WrapPanel_FoodAttribute.Children.Add(ESDairyProduct);
                WrapPanel_FoodAttribute.Children.Add(ESSweetener);
                if (UI_gameversion.SelectedIndex == 2)
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
                    ESJellyfish.UCWrapPanel.Children.Add(bwpJellyfish);
                    ESJellyfish.UCWrapPanel.Children.Add(bwpDeadJellyFish);
                    ESJellyfish.UCWrapPanel.Children.Add(bwpCookedJellyfish);
                    ESJellyfish.UCWrapPanel.Children.Add(bwpDriedJellyfish);
                    WrapPanel_FoodAttribute.Children.Add(ESJellyfish);
                }
                pop_FoodAttribute.PlacementTarget = (Button)sender;
                pop_FoodAttribute.UpdateLayout();
                pop_FoodAttribute.IsOpen = true;
                Point point;
                switch (BWTTag)
                {
                    case "FC_Meats":
                        point = ESMeats_p5.TransformToVisual(WrapPanel_FoodAttribute).Transform(new Point(0, 0));
                        ScrollViewer_FoodAttribute.ScrollToVerticalOffset(point.Y);
                        break;
                    case "FC_Monster_Meats":
                        point = ESMonsterMeats.TransformToVisual(WrapPanel_FoodAttribute).Transform(new Point(0, 0));
                        ScrollViewer_FoodAttribute.ScrollToVerticalOffset(point.Y);
                        break;
                    case "FC_Fishes":
                        if (ESFishes_p5.UCWrapPanel.Children.Count != 0)
                        {
                            point = ESFishes_p5.TransformToVisual(WrapPanel_FoodAttribute).Transform(new Point(0, 0));
                            ScrollViewer_FoodAttribute.ScrollToVerticalOffset(point.Y);
                        }
                        else
                        {
                            point = ESFishes_1.TransformToVisual(WrapPanel_FoodAttribute).Transform(new Point(0, 0));
                            ScrollViewer_FoodAttribute.ScrollToVerticalOffset(point.Y);
                        }
                        break;
                    case "FC_Vegetables":
                        point = ESVegetables_p5.TransformToVisual(WrapPanel_FoodAttribute).Transform(new Point(0, 0));
                        ScrollViewer_FoodAttribute.ScrollToVerticalOffset(point.Y);
                        break;
                    case "FC_Fruit":
                        point = ESFruit_p5.TransformToVisual(WrapPanel_FoodAttribute).Transform(new Point(0, 0));
                        ScrollViewer_FoodAttribute.ScrollToVerticalOffset(point.Y);
                        break;
                    case "FC_Eggs":
                        point = ESEggs_1.TransformToVisual(WrapPanel_FoodAttribute).Transform(new Point(0, 0));
                        ScrollViewer_FoodAttribute.ScrollToVerticalOffset(point.Y);
                        break;
                    case "FC_Dairy_product":
                        point = ESDairyProduct.TransformToVisual(WrapPanel_FoodAttribute).Transform(new Point(0, 0));
                        ScrollViewer_FoodAttribute.ScrollToVerticalOffset(point.Y);
                        break;
                    case "FC_Sweetener":
                        point = ESSweetener.TransformToVisual(WrapPanel_FoodAttribute).Transform(new Point(0, 0));
                        ScrollViewer_FoodAttribute.ScrollToVerticalOffset(point.Y);
                        break;
                    case "FC_Jellyfish":
                        point = ESJellyfish.TransformToVisual(WrapPanel_FoodAttribute).Transform(new Point(0, 0));
                        ScrollViewer_FoodAttribute.ScrollToVerticalOffset(point.Y);
                        break;
                }
            }
            else
            {
                foreach (UIElement expanderStackpanel in WrapPanel_Right_Food.Children)
                {
                    foreach (UIElement buttonWithText in ((ExpanderStackpanel)expanderStackpanel).UCWrapPanel.Children)
                    {
                        string[] RightButtonTag = (string[])(((ButtonWithText)buttonWithText).UCButton.Tag);
                        string RightButtonTag0 = RightButtonTag[0];
                        RightButtonTag0 = RightButtonTag0.Substring(RightButtonTag0.LastIndexOf('/') + 1, RightButtonTag0.Length - RightButtonTag0.LastIndexOf('/') - 5);
                        if (BWTTag == RightButtonTag0)
                        {
                            Food_Click(((ButtonWithText)buttonWithText).UCButton, null);
                            Point point = ((ButtonWithText)buttonWithText).TransformToVisual(WrapPanel_Right_Food).Transform(new Point(0, 0));
                            ScrollViewer_Right_Food.ScrollToVerticalOffset(point.Y);
                        }
                    }
                }
            }
        }

        //跳转按钮(食物属性)事件
        private void FoodAttribute_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string BWTTag = (string)button.Tag;
            FoodAttribute_Click_Handle(BWTTag);
        }
        private void FoodAttribute_Click_Handle(string BWTTag)
        {
            foreach (UIElement expanderStackpanel in WrapPanel_Right_Food.Children)
            {
                foreach (UIElement buttonWithText in ((ExpanderStackpanel)expanderStackpanel).UCWrapPanel.Children)
                {
                    string[] RightButtonTag = (string[])(((ButtonWithText)buttonWithText).UCButton.Tag);
                    string RightButtonTag0 = RightButtonTag[0];
                    RightButtonTag0 = RightButtonTag0.Substring(RightButtonTag0.LastIndexOf('/') + 1, RightButtonTag0.Length - RightButtonTag0.LastIndexOf('/') - 5);
                    if (BWTTag == RightButtonTag0)
                    {
                        pop_FoodAttribute.IsOpen = false;
                        Food_Click(((ButtonWithText)buttonWithText).UCButton, null);
                        Point point = ((ButtonWithText)buttonWithText).TransformToVisual(WrapPanel_Right_Food).Transform(new Point(0, 0));
                        ScrollViewer_Right_Food.ScrollToVerticalOffset(point.Y);
                    }
                }
            }
        }

        //WrapPanel_Food_Character内Grid.Width设置为WrapPanel_Food_Character.ActualWidth
        private void WrapPanel_Left_Food_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            int LeftFoodWidth = (int)WrapPanel_Left_Food.ActualWidth;
            foreach (UIElement uielement in WrapPanel_Left_Food.Children)
            {
                if (uielement.GetType().ToString() == "System.Windows.Controls.Grid")
                {
                    ((Grid)uielement).Width = LeftFoodWidth;
                }
                if (uielement.GetType().ToString() == "System.Windows.Controls.WrapPanel")
                {
                    ((WrapPanel)uielement).Width = LeftFoodWidth;
                }
            }
        }
        //WrapPanel_Right_Food内Expander.Width设置为WrapPanel_Right_Food.ActualWidth
        private void WrapPanel_Right_Food_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            foreach (ExpanderStackpanel expanderStackpanel in WrapPanel_Right_Food.Children)
            {
                expanderStackpanel.Width = (int)WrapPanel_Right_Food.ActualWidth;
            }
        }
    }
}
