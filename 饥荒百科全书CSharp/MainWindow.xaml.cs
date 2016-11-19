//控件计数例子
//int sum = 0;
//foreach (Control vControl in WrapPanel_Right.Children)
//{
//    if (vControl is ButtonWithText)
//    {
//        sum += 1;
//    }
//}

//#region "删除旧版本文件"
//string oldVersionPath = RegeditRW.RegReadString("OldVersionPath");
//if (oldVersionPath != System.Windows.Forms.Application.ExecutablePath && oldVersionPath != "")
//{
//    try
//    {
//        File.Delete(oldVersionPath);
//    }
//    catch (Exception ex)
//    {
//        System.Windows.Forms.MessageBox.Show("删除旧版本错误，请手动删除：" + ex);
//    }
//}
//RegeditRW.RegWrite("OldVersionPath", "");
//#endregion
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml;
using 饥荒百科全书CSharp.Class;
using 饥荒百科全书CSharp.MyUserControl;

namespace 饥荒百科全书CSharp
{
    /// <summary>
    /// MainWindow窗口控制类
    /// </summary>
    

    /// <summary>
    /// MainWindow主类
    /// </summary>
    public partial class MainWindow : Window
    {
        
        #region "颜色常量"
        public const string PBCGreen = "5EB660";  //绿色
        public const string PBCKhaki = "EDB660";  //卡其布色/土黄色
        public const string PBCBlue = "337AB8";   //蓝色
        public const string PBCCyan = "15E3EA";   //青色
        public const string PBCOrange = "F6A60B"; //橙色
        public const string PBCPink = "F085D3";   //粉色
        public const string PBCYellow = "EEE815"; //黄色
        public const string PBCRed = "D8524F";    //红色
        public const string PBCPurple = "A285F0"; //紫色
        #endregion

        //检查更新实例 update(网盘)
        public static UpdatePan updatePan = new UpdatePan();

        #region "窗口可视化属性"
        Timer VisiTimer = new Timer();

        public static bool mWVisivility;
        public static bool MWVisivility
        {
            get
            {
                return mWVisivility;
            }
            set
            {
                mWVisivility = value;
            }
        }

        void VisiTimerEvent(object sender, EventArgs e)
        {
            if (MWVisivility == true)
            {
                Visibility = Visibility.Visible;
            }
            else
            {
                Visibility = Visibility.Collapsed;
            }
        }
        #endregion

        #region "窗口是否初始化属性"
        public static bool mWInit = false;
        public static bool MWInit
        {
            get
            {
                return mWInit;
            }
            set
            {
                mWInit = value;
            }
        }
        #endregion

