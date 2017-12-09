using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using 饥荒百科全书CSharp.Class.DedicatedServers.Tools;


namespace 饥荒百科全书CSharp.Class.DedicatedServers.DedicateServer
{
    /// <summary>
    /// 2016.12.1  2016.12.8 写 基本设置类
    /// 
    /// 添加字段和属性后，记得在构造函数附一个初始值
    /// </summary>
    class BaseSet : INotifyPropertyChanged
    {
        #region 字段和属性

        private readonly UTF8Encoding _utf8WithoutBom = new UTF8Encoding(false);

        private bool _isFileToProperty;
        private readonly string _clusterIniFilePath;

        /// <summary>
        /// 房间名称
        /// </summary>
        private string _houseName;

        public string HouseName
        {
            get => _houseName;
            set
            {
                _houseName = value;
                if (!_isFileToProperty) { SavePropertyToFile("HouseName"); }
                //NotifyPropertyChange("HouseName");
            }
        }

        /// <summary>
        /// 描述
        /// </summary>
        private string _describe;

        public string Describe
        {
            get => _describe;
            set
            {
                _describe = value;
                //NotifyPropertyChange("Describe");
                if (!_isFileToProperty) { SavePropertyToFile("Describe"); }
            }
        }

        /// <summary>
        /// 游戏风格
        /// </summary>
        private List<string> _gameStyle;

        public List<string> GameStyle
        {
            get => _gameStyle;
            set
            {
                _gameStyle = value;
                //NotifyPropertyChange("GameStyle");
            }
        }

        private string _serverMode;

        public string ServerMode
        {
            get => _serverMode;
            set
            {
                _serverMode = value;
                if (!_isFileToProperty) { SavePropertyToFile("ServerMode"); }
            }
        }

        /// <summary>
        /// 【当前显示的】游戏风格
        /// </summary>
        private string _gameStyleText;

        public string GameStyleText
        {
            get => _gameStyleText;
            set
            {
                _gameStyleText = value;
                //NotifyPropertyChange("GameStyleText");
                if (!_isFileToProperty) { SavePropertyToFile("GameStyleText"); }
            }
        }

        /// <summary>
        /// 是否开启PVP
        /// </summary>
        private string _isPvp;

        public string IsPvp
        {
            get => _isPvp;
            set
            {
                _isPvp = value;
                //NotifyPropertyChange("IsPVP");
                if (!_isFileToProperty) { SavePropertyToFile("IsPVP"); }
            }
        }

        /// <summary>
        /// 人数限制
        /// </summary>
        private int _maxPlayers;

        public int MaxPlayers
        {
            get => _maxPlayers;
            set
            {
                _maxPlayers = value;
                //NotifyPropertyChange("LimitNumOfPeople");
                if (!_isFileToProperty) { SavePropertyToFile("MaxPlayers"); }
            }
        }

        /// <summary>
        /// 密码
        /// </summary>
        private string _secret;

        public string Secret
        {
            get => _secret;
            set
            {
                _secret = value;
                //NotifyPropertyChange("Secret");
                if (!_isFileToProperty) { SavePropertyToFile("Secret"); }
            }
        }

        /// <summary>
        /// 【当前显示的】游戏模式
        /// </summary>
        private string _gameModeText;

        public string GameModeText
        {
            get => _gameModeText;
            set
            {
                _gameModeText = value;
                //NotifyPropertyChange("GameModeText");
                if (!_isFileToProperty) { SavePropertyToFile("GameModeText"); }
            }
        }

        /// <summary>
        /// 游戏模式
        /// </summary>
        private List<string> _gameMode;

        public List<string> GameMode
        {
            get => _gameMode;
            set
            {
                _gameMode = value;
                //NotifyPropertyChange("GameMode");
            }
        }

        /// <summary>
        /// 是否无人时暂停
        /// </summary>
        private string _isPause;

        public string IsPause
        {
            get => _isPause;
            set
            {
                _isPause = value;
                //NotifyPropertyChange("IsPause");
                if (!_isFileToProperty) { SavePropertyToFile("IsPause"); }
            }
        }

        /// <summary>
        /// 是否开启洞穴
        /// </summary>
        private string _isCave;

        public string IsCave
        {
            get => _isCave;
            set
            {
                _isCave = value;
                //NotifyPropertyChange("IsCave");
                if (!_isFileToProperty) { SavePropertyToFile("IsCave"); }
            }
        }

        /// <summary>
        /// 是否开启控制台
        /// </summary>
        private string _isConsole;

        public string IsConsole
        {
            get => _isConsole;
            set
            {
                _isConsole = value;
                //NotifyPropertyChange("IsConsole");
                if (!_isFileToProperty) { SavePropertyToFile("IsConsole"); }
            }
        }

        #endregion

        #region 构造函数 

