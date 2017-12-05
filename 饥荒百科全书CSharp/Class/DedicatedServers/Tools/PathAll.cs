using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 饥荒百科全书CSharp.Class.DedicatedServers.Tools;

namespace 饥荒百科全书CSharp.Class.DedicatedServers.Tools

{
    class PathAll
    {
        /// <summary>
        /// DoNotStarveTogether
        /// </summary>
        private string _doNotStarveTogetherDirPath;

        /// <summary>
        /// 我的文档路径
        /// </summary>
        public string DocumentDirPath { get; set; }

        /// <summary>
        /// 当前路径
        /// </summary>
        public string CurrentDirPath { get; set; }

        /// <summary>
        /// 客户端exe文件路径
        /// </summary>
        private string _clientFilePath;

        /// <summary>
        /// 客户端exe文件路径
        /// </summary>
        public string ClientFilePath
        {
            get => _clientFilePath;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _clientFilePath = null;
                    ClientModsDirPath = null;
                    XmlHelper.WriteClientPath("", GamePingTai);
                    return;
                }
                _clientFilePath = value.Trim();
                XmlHelper.WriteClientPath(_clientFilePath, GamePingTai);
                // 客户端mods路径 
                if (!string.IsNullOrEmpty(_clientFilePath))
                {

                    ClientModsDirPath = Path.GetDirectoryName(Path.GetDirectoryName(_clientFilePath)) + @"\mods";


                }
            }
        }

        /// <summary>
        /// 服务端exe文件路径
        /// </summary>
        private string _serverFilePath;

        /// <summary>
        /// 服务器exe文件路径
        /// </summary>
        public string ServerFilePath
        {
            get => _serverFilePath;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _serverFilePath = null;
                    ServerModsDirPath = null;
                    XmlHelper.WriteServerPath("", GamePingTai);

                    return;
                }
                // 判断文件名对不对
                if (value.Contains("dontstarve_dedicated_server_nullrenderer.exe"))
                {
                    _serverFilePath = value.Trim();
                    XmlHelper.WriteServerPath(_serverFilePath, GamePingTai);


                }
                // 服务端mods路径 
                if (!string.IsNullOrEmpty(_serverFilePath))
                {

                    ServerModsDirPath = Path.GetDirectoryName(Path.GetDirectoryName(_serverFilePath)) + @"\mods";

                }
            }
        }

        /// <summary>
        /// yyServer路径
        /// </summary>
        private string _yyServerDirPath;

        /// <summary>
        /// yyServer路径
        /// </summary>
        public string YyServerDirPath
        {
            get => _yyServerDirPath;
            set
            {
                _yyServerDirPath = value;
                if (!string.IsNullOrEmpty(_yyServerDirPath))
                {
                    ModConfigOverworldFilePath = _yyServerDirPath + "\\Master\\modoverrides.lua";
                    ModConfigCaveFilePath = _yyServerDirPath + "\\Caves\\modoverrides.lua";
                    OverworldConfigFilePath = _yyServerDirPath + "\\Master\\leveldataoverride.lua";
                    CaveConfigFilePath = _yyServerDirPath + "\\Caves\\leveldataoverride.lua";
                }
            }
        }

        /// <summary>
        /// 地上世界-配置-路径
        /// </summary>
        public string OverworldConfigFilePath { get; set; }

        /// <summary>
        /// 地下世界-配置-路径
        /// </summary>
        public string CaveConfigFilePath { get; set; }


        /// <summary>
        /// mod-设置-地上
        /// </summary>
        public string ModConfigOverworldFilePath { get; set; }

        /// <summary>
        /// mod-设置-地下
        /// </summary>
        public string ModConfigCaveFilePath { get; set; }

        /// <summary>
        /// DoNotStarveTogether
        /// </summary>
        public string DoNotStarveTogetherDirPath
        {
            get => _doNotStarveTogetherDirPath;
            set
            {
                _doNotStarveTogetherDirPath = value;
                if (!string.IsNullOrEmpty(_doNotStarveTogetherDirPath))
                {
                    YyServerDirPath = _doNotStarveTogetherDirPath + @"\Server_" + GamePingTai + "_" + CunDangCao;
                }
            }
        }

        /// <summary>
        /// 客户端mods路径
        /// </summary>
        public string ClientModsDirPath { get; set; }

        /// <summary>
        /// 服务器mods路径
        /// </summary>
        public string ServerModsDirPath { get; set; }

        public string GamePingTai { get; set; }
        private int _cunDangCao;
        public int CunDangCao
        {
            get => _cunDangCao;
            set
            {
                _cunDangCao = value;
                YyServerDirPath = _doNotStarveTogetherDirPath + @"\Server_" + GamePingTai + "_" + value;
            }
        }

        public string PicDirPath { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="gamePingTai">游戏平台</param>
        /// <param name="cunDangCao">存档槽</param>
        public PathAll(string gamePingTai, int cunDangCao)
        {
            GamePingTai = gamePingTai;
            CunDangCao = cunDangCao;
            SetAllPath(gamePingTai, cunDangCao);
        }

        /// <summary>
        /// 设置所有路径
        /// </summary>
        /// <param name="gamePingTai">游戏平台</param>
        /// <param name="cunDangCao">第几个存档槽,从0开始</param>
        public void SetAllPath(string gamePingTai, int cunDangCao = 0)
        {
            GamePingTai = gamePingTai;
            CunDangCao = cunDangCao;
            // 我的文档
            DocumentDirPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            // 当前路径
            CurrentDirPath = Environment.CurrentDirectory;
            // 世界图片路径
            PicDirPath = CurrentDirPath + "\\世界图片";
            // DoNotStarveTogether
            if (gamePingTai.ToLower() == "tgp")
            {
                DoNotStarveTogetherDirPath = DocumentDirPath + @"\Klei\DoNotStarveTogetherRail";

            }
            if (gamePingTai.ToLower() == "youxia" || gamePingTai.ToLower() == "steam")
            {
                DoNotStarveTogetherDirPath = DocumentDirPath + @"\Klei\DoNotStarveTogether";
            }
            // 客户端服务器路径
            ClientFilePath = XmlHelper.ReadClientPath(gamePingTai);
            ServerFilePath = XmlHelper.ReadServerPath(gamePingTai);
        }
    }
}
