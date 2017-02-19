using ServerTools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml;
using WpfLearn.UserControls;
using 饥荒百科全书CSharp.Class;
using 饥荒百科全书CSharp.Class.DedicatedServerClass.DedicateServer;
using 饥荒百科全书CSharp.MyUserControl.DedicatedServer;

namespace 饥荒百科全书CSharp
{
    /// <summary>
    /// Server
    /// </summary>
    public partial class MainWindow : Window
    {
        #region 字段、属性
        int cunDangCao = 0; // 存档槽
        string gamePingTai; // 游戏平台
        private UTF8Encoding utf8NoBom = new UTF8Encoding(false); // 编码
        Dictionary<string, string> hanhua;  // 汉化

        PathAll pathAll; // 路径
        BaseSet baseSet; // 基本设置
        Leveldataoverride OverWorld; // 地上世界
        Leveldataoverride Caves;     // 地下世界
        Mods mods;  // mods

        public string GamePingTai
        {
            get
            {
                return gamePingTai;
            }
            set
            {
                XmlHelper.WriteGamePingTai(value);
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

        #region 设置
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
            hanhua = XmlHelper.ReadHanhua();

            // 3.读取服务器mods文件夹下所有信息.mod多的话,读取时间也多
            //   此时的mod没有被current覆盖
            mods = null;
            if (!string.IsNullOrEmpty(pathAll.ServerMods_DirPath))
            {
                mods = new Mods(pathAll.ServerMods_DirPath);
            }
            // 4. "控制台"
            CreateConsoleButton();
            // 5.clusterToken
            this.DediSettingClusterTokenTextBox.Text = Class.RegeditRW.RegReadString("cluster");
            // 3."基本设置" 等在 点击radioButton后设置

        }
        //点击radioButton 时
        private void DediRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            // 0.保存之前的
            if (OverWorld != null & Caves != null & mods != null & Directory.Exists(pathAll.YyServer_DirPath))
            {
                OverWorld.SaveWorld();
                Caves.SaveWorld();

                mods.saveListmodsToFile(pathAll.YyServer_DirPath + @"\Master\modoverrides.lua", utf8NoBom);
                mods.saveListmodsToFile(pathAll.YyServer_DirPath + @"\Caves\modoverrides.lua", utf8NoBom);
            }


            // 1.存档槽
            CunDangCao = (int)((RadioButton)sender).Tag;

            // 1.5 创建世界
            if (((RadioButton)sender).Content.ToString() == "创建世界")
            {
                // 复制一份过去                  
                //Tool.CopyDirectory(pathAll.ServerMoBanPath, pathAll.DoNotStarveTogether_DirPath);
                CopyServerModel(pathAll.DoNotStarveTogether_DirPath);
                // 改名字
                if (!Directory.Exists(pathAll.DoNotStarveTogether_DirPath + "\\Server_" + GamePingTai + "_" + CunDangCao))
                {
                    Directory.Move(pathAll.DoNotStarveTogether_DirPath + "\\Server", pathAll.DoNotStarveTogether_DirPath + "\\Server_" + GamePingTai + "_" + CunDangCao);

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
            if (!string.IsNullOrEmpty(pathAll.ServerMods_DirPath) )
            {
                // 清空,Enabled变成默认值
                foreach (Mod item in mods.ListMod)
                {
                    item.Enabled = false;
                }
                // 细节也要变成默认值,之后再重新读取1
                foreach (Mod item in mods.ListMod)
                {
                    foreach (KeyValuePair<string,ModXiJie> item1 in item.Configuration_options)
                    {
                         item1.Value.Current = item1.Value.Default1;
                    } 
                
                }

                // 重新读取
                mods.ReadModsOverrides(pathAll.ServerMods_DirPath, pathAll.YyServer_DirPath + @"\Master\modoverrides.lua");
            }
            // 显示 
            DediModList.Children.Clear();
            DediModXiJie.Children.Clear();
            DediModDescription.Text = "";
            if (mods != null)
            {
                for (int i = 0; i < mods.ListMod.Count; i++)
                {
                    // 屏蔽 客户端MOD
                    if (mods.ListMod[i].Tyype == ModType.客户端)
                    {
                        continue;
                    }
                    DediModBox dod = new DediModBox()
                    {
                        Width = 200,
                        Height = 70
                    };
                    dod.UCTitle.Content = mods.ListMod[i].Name;
                    dod.UCCheckBox.Tag = i;
                    if (mods.ListMod[i].Configuration_options.Count != 0)
                    {
                        dod.UCConfig.Source = new BitmapImage(new Uri("/饥荒百科全书CSharp;component/Resources/DedicatedServer/D_mp_mod_config.png", UriKind.Relative));
                    }
                    else
                    {
                        dod.UCConfig.Source = null;
                    }
                    if (mods.ListMod[i].Enabled == false)
                    {
                        dod.UCCheckBox.IsChecked = false;

                    }
                    else
                    {
                        dod.UCCheckBox.IsChecked = true;
                    }
                    dod.UCCheckBox.Checked += CheckBox_Checked;
                    dod.UCCheckBox.Unchecked += CheckBox_Unchecked;
                    dod.PreviewMouseLeftButtonDown += Dod_MouseLeftButtonDown;
                    dod.UCEnableLabel.Content = mods.ListMod[i].Tyype;


                    DediModList.Children.Add(dod);
                }


            }

        }
        // 设置 "Mod" "MouseLeftButtonDown"
        private void Dod_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // 左边显示
            int n = (int)(((DediModBox)sender).UCCheckBox.Tag);
            string author = "作者:\r\n" + mods.ListMod[n].Author + "\r\n\r\n";
            string description = "描述:\r\n" + mods.ListMod[n].Description + "\r\n\r\n";
            string strName = "Mod名字:\r\n" + mods.ListMod[n].Name + "\r\n\r\n";
            string version = "版本:\r\n" + mods.ListMod[n].Version + "\r\n\r\n";
            string fileName = "文件夹:\r\n" + mods.ListMod[n].DirName + "\r\n\r\n";
            DediModDescription.FontSize = 12;
            DediModDescription.TextWrapping = TextWrapping.WrapWithOverflow;
            DediModDescription.Text = strName + author + description + version + fileName;


            if (mods.ListMod[n].Configuration_options.Count == 0)
            {
                // 没有细节配置项
                Debug.WriteLine(n);
                DediModXiJie.Children.Clear();

                Label labelModXiJie = new Label()
                {
                    Height = 300,
                    Width = 300,
                    Content = "QQ群: 580332268 \r\n mod类型:\r\n 所有人: 所有人都必须有.\r\n 服务器:只要服务器有就行",
                    FontWeight = FontWeights.Bold,
                    FontSize = (double)20
                };
                DediModXiJie.Children.Add(labelModXiJie);
            }
            else
            {
                // 有,显示细节配置项
                Debug.WriteLine(n);
                DediModXiJie.Children.Clear();
                foreach (KeyValuePair<string, ModXiJie> item in mods.ListMod[n].Configuration_options)
                {
                    // stackPanel
                    StackPanel stackPanel = new StackPanel();
                    stackPanel.Height = 40;
                    stackPanel.Width = 330;
                    stackPanel.Orientation = Orientation.Horizontal;
                    Label labelModXiJie = new Label();
                    labelModXiJie.Height = stackPanel.Height;
                    labelModXiJie.Width = 180;
                    labelModXiJie.FontWeight = FontWeights.Bold;
                    labelModXiJie.Content = string.IsNullOrEmpty(item.Value.Label) ? item.Value.Name : item.Value.Label;

                    // dediComboBox
                    DediComboBox dod = new DediComboBox();
                    dod.Height = stackPanel.Height;
                    dod.Width = 150;
                    dod.FontSize = 12;
                    dod.Tag = n.ToString() + "$" + item.Key; // 把当前选择mod的第n个,放到tag里
                    foreach (Option item1 in item.Value.Options)
                    {
                        dod.Items.Add(item1.description);
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
                mods.ListMod[n].Configuration_options[name].Current =
                    mods.ListMod[n].Configuration_options[name].Options[((DediComboBox)sender).SelectedIndex].data;

            }

        }
        // 设置 "Mod" "CheckBox_Unchecked"
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
        
                mods.ListMod[(int)(((CheckBox)sender).Tag)].Enabled = false;
     
            //Debug.WriteLine(((CheckBox)sender).Tag.ToString());
          
        }
        // 设置 "Mod" "CheckBox_Checked"
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
           
                mods.ListMod[(int)(((CheckBox)sender).Tag)].Enabled = true;
   
            //Debug.WriteLine(((CheckBox)sender).Tag.ToString());
          
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
            gamePingTai = XmlHelper.ReadGamePingTai();
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
            DediBaseIsCave.DataContext = baseSet;
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

            Dictionary<string, string> OverWorld_FenLei = XmlHelper.ReadWorldFenLei(false);

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
            if (e.RemovedItems.Count != 0 && e.AddedItems[0].ToString() == HanHua(OverWorld.ShowWorldDic[Dedi.Tag.ToString()].WorldconfigList[Dedi.SelectedIndex]))
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

            Dictionary<string, string> fenleil = XmlHelper.ReadWorldFenLei(true);

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

            if (!Directory.Exists(pathAll.DoNotStarveTogether_DirPath))
            {
                Directory.CreateDirectory(pathAll.DoNotStarveTogether_DirPath);
            }
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
                //Tool.CopyDirectory(pathAll.ServerMoBanPath, pathAll.DoNotStarveTogether_DirPath);
                CopyServerModel(pathAll.DoNotStarveTogether_DirPath);

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

            if (string.IsNullOrEmpty(pathAll.ClientMods_DirPath))
            {
                MessageBox.Show("客户端路径没有设置");
                return;
            }
            Process p = new Process();
            p.StartInfo.Arguments = "";
            p.StartInfo.WorkingDirectory = Path.GetDirectoryName(pathAll.Client_FilePath); // 目录,这个必须设置
            p.StartInfo.FileName = pathAll.Client_FilePath;
            p.Start();
        }
        // 打开"服务器"
        private void RunServer()
        {

            if (pathAll.Server_FilePath == null || pathAll.Server_FilePath.Trim() == "")
            {
                MessageBox.Show("服务器路径不对,请重新设置服务器路径"); return;
            }

            // 保存世界
            if (OverWorld != null && Caves != null && mods != null)
            {
                OverWorld.SaveWorld();
                Caves.SaveWorld();
                mods.saveListmodsToFile(pathAll.YyServer_DirPath + @"\Master\modoverrides.lua", utf8NoBom);
                mods.saveListmodsToFile(pathAll.YyServer_DirPath + @"\Caves\modoverrides.lua", utf8NoBom);
            }
            // 如果是youxia,强行设置为 离线,局域网
            if (GamePingTai == "youxia")
            {
                INIhelper ini1 = new INIhelper(pathAll.YyServer_DirPath + @"\cluster.ini", utf8NoBom);
                ini1.write("NETWORK", "offline_cluster", "true", utf8NoBom);
                ini1.write("NETWORK", "lan_only_cluster", "true", utf8NoBom);

            }
            if (GamePingTai == "TGP")
            {
                INIhelper ini1 = new INIhelper(pathAll.YyServer_DirPath + @"\cluster.ini", utf8NoBom);
                //ini1.write("NETWORK", "offline_cluster", "false", utf8NoBom);
                ini1.write("NETWORK", "lan_only_cluster", "false", utf8NoBom);
            }
            if (GamePingTai == "Steam")
            {
                INIhelper ini1 = new INIhelper(pathAll.YyServer_DirPath + @"\cluster.ini", utf8NoBom);
                //ini1.write("NETWORK", "offline_cluster", "false", utf8NoBom);
                ini1.write("NETWORK", "lan_only_cluster", "false", utf8NoBom);
            }

            // 打开服务器
            Process p = new Process();
            if (GamePingTai != "TGP")
            {

                p.StartInfo.UseShellExecute = false; // 是否
                p.StartInfo.WorkingDirectory = Path.GetDirectoryName(pathAll.Server_FilePath); // 目录,这个必须设置
                p.StartInfo.FileName = pathAll.Server_FilePath; ;  // 服务器名字

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
        private void ssendMessage(string messageStr)
        {
            mySendMessage mySendMessage = new mySendMessage();

            // 得到句柄
            Process[] pstr = Process.GetProcessesByName("dontstarve_dedicated_server_nullrenderer");

            // 根据句柄,发送消息
            for (int i = 0; i < pstr.Length; i++)
            {
                mySendMessage.InputStr(pstr[i].MainWindowHandle, messageStr);
                mySendMessage.sendEnter(pstr[i].MainWindowHandle);
            }
        }
 
        /// <summary>
        /// 根据分类生产RadioButton
        /// </summary>
        private void CreateConsoleButton()
        {
            DediConsoleFenLei.Children.Clear();
            // 读取分类信息
            System.Reflection.Assembly _assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.IO.Stream sStream = _assembly.GetManifestResourceStream("饥荒百科全书CSharp.XML.ItemList.xml");
            System.Xml.XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(sStream);


            XmlNodeList details = xmldoc.SelectNodes("/items/*");

            foreach (XmlNode item in details)
            {

                string chinese = ((XmlElement)item).GetAttribute("chinese");
                // RadioButton
                RadioButton b = new RadioButton()
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
                if (b.Content.ToString()=="其他")
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
            DediConsoleFenLei.Tag=((RadioButton)sender).Content;
            XmlElement item = (XmlElement)((RadioButton)sender).Tag;
            DediConsoleDetails.Children.Clear();
            // 显示具体分类信息
            XmlNodeList xnl = item.ChildNodes;
            foreach (XmlNode x in xnl)
            {
                string codestr = ((XmlElement)x).GetAttribute("code");
                string chinesestr = ((XmlElement)x).GetAttribute("chinese");
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
            string code = ((Button)sender).Tag.ToString();
            // 如果是其他分类,则直接运行code
            if (DediConsoleFenLei.Tag.ToString() == "其他")
            {
                ssendMessage(code);
                System.Windows.Forms.Clipboard.SetDataObject(code);
            }
            // 如果不是其他
            else
            {
                ssendMessage("c_give(\""+ code+"\", 1)");
                System.Windows.Forms.Clipboard.SetDataObject("c_give(\"" + code + "\", 1)");
            }

            



        }
        #endregion

        #region 其他
        // 获取房间名
        private string GetHouseName(int d_cundangcao)
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
        private void Jinyong(bool b)
        {
            // DediMainBorder.IsEnabled = b;
            DediTitleBaseSet.IsEnabled = b;
            DediTitleEditWorld.IsEnabled = b;
            DediTitleMod.IsEnabled = b;
            DediTitleRollback.IsEnabled = b;
         
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
            File.WriteAllText(path + @"\Server\cluster.ini", Tool.ReadResources("Server模板.cluster.ini"), utf8NoBom);
            File.WriteAllText(path + @"\Server\Caves\leveldataoverride.lua", Tool.ReadResources("Server模板.Caves.leveldataoverride.lua"), utf8NoBom);
            File.WriteAllText(path + @"\Server\Caves\modoverrides.lua", Tool.ReadResources("Server模板.Caves.modoverrides.lua"), utf8NoBom);
            File.WriteAllText(path + @"\Server\Caves\server.ini", Tool.ReadResources("Server模板.Caves.server.ini"), utf8NoBom);
            File.WriteAllText(path + @"\Server\Master\leveldataoverride.lua", Tool.ReadResources("Server模板.Master.leveldataoverride.lua"), utf8NoBom);
            File.WriteAllText(path + @"\Server\Master\modoverrides.lua", Tool.ReadResources("Server模板.Master.modoverrides.lua"), utf8NoBom);
            File.WriteAllText(path + @"\Server\Master\server.ini", Tool.ReadResources("Server模板.Master.server.ini"), utf8NoBom);

            // clusterToken
            bool flag2 = !string.IsNullOrEmpty(RegeditRW.RegReadString("cluster"));
            if (flag2)
            {
                File.WriteAllText(path + "\\Server\\cluster_token.txt", RegeditRW.RegReadString("cluster"), this.utf8NoBom);
            }
            else
            {
                File.WriteAllText(path + "\\Server\\cluster_token.txt", "", this.utf8NoBom);
            }


        }


        #endregion
    }

    public enum Message
    {
        保存,
        复活,
        回档,
        重置世界
    }


}
