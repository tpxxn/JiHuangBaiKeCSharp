using ServerTools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace 饥荒百科全书CSharp.Class.DedicatedServerClass.DedicateServer
{
    /// <summary>
    /// 2016.12.1  2016.12.8 写 基本设置类
    /// 
    /// 添加字段和属性后，记得在构造函数附一个初始值
    /// </summary>
    class BaseSet : INotifyPropertyChanged
    {
        #region 字段和属性

        private UTF8Encoding utf8NoBom = new UTF8Encoding(false);

        private bool isFileToProperty = false;
        private List<string> gameStyle;
        private string gameStyleText;
        private string houseName;
        private string describe;
        private List<string> gameMode;
        private string gameModeText;
        private int maxPlayers;
        private string secret;
        private string isCave;
        private string isConsole;
        private string isPause;
        private string isPVP;
        private string serverMode;
        private string clusterIni_FilePath;

        /// <summary>
        /// 房间名称
        /// </summary>
        public string HouseName
        {
            get
            {

                return houseName;

            }

            set
            {

                houseName = value;
                if (!isFileToProperty) { SavePropertyToFile("HouseName"); };
                //NotifyPropertyChange("HouseName");
            }
        }

        /// <summary>
        /// 描述
        /// </summary>
        public string Describe
        {
            get
            {
                return describe;
            }

            set
            {
                describe = value;
                //NotifyPropertyChange("Describe");
                if (!isFileToProperty) { SavePropertyToFile("Describe"); };
            }
        }


        /// <summary>
        /// 人数限制
        /// </summary>
        public int MaxPlayers
        {
            get
            {
                return maxPlayers;
            }

            set
            {
                maxPlayers = value;
                //NotifyPropertyChange("LimitNumOfPeople");
                if (!isFileToProperty) { SavePropertyToFile("MaxPlayers"); };
            }
        }

        /// <summary>
        /// 密码
        /// </summary>
        public string Secret
        {
            get
            {
                return secret;
            }

            set
            {
                secret = value;
                //NotifyPropertyChange("Secret");
                if (!isFileToProperty) { SavePropertyToFile("Secret"); };
            }
        }

        /// <summary>
        /// 是否开启洞穴
        /// </summary>
        public string IsCave
        {
            get
            {
                return isCave;
            }

            set
            {
                isCave = value;
                //NotifyPropertyChange("IsCave");
                if (!isFileToProperty) { SavePropertyToFile("IsCave"); };
            }
        }

        /// <summary>
        /// 是否开启控制台
        /// </summary>
        public string IsConsole
        {
            get
            {
                return isConsole;
            }

            set
            {
                isConsole = value;
                //NotifyPropertyChange("IsConsole");
                if (!isFileToProperty) { SavePropertyToFile("IsConsole"); };
            }
        }

        /// <summary>
        /// 是否无人时暂停
        /// </summary>
        public string IsPause
        {
            get
            {
                return isPause;
            }

            set
            {
                isPause = value;
                //NotifyPropertyChange("IsPause");
                if (!isFileToProperty) { SavePropertyToFile("IsPause"); };
            }
        }

        /// <summary>
        /// 是否开启PVP
        /// </summary>
        public string IsPVP
        {
            get
            {
                return isPVP;

            }

            set
            {
                isPVP = value;
                //NotifyPropertyChange("IsPVP");
                if (!isFileToProperty) { SavePropertyToFile("IsPVP"); };
            }
        }

        /// <summary>
        /// 【当前显示的】游戏风格
        /// </summary>
        public string GameStyleText
        {
            get
            {
                return gameStyleText;
            }

            set
            {
                gameStyleText = value;
                //NotifyPropertyChange("GameStyleText");
                if (!isFileToProperty) { SavePropertyToFile("GameStyleText"); };
            }
        }

        /// <summary>
        /// 【当前显示的】游戏模式
        /// </summary>
        public string GameModeText
        {
            get
            {
                return gameModeText;
            }

            set
            {
                gameModeText = value;
                //NotifyPropertyChange("GameModeText");
                if (!isFileToProperty) { SavePropertyToFile("GameModeText"); };
            }
        }

        /// <summary>
        /// 游戏模式
        /// </summary>
        public List<string> GameMode
        {
            get
            {
                return gameMode;
            }
            set
            {

                gameMode = value;
                //NotifyPropertyChange("GameMode");
            }


        }

        /// <summary>
        /// 游戏风格
        /// </summary>
        public List<string> GameStyle
        {
            get
            {
                return gameStyle;
            }
            set
            {

                gameStyle = value;
                //NotifyPropertyChange("GameStyle");
            }


        }

        public string ServerMode
        {
            get
            {
                return serverMode;
            }

            set
            {
                serverMode = value;
                if (!isFileToProperty) { SavePropertyToFile("ServerMode"); };
            }
        }

        #endregion

        #region 构造函数 

        /// <summary>
        /// clusterIni的地址，目前地址只能通过构造函数传入，以后有需要再改
        /// </summary>
        /// <param name="clusterIni_FilePath"></param>
        public BaseSet(string clusterIni_FilePath)
        {

            // 游戏风格【字段赋值】
            gameStyle = new List<string>();
            gameStyle.Add("合作");
            gameStyle.Add("交际");
            gameStyle.Add("竞争");
            gameStyle.Add("疯狂");
            gameStyleText = "合作";
            //NotifyPropertyChange("GameStyle");

            // 游戏模式【字段赋值】
            gameMode = new List<string>();
            gameMode.Add("生存");
            gameMode.Add("无尽");
            gameMode.Add("荒野");
            gameModeText = "无尽";
            //NotifyPropertyChange("GameMode");


            // 其他先全部赋值，防止为空
            //houseName = "qq群：351765204";
            //describe = "qq群：351765204";
            //maxPlayers = 6;
            //secret = "333333";
            ////isCave = "否";
            //isConsole = "是";
            //isPause = "是";
            //isPVP = "否";


            if (File.Exists(clusterIni_FilePath))
            {
                this.clusterIni_FilePath = clusterIni_FilePath;
                // 从文件读，给字段赋值
                FileToProperty(clusterIni_FilePath);
            }
            else
            {

                Debug.WriteLine("cluster.ini文件不存在");
            }

        }
        #endregion

        #region 方法
        /// <summary>
        /// 从文件读，给字段赋值
        /// </summary>
        private void FileToProperty(string clusterIniPath)
        {

            // 改变记号（这个记号可能以后保存的时候会有用）
            isFileToProperty = true;


            // 标记！：这里没有判断文件是否存在，在外面判断了，以后再看用不用修改

            //读取基本设置
            INIhelper iniTool = new INIhelper(clusterIniPath, utf8NoBom);


            //读取游戏风格
            string yx_fengge = iniTool.ReadValue("NETWORK", "cluster_intention");
            if (yx_fengge == "cooperative") { GameStyleText = "合作"; };
            if (yx_fengge == "social") { GameStyleText = "交际"; };
            if (yx_fengge == "competitive") { GameStyleText = "竞争"; };
            if (yx_fengge == "madness") { GameStyleText = "疯狂"; };

            //读取房间名称
            string fj_name = iniTool.ReadValue("NETWORK", "cluster_name");
            HouseName = fj_name;
            //读取描述
            string fj_miaoshu = iniTool.ReadValue("NETWORK", "cluster_description");
            Describe = fj_miaoshu;
            //读取游戏模式
            string yx_moshi = iniTool.ReadValue("GAMEPLAY", "game_mode");
            if (yx_moshi == "endless") { GameModeText = "无尽"; };
            if (yx_moshi == "survival") { GameModeText = "生存"; };
            if (yx_moshi == "wilderness") { GameModeText = "荒野"; };
            //读取人数限制
            string yx_renshu = iniTool.ReadValue("GAMEPLAY", "max_players");
            MaxPlayers = int.Parse(yx_renshu);
            //读取密码
            string yx_mima = iniTool.ReadValue("NETWORK", "cluster_password");
            Secret = yx_mima;


            //读取是否启用控制台[标记：这里没有变成小写]
            string yx_kongzhitai = iniTool.ReadValue("MISC", "console_enabled");
            if (yx_kongzhitai == "true") { IsConsole = "是"; };
            if (yx_kongzhitai == "false") { IsConsole = "否"; };

            //读取无人时暂停[标记：这里没有变成小写]
            string yx_zhanting = iniTool.ReadValue("GAMEPLAY", "pause_when_empty");
            if (yx_zhanting == "true") { IsPause = "是"; };
            if (yx_zhanting == "false") { IsPause = "否"; };
            //读取PVP[标记：这里没有变成小写]
            string yx_pvp = iniTool.ReadValue("GAMEPLAY", "pvp");
            if (yx_pvp == "true") { IsPVP = "是"; };
            if (yx_pvp == "false") { IsPVP = "否"; };

            // 读取是否开启洞穴[标记：这里没有变成小写]
            string yx_cave = iniTool.ReadValue("SHARD", "shard_enabled");
            if (yx_cave == "true") { IsCave = "是"; };
            if (yx_cave == "false") { IsCave = "否"; };

            // 读取服务器模式 offline_cluster=true
            string yx_serverMode = iniTool.ReadValue("NETWORK", "offline_cluster");
            if (yx_serverMode == "true") { ServerMode = "离线"; };
            if (yx_serverMode == "false") { ServerMode = "在线"; };

            isFileToProperty = false;



        }

        #endregion

        #region 保存
        public void SavePropertyToFile(string propertyName)
        {

            // 保存
            if (isFileToProperty == false)
            {
                INIhelper ini1 = new INIhelper(clusterIni_FilePath, utf8NoBom);

                if (propertyName == "HouseName")
                {
                    ini1.write("NETWORK", "cluster_name", HouseName, utf8NoBom);
                }

                if (propertyName == "Describe")
                {
                    ini1.write("NETWORK", "cluster_description", Describe, utf8NoBom);
                }
                if (propertyName == "MaxPlayers")
                {
                    ini1.write("GAMEPLAY", "max_players", MaxPlayers.ToString(), utf8NoBom);
                }
                if (propertyName == "Secret")
                {
                    ini1.write("NETWORK", "cluster_password", Secret, utf8NoBom);
                }
                if (propertyName == "IsCave")
                {
                    ini1.write("SHARD", "shard_enabled", IsCave == "是" ? "true" : "false", utf8NoBom);
                }
                if (propertyName == "IsConsole")
                {
                    ini1.write("MISC", "console_enabled", IsConsole == "是" ? "true" : "false", utf8NoBom);
                }
                if (propertyName == "IsPause")
                {
                    ini1.write("GAMEPLAY", "pause_when_empty", IsPause == "是" ? "true" : "false", utf8NoBom);
                }
                if (propertyName == "IsPVP")
                {
                    ini1.write("GAMEPLAY", "pvp", IsPVP == "是" ? "true" : "false", utf8NoBom);
                }
                if (propertyName == "GameStyleText")
                {
                    if (GameStyleText == "合作") { ini1.write("NETWORK", "cluster_intention", "cooperative", utf8NoBom); };
                    if (GameStyleText == "交际") { ini1.write("NETWORK", "cluster_intention", "social", utf8NoBom); };
                    if (GameStyleText == "竞争") { ini1.write("NETWORK", "cluster_intention", "competitive", utf8NoBom); };
                    if (GameStyleText == "疯狂") { ini1.write("NETWORK", "cluster_intention", "madness", utf8NoBom); };
                }
                if (propertyName == "GameModeText")
                {
                    if (GameModeText == "无尽") { ini1.write("GAMEPLAY", "game_mode", "endless", utf8NoBom); };
                    if (GameModeText == "生存") { ini1.write("GAMEPLAY", "game_mode", "survival", utf8NoBom); };
                    if (GameModeText == "荒野") { ini1.write("GAMEPLAY", "game_mode", "wilderness", utf8NoBom); };
                }
                if (propertyName == "ServerMode")
                {
                    if (ServerMode == "离线") { ini1.write("NETWORK", "offline_cluster", "true", utf8NoBom); };
                    if (ServerMode == "在线") { ini1.write("NETWORK", "offline_cluster", "false", utf8NoBom); };

                }
            }

        }
        #endregion


        #region  标记：【接口】,还是第一次用接口

        public event PropertyChangedEventHandler PropertyChanged;
        //PropertyChangedEventArgs类型，这个类用于传递更改值的属性的名称，实现向客户端已经更改的属性发送更改通知。属性的名称为字符串类型。 
        private void NotifyPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                //根据PropertyChanged事件的委托类，实现PropertyChanged事件： 
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

            }
        }

        #endregion












    }
}
