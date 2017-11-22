using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using 饥荒百科全书CSharp.Class;
using 饥荒百科全书CSharp.MyUserControl;
using 饥荒百科全书CSharp.View;

namespace 饥荒百科全书CSharp
{
    /// <summary>
    /// MainWindow窗口控制类
    /// </summary>
    public partial class MainWindow : Window
    {
        #region "颜色常量"
        public const string PbcGreen = "5EB660";  //绿色
        public const string PbcKhaki = "EDB660";  //卡其布色/土黄色
        public const string PbcBlue = "337AB8";   //蓝色
        public const string PbcCyan = "15E3EA";   //青色
        public const string PbcOrange = "F6A60B"; //橙色
        public const string PbcPink = "F085D3";   //粉色
        public const string PbcYellow = "EEE815"; //黄色
        public const string PbcRed = "D8524F";    //红色
        public const string PbcPurple = "A285F0"; //紫色
        public const string PbcBorderCyan = "B2ECED"; //紫色
        #endregion

        #region "窗口尺寸/拖动窗口"
        //引用光标资源字典
        private static readonly ResourceDictionary CursorDictionary = new ResourceDictionary();
        private const int WmSyscommand = 0x112;
        private HwndSource _hwndSource;
        private enum ResizeDirection
        {
            Left = 1,
            Right = 2,
            Top = 3,
            TopLeft = 4,
            TopRight = 5,
            Bottom = 6,
            BottomLeft = 7,
            BottomRight = 8,
        }
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed) return;
            if (e.OriginalSource is FrameworkElement element && !element.Name.Contains("Resize"))
            {
                Cursor = (Cursor)CursorDictionary["CursorPointer"];
            }
        }
        private void ResizePressed(object sender, MouseEventArgs e)
        {
            if (!(sender is FrameworkElement element)) return;
            var direction = (ResizeDirection)Enum.Parse(typeof(ResizeDirection), element.Name.Replace("Resize", ""));

            switch (direction)
            {
                case ResizeDirection.Left:
                    Cursor = (Cursor)CursorDictionary["CursorHorz"];
                    break;
                case ResizeDirection.Right:
                    Cursor = (Cursor)CursorDictionary["CursorHorz"];
                    break;
                case ResizeDirection.Top:
                    Cursor = (Cursor)CursorDictionary["CursorVert"];
                    break;
                case ResizeDirection.Bottom:
                    Cursor = (Cursor)CursorDictionary["CursorVert"];
                    break;
                case ResizeDirection.TopLeft:
                    Cursor = (Cursor)CursorDictionary["CursorDgn1"];
                    break;
                case ResizeDirection.BottomRight:
                    Cursor = (Cursor)CursorDictionary["CursorDgn1"];
                    break;
                case ResizeDirection.TopRight:
                    Cursor = (Cursor)CursorDictionary["CursorDgn2"];
                    break;
                case ResizeDirection.BottomLeft:
                    Cursor = (Cursor)CursorDictionary["CursorDgn2"];
                    break;
            }
            if (e.LeftButton == MouseButtonState.Pressed)
                ResizeWindow(direction);
        }

        private void ResizeWindow(ResizeDirection direction)
        {
            SendMessage(_hwndSource.Handle, WmSyscommand, (IntPtr)(61440 + direction), IntPtr.Zero);
        }

        //MainWindow拖动窗口
        private void MainWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var positionUiGrid = e.GetPosition(UiGrid);
            var positionRightGridFrame = e.GetPosition(RightFrame);
            var positionRightGridSetting = e.GetPosition(RightGridSetting);
            var inUiGrid = false;
            var inFrame = false;
            var inSetting = false;
            if (positionUiGrid.X >= 0 && positionUiGrid.X < UiGrid.ActualWidth && positionUiGrid.Y >= 0 && positionUiGrid.Y < UiGrid.ActualHeight)
            {
                inUiGrid = true;
            }
            if (positionRightGridFrame.X >= 0 && positionRightGridFrame.X < RightFrame.ActualWidth && positionRightGridFrame.Y >= 0 && positionRightGridFrame.Y < RightFrame.ActualHeight)
            {
                inFrame = true;
            }
            if (positionRightGridSetting.X >= 0 && positionRightGridSetting.X < RightGridSetting.ActualWidth && positionRightGridSetting.Y >= 0 && positionRightGridSetting.Y < RightGridSetting.ActualHeight)
            {
                inSetting = true;
            }
            // 如果鼠标位置在标题栏内，允许拖动  
            if (e.LeftButton != MouseButtonState.Pressed || (!inUiGrid && !inFrame && !inSetting)) return;
            Cursor = (Cursor)CursorDictionary["CursorMove"];
            DragMove();
        }

        /// <summary>
        /// 切换鼠标指针为默认状态
        /// </summary>
        private void MainWindow_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Cursor = (Cursor)CursorDictionary["CursorPointer"];
        }

        /// <summary>
        /// 双击标题栏最大化
        /// </summary>
        private void MainWindow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var positionUiGrid = e.GetPosition(UiGrid);
            if ((!(positionUiGrid.X >= 0) || !(positionUiGrid.X < UiGrid.ActualWidth) || !(positionUiGrid.Y >= 0) ||
                 !(positionUiGrid.Y < UiGrid.ActualHeight))) return;
            if (UiBtnMaximized.Visibility == Visibility.Collapsed)
            {
                UI_btn_normal_Click(null, null);
            }
            else
            {
                UI_btn_maximized_Click(null, null);
            }
        }

        /// <summary>
        /// MainWindow窗口尺寸改变
        /// </summary>
        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //最大化
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
                UI_btn_maximized_Click(null, null);
            }
            //设置版本号位置
            UiVersion.Margin = new Thickness(10, mainWindow.ActualHeight - 35, 0, 0);
            //左侧面板高度
            LeftCanvas.Height = mainWindow.ActualHeight - 2;
            LeftWrapPanel.Height = mainWindow.ActualHeight - 2;
            RegeditRw.RegWrite("MainWindowHeight", ActualHeight);
            RegeditRw.RegWrite("MainWindowWidth", ActualWidth);
        }
        #endregion

        #region "右上角按钮"
        #region "搜索框清除按钮显示/隐藏"
        //设置清除按钮可见性
        private void UI_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            Visi.VisiCol(UiSearch.Text == "", UiSearchClear);
        }
        //清除按钮
        private void UI_search_clear_Click(object sender, RoutedEventArgs e)
        {
            UiSearch.Text = "";
            Visi.VisiCol(true, UiSearchClear);
        }
        #endregion

        #region "游戏版本"
        //游戏版本选择
        private void UI_gameversion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!MwInit) return;
            Global.GameVersion = UiGameversion.SelectedIndex;
            RightFrame.Navigate(new Uri("../View/WelcomePage.xaml", UriKind.Relative));
            SidebarWelcome.IsChecked = true;
            RegeditRw.RegWrite("GameVersion", UiGameversion.SelectedIndex);
        }
        #endregion

        #region "设置菜单"
        //设置
        private void UI_btn_setting_Click(object sender, RoutedEventArgs e)
        {
            UiPopSetting.IsOpen = true;
        }
        //检查更新
        private void Se_button_Update_Click(object sender, RoutedEventArgs e)
        {
            UiPopSetting.IsOpen = false;
            MwVisivility = false;
            UpdatePan.UpdateNow();
        }
        //窗口置顶
        private void Se_button_Topmost_Click(object sender, RoutedEventArgs e)
        {
            if (Topmost == false)
            {
                Topmost = true;
                SeImageTopmost.Source = RSN.PictureShortName(RSN.ShortName("Setting_Top_T"));
                SeTextblockTopmost.Text = "永远置顶";
                RegeditRw.RegWrite("Topmost", 1);
            }
            else
            {
                Topmost = false;
                SeImageTopmost.Source = RSN.PictureShortName(RSN.ShortName("Setting_Top_F"));
                SeTextblockTopmost.Text = "永不置顶";
                RegeditRw.RegWrite("Topmost", 0);
            }
        }
        #endregion

        #region "皮肤菜单"
        //皮肤菜单
        private void UI_btn_bg_Click(object sender, RoutedEventArgs e)
        {
            UiPopBg.IsOpen = true;
        }
        
        //获取字体函数
        private static IEnumerable<string> ReadFont()
        {
            var installedFontCollectionFont = new InstalledFontCollection();
            var fontFamilys = installedFontCollectionFont.Families;
            return fontFamilys.Length < 1 ? null : fontFamilys.Select(item => item.Name).ToList();
        }

        /// <summary>
        /// 设置背景
        /// </summary>
        private void Se_button_Background_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new Microsoft.Win32.OpenFileDialog()
            {
                FileName = "", //默认文件名
                DefaultExt = ".png", // 默认文件扩展名
                Filter = "图像文件 (*.bmp;*.gif;*.jpg;*.jpeg;*.png)|*.bmp;*.gif;*.jpg;*.jpeg;*.png" //文件扩展名过滤器
            };
            var result = ofd.ShowDialog(); //显示打开文件对话框

            Visi.VisiCol(false, UiBackGroundBorder);
            try
            {
                var pictruePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\JiHuangBaiKeCSharp\Background\"; //设置文件夹位置
                if (Directory.Exists(pictruePath) == false) //若文件夹不存在
                {
                    Directory.CreateDirectory(pictruePath);
                }
                var filename = Path.GetFileName(ofd.FileName); //设置文件名
                try
                {
                    File.Copy(ofd.FileName, pictruePath + filename, true);
                }
                catch (Exception)
                {
                    // ignored
                }
                var brush = new ImageBrush()
                {
                    ImageSource = new BitmapImage(new Uri(pictruePath + filename))
                };
                UiBackGroundBorder.Background = brush;
                SeBgAlphaText.Foreground = Brushes.Black;
                SeBgAlpha.IsEnabled = true;
                RegeditRw.RegWrite("Background", pictruePath + filename);
            }
            catch (Exception)
            {
                MessageBox.Show("没有选择正确的图片");
            }
        }

        /// <summary>
        /// 清除背景
        /// </summary>
        private void Se_button_Background_Clear_Click(object sender, RoutedEventArgs e)
        {
            Visi.VisiCol(true, UiBackGroundBorder);
            SeBgAlphaText.Foreground = Brushes.Silver;
            SeBgAlpha.IsEnabled = false;
            RegeditRw.RegWrite("Background", "");
        }

        /// <summary>
        /// 设置背景拉伸方式
        /// </summary>
        private void Se_ComboBox_Background_Stretch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var bg = RegeditRw.RegReadString("Background");
            if (!MwInit) return;
            if (bg == "")
            {
                SeBgAlphaText.Foreground = Brushes.Silver;
                SeBgAlpha.IsEnabled = false;
            }
            else
            {
                SeBgAlphaText.Foreground = Brushes.Black;
                try
                {
                    var brush = new ImageBrush()
                    {
                        ImageSource = new BitmapImage(new Uri(bg)),
                        Stretch = (Stretch)SeComboBoxBackgroundStretch.SelectedIndex
                    };
                    UiBackGroundBorder.Background = brush;
                    RegeditRw.RegWrite("BackgroundStretch", SeComboBoxBackgroundStretch.SelectedIndex + 1);
                }
                catch
                {
                    Visi.VisiCol(true, UiBackGroundBorder);
                }
            }
        }

        /// <summary>
        /// 修改字体
        /// </summary>
        private void Se_ComboBox_Font_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!LoadFont) return;
            var textList = (from TextBlock textBlock in SeComboBoxFont.Items select textBlock.Text).ToList();
            mainWindow.FontFamily = new FontFamily(textList[SeComboBoxFont.SelectedIndex]);
            Global.FontFamily = mainWindow.FontFamily;
            RightFrame.NavigationService.Navigate(new WelcomePage());
            RegeditRw.RegWrite("MainWindowFont", textList[SeComboBoxFont.SelectedIndex]);
        }

        /// <summary>
        /// 设置背景透明度
        /// </summary>
        private void Se_BG_Alpha_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            UiBackGroundBorder.Opacity = SeBgAlpha.Value / 100;
            SeBgAlphaText.Text = "背景不透明度：" + (int)SeBgAlpha.Value + "%";
            RegeditRw.RegWrite("BGAlpha", SeBgAlpha.Value + 1);
        }

        /// <summary>
        /// 设置面板透明度
        /// </summary>
        private void Se_Panel_Alpha_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            RightFrame.Background.Opacity = SePanelAlpha.Value / 100;
            SePanelAlphaText.Text = "面板不透明度：" + (int)SePanelAlpha.Value + "%";
            RegeditRw.RegWrite("BGPanelAlpha", SePanelAlpha.Value + 1);
        }

        /// <summary>
        /// 设置窗口透明度
        /// </summary>
        private void Se_Window_Alpha_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Opacity = SeWindowAlpha.Value / 100;
            SeWindowAlphaText.Text = "窗口不透明度：" + (int)SeWindowAlpha.Value + "%";
            RegeditRw.RegWrite("WindowAlpha", SeWindowAlpha.Value + 1);
        }
        #endregion

        #region "最小化/最大化/关闭按钮"
        public Rect Rcnormal;//窗口位置
        /// <summary>
        /// 最小化按钮
        /// </summary>
        private void UI_btn_minimized_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        /// <summary>
        /// 最大化按钮
        /// </summary>
        private void UI_btn_maximized_Click(object sender, RoutedEventArgs e)
        {
            Visi.VisiCol(true, UiBtnMaximized);
            Visi.VisiCol(false, UiBtnNormal);
            Rcnormal = new Rect(Left, Top, Width, Height);//保存下当前位置与大小
            Left = 0;
            Top = 0;
            var rc = SystemParameters.WorkArea;
            Width = rc.Width;
            Height = rc.Height;
            //WindowState = WindowState.Maximized;
        }
        /// <summary>
        /// 还原按钮
        /// </summary>
        private void UI_btn_normal_Click(object sender, RoutedEventArgs e)
        {
            Visi.VisiCol(false, UiBtnMaximized);
            Visi.VisiCol(true, UiBtnNormal);
            Left = Rcnormal.Left;
            Top = Rcnormal.Top;
            Width = Rcnormal.Width;
            Height = Rcnormal.Height;
            //WindowState = WindowState.Normal;
        }
        /// <summary>
        /// 关闭按钮
        /// </summary>
        private void UI_btn_close_Click(object sender, RoutedEventArgs e)
        {

            Environment.Exit(0);
        }
        #endregion
        #endregion

        #region "模拟SplitView按钮"
        #region "左侧菜单按钮"
        //左侧菜单状态，0为关闭，1为打开
        public static byte LeftMenuState;
        //左侧菜单开关
        private void Sidebar_Menu_Click(object sender, RoutedEventArgs e)
        {
//            var MainWindowWidth = mainWindow.ActualWidth;
//            double MainGridWidth = MainGrid.ActualWidth;
            if (LeftMenuState == 0)
            {
                Visi.VisiCol(false, UiVersion);
                Animation.Anim(LcWidth, 50, 150, WidthProperty);
                Animation.Anim(LeftCanvas, 50, 150, WidthProperty);
                Animation.Anim(LeftWrapPanel, 50, 150, WidthProperty);
                LcWidth.Width = new GridLength(150);
                LeftCanvas.Width = 150;
                LeftWrapPanel.Width = 150;
                LeftMenuState = 1;
            }
            else
            {
                Visi.VisiCol(true, UiVersion);
                Animation.Anim(LcWidth, 150, 50, WidthProperty);
                Animation.Anim(LeftCanvas, 150, 50, WidthProperty);
                Animation.Anim(LeftWrapPanel, 150, 50, WidthProperty);
                LcWidth.Width = new GridLength(50);
                LeftCanvas.Width = 50;
                LeftWrapPanel.Width = 50;
                LeftMenuState = 0;
            }
        }
        //左侧菜单按钮
        private void Sidebar_Welcome_Click(object sender, RoutedEventArgs e)
        {
            RightFrame.NavigationService.Navigate(new WelcomePage());
        }
        private void Sidebar_Character_Click(object sender, RoutedEventArgs e)
        {
            RightFrame.NavigationService.Navigate(new CharacterPage());
        }
        private void Sidebar_Food_Click(object sender, RoutedEventArgs e)
        {
            RightFrame.NavigationService.Navigate(new FoodPage());
        }
        private void Sidebar_Cooking_Simulator_Click(object sender, RoutedEventArgs e)
        {
            RightFrame.NavigationService.Navigate(new CookingSimulatorPage());
        }
        private void Sidebar_Science_Click(object sender, RoutedEventArgs e)
        {
            RightFrame.NavigationService.Navigate(new SciencePage());
        }
        private void Sidebar_Animal_Click(object sender, RoutedEventArgs e)
        {
            RightFrame.NavigationService.Navigate(new CreaturePage());
        }
        private void Sidebar_Natural_Click(object sender, RoutedEventArgs e)
        {
            RightFrame.NavigationService.Navigate(new NaturalPage());
        }
        private void Sidebar_Goods_Click(object sender, RoutedEventArgs e)
        {
            RightFrame.NavigationService.Navigate(new GoodPage());
        }
        private void Sidebar_DedicatedServer_Click(object sender, RoutedEventArgs e)
        {
            //TODO
        }
        private void Sidebar_Setting_Click(object sender, RoutedEventArgs e)
        {
            //TODO
        }
        #endregion

        #endregion

        #region "DedicatedServer"
        #region "主菜单按钮"
        private void DediTitleSetting_Click(object sender, RoutedEventArgs e)
        {
            DediButtomPanelVisibility("Setting");
        }

        private void DediTitleBaseSet_Click(object sender, RoutedEventArgs e)
        {
            DediButtomPanelVisibility("BaseSet");
        }

        private void DediTitleEditWorld_Click(object sender, RoutedEventArgs e)
        {
            DediButtomPanelVisibility("EditWorld");
        }

        private void DediTitleMod_Click(object sender, RoutedEventArgs e)
        {
            DediButtomPanelVisibility("Mod");
        }

        private void DediTitleRollback_Click(object sender, RoutedEventArgs e)
        {
            DediButtomPanelVisibility("Rollback");
        }

        private void DediTitleBlacklist_Click(object sender, RoutedEventArgs e)
        {
            DediButtomPanelVisibility("Blacklist");
        }
        #endregion

        #region "下侧面板Visibility属性设置"
        private void DediButtomPanelVisibilityInitialize()
        {
            foreach (UIElement vControl in DediButtomBg.Children)
            {
                Visi.VisiCol(true, vControl);
            }
            Visi.VisiCol(false, DediButtomBorderH1, DediButtomBorderH2, DediButtomBorderV1, DediButtomBorderV4);
        }

        private void DediButtomPanelVisibility(string obj)
        {
            DediButtomPanelVisibilityInitialize();
            switch (obj)
            {
                case "Setting":
                    Visi.VisiCol(false, DediSetting);
                    break;
                case "BaseSet":
                    Visi.VisiCol(false, DediBaseSet);
                    break;
                case "EditWorld":

                    break;
                case "Mod":

                    break;
                case "Rollback":

                    break;
                case "Blacklist":

                    break;
            }
        }
        #endregion

        private void DediButtomPanelInitalize()
        {
            string[] gameVersion = { "Steam", "TGP", "游侠" };
            DediSettingGameVersionSelect.Init(gameVersion);

            DediButtomPanelVisibilityInitialize();
            string[] noYes = { "否", "是" };
            string[] gamemode = { "生存", "荒野", "无尽" };
            var maxPlayer = new string[64];
            for (var i = 1; i <= 64; i++)
            {
                maxPlayer[i - 1] = i.ToString();
            }
            string[] offline = { "在线", "离线" };
            DediBaseSetGrouponlySelect.Init(noYes);
            DediBaseSetGroupadminsSelect.Init(noYes);
            DediBaseSetGamemodeSelect.Init(gamemode);
            DediBaseSetPvpSelect.Init(noYes);
            DediBaseSetMaxPlayerSelect.Init(maxPlayer, 5);
            DediBaseOfflineSelect.Init(offline);
            Visi.VisiCol(false, DediBaseSet);
            DediBaseSetRangeInitalize();
        }

        #region "Intention"
        private void DediIntention_social_Click(object sender, RoutedEventArgs e)
        {
            DediIntention_Click("social");
        }

        private void DediIntention_social_MouseEnter(object sender, MouseEventArgs e)
        {
            DidiIntentionTextBlock.Text = (string)(((Button)sender).Tag);
        }

        private void DediIntention_social_MouseLeave(object sender, MouseEventArgs e)
        {
            DidiIntentionTextBlock.Text = "";
        }

        private void DediIntention_cooperative_Click(object sender, RoutedEventArgs e)
        {
            DediIntention_Click("cooperative");
        }

        private void DediIntention_cooperative_MouseEnter(object sender, MouseEventArgs e)
        {
            DidiIntentionTextBlock.Text = (string)(((Button)sender).Tag);
        }

        private void DediIntention_cooperative_MouseLeave(object sender, MouseEventArgs e)
        {
            DidiIntentionTextBlock.Text = "";
        }

        private void DediIntention_competitive_Click(object sender, RoutedEventArgs e)
        {
            DediIntention_Click("competitive");
        }

        private void DediIntention_competitive_MouseEnter(object sender, MouseEventArgs e)
        {
            DidiIntentionTextBlock.Text = (string)(((Button)sender).Tag);
        }

        private void DediIntention_competitive_MouseLeave(object sender, MouseEventArgs e)
        {
            DidiIntentionTextBlock.Text = "";
        }

        private void DediIntention_madness_Click(object sender, RoutedEventArgs e)
        {
            DediIntention_Click("madness");
        }

        private void DediIntention_madness_MouseEnter(object sender, MouseEventArgs e)
        {
            DidiIntentionTextBlock.Text = (string)(((Button)sender).Tag);
        }

        private void DediIntention_madness_MouseLeave(object sender, MouseEventArgs e)
        {
            DidiIntentionTextBlock.Text = "";
        }

        private void DediIntention_Click(string intention)
        {
            DediButtomPanelVisibilityInitialize();
            Visi.VisiCol(false, DediBaseSet);
            switch (intention)
            {
                case "social":
                    DediBaseSetIntentionButton.Content = "交际";
                    break;
                case "cooperative":
                    DediBaseSetIntentionButton.Content = "合作";
                    break;
                case "competitive":
                    DediBaseSetIntentionButton.Content = "竞争";
                    break;
                case "madness":
                    DediBaseSetIntentionButton.Content = "疯狂";
                    break;
            }
        }
        #endregion

        #region "BaseSet"
        private void DediBaseSetIntentionButton_Click(object sender, RoutedEventArgs e)
        {
            DediButtomPanelVisibilityInitialize();
            Visi.VisiCol(false, DediIntention);
        }


        private void DediBaseSetRangePublic_Click(object sender, RoutedEventArgs e)
        {
            DediBaseSetRangeInitalize();
        }

        private void DediBaseSetRangeFriendonly_Click(object sender, RoutedEventArgs e)
        {
            DediBaseSetRangeInitalize();
        }

        private void DediBaseSetRangeLocal_Click(object sender, RoutedEventArgs e)
        {
            DediBaseSetRangeInitalize();
        }

        private void DediBaseSetRangeSteamgroup_Click(object sender, RoutedEventArgs e)
        {
            Visi.VisiCol(false, DediBaseSetGroupid, DediBaseSetGrouponly, DediBaseSetGroupadmins);
        }

        private void DediBaseSetRangeInitalize()
        {
            Visi.VisiCol(true, DediBaseSetGroupid, DediBaseSetGrouponly, DediBaseSetGroupadmins);
        }
        #endregion
        #endregion
    }
}
