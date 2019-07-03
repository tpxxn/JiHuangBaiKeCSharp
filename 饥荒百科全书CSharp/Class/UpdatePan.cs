using System;
using System.ComponentModel;
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
        private readonly WebClient _client = new WebClient();
        //用于计算下载速度
        private Stopwatch _stopwatch = new Stopwatch();
        //当前版本
        private string _localVersion = null;
        //XML文件下载地址
        private static readonly string UpdatePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\JiHuangBaiKeCSharp\Update\";
        private static readonly string UpdateXmlPath = UpdatePath + "update.xml";
        #endregion

        #region "属性"
        /// <summary>
        /// 新版本版本号
        /// </summary>
        public string NewVersion { get; set; }

        /// <summary>
        /// 新版本下载地址
        /// </summary>
        public string DownloadUrl { get; set; }

        #endregion

        #region "方法"
        //开始更新，获取本地版本号
        public void UpdateNow()
        {
            _localVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            DownloadUpdateXml();//从服务器上获取最新的版本号(下载XML文件)
        }

        //从服务器上获取最新的版本号(下载XML文件)
        private void DownloadUpdateXml()
        {
            try
            {
                //创建update.xml文件
                if (Directory.Exists(UpdatePath) == false)
                {
                    Directory.CreateDirectory(UpdatePath);
                }
                if (File.Exists(UpdateXmlPath))
                {
                    File.Delete(UpdateXmlPath);
                }
                ServicePointManager.ServerCertificateValidationCallback += delegate { return true; };// **** Always accept
                //第一个参数是下载地址,第二个参数是文件保存的路径文件名
                _client.DownloadFileAsync(new Uri("https://www.jihuangbaike.com/update/update.xml"), UpdateXmlPath);
                //读取从服务器获取的最新版本号(读取XML文件)
                _client.DownloadFileCompleted += Completed;
                //LatestVersion();
            }
            catch(Exception e)
            {
                MessageBox.Show("获取版本号信息文件失败\r\n" + e.Message);
            }
        }

        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            LatestVersion();
        }

        //读取从服务器获取的最新版本号(读取XML文件)
        private void LatestVersion()
        {
            try
            {
                if (File.Exists(UpdateXmlPath))
                {
                    var doc = new XmlDocument();
                    //加载要读取的XML
                    doc.Load(UpdateXmlPath);
                    var list = doc.SelectSingleNode("Update");
                    // ReSharper disable once PossibleNullReferenceException
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
                                    DownloadUrl = xml.InnerText;
                                }
                            }
                            //获取新版本
                            GetNewVersion();
                        }
                    }
                }
                else if (!File.Exists(UpdateXmlPath))
                {
                    MessageBox.Show("获取版本号信息失败");
                    //Environment.Exit(0);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("加载文件失败\r\n" + e.Message);
            }
        }

        //获取新版本
        private void GetNewVersion()
        {
            if (VersionCompare(_localVersion, NewVersion) && File.Exists(UpdateXmlPath))
            {
                var downloadWindow = new DownloadWindow(true);
                downloadWindow.Show();
            }
        }

        private static bool VersionCompare(string localVersion, string newVersion)
        {
            var localVersionNum = int.Parse(localVersion.Replace(".", ""));
            var newVersionNum = int.Parse(newVersion.Replace(".", ""));
            return localVersionNum < newVersionNum;
        }
        #endregion
    }

}
