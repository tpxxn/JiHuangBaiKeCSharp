using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using 饥荒百科全书CSharp.Class;
using 饥荒百科全书CSharp.MyUserControl;

namespace 饥荒百科全书CSharp
{
    /// <summary>
    /// MainWindow窗口控制类
    /// </summary>
    public partial class MainWindow : Window
    {
        #region "窗口尺寸/拖动窗口"
        //引用光标资源字典
        static ResourceDictionary cursorDictionary = new ResourceDictionary();
        private const int WM_SYSCOMMAND = 0x112;
        private HwndSource _HwndSource;
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
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton != MouseButtonState.Pressed)
            {
                FrameworkElement element = e.OriginalSource as FrameworkElement;
                if (element != null && !element.Name.Contains("Resize"))
                {
                    Cursor = (Cursor)cursorDictionary["Cursor_pointer"];
                }
            }
        }
        private void ResizePressed(object sender, MouseEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            ResizeDirection direction = (ResizeDirection)Enum.Parse(typeof(ResizeDirection), element.Name.Replace("Resize", ""));

            switch (direction)
            {
                case ResizeDirection.Left:
                    Cursor = (Cursor)cursorDictionary["Cursor_horz"];
                    break;
                case ResizeDirection.Right:
                    Cursor = (Cursor)cursorDictionary["Cursor_horz"];
                    break;
                case ResizeDirection.Top:
                    Cursor = (Cursor)cursorDictionary["Cursor_vert"];
                    break;
                case ResizeDirection.Bottom:
                    Cursor = (Cursor)cursorDictionary["Cursor_vert"];
                    break;
                case ResizeDirection.TopLeft:
                    Cursor = (Cursor)cursorDictionary["Cursor_dgn1"];
                    break;
                case ResizeDirection.BottomRight:
                    Cursor = (Cursor)cursorDictionary["Cursor_dgn1"];
                    break;
                case ResizeDirection.TopRight:
                    Cursor = (Cursor)cursorDictionary["Cursor_dgn2"];
                    break;
                case ResizeDirection.BottomLeft:
                    Cursor = (Cursor)cursorDictionary["Cursor_dgn2"];
                    break;
            }
            if (e.LeftButton == MouseButtonState.Pressed)
                ResizeWindow(direction);
        }

        private void ResizeWindow(ResizeDirection direction)
        {
            SendMessage(_HwndSource.Handle, WM_SYSCOMMAND, (IntPtr)(61440 + direction), IntPtr.Zero);
        }

        //MainWindow拖动窗口
        private void mainWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point position_UIGrid = e.GetPosition(UIGrid);
            Point position_RightGrid_Welcome = e.GetPosition(RightGrid_Welcome);
            Point position_RightGrid_Setting = e.GetPosition(RightGrid_Setting);
            bool inUIGrid = false;
            bool inWelcome = false;
            bool inSetting = false;
            if ((position_UIGrid.X >= 0 && position_UIGrid.X < UIGrid.ActualWidth && position_UIGrid.Y >= 0 && position_UIGrid.Y < UIGrid.ActualHeight))
            {
                inUIGrid = true;
            }
            if ((position_RightGrid_Welcome.X >= 0 && position_RightGrid_Welcome.X < RightGrid_Welcome.ActualWidth && position_RightGrid_Welcome.Y >= 0 && position_RightGrid_Welcome.Y < RightGrid_Welcome.ActualHeight))
            {
                inWelcome = true;
            }
            if ((position_RightGrid_Setting.X >= 0 && position_RightGrid_Setting.X < RightGrid_Setting.ActualWidth && position_RightGrid_Setting.Y >= 0 && position_RightGrid_Setting.Y < RightGrid_Setting.ActualHeight))
            {
                inSetting = true;
            }
            // 如果鼠标位置在标题栏内，允许拖动  
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (inUIGrid || inWelcome || inSetting)
                {
                    Cursor = (Cursor)cursorDictionary["Cursor_move"];
                    DragMove();
                }
            }
        }
        private void mainWindow_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Cursor = (Cursor)cursorDictionary["Cursor_pointer"];
        }

        //双击标题栏最大化
        private void mainWindow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Point position_UIGrid = e.GetPosition(UIGrid);
            if ((position_UIGrid.X >= 0 && position_UIGrid.X < UIGrid.ActualWidth && position_UIGrid.Y >= 0 && position_UIGrid.Y < UIGrid.ActualHeight))
            {
                if (UI_btn_maximized.Visibility == Visibility.Collapsed)
                {
                    UI_btn_normal_Click(null, null);
                }
                else
                {
                    UI_btn_maximized_Click(null, null);
                }
            }
        }

        //MainWindow窗口尺寸改变
        private void mainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //最大化
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
                UI_btn_maximized_Click(null, null);
            }
            //设置版本号位置
            UI_Version.Margin = new Thickness(10, mainWindow.ActualHeight - 35, 0, 0);
            //左侧面板高度
            LeftCanvas.Height = mainWindow.ActualHeight - 2;
            LeftWrapPanel.Height = mainWindow.ActualHeight - 2;
            //Splitter高度
            UI_Splitter.Height = ActualHeight - 52;
            RegeditRW.RegWrite("MainWindowHeight", ActualHeight);
            RegeditRW.RegWrite("MainWindowWidth", ActualWidth);
        }
        #endregion

        #region "右上角按钮"
        #region "搜索框清除按钮显示/隐藏"
        //设置清除按钮可见性
        private void UI_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (UI_search.Text != "")
            {
                Visi.VisiCol(false, UI_search_clear);
            }
            else
            {
                Visi.VisiCol(true, UI_search_clear);
            }
        }
        //清除按钮
        private void UI_search_clear_Click(object sender, RoutedEventArgs e)
        {
            UI_search.Text = "";
            Visi.VisiCol(true, UI_search_clear);
        }
        #endregion

        #region "游戏版本"
        //游戏版本选择
        private void UI_gameversion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RegeditRW.RegWrite("GameVersion", UI_gameversion.SelectedIndex);
            LoadGameVersionXml();
        }
        #endregion

        #region "设置菜单"
        //设置
        private void UI_btn_setting_Click(object sender, RoutedEventArgs e)
        {
            UI_pop_setting.IsOpen = true;
        }
        //检查更新
        private void Se_button_Update_Click(object sender, RoutedEventArgs e)
        {
            updatePan.UpdateNow();
            UI_pop_setting.IsOpen = false;
            MWVisivility = false;
        }
        //窗口置顶
        private void Se_button_Topmost_Click(Object sender, RoutedEventArgs e)
        {
            if (Topmost == false)
            {
                Topmost = true;
                Se_image_Topmost.Source = RSN.PictureShortName(RSN.ShortName("Setting_Top_T"));
                Se_textblock_Topmost.Text = "永远置顶";
                RegeditRW.RegWrite("Topmost", 1);
            }
            else
            {
                Topmost = false;
                Se_image_Topmost.Source = RSN.PictureShortName(RSN.ShortName("Setting_Top_F"));
                Se_textblock_Topmost.Text = "永不置顶";
                RegeditRW.RegWrite("Topmost", 0);
            }
        }
        #endregion

        #region "皮肤菜单"
        //皮肤菜单
        private void UI_btn_bg_Click(object sender, RoutedEventArgs e)
        {
            UI_pop_bg.IsOpen = true;
        }

        //设置背景方法
        public void SetBackground()
        {
            var OFD = new Microsoft.Win32.OpenFileDialog();
            OFD.FileName = ""; //默认文件名
            OFD.DefaultExt = ".png"; // 默认文件扩展名
            OFD.Filter = "图像文件 (*.bmp;*.gif;*.jpg;*.jpeg;*.png)|*.bmp;*.gif;*.jpg;*.jpeg;*.png"; //文件扩展名过滤器

            bool? result = OFD.ShowDialog(); //显示打开文件对话框

            Visi.VisiCol(false, UI_BackGroundBorder);
            try
            {
                string PictruePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\JiHuangBaiKeCSharp\Background\"; //设置文件夹位置
                if ((Directory.Exists(PictruePath)) == false) //若文件夹不存在
                {
                    Directory.CreateDirectory(PictruePath);
                }
                var filename = Path.GetFileName(OFD.FileName); //设置文件名
                try
                {
                    File.Copy(OFD.FileName, PictruePath + filename, true);
                }
                catch (Exception) { }
                var brush = new ImageBrush();
                brush.ImageSource = new BitmapImage(new Uri(PictruePath + filename));
                UI_BackGroundBorder.Background = brush;
                Se_BG_Alpha_Text.Foreground = Brushes.Black;
                Se_BG_Alpha.IsEnabled = true;
                RegeditRW.RegWrite("Background", PictruePath + filename);
            }
            catch (Exception)
            {
                MessageBox.Show("没有选择正确的图片");
            }
        }
        //清除背景方法
        private void ClearBackground()
        {
            Visi.VisiCol(true, UI_BackGroundBorder);
            Se_BG_Alpha_Text.Foreground = Brushes.Silver;
            Se_BG_Alpha.IsEnabled = false;
            RegeditRW.RegWrite("Background", "");
        }
        //获取字体函数
        private List<string> rF()
        {
            List<string> Font = new List<string>();
            InstalledFontCollection IFCFont = new InstalledFontCollection();
            System.Drawing.FontFamily[] fontFamilys = IFCFont.Families;
            if (fontFamilys == null || fontFamilys.Length < 1)
            {
                return null;
            }
            foreach (System.Drawing.FontFamily item in fontFamilys)
            {
                Font.Add(item.Name);
            }
            return Font;
        }

        //设置背景
        private void Se_button_Background_Click(object sender, RoutedEventArgs e)
        {
            SetBackground();
        }
        //清除背景
        private void Se_button_Background_Clear_Click(object sender, RoutedEventArgs e)
        {
            ClearBackground();
        }
        //设置背景拉伸方式
        private void Se_ComboBox_Background_Stretch_SelectionChanged(Object sender, SelectionChangedEventArgs e)
        {
            string bg = RegeditRW.RegReadString("Background");
            if (MWInit == true)
            {
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
                        brush.Stretch = (Stretch)Se_ComboBox_Background_Stretch.SelectedIndex;
                        UI_BackGroundBorder.Background = brush;
                        RegeditRW.RegWrite("BackgroundStretch", Se_ComboBox_Background_Stretch.SelectedIndex + 1);
                    }
                    catch
                    {
                        Visi.VisiCol(true, UI_BackGroundBorder);
                    }
                }
            }
        }
        //修改字体
        private void Se_ComboBox_Font_SelectionChanged(Object sender, SelectionChangedEventArgs e)
        {
            if (loadFont == true)
            {
                List<string> Ls = new List<string>();
                foreach (TextBlock TB in Se_ComboBox_Font.Items)
                {
                    Ls.Add(TB.Text);
                }
                mainWindow.FontFamily = new FontFamily(Ls[Se_ComboBox_Font.SelectedIndex]);
                RegeditRW.RegWrite("MainWindowFont", Ls[Se_ComboBox_Font.SelectedIndex]);
            }
        }
        //设置背景透明度
        private void Se_BG_Alpha_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            UI_BackGroundBorder.Opacity = Se_BG_Alpha.Value / 100;
            Se_BG_Alpha_Text.Text = "背景不透明度：" + (int)Se_BG_Alpha.Value + "%";
            RegeditRW.RegWrite("BGAlpha", Se_BG_Alpha.Value + 1);
        }
        //设置面板透明度
        private void Se_Panel_Alpha_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            RightGrid.Background.Opacity = Se_Panel_Alpha.Value / 100;
            Se_Panel_Alpha_Text.Text = "面板不透明度：" + (int)Se_Panel_Alpha.Value + "%";
            RegeditRW.RegWrite("BGPanelAlpha", Se_Panel_Alpha.Value + 1);
        }
        //设置窗口透明度
        private void Se_Window_Alpha_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Opacity = Se_Window_Alpha.Value / 100;
            Se_Window_Alpha_Text.Text = "窗口不透明度：" + (int)Se_Window_Alpha.Value + "%";
            RegeditRW.RegWrite("WindowAlpha", Se_Window_Alpha.Value + 1);
        }
        #endregion

        #region "最小化/最大化/关闭按钮"
        //最小化按钮
        private void UI_btn_minimized_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        Rect rcnormal;//窗口位置
        //最大化按钮
        private void UI_btn_maximized_Click(object sender, RoutedEventArgs e)
        {
            Visi.VisiCol(true, UI_btn_maximized);
            Visi.VisiCol(false, UI_btn_normal);
            rcnormal = new Rect(Left, Top, Width, Height);//保存下当前位置与大小
            Left = 0;
            Top = 0;
            Rect rc = SystemParameters.WorkArea;
            Width = rc.Width;
            Height = rc.Height;
            //WindowState = WindowState.Maximized;
        }
        //还原按钮
        private void UI_btn_normal_Click(object sender, RoutedEventArgs e)
        {
            Visi.VisiCol(false, UI_btn_maximized);
            Visi.VisiCol(true, UI_btn_normal);
            Left = rcnormal.Left;
            Top = rcnormal.Top;
            Width = rcnormal.Width;
            Height = rcnormal.Height;
            //WindowState = WindowState.Normal;
        }
        //关闭按钮
        private void UI_btn_close_Click(object sender, RoutedEventArgs e)
        {

            Environment.Exit(0);
        }
        #endregion
        #endregion

        #region "模拟SplitView按钮"
        #region "左侧菜单按钮"
        //左侧菜单状态，0为关闭，1为打开
        public static byte LeftMenuState = 0;
        //左侧菜单开关
        private void Sidebar_Menu_Click(object sender, RoutedEventArgs e)
        {
            var MainWindowWidth = mainWindow.ActualWidth;
            double MainGridWidth = MainGrid.ActualWidth;
            if (LeftMenuState == 0)
            {
                Visi.VisiCol(false, UI_Version);
                Animation.Anim(LCWidth, 50, 150, WidthProperty);
                Animation.Anim(LeftCanvas, 50, 150, WidthProperty);
                Animation.Anim(LeftWrapPanel, 50, 150, WidthProperty);
                LCWidth.Width = new GridLength(150);
                LeftCanvas.Width = 150;
                LeftWrapPanel.Width = 150;
                LeftMenuState = 1;
            }
            else
            {
                Visi.VisiCol(true, UI_Version);
                Animation.Anim(LCWidth, 150, 50, WidthProperty);
                Animation.Anim(LeftCanvas, 150, 50, WidthProperty);
                Animation.Anim(LeftWrapPanel, 150, 50, WidthProperty);
                LCWidth.Width = new GridLength(50);
                LeftCanvas.Width = 50;
                LeftWrapPanel.Width = 50;
                LeftMenuState = 0;
            }

        }
        //左侧菜单按钮
        private void Sidebar_Welcome_Click(object sender, RoutedEventArgs e)
        {
            RightPanelVisibility("Welcome");
        }
        private void Sidebar_Character_Click(object sender, RoutedEventArgs e)
        {
            RightPanelVisibility("Character");
        }
        private void Sidebar_Food_Click(object sender, RoutedEventArgs e)
        {
            RightPanelVisibility("Food");
        }
        private void Sidebar_Science_Click(object sender, RoutedEventArgs e)
        {
            RightPanelVisibility("Science");
        }
        private void Sidebar_Cooking_Simulator_Click(object sender, RoutedEventArgs e)
        {
            RightPanelVisibility("Cooking_Simulator");
        }
        private void Sidebar_Animal_Click(object sender, RoutedEventArgs e)
        {
            RightPanelVisibility("Animal");
        }
        private void Sidebar_Natural_Click(object sender, RoutedEventArgs e)
        {
            RightPanelVisibility("Natural");
        }
        private void Sidebar_Goods_Click(object sender, RoutedEventArgs e)
        {
            RightPanelVisibility("Goods");
        }
        private void Sidebar_Setting_Click(object sender, RoutedEventArgs e)
        {
            RightPanelVisibility("Setting");
        }
        #endregion

        #region "右侧面板Visibility属性设置"
        //右侧面板初始化
        private void RightPanelVisibilityInitialize()
        {
            foreach (UIElement vControl in RightGrid.Children)
            {
                Visi.VisiCol(true, vControl);
            }
        }

        // 右侧面板可视化设置
        // obj可选值：
        // 主页：Welcome
        // 人物：Character
        // 食物：Food
        // 科技：Science
        // 模拟：Cooking_Simulator
        // 生物：Animal
        // 自然：Natural
        // 物品：Goods
        // 设置：Setting
        private void RightPanelVisibility(string obj)
        {
            RightPanelVisibilityInitialize();
            switch (obj)
            {
                //欢迎界面
                case "Welcome":
                    Visi.VisiCol(false, RightGrid_Welcome);
                    Visi.VisiCol(true, RightGrid_Setting, RightGrid);
                    break;
                //设置界面
                case "Setting":
                    Visi.VisiCol(false, RightGrid_Setting);
                    Visi.VisiCol(true, RightGrid_Welcome, RightGrid);
                    break;
                //内容界面
                default:
                    //隐藏欢迎/设置界面
                    Visi.VisiCol(true, RightGrid_Welcome);
                    Visi.VisiCol(true, RightGrid_Setting);
                    //显示右侧内容Grid容器/分割器
                    Visi.VisiCol(false, RightGrid);
                    Visi.VisiCol(false, UI_Splitter);
                    switch (obj)
                    {
                        case "Character":
                            Visi.VisiCol(false, ScrollViewer_Left_Character, ScrollViewer_Right_Character);
                            SLWidth.MinWidth = 320;
                            SLWidth.Width = new GridLength(320);
                            break;
                        case "Food":
                            Visi.VisiCol(false, ScrollViewer_Left_Food, ScrollViewer_Right_Food);
                            SLWidth.MinWidth = 220;
                            SLWidth.Width = new GridLength(220);
                            break;
                        case "Science":
                            Visi.VisiCol(false, ScrollViewer_Left_Science, ScrollViewer_Right_Science);
                            SLWidth.MinWidth = 220;
                            SLWidth.Width = new GridLength(220);
                            break;
                        case "Cooking_Simulator":
                            Visi.VisiCol(false, ScrollViewer_Left_Cooking_Simulator, ScrollViewer_Right_Cooking_Simulator);
                            SLWidth.MinWidth = 220;
                            SLWidth.Width = new GridLength(220);
                            break;
                        case "Animal":
                            Visi.VisiCol(false, ScrollViewer_Left_Animal, ScrollViewer_Right_Animal);
                            SLWidth.MinWidth = 220;
                            SLWidth.Width = new GridLength(220);
                            break;
                        case "Natural":
                            Visi.VisiCol(false, ScrollViewer_Left_Natural, ScrollViewer_Right_Natural);
                            SLWidth.MinWidth = 220;
                            SLWidth.Width = new GridLength(220);
                            break;
                        case "Goods":
                            Visi.VisiCol(false, ScrollViewer_Left_Goods, ScrollViewer_Right_Goods);
                            SLWidth.MinWidth = 220;
                            SLWidth.Width = new GridLength(220);
                            break;
                    }
                    break;
            }
        }
        #endregion
        #endregion
    }
}
