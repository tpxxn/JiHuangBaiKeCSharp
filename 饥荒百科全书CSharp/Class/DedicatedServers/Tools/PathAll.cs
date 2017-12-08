using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using 饥荒百科全书CSharp.Class.DedicatedServers.Tools;

namespace 饥荒百科全书CSharp.Class.DedicatedServers.Tools

{
    internal static class PathCommon
    {

        #region 读写当前游戏平台、客户端、服务器路径、ClusterToken
        /// <summary>
        /// 读取当前游戏版本[WeGame,Steam] 
        /// </summary>
        public static string ReadGamePlatform()
        {
            var platform = RegeditRw.RegReadString("Platform");
            return string.IsNullOrEmpty(platform) ? "Steam" : platform;
        }

        /// <summary>
        /// 保存当前游戏平台[WeGame,Steam]
        /// </summary>
        public static void WriteGamePlatform(string platform)
        {
            RegeditRw.RegWrite("Platform", platform);
        }

        /// <summary>
        /// 读取客户端路径
        /// </summary>
        public static string ReadClientPath(string platform)
        {
            return RegeditRw.RegReadString(platform + "_client_path");
        }

        /// <summary>
        /// 设置客户端路径
        /// </summary>
        public static void WriteClientPath(string clientPath, string platform)
        {
            RegeditRw.RegWrite(platform + "_client_path", clientPath);
        }

        /// <summary>
        /// 读取服务端路径
        /// </summary>
        public static string ReadServerPath(string platform)
        {
            return RegeditRw.RegReadString(platform + "_Server_path");
        }

        /// <summary>
        /// 设置服务端路径
        /// </summary>
        public static void WriteServerPath(string serverPath, string platform)
        {
            RegeditRw.RegWrite(platform + "_Server_path", serverPath);
        }

        /// <summary>
        /// 读取ClusterToken
        /// </summary>
        public static string ReadClusterTokenPath(string platform)
        {
            return RegeditRw.RegReadString(platform + "_ClusterToken");
        }

        /// <summary>
        /// 设置ClusterToken
        /// </summary>
        public static void WriteClusterTokenPath(string clusterToken, string platform)
        {
            RegeditRw.RegWrite(platform + "_ClusterToken", clusterToken);
        }
        #endregion

        /// <summary>
        /// 我的文档路径
        /// </summary>
        public static string DocumentDirPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        /// <summary>
        /// 存档根目录
        /// </summary>
        public static string SaveRootDirPath { get; set; }

        /// <summary>
        /// 游戏平台
        /// </summary>
        private static string _gamePlatform; 

        public static string GamePlatform
        {
            get => _gamePlatform;
            set
            {
                WriteGamePlatform(value);
                switch (value)
                {
                    case "WeGame":
                        SaveRootDirPath = DocumentDirPath + @"\Klei\DoNotStarveTogetherRail";
                        break;
                    case "Steam":
                        SaveRootDirPath = DocumentDirPath + @"\Klei\DoNotStarveTogether";
                        break;
                }
                _gamePlatform = value;
            }
        }

        /// <summary>
        /// 当前路径
        /// </summary>
        public static string CurrentDirPath { get; set; }

        /// <summary>
        /// 客户端exe文件路径
        /// </summary>
        private static string _clientFilePath;

        public static string ClientFilePath
        {
            get => _clientFilePath;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _clientFilePath = null;
                    //JsonHelper.WriteClientPath("", JsonHelper.ReadGamePlatform());
                    return;
                }
                _clientFilePath = value.Trim();
                WriteClientPath(_clientFilePath, ReadGamePlatform());
                ClientModsDirPath = _clientFilePath.Substring(0, _clientFilePath.Length - 25) + "\\mods";
            }
        }

        /// <summary>
        /// 服务端exe文件路径
        /// </summary>
        private static string _serverFilePath;

        public static string ServerFilePath
        {
            get => _serverFilePath;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _serverFilePath = null;
                    //JsonHelper.WriteServerPath("", JsonHelper.ReadGamePlatform());
                    return;
                }
                // 判断文件名对不对
                if (value.Contains("dontstarve_dedicated_server_nullrenderer.exe"))
                {
                    _serverFilePath = value.Trim();
                    WriteServerPath(_serverFilePath, ReadGamePlatform());
                    ServerModsDirPath = _serverFilePath.Substring(0, _serverFilePath.Length - 49) + "\\mods";
                }
            }
        }

        /// <summary>
        /// ClusterToken
        /// </summary>
        private static string _clusterToken;

        public static string ClusterToken
        {
            get => _clusterToken;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _clusterToken = null;
                    //JsonHelper.WriteClientPath("", JsonHelper.ReadGamePlatform());
                    return;
                }
                _clusterToken = value.Trim();
                WriteClusterTokenPath(_clusterToken, ReadGamePlatform());
            }
        }

        /// <summary>
        /// 客户端mods路径
        /// </summary>
        public static string ClientModsDirPath { get; set; }

        /// <summary>
        /// 服务器mods路径
        /// </summary>
        public static string ServerModsDirPath { get; set; }


    }

    internal class PathAll
    {
        /// <summary>
        /// Server路径
        /// </summary>
        private string _serverDirPath;

        public string ServerDirPath
        {
            get => _serverDirPath;
            set
            {
                _serverDirPath = value;
                if (!string.IsNullOrEmpty(_serverDirPath))
                {
                    ModConfigOverworldFilePath = _serverDirPath + "\\Master\\modoverrides.lua";
                    ModConfigCaveFilePath = _serverDirPath + "\\Caves\\modoverrides.lua";
                    OverworldConfigFilePath = _serverDirPath + "\\Master\\leveldataoverride.lua";
                    CaveConfigFilePath = _serverDirPath + "\\Caves\\leveldataoverride.lua";
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

        private int _saveSlot;
        public int SaveSlot
        {
            get => _saveSlot;
            set
            {
                _saveSlot = value;
                ServerDirPath = PathCommon.SaveRootDirPath + @"\Server_" + PathCommon.GamePlatform + "_" + value;
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
            if (!string.IsNullOrEmpty(PathCommon.SaveRootDirPath))
            {
                ServerDirPath = PathCommon.SaveRootDirPath + @"\Server_" + PathCommon.GamePlatform + "_" + SaveSlot;
            }
        }
    }
}
