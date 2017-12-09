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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Xml;
using Newtonsoft.Json;
using 饥荒百科全书CSharp.Class;
using 饥荒百科全书CSharp.Class.DedicatedServers.DedicateServer;
using 饥荒百科全书CSharp.Class.DedicatedServers.JsonDeserialize;
using 饥荒百科全书CSharp.Class.DedicatedServers.Tools;
using 饥荒百科全书CSharp.MyUserControl.DedicatedServer;

namespace 饥荒百科全书CSharp.View
{
    /// <summary>
    /// 枚举类型 Message
    /// </summary>
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
        #region 字段、属性
        private readonly UTF8Encoding _utf8WithoutBom = new UTF8Encoding(false); // 编码
        private Dictionary<string, string> _hanhua;  // 汉化
        private PathAll _pathAll; // 路径
        private BaseSet _baseSet; // 基本设置
        private Leveldataoverride _overWorld; // 地上世界
        private Leveldataoverride _caves;     // 地下世界
        private Mods _mods;  // mods
        private bool _firstLoad = true; // 首次加载
        public int SaveSlot { get; set; } // 存档槽
        #endregion

        /// <summary>
        /// 构造事件
        /// </summary>
        public DedicatedServerPage()
        {
            InitializeComponent();
            // 当前路径
            PathCommon.CurrentDirPath = Environment.CurrentDirectory;
            //初始化左侧选择存档RadioButton的Tag
            for (var i = 0; i < 20; i++)
            {
                // ReSharper disable once PossibleNullReferenceException
                ((RadioButton)SaveSlotStackPanel.FindName($"SaveSlotRadioButton{i}")).Tag = i;
            }
            // 初始化服务器面板
            DedicatedServerPanelInitalize();
        }

        /// <summary>
        /// 初始化服务器面板
        /// </summary>
        private void DedicatedServerPanelInitalize()
        {
            // 隐藏所有面板
            DediButtomPanelVisibilityInitialize();
            #region ComboBox初始化
            string[] noYes = { "否", "是" };
            var maxPlayer = new string[64];
            for (var i = 1; i <= 64; i++)
            {
                maxPlayer[i - 1] = i.ToString();
            }
            DediBaseSetGamemodeSelect.ItemsSource = new[] { "生存", "荒野", "无尽" };
            DediBaseSetPvpSelect.ItemsSource = noYes;
            DediBaseSetMaxPlayerSelect.ItemsSource = maxPlayer;
            DediBaseOfflineSelect.ItemsSource = new[] { "在线", "离线" };
            DediBaseIsPause.ItemsSource = noYes;
            IsCaveComboBox.ItemsSource = noYes;
            #endregion
            #region 设置PathCommon类数据
            // 服务器版本[Steam/WeGame]
            DediSettingGameVersionSelect.ItemsSource = new[] { "Steam", "WeGame" };
            // 检查通用设置
            CheckCommonSetting();
            #endregion
            // 初始化
            InitServer();
            // 显示通用设置面板
            DediButtomPanelVisibility("Setting");
        }

        #region 主面板菜单

        #region "面板菜单按钮"
        private void TitleMenuBaseSet_Click(object sender, RoutedEventArgs e)
        {
            DediButtomPanelVisibility("BaseSet");
        }

        private void TitleMenuEditWorld_Click(object sender, RoutedEventArgs e)
        {
            DediButtomPanelVisibility("EditWorld");
        }

        private void TitleMenuMod_Click(object sender, RoutedEventArgs e)
        {
            DediButtomPanelVisibility("Mod");
        }

        private void TitleMenuRollback_Click(object sender, RoutedEventArgs e)
        {
            DediButtomPanelVisibility("Rollback");
        }

        //TODO 黑名单列表
        //private void DediTitleBlacklist_Click(object sender, RoutedEventArgs e)
        //{
        //    DediButtomPanelVisibility("Blacklist");
        //}

        /// <summary>
        /// 通用设置
        /// </summary>
        private void CommonSettingButton_Click(object sender, RoutedEventArgs e)
        {
            DediButtomPanelVisibility("Setting");
        }

        /// <summary>
        /// 打开客户端
        /// </summary>
        private static void RunClient()
        {
            if (string.IsNullOrEmpty(PathCommon.ClientModsDirPath))
            {
                MessageBox.Show("客户端路径没有设置");
                return;
            }
            var process = new Process
            {
                StartInfo =
                {
                    Arguments = "",
                    WorkingDirectory = Path.GetDirectoryName(PathCommon.ClientFilePath) ?? throw new InvalidOperationException(),
                    FileName = PathCommon.ClientFilePath
                }
            };
            // 目录,这个必须设置
            process.Start();
        }

