using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Xml;
using 饥荒百科全书CSharp.Class;
using 饥荒百科全书CSharp.Class.DedicatedServers.DedicateServer;
using 饥荒百科全书CSharp.Class.DedicatedServers.Tools;
using 饥荒百科全书CSharp.MyUserControl.DedicatedServer;
using Button = System.Windows.Controls.Button;
using CheckBox = System.Windows.Controls.CheckBox;
using Label = System.Windows.Controls.Label;
using MessageBox = System.Windows.MessageBox;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;
using Orientation = System.Windows.Controls.Orientation;
using RadioButton = System.Windows.Controls.RadioButton;

namespace 饥荒百科全书CSharp.View
{

    public enum Message
    {
        保存,
        复活,
        回档,
        重置世界
    }
    /// <summary>
    /// DedicatedServerPage.xaml 的交互逻辑
    /// </summary>
    public partial class DedicatedServerPage : Page
    {
        public DedicatedServerPage()
        {
            InitializeComponent();
        }

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
                vControl.Visibility = Visibility.Collapsed;
            }

            Global.UiElementVisibility(Visibility.Visible, DediButtomBorderH1, DediButtomBorderH2, DediButtomBorderV1, DediButtomBorderV4);
        }

        private void DediButtomPanelVisibility(string obj)
        {
            DediButtomPanelVisibilityInitialize();
            switch (obj)
            {
                case "Setting":
                    DediSetting.Visibility = Visibility.Visible;
                    break;
                case "BaseSet":
                    DediBaseSet.Visibility = Visibility.Visible;
                    break;
                case "EditWorld":
                    DediWorldSet.Visibility = Visibility.Visible;
                    break;
                case "Mod":
                    DediModSet.Visibility = Visibility.Visible;
                    break;
                case "Rollback":
                    DediModRollBack.Visibility = Visibility.Visible;
                    break;
                case "Blacklist":
                    DediModManager.Visibility = Visibility.Visible;
                    break;
            }
        }
        #endregion

        private void DediButtomPanelInitalize()
        {
            //DediBaseSetRangeInitalize();



            string[] gameVersion = { "Steam", "TGP", "youxia" };
            DediSettingGameVersionSelect.ItemsSource = gameVersion;
            DediButtomPanelVisibilityInitialize();


            string[] noYes = { "否", "是" };
            string[] gamemode = { "生存", "荒野", "无尽" };
            var maxPlayer = new string[64];
            for (int i = 1; i <= 64; i++)
            {
                maxPlayer[i - 1] = i.ToString();
            }
            string[] offline = { "在线", "离线" };
            DediBaseSetGamemodeSelect.ItemsSource = gamemode;
            DediBaseSetPvpSelect.ItemsSource = noYes;
            DediBaseSetMaxPlayerSelect.ItemsSource = maxPlayer;
            DediBaseOfflineSelect.ItemsSource = offline;
            DediBaseIsPause.ItemsSource = noYes;
            DediBaseIsCave.ItemsSource = noYes;
            DediBaseSet.Visibility = Visibility.Visible;

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
            DediBaseSet.Visibility = Visibility.Visible;
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

        private void DediBaseSetHouseName_TextChanged(object sender, TextChangedEventArgs e)
        {
            DediMainTopWorldName.Text = DediBaseSetHouseName.Text;
            if (((RadioButton)DediLeftStackPanel.FindName("DediRadioButton" + CunDangCao))?.IsChecked == true)
            {
                ((RadioButton)DediLeftStackPanel.FindName(name: $"DediRadioButton{CunDangCao}")).Content = DediBaseSetHouseName.Text;

            }
        }

        private void DediBaseSetIntentionButton_Click(object sender, RoutedEventArgs e)
        {
            DediButtomPanelVisibilityInitialize();

            DediIntention.Visibility = Visibility.Visible;
        }

        #endregion
        #endregion

        #region "服务器面板"

        // 游戏平台改变,初始化一切
        private void DediSettingGameVersionSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 赋值

            GamePingTai = e.AddedItems[0].ToString();
            if (GamePingTai == "TGP")
            {
                DediCtrateOpenGame.Visibility = Visibility.Collapsed;
                DediCtrateWorldButton.Content = "保存世界";
            }
            else
            {
                DediCtrateOpenGame.Visibility = Visibility.Visible;
                DediCtrateWorldButton.Content = "创建世界";
            }
            if (e.RemovedItems.Count != 0)
            {
                InitServer();
            }
        }

        // 选择游戏exe文件
        private void DediSettingGameDirSelect_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog fileDialog = new OpenFileDialog();
            // fileDialog.InitialDirectory = "d:\\";
            fileDialog.Title = "选择游戏exe文件";
            if (_gamePingTai == "TGP")
            {
                fileDialog.Filter = "饥荒游戏exe文件(*.exe)|dontstarve_rail.exe";
            }
            else
            {
                fileDialog.Filter = "饥荒游戏exe文件(*.exe)|dontstarve_steam.exe";
            }
            fileDialog.FilterIndex = 1;
            fileDialog.RestoreDirectory = true;

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                String fileName = fileDialog.FileName;
                if (string.IsNullOrEmpty(fileName) || !fileName.Contains("dontstarve_"))
                {
                    System.Windows.MessageBox.Show("文件选择错误,请选择正确文件,以免出错");
                    return;
                }
                _pathAll.ClientFilePath = fileName;
                DediSettingGameDirSelectTextBox.Text = fileName;
                XmlHelper.WriteClientPath(fileName, GamePingTai);

            }
        }

        // 选择服务器文件
        private void DediSettingDediDirSelect_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            // fileDialog.InitialDirectory = "d:\\";
            fileDialog.Title = "选择服务器exe文件";

            fileDialog.Filter = "饥荒服务器exe文件(*.exe)|dontstarve_dedicated_server_nullrenderer.exe";


            fileDialog.FilterIndex = 1;
            fileDialog.RestoreDirectory = true;

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                String fileName = fileDialog.FileName;
                if (string.IsNullOrEmpty(fileName) || !fileName.Contains("dontstarve_dedicated_server_nullrenderer"))
                {
                    System.Windows.MessageBox.Show("文件选择错误,请选择正确文件,以免出错");
                    return;
                }
                _pathAll.ServerFilePath = fileName;
                DediSettingDediDirSelectTextBox.Text = fileName;
                XmlHelper.WriteServerPath(fileName, GamePingTai);

                // 读取mods
                _mods = null;
                if (!string.IsNullOrEmpty(_pathAll.ServerModsDirPath))
                {
                    _mods = new Mods(_pathAll.ServerModsDirPath);
                }
                SetModSet();


            }

        }

        // 双击打开所在文件夹"客户端"
        private void DediSettingGameDirSelectTextBox_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (!string.IsNullOrEmpty(_pathAll.ClientFilePath) && File.Exists(_pathAll.ClientFilePath))
            {
                Process.Start(Path.GetDirectoryName(_pathAll.ClientFilePath));
            }
        }
        // 双击打开所在文件夹"服务端"
        private void DediSettingDediDirSelectTextBox_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (!string.IsNullOrEmpty(_pathAll.ServerFilePath) && File.Exists(_pathAll.ServerFilePath))
            {
                Process.Start(Path.GetDirectoryName(_pathAll.ServerFilePath));
            }
        }

        // 删除当前存档按钮
        private void DediMainTop_Delete_Click(object sender, RoutedEventArgs e)
        {

            // 0. 关闭服务器

            Process[] ps = Process.GetProcesses();
            foreach (Process item in ps)
            {
                if (item.ProcessName == "dontstarve_dedicated_server_nullrenderer")
                {
                    item.Kill();
                }
            }

            // 1. radioBox 写 创建世界
            ((RadioButton)DediLeftStackPanel.FindName($"DediRadioButton{CunDangCao}")).Content = "创建世界";
            // 2. 删除当前存档
            if (Directory.Exists(_pathAll.YyServerDirPath))
            {
                Directory.Delete(_pathAll.YyServerDirPath, true);
            }

           // 2.1 取消选择,谁都不选
           ((RadioButton)DediLeftStackPanel.FindName($"DediRadioButton{CunDangCao}")).IsChecked = false;

            // 2.2 

            // DediMainBorder.IsEnabled = false;
            Jinyong(false);
            //// 3. 复制一份新的过来                 
            //ServerTools.Tool.CopyDirectory(pathAll.ServerMoBanPath, pathAll.DoNotStarveTogether_DirPath);


            //if (!Directory.Exists(pathAll.DoNotStarveTogether_DirPath + "\\Server_" + GamePingTai + "_" + CunDangCao))
            //{
            //    Directory.Move(pathAll.DoNotStarveTogether_DirPath + "\\Server", pathAll.DoNotStarveTogether_DirPath + "\\Server_" + GamePingTai + "_" + CunDangCao);
            //}
            //// 4. 读取新的存档
            //SetBaseSet();

        }

        // 创建世界按钮
        private void DediCtrateWorldButton_Click(object sender, RoutedEventArgs e)
        {
            RunServer();
        }
        // 打开游戏
        private void DediOpenGameButton_Click(object sender, RoutedEventArgs e)
        {
            RunClient();
        }




        private void DediSettingSaveCluster_Click(object sender, RoutedEventArgs e)
        {
            var flag = string.IsNullOrEmpty(DediSettingClusterTokenTextBox.Text);
            if (flag)
            {
                MessageBox.Show("cluster没填写，不能保存");
            }
            else
            {
                bool flag2 = this.DediSettingClusterTokenTextBox.Text.Trim() == "";
                if (flag2)
                {
                    MessageBox.Show("cluster没填写，不能保存");
                }
                else
                {
                    RegeditRw.RegWrite("cluster", this.DediSettingClusterTokenTextBox.Text.Trim());
                    MessageBox.Show("保存完毕！");
                }
            }
        }

        #endregion

        #region 字段、属性

        private int _cunDangCao = 0; // 存档槽
        private string _gamePingTai; // 游戏平台
        private readonly UTF8Encoding _utf8NoBom = new UTF8Encoding(false); // 编码
        private Dictionary<string, string> _hanhua;  // 汉化

        private PathAll _pathAll; // 路径
        private BaseSet _baseSet; // 基本设置
        private Leveldataoverride _overWorld; // 地上世界
        private Leveldataoverride _caves;     // 地下世界
        private Mods _mods;  // mods

        public string GamePingTai
        {
            get => _gamePingTai;
            set
            {
                XmlHelper.WriteGamePingTai(value);
                _gamePingTai = value;
            }
        }

        public int CunDangCao
        {
            get => _cunDangCao;
            set
            {
                _cunDangCao = value;
                _pathAll.CunDangCao = value;
            }
        }
        #endregion

        #region 设置
        // 初始化
        public void InitServer()
        {
            // -2.游侠修改为youxia，不用汉字了         
            string pingtail = RegeditRw.RegReadString("banben");

            if (!string.IsNullOrEmpty(pingtail))
            {
                if (pingtail == "游侠")
                {
                    RegeditRw.RegWrite("banben", "youxia");
                }
            }


            //-1.游戏平台
            SetPingTai();

            // 0.路径信息
            SetPath();

            // 1.检查存档Server是否存在 
            CheckServer();

            // 2.汉化
            _hanhua = XmlHelper.ReadHanhua();

            // 3.读取服务器mods文件夹下所有信息.mod多的话,读取时间也多
            //   此时的mod没有被current覆盖
            _mods = null;
            if (!string.IsNullOrEmpty(_pathAll.ServerModsDirPath))
            {
                _mods = new Mods(_pathAll.ServerModsDirPath);
            }
            // 4. "控制台"
            CreateConsoleButton();
            // 5.clusterToken
            this.DediSettingClusterTokenTextBox.Text = Class.RegeditRw.RegReadString("cluster");
            // 3."基本设置" 等在 点击radioButton后设置

        }
        //点击radioButton 时
        private void DediRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            // 0.保存之前的
            if (_overWorld != null & _caves != null & _mods != null & Directory.Exists(_pathAll.YyServerDirPath))
            {
                _overWorld.SaveWorld();
                _caves.SaveWorld();

                _mods.SaveListmodsToFile(_pathAll.YyServerDirPath + @"\Master\modoverrides.lua", _utf8NoBom);
                _mods.SaveListmodsToFile(_pathAll.YyServerDirPath + @"\Caves\modoverrides.lua", _utf8NoBom);
            }


            // 1.存档槽
            CunDangCao = (int)((RadioButton)sender).Tag;

            // 1.5 创建世界
            if (((RadioButton)sender).Content.ToString() == "创建世界")
            {
                // 复制一份过去                  
                //Tool.CopyDirectory(pathAll.ServerMoBanPath, pathAll.DoNotStarveTogether_DirPath);
                CopyServerModel(_pathAll.DoNotStarveTogetherDirPath);
                // 改名字
                if (!Directory.Exists(_pathAll.DoNotStarveTogetherDirPath + "\\Server_" + GamePingTai + "_" + CunDangCao))
                {
                    Directory.Move(_pathAll.DoNotStarveTogetherDirPath + "\\Server", _pathAll.DoNotStarveTogetherDirPath + "\\Server_" + GamePingTai + "_" + CunDangCao);

                }
               ((RadioButton)sender).Content = GetHouseName(CunDangCao);

            }
            // 1.6 复活
            Jinyong(true);

            // 2.【基本设置】
            SetBaseSet();

            // 3. "世界设置"
            SetOverWorldSet();
            // 3. "世界设置"
            SetCavesSet();
            // 4. "Mod"

            SetModSet();




        }

        // 设置 "Mod"
        private void SetModSet()
        {   // 设置
            if (!string.IsNullOrEmpty(_pathAll.ServerModsDirPath))
            {
                // 清空,Enabled变成默认值
                foreach (Mod item in _mods.ListMod)
                {
                    item.Enabled = false;
                }
                // 细节也要变成默认值,之后再重新读取1
                foreach (Mod item in _mods.ListMod)
                {
                    foreach (KeyValuePair<string, ModXiJie> item1 in item.Configuration_options)
                    {
                        item1.Value.Current = item1.Value.Default1;
                    }

                }

                // 重新读取
                _mods.ReadModsOverrides(_pathAll.ServerModsDirPath, _pathAll.YyServerDirPath + @"\Master\modoverrides.lua");
            }
            // 显示 
            DediModList.Children.Clear();
            DediModXiJie.Children.Clear();
            DediModDescription.Text = "";
            if (_mods != null)
            {
                for (int i = 0; i < _mods.ListMod.Count; i++)
                {
                    // 屏蔽 客户端MOD
                    if (_mods.ListMod[i].Tyype == ModType.客户端)
                    {
                        continue;
                    }
                    var dod = new DediModBox
                    {
                        Width = 200,
                        Height = 70,
                        UCTitle = {Content = _mods.ListMod[i].Name},
                        UCCheckBox = {Tag = i}
                    };
                    if (_mods.ListMod[i].Configuration_options.Count != 0)
                    {
                        dod.UCConfig.Source = new BitmapImage(new Uri("/饥荒百科全书CSharp;component/Resources/DedicatedServer/D_mp_mod_config.png", UriKind.Relative));
                    }
                    else
                    {
                        dod.UCConfig.Source = null;
                    }
                    dod.UCCheckBox.IsChecked = _mods.ListMod[i].Enabled;
                    dod.UCCheckBox.Checked += CheckBox_Checked;
                    dod.UCCheckBox.Unchecked += CheckBox_Unchecked;
                    dod.PreviewMouseLeftButtonDown += Dod_MouseLeftButtonDown;
                    dod.UCEnableLabel.Content = _mods.ListMod[i].Tyype;


                    DediModList.Children.Add(dod);
                }


            }

        }
        // 设置 "Mod" "MouseLeftButtonDown"
        private void Dod_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // 左边显示
            var n = (int)(((DediModBox)sender).UCCheckBox.Tag);
            var author = "作者:\r\n" + _mods.ListMod[n].Author + "\r\n\r\n";
            var description = "描述:\r\n" + _mods.ListMod[n].Description + "\r\n\r\n";
            var strName = "Mod名字:\r\n" + _mods.ListMod[n].Name + "\r\n\r\n";
            var version = "版本:\r\n" + _mods.ListMod[n].Version + "\r\n\r\n";
            var fileName = "文件夹:\r\n" + _mods.ListMod[n].DirName + "\r\n\r\n";
            DediModDescription.FontSize = 12;
            DediModDescription.TextWrapping = TextWrapping.WrapWithOverflow;
            DediModDescription.Text = strName + author + description + version + fileName;


            if (_mods.ListMod[n].Configuration_options.Count == 0)
            {
                // 没有细节配置项
                Debug.WriteLine(n);
                DediModXiJie.Children.Clear();

                var labelModXiJie = new Label
                {
                    Height = 300,
                    Width = 300,
                    Content = "QQ群: 580332268 \r\n mod类型:\r\n 所有人: 所有人都必须有.\r\n 服务器:只要服务器有就行",
                    FontWeight = FontWeights.Bold,
                    FontSize = 20
                };
                DediModXiJie.Children.Add(labelModXiJie);
            }
            else
            {
                // 有,显示细节配置项
                Debug.WriteLine(n);
                DediModXiJie.Children.Clear();
                foreach (KeyValuePair<string, ModXiJie> item in _mods.ListMod[n].Configuration_options)
                {
                    // stackPanel
                    var stackPanel = new StackPanel
                    {
                        Height = 40,
                        Width = 330,
                        Orientation = Orientation.Horizontal
                    };
                    var labelModXiJie = new Label
                    {
                        Height = stackPanel.Height,
                        Width = 180,
                        FontWeight = FontWeights.Bold,
                        Content = string.IsNullOrEmpty(item.Value.Label) ? item.Value.Name : item.Value.Label
                    };

                    // dediComboBox
                    var dod = new DediComboBox
                    {
                        Height = stackPanel.Height,
                        Width = 150,
                        FontSize = 12,
                        Tag = n + "$" + item.Key
                    };
                    // 把当前选择mod的第n个,放到tag里
                    foreach (var item1 in item.Value.Options)
                    {
                        dod.Items.Add(item1.Description);
                    }
                    dod.SelectedValue = item.Value.CurrentDescription;
                    dod.SelectionChanged += Dod_SelectionChanged;
                    // 添加
                    stackPanel.Children.Add(labelModXiJie);
                    stackPanel.Children.Add(dod);
                    DediModXiJie.Children.Add(stackPanel);

                }

            }
        }
        // 设置 "Mod" "SelectionChanged"
        private void Dod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Debug.WriteLine(((DediComboBox)sender).Tag);
            string[] str = ((DediComboBox)sender).Tag.ToString().Split('$');
            if (str.Length != 0)
            {
                int n = int.Parse(str[0]);
                string name = str[1];
                // 好复杂
                _mods.ListMod[n].Configuration_options[name].Current =
                    _mods.ListMod[n].Configuration_options[name].Options[((DediComboBox)sender).SelectedIndex].Data;

            }

        }
        // 设置 "Mod" "CheckBox_Unchecked"
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

            _mods.ListMod[(int)(((CheckBox)sender).Tag)].Enabled = false;

            //Debug.WriteLine(((CheckBox)sender).Tag.ToString());

        }
        // 设置 "Mod" "CheckBox_Checked"
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

            _mods.ListMod[(int)(((CheckBox)sender).Tag)].Enabled = true;

            //Debug.WriteLine(((CheckBox)sender).Tag.ToString());

        }

        // 设置"路径"
        private void SetPath()
        {
            _pathAll = new PathAll(GamePingTai, 0);
            DediSettingGameDirSelectTextBox.Text = "";
            if (!string.IsNullOrEmpty(_pathAll.ClientFilePath) && File.Exists(_pathAll.ClientFilePath))
            {
                DediSettingGameDirSelectTextBox.Text = _pathAll.ClientFilePath;
            }
            else
            {
                _pathAll.ClientFilePath = "";

            }
            DediSettingDediDirSelectTextBox.Text = "";
            if (!string.IsNullOrEmpty(_pathAll.ServerFilePath) && File.Exists(_pathAll.ServerFilePath))
            {
                DediSettingDediDirSelectTextBox.Text = _pathAll.ServerFilePath;
            }
            else
            {
                _pathAll.ServerFilePath = "";

            }

            Debug.WriteLine("路径读取-完");
        }
        // 设置"平台"
        private void SetPingTai()
        {
            _gamePingTai = XmlHelper.ReadGamePingTai();
            DediSettingGameVersionSelect.Text = _gamePingTai;
            Debug.WriteLine("游戏平台-完");
        }
        // 设置"基本"
        private void SetBaseSet()
        {
            var clusterIniFilePath = _pathAll.YyServerDirPath + @"\cluster.ini";
            if (!File.Exists(clusterIniFilePath))
            {
                //MessageBox.Show("cluster.ini不存在");
                return;
            }
            _baseSet = new BaseSet(clusterIniFilePath);

            DediBaseSetGamemodeSelect.DataContext = _baseSet;
            DediBaseSetPvpSelect.DataContext = _baseSet;
            DediBaseSetMaxPlayerSelect.DataContext = _baseSet;
            DediBaseOfflineSelect.DataContext = _baseSet;
            DediBaseSetHouseName.DataContext = _baseSet;
            DediBaseSetDescribe.DataContext = _baseSet;
            DediBaseSetSecret.DataContext = _baseSet;
            DediBaseOfflineSelect.DataContext = _baseSet;
            DediBaseIsPause.DataContext = _baseSet;
            DediBaseSetIntentionButton.DataContext = _baseSet;
            DediBaseIsCave.DataContext = _baseSet;
            Debug.WriteLine("基本设置-完");
        }
        // 设置"地上世界"
        private void SetOverWorldSet()
        {
            // 地上 
            _overWorld = new Leveldataoverride(_pathAll, false);
            DediOverWorldWorld.Children.Clear();
            DediOverWolrdFoods.Children.Clear();
            DediOverWorldAnimals.Children.Clear();
            DediOverWorldMonsters.Children.Clear();
            DediOverWorldResources.Children.Clear();
            // 地上 分类

            var overWorldFenLei = XmlHelper.ReadWorldFenLei(false);

            var foods = new Dictionary<string, ShowWorld>();
            var animals = new Dictionary<string, ShowWorld>();
            var monsters = new Dictionary<string, ShowWorld>();
            var resources = new Dictionary<string, ShowWorld>();
            var world = new Dictionary<string, ShowWorld>();

            #region 地上分类方法
            foreach (var item in _overWorld.ShowWorldDic)
            {
                if (overWorldFenLei.ContainsKey(item.Key))
                {
                    if (overWorldFenLei[item.Key] == "foods")
                    {
                        foods[item.Key] = item.Value;
                    }
                    if (overWorldFenLei[item.Key] == "animals")
                    {
                        animals[item.Key] = item.Value;
                    }
                    if (overWorldFenLei[item.Key] == "monsters")
                    {
                        monsters[item.Key] = item.Value;
                    }
                    if (overWorldFenLei[item.Key] == "resources")
                    {
                        resources[item.Key] = item.Value;
                    }
                    if (overWorldFenLei[item.Key] == "world")
                    {
                        world[item.Key] = item.Value;
                    }
                }
                else
                {
                    world[item.Key] = item.Value;
                }

            }

            #endregion

            #region "显示" 地上
            //  
            foreach (var item in world)
            {
                if (item.Value.ToolTip == "roads" || item.Value.ToolTip == "layout_mode" || item.Value.ToolTip == "wormhole_prefab")
                {
                    continue;
                }

                DediComboBoxWithImage di = new DediComboBoxWithImage()
                {
                    ImageSource = new BitmapImage(new Uri("/" + item.Value.PicPath, UriKind.Relative)),
                    ItemsSource = HanHua(item.Value.WorldconfigList),
                    SelectedValue = HanHua(item.Value.Worldconfig),
                    ImageToolTip = HanHua(item.Value.ToolTip),
                    Tag = item.Key,
                    Width = 200,
                    Height = 60

                };
                di.SelectionChanged += DiOverWorld_SelectionChanged;
                DediOverWorldWorld.Children.Add(di);

            }

            foreach (var item in foods)
            {
                var di = new DediComboBoxWithImage
                {
                    ImageSource = new BitmapImage(new Uri("/" + item.Value.PicPath, UriKind.Relative)),
                    ItemsSource = HanHua(item.Value.WorldconfigList),
                    SelectedValue = HanHua(item.Value.Worldconfig),
                    ImageToolTip = HanHua(item.Value.ToolTip),
                    Tag = item.Key,
                    Width = 200,
                    Height = 60
                };
                di.SelectionChanged += DiOverWorld_SelectionChanged;
                DediOverWolrdFoods.Children.Add(di);

            }

            foreach (var item in animals)
            {
                var di = new DediComboBoxWithImage
                {
                    ImageSource = new BitmapImage(new Uri("/" + item.Value.PicPath, UriKind.Relative)),
                    ItemsSource = HanHua(item.Value.WorldconfigList),
                    SelectedValue = HanHua(item.Value.Worldconfig),
                    ImageToolTip = HanHua(item.Value.ToolTip),
                    Tag = item.Key,
                    Width = 200,
                    Height = 60
                };
                di.SelectionChanged += DiOverWorld_SelectionChanged;
                DediOverWorldAnimals.Children.Add(di);

            }

            foreach (var item in monsters)
            {
                var di = new DediComboBoxWithImage
                {
                    ImageSource = new BitmapImage(new Uri("/" + item.Value.PicPath, UriKind.Relative)),
                    ItemsSource = HanHua(item.Value.WorldconfigList),
                    SelectedValue = HanHua(item.Value.Worldconfig),
                    ImageToolTip = HanHua(item.Value.ToolTip),
                    Tag = item.Key,
                    Width = 200,
                    Height = 60
                };
                di.SelectionChanged += DiOverWorld_SelectionChanged;
                DediOverWorldMonsters.Children.Add(di);

            }

            foreach (var item in resources)
            {
                var di = new DediComboBoxWithImage
                {
                    ImageSource = new BitmapImage(new Uri("/" + item.Value.PicPath, UriKind.Relative)),
                    ItemsSource = HanHua(item.Value.WorldconfigList),
                    SelectedValue = HanHua(item.Value.Worldconfig),
                    ImageToolTip = HanHua(item.Value.ToolTip),
                    Tag = item.Key,
                    Width = 200,
                    Height = 60
                };
                di.SelectionChanged += DiOverWorld_SelectionChanged;
                DediOverWorldResources.Children.Add(di);

            }

            #endregion

        }
        // 设置"地上世界"
        private void DiOverWorld_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //// 测试 用
            var dedi = (DediComboBoxWithImage)sender;
            //List<string> s = new List<string>();
            //s.Add("tag:" + Dedi.Tag.ToString());
            //s.Add("e.source:" + e.Source.ToString());
            //s.Add(e.AddedItems.Count.ToString());
            //s.Add(e.RemovedItems.Count.ToString());
            //s.Add(Dedi.SelectedIndex.ToString());
            //foreach (var item in s)
            //{
            //    Debug.WriteLine(item);
            //}

            // 此时说明修改
            if (e.RemovedItems.Count != 0 && e.AddedItems[0].ToString() == HanHua(_overWorld.ShowWorldDic[dedi.Tag.ToString()].WorldconfigList[dedi.SelectedIndex]))
            {
                _overWorld.ShowWorldDic[dedi.Tag.ToString()].Worldconfig = _overWorld.ShowWorldDic[dedi.Tag.ToString()].WorldconfigList[dedi.SelectedIndex];
                Debug.WriteLine(dedi.Tag + "选项变为:" + _overWorld.ShowWorldDic[dedi.Tag.ToString()].Worldconfig);

                // 保存,这样保存有点卡,换为每次点击radioButton或创建世界时
                //OverWorld.SaveWorld();
                //Debug.WriteLine("保存地上世界");
            }



        }

        // 设置"地下世界"
        private void SetCavesSet()
        {
            // 地下
            _caves = new Leveldataoverride(_pathAll, true);
            DediCavesWorld.Children.Clear();
            DediCavesFoods.Children.Clear();
            DediCavesAnimals.Children.Clear();
            DediCavesMonsters.Children.Clear();
            DediCavesResources.Children.Clear();
            // 地下 分类

            var fenleil = XmlHelper.ReadWorldFenLei(true);

            var foods = new Dictionary<string, ShowWorld>();
            var animals = new Dictionary<string, ShowWorld>();
            var monsters = new Dictionary<string, ShowWorld>();
            var resources = new Dictionary<string, ShowWorld>();
            var world = new Dictionary<string, ShowWorld>();


            #region  地下分类方法
            foreach (var item in _caves.ShowWorldDic)
            {
                if (fenleil.ContainsKey(item.Key))
                {
                    switch (fenleil[item.Key])
                    {
                        case "foods":
                            foods[item.Key] = item.Value;
                            break;
                        case "animals":
                            animals[item.Key] = item.Value;
                            break;
                        case "monsters":
                            monsters[item.Key] = item.Value;
                            break;
                        case "resources":
                            resources[item.Key] = item.Value;
                            break;
                        case "world":
                            world[item.Key] = item.Value;
                            break;
                    }
                }
                else
                {
                    world[item.Key] = item.Value;
                }

            }

            #endregion

            #region "显示" 地下
            // animals
            foreach (var item in world)
            {
                if (item.Value.ToolTip == "roads" || item.Value.ToolTip == "layout_mode" || item.Value.ToolTip == "wormhole_prefab")
                {
                    continue;
                }

                var di = new DediComboBoxWithImage
                {
                    ImageSource = new BitmapImage(new Uri("/" + item.Value.PicPath, UriKind.Relative)),
                    ItemsSource = HanHua(item.Value.WorldconfigList),
                    SelectedValue = HanHua(item.Value.Worldconfig),
                    ImageToolTip = HanHua(item.Value.ToolTip),
                    Tag = item.Key,
                    Width = 200,
                    Height = 60
                };
                di.SelectionChanged += DiCaves_SelectionChanged;
                DediCavesWorld.Children.Add(di);

            }

            foreach (var item in foods)
            {
                var di = new DediComboBoxWithImage
                {
                    ImageSource = new BitmapImage(new Uri("/" + item.Value.PicPath, UriKind.Relative)),
                    ItemsSource = HanHua(item.Value.WorldconfigList),
                    SelectedValue = HanHua(item.Value.Worldconfig),
                    ImageToolTip = HanHua(item.Value.ToolTip),
                    Tag = item.Key,
                    Width = 200,
                    Height = 60
                };
                di.SelectionChanged += DiCaves_SelectionChanged;
                DediCavesFoods.Children.Add(di);

            }

            foreach (var item in animals)
            {
                var di = new DediComboBoxWithImage
                {
                    ImageSource = new BitmapImage(new Uri("/" + item.Value.PicPath, UriKind.Relative)),
                    ItemsSource = HanHua(item.Value.WorldconfigList),
                    SelectedValue = HanHua(item.Value.Worldconfig),
                    ImageToolTip = HanHua(item.Value.ToolTip),
                    Tag = item.Key,
                    Width = 200,
                    Height = 60
                };
                di.SelectionChanged += DiCaves_SelectionChanged;
                DediCavesAnimals.Children.Add(di);

            }

            foreach (var item in monsters)
            {
                var di = new DediComboBoxWithImage
                {
                    ImageSource = new BitmapImage(new Uri("/" + item.Value.PicPath, UriKind.Relative)),
                    ItemsSource = HanHua(item.Value.WorldconfigList),
                    SelectedValue = HanHua(item.Value.Worldconfig),
                    ImageToolTip = HanHua(item.Value.ToolTip),
                    Tag = item.Key,
                    Width = 200,
                    Height = 60
                };
                di.SelectionChanged += DiCaves_SelectionChanged;
                DediCavesMonsters.Children.Add(di);

            }

            foreach (var item in resources)
            {
                var di = new DediComboBoxWithImage
                {
                    ImageSource = new BitmapImage(new Uri("/" + item.Value.PicPath, UriKind.Relative)),
                    ItemsSource = HanHua(item.Value.WorldconfigList),
                    SelectedValue = HanHua(item.Value.Worldconfig),
                    ImageToolTip = HanHua(item.Value.ToolTip),
                    Tag = item.Key,
                    Width = 200,
                    Height = 60
                };
                di.SelectionChanged += DiCaves_SelectionChanged;
                DediCavesResources.Children.Add(di);

            }

            #endregion


        }
        // 设置"地下世界"
        private void DiCaves_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //// 测试 用
            var dedi = (DediComboBoxWithImage)sender;


            // 此时说明修改
            if (e.RemovedItems.Count != 0 && e.AddedItems[0].ToString() == HanHua(_caves.ShowWorldDic[dedi.Tag.ToString()].WorldconfigList[dedi.SelectedIndex]))
            {
                _caves.ShowWorldDic[dedi.Tag.ToString()].Worldconfig = _caves.ShowWorldDic[dedi.Tag.ToString()].WorldconfigList[dedi.SelectedIndex];
                Debug.WriteLine(dedi.Tag + "选项变为:" + _caves.ShowWorldDic[dedi.Tag.ToString()].Worldconfig);

                // 保存,这样保存有点卡,换为每次点击radioButton或创建世界时
                //Caves.SaveWorld();
                //Debug.WriteLine("保存地上世界");
            }
        }

        // "检查"
        private void CheckServer()
        {

            if (!Directory.Exists(_pathAll.DoNotStarveTogetherDirPath))
            {
                Directory.CreateDirectory(_pathAll.DoNotStarveTogetherDirPath);
            }
            var dinfo = new DirectoryInfo(_pathAll.DoNotStarveTogetherDirPath);
            var dinfostr = dinfo.GetDirectories();

            var serverTgpPathList = new List<string>();
            foreach (var t in dinfostr)
            {
                if (t.Name.StartsWith("Server_" + GamePingTai + "_"))
                {
                    serverTgpPathList.Add(t.FullName);
                }
            }

            // 清空左边
            for (var i = 0; i < 20; i++)
            {
                ((RadioButton)DediLeftStackPanel.FindName($"DediRadioButton{i}")).Content = "创建世界";
                ((RadioButton)DediLeftStackPanel.FindName($"DediRadioButton{i}")).Tag = i;
                ((RadioButton)DediLeftStackPanel.FindName($"DediRadioButton{i}")).Checked += DediRadioButton_Checked;

            }


            // 等于0
            if (serverTgpPathList.Count == 0)
            {
                // 复制一份过去                  
                //Tool.CopyDirectory(pathAll.ServerMoBanPath, pathAll.DoNotStarveTogether_DirPath);
                CopyServerModel(_pathAll.DoNotStarveTogetherDirPath);

                // 改名字
                if (!Directory.Exists(_pathAll.DoNotStarveTogetherDirPath + "\\Server_" + GamePingTai + "_0"))
                {
                    Directory.Move(_pathAll.DoNotStarveTogetherDirPath + "\\Server", _pathAll.DoNotStarveTogetherDirPath + "\\Server_" + GamePingTai + "_0");
                }
            }
            else
            {
                for (int i = 0; i < serverTgpPathList.Count; i++)
                {
                    // 取出序号 
                    string Num = serverTgpPathList[i].Substring(serverTgpPathList[i].LastIndexOf('_') + 1);


                    // 取出存档名称
                    ((RadioButton)DediLeftStackPanel.FindName("DediRadioButton" + Num)).Content = GetHouseName(int.Parse(Num));


                }

            }

            // 禁用
            Jinyong(false);
            DediSettingGameVersionSelect.IsEnabled = true;
            // 不选择任何一项
            ((RadioButton)DediLeftStackPanel.FindName("DediRadioButton" + CunDangCao)).IsChecked = false;

            //// 选择第0个存档
            //((RadioButton)DediLeftStackPanel.FindName("DediRadioButton0")).IsChecked = true;
            //CunDangCao = 0;

        }
        #endregion

        #region 打开
        // 打开"客户端"
        private void RunClient()
        {

            if (string.IsNullOrEmpty(_pathAll.ClientModsDirPath))
            {
                MessageBox.Show("客户端路径没有设置");
                return;
            }
            var p = new Process
            {
                StartInfo =
                {
                    Arguments = "",
                    WorkingDirectory = Path.GetDirectoryName(_pathAll.ClientFilePath),
                    FileName = _pathAll.ClientFilePath
                }
            };
            // 目录,这个必须设置
            p.Start();
        }
        // 打开"服务器"
        private void RunServer()
        {

            if (_pathAll.ServerFilePath == null || _pathAll.ServerFilePath.Trim() == "")
            {
                MessageBox.Show("服务器路径不对,请重新设置服务器路径"); return;
            }

            // 保存世界
            if (_overWorld != null && _caves != null && _mods != null)
            {
                _overWorld.SaveWorld();
                _caves.SaveWorld();
                _mods.SaveListmodsToFile(_pathAll.YyServerDirPath + @"\Master\modoverrides.lua", _utf8NoBom);
                _mods.SaveListmodsToFile(_pathAll.YyServerDirPath + @"\Caves\modoverrides.lua", _utf8NoBom);
            }
            // 如果是youxia,强行设置为 离线,局域网
            if (GamePingTai == "youxia")
            {
                var ini1 = new IniHelper(_pathAll.YyServerDirPath + @"\cluster.ini", _utf8NoBom);
                ini1.Write("NETWORK", "offline_cluster", "true", _utf8NoBom);
                ini1.Write("NETWORK", "lan_only_cluster", "true", _utf8NoBom);

            }
            if (GamePingTai == "TGP")
            {
                var ini1 = new IniHelper(_pathAll.YyServerDirPath + @"\cluster.ini", _utf8NoBom);
                //ini1.write("NETWORK", "offline_cluster", "false", utf8NoBom);
                ini1.Write("NETWORK", "lan_only_cluster", "false", _utf8NoBom);
            }
            if (GamePingTai == "Steam")
            {
                var ini1 = new IniHelper(_pathAll.YyServerDirPath + @"\cluster.ini", _utf8NoBom);
                //ini1.write("NETWORK", "offline_cluster", "false", utf8NoBom);
                ini1.Write("NETWORK", "lan_only_cluster", "false", _utf8NoBom);
            }

            // 打开服务器
            var p = new Process();
            if (GamePingTai != "TGP")
            {

                p.StartInfo.UseShellExecute = false; // 是否
                p.StartInfo.WorkingDirectory = Path.GetDirectoryName(_pathAll.ServerFilePath); // 目录,这个必须设置
                p.StartInfo.FileName = _pathAll.ServerFilePath; ;  // 服务器名字

                p.StartInfo.Arguments = "-console -cluster Server_" + GamePingTai + "_" + CunDangCao.ToString() + " -shard Master";
                p.Start();
            }
            // 打开服务器
            if (GamePingTai == "TGP")
            {
                MessageBox.Show("保存完毕! 请通过TGP启动,存档文件名为" + GamePingTai + "_" + CunDangCao.ToString());
            }

            if (GamePingTai != "TGP")
            {

                // 是否开启洞穴
                if (DediBaseIsCave.Text == "是")
                {
                    p.StartInfo.Arguments = "-console -cluster Server_" + GamePingTai + "_" + CunDangCao.ToString() + " -shard Caves";
                    p.Start();
                }
            }



        }
        #endregion

        #region 控制台
        // 发送"消息"
        private static void SsendMessage(string messageStr)
        {
            var mySendMessage = new mySendMessage();

            // 得到句柄
            var pstr = Process.GetProcessesByName("dontstarve_dedicated_server_nullrenderer");

            // 根据句柄,发送消息
            foreach (var t in pstr)
            {
                mySendMessage.InputStr(t.MainWindowHandle, messageStr);
                mySendMessage.SendEnter(t.MainWindowHandle);
            }
        }

        /// <summary>
        /// 根据分类生产RadioButton
        /// </summary>
        private void CreateConsoleButton()
        {
            DediConsoleFenLei.Children.Clear();
            // 读取分类信息
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            var sStream = assembly.GetManifestResourceStream("饥荒百科全书CSharp.XML.ItemList.xml");
            var xmldoc = new XmlDocument();
            xmldoc.Load(sStream);

            var details = xmldoc.SelectNodes("/items/*");

            foreach (XmlNode item in details)
            {

                var chinese = ((XmlElement)item).GetAttribute("chinese");
                // RadioButton
                var b = new RadioButton()
                {
                    Content = chinese,
                    Width = 140,
                    Height = 40,
                    Foreground = new SolidColorBrush(Colors.Black),
                    Tag = ((XmlElement)item),
                    FontWeight = FontWeights.Bold,
                    Style = (Style)this.FindResource("RadioButtonStyle")

                };
                b.Checked += B_Click;
                if (b.Content.ToString() == "其他")
                {
                    b.IsChecked = true;
                }

                DediConsoleFenLei.Children.Add(b);
            }


        }

        // 显示具体分类信息
        private void B_Click(object sender, RoutedEventArgs e)
        {
            // 把当前选择的值放到这里了
            DediConsoleFenLei.Tag = ((RadioButton)sender).Content;
            var item = (XmlElement)((RadioButton)sender).Tag;
            DediConsoleDetails.Children.Clear();
            // 显示具体分类信息
            var xnl = item.ChildNodes;
            foreach (XmlNode x in xnl)
            {
                var codestr = ((XmlElement)x).GetAttribute("code");
                var chinesestr = ((XmlElement)x).GetAttribute("chinese");
                if (string.IsNullOrEmpty(chinesestr))
                {
                    continue;
                }
                // 按钮
                Button bx = new Button()
                {
                    Content = chinesestr,
                    Width = 115,
                    Height = 35,
                    Tag = codestr,
                    FontWeight = FontWeights.Bold,
                    Style = (Style)this.FindResource("DediButtonCreateWorldStyle")

                };
                bx.Click += Bx_Click;
                DediConsoleDetails.Children.Add(bx);


            }
        }

        private void Bx_Click(object sender, RoutedEventArgs e)
        {
            var code = ((Button)sender).Tag.ToString();
            // 如果是其他分类,则直接运行code
            if (DediConsoleFenLei.Tag.ToString() == "其他")
            {
                SsendMessage(code);
                System.Windows.Forms.Clipboard.SetDataObject(code);
            }
            // 如果不是其他
            else
            {
                SsendMessage("c_give(\"" + code + "\", 1)");
                System.Windows.Forms.Clipboard.SetDataObject("c_give(\"" + code + "\", 1)");
            }





        }
        #endregion

        #region 其他
        // 获取房间名
        private string GetHouseName(int d_cundangcao)
        {
            var clusterIniPath = _pathAll.DoNotStarveTogetherDirPath + @"\Server_" + GamePingTai + "_" + d_cundangcao.ToString() + @"\cluster.ini";
            if (!File.Exists(clusterIniPath))
            {
                return "创建世界";
            }
            var iniTool = new IniHelper(clusterIniPath, _utf8NoBom);

            var houseName = iniTool.ReadValue("NETWORK", "cluster_name");
            return houseName;
        }
        // 禁用
        private void Jinyong(bool b)
        {
            // DediMainBorder.IsEnabled = b;
            DediTitleBaseSet.IsEnabled = b;
            DediTitleEditWorld.IsEnabled = b;
            DediTitleMod.IsEnabled = b;
            DediTitleRollback.IsEnabled = b;

            DediMainTopDelete.IsEnabled = b;
            DediCtrateWorldButton.IsEnabled = b;
            DediBaseSet.IsEnabled = b;
        }
        // 汉化
        private string HanHua(string s)
        {
            return _hanhua.ContainsKey(s) ? _hanhua[s] : s;
        }

        private IEnumerable<string> HanHua(IEnumerable<string> s)
        {
            var r = new List<string>();
            foreach (var item in s)
            {
                r.Add(_hanhua.ContainsKey(item) ? _hanhua[item] : item);
            }
            return r;
        }

        // 复制Server模板到指定位置
        private void CopyServerModel(string path)
        {
            // 判断是否存在
            if (Directory.Exists(path + @"\Server"))
            {
                Directory.Delete(path + @"\Server", true);
            }
            // 建立文件夹
            Directory.CreateDirectory(path + @"\Server");
            Directory.CreateDirectory(path + @"\Server\Caves");
            Directory.CreateDirectory(path + @"\Server\Master");

            // 填文件
            File.WriteAllText(path + @"\Server\cluster.ini", Tool.ReadResources("Server模板.cluster.ini"), _utf8NoBom);
            File.WriteAllText(path + @"\Server\Caves\leveldataoverride.lua", Tool.ReadResources("Server模板.Caves.leveldataoverride.lua"), _utf8NoBom);
            File.WriteAllText(path + @"\Server\Caves\modoverrides.lua", Tool.ReadResources("Server模板.Caves.modoverrides.lua"), _utf8NoBom);
            File.WriteAllText(path + @"\Server\Caves\server.ini", Tool.ReadResources("Server模板.Caves.server.ini"), _utf8NoBom);
            File.WriteAllText(path + @"\Server\Master\leveldataoverride.lua", Tool.ReadResources("Server模板.Master.leveldataoverride.lua"), _utf8NoBom);
            File.WriteAllText(path + @"\Server\Master\modoverrides.lua", Tool.ReadResources("Server模板.Master.modoverrides.lua"), _utf8NoBom);
            File.WriteAllText(path + @"\Server\Master\server.ini", Tool.ReadResources("Server模板.Master.server.ini"), _utf8NoBom);

            // clusterToken
            var flag2 = !string.IsNullOrEmpty(RegeditRw.RegReadString("cluster"));
            File.WriteAllText(path + "\\Server\\cluster_token.txt", flag2 ? RegeditRw.RegReadString("cluster") : "",
                _utf8NoBom);
        }
        #endregion
        
    }
}
