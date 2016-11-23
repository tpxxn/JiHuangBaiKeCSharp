using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using 饥荒百科全书CSharp.Class;
using 饥荒百科全书CSharp.MyUserControl;

namespace 饥荒百科全书CSharp
{
    /// <summary>
    /// MainWindowFood
    /// </summary>
    public partial class MainWindow : Window
    {
        //Food面板Click事件
        private void Food_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string[] BWTTag = (string[])button.Tag;//获取参数
            Food_Click_Handle(BWTTag);
        }

        //WrapPanel_Left_Food控件创建事件
        private void Food_Click_Handle(string[] BWTTag)
        {
            //string[] BWTTag = { Picture, Name, EnName, PortableCrockPot, Health, Hunger, Sanity, Perish, Cooktime, Priority, NeedPicture_1, Need_1, NeedPicture_or, Need_or, NeedPicture_2, Need_2, NeedPicture_3, Need_3, Restrictions_1, RestrictionsAttributes_1, Restrictions_2, RestrictionsAttributes_2, Restrictions_3, RestrictionsAttributes_3, Restrictions_4, RestrictionsAttributes_4, Restrictions_5, RestrictionsAttributes_5, Recommend_1, Recommend_2, Recommend_3, Recommend_4, Introduce };
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
                Thickness TEnName = new Thickness();
                TEnName.Top = -5;
                tbEnName.Margin = TEnName;
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
                    if(propertyHealth< 0)
                        pbHealth = new PropertyBar(true);
                    else
                        pbHealth = new PropertyBar();
                    pbHealth.UCTextBlockName.Width = 30;
                    pbHealth.UCTextBlockName.Text = "生命";
                    pbHealth.UCProgressBar.Value = Math.Abs(propertyHealth);
                    pbHealth.UCProgressBar.Foreground = BC.brushConverter(PBCGreen);
                    pbHealth.UCTextBlockValue.Width = 37;
                    pbHealth.UCTextBlockValue.Text = BWTTag[4];
                    gHealth.Children.Add(pbHealth);
                    WrapPanel_Left_Food.Children.Add(gHealth);
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
                    pbHunger.UCTextBlockValue.Width = 37;
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
                    pbSanity.UCTextBlockValue.Width = 37;
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
                    pbPerish.UCTextBlockValue.Width = 37;
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
                    pbCooktime.UCTextBlockValue.Width = 37;
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
                    pbPriority.UCTextBlockValue.Width = 37;
                    pbPriority.UCTextBlockValue.Text = BWTTag[9];
                    gPriority.Children.Add(pbPriority);
                    WrapPanel_Left_Food.Children.Add(gPriority);
                }
                #endregion
                GI = PG.GridInterval(20);
                WrapPanel_Left_Food.Children.Add(GI);
                #region "烹饪需求  BWTTag[10-17]"
                WrapPanel_Left_Food.Children.Add(PG.GridTag("烹饪需求："));

                #endregion
                GI = PG.GridInterval(20);
                WrapPanel_Left_Food.Children.Add(GI);
                #region "填充限制  BWTTag[18-27]"
                WrapPanel_Left_Food.Children.Add(PG.GridTag("填充限制："));

                #endregion
                GI = PG.GridInterval(20);
                WrapPanel_Left_Food.Children.Add(GI);
                #region "推荐食谱 BWTTag[28-31]"
                WrapPanel_Left_Food.Children.Add(PG.GridTag("推荐食谱："));

                Grid gRecommendContent = PG.GridInit(35);
                WrapPanel sRecommendContent = new WrapPanel();
                sRecommendContent.HorizontalAlignment = HorizontalAlignment.Center;
                ButtonWithPicture bRecommed1 = new ButtonWithPicture(BWTTag[28], "GameResources/Food/");
                ButtonWithPicture bRecommed2 = new ButtonWithPicture(BWTTag[29], "GameResources/Food/");
                ButtonWithPicture bRecommed3 = new ButtonWithPicture(BWTTag[30], "GameResources/Food/");
                ButtonWithPicture bRecommed4 = new ButtonWithPicture(BWTTag[31], "GameResources/Food/");
                sRecommendContent.Children.Add(bRecommed1);
                sRecommendContent.Children.Add(bRecommed2);
                sRecommendContent.Children.Add(bRecommed3);
                sRecommendContent.Children.Add(bRecommed4);
                gRecommendContent.Children.Add(sRecommendContent);
                WrapPanel_Left_Food.Children.Add(gRecommendContent);
                #endregion
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
