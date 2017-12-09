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
        #region 汉化
        /// <summary>
        /// 读取汉化
        /// </summary>
        public static Dictionary<string, string> ReadHanization()
        {
            var serverConfig = JsonConvert.DeserializeObject<ServerConfigRootObject>(StringProcess.GetJsonStringDedicatedServer("ServerConfig.json"));
            var dictionary = new Dictionary<string, string>();
            foreach (var detail in serverConfig.Configuration.Hanization.Details)
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
        /// <returns>x=xx,xx,xx</returns>
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
        public static Dictionary<string, string> ReadWorldClassification(bool isCave)
        {
            var serverConfig = JsonConvert.DeserializeObject<ServerConfigRootObject>(StringProcess.GetJsonStringDedicatedServer("ServerConfig.json"));
            var dictionary = new Dictionary<string, string>();
            if (!isCave)
            {
                foreach (var detail in serverConfig.Configuration.Classification.Master.Details)
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
                foreach (var detail in serverConfig.Configuration.Classification.Cave.Details)
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
