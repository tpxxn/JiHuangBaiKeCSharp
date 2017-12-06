using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using Newtonsoft.Json;
using 饥荒百科全书CSharp.Class.DedicatedServers.JsonDeserialize;

namespace 饥荒百科全书CSharp.Class.DedicatedServers.Tools
{
    internal class JsonHelper
    {
        #region 读写当前游戏平台
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

        #endregion

        #region 读写 客户端,服务器 路径
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
        #endregion

        #region 汉化
        /// <summary>
        /// 读取汉化
        /// </summary>
        public static Dictionary<string, string> ReadHanhua()
        {
            var serverConfig = JsonConvert.DeserializeObject<ServerConfigRootObject>(StringProcess.GetJsonStringDedicatedServer("ServerConfig.json"));
            var dictionary = new Dictionary<string, string>();
            foreach (var detail in serverConfig.Configuration.Hanhua.Details)
            {
                dictionary[detail.English] = detail.Chinese;
            }
            return dictionary;
        }
        #endregion

        #region 读取世界选项,世界分类
        /// <summary>
        /// 读取世界选项,返回 x=xx,xx,xx
        /// </summary>
        /// <param name="isCave"></param>
        /// <returns></returns>
        public static List<string> ReadWorldSelect(bool isCave)
        {
            var serverConfig = JsonConvert.DeserializeObject<ServerConfigRootObject>(StringProcess.GetJsonStringDedicatedServer("ServerConfig.json"));
            var listStr = new List<string>();
            listStr.AddRange(!isCave
                ? serverConfig.Configuration.Master.Details.Select(detail =>
                    detail.Key.Trim() + "=" + detail.Value.Trim())
                : serverConfig.Configuration.Caves.Details.Select(detail =>
                    detail.Key.Trim() + "=" + detail.Value.Trim()));
            return listStr;
        }

        /// <summary>
        /// 读取世界分类,Fenlei="foods","animals","world","monsters","resources"
        /// </summary>
        public static Dictionary<string, string> ReadWorldFenLei(bool isCave)
        {
            var serverConfig = JsonConvert.DeserializeObject<ServerConfigRootObject>(StringProcess.GetJsonStringDedicatedServer("ServerConfig.json"));
            var dictionary = new Dictionary<string, string>();
            if (!isCave)
            {
                foreach (var detail in serverConfig.Configuration.Fenlei.Master.Details)
                {
                    var values = detail.Value.Split(',');
                    foreach (var value in values)
                    {
                        dictionary[value] = detail.Key;
                    }
                }
            }
            else
            {
                foreach (var detail in serverConfig.Configuration.Fenlei.Cave.Details)
                {
                    var values = detail.Value.Split(',');
                    foreach (var value in values)
                    {
                        dictionary[value] = detail.Key;
                    }
                }
            }
            return dictionary;
        }
        #endregion
    }
}
