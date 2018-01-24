using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using 饥荒百科全书CSharp.Class;
using 饥荒百科全书CSharp.View;
using 饥荒百科全书CSharp.View.Dialog;
using Application = System.Windows.Application;
using Brushes = System.Windows.Media.Brushes;
using Button = System.Windows.Controls.Button;
using ContextMenu = System.Windows.Controls.ContextMenu;
using FontFamily = System.Windows.Media.FontFamily;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using MenuItem = System.Windows.Controls.MenuItem;
using MessageBox = System.Windows.MessageBox;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;

namespace 饥荒百科全书CSharp
{
    /// <summary>
    /// MainWindow窗口控制类
    /// </summary>
    public partial class MainWindow : Window
    {
        #region "窗口尺寸/拖动窗口"
        // 引用光标资源字典
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

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed) return;
            if (e.OriginalSource is FrameworkElement element && !element.Name.Contains("Resize"))
            {
                Cursor = ((TextBlock)CursorDictionary["CursorPointer"]).Cursor;
            }
        }

        private void ResizePressed(object sender, MouseEventArgs e)
        {
            if (!(sender is FrameworkElement element)) return;
            var direction = (ResizeDirection)Enum.Parse(typeof(ResizeDirection), element.Name.Replace("Resize", ""));

            switch (direction)
            {
                case ResizeDirection.Left:
                    Cursor = ((TextBlock)CursorDictionary["CursorHorz"]).Cursor;
                    break;
                case ResizeDirection.Right:
                    Cursor = ((TextBlock)CursorDictionary["CursorHorz"]).Cursor;
                    break;
                case ResizeDirection.Top:
                    Cursor = ((TextBlock)CursorDictionary["CursorVert"]).Cursor;
                    break;
                case ResizeDirection.Bottom:
                    Cursor = ((TextBlock)CursorDictionary["CursorVert"]).Cursor;
                    break;
                case ResizeDirection.TopLeft:
                    Cursor = ((TextBlock)CursorDictionary["CursorDgn1"]).Cursor;
                    break;
                case ResizeDirection.BottomRight:
                    Cursor = ((TextBlock)CursorDictionary["CursorDgn1"]).Cursor;
                    break;
                case ResizeDirection.TopRight:
                    Cursor = ((TextBlock)CursorDictionary["CursorDgn2"]).Cursor;
                    break;
                case ResizeDirection.BottomLeft:
                    Cursor = ((TextBlock)CursorDictionary["CursorDgn2"]).Cursor;
                    break;
            }
            if (e.LeftButton == MouseButtonState.Pressed)
                ResizeWindow(direction);
        }

        private void ResizeWindow(ResizeDirection direction)
        {
            SendMessage(_hwndSource.Handle, WmSyscommand, (IntPtr)(61440 + direction), IntPtr.Zero);
        }

        /// <summary>
        /// MainWindow拖动窗口
        /// </summary>
        private void MainWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var positionUiGrid = e.GetPosition(UiGrid);
            var positionRightGridFrame = e.GetPosition(RightFrame);
            var positionDedicatedServerFrame = e.GetPosition(DedicatedServerFrame);
            var inUiGrid = false;
            var inRightGridFrame = false;
            var inDedicatedServerFrame = false;
            if (positionUiGrid.X >= 0 && positionUiGrid.X < UiGrid.ActualWidth && positionUiGrid.Y >= 0 && positionUiGrid.Y < UiGrid.ActualHeight)
            {
                inUiGrid = true;
            }
            if (positionRightGridFrame.X >= 0 && positionRightGridFrame.X < RightFrame.ActualWidth && positionRightGridFrame.Y >= 0 && positionRightGridFrame.Y < RightFrame.ActualHeight)
            {
                inRightGridFrame = true;
            }
            if (positionDedicatedServerFrame.X >= 0 && positionDedicatedServerFrame.X < DedicatedServerFrame.ActualWidth && positionDedicatedServerFrame.Y >= 0 && positionDedicatedServerFrame.Y < DedicatedServerFrame.ActualHeight)
            {
                inDedicatedServerFrame = true;
            }
            // 如果鼠标位置在标题栏内，允许拖动  
            if (e.LeftButton != MouseButtonState.Pressed || (!inUiGrid && !inRightGridFrame && !inDedicatedServerFrame)) return;
            Cursor = ((TextBlock)CursorDictionary["CursorMove"]).Cursor;
            DragMove();
        }

        /// <summary>
        /// 切换鼠标指针为默认状态
        /// </summary>
        private void MainWindow_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Cursor = ((TextBlock)CursorDictionary["CursorPointer"]).Cursor;
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
            HamburgerButtonCanvas.Height = mainWindow.ActualHeight - 2;
            LeftWrapPanel.Height = mainWindow.ActualHeight - 2;
            RegeditRw.RegWrite("MainWindowHeight", ActualHeight);
            RegeditRw.RegWrite("MainWindowWidth", ActualWidth);
        }
        #endregion

        #region "右上角按钮"
        #region "搜索框"
        /// <summary>
        /// 搜索框内容改变
        /// </summary>
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // 显示清除按钮
            if (string.IsNullOrEmpty(SearchTextBox.Text.Trim()))
            {
                SearchClearButton.Visibility = Visibility.Collapsed;
                SearchPopup.IsOpen = false;
            }
            else
            {
                SearchClearButton.Visibility = Visibility.Visible;
                // 搜索
                Global.AutoSuggestBoxItem.Clear();
                foreach (var item in Global.AutoSuggestBoxItemSource)
                {
                    Global.AutoSuggestBoxItem.Add(item);
                }
                var str = SearchTextBox.Text.Trim().ToLower();
                if (string.IsNullOrEmpty(str)) return;
                for (var i = Global.AutoSuggestBoxItem.Count - 1; i >= 0; i--)
                {
                    if (Global.AutoSuggestBoxItem[i].Name.ToLower().IndexOf(str, StringComparison.Ordinal) < 0 && Global.AutoSuggestBoxItem[i].EnName.ToLower().IndexOf(str, StringComparison.Ordinal) < 0)
                    {
                        Global.AutoSuggestBoxItem.Remove(Global.AutoSuggestBoxItem[i]);
                    }
                }
                if (Global.AutoSuggestBoxItem.Count != 0)
                {
                    SearchItemsControl.DataContext = Global.AutoSuggestBoxItem;
                    SearchScrollViewer.UpdateLayout();
                    SearchScrollViewer.ScrollToHorizontalOffset(0);
                    SearchPopup.IsOpen = true;
                }
                else
                {
                    SearchPopup.IsOpen = false;
                }
            }
        }

        /// <summary>
        /// 清除按钮
        /// </summary>
        private void UI_search_clear_Click(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = "";
            SearchClearButton.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// 搜索结果Click事件
        /// </summary>
        private void SearchResultButton_Click(object sender, RoutedEventArgs e)
        {
            var suggestBoxItem = (SuggestBoxItem)((Button)sender).DataContext;
            if (suggestBoxItem == null) return;
            var extraData = new[] { suggestBoxItem.SourcePath, suggestBoxItem.Picture, suggestBoxItem.Category };
            AutoSuggestNavigate(extraData);
            //清空搜索框文本
            SearchTextBox.Text = "";
            SearchPopup.IsOpen = false;
        }

        /// <summary>
        /// 自动搜索框导航
        /// </summary>
        /// <param name="extraData">额外数据</param>
        private void AutoSuggestNavigate(string[] extraData)
        {
            switch (extraData[2])
            {
                case "人物":
                    SidebarCharacter.IsChecked = true;
                    RightFrame.NavigationService.Navigate(new CharacterPage(), extraData);
                    break;
                case "食物":
                    SidebarFood.IsChecked = true;
                    RightFrame.NavigationService.Navigate(new FoodPage(), extraData);
                    break;
                case "科技":
                    SidebarScience.IsChecked = true;
                    RightFrame.NavigationService.Navigate(new SciencePage(), extraData);
                    break;
                case "生物":
                    SidebarCreature.IsChecked = true;
                    RightFrame.NavigationService.Navigate(new CreaturePage(), extraData);
                    break;
                case "自然":
                    SidebarNatural.IsChecked = true;
                    RightFrame.NavigationService.Navigate(new NaturalPage(), extraData);
                    break;
                case "物品":
                    SidebarGoods.IsChecked = true;
                    RightFrame.NavigationService.Navigate(new GoodPage(), extraData);
                    break;
                case "皮肤":
                    SidebarSkins.IsChecked = true;
                    RightFrame.NavigationService.Navigate(new SkinsPage(), extraData);
                    break;
            }
        }
        #endregion

        #region "游戏版本"
        /// <summary>
        /// 游戏版本选择
        /// </summary>
        private void UI_gameversion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!MwInit) return;
            Global.GameVersion = UiGameversion.SelectedIndex;
            // 设置AutoSuggestBox的数据源
            Global.SetAutoSuggestBoxItem();
            RightFrame.Navigate(new Uri("../View/WelcomePage.xaml", UriKind.Relative));
            SidebarWelcome.IsChecked = true;
            RegeditRw.RegWrite("GameVersion", UiGameversion.SelectedIndex);
        }
        #endregion

        #region "设置菜单"
        /// <summary>
        /// 设置
        /// </summary>
        private void UI_btn_setting_Click(object sender, RoutedEventArgs e)
        {
            UiPopSetting.IsOpen = true;
        }

        /// <summary>
        /// 检查更新
        /// </summary>
        private void Se_button_Update_Click(object sender, RoutedEventArgs e)
        {
            UiPopSetting.IsOpen = false;
            MwVisibility = false;
            UpdatePan.UpdateNow();
        }

        /// <summary>
        /// 窗口置顶
        /// </summary>
        private void Se_button_Topmost_Click(object sender, RoutedEventArgs e)
        {
            if (Topmost == false)
            {
                Topmost = true;
                SeImageTopmost.Source = new BitmapImage(new Uri("pack://application:,,,/饥荒百科全书CSharp;component/Resources/Setting_Top_T.png", UriKind.Absolute));
                SeTextblockTopmost.Text = "永远置顶";
                RegeditRw.RegWrite("Topmost", 1);
            }
            else
            {
                Topmost = false;
                SeImageTopmost.Source = new BitmapImage(new Uri("pack://application:,,,/饥荒百科全书CSharp;component/Resources/Setting_Top_F.png", UriKind.Absolute));
                SeTextblockTopmost.Text = "永不置顶";
                RegeditRw.RegWrite("Topmost", 0);
            }
        }
        #endregion

        #region "皮肤菜单"
        /// <summary>
        /// 皮肤菜单
        /// </summary>
        private void UI_btn_bg_Click(object sender, RoutedEventArgs e)
        {
            UiPopBg.IsOpen = true;
        }

        /// <summary>
        /// 获取字体函数
        /// </summary>
        /// <returns>字体列表</returns>
        private static IEnumerable<string> ReadFont()
        {
            var installedFontCollectionFont = new InstalledFontCollection();
            var fontFamilys = installedFontCollectionFont.Families;
            return fontFamilys.Length < 1 ? null : fontFamilys.Select(item => item.Name).ToList();
        }

        /// <summary>
        /// 修改字体
        /// </summary>
        private void Se_ComboBox_Font_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!LoadFont) return;
            var textList = (from TextBlock textBlock in SeComboBoxFont.Items select textBlock.Text).ToList();
            mainWindow.FontFamily = new FontFamily(textList[SeComboBoxFont.SelectedIndex]);
            ((TextBlock)((VisualBrush)FindResource("HelpBrush")).Visual).FontFamily = mainWindow.FontFamily;
            Global.FontFamily = mainWindow.FontFamily;
            RightFrame.NavigationService.Navigate(new WelcomePage());
            SidebarWelcome.IsChecked = true;
            RegeditRw.RegWrite("MainWindowFont", textList[SeComboBoxFont.SelectedIndex]);
        }

        /// <summary>
        /// 修改字体加粗
        /// </summary>
        private void Se_CheckBox_FontWeight_Click(object sender, RoutedEventArgs e)
        {
            if (!LoadFont) return;
            mainWindow.FontWeight = SeCheckBoxFontWeight.IsChecked == true ? FontWeights.Bold : FontWeights.Normal;
            ((TextBlock)((VisualBrush)FindResource("HelpBrush")).Visual).FontWeight = mainWindow.FontWeight;
            Global.FontWeight = mainWindow.FontWeight;
            RightFrame.NavigationService.Navigate(new WelcomePage());
            SidebarWelcome.IsChecked = true;
            RegeditRw.RegWrite("MainWindowFontWeight", SeCheckBoxFontWeight.IsChecked.ToString());
        }

        /// <summary>
        /// 修改淡紫色透明光标
        /// </summary>
        private void SeCheckBoxLavenderCursor_Click(object sender, RoutedEventArgs e)
        {
            var copySplashWindow = new CopySplashScreen("重启生效");
            copySplashWindow.InitializeComponent();
            copySplashWindow.ContentTextBlock.FontSize = 20;
            copySplashWindow.Show();
            RegeditRw.RegWrite("MainWindowLavenderCursor", SeCheckBoxLavenderCursor.IsChecked.ToString());
        }

        /// <summary>
        /// 设置背景
        /// </summary>
        private void Se_button_Background_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog()
            {
                Title = "选择背景图片",
                FileName = "", //默认文件名
                DefaultExt = ".png", // 默认文件扩展名
                Filter = "图像文件 (*.bmp;*.gif;*.jpg;*.jpeg;*.png)|*.bmp;*.gif;*.jpg;*.jpeg;*.png" //文件扩展名过滤器
            };
            // ReSharper disable once UnusedVariable
            var result = openFileDialog.ShowDialog(); //显示打开文件对话框
            UiBackGroundBorder.Visibility = Visibility.Visible;
            try
            {
                var pictruePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\JiHuangBaiKeCSharp\Background\"; //设置文件夹位置
                if (Directory.Exists(pictruePath) == false) //若文件夹不存在
                {
                    Directory.CreateDirectory(pictruePath);
                }
                var filename = Path.GetFileName(openFileDialog.FileName); //设置文件名
                try
                {
                    File.Copy(openFileDialog.FileName, pictruePath + filename, true);
                }
                catch (Exception)
                {
                    // ignored
                }
                var brush = new ImageBrush
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
                MessageBox.Show("没有选择正确的图片ヽ(ﾟДﾟ)ﾉ");
            }
        }

        /// <summary>
        /// 清除背景
        /// </summary>
        private void Se_button_Background_Clear_Click(object sender, RoutedEventArgs e)
        {
            UiBackGroundBorder.Visibility = Visibility.Collapsed;
            UiBackGroundBorder.Background = null;
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
                    UiBackGroundBorder.Visibility = Visibility.Collapsed;
                }
            }
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
            UiBtnMaximized.Visibility = Visibility.Collapsed;
            UiBtnNormal.Visibility = Visibility.Visible;
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
            UiBtnMaximized.Visibility = Visibility.Visible;
            UiBtnNormal.Visibility = Visibility.Collapsed;
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
            if (Settings.HideToNotifyIconPrompt == false)
            {
                var notifyIconMessageBox = new NotifyIconMessageBox();
                notifyIconMessageBox.ShowDialog();
            }
            else
            {
                if (Settings.HideToNotifyIcon)
                {
                    NotifyIcon.ShowBalloonTip(1000);
                    MwVisibility = false;
                }
                else
                {
                    DisposeNotifyIcon();
                    Application.Current.Shutdown();
                }
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            e.Cancel = true;
        }

        #region 自定义Alt+F4 和 屏蔽Alt+Space
        private bool _altDown;
        private bool _ctrlDown;
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            if (e.SystemKey == Key.LeftAlt || e.SystemKey == Key.RightAlt)
            {
                _altDown = true;
            }
            else if (e.SystemKey == Key.LeftCtrl || e.SystemKey == Key.RightCtrl)
            {
                _ctrlDown = true;
            }
            // 调用自定义Alt+F4事件
            else if (e.SystemKey == Key.F4 && _altDown)
            {
                e.Handled = true;
                UI_btn_close_Click(null, null);
            }
            // Alt+Space直接屏蔽
            else if (e.SystemKey == Key.Space && _altDown)
            {
                e.Handled = true;
            }
            // 聚焦搜索框
            else if (e.SystemKey == Key.F && _ctrlDown)
            {
                SearchTextBox.Focus();
            }
        }

        private void Window_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.SystemKey == Key.LeftAlt || e.SystemKey == Key.RightAlt)
            {
                _altDown = false;
            }
            if (e.SystemKey == Key.LeftCtrl || e.SystemKey == Key.RightCtrl)
            {
                _ctrlDown = false;
            }
        }
        #endregion

        private void DisposeNotifyIcon()
        {
            NotifyIcon.Visible = false;
            NotifyIcon.Dispose();
        }

        #endregion
        #endregion

        #region "模拟SplitView按钮"
        #region "汉堡菜单开关"
        //左侧菜单状态，0为关闭，1为打开
        public static byte LeftMenuState;
        //左侧菜单开关
        private void Sidebar_Menu_Click(object sender, RoutedEventArgs e)
        {
            //            var MainWindowWidth = mainWindow.ActualWidth;
            //            double MainGridWidth = MainGrid.ActualWidth;
            if (LeftMenuState == 0)
            {
                UiVersion.Visibility = Visibility.Visible;
                Animation.Anim(LcWidth, 50, 150, WidthProperty);
                Animation.Anim(HamburgerButtonCanvas, 50, 150, WidthProperty);
                Animation.Anim(LeftWrapPanel, 50, 150, WidthProperty);
                LeftMenuState = 1;
            }
            else
            {
                UiVersion.Visibility = Visibility.Collapsed;
                Animation.Anim(LcWidth, 150, 50, WidthProperty);
                Animation.Anim(HamburgerButtonCanvas, 150, 50, WidthProperty);
                Animation.Anim(LeftWrapPanel, 150, 50, WidthProperty);
                LeftMenuState = 0;
            }
        }
        #endregion

        #region "页面切换"
        private void Sidebar_Welcome_Click(object sender, RoutedEventArgs e)
        {
            DedicatedServerFrame.Visibility = Visibility.Collapsed;
            RightFrame.Visibility = Visibility.Visible;
            RightFrame.NavigationService.Navigate(new WelcomePage());
        }
        private void Sidebar_Character_Click(object sender, RoutedEventArgs e)
        {
            DedicatedServerFrame.Visibility = Visibility.Collapsed;
            RightFrame.Visibility = Visibility.Visible;
            RightFrame.NavigationService.Navigate(new CharacterPage());
        }
        private void Sidebar_Food_Click(object sender, RoutedEventArgs e)
        {
            DedicatedServerFrame.Visibility = Visibility.Collapsed;
            RightFrame.Visibility = Visibility.Visible;
            RightFrame.NavigationService.Navigate(new FoodPage());
        }
        private void Sidebar_Cooking_Simulator_Click(object sender, RoutedEventArgs e)
        {
            DedicatedServerFrame.Visibility = Visibility.Collapsed;
            RightFrame.Visibility = Visibility.Visible;
            RightFrame.NavigationService.Navigate(new CookingSimulatorPage());
        }
        private void Sidebar_Science_Click(object sender, RoutedEventArgs e)
        {
            DedicatedServerFrame.Visibility = Visibility.Collapsed;
            RightFrame.Visibility = Visibility.Visible;
            RightFrame.NavigationService.Navigate(new SciencePage());
        }
        private void Sidebar_Creature_Click(object sender, RoutedEventArgs e)
        {
            DedicatedServerFrame.Visibility = Visibility.Collapsed;
            RightFrame.Visibility = Visibility.Visible;
            RightFrame.NavigationService.Navigate(new CreaturePage());
        }
        private void Sidebar_Natural_Click(object sender, RoutedEventArgs e)
        {
            DedicatedServerFrame.Visibility = Visibility.Collapsed;
            RightFrame.Visibility = Visibility.Visible;
            RightFrame.NavigationService.Navigate(new NaturalPage());
        }
        private void Sidebar_Goods_Click(object sender, RoutedEventArgs e)
        {
            DedicatedServerFrame.Visibility = Visibility.Collapsed;
            RightFrame.Visibility = Visibility.Visible;
            RightFrame.NavigationService.Navigate(new GoodPage());
        }
        private void Sidebar_Skins_Click(object sender, RoutedEventArgs e)
        {
            DedicatedServerFrame.Visibility = Visibility.Collapsed;
            RightFrame.Visibility = Visibility.Visible;
            RightFrame.NavigationService.Navigate(new SkinsPage());
        }
        private void Sidebar_DedicatedServer_Click(object sender, RoutedEventArgs e)
        {
            RightFrame.Visibility = Visibility.Collapsed;
            DedicatedServerFrame.Visibility = Visibility.Visible;
            if (DedicatedServerFrame.Content == null)
            {
                DedicatedServerFrame.NavigationService.Navigate(new DedicatedServerPage());
            }
        }
        private void Sidebar_Setting_Click(object sender, RoutedEventArgs e)
        {
            DedicatedServerFrame.Visibility = Visibility.Collapsed;
            RightFrame.Visibility = Visibility.Visible;
            RightFrame.NavigationService.Navigate(new SettingPage());
        }
        #endregion
        #endregion

        #region 托盘区图标
        /// <summary>
        /// 托盘区图标
        /// </summary>
        public NotifyIcon NotifyIcon;

        /// <summary>
        /// 托盘区图标菜单
        /// </summary>
        private ContextMenu _notifyIconMenu;

        /// <summary>
        /// 设置托盘区图标
        /// </summary>
        private void SetNotifyIcon()
        {
            NotifyIcon = new NotifyIcon
            {
                BalloonTipText = "饥荒百科全书躲起来了~",
                Text = "饥荒百科全书",
                // ReSharper disable once PossibleNullReferenceException
                Icon = new Icon(Application.GetResourceStream(new Uri("pack://application:,,,/饥荒百科全书CSharp;component/Resources/DST.ico")).Stream),
                Visible = true
            };
            NotifyIcon.MouseClick += NotifyIcon_MouseClick;
            _notifyIconMenu = (ContextMenu)FindResource("NotifyIconMenu");
        }

        private void NotifyIcon_Navigated(object sender, EventArgs e)
        {
            MwVisibility = true;
            switch (((Button)sender).Name)
            {
                case "CharacterButton":
                    SidebarCharacter.IsChecked = true;
                    Sidebar_Character_Click(null, null);
                    break;
                case "FoodButton":
                    SidebarFood.IsChecked = true;
                    Sidebar_Food_Click(null, null);
                    break;
                case "CookingSimulatorButton":
                    SidebarCookingSimulator.IsChecked = true;
                    Sidebar_Cooking_Simulator_Click(null, null);
                    break;
                case "ScienceButton":
                    SidebarScience.IsChecked = true;
                    Sidebar_Science_Click(null, null);
                    break;
                case "CreatureButton":
                    SidebarCreature.IsChecked = true;
                    Sidebar_Creature_Click(null, null);
                    break;
                case "NaturalButton":
                    SidebarNatural.IsChecked = true;
                    Sidebar_Natural_Click(null, null);
                    break;
                case "GoodButton":
                    SidebarGoods.IsChecked = true;
                    Sidebar_Goods_Click(null, null);
                    break;
                case "SettingButton":
                    SidebarSetting.IsChecked = true;
                    Sidebar_Setting_Click(null, null);
                    break;
                case "ExitButton":
                    DisposeNotifyIcon();
                    Application.Current.Shutdown();
                    break;
            }
            ((ContextMenu)GetParent((MenuItem)GetParent((WrapPanel)GetParent((Button)sender)))).IsOpen = false;
            if (RegeditRw.RegRead("Topmost") == 0)
            {
                mainWindow.Topmost = true;
                mainWindow.Topmost = false;
            }
        }

        /// <summary>
        /// 寻找逻辑树上的父控件
        /// </summary>
        /// <param name="dependencyObject">控件对象</param>
        /// <returns>父控件对象</returns>
        private static DependencyObject GetParent(DependencyObject dependencyObject)
        {
            return LogicalTreeHelper.GetParent(dependencyObject);
        }

        /// <summary>  
        /// 鼠标单击  
        /// </summary>  
        private void NotifyIcon_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            // 左键显示隐藏界面
            if (e.Button == MouseButtons.Left)
            {
                if (MwVisibility)
                {
                    NotifyIcon.ShowBalloonTip(1000);
                }
                MwVisibility = !MwVisibility;
            }
            //右键打开菜单
            else if (e.Button == MouseButtons.Right)
            {
                _notifyIconMenu.IsOpen = true;
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            _notifyIconMenu.IsOpen = false;
        }
        #endregion

    }
}