        /// <summary>
        /// 打开服务器
        /// </summary>
        private void RunServer()
        {
            if (PathCommon.ServerFilePath == null || PathCommon.ServerFilePath.Trim() == "")
            {
                MessageBox.Show("服务器路径不对,请重新设置服务器路径"); return;
            }
            // 保存世界
            if (_overWorld != null && _caves != null && _mods != null)
            {
                _overWorld.SaveWorld();
                _caves.SaveWorld();
                _mods.SaveListmodsToFile(_pathAll.ServerDirPath + @"\Master\modoverrides.lua", _utf8WithoutBom);
                _mods.SaveListmodsToFile(_pathAll.ServerDirPath + @"\Caves\modoverrides.lua", _utf8WithoutBom);
            }
            // 服务器启动
            if (PathCommon.GamePlatform == "WeGame")
            {
                MessageBox.Show("保存完毕! 请通过WeGame启动,存档文件名为" + PathCommon.GamePlatform + "_" + SaveSlot);
            }
            else
            {
                // 打开服务器(地面)
                var masterProcess = new Process
                {
                    StartInfo =
                    {
                        UseShellExecute = false, // 是否
                        WorkingDirectory = Path.GetDirectoryName(PathCommon.ServerFilePath) ?? throw new InvalidOperationException(), // 目录,这个必须设置
                        FileName = PathCommon.ServerFilePath, // 服务器名字
                        Arguments = "-console -cluster Server_" + PathCommon.GamePlatform + "_" + SaveSlot +" -shard Master"
                    }
                };
                masterProcess.Start();
                // 打开服务器(洞穴)
                if (IsCaveComboBox.Text == "是")
                {
                    var caveProcess = new Process
                    {
                        StartInfo =
                        {
                            UseShellExecute = false, // 是否
                            WorkingDirectory = Path.GetDirectoryName(PathCommon.ServerFilePath) ?? throw new InvalidOperationException(), // 目录,这个必须设置
                            FileName = PathCommon.ServerFilePath, // 服务器名字
                            Arguments = "-console -cluster Server_" + PathCommon.GamePlatform + "_" + SaveSlot +" -shard Caves"
                        }
                    };
                    caveProcess.Start();
                }
            }
        }
        #endregion

        #region "主面板Visibility属性设置"
        /// <summary>
        /// 隐藏所有面板
        /// </summary>
        private void DediButtomPanelVisibilityInitialize()
        {
            foreach (UIElement vControl in ButtomGrid.Children)
            {
                vControl.Visibility = Visibility.Collapsed;
            }
            Global.UiElementVisibility(Visibility.Visible, DediButtomBorderH1, DediButtomBorderH2, DediButtomBorderV1, DediButtomBorderV4);
        }

        // 显示指定面板
        private void DediButtomPanelVisibility(string obj)
        {
            DediButtomPanelVisibilityInitialize();
            switch (obj)
            {
                case "Setting":
                    DediSetting.Visibility = Visibility.Visible;
                    TitleMenuBaseSet.IsChecked = false;
                    TitleMenuEditWorld.IsChecked = false;
                    TitleMenuMod.IsChecked = false;
                    TitleMenuRollback.IsChecked = false;
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
                    DediConsole.Visibility = Visibility.Visible;
                    break;
                case "Blacklist":
                    DediModManager.Visibility = Visibility.Visible;
                    break;
            }
        }
        #endregion

        #endregion

        #region "通用设置面板"

