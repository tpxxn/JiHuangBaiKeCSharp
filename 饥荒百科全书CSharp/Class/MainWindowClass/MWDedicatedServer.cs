using ServerTools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

        public string GamePingTai
        {
            get
            {
                return gamePingTai;
            }

            set
            {
                XmlHelper.WriteGamePingTai("ServerConfig.xml",value);
                gamePingTai = value;
              
            }
        }
        #endregion

        public void InitServer()
        {
            //-1.游戏平台
            SetPingTai();

            // 0.路径信息
            SetPath();

            // 1.检查存档Server是否存在，不存在从模板中复制一份过去, 存在则遍历存档列表,用于显示
            CheckServer();

            // 2.【基本设置】
            SetBaseSet();
        
        }

        private void CheckServer()
        {
            return;
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
                XmlHelper.WriteClientPath("ServerConfig.xml", "", GamePingTai);
            }
            DediSettingDediDirSelectTextBox.Text = "";
            if (!String.IsNullOrEmpty(pathAll.Server_FilePath) && File.Exists(pathAll.Server_FilePath))
            {
                DediSettingDediDirSelectTextBox.Text = pathAll.Server_FilePath;
            }
            else
            {
                pathAll.Server_FilePath = "";
                XmlHelper.WriteServerPath("ServerConfig.xml", "", GamePingTai);
            }

            Debug.WriteLine("路径读取-完");
        }

        private void SetPingTai()
        {
            gamePingTai = ReadGamePingTai();
            DediSettingGameVersionSelect.Text = gamePingTai;
            Debug.WriteLine("游戏平台-完");
        }

        private string ReadGamePingTai()
        {
            return XmlHelper.ReadGamePingTai("ServerConfig.xml");
        }

        private void SetBaseSet()
        {
            string clusterIni_FilePath = @"C:\Users\yy\Documents\Klei\DoNotStarveTogether\yyServer\cluster.ini";
            if (!File.Exists(clusterIni_FilePath))
            {
                MessageBox.Show("文件不存在,请在MWDedicatedServer.cs 第31行设置正确路径.以便测试基本设置");
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