        /// <summary>
        /// clusterIni的地址，目前地址只能通过构造函数传入，以后有需要再改
        /// </summary>
        /// <param name="clusterIniFilePath"></param>
        public BaseSet(string clusterIniFilePath)
        {

            // 游戏风格【字段赋值】
            _gameStyle = new List<string> { "合作", "交际", "竞争", "疯狂" };
            _gameStyleText = "合作";
            //NotifyPropertyChange("GameStyle");

            // 游戏模式【字段赋值】
            _gameMode = new List<string> { "生存", "无尽", "荒野" };
            _gameModeText = "无尽";
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

            if (File.Exists(clusterIniFilePath))
            {
                this._clusterIniFilePath = clusterIniFilePath;
                // 从文件读，给字段赋值
                FileToProperty(clusterIniFilePath);
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
            _isFileToProperty = true;

            // 标记！：这里没有判断文件是否存在，在外面判断了，以后再看用不用修改

            //读取基本设置
            var iniTool = new IniHelper(clusterIniPath, _utf8WithoutBom);

            //读取游戏风格
            var yxFengge = iniTool.ReadValue("NETWORK", "cluster_intention");
            if (yxFengge == "cooperative") { GameStyleText = "合作"; };
            if (yxFengge == "social") { GameStyleText = "交际"; };
            if (yxFengge == "competitive") { GameStyleText = "竞争"; };
            if (yxFengge == "madness") { GameStyleText = "疯狂"; };

            //读取房间名称
            var fjName = iniTool.ReadValue("NETWORK", "cluster_name");
            HouseName = fjName;

            //读取描述
            var fjMiaoshu = iniTool.ReadValue("NETWORK", "cluster_description");
            Describe = fjMiaoshu;

            //读取游戏模式
            var yxMoshi = iniTool.ReadValue("GAMEPLAY", "game_mode");
            if (yxMoshi == "endless") { GameModeText = "无尽"; };
            if (yxMoshi == "survival") { GameModeText = "生存"; };
            if (yxMoshi == "wilderness") { GameModeText = "荒野"; };

            //读取PVP[标记：这里没有变成小写]
            var yxPvp = iniTool.ReadValue("GAMEPLAY", "pvp");
            if (yxPvp == "true") { IsPvp = "是"; };
            if (yxPvp == "false") { IsPvp = "否"; };

            //读取人数限制
            var yxRenshu = iniTool.ReadValue("GAMEPLAY", "max_players");
            MaxPlayers = int.Parse(yxRenshu);

            //读取密码
            var yxMima = iniTool.ReadValue("NETWORK", "cluster_password");
            Secret = yxMima;

            // 读取服务器模式 offline_cluster=true
            var yxServerMode = iniTool.ReadValue("NETWORK", "offline_cluster");
            if (yxServerMode == "true") { ServerMode = "离线"; };
            if (yxServerMode == "false") { ServerMode = "在线"; };

            //读取无人时暂停[标记：这里没有变成小写]
            var yxZhanting = iniTool.ReadValue("GAMEPLAY", "pause_when_empty");
            if (yxZhanting == "true") { IsPause = "是"; };
            if (yxZhanting == "false") { IsPause = "否"; };

            // 读取是否开启洞穴[标记：这里没有变成小写]
            var yxCave = iniTool.ReadValue("SHARD", "shard_enabled");
            if (yxCave == "true") { IsCave = "是"; };
            if (yxCave == "false") { IsCave = "否"; };

            //读取是否启用控制台[标记：这里没有变成小写]
            var yxKongzhitai = iniTool.ReadValue("MISC", "console_enabled");
            if (yxKongzhitai == "true") { IsConsole = "是"; };
            if (yxKongzhitai == "false") { IsConsole = "否"; };

            _isFileToProperty = false;
        }

        #endregion

        #region 保存
        public void SavePropertyToFile(string propertyName)
        {
            // 保存
            if (_isFileToProperty == false)
            {
                var ini1 = new IniHelper(_clusterIniFilePath, _utf8WithoutBom);

                switch (propertyName)
                {
                    case "GameStyleText":
                        if (GameStyleText == "合作") { ini1.Write("NETWORK", "cluster_intention", "cooperative", _utf8WithoutBom); };
                        if (GameStyleText == "交际") { ini1.Write("NETWORK", "cluster_intention", "social", _utf8WithoutBom); };
                        if (GameStyleText == "竞争") { ini1.Write("NETWORK", "cluster_intention", "competitive", _utf8WithoutBom); };
                        if (GameStyleText == "疯狂") { ini1.Write("NETWORK", "cluster_intention", "madness", _utf8WithoutBom); };
                        break;
                    case "HouseName":
                        ini1.Write("NETWORK", "cluster_name", HouseName, _utf8WithoutBom);
                        break;
                    case "Describe":
                        ini1.Write("NETWORK", "cluster_description", Describe, _utf8WithoutBom);
                        break;
                    case "GameModeText":
                        if (GameModeText == "无尽") { ini1.Write("GAMEPLAY", "game_mode", "endless", _utf8WithoutBom); };
                        if (GameModeText == "生存") { ini1.Write("GAMEPLAY", "game_mode", "survival", _utf8WithoutBom); };
                        if (GameModeText == "荒野") { ini1.Write("GAMEPLAY", "game_mode", "wilderness", _utf8WithoutBom); };
                        break;
                    case "IsPVP":
                        ini1.Write("GAMEPLAY", "pvp", IsPvp == "是" ? "true" : "false", _utf8WithoutBom);
                        break;
                    case "MaxPlayers":
                        ini1.Write("GAMEPLAY", "max_players", MaxPlayers.ToString(), _utf8WithoutBom);
                        break;
                    case "Secret":
                        ini1.Write("NETWORK", "cluster_password", Secret, _utf8WithoutBom);
                        break;
                    case "ServerMode":
                        if (ServerMode == "离线") { ini1.Write("NETWORK", "offline_cluster", "true", _utf8WithoutBom); };
                        if (ServerMode == "在线") { ini1.Write("NETWORK", "offline_cluster", "false", _utf8WithoutBom); };
                        break;
                    case "IsPause":
                        ini1.Write("GAMEPLAY", "pause_when_empty", IsPause == "是" ? "true" : "false", _utf8WithoutBom);
                        break;
                    case "IsCave":
                        ini1.Write("SHARD", "shard_enabled", IsCave == "是" ? "true" : "false", _utf8WithoutBom);
                        break;
                    case "IsConsole":
                        ini1.Write("MISC", "console_enabled", IsConsole == "是" ? "true" : "false", _utf8WithoutBom);
                        break;
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
