using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using 饥荒百科全书CSharp.Class;

namespace ServerTools
{
  

    /// <summary>
    /// 读写ServerConfig.xml
    /// </summary>
    class XmlHelper
    {

 


        #region 读写当前游戏平台
        /// <summary>
        /// 读取当前游戏版本[tgp,steam,游侠] 
        /// </summary>
        public static String ReadGamePingTai( )
        {

            string pingtai=RegeditRW.RegReadString("banben");
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
            //    return "游侠";
            //}
         

        }

        /// <summary>
        /// 保存当前游戏平台[tgp,steam,游侠]
        /// </summary>
        public static void WriteGamePingTai( String pingtai)
        {
            //xmlpath = "ServerConfig1.xml";
            //Dictionary<string, string> dic = new Dictionary<string, string>();
            //XmlDocument xmldoc = new XmlDocument();
            //xmldoc.Load(xmlpath);
            //XmlNode details = xmldoc.SelectSingleNode("configuration/banben/details");

            //((XmlElement)details).SetAttribute("value", pingtai.ToString());

            //xmldoc.Save(xmlpath);

            RegeditRW.RegWrite("banben", pingtai);


        }

        #endregion

        #region 读写 客户端,服务器 路径
        /// <summary>
        /// 读取客户端路径
        /// </summary>
        public static string ReadClientPath(  String pingtai)
        {
            return RegeditRW.RegReadString(pingtai + "_" + "client_path");
        }

        /// <summary>
        /// 设置客户端路径
        /// </summary>
        public static void WriteClientPath( string ClientPath, String pingtai)
        {

            RegeditRW.RegWrite(pingtai + "_" + "client_path", ClientPath);

        }

        /// <summary>
        /// 读取服务端路径
        /// </summary>
        public static string ReadServerPath( String pingtai)
        {
            return RegeditRW.RegReadString(pingtai + "_" + "Server_path");
        }

        /// <summary>
        /// 设置服务端路径
        /// </summary>
        public static void WriteServerPath( string ServerPath, String pingtai)
        {
            RegeditRW.RegWrite(pingtai + "_" + "Server_path", ServerPath);
        }
        #endregion

        #region 汉化
        /// <summary>
        /// 读取汉化
        /// </summary>
        public static Dictionary<string, string> ReadHanhua()
        {
            System.Reflection.Assembly _assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.IO.Stream sStream = _assembly.GetManifestResourceStream("饥荒百科全书CSharp.XML.ServerConfig.xml");
            System.Xml.XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(sStream);

            Dictionary<string, string> dic = new Dictionary<string, string>();
            //XmlDocument xmldoc = new XmlDocument();
            //xmldoc.Load(xmlpath);
            XmlNodeList details = xmldoc.SelectNodes("configuration/hanhua/details");

            foreach (XmlNode item in details)
            {
                string english = ((XmlElement)item).GetAttribute("english").Trim();
                string chinese = ((XmlElement)item).GetAttribute("chinese").Trim();
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
            System.Reflection.Assembly _assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.IO.Stream sStream = _assembly.GetManifestResourceStream("饥荒百科全书CSharp.XML.ServerConfig.xml");
            System.Xml.XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(sStream);


            List<string> listStr = new List<string>();
            //XmlDocument xmldoc = new XmlDocument();
            //xmldoc.Load(xmlPath);
            XmlNodeList details;
            if (!isCave)
            {
                details = xmldoc.SelectNodes("configuration/master/details");
            }
            else {
                details = xmldoc.SelectNodes("configuration/caves/details");
            }

            foreach (XmlNode item in details)
            {
               string key= ((XmlElement)item).GetAttribute("key");
               string value= ((XmlElement)item).GetAttribute("value");
                listStr.Add(key.Trim() + "=" + value.Trim());
            }
            return listStr;
        }

        /// <summary>
        /// 读取世界分类,fenlei="foods","animals","world","monsters","resources"
        /// </summary>
        public static Dictionary<string, string> ReadWorldFenLei(bool isCave) {

            System.Reflection.Assembly _assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.IO.Stream sStream = _assembly.GetManifestResourceStream("饥荒百科全书CSharp.XML.ServerConfig.xml");
            System.Xml.XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(sStream);




            string isCavestr = isCave? "cave" : "master";

            Dictionary<string, string> dic = new Dictionary<string, string>();
            //XmlDocument xmldoc = new XmlDocument();
            //xmldoc.Load(xmlPath);
            XmlNodeList details = xmldoc.SelectNodes("configuration/fenlei/"+isCavestr+"/details");

            foreach (var item in details)
            {
                string key = ((XmlElement)item).GetAttribute("key");
                string[] value = ((XmlElement)item).GetAttribute("value").Split(',');

                for (int i = 0; i < value.Length; i++)
                {
                    dic[value[i]] = key;
                }

            }

            return dic;
  
        }
        #endregion
    }
}