        public static bool loadFont = false;
        #region "MainWindow"
        //MainWindow构造函数
        public MainWindow()
        {
            //读取注册表(必须在初始化之前读取)
            ////背景图片
            string bg = RegeditRW.RegReadString("Background");
            double bgStretch = RegeditRW.RegRead("BackgroundStretch");
            ////透明度
            double bgAlpha = RegeditRW.RegRead("BGAlpha");
            double bgPanelAlpha = RegeditRW.RegRead("BGPanelAlpha");
            double windowAlpha = RegeditRW.RegRead("WindowAlpha");
            ////窗口大小
            double mainWindowHeight = RegeditRW.RegRead("MainWindowHeight");
            double mainWindowWidth = RegeditRW.RegRead("MainWindowWidth");
            //字体
            string mainWindowFont = RegeditRW.RegReadString("MainWindowFont");
            //设置菜单
            double winTopmost = RegeditRW.RegRead("Topmost");
            ////游戏版本
            double gameVersion = RegeditRW.RegRead("GameVersion");
            //初始化
            InitializeComponent();
            //窗口缩放
            SourceInitialized += delegate (object sender, EventArgs e){ _HwndSource = PresentationSource.FromVisual((Visual)sender) as HwndSource;};
            MouseMove += new System.Windows.Input.MouseEventHandler(Window_MouseMove);
            //mainWindow初始化标志
            MWInit = true;
            //设置字体
            if (mainWindowFont == "")
            {
                RegeditRW.RegWrite("MainWindowFont", "微软雅黑");
                mainWindowFont = "微软雅黑";
            }
            mainWindow.FontFamily = new FontFamily(mainWindowFont);
            //版本初始化
            UI_Version.Text = "v" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            //窗口可视化计时器
            VisiTimer.Enabled = true;
            VisiTimer.Interval = 200;
            VisiTimer.Tick += new EventHandler(VisiTimerEvent);
            VisiTimer.Start();
            //设置光标资源字典路径
            cursorDictionary.Source = new Uri("Dictionary/CursorDictionary.xaml", UriKind.Relative);
            //显示窗口
            MWVisivility = true;
            //右侧面板Visibility属性初始化
            RightPanelVisibility("Welcome");

            //窗口置顶
            if (winTopmost == 1)
            {
                Se_button_Topmost_Click(null, null);
            }
            //设置背景
            if (bg == "")
            {
                Se_BG_Alpha_Text.Foreground = Brushes.Silver;
                Se_BG_Alpha.IsEnabled = false;
            }
            else
            {
                Se_BG_Alpha_Text.Foreground = Brushes.Black;
                try
                {
                    var brush = new ImageBrush();
                    brush.ImageSource = new BitmapImage(new Uri(bg));
                    UI_BackGroundBorder.Background = brush;
                }
                catch
                {
                    Visi.VisiCol(true, UI_BackGroundBorder);
                }
            }
            //设置背景拉伸方式
            if (bgStretch == 0)
            {
                bgStretch = 2;
            }
            Se_ComboBox_Background_Stretch.SelectedIndex = (int)bgStretch - 1;
            //设置背景透明度
            if (bgAlpha == 0)
            {
                bgAlpha = 101;
            }
            Se_BG_Alpha.Value = bgAlpha - 1;
            Se_BG_Alpha_Text.Text = "背景不透明度：" + (int)Se_BG_Alpha.Value + "%";
            UI_BackGroundBorder.Opacity = (bgAlpha - 1) / 100;
            //设置面板透明度
            if (bgPanelAlpha == 0)
            {
                bgPanelAlpha = 61;
            }
            Se_Panel_Alpha.Value = bgPanelAlpha - 1;
            Se_Panel_Alpha_Text.Text = "面板不透明度：" + (int)Se_Panel_Alpha.Value + "%";
            RightGrid.Background.Opacity = (bgPanelAlpha - 1) / 100;
            //设置窗口透明度
            if (windowAlpha == 0)
            {
                windowAlpha = 101;
            }
            Se_Window_Alpha.Value = windowAlpha - 1;
            Se_Window_Alpha_Text.Text = "窗口不透明度：" + (int)Se_Window_Alpha.Value + "%";
            Opacity = (windowAlpha - 1) / 100;
            //设置高度和宽度
            Width = mainWindowWidth;
            Height = mainWindowHeight;
            //设置游戏版本
            UI_gameversion.SelectedIndex = (int)gameVersion;
        }
        //MainWindow窗口加载
        private void mainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            new KeyboardHandler(this);//加载快捷键
            LoadGameVersionXml();//加载游戏版本Xml文件

