using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows;
using System.Xml;

namespace 饥荒百科全书CSharp.Class
{
    public class UpdatePan
    {
        #region "字段"
        //使用WebClient下载
        private WebClient client = new WebClient();
        //用于计算下载速度
        private Stopwatch stopwatch = new Stopwatch();
        //当前版本
        private string LocalVersion = null;
        //XML文件下载地址
        private static string UpdatePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\JiHuangBaiKeCSharp\Update\";
        private static string UpdateXmlPath = UpdatePath + "update.xml";
        #endregion

        #region "属性"
        //新版本版本号
        private string newVersion;
        //新版本下载地址
        private string downloadURL;

        public string NewVersion
        {
            get { return newVersion; }
            set { newVersion = value; }
        }
        public string DownloadURL
        {
            get { return downloadURL; }
            set { downloadURL = value; }
        }
        #endregion

        #region "方法"
        //开始更新，获取本地版本号
        public void UpdateNow()
        {
            LocalVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            DownloadUpdateXml();//从服务器上获取最新的版本号(下载XML文件)
        }

        //从服务器上获取最新的版本号(下载XML文件)
        private void DownloadUpdateXml()
        {
            try
            {
                //创建update.xml文件
                if ((Directory.Exists(UpdatePath)) == false)
                {
                    Directory.CreateDirectory(UpdatePath);
                }
                //第一个参数是下载地址,第二个参数是文件保存的路径文件名
                client.DownloadFile("http://www.jihuangbaike.com/update/update.xml", UpdateXmlPath);
                LatestVersion();//读取从服务器获取的最新版本号(读取XML文件)
            }
            catch
            {
                MessageBox.Show("获取版本号信息文件失败");
            }
        }

        //读取从服务器获取的最新版本号(读取XML文件)
        private void LatestVersion()
        {
            if (File.Exists(UpdateXmlPath))
            {
                XmlDocument doc = new XmlDocument();
                //加载要读取的XML
                doc.Load(UpdateXmlPath);
                XmlNode list = doc.SelectSingleNode("Update");
                foreach (XmlNode node in list)
                {
                    if (node.Name == "Soft")
                    {
                        foreach (XmlNode xml in node)
                        {
                            if (xml.Name == "Verson")
                            {
                                NewVersion = xml.InnerText;
                            }
                            if (xml.Name == "DownLoad")
                            {
                                DownloadURL = xml.InnerText;
                            }
                        }
                        GetNewVersion();//获取新版本
                    }
                }
            }
            else if (!File.Exists(UpdateXmlPath))
            {
                MessageBox.Show("获取版本号信息失败");
                Environment.Exit(0);
            }
        }

        //获取新版本
        private void GetNewVersion()
        {
            if (LocalVersion == NewVersion)
            {
                MessageBox.Show("你正在使用最新版本！");
            }
            else if (LocalVersion != NewVersion && File.Exists(UpdateXmlPath))
            {
                DownloadWindow DW = new DownloadWindow();
                DW.Show();
            }
        }
        #endregion
    }
}
