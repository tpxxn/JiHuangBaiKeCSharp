using ServerTools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using WpfLearn.UserControls;
using 饥荒百科全书CSharp.Class;
using 饥荒百科全书CSharp.Class.DedicatedServerClass.DedicateServer;

using 饥荒百科全书CSharp.MyUserControl;

namespace 饥荒百科全书CSharp
{
    /// <summary>
    /// Server
    /// </summary>
    public partial class MainWindow : Window
    {
        #region "字段,属性"
        int cunDangCao = 0; // 存档槽
        string gamePingTai;
        private UTF8Encoding utf8NoBom = new UTF8Encoding(false);
        Dictionary<string, string> hanhua;

        PathAll pathAll;
        BaseSet baseSet;
        Leveldataoverride OverWorld;
        Leveldataoverride Caves;
        Mods mods;

        public string GamePingTai
        {
            get
            {
                return gamePingTai;
            }

            set
            {
                XmlHelper.WriteGamePingTai("ServerConfig.xml", value);
                gamePingTai = value;


            }
        }

        public int CunDangCao
        {
            get
            {
                return cunDangCao;
            }

            set
            {
                cunDangCao = value;
                pathAll.CunDangCao = value;

            }
        }
        #endregion

        #region 各种设置
        // 初始化
        public void InitServer()
        {
            //-1.游戏平台
            SetPingTai();

            // 0.路径信息
            SetPath();

            // 1.检查存档Server是否存在 
            CheckServer();

            // 2.汉化
            hanhua = XmlHelper.ReadHanhua("ServerConfig.xml");

            // 3.读取服务器mods文件夹下所有信息.mod多的话,读取时间也多
            //   此时的mod没有被current覆盖
            mods = null;
            if (!string.IsNullOrEmpty(pathAll.ServerMods_DirPath))
            {
                mods = new Mods(pathAll.ServerMods_DirPath);
            }
          

            // 3."基本设置" 等在 点击radioButton后设置

        }
        //点击radioButton 时
        private void DediRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            // 0.保存之前的
            if (OverWorld!=null && Caves!=null)
            {
                OverWorld.SaveWorld();
                Caves.SaveWorld();
            }
 

            // 1.存档槽
            CunDangCao = (int)((RadioButton)sender).Tag;