            foreach (string str in rF())//加载字体
            {
                TextBlock TB = new TextBlock();
                TB.Text = str;
                TB.FontFamily = new FontFamily(str);
                Se_ComboBox_Font.Items.Add(TB);
            }
            string mainWindowFont = RegeditRW.RegReadString("MainWindowFont");
            List<string> Ls = new List<string>();
            foreach (TextBlock TB in Se_ComboBox_Font.Items)
            {
                Ls.Add(TB.Text);
            }
            Se_ComboBox_Font.SelectedIndex = Ls.IndexOf(mainWindowFont);
            loadFont = true;
        }
        //加载游戏版本Xml文件
        private void LoadGameVersionXml()
        {
            XmlDocument doc = new XmlDocument();
            Assembly assembly = Assembly.GetEntryAssembly();
            switch (UI_gameversion.SelectedIndex)
            {
                case 0:
                    Stream streamDS = assembly.GetManifestResourceStream("饥荒百科全书CSharp.Resources.XML.DSXml.xml");
                    doc.Load(streamDS);
                    XmlNode listDS = doc.SelectSingleNode("DS");
                    HandleXml(listDS);
                    break;
                case 1:
                    Stream streamRoG = assembly.GetManifestResourceStream("饥荒百科全书CSharp.Resources.XML.RoGXml.xml");
                    doc.Load(streamRoG);
                    XmlNode listRoG = doc.SelectSingleNode("RoG");
                    HandleXml(listRoG);
                    break;
                case 2:
                    Stream streamSW = assembly.GetManifestResourceStream("饥荒百科全书CSharp.Resources.XML.SWXml.xml");
                    doc.Load(streamSW);
                    XmlNode listSW = doc.SelectSingleNode("SW");
                    HandleXml(listSW);
                    break;
                case 3:
                    Stream streamDST = assembly.GetManifestResourceStream("饥荒百科全书CSharp.Resources.XML.DSTXml.xml");
                    doc.Load(streamDST);
                    XmlNode listDST = doc.SelectSingleNode("DST");
                    HandleXml(listDST);
                    break;
                case 4:
                    Stream streamTencent = assembly.GetManifestResourceStream("饥荒百科全书CSharp.Resources.XML.TencentXml.xml");
                    doc.Load(streamTencent);
                    XmlNode listTencent = doc.SelectSingleNode("Tencent");
                    HandleXml(listTencent);
                    break;
            }
        }
        private void HandleXml(XmlNode list)
        {
            if (WrapPanel_Left_Character != null)
            {
                WrapPanel_Left_Character.Children.Clear();
            }
            if (WrapPanel_Right_Character != null)
            {
                WrapPanel_Right_Character.Children.Clear();
            }
            foreach (XmlNode Node in list)
            {
                #region "人物"
                if (Node.Name == "CharacterNode")
                {
                    //messagebox.show(node.attributes["name"].value);
                    foreach (XmlNode childNode in Node)
                    {
                        if (childNode.Name == "Character")
                        {
                            string Picture = "";
                            string Name = "";
                            string EnName = "";
                            string Motto = "";
                            string Descriptions_1 = "";
                            string Descriptions_2 = "";
                            string Descriptions_3 = "";
                            string Health = "";
                            string Hunger = "";
                            string Sanity = "";
                            string LogMeter = "";
                            string Damage = "";
                            string DamageDay = "";
                            string DamageDusk = "";
                            string DamageNight = "";
                            string Introduce = "";
                            foreach (XmlNode Character in childNode)
                            {
                                switch (Character.Name)
                                {
                                    case "Picture":
                                        Picture = Character.InnerText;
                                        break;
                                    case "Name":
                                        Name = Character.InnerText;
                                        break;
                                    case "EnName":
                                        EnName = Character.InnerText;
                                        break;
                                    case "Motto":
                                        Motto = Character.InnerText;
                                        break;
                                    case "Descriptions_1":
                                        Descriptions_1 = Character.InnerText;
                                        break;
                                    case "Descriptions_2":
                                        Descriptions_2 = Character.InnerText;
                                        break;
                                    case "Descriptions_3":
                                        Descriptions_3 = Character.InnerText;
                                        break;
                                    case "Health":
                                        Health = Character.InnerText;
                                        break;
                                    case "Hunger":
                                        Hunger = Character.InnerText;
                                        break;
                                    case "Sanity":
                                        Sanity = Character.InnerText;
                                        break;
                                    case "LogMeter":
                                        LogMeter = Character.InnerText;
                                        break;
                                    case "Damage":
                                        Damage = Character.InnerText;
                                        break;
                                    case "DamageDay":
                                        DamageDay = Character.InnerText;
                                        break;
                                    case "DamageDusk":
                                        DamageDusk = Character.InnerText;
                                        break;
                                    case "DamageNight":
                                        DamageNight = Character.InnerText;
                                        break;
                                    case "Introduce":
                                        Introduce = Character.InnerText;
                                        break;
                                }
                            }
                            ButtonWithText BWT = new ButtonWithText();
                            BWT.Height = 205;
                            BWT.Width = 140;
                            BWT.ButtonGrid.Height = 190;
                            BWT.ButtonGrid.Width = 140;
                            BWT.GridPictureHeight.Height = new GridLength(160);
                            BWT.UCImage.Height = 160;
                            BWT.UCImage.Width = 140;
                            BWT.UCImage.Source = RSN.PictureShortName(Picture);
                            BWT.UCTextBlock.FontSize = 20;
                            BWT.UCTextBlock.Text = Name;
                            string[] BWTTag = { Picture, Name, EnName, Motto, Descriptions_1, Descriptions_2, Descriptions_3, Health, Hunger, Sanity, LogMeter, Damage, DamageDay, DamageDusk, DamageNight, Introduce };
                            object obj = BWTTag;
                            if (Name == "威尔逊")
                            {
                                Character_Click_Handle(BWTTag);
                            }
                            BWT.UCButton.Tag = obj;
                            BWT.UCButton.Click += Character_Click;
                            try
                            {
                                WrapPanel_Right_Character.Children.Add(BWT);
                            }
                            catch { }
                        }
                    }
                }
                #endregion
                #region "食物"
                if (Node.Name == "FoodNode")
                {
                    foreach (XmlNode childNode in Node)
                    {
                        if (childNode.Name == "Food")
                        {
                            string Picture = "";
                            string Name = "";
                            string EnName = "";
                            string Introduce = "";
                            foreach (XmlNode Food in childNode)
                            {
                                if (Food.Name == "Picture")
                                {
                                    Picture = Food.InnerText;
                                }
                                if (Food.Name == "Name")
                                {
                                    Name = Food.InnerText;
                                }
                            }
                            ButtonWithText BWT = new ButtonWithText();
                            BWT.UCImage.Source = RSN.PictureShortName(Picture);
                            BWT.UCTextBlock.Text = Name;
                            //BWT.UCButton.Click += test_click;
                            WrapPanel_Right_Food.Children.Add(BWT);
                        }
                    }
                }
                #endregion
            }
        }
        //Character面板Click事件
        private void Character_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Button button = (System.Windows.Controls.Button)sender;
            string[] BWTTag = (string[])button.Tag;//获取参数
            Character_Click_Handle(BWTTag);
        }
        //WrapPanel_Left_Character控件创建事件
        private void Character_Click_Handle(string[] BWTTag)
        {
            try
            {
                WrapPanel_Left_Character.Children.Clear();//清空WrapPanel_Left_Character
                #region "图片"
                Grid gPicture = new Grid();
                gPicture.Height = 180;
                Image iPicture = new Image();
                iPicture.Height = 160;
                iPicture.Width = 140;
                iPicture.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                iPicture.Source = RSN.PictureShortName(BWTTag[0]);
                Thickness tPicture = new Thickness();
                tPicture.Top = 20;
                iPicture.Margin = tPicture;
                gPicture.Children.Add(iPicture);
                WrapPanel_Left_Character.Children.Add(gPicture);
                #endregion
                #region "中文名"
                Grid gName = new Grid();
                gName.Height = 38.6;
                TextBlock tbName = new TextBlock();
                tbName.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                tbName.Text = BWTTag[1];
                tbName.FontSize = 26.667;
                Thickness TName = new Thickness();
                TName.Top = 5;
                tbName.Margin = TName;
                gName.Children.Add(tbName);
                WrapPanel_Left_Character.Children.Add(gName);
                #endregion
                #region "英文名"
                Grid gEnName = new Grid();
                gEnName.Height = 33.6;
                TextBlock tbEnName = new TextBlock();
                tbEnName.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                tbEnName.Text = BWTTag[2];
                tbEnName.FontSize = 26.667;
                Thickness TEnName = new Thickness();
                TEnName.Top = -5;
                tbEnName.Margin = TEnName;
                gEnName.Children.Add(tbEnName);
                WrapPanel_Left_Character.Children.Add(gEnName);
                #endregion
                #region "座右铭"
                if (BWTTag[3] != "")
                {
                    Grid gMotto = new Grid();
                    gMotto.Height = 25;
                    TextBlock tbMotto = new TextBlock();
                    tbMotto.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                    tbMotto.Text = BWTTag[3];
                    tbMotto.FontSize = 16;
                    Thickness TMotto = new Thickness();
                    TMotto.Top = 5;
                    tbMotto.Margin = TMotto;
                    gMotto.Children.Add(tbMotto);
                    WrapPanel_Left_Character.Children.Add(gMotto);
                }
                #endregion
                #region "描述_1"
                if (BWTTag[4] != "")
                {
                    Grid gDescriptions_1 = new Grid();
                    gDescriptions_1.Height = 27.6;
                    TextBlock tbDescriptions_1 = new TextBlock();
                    tbDescriptions_1.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    tbDescriptions_1.Text = BWTTag[4];
                    tbDescriptions_1.FontSize = 14;
                    Thickness TDescriptions_1 = new Thickness();
                    TDescriptions_1.Top = 10;
                    TDescriptions_1.Left = 25;
                    tbDescriptions_1.Margin = TDescriptions_1;
                    gDescriptions_1.Children.Add(tbDescriptions_1);
                    WrapPanel_Left_Character.Children.Add(gDescriptions_1);
                }
                #endregion
                #region "描述_2"
                if (BWTTag[5] != "")
                {
                    Grid gDescriptions_2 = new Grid();
                    gDescriptions_2.Height = 17.6;
                    TextBlock tbDescriptions_2 = new TextBlock();
                    tbDescriptions_2.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    tbDescriptions_2.Text = BWTTag[5];
                    tbDescriptions_2.FontSize = 14;
                    Thickness TDescriptions_2 = new Thickness();
                    TDescriptions_2.Left = 25;
                    tbDescriptions_2.Margin = TDescriptions_2;
                    gDescriptions_2.Children.Add(tbDescriptions_2);
                    WrapPanel_Left_Character.Children.Add(gDescriptions_2);
                }
                #endregion
                #region "描述_3"
                if (BWTTag[6] != "")
                {
                    Grid gDescriptions_3 = new Grid();
                    gDescriptions_3.Height = 17.6;
                    TextBlock tbDescriptions_3 = new TextBlock();
                    tbDescriptions_3.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    tbDescriptions_3.Text = BWTTag[6];
                    tbDescriptions_3.FontSize = 14;
                    Thickness TDescriptions_3 = new Thickness();
                    TDescriptions_3.Left = 25;
                    tbDescriptions_3.Margin = TDescriptions_3;
                    gDescriptions_3.Children.Add(tbDescriptions_3);
                    WrapPanel_Left_Character.Children.Add(gDescriptions_3);
                }
                #endregion
                #region "生命"
                if (BWTTag[7] != "")
                {
                    Grid gHealth = new Grid();
                    gHealth.Height = 40;
                    PropertyBar pbHealth = new PropertyBar();
                    pbHealth.UCTextBlockName.Width = 57;
                    pbHealth.UCTextBlockName.Text = "生命";
                    pbHealth.UCProgressBar.Value = Convert.ToDouble(BWTTag[7]) / 3;
                    pbHealth.UCProgressBar.Foreground = BC.brushConverter(PBCGreen);
                    pbHealth.UCTextBlockValue.Width = 32;
                    pbHealth.UCTextBlockValue.Text = BWTTag[7];
                    Thickness THealth = new Thickness();
                    THealth.Top = 25;
                    pbHealth.Margin = THealth;
                    gHealth.Children.Add(pbHealth);
                    WrapPanel_Left_Character.Children.Add(gHealth);
                }
                #endregion
                #region "饥饿"
                if (BWTTag[8] != "")
                {
                    Grid gHunger = new Grid();
                    gHunger.Height = 25;
                    PropertyBar pbHunger = new PropertyBar();
                    pbHunger.UCTextBlockName.Width = 57;
                    pbHunger.UCTextBlockName.Text = "饥饿";
                    pbHunger.UCProgressBar.Value = Convert.ToDouble(BWTTag[8]) / 3;
                    pbHunger.UCProgressBar.Foreground = BC.brushConverter(PBCOrange);
                    pbHunger.UCTextBlockValue.Width = 32;
                    pbHunger.UCTextBlockValue.Text = BWTTag[8];
                    Thickness THunger = new Thickness();
                    THunger.Top = 10;
                    pbHunger.Margin = THunger;
                    gHunger.Children.Add(pbHunger);
                    WrapPanel_Left_Character.Children.Add(gHunger);
                }
                #endregion
                #region "精神"
                if (BWTTag[9] != "")
                {
                    Grid gSanity = new Grid();
                    gSanity.Height = 25;
                    PropertyBar pbSanity = new PropertyBar();
                    pbSanity.UCTextBlockName.Width = 57;
                    pbSanity.UCTextBlockName.Text = "精神";
                    pbSanity.UCProgressBar.Value = Convert.ToDouble(BWTTag[9]) / 2.5;
                    pbSanity.UCProgressBar.Foreground = BC.brushConverter(PBCRed);
                    pbSanity.UCTextBlockValue.Width = 32;
                    pbSanity.UCTextBlockValue.Text = BWTTag[9];
                    Thickness TSanity = new Thickness();
                    TSanity.Top = 10;
                    pbSanity.Margin = TSanity;
                    gSanity.Children.Add(pbSanity);
                    WrapPanel_Left_Character.Children.Add(gSanity);
                }
                #endregion
                #region "树木值"
                if (BWTTag[10] != "")
                {
                    Grid gDamage = new Grid();
                    gDamage.Height = 25;
                    PropertyBar pbDamage = new PropertyBar();
                    pbDamage.UCTextBlockName.Width = 57;
                    pbDamage.UCTextBlockName.Text = "树木值";
                    pbDamage.UCProgressBar.Value = Convert.ToDouble(BWTTag[10]);
                    pbDamage.UCProgressBar.Foreground = BC.brushConverter(PBCPink);
                    pbDamage.UCTextBlockValue.Width = 32;
                    pbDamage.UCTextBlockValue.Text = BWTTag[10];
                    Thickness TDamage = new Thickness();
                    TDamage.Top = 10;
                    pbDamage.Margin = TDamage;
                    gDamage.Children.Add(pbDamage);
                    WrapPanel_Left_Character.Children.Add(gDamage);
                }
                #endregion
                #region "伤害"
                if (BWTTag[11] != "")
                {
                    Grid gDamage = new Grid();
                    gDamage.Height = 25;
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
                    Thickness TDamage = new Thickness();
                    TDamage.Top = 10;
                    pbDamage.Margin = TDamage;
                    gDamage.Children.Add(pbDamage);
                    WrapPanel_Left_Character.Children.Add(gDamage);
                }
                #endregion
                #region "伤害(白天)"
                if (BWTTag[12] != "")
                {
                    Grid gDamage = new Grid();
                    gDamage.Height = 25;
                    PropertyBar pbDamage = new PropertyBar();
                    pbDamage.UCTextBlockName.Width = 57;
                    pbDamage.UCTextBlockName.Text = "伤害(白天)";
                    pbDamage.UCProgressBar.Value = Convert.ToDouble(BWTTag[12]) * 2.5;
                    pbDamage.UCProgressBar.Foreground = BC.brushConverter(PBCBlue);
                    pbDamage.UCTextBlockValue.Width = 32;
                    pbDamage.UCTextBlockValue.Text = BWTTag[12];
                    Thickness TDamage = new Thickness();
                    TDamage.Top = 10;
                    pbDamage.Margin = TDamage;
                    gDamage.Children.Add(pbDamage);
                    WrapPanel_Left_Character.Children.Add(gDamage);
                }
                #endregion
                #region "伤害(黄昏)"
                if (BWTTag[13] != "")
                {
                    Grid gDamage = new Grid();
                    gDamage.Height = 25;
                    PropertyBar pbDamage = new PropertyBar();
                    pbDamage.UCTextBlockName.Width = 57;
                    pbDamage.UCTextBlockName.Text = "伤害(黄昏)";
                    pbDamage.UCProgressBar.Value = Convert.ToDouble(BWTTag[13]) * 2.5;
                    pbDamage.UCProgressBar.Foreground = BC.brushConverter(PBCBlue);
                    pbDamage.UCTextBlockValue.Width = 32;
                    pbDamage.UCTextBlockValue.Text = BWTTag[13];
                    Thickness TDamage = new Thickness();
                    TDamage.Top = 10;
                    pbDamage.Margin = TDamage;
                    gDamage.Children.Add(pbDamage);
                    WrapPanel_Left_Character.Children.Add(gDamage);
                }
                #endregion
                #region "伤害(夜晚)"
                if (BWTTag[14] != "")
                {
                    Grid gDamage = new Grid();
                    gDamage.Height = 25;
                    PropertyBar pbDamage = new PropertyBar();
                    pbDamage.UCTextBlockName.Width = 57;
                    pbDamage.UCTextBlockName.Text = "伤害(夜晚)";
                    pbDamage.UCProgressBar.Value = Convert.ToDouble(BWTTag[14]) * 2.5;
                    pbDamage.UCProgressBar.Foreground = BC.brushConverter(PBCBlue);
                    pbDamage.UCTextBlockValue.Width = 32;
                    pbDamage.UCTextBlockValue.Text = BWTTag[14];
                    Thickness TDamage = new Thickness();
                    TDamage.Top = 10;
                    pbDamage.Margin = TDamage;
                    gDamage.Children.Add(pbDamage);
                    WrapPanel_Left_Character.Children.Add(gDamage);
                }
                #endregion
                #region "介绍"
                Grid gIntroduce = new Grid();
                TextBlock tbIntroduce = new TextBlock();
                tbIntroduce.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                tbIntroduce.TextWrapping = TextWrapping.Wrap;
                tbIntroduce.Text = BWTTag[15];
                tbIntroduce.FontSize = 13;
                Thickness TIntroduce = new Thickness();
                TIntroduce.Top = 20;
                TIntroduce.Left = 15;
                tbIntroduce.Margin = TIntroduce;
                gIntroduce.Children.Add(tbIntroduce);
                WrapPanel_Left_Character.Children.Add(gIntroduce);
                #endregion
                WrapPanel_Left_Character_SizeChanged(null, null);//调整位置
            }
            catch { }
        }
        //WrapPanel_Left_Character内Grid.Width设置为WrapPanel_Left_Character.ActualWidth
        private void WrapPanel_Left_Character_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            foreach (Grid grid in WrapPanel_Left_Character.Children)
            {
                grid.Width = (int)WrapPanel_Left_Character.ActualWidth;
            }
        }

        #endregion

        #region "主页面板"
        //官网
        private void W_button_official_website_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("http://www.jihuangbaike.com");
        }
        //Mod
        private void W_button_Mods_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("http://steamcommunity.com/sharedfiles/filedetails/?id=635215011");
        }
        //DS新闻
        private void W_button_DSNews_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("http://store.steampowered.com/news/?appids=219740");
        }
        //DST新闻
        private void W_button_DSTNewS_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("http://store.steampowered.com/news/?appids=322330");
        }
        //群二维码
        private void W_button_QRCode_Qun_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("tencent://groupwpa/?subcmd=all&param=7B2267726F757055696E223A3538303333323236382C2274696D655374616D70223A313437303132323238337D0A");
        }
        #endregion

        #region "设置面板"
        //老板键
        private void Se_BossKey_Key_KeyDown(Object sender, System.Windows.Input.KeyEventArgs e)
        {
            byte PressAlt = 0; //Alt
            byte PressCtrl = 0; //Ctrl
            byte PressShift = 0; //Shift
            int ControlKeys = 0; //Alt + Ctrl + Shift的值
            string PreString = ""; //前面的值
            string MainKey = ""; //主值

            //字母 || F1-F12 || 小键盘区的数字 || 空格?
            if ((e.Key >= Key.A && e.Key <= Key.Z) || (e.Key >= Key.F1 && e.Key <= Key.F12) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || (e.Key == Key.Space))
            {
                e.Handled = true;
                MainKey = e.Key.ToString();
            }
            //字母区上面的数字
            if (e.Key >= Key.D0 && e.Key <= Key.D9)
            {
                e.Handled = true;
                MainKey = e.Key.ToString().Replace("D", "");
            }
            //Alt Ctrl Shift键判断
            if ((System.Windows.Forms.Control.ModifierKeys & Keys.Alt) == Keys.Alt)
                PressAlt = 1;
            else
                PressAlt = 0;

            if ((System.Windows.Forms.Control.ModifierKeys & Keys.Control) == Keys.Control)
                PressCtrl = 2;
            else
                PressCtrl = 0;

            if ((System.Windows.Forms.Control.ModifierKeys & Keys.Shift) == Keys.Shift)
                PressShift = 4;
            else
                PressShift = 0;

            ControlKeys = PressAlt + PressCtrl + PressShift;
            switch (ControlKeys)
            {
                case 1:
                    PreString = "Alt + ";
                    break;
                case 2:
                    PreString = "Ctrl + ";
                    break;
                case 3:
                    PreString = "Ctrl + Alt + ";
                    break;
                case 4:
                    PreString = "Shift + ";
                    break;
                case 5:
                    PreString = "Alt + Shift + ";
                    break;
                case 6:
                    PreString = "Ctrl + Shift + ";
                    break;
                case 7:
                    PreString = "Ctrl + Alt + Shift + ";
                    break;
                default:
                    PreString = "";
                    break;
            }
            //输出值
            if (MainKey != "")
            {
                Se_BossKey_Key.Content = PreString + MainKey;
            }
            else
            {
                Se_BossKey_Key.Content = "Ctrl + Alt + B";
            }
        }
        #endregion

    }
}