using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Windows;
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
        ///MD5
        private string MD5Value = null;
        ///设置文件夹位置
        static string CurrentPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
        static string UpdatePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\JiHuangBaiKe\";
        static string UpdateXmlPath = UpdatePath + "update.xml";
        #endregion

        #region "成员属性"
        public bool download;
        public string downloadSpeed;
        public double downloadProgress;
        public string downloaded;
        public bool downloadcompleted;

        public bool Download
        {
            get { return download; }
            set { download = value; }
        }
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
        public bool Downloadcompleted
        {
            get { return downloadcompleted; }
            set { downloadcompleted = value; }
        }
        #endregion

        /// <summary>
        /// 开始更新，获取本地版本号
        /// </summary>
        public void UpdateNow()
        {
            LocalVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            DownloadCheckUpdateXml();
        }

        /// <summary>
        /// 从服务器上获取最新的版本号(下载XML文件)
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
                MessageBox.Show("获取版本号信息文件失败");
            }
        }

        /// <summary>
        /// 读取从服务器获取的最新版本号(读取XML文件)
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
                            if (xml.Name == "MD5")
                            {
                                MD5Value = xml.InnerText;
                            }
                        }
                        int slashPlace = NewVersionDownloadURL.LastIndexOf('/');
                        NewVersionFileName = NewVersionDownloadURL.Replace(".zip", "").Replace("_"," ").Substring(slashPlace + 1);
                        DownloadNewvirsion();
                    }
                }
            }
            else if (!File.Exists(UpdateXmlPath))
            {
                MessageBox.Show("获取版本号信息失败");
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// 下载新版本
        /// </summary>
        private void DownloadNewvirsion()
        {
            if (LocalVersion == NewVersion)
            {
                MessageBox.Show("恭喜你，已经更新到最新版本");
            }
            else if (LocalVersion != NewVersion && File.Exists(UpdateXmlPath))
            {
                if (MessageBox.Show("检测到新版本，是否下载？", "检查更新", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    //检测并删除已经下载但未完成的文件
                    if (File.Exists(CurrentPath + NewVersionFileName + ".zip"))
                    {
                        if (GetMD5HashFromFile(CurrentPath + NewVersionFileName + ".zip") == MD5Value)
                        {
                            MessageBox.Show("检测到文件已下载，开始解压！");
                            Downloadcompleted = true;
                            //解压并运行
                            CompressionRun();
                        }
                        else
                        {
                            File.Delete(CurrentPath + NewVersionFileName + ".zip");
                            DownloadNewversion_();
                        }
                    }
                    else
                    {
                        DownloadNewversion_();
                    }
                }
            }
        }

        /// <summary>
        /// 下载
        /// </summary>
        private void DownloadNewversion_()
        {
            DownloadWindow DW = new DownloadWindow();
            DW.Show();
            Download = true;
            //添加下载完成/下载进度事件
            client.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
            //
            stopwatch.Start();
            // 开始异步下载  
            Uri newVersionDownloadURL = new Uri(NewVersionDownloadURL);
            client.DownloadFileAsync(newVersionDownloadURL, CurrentPath + NewVersionFileName + ".zip");
        }
        
        /// <summary>
        /// 显示下载进度
        /// </summary>
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

        /// <summary>
        /// 下载完成/中断操作
        /// </summary>
        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            stopwatch.Reset();
            if (e.Cancelled == true)
            {
                MessageBox.Show("下载未完成!", "下载中断");
            }
            else
            {
                MessageBox.Show("下载完毕!");
                if (GetMD5HashFromFile(CurrentPath + NewVersionFileName + ".zip") == MD5Value)
                {
                    MessageBox.Show("MD5校验正确！");
                    Downloadcompleted = true;
                    CompressionRun();
                }
                else
                {
                    MessageBox.Show("MD5校验错误！");
                }
            }
        }

        /// <summary>
        /// 取消下载
        /// </summary>
        public void DownloadCancel()
        {
            client.CancelAsync();
        }

        /// <summary>
        /// 解压并运行
        /// </summary>
        private void CompressionRun()
        {
            //解压
            string zipPath = CurrentPath + NewVersionFileName + ".zip";

            if (!File.Exists(CurrentPath + NewVersionFileName + ".exe"))
            {
                using (ZipArchive archive = ZipFile.Open(zipPath, ZipArchiveMode.Update))
                {
                    archive.ExtractToDirectory(CurrentPath);
                }
            }
            //删除下载的压缩文件
            File.Delete(zipPath);
            //注册表写入需要删除的旧版本文件路径
            RegeditRW.RegWrite("OldVersionPath", System.Windows.Forms.Application.ExecutablePath);
            //运行
            Process.Start(CurrentPath + NewVersionFileName + ".exe");
            Environment.Exit(0);
        }

        /// <summary>
        /// 计算文件的MD5校验
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetMD5HashFromFile(string fileName)
        {
            try
            {
                FileStream file = new FileStream(fileName, FileMode.Open);
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(file);
                file.Close();

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                //MessageBox.Show(sb.ToString());
                return sb.ToString().ToUpper();
            }
            catch (Exception ex)
            {
                throw new Exception("获取MD5值失败,错误：" + ex.Message);
            }
        }
    }
}
