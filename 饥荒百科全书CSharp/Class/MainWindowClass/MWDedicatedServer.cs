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
using System.Windows.Threading;
using 饥荒百科全书CSharp.Class;
using 饥荒百科全书CSharp.Class.DedicatedServerClass.DedicateServer;
using 饥荒百科全书CSharp.Class.DedicatedServerClass.Tools;
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
        PathAll pathAll;
        private UTF8Encoding utf8NoBom = new UTF8Encoding(false);

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

        // 游戏平台改变时,或 最开始初始化时
        public void InitServer()
        {
            //-1.游戏平台
            SetPingTai();

            // 0.路径信息
            SetPath();

            // 1.检查存档Server是否存在 
            CheckServer();

            // 2.【基本设置】
            SetBaseSet();

        }

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
            ((RadioButton)DediLeftStackPanel.FindName("DediRadioButton"+CunDangCao)).IsChecked = false;

            //// 选择第0个存档
            //((RadioButton)DediLeftStackPanel.FindName("DediRadioButton0")).IsChecked = true;
            //CunDangCao = 0;

        }

        private string getHouseName(int d_cundangcao)
        {
            string clusterIniPath = pathAll.DoNotStarveTogether_DirPath+@"\Server_"+GamePingTai+"_"+ d_cundangcao.ToString() + @"\cluster.ini";
            if (!File.Exists(clusterIniPath))
            {
                return "创建世界";
            }
            INIhelper iniTool = new INIhelper(clusterIniPath, utf8NoBom);

            string houseName = iniTool.ReadValue("NETWORK", "cluster_name");
            return houseName;
        }

        //点击radioButton 时
        private void DediRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            // 1.存档槽
            CunDangCao = (int)((RadioButton)sender).Tag;

            // 1.5 创建世界
             if(((RadioButton)sender).Content.ToString() == "创建世界")
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
        }


        private void jinyong(bool b) {

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

        private void SetPingTai()
        {
            gamePingTai = XmlHelper.ReadGamePingTai("ServerConfig.xml");
            DediSettingGameVersionSelect.Text = gamePingTai;
            Debug.WriteLine("游戏平台-完");
        }

        private void SetBaseSet()
        {
            string clusterIni_FilePath = pathAll.YyServer_DirPath + @"\cluster.ini";
            if (!File.Exists(clusterIni_FilePath))
            {
                //MessageBox.Show("cluster.ini不存在");
                return;
            }
            BaseSet baseSet = new BaseSet(clusterIni_FilePath);

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
    }
}
