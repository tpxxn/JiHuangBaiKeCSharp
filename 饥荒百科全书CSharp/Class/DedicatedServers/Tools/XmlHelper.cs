using System;
using System.Collections.Generic;
using System.Xml;

namespace 饥荒百科全书CSharp.Class.DedicatedServers.Tools
{
    /// <summary>
    /// 读写ServerConfig.xml
    /// </summary>
    class XmlHelper
    {
        #region 读写当前游戏平台
        /// <summary>
        /// 读取当前游戏版本[tgp,steam,youxia] 
        /// </summary>
        public static String ReadGamePingTai()
        {

            string pingtai = RegeditRw.RegReadString("banben");
            if (string.IsNullOrEmpty(pingtai))
            {
                return "Steam";
            }
            else
            {
                return pingtai;
            }

            //xmlpath = "ServerConfig1.xml";
            //XmlDocument xmldoc = new XmlDocument();
            //xmldoc.Load(xmlpath);
            //XmlNode details = xmldoc.SelectSingleNode("configuration/banben/details");
            //string s = ((XmlElement)details).GetAttribute("value").Trim();

            //if (s.ToLower() == "tgp")
            //{
            //    return "TGP";
            //}
            //else
            //if (s.ToLower() == "steam")
            //{
            //    return "Steam";
            //}
            //else
            //{
            //    return "youxia";
            //}
        }

        /// <summary>
        /// 保存当前游戏平台[tgp,steam,youxia]
        /// </summary>
        public static void WriteGamePingTai(String pingtai)
        {
            //xmlpath = "ServerConfig1.xml";
            //Dictionary<string, string> dic = new Dictionary<string, string>();
            //XmlDocument xmldoc = new XmlDocument();
            //xmldoc.Load(xmlpath);
            //XmlNode details = xmldoc.SelectSingleNode("configuration/banben/details");

            //((XmlElement)details).SetAttribute("value", pingtai.ToString());

            //xmldoc.Save(xmlpath);

            RegeditRw.RegWrite("banben", pingtai);


        }

        #endregion

        #region 读写 客户端,服务器 路径
        /// <summary>
        /// 读取客户端路径
        /// </summary>
        public static string ReadClientPath(String pingtai)
        {
            return RegeditRw.RegReadString(pingtai + "_" + "client_path");
        }

        /// <summary>
        /// 设置客户端路径
        /// </summary>
        public static void WriteClientPath(string clientPath, String pingtai)
        {
            RegeditRw.RegWrite(pingtai + "_" + "client_path", clientPath);
        }

        /// <summary>
        /// 读取服务端路径
        /// </summary>
        public static string ReadServerPath(string pingtai)
        {
            return RegeditRw.RegReadString(pingtai + "_" + "Server_path");
        }

        /// <summary>
        /// 设置服务端路径
        /// </summary>
        public static void WriteServerPath(string serverPath, string pingtai)
        {
            RegeditRw.RegWrite(pingtai + "_" + "Server_path", serverPath);
        }
        #endregion

        #region 汉化
        /// <summary>
        /// 读取汉化
        /// </summary>
        public static Dictionary<string, string> ReadHanhua()
        {
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            var sStream = assembly.GetManifestResourceStream("饥荒百科全书CSharp.XML.ServerConfig.xml");
            var xmldoc = new XmlDocument();
            xmldoc.Load(sStream);

            var dic = new Dictionary<string, string>();
            //XmlDocument xmldoc = new XmlDocument();
            //xmldoc.Load(xmlpath);
            var details = xmldoc.SelectNodes("configuration/hanhua/details");

            foreach (XmlNode item in details)
            {
                var english = ((XmlElement)item).GetAttribute("english").Trim();
                var chinese = ((XmlElement)item).GetAttribute("chinese").Trim();
                dic[english] = chinese;
            }
            return dic;
        }

        #endregion

        #region 读取世界选项,世界分类
        /// <summary>
        /// 读取世界选项,返回 x=xx,xx,xx
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <param name="isCave"></param>
        /// <returns></returns>
        public static List<string> ReadWorldSelect(bool isCave)
        {
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            var sStream = assembly.GetManifestResourceStream("饥荒百科全书CSharp.XML.ServerConfig.xml");
            var xmldoc = new XmlDocument();
            xmldoc.Load(sStream);

            var listStr = new List<string>();
            //XmlDocument xmldoc = new XmlDocument();
            //xmldoc.Load(xmlPath);
            var details = xmldoc.SelectNodes(!isCave ? "configuration/master/details" : "configuration/caves/details");

            foreach (XmlNode item in details)
            {
                var key = ((XmlElement)item).GetAttribute("key");
                var value = ((XmlElement)item).GetAttribute("value");
                listStr.Add(key.Trim() + "=" + value.Trim());
            }
            return listStr;
        }

        /// <summary>
        /// 读取世界分类,fenlei="foods","animals","world","monsters","resources"
        /// </summary>
        public static Dictionary<string, string> ReadWorldFenLei(bool isCave)
        {
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            var sStream = assembly.GetManifestResourceStream("饥荒百科全书CSharp.XML.ServerConfig.xml");
            var xmldoc = new XmlDocument();
            xmldoc.Load(sStream);

            var isCavestr = isCave ? "cave" : "master";

            var dic = new Dictionary<string, string>();
            //XmlDocument xmldoc = new XmlDocument();
            //xmldoc.Load(xmlPath);
            var details = xmldoc.SelectNodes("configuration/fenlei/" + isCavestr + "/details");

            foreach (var item in details)
            {
                var key = ((XmlElement)item).GetAttribute("key");
                var value = ((XmlElement)item).GetAttribute("value").Split(',');

                foreach (var val in value)
                {
                    dic[val] = key;
                }
            }
            return dic;
        }
        #endregion
    }
}
