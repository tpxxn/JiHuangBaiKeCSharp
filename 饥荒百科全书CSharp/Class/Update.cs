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
        private readonly WebClient _client = new WebClient();
        ///用于计算下载速度
        private readonly Stopwatch _stopwatch = new Stopwatch();
        ///当前版本
        private string _localVersion;
        ///最新版本
        private string _newVersion;
        ///下载地址
        private string _newVersionDownloadUrl;
        ///新版本文件名
        private string _newVersionFileName;
        ///MD5
        private string _md5Value;
        ///设置文件夹位置
        private static readonly string CurrentPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
        private static readonly string UpdatePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\JiHuangBaiKe\";
        private static readonly string UpdateXmlPath = UpdatePath + "update.xml";
        #endregion

        #region "成员属性"

        public bool Download { get; set; }

        public string DownloadSpeed { get; set; }

        public double DownloadProgress { get; set; }

        public string Downloaded { get; set; }

        public bool Downloadcompleted { get; set; }

        #endregion

        /// <summary>
        /// 开始更新，获取本地版本号
        /// </summary>
        public void UpdateNow()
        {
            _localVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
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
                ServicePointManager.ServerCertificateValidationCallback += delegate { return true; };// **** Always accept
                _client.DownloadFile("https://www.jihuangbaike.com/update/update.xml", UpdateXmlPath);
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
                var doc = new XmlDocument();
                //加载要读取的XML
                doc.Load(UpdateXmlPath);
                var list = doc.SelectSingleNode("Update");
                if (list == null) return;
                foreach (XmlNode node in list)
                {
                    if (node.Name != "Soft") continue;
                    foreach (XmlNode xml in node)
                    {
                        if (xml.Name == "Verson")
                        {
                            _newVersion = xml.InnerText;
                        }
                        if (xml.Name == "DownLoad")
                        {
                            _newVersionDownloadUrl = xml.InnerText;
                        }
                        if (xml.Name == "MD5")
                        {
                            _md5Value = xml.InnerText;
                        }
                    }
                    var slashPlace = _newVersionDownloadUrl.LastIndexOf('/');
                    _newVersionFileName = _newVersionDownloadUrl.Replace(".zip", "").Replace("_", " ")
                        .Substring(slashPlace + 1);
                    DownloadNewVersion();
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
        private void DownloadNewVersion()
        {
            if (_localVersion == _newVersion)
            {
                MessageBox.Show("恭喜你，已经更新到最新版本");
            }
            else if (_localVersion != _newVersion && File.Exists(UpdateXmlPath))
            {
                if (MessageBox.Show("检测到新版本，是否下载？", "检查更新", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    //检测并删除已经下载但未完成的文件
                    if (File.Exists(CurrentPath + _newVersionFileName + ".zip"))
                    {
                        if (GetMd5HashFromFile(CurrentPath + _newVersionFileName + ".zip") == _md5Value)
                        {
                            MessageBox.Show("检测到文件已下载，开始解压！");
                            Downloadcompleted = true;
                            //解压并运行
                            CompressionRun();
                        }
                        else
                        {
                            File.Delete(CurrentPath + _newVersionFileName + ".zip");
                            DownloadNewVersion_();
                        }
                    }
                    else
                    {
                        DownloadNewVersion_();
                    }
                }
            }
        }

        /// <summary>
        /// 下载
        /// </summary>
        private void DownloadNewVersion_()
        {
            var downloadWindow = new DownloadWindow(true);
            downloadWindow.Show();
            Download = true;
            //添加下载完成/下载进度事件
            _client.DownloadFileCompleted += Completed;
            _client.DownloadProgressChanged += ProgressChanged;
            //
            _stopwatch.Start();
            // 开始异步下载  
            var newVersionDownloadUrl = new Uri(_newVersionDownloadUrl);
            _client.DownloadFileAsync(newVersionDownloadUrl, CurrentPath + _newVersionFileName + ".zip");
        }
        
        /// <summary>
        /// 显示下载进度
        /// </summary>
        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            try
            {
                // 显示下载速度
                DownloadSpeed = (Convert.ToDouble(e.BytesReceived) / 1024 / _stopwatch.Elapsed.TotalSeconds).ToString("0.00") + " kb/s";
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
            _stopwatch.Reset();
            if (e.Cancelled)
            {
                MessageBox.Show("下载未完成!", "下载中断");
            }
            else
            {
                MessageBox.Show("下载完毕!");
                if (GetMd5HashFromFile(CurrentPath + _newVersionFileName + ".zip") == _md5Value)
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
            _client.CancelAsync();
        }

        /// <summary>
        /// 解压并运行
        /// </summary>
        private void CompressionRun()
        {
            //解压
            string zipPath = CurrentPath + _newVersionFileName + ".zip";

            if (!File.Exists(CurrentPath + _newVersionFileName + ".exe"))
            {
                using (ZipArchive archive = ZipFile.Open(zipPath, ZipArchiveMode.Update))
                {
                    archive.ExtractToDirectory(CurrentPath);
                }
            }
            //删除下载的压缩文件
            File.Delete(zipPath);
            //注册表写入需要删除的旧版本文件路径
            RegeditRw.RegWrite("OldVersionPath", System.Windows.Forms.Application.ExecutablePath);
            //运行
            Process.Start(CurrentPath + _newVersionFileName + ".exe");
            Environment.Exit(0);
        }

        /// <summary>
        /// 计算文件的MD5校验
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetMd5HashFromFile(string fileName)
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