            // 1.5 创建世界
            if (((RadioButton)sender).Content.ToString() == "创建世界")
            {
                // 复制一份过去                  
                Tool.CopyDirectory(pathAll.ServerMoBanPath, pathAll.DoNotStarveTogether_DirPath);

                // 改名字
                if (!Directory.Exists(pathAll.DoNotStarveTogether_DirPath + "\\Server_" + GamePingTai + "_" + CunDangCao))
                {
                    Directory.Move(pathAll.DoNotStarveTogether_DirPath + "\\Server", pathAll.DoNotStarveTogether_DirPath + "\\Server_" + GamePingTai + "_" + CunDangCao);

                }
               ((RadioButton)sender).Content = getHouseName(CunDangCao);

            }
            // 1.6 复活
            jinyong(true);

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
            if (!string.IsNullOrEmpty(pathAll.ServerMods_DirPath))
            {
                mods.ReadModsOverrides(pathAll.ServerMods_DirPath, pathAll.YyServer_DirPath + @"\Master\modoverrides.lua");
            }
            // 显示
            DediModList.Children.Clear();
            if (mods!=null)
            { 
                for (int i = 0; i < mods.ListMod.Count; i++)
                {
                    DediMod dod = new DediMod();
                    dod.Width = 200;
                    dod.Height = 80;
                    dod.Title.Content = mods.ListMod[i].Name;
                    dod.checkBox.Tag = i;
                    if (mods.ListMod[i].Configuration_options.Count!=0)
                    {
                        dod.config.Source = new BitmapImage(new Uri("/饥荒百科全书CSharp;component/Resources/DedicatedServer/设置1_Leave.png", UriKind.Relative));
                    }
                    else
                    {
                        dod.config.Source = null;
                    }
                    if (mods.ListMod[i].Enabled==false)
                    {
                        dod.checkBox.IsChecked= false;
                        
                    }
                    else
                    {
                        dod.checkBox.IsChecked = true;  
                    }
                    dod.checkBox.Checked += CheckBox_Checked;
                    dod.checkBox.Unchecked += CheckBox_Unchecked;
                    dod.MouseLeftButtonDown += Dod_MouseLeftButtonDown;
                    dod.EnableLabel.Content = mods.ListMod[i].Tyype;


                    DediModList.Children.Add(dod);
                }
     

            }

        }
        // 设置 "Mod" "MouseLeftButtonDown"
        private void Dod_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            int n = (int)(((DediMod)sender).checkBox.Tag);
           if( mods.ListMod[n].Configuration_options.Count==0)
            {
                // 没有细节配置项
                Debug.WriteLine(n);
            }
            else
            {
                // 有,显示细节配置项
                Debug.WriteLine(n);

            }
        }
        // 设置 "Mod" "CheckBox_Unchecked"
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            //Debug.WriteLine(((CheckBox)sender).Tag.ToString());
            mods.ListMod[(int)(((CheckBox)sender).Tag)].Enabled = false;
        }
        // 设置 "Mod" "CheckBox_Checked"
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            //Debug.WriteLine(((CheckBox)sender).Tag.ToString());
            mods.ListMod[(int)(((CheckBox)sender).Tag)].Enabled = true;
        }

        // 设置"路径"
        private void SetPath()
        {
            pathAll = new PathAll(GamePingTai, 0);
            DediSettingGameDirSelectTextBox.Text = "";
            if (!String.IsNullOrEmpty(pathAll.Client_FilePath) && File.Exists(pathAll.Client_FilePath))
            {
                DediSettingGameDirSelectTextBox.Text = pathAll.Client_FilePath;
            }
            else
            {
                pathAll.Client_FilePath = "";

            }
            DediSettingDediDirSelectTextBox.Text = "";
            if (!String.IsNullOrEmpty(pathAll.Server_FilePath) && File.Exists(pathAll.Server_FilePath))
            {
                DediSettingDediDirSelectTextBox.Text = pathAll.Server_FilePath;
            }
            else
            {
                pathAll.Server_FilePath = "";

            }

            Debug.WriteLine("路径读取-完");
        }
        // 设置"平台"
        private void SetPingTai()
        {
            gamePingTai = XmlHelper.ReadGamePingTai("ServerConfig.xml");
            DediSettingGameVersionSelect.Text = gamePingTai;
            Debug.WriteLine("游戏平台-完");
        }
        // 设置"基本"
        private void SetBaseSet()
        {
            string clusterIni_FilePath = pathAll.YyServer_DirPath + @"\cluster.ini";
            if (!File.Exists(clusterIni_FilePath))
            {
                //MessageBox.Show("cluster.ini不存在");
                return;
            }
            baseSet = new BaseSet(clusterIni_FilePath);

            DediBaseSetGamemodeSelect.DataContext = baseSet;
            DediBaseSetPvpSelect.DataContext = baseSet;
            DediBaseSetMaxPlayerSelect.DataContext = baseSet;
            DediBaseOfflineSelect.DataContext = baseSet;
            DediBaseSetHouseName.DataContext = baseSet;
            DediBaseSetDescribe.DataContext = baseSet;
            DediBaseSetSecret.DataContext = baseSet;
            DediBaseOfflineSelect.DataContext = baseSet;
            DediBaseIsPause.DataContext = baseSet;
            DediBaseSetIntentionButton.DataContext = baseSet;

            Debug.WriteLine("基本设置-完");
        }
        // 设置"地上世界"
        private void SetOverWorldSet()
        {
            // 地上 
            OverWorld = new Leveldataoverride(pathAll, false);
            DediOverWorldWorld.Children.Clear();
            DediOverWolrdFoods.Children.Clear();
            DediOverWorldAnimals.Children.Clear();
            DediOverWorldMonsters.Children.Clear();
            DediOverWorldResources.Children.Clear();
            // 地上 分类

            Dictionary<string, string> OverWorld_FenLei = XmlHelper.ReadWorldFenLei("ServerConfig.xml", false);

            Dictionary<string, ShowWorld> foods = new Dictionary<string, ShowWorld>();
            Dictionary<string, ShowWorld> animals = new Dictionary<string, ShowWorld>();
            Dictionary<string, ShowWorld> monsters = new Dictionary<string, ShowWorld>();
            Dictionary<string, ShowWorld> resources = new Dictionary<string, ShowWorld>();
            Dictionary<string, ShowWorld> world = new Dictionary<string, ShowWorld>();


            #region 地上分类方法
            foreach (KeyValuePair<string, ShowWorld> item in OverWorld.ShowWorldDic)
            {
                if (OverWorld_FenLei.ContainsKey(item.Key))
                {


                    if (OverWorld_FenLei[item.Key] == "foods")
                    {
                        foods[item.Key] = item.Value;
                    }
                    if (OverWorld_FenLei[item.Key] == "animals")
                    {
                        animals[item.Key] = item.Value;
                    }
                    if (OverWorld_FenLei[item.Key] == "monsters")
                    {
                        monsters[item.Key] = item.Value;
                    }
                    if (OverWorld_FenLei[item.Key] == "resources")
                    {
                        resources[item.Key] = item.Value;
                    }
                    if (OverWorld_FenLei[item.Key] == "world")
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
            foreach (KeyValuePair<string, ShowWorld> item in world)
            {
                if (item.Value.ToolTip == "roads" || item.Value.ToolTip == "layout_mode" || item.Value.ToolTip == "wormhole_prefab")
                {
                    continue;
                }

                DediComboBoxWithImage di = new DediComboBoxWithImage();
                di.ImageSource = new BitmapImage(new Uri("/" + item.Value.PicPath, UriKind.Relative));
                di.ItemsSource = HanHua(item.Value.WorldconfigList);
                di.SelectedValue = HanHua(item.Value.Worldconfig);
                di.ImageToolTip = HanHua(item.Value.ToolTip);
                di.Tag = item.Key;
                di.Width = 200;
                di.Height = 60;
                di.SelectionChanged += DiOverWorld_SelectionChanged;
                DediOverWorldWorld.Children.Add(di);

            }

            foreach (KeyValuePair<string, ShowWorld> item in foods)
            {
                DediComboBoxWithImage di = new DediComboBoxWithImage();
                di.ImageSource = new BitmapImage(new Uri("/" + item.Value.PicPath, UriKind.Relative));
                di.ItemsSource = HanHua(item.Value.WorldconfigList);
                di.SelectedValue = HanHua(item.Value.Worldconfig);
                di.ImageToolTip = HanHua(item.Value.ToolTip);
                di.Tag = item.Key;
                di.Width = 200;
                di.Height = 60;
                di.SelectionChanged += DiOverWorld_SelectionChanged;
                DediOverWolrdFoods.Children.Add(di);

            }

            foreach (KeyValuePair<string, ShowWorld> item in animals)
            {
                DediComboBoxWithImage di = new DediComboBoxWithImage();
                di.ImageSource = new BitmapImage(new Uri("/" + item.Value.PicPath, UriKind.Relative));
                di.ItemsSource = HanHua(item.Value.WorldconfigList);
                di.SelectedValue = HanHua(item.Value.Worldconfig);
                di.ImageToolTip = HanHua(item.Value.ToolTip);
                di.Tag = item.Key;
                di.Width = 200;
                di.Height = 60;
                di.SelectionChanged += DiOverWorld_SelectionChanged;
                DediOverWorldAnimals.Children.Add(di);

            }

            foreach (KeyValuePair<string, ShowWorld> item in monsters)
            {
                DediComboBoxWithImage di = new DediComboBoxWithImage();
                di.ImageSource = new BitmapImage(new Uri("/" + item.Value.PicPath, UriKind.Relative));
                di.ItemsSource = HanHua(item.Value.WorldconfigList);
                di.SelectedValue = HanHua(item.Value.Worldconfig);
                di.ImageToolTip = HanHua(item.Value.ToolTip);
                di.Tag = item.Key;
                di.Width = 200;
                di.Height = 60;
                di.SelectionChanged += DiOverWorld_SelectionChanged;
                DediOverWorldMonsters.Children.Add(di);

            }

            foreach (KeyValuePair<string, ShowWorld> item in resources)
            {
                DediComboBoxWithImage di = new DediComboBoxWithImage();
                di.ImageSource = new BitmapImage(new Uri("/" + item.Value.PicPath, UriKind.Relative));
                di.ItemsSource = HanHua(item.Value.WorldconfigList);
                di.SelectedValue = HanHua(item.Value.Worldconfig);
                di.ImageToolTip = HanHua(item.Value.ToolTip);
                di.Tag = item.Key;
                di.Width = 200;
                di.Height = 60;
                di.SelectionChanged += DiOverWorld_SelectionChanged;
                DediOverWorldResources.Children.Add(di);

            }

            #endregion

        }
        // 设置"地上世界"
        private void DiOverWorld_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //// 测试 用
            DediComboBoxWithImage Dedi = (DediComboBoxWithImage)sender;
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
            if (e.RemovedItems.Count!=0 && e.AddedItems[0].ToString() == HanHua(OverWorld.ShowWorldDic[Dedi.Tag.ToString()].WorldconfigList[Dedi.SelectedIndex]))
            {
                OverWorld.ShowWorldDic[Dedi.Tag.ToString()].Worldconfig = OverWorld.ShowWorldDic[Dedi.Tag.ToString()].WorldconfigList[Dedi.SelectedIndex];
                Debug.WriteLine(Dedi.Tag.ToString() + "选项变为:" + OverWorld.ShowWorldDic[Dedi.Tag.ToString()].Worldconfig);
                
                // 保存,这样保存有点卡,换为每次点击radioButton或创建世界时
                //OverWorld.SaveWorld();
                //Debug.WriteLine("保存地上世界");
            }
       
  

        }

        // 设置"地下世界"
        private void SetCavesSet()
        {
            // 地下
            Caves = new Leveldataoverride(pathAll, true);
            DediCavesWorld.Children.Clear();
            DediCavesFoods.Children.Clear();
            DediCavesAnimals.Children.Clear();
            DediCavesMonsters.Children.Clear();
            DediCavesResources.Children.Clear();
            // 地下 分类

            Dictionary<string, string> fenleil = XmlHelper.ReadWorldFenLei("ServerConfig.xml", true);

            Dictionary<string, ShowWorld> foods = new Dictionary<string, ShowWorld>();
            Dictionary<string, ShowWorld> animals = new Dictionary<string, ShowWorld>();
            Dictionary<string, ShowWorld> monsters = new Dictionary<string, ShowWorld>();
            Dictionary<string, ShowWorld> resources = new Dictionary<string, ShowWorld>();
            Dictionary<string, ShowWorld> world = new Dictionary<string, ShowWorld>();


            #region  地下分类方法
            foreach (KeyValuePair<string, ShowWorld> item in Caves.ShowWorldDic)
            {
                if (fenleil.ContainsKey(item.Key))
                {


                    if (fenleil[item.Key] == "foods")
                    {
                        foods[item.Key] = item.Value;
                    }
                    if (fenleil[item.Key] == "animals")
                    {
                        animals[item.Key] = item.Value;
                    }
                    if (fenleil[item.Key] == "monsters")
                    {
                        monsters[item.Key] = item.Value;
                    }
                    if (fenleil[item.Key] == "resources")
                    {
                        resources[item.Key] = item.Value;
                    }
                    if (fenleil[item.Key] == "world")
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

            #region "显示" 地下
            // animals
            foreach (KeyValuePair<string, ShowWorld> item in world)
            {
                if (item.Value.ToolTip == "roads" || item.Value.ToolTip == "layout_mode" || item.Value.ToolTip == "wormhole_prefab")
                {
                    continue;
                }

                DediComboBoxWithImage di = new DediComboBoxWithImage();
                di.ImageSource = new BitmapImage(new Uri("/" + item.Value.PicPath, UriKind.Relative));
                di.ItemsSource = HanHua(item.Value.WorldconfigList);
                di.SelectedValue = HanHua(item.Value.Worldconfig);
                di.ImageToolTip = HanHua(item.Value.ToolTip);
                di.Tag = item.Key;
                di.Width = 200;
                di.Height = 60;
                di.SelectionChanged += DiCaves_SelectionChanged;
                DediCavesWorld.Children.Add(di);

            }

            foreach (KeyValuePair<string, ShowWorld> item in foods)
            {
                DediComboBoxWithImage di = new DediComboBoxWithImage();
                di.ImageSource = new BitmapImage(new Uri("/" + item.Value.PicPath, UriKind.Relative));
                di.ItemsSource = HanHua(item.Value.WorldconfigList);
                di.SelectedValue = HanHua(item.Value.Worldconfig);
                di.ImageToolTip = HanHua(item.Value.ToolTip);
                di.Tag = item.Key;
                di.Width = 200;
                di.Height = 60;
                di.SelectionChanged += DiCaves_SelectionChanged;
                DediCavesFoods.Children.Add(di);

            }

            foreach (KeyValuePair<string, ShowWorld> item in animals)
            {
                DediComboBoxWithImage di = new DediComboBoxWithImage();
                di.ImageSource = new BitmapImage(new Uri("/" + item.Value.PicPath, UriKind.Relative));
                di.ItemsSource = HanHua(item.Value.WorldconfigList);
                di.SelectedValue = HanHua(item.Value.Worldconfig);
                di.ImageToolTip = HanHua(item.Value.ToolTip);
                di.Tag = item.Key;
                di.Width = 200;
                di.Height = 60;
                di.SelectionChanged += DiCaves_SelectionChanged;
                DediCavesAnimals.Children.Add(di);

            }

            foreach (KeyValuePair<string, ShowWorld> item in monsters)
            {
                DediComboBoxWithImage di = new DediComboBoxWithImage();
                di.ImageSource = new BitmapImage(new Uri("/" + item.Value.PicPath, UriKind.Relative));
                di.ItemsSource = HanHua(item.Value.WorldconfigList);
                di.SelectedValue = HanHua(item.Value.Worldconfig);
                di.ImageToolTip = HanHua(item.Value.ToolTip);
                di.Tag = item.Key;
                di.Width = 200;
                di.Height = 60;
                di.SelectionChanged += DiCaves_SelectionChanged;
                DediCavesMonsters.Children.Add(di);

            }

            foreach (KeyValuePair<string, ShowWorld> item in resources)
            {
                DediComboBoxWithImage di = new DediComboBoxWithImage();
                di.ImageSource = new BitmapImage(new Uri("/" + item.Value.PicPath, UriKind.Relative));
                di.ItemsSource = HanHua(item.Value.WorldconfigList);
                di.SelectedValue = HanHua(item.Value.Worldconfig);
                di.ImageToolTip = HanHua(item.Value.ToolTip);
                di.Tag = item.Key;
                di.Width = 200;
                di.Height = 60;
                di.SelectionChanged += DiCaves_SelectionChanged;
                DediCavesResources.Children.Add(di);

            }

            #endregion
     

        }
        // 设置"地下世界"
        private void DiCaves_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //// 测试 用
            DediComboBoxWithImage Dedi = (DediComboBoxWithImage)sender;
        

            // 此时说明修改
            if (e.RemovedItems.Count != 0 && e.AddedItems[0].ToString() == HanHua(Caves.ShowWorldDic[Dedi.Tag.ToString()].WorldconfigList[Dedi.SelectedIndex]))
            {
                Caves.ShowWorldDic[Dedi.Tag.ToString()].Worldconfig = Caves.ShowWorldDic[Dedi.Tag.ToString()].WorldconfigList[Dedi.SelectedIndex];
                Debug.WriteLine(Dedi.Tag.ToString() + "选项变为:" + Caves.ShowWorldDic[Dedi.Tag.ToString()].Worldconfig);

                // 保存,这样保存有点卡,换为每次点击radioButton或创建世界时
                //Caves.SaveWorld();
                //Debug.WriteLine("保存地上世界");
            }

        }

        // "检查"
        private void CheckServer()
        {


            DirectoryInfo dinfo = new DirectoryInfo(pathAll.DoNotStarveTogether_DirPath);
            DirectoryInfo[] dinfostr = dinfo.GetDirectories();

            List<String> ServerTGPPathList = new List<string>();
            for (int i = 0; i < dinfostr.Length; i++)
            {
                if (dinfostr[i].Name.StartsWith("Server_" + GamePingTai + "_"))
                {
                    ServerTGPPathList.Add(dinfostr[i].FullName);
                }
            }

            // 清空左边
            for (int i = 0; i < 20; i++)
            {
                ((RadioButton)DediLeftStackPanel.FindName("DediRadioButton" + i.ToString())).Content = "创建世界";
                ((RadioButton)DediLeftStackPanel.FindName("DediRadioButton" + i.ToString())).Tag = i;
                ((RadioButton)DediLeftStackPanel.FindName("DediRadioButton" + i.ToString())).Checked += DediRadioButton_Checked;

            }


            // 等于0
            if (ServerTGPPathList.Count == 0)
            {
                // 复制一份过去                  
                Tool.CopyDirectory(pathAll.ServerMoBanPath, pathAll.DoNotStarveTogether_DirPath);

                // 改名字
                if (!Directory.Exists(pathAll.DoNotStarveTogether_DirPath + "\\Server_" + GamePingTai + "_0"))
                {
                    Directory.Move(pathAll.DoNotStarveTogether_DirPath + "\\Server", pathAll.DoNotStarveTogether_DirPath + "\\Server_" + GamePingTai + "_0");
                }
            }
            else
            {
                for (int i = 0; i < ServerTGPPathList.Count; i++)
                {
                    // 取出序号 
                    string Num = ServerTGPPathList[i].Substring(ServerTGPPathList[i].LastIndexOf('_') + 1);


                    // 取出存档名称
                    ((RadioButton)DediLeftStackPanel.FindName("DediRadioButton" + Num)).Content = getHouseName(int.Parse(Num));


                }

            }

            // 禁用
            jinyong(false);
            DediSettingGameVersionSelect.IsEnabled = true;
            // 不选择任何一项
            ((RadioButton)DediLeftStackPanel.FindName("DediRadioButton" + CunDangCao)).IsChecked = false;

            //// 选择第0个存档
            //((RadioButton)DediLeftStackPanel.FindName("DediRadioButton0")).IsChecked = true;
            //CunDangCao = 0;

        }
        #endregion

        #region 其他
        // 获取房间名
        private string getHouseName(int d_cundangcao)
        {
            string clusterIniPath = pathAll.DoNotStarveTogether_DirPath + @"\Server_" + GamePingTai + "_" + d_cundangcao.ToString() + @"\cluster.ini";
            if (!File.Exists(clusterIniPath))
            {
                return "创建世界";
            }
            INIhelper iniTool = new INIhelper(clusterIniPath, utf8NoBom);

            string houseName = iniTool.ReadValue("NETWORK", "cluster_name");
            return houseName;
        }
        // 禁用
        private void jinyong(bool b)
        {

            // DediMainBorder.IsEnabled = b;
            DediTitleBaseSet.IsEnabled = b;
            DediTitleEditWorld.IsEnabled = b;
            DediTitleMod.IsEnabled = b;
            DediTitleRollback.IsEnabled = b;
            DediTitleBlacklist.IsEnabled = b;
            DediMainTop_Delete.IsEnabled = b;
            DediCtrateWorldButton.IsEnabled = b;
            DediBaseSet.IsEnabled = b;


        }
        // 汉化
        private string HanHua(string s)
        {

            if (hanhua.ContainsKey(s))
            {
                return hanhua[s];
            }
            else
            {
                return s;
            }
        }
        private List<string> HanHua(List<string> s)
        {

            List<string> r = new List<string>();
            foreach (string item in s)
            {
                if (hanhua.ContainsKey(item))
                {
                    r.Add(hanhua[item]);
                }
                else
                {
                    r.Add(item);
                }
            }
            return r;

        }

        #endregion

    }
}