        /// <summary>
        /// 游戏平台改变,初始化一切
        /// </summary>
        private void DediSettingGameVersionSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 赋值
            PathCommon.GamePlatform = e.AddedItems[0].ToString();
            if (PathCommon.GamePlatform == "WeGame")
            {
                CtrateRunGame.Visibility = Visibility.Collapsed;
                CtrateWorldButton.Content = "保存世界";
            }
            else
            {
                CtrateRunGame.Visibility = Visibility.Visible;
                CtrateWorldButton.Content = "创建世界";
            }
            if (e.RemovedItems.Count != 0)
            {
                // 初始化
                InitServer();
            }
        }

        /// <summary>
        /// 左侧SaveSlotRadioButton Click事件
        /// </summary>
        private void SaveSlotRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            // 存档
            SaveSlot = int.Parse(((RadioButton)sender).Name.Remove(0, 19));
            // -1.保存之前的
            if (_overWorld != null && _caves != null && _mods != null && Directory.Exists(_pathAll.ServerDirPath))
            {
                // 地面和洞穴世界保存
                _overWorld.SaveWorld();
                _caves.SaveWorld();
                // 地面和洞穴MOD保存
                _mods.SaveListmodsToFile(_pathAll.ServerDirPath + @"\Master\modoverrides.lua", _utf8WithoutBom);
                _mods.SaveListmodsToFile(_pathAll.ServerDirPath + @"\Caves\modoverrides.lua", _utf8WithoutBom);
            }
            // 1.复制文件
            if (((RadioButton)sender).Content.ToString() == "创建世界")
            {
                // 复制一份过去                  
                CopyServerFile();
                // 改名字
                if (!Directory.Exists(PathCommon.SaveRootDirPath + @"\Server_" + PathCommon.GamePlatform + "_" + SaveSlot))
                {
                    Directory.Move(PathCommon.SaveRootDirPath + @"\Server", PathCommon.SaveRootDirPath + @"\Server_" + PathCommon.GamePlatform + "_" + SaveSlot);
                    // 删除临时文件
                    if (Directory.Exists(PathCommon.SaveRootDirPath + @"\Server"))
                    {
                        Directory.Delete(PathCommon.SaveRootDirPath + @"\Server", true);
                    }
                }
                ((RadioButton)sender).Content = GetHouseName(SaveSlot);
            }
            // 2.初始化服务器
            InitServer();
            // 3 禁用
            JinYong(false);
            // 4.读取并设定基本设置
            SetBaseSet();
            // 5. "地面世界设置"
            SetOverWorldSet();
            // 5. "洞穴世界设置"
            SetCavesSet();
            // 6.打开基本设置页面
            TitleMenuBaseSet.IsChecked = true;
            TitleMenuBaseSet_Click(null, null);
        }

        /// <summary>
        /// 初始化服务器
        /// </summary>
        public void InitServer()
        {
            //-1.游戏平台
            PathCommon.GamePlatform = PathCommon.ReadGamePlatform();
            DediSettingGameVersionSelect.Text = PathCommon.GamePlatform;
            // 0.路径信息
            if (_firstLoad == false)
            {
                SetPath();
            }
            // 1.检查存档Server是否存在 
            CheckServer();
            // 2.汉化
            _hanhua = JsonHelper.ReadHanhua();
            // 3.读取服务器mods文件夹下所有信息.mod多的话,读取时间也多
            //   此时的mod没有被current覆盖
            _mods = null;
            if (!string.IsNullOrEmpty(PathCommon.ServerModsDirPath) && _firstLoad == false)
            {
                _mods = new Mods(PathCommon.ServerModsDirPath);
                SetModSet();
            }
            // 4. "控制台"
            CreateConsoleClassificationButton();
            // 5.如果_firstLoad为真则设置为假
            if (_firstLoad)
                _firstLoad = false;
        }

        /// <summary>
        /// 设置"路径"
        /// </summary>
        private void SetPath()
        {
            _pathAll = new PathAll(SaveSlot);
            PathCommon.GamePlatform = PathCommon.ReadGamePlatform();
            // 客户端路径
            GameDirSelectTextBox.Text = "";
            PathCommon.ClientFilePath = PathCommon.ReadClientPath(PathCommon.GamePlatform);
            if (!string.IsNullOrEmpty(PathCommon.ClientFilePath) && File.Exists(PathCommon.ClientFilePath))
            {
                GameDirSelectTextBox.Text = PathCommon.ClientFilePath;
            }
            else
            {
                PathCommon.ClientFilePath = "";
            }
            // 服务器路径
            DediDirSelectTextBox.Text = "";
            PathCommon.ServerFilePath = PathCommon.ReadServerPath(PathCommon.GamePlatform);
            if (!string.IsNullOrEmpty(PathCommon.ServerFilePath) && File.Exists(PathCommon.ServerFilePath))
            {
                DediDirSelectTextBox.Text = PathCommon.ServerFilePath;
            }
            else
            {
                PathCommon.ServerFilePath = "";
            }
            // ClusterToken
            DediSettingClusterTokenTextBox.Text = "";
            PathCommon.ClusterToken = PathCommon.ReadClusterTokenPath(PathCommon.GamePlatform);
            if (!string.IsNullOrEmpty(PathCommon.ClusterToken))
            {
                DediSettingClusterTokenTextBox.Text = PathCommon.ClusterToken;
            }
            else
            {
                PathCommon.ClusterToken = "";
            }
        }

        /// <summary>
        /// "检查"服务器目录
        /// </summary>
        private void CheckServer()
        {
            if (!Directory.Exists(PathCommon.SaveRootDirPath))
            {
                Directory.CreateDirectory(PathCommon.SaveRootDirPath);
            }
            var directoryInfos = new DirectoryInfo(PathCommon.SaveRootDirPath).GetDirectories();
            var serverPathList = (from directoryInfo in directoryInfos where directoryInfo.Name.StartsWith("Server_" + PathCommon.GamePlatform + "_") select directoryInfo.FullName).ToList();
            // 清空左边
            for (var i = 0; i < 20; i++)
            {
                // ReSharper disable once PossibleNullReferenceException
                ((RadioButton)SaveSlotStackPanel.FindName($"SaveSlotRadioButton{i}")).Content = "创建世界";
            }
            // 等于0
            if (serverPathList.Count != 0)
            {
            //    // 复制一份过去                  
            //    //Tool.CopyDirectory(pathAll.ServerMoBanPath, pathAll.DoNotStarveTogether_DirPath);
            //    CopyServerModel(PathCommon.SaveRootDirPath);
            //    // 改名字
            //    if (!Directory.Exists(PathCommon.SaveRootDirPath + @"\Server_" + PathCommon.GamePlatform + "_0"))
            //    {
            //        Directory.Move(PathCommon.SaveRootDirPath + @"\Server", PathCommon.SaveRootDirPath + @"\Server_" + PathCommon.GamePlatform + "_0");
            //    }
            //}
            //else
            //{
                foreach (var serverPath in serverPathList)
                {
                    // 取出序号 
                    var num = serverPath.Substring(serverPath.LastIndexOf('_') + 1);
                    // 取出存档名称
                    // ReSharper disable once PossibleNullReferenceException
                    ((RadioButton)SaveSlotStackPanel.FindName("SaveSlotRadioButton" + num)).Content = GetHouseName(int.Parse(num));
                }
            }
            // 禁用
            JinYong(true);
        }

        /// <summary>
        /// 复制Server模板到指定位置
        /// </summary>
        private void CopyServerFile()
        {
            var path = PathCommon.SaveRootDirPath;
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
            File.WriteAllText(path + @"\Server\cluster.ini", Tool.ReadResources("Server模板.cluster.ini"), _utf8WithoutBom);
            File.WriteAllText(path + @"\Server\Caves\leveldataoverride.lua", Tool.ReadResources("Server模板.Caves.leveldataoverride.lua"), _utf8WithoutBom);
            File.WriteAllText(path + @"\Server\Caves\modoverrides.lua", Tool.ReadResources("Server模板.Caves.modoverrides.lua"), _utf8WithoutBom);
            File.WriteAllText(path + @"\Server\Caves\server.ini", Tool.ReadResources("Server模板.Caves.server.ini"), _utf8WithoutBom);
            File.WriteAllText(path + @"\Server\Master\leveldataoverride.lua", Tool.ReadResources("Server模板.Master.leveldataoverride.lua"), _utf8WithoutBom);
            File.WriteAllText(path + @"\Server\Master\modoverrides.lua", Tool.ReadResources("Server模板.Master.modoverrides.lua"), _utf8WithoutBom);
            File.WriteAllText(path + @"\Server\Master\server.ini", Tool.ReadResources("Server模板.Master.server.ini"), _utf8WithoutBom);
            // clusterToken
            File.WriteAllText(path + @"\Server\cluster_token.txt", !string.IsNullOrEmpty(RegeditRw.RegReadString("ClusterToken")) ? RegeditRw.RegReadString("ClusterToken") : "", _utf8WithoutBom);
        }

        /// <summary>
        /// 选择游戏exe文件
        /// </summary>
        private void DediSettingGameDirSelect_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Title = "选择游戏exe文件",
                FileName = PathCommon.GamePlatform == "WeGame"
                    ? "dontstarve_rail"
                    : "dontstarve_steam", //默认文件名
                DefaultExt = ".exe",// 默认文件扩展名
                Filter = PathCommon.GamePlatform == "WeGame"
                    ? "饥荒游戏exe文件(*.exe)|dontstarve_rail.exe"
                    : "饥荒游戏exe文件(*.exe)|dontstarve_steam.exe",
                FilterIndex = 1,
                RestoreDirectory = true
            };
            if (openFileDialog.ShowDialog() == true)
            {
                var fileName = openFileDialog.FileName;
                if (string.IsNullOrEmpty(fileName) || !fileName.Contains("dontstarve_"))
                {
                    MessageBox.Show("文件选择错误,请选择正确文件");
                    return;
                }
                PathCommon.ClientFilePath = fileName;
                GameDirSelectTextBox.Text = fileName;
                PathCommon.WriteClientPath(fileName, PathCommon.GamePlatform);
                // 检查通用设置
                CheckCommonSetting(true);
            }
        }

        /// <summary>
        /// 选择服务器文件
        /// </summary>
        private void DediSettingDediDirSelect_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Title = "选择服务器exe文件",
                FileName = "dontstarve_dedicated_server_nullrenderer", //默认文件名
                DefaultExt = ".exe",// 默认文件扩展名
                Filter = "饥荒服务器exe文件(*.exe)|dontstarve_dedicated_server_nullrenderer.exe",
                FilterIndex = 1,
                RestoreDirectory = true
            };
            if (openFileDialog.ShowDialog() == true)
            {
                var fileName = openFileDialog.FileName;
                if (string.IsNullOrEmpty(fileName) || !fileName.Contains("dontstarve_dedicated_server_nullrenderer"))
                {
                    MessageBox.Show("文件选择错误,请选择正确文件");
                    return;
                }
                PathCommon.ServerFilePath = fileName;
                DediDirSelectTextBox.Text = fileName;
                PathCommon.WriteServerPath(fileName, PathCommon.GamePlatform);
                // 检查通用设置
                CheckCommonSetting(true);
            }
        }

        /// <summary>
        /// 双击打开所在文件夹"客户端"
        /// </summary>
        private void DediSettingGameDirSelectTextBox_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (!string.IsNullOrEmpty(PathCommon.ClientFilePath) && File.Exists(PathCommon.ClientFilePath))
            {
                Process.Start(Path.GetDirectoryName(PathCommon.ClientFilePath) ?? throw new InvalidOperationException());
            }
        }

        /// <summary>
        /// 双击打开所在文件夹"服务端"
        /// </summary>
        private void DediSettingDediDirSelectTextBox_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (!string.IsNullOrEmpty(PathCommon.ServerFilePath) && File.Exists(PathCommon.ServerFilePath))
            {
                Process.Start(Path.GetDirectoryName(PathCommon.ServerFilePath) ?? throw new InvalidOperationException());
            }
        }

        // 保存ClusterToken
        private void DediSettingSaveCluster_Click(object sender, RoutedEventArgs e)
        {
            var clusterToken = DediSettingClusterTokenTextBox.Text.Trim();
            if (string.IsNullOrEmpty(clusterToken))
            {
                MessageBox.Show("clusterToken没填写，不能保存");
            }
            else
            {
                // 判断ClusterToken有效性
                if (clusterToken.Length < 6)
                {
                    MessageBox.Show("clusterToken过短，不能保存");
                    return;
                }
                var flag = clusterToken.Length != 32;
                if (clusterToken.Substring(0, 6) == "pds-g^" && clusterToken.Length == 62)
                {
                    flag = false;

                }
                if (flag)
                {
                    if (MessageBox.Show("clusterToken格式不正确，确定依然要保存吗？", "出错了唉",
                            MessageBoxButton.YesNo) == MessageBoxResult.No)
                        return;
                }
                // 确定有效↓
                PathCommon.ClusterToken = clusterToken;
                PathCommon.WriteClusterTokenPath(clusterToken, PathCommon.ReadGamePlatform());
                MessageBox.Show("保存完毕！");
                // 检查通用设置
                CheckCommonSetting(true);
            }
        }
        #endregion

        #region "游戏风格"
        private void DediIntention_Click(object sender, RoutedEventArgs e)
        {
            DediButtomPanelVisibilityInitialize();
            DediBaseSet.Visibility = Visibility.Visible;
            switch (((Button)sender).Name)
            {
                case "IntentionSocialButton":
                    DediBaseSetIntentionButton.Content = "交际";
                    break;
                case "IntentionCooperativeButton":
                    DediBaseSetIntentionButton.Content = "合作";
                    break;
                case "IntentionCompetitiveButton":
                    DediBaseSetIntentionButton.Content = "竞争";
                    break;
                case "IntentionMadnessButton":
                    DediBaseSetIntentionButton.Content = "疯狂";
                    break;
            }
        }

        private void DediIntention_MouseEnter(object sender, MouseEventArgs e)
        {
            switch (((Button)sender).Name)
            {
                case "IntentionSocialButton":
                    DidiIntentionTextBlock.Text = "这是一个闲聊&扯蛋的地方。\r\n轻松的游戏风格，只是为了互相沟通&扯蛋。\r\n还等什么，快进来一起扯蛋吧~~";
                    break;
                case "IntentionCooperativeButton":
                    DidiIntentionTextBlock.Text = "一个团队生存的世界。在这个世界，我们要一起合作，尽我们可能来驯服这个充满敌意的世界。";
                    break;
                case "IntentionCompetitiveButton":
                    DidiIntentionTextBlock.Text = "这是一个完美的舞台。\r\n展示你的生存能力，战斗能力、建设能力...吧！";
                    break;
                case "IntentionMadnessButton":
                    DidiIntentionTextBlock.Text = "在这里，你将过着茹毛饮血的生活！\r\n是你吃掉粮食还是被粮食吃掉呢？\r\n让我们拭目以待吧！";
                    break;
            }
        }

        private void DediIntention_MouseLeave(object sender, MouseEventArgs e)
        {
            DidiIntentionTextBlock.Text = "";
        }
        #endregion

        #region "基本设置面板"
        /// <summary>
        /// 修改房间名时顶部显示房间名和左侧显示房间名同步修改
        /// </summary>
        private void DediBaseSetHouseName_TextChanged(object sender, TextChangedEventArgs e)
        {
            DediMainTopWorldName.Text = DediBaseSetHouseName.Text;
            if (((RadioButton)SaveSlotStackPanel.FindName("SaveSlotRadioButton" + SaveSlot))?.IsChecked == true)
            {
                // ReSharper disable once PossibleNullReferenceException
                ((RadioButton)SaveSlotStackPanel.FindName($"SaveSlotRadioButton{SaveSlot}")).Content = DediBaseSetHouseName.Text;
            }
        }

        /// <summary>
        /// 选择游戏风格
        /// </summary>
        private void DediBaseSetIntentionButton_Click(object sender, RoutedEventArgs e)
        {
            DediButtomPanelVisibilityInitialize();
            DediIntention.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// 删除当前存档按钮
        /// </summary>
        private void DediMainTop_Delete_Click(object sender, RoutedEventArgs e)
        {
            // 0. 关闭服务器
            var processes = Process.GetProcesses();
            foreach (var item in processes)
            {
                if (item.ProcessName == "dontstarve_dedicated_server_nullrenderer")
                {
                    item.Kill();
                }
            }
            // 1. radioBox 写 创建世界
            // ReSharper disable once PossibleNullReferenceException
            ((RadioButton)SaveSlotStackPanel.FindName($"SaveSlotRadioButton{SaveSlot}")).Content = "创建世界";
            // 2. 删除当前存档
            if (Directory.Exists(_pathAll.ServerDirPath))
            {
                Directory.Delete(_pathAll.ServerDirPath, true);
            }
           // 2.1 取消选择,谁都不选
           // ReSharper disable once PossibleNullReferenceException
           ((RadioButton)SaveSlotStackPanel.FindName($"SaveSlotRadioButton{SaveSlot}")).IsChecked = false;
            // 2.2 
            // DediMainBorder.IsEnabled = false;
            JinYong(true);
            //// 3. 复制一份新的过来                 
            //ServerTools.Tool.CopyDirectory(pathAll.ServerMoBanPath, pathAll.DoNotStarveTogether_DirPath);
            //if (!Directory.Exists(pathAll.DoNotStarveTogether_DirPath + "\\Server_" + PathCommon.GamePlatform + "_" + SaveSlot))
            //{
            //    Directory.Move(pathAll.DoNotStarveTogether_DirPath + "\\Server", pathAll.DoNotStarveTogether_DirPath + "\\Server_" + PathCommon.GamePlatform + "_" + SaveSlot);
            //}
            //// 4. 读取新的存档
            //SetBaseSet();
        }

        /// <summary>
        /// 打开游戏
        /// </summary>
        private void OpenGameButton_Click(object sender, RoutedEventArgs e)
        {
            RunClient();
        }

        /// <summary>
        /// 创建世界按钮
        /// </summary>
        private void CtrateWorldButton_Click(object sender, RoutedEventArgs e)
        {
            RunServer();
        }

        /// <summary>
        /// 读取并设定基本设置
        /// </summary>
        private void SetBaseSet()
        {
            var clusterIniFilePath = _pathAll.ServerDirPath + @"\cluster.ini";
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
            IsCaveComboBox.DataContext = _baseSet;
            Debug.WriteLine("基本设置-完");
        }

        #endregion

        #region 编辑世界面板
        /// <summary>
        /// 是否开启洞穴的
        /// </summary>
        private void DediBaseIsCave_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = e.AddedItems[0].ToString();
            CaveSettingColumnDefinition.Width = selected == "否" ? new GridLength(0) : new GridLength(1, GridUnitType.Star);
        }

        /// <summary>
        /// 设置"地上世界"
        /// </summary>
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

            var overWorldFenLei = JsonHelper.ReadWorldFenLei(false);

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
                    switch (overWorldFenLei[item.Key])
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

            #region "显示" 地上
            //  
            foreach (var item in world)
            {
                if (item.Value.ToolTip == "roads" || item.Value.ToolTip == "layout_mode" || item.Value.ToolTip == "wormhole_prefab")
                {
                    continue;
                }

                var di = new DediComboBoxWithImage()
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

        /// <summary>
        /// 设置"地上世界"
        /// </summary>
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

        /// <summary>
        /// 设置"地下世界"
        /// </summary>
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

            var fenleil = JsonHelper.ReadWorldFenLei(true);

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

        /// <summary>
        /// 设置"地下世界"
        /// </summary>
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
        #endregion

        #region Mods面板

        /// <summary>
        /// 设置 "Mod集"
        /// </summary>
        private void SetModSet()
        {   // 设置
            if (!string.IsNullOrEmpty(PathCommon.ServerModsDirPath))
            {
                // 清空,Enabled变成默认值
                foreach (var item in _mods.ListMod)
                {
                    item.Enabled = false;
                }
                // 细节也要变成默认值,之后再重新读取1
                foreach (var item in _mods.ListMod)
                {
                    foreach (var item1 in item.ConfigurationOptions)
                    {
                        item1.Value.Current = item1.Value.Default1;
                    }
                }
                // 重新读取
                _mods.ReadModsOverrides(PathCommon.ServerModsDirPath, _pathAll.ServerDirPath + @"\Master\modoverrides.lua");
            }
            // 显示 
            ModListStackPanel.Children.Clear();
            ModSettingStackPanel.Children.Clear();
            ModDescriptionStackPanel.Text = "";
            if (_mods != null)
            {
                for (var i = 0; i < _mods.ListMod.Count; i++)
                {
                    // 屏蔽 客户端MOD
                    if (_mods.ListMod[i].ModType == ModType.客户端)
                    {
                        continue;
                    }
                    var dod = new DediModBox
                    {
                        Width = 200,
                        Height = 70,
                        UCTitle = { Content = _mods.ListMod[i].Name },
                        UCCheckBox = { Tag = i },
                        UCConfig =
                        {
                            Source = _mods.ListMod[i].ConfigurationOptions.Count != 0
                                ? new BitmapImage(new Uri(
                                    "/饥荒百科全书CSharp;component/Resources/DedicatedServer/D_mp_mod_config.png",
                                    UriKind.Relative))
                                : null
                        }
                    };
                    dod.UCCheckBox.IsChecked = _mods.ListMod[i].Enabled;
                    dod.UCCheckBox.Checked += CheckBox_Checked;
                    dod.UCCheckBox.Unchecked += CheckBox_Unchecked;
                    dod.PreviewMouseLeftButtonDown += Dod_MouseLeftButtonDown;
                    dod.UCEnableLabel.Content = _mods.ListMod[i].ModType;
                    ModListStackPanel.Children.Add(dod);
                }
            }
        }

        /// <summary>
        /// 设置 "Mod" "MouseLeftButtonDown"
        /// </summary>
        private void Dod_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // 左边显示
            var n = (int)((DediModBox)sender).UCCheckBox.Tag;
            var author = "作者:\r\n" + _mods.ListMod[n].Author + "\r\n\r\n";
            var description = "描述:\r\n" + _mods.ListMod[n].Description + "\r\n\r\n";
            var strName = "Mod名字:\r\n" + _mods.ListMod[n].Name + "\r\n\r\n";
            var version = "版本:\r\n" + _mods.ListMod[n].Version + "\r\n\r\n";
            var fileName = "文件夹:\r\n" + _mods.ListMod[n].DirName + "\r\n\r\n";
            ModDescriptionStackPanel.FontSize = 12;
            ModDescriptionStackPanel.TextWrapping = TextWrapping.WrapWithOverflow;
            ModDescriptionStackPanel.Text = strName + author + description + version + fileName;
            if (_mods.ListMod[n].ConfigurationOptions.Count == 0)
            {
                // 没有细节配置项
                Debug.WriteLine(n);
                ModSettingStackPanel.Children.Clear();
                var labelModXiJie = new Label
                {
                    Height = 300,
                    Width = 300,
                    Content = "QQ群: 580332268 \r\n mod类型:\r\n 所有人: 所有人都必须有.\r\n 服务器:只要服务器有就行",
                    FontWeight = FontWeights.Bold,
                    FontSize = 20
                };
                ModSettingStackPanel.Children.Add(labelModXiJie);
            }
            else
            {
                // 有,显示细节配置项
                Debug.WriteLine(n);
                ModSettingStackPanel.Children.Clear();
                foreach (var item in _mods.ListMod[n].ConfigurationOptions)
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
                    ModSettingStackPanel.Children.Add(stackPanel);
                }
            }
        }

        /// <summary>
        /// 设置 "Mod" "SelectionChanged"
        /// </summary>
        private void Dod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Debug.WriteLine(((DediComboBox)sender).Tag);
            var str = ((DediComboBox)sender).Tag.ToString().Split('$');
            if (str.Length != 0)
            {
                var n = int.Parse(str[0]);
                var name = str[1];
                // 好复杂
                _mods.ListMod[n].ConfigurationOptions[name].Current =
                    _mods.ListMod[n].ConfigurationOptions[name].Options[((DediComboBox)sender).SelectedIndex].Data;

            }
        }

        /// <summary>
        /// 设置 "Mod" "CheckBox_Unchecked"
        /// </summary>
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            _mods.ListMod[(int)(((CheckBox)sender).Tag)].Enabled = false;
            //Debug.WriteLine(((CheckBox)sender).Tag.ToString());
        }

        /// <summary>
        /// 设置 "Mod" "CheckBox_Checked"
        /// </summary>
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            _mods.ListMod[(int)((CheckBox)sender).Tag].Enabled = true;
            //Debug.WriteLine(((CheckBox)sender).Tag.ToString());
        }


        #endregion

        #region 控制台面板
        /// <summary>
        /// 根据分类生产RadioButton
        /// </summary>
        private void CreateConsoleClassificationButton()
        {
            ConsoleClassificationStackPanel.Children.Clear();
            // otherRadioButton
            var otherRadioButton = new RadioButton
            {
                Content = "其他",
                Width = 140,
                Height = 40,
                Foreground = new SolidColorBrush(Colors.Black),
                FontWeight = FontWeights.Bold,
                Style = (Style)FindResource("RadioButtonStyle")
            };
            otherRadioButton.Checked += ConsoleRadioButton_Click;
            otherRadioButton.IsChecked = true;
            ConsoleClassificationStackPanel.Children.Add(otherRadioButton);
            // foodRadioButton
            var foodRadioButton = new RadioButton
            {
                Content = "食物",
                Width = 140,
                Height = 40,
                Foreground = new SolidColorBrush(Colors.Black),
                FontWeight = FontWeights.Bold,
                Style = (Style)FindResource("RadioButtonStyle")
            };
            foodRadioButton.Checked += ConsoleRadioButton_Click;
            foodRadioButton.IsChecked = false;
            ConsoleClassificationStackPanel.Children.Add(foodRadioButton);
            // resourcesRadioButton
            var resourcesRadioButton = new RadioButton
            {
                Content = "资源",
                Width = 140,
                Height = 40,
                Foreground = new SolidColorBrush(Colors.Black),
                FontWeight = FontWeights.Bold,
                Style = (Style)FindResource("RadioButtonStyle")
            };
            resourcesRadioButton.Checked += ConsoleRadioButton_Click;
            resourcesRadioButton.IsChecked = false;
            ConsoleClassificationStackPanel.Children.Add(resourcesRadioButton);
            // toolsRadioButton
            var toolsRadioButton = new RadioButton
            {
                Content = "工具",
                Width = 140,
                Height = 40,
                Foreground = new SolidColorBrush(Colors.Black),
                FontWeight = FontWeights.Bold,
                Style = (Style)FindResource("RadioButtonStyle")
            };
            toolsRadioButton.Checked += ConsoleRadioButton_Click;
            toolsRadioButton.IsChecked = false;
            ConsoleClassificationStackPanel.Children.Add(toolsRadioButton);
            // weaponsRadioButton
            var weaponsRadioButton = new RadioButton
            {
                Content = "武器",
                Width = 140,
                Height = 40,
                Foreground = new SolidColorBrush(Colors.Black),
                FontWeight = FontWeights.Bold,
                Style = (Style)FindResource("RadioButtonStyle")
            };
            weaponsRadioButton.Checked += ConsoleRadioButton_Click;
            weaponsRadioButton.IsChecked = false;
            ConsoleClassificationStackPanel.Children.Add(weaponsRadioButton);
            // giftsRadioButton
            var giftsRadioButton = new RadioButton
            {
                Content = "礼物",
                Width = 140,
                Height = 40,
                Foreground = new SolidColorBrush(Colors.Black),
                FontWeight = FontWeights.Bold,
                Style = (Style)FindResource("RadioButtonStyle")
            };
            giftsRadioButton.Checked += ConsoleRadioButton_Click;
            giftsRadioButton.IsChecked = false;
            ConsoleClassificationStackPanel.Children.Add(giftsRadioButton);
            // clothesRadioButton
            var clothesRadioButton = new RadioButton
            {
                Content = "衣物",
                Width = 140,
                Height = 40,
                Foreground = new SolidColorBrush(Colors.Black),
                FontWeight = FontWeights.Bold,
                Style = (Style)FindResource("RadioButtonStyle")
            };
            clothesRadioButton.Checked += ConsoleRadioButton_Click;
            clothesRadioButton.IsChecked = false;
            ConsoleClassificationStackPanel.Children.Add(clothesRadioButton);
        }

        /// <summary>
        /// 显示具体分类信息
        /// </summary>
        private void ConsoleRadioButton_Click(object sender, RoutedEventArgs e)
        {
            ConsoleDetailsWrapPanel.Children.Clear();
            // 读取分类信息
            var itemList = JsonConvert.DeserializeObject<ItemListRootObject>(StringProcess.GetJsonStringDedicatedServer("ItemList.json"));
            // 把当前选择的值放到这里了
            ConsoleClassificationStackPanel.Tag = ((RadioButton)sender).Content;
            // 显示具体分类信息
            switch (ConsoleClassificationStackPanel.Tag)
            {
                case "其他":
                    foreach (var detail in itemList.Items.Other.Details)
                    {
                        if (string.IsNullOrEmpty(detail.Chinese))
                        {
                            continue;
                        }
                        CreateConsoleButton(detail);
                    }
                    break;
                case "食物":
                    foreach (var detail in itemList.Items.Food.Details)
                    {
                        if (string.IsNullOrEmpty(detail.Chinese))
                        {
                            continue;
                        }
                        CreateConsoleButton(detail);
                    }
                    break;
                case "资源":
                    foreach (var detail in itemList.Items.Resources.Details)
                    {
                        if (string.IsNullOrEmpty(detail.Chinese))
                        {
                            continue;
                        }
                        CreateConsoleButton(detail);
                    }
                    break;
                case "工具":
                    foreach (var detail in itemList.Items.Tools.Details)
                    {
                        if (string.IsNullOrEmpty(detail.Chinese))
                        {
                            continue;
                        }
                        CreateConsoleButton(detail);
                    }
                    break;
                case "武器":
                    foreach (var detail in itemList.Items.Weapons.Details)
                    {
                        if (string.IsNullOrEmpty(detail.Chinese))
                        {
                            continue;
                        }
                        CreateConsoleButton(detail);
                    }
                    break;
                case "礼物":
                    foreach (var detail in itemList.Items.Gifts.Details)
                    {
                        if (string.IsNullOrEmpty(detail.Chinese))
                        {
                            continue;
                        }
                        CreateConsoleButton(detail);
                    }
                    break;
                case "衣物":
                    foreach (var detail in itemList.Items.Clothes.Details)
                    {
                        if (string.IsNullOrEmpty(detail.Chinese))
                        {
                            continue;
                        }
                        CreateConsoleButton(detail);
                    }
                    break;
            }
        }

        /// <summary>
        /// 创建Console按钮
        /// </summary>
        /// <param name="detail"></param>
        private void CreateConsoleButton(Detail3 detail)
        {
            var codeString = detail.Code;
            var chineseString = detail.Chinese;
            // 按钮
            var button = new Button
            {
                Content = chineseString,
                Width = 115,
                Height = 35,
                Tag = codeString,
                FontWeight = FontWeights.Bold,
                Style = (Style)FindResource("DediButtonCreateWorldStyle")
            };
            button.Click += ConsoleButton_Click;
            ConsoleDetailsWrapPanel.Children.Add(button);
        }

        /// <summary>
        /// Console按钮Click事件
        /// </summary>
        private void ConsoleButton_Click(object sender, RoutedEventArgs e)
        {
            var code = ((Button)sender).Tag.ToString();
            // 如果是其他分类,则直接运行code
            if (ConsoleClassificationStackPanel.Tag.ToString() == "其他")
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

        /// <summary>
        /// 发送“消息”
        /// </summary>
        /// <param name="messageStr">消息字符串</param>
        private static void SsendMessage(string messageStr)
        {
            var mySendMessage = new MySendMessage();
            // 得到句柄
            var pstr = Process.GetProcessesByName("dontstarve_dedicated_server_nullrenderer");
            // 根据句柄,发送消息
            foreach (var t in pstr)
            {
                mySendMessage.InputStr(t.MainWindowHandle, messageStr);
                mySendMessage.SendEnter(t.MainWindowHandle);
            }
        }

        #endregion

        #region 其他方法
        /// <summary>
        /// 汉化(字符串)
        /// </summary>
        /// <param name="str">字符串str</param>
        /// <returns>汉化文本</returns>
        private string HanHua(string str)
        {
            return _hanhua.ContainsKey(str) ? _hanhua[str] : str;
        }

        /// <summary>
        /// 汉化(字符串公开枚举数)
        /// </summary>
        /// <param name="str">字符串str</param>
        /// <returns>汉化文本(List)</returns>
        private IEnumerable<string> HanHua(IEnumerable<string> str)
        {
            return str.Select(item => _hanhua.ContainsKey(item) ? _hanhua[item] : item).ToList();
        }

        /// <summary>
        /// 获取房间名
        /// </summary>
        /// <param name="saveSlot">存档槽</param>
        /// <returns>房间名</returns>
        private string GetHouseName(int saveSlot)
        {
            var clusterIniPath = PathCommon.SaveRootDirPath + @"\Server_" + PathCommon.GamePlatform + "_" + saveSlot + @"\cluster.ini";
            if (!File.Exists(clusterIniPath))
            {
                return "创建世界";
            }
            var iniTool = new IniHelper(clusterIniPath, _utf8WithoutBom);

            var houseName = iniTool.ReadValue("NETWORK", "cluster_name");
            return houseName;
        }

        /// <summary>
        /// 检查通用设置
        /// </summary>
        /// <param name="onCommonSettingPanel">是否已经在通用设置面板</param>
        private void CheckCommonSetting(bool onCommonSettingPanel = false)
        {
            // 读取通用设置
            SetPath();
            if (string.IsNullOrEmpty(PathCommon.ClientFilePath) || string.IsNullOrEmpty(PathCommon.ServerFilePath) || string.IsNullOrEmpty(PathCommon.ClusterToken))
            {
                if (onCommonSettingPanel == false)
                {
                    CommonSettingSetOverTextBlock.Text = "请先设定好通用设置！";
                    CommonSettingSetOverTextBlock.Foreground = new SolidColorBrush(Colors.Red);
                }
                JinYong(true);
                SaveSlotScrollViewer.IsEnabled = false;
                DediButtomPanelVisibility("Setting");
            }
            else
            {
                SaveSlotScrollViewer.IsEnabled = true;
                CommonSettingSetOverTextBlock.Foreground = new SolidColorBrush(Colors.Green);
                CommonSettingSetOverTextBlock.Text = "通用设置设定完毕，现在可以在左侧选择存档开启服务器";
            }
        }

        /// <summary>
        /// 禁用菜单
        /// </summary>
        /// <param name="isDisable">是否禁用</param>
        private void JinYong(bool isDisable)
        {
            // 基本设置
            TitleMenuBaseSet.IsEnabled = !isDisable;
            // 编辑世界
            TitleMenuEditWorld.IsEnabled = !isDisable;
            // Mod
            TitleMenuMod.IsEnabled = !isDisable;
            // 控制台
            TitleMenuRollback.IsEnabled = !isDisable;
            // 删除存档按钮
            DediMainTopDelete.IsEnabled = !isDisable;
            // 创建世界按钮
            CtrateWorldButton.IsEnabled = !isDisable;
        }
        #endregion
    }
}
