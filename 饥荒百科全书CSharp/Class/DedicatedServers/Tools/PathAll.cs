using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 饥荒百科全书CSharp.Class.DedicatedServers.Tools;

namespace 饥荒百科全书CSharp.Class.DedicatedServers.Tools

{
    internal static class PathCommon
    {
        /// <summary>
        /// 游戏平台
        /// </summary>
        private static string _gamePlatform; 

        public static string GamePlatform
        {
            get => _gamePlatform;
            set
            {
                JsonHelper.WriteGamePlatform(value);
                _gamePlatform = value;
            }
        }

        /// <summary>
        /// 我的文档路径
        /// </summary>
        public static string DocumentDirPath { get; set; }

        /// <summary>
        /// 当前路径
        /// </summary>
        public static string CurrentDirPath { get; set; }

        /// <summary>
        /// 客户端exe文件路径
        /// </summary>
        private static string _clientFilePath;

        /// <summary>
        /// 客户端exe文件路径
        /// </summary>
        public static string ClientFilePath
        {
            get => _clientFilePath;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _clientFilePath = null;
                    JsonHelper.WriteClientPath("", JsonHelper.ReadGamePlatform());
                    return;
                }
                _clientFilePath = value.Trim();
                JsonHelper.WriteClientPath(_clientFilePath, JsonHelper.ReadGamePlatform());
            }
        }

        /// <summary>
        /// 服务端exe文件路径
        /// </summary>
        private static string _serverFilePath;

        /// <summary>
        /// 服务器exe文件路径
        /// </summary>
        public static string ServerFilePath
        {
            get => _serverFilePath;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _serverFilePath = null;
                    JsonHelper.WriteServerPath("", JsonHelper.ReadGamePlatform());
                    return;
                }
                // 判断文件名对不对
                if (value.Contains("dontstarve_dedicated_server_nullrenderer.exe"))
                {
                    _serverFilePath = value.Trim();
                    JsonHelper.WriteServerPath(_serverFilePath, JsonHelper.ReadGamePlatform());
                }
            }
        }
    }

    internal class PathAll
    {
        /// <summary>
        /// yyServer路径
        /// </summary>
        private string _yyServerDirPath;

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
        /// 地上世界-mod-设置
        /// </summary>
        public string ModConfigOverworldFilePath { get; set; }

        /// <summary>
        /// 地下世界-mod-设置
        /// </summary>
        public string ModConfigCaveFilePath { get; set; }

        /// <summary>
        /// DoNotStarveTogether
        /// </summary>
        private string _doNotStarveTogetherDirPath;

        public string DoNotStarveTogetherDirPath
        {
            get => _doNotStarveTogetherDirPath;
            set
            {
                _doNotStarveTogetherDirPath = value;
                if (!string.IsNullOrEmpty(_doNotStarveTogetherDirPath))
                {
                    YyServerDirPath = _doNotStarveTogetherDirPath + @"\Server_" + PathCommon.GamePlatform + "_" + SaveSlot;
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

        private int _saveSlot;
        public int SaveSlot
        {
            get => _saveSlot;
            set
            {
                _saveSlot = value;
                YyServerDirPath = _doNotStarveTogetherDirPath + @"\Server_" + PathCommon.GamePlatform + "_" + value;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="saveSlot">存档槽</param>
        public PathAll(int saveSlot)
        {
            SaveSlot = saveSlot;
            SetAllPath(PathCommon.GamePlatform, SaveSlot);
        }

        /// <summary>
        /// 设置所有路径
        /// </summary>
        /// <param name="gamePlatform">游戏平台</param>
        /// <param name="saveSlot">第几个存档槽,从0开始</param>
        public void SetAllPath(string gamePlatform, int saveSlot = 0)
        {
            //GamePlatform = gamePlatform;
            //SaveSlot = saveSlot;
            // DoNotStarveTogether
            switch (PathCommon.GamePlatform)
            {
                case "WeGame":
                    DoNotStarveTogetherDirPath = PathCommon.DocumentDirPath + @"\Klei\DoNotStarveTogetherRail";
                    break;
                case "Steam":
                    DoNotStarveTogetherDirPath = PathCommon.DocumentDirPath + @"\Klei\DoNotStarveTogether";
                    break;
            }
        }
    }
}
