using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml;

namespace 饥荒百科全书CSharp.Class
{
    public class Update
    {
        #region "成员变量"
        ///使用WebClient下载
        private WebClient client = new WebClient();
        ///用于计算下载速度
        private Stopwatch stopwatch = new Stopwatch();
        ///当前版本
        private string LocalVersion = null;
        ///最新版本
        private string NewVersion = null;
        ///下载地址
        private string NewVersionDownloadURL = null;
        ///新版本文件名
        private string NewVersionFileName = null;
        ////主窗体
        //public MainWindow form;
        ///通知内容
        private string nnidtext = null;
        ///设置文件夹位置
        static string CurrentPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
        static string UpdatePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\JiHuangBaiKe\";
        static string UpdateXmlPath = UpdatePath + "update.xml";
        #endregion

        /// <summary>
        /// 开始更新
        /// </summary>
        public void UpdateNow()
        {
            NowVersion();
        }

        /// <summary>
        /// 获取本地版本号
        /// </summary>
        private void NowVersion()
        {
            LocalVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            DownloadCheckUpdateXml();
        }

        /// <summary>
        /// 从服务器上获取最新的版本号
        /// </summary>
        private void DownloadCheckUpdateXml()
        {
            try
            {
                //创建update.xml文件
                if ((Directory.Exists(UpdatePath)) == false)
                {
                    Directory.CreateDirectory(UpdatePath);
                }
                //第一个参数是文件的地址,第二个参数是文件保存的路径文件名
                client.DownloadFile("http://www.jihuangbaike.com/Update/update.xml", UpdateXmlPath);
                LatestVersion();
            }
            catch
            {
                nnidtext = "检查更新失败";
                MessageBox.Show(nnidtext);
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// 读取从服务器获取的最新版本号
        /// </summary>
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
                                NewVersionDownloadURL = xml.InnerText;
                            }
                            if (xml.Name == "FileName")
                            {
                                NewVersionFileName = xml.InnerText;
                            }
                        }
                        DownloadInstall();
                    }
                }
            }
            else if (!File.Exists(UpdateXmlPath))
            {
                nnidtext = "检查更新失败";
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// 下载安装包
        /// </summary>
        private void DownloadInstall()
        {
            if (LocalVersion == NewVersion)
            {
                nnidtext = "恭喜你，已经更新到最新版本";
            }
            else if (LocalVersion != NewVersion && File.Exists(UpdateXmlPath))
            {
                nnidtext = "发现新版本，即将下载新版本";
                client.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
                stopwatch.Start();
                // 开始异步下载  
                Uri newVersionDownloadURL = new Uri(NewVersionDownloadURL);
                client.DownloadFileAsync(newVersionDownloadURL, CurrentPath + NewVersionFileName);
                DownloadWindow downloadWindow = new DownloadWindow();
                downloadWindow.ShowDialog();
                //for (int i = 1; i < 3; i++)
                //{
                //    if (File.Exists(CurrentPath + NewVersionFileName))
                //    {
                //        InstallandDelete();//这里调用安装的类
                //        break;
                //    }
                //    else if (!File.Exists(CurrentPath + NewVersionFileName))
                //    {
                //        //如果一次没有下载成功，则检查三次
                //        client.DownloadFileAsync(newVersionDownloadURL, CurrentPath + NewVersionFileName);
                //        nnidtext = "下载失败，请检查您的网络连接是否正常";
                //        Environment.Exit(0);
                //    }
                //}    
            }
        }

        public string downloadSpeed;
        public double downloadProgress;
        public string downloaded;

        public string DownloadSpeed
        {
            get { return downloadSpeed; }
            set { downloadSpeed = value; }
        }
        public double DownloadProgress
        {
            get { return downloadProgress; }
            set { downloadProgress = value; }
        }
        public string Downloaded
        {
            get { return downloaded; }
            set { downloaded = value; }
        }

        //显示下载进度
        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {

            try
            {
                // 显示下载速度
                DownloadSpeed = (Convert.ToDouble(e.BytesReceived) / 1024 / stopwatch.Elapsed.TotalSeconds).ToString("0.00") + " kb/s";
                // 进度条  
                DownloadProgress = e.ProgressPercentage;
                // 当前比例  
                //labelPerc.Content = e.ProgressPercentage.ToString() + "%";
                // 下载了多少 还剩余多少  
                Downloaded = (Convert.ToDouble(e.BytesReceived) / 1024 / 1024).ToString("0.00") + " Mb" + "  /  " + (Convert.ToDouble(e.TotalBytesToReceive) / 1024 / 1024).ToString("0.00") + " Mb";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // The event that will trigger when the WebClient is completed  
        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            stopwatch.Reset();
            if (e.Cancelled == true)
            {
                MessageBox.Show("下载未完成!");
            }
            else
            {
                MessageBox.Show("下载完毕!");
                if (File.Exists(CurrentPath + NewVersionFileName))
                {
                    InstallandDelete();//这里调用安装的类
                }
            }

        }

        /// <summary>
        /// 安装及删除
        /// </summary>
        private void InstallandDelete()
        {
            ////安装前关闭正在运行的程序
            //KillProgram();
            //启动安装程序
            Process.Start(NewVersionFileName);
            Environment.Exit(0);
            //JudgeInstall();
        }

        #region "其他"
        ///// <summary>
        ///// 判断安装进程是否存在
        ///// </summary>
        //public void JudgeInstall()
        //{
        //    Process[] processList = Process.GetProcesses();
        //    foreach (Process process in processList)
        //    {
        //        if (process.ProcessName == "NewCloudTranslator2_2_1_210_Setup.exe")
        //        {
        //            process.Kill();
        //            File.Delete(@"Update\NewCloudTranslator2_2_1_210_Setup.exe");
        //            File.Delete(@"Update\NewcloudTranslator221210.XML");
        //        }
        //        else
        //        {
        //            File.Delete(@"Update\NewCloudTranslator2_2_1_210_Setup.exe");
        //            File.Delete(@"Update\NewcloudTranslator221210.XML");
        //            return;
        //        }
        //    }
        //}

        ///// <summary>
        ///// 结束程序
        ///// </summary>
        //public void KillProgram()
        //{
        //    Process[] processList = Process.GetProcesses();
        //    foreach (Process process in processList)
        //    {
        //        //如果程序启动了，则杀死
        //        if (process.ProcessName == "新云翻译器.exe")
        //        {
        //            process.Kill();
        //        }
        //    }
        //}
        #endregion
    }
}
