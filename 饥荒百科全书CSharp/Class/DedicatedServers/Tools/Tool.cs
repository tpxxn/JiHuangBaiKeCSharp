using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 饥荒百科全书CSharp.Class.DedicatedServers.Tools
{
    class Tool
    {


        #region 读取资源文件
        /// <summary>
        /// 读取资源文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ReadResources(string path)
        {
            var utf8Encoding = new UTF8Encoding(false);

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            var sStream = assembly.GetManifestResourceStream("饥荒百科全书CSharp." + path);
            var sr = new StreamReader(sStream, Encoding.UTF8);
            return sr.ReadToEnd();
        }
        #endregion

        /// <summary>
        /// 拷贝文件夹,会覆盖!!
        /// </summary>
        /// <param name="strFromPath"></param>
        /// <param name="strToPath"></param>
        public static void CopyDirectory(string strFromPath, string strToPath)
        {
            //如果源文件夹不存在，则创建
            if (!Directory.Exists(strFromPath))
            {
                Directory.CreateDirectory(strFromPath);
            }
            //取得要拷贝的文件夹名
            var strFolderName = strFromPath.Substring(strFromPath.LastIndexOf("\\") +
              1, strFromPath.Length - strFromPath.LastIndexOf("\\") - 1);
            //如果目标文件夹中没有源文件夹则在目标文件夹中创建源文件夹
            if (!Directory.Exists(strToPath + "\\" + strFolderName))
            {
                Directory.CreateDirectory(strToPath + "\\" + strFolderName);
            }
            //创建数组保存源文件夹下的文件名
            var strFiles = Directory.GetFiles(strFromPath);
            //循环拷贝文件
            foreach (var filename in strFiles)
            {
                //取得拷贝的文件名，只取文件名，地址截掉。
                var strFileName = filename.Substring(filename.LastIndexOf("\\") + 1, filename.Length - filename.LastIndexOf("\\") - 1);
                //开始拷贝文件,true表示覆盖同名文件
                File.Copy(filename, strToPath + "\\" + strFolderName + "\\" + strFileName, true);
            }
            //创建DirectoryInfo实例
            var dirInfo = new DirectoryInfo(strFromPath);
            //取得源文件夹下的所有子文件夹名称
            var ziPath = dirInfo.GetDirectories();
            foreach (DirectoryInfo ziDirectoryInfo in ziPath)
            {
                //获取所有子文件夹名
                string strZiPath = strFromPath + "\\" + ziDirectoryInfo;
                //把得到的子文件夹当成新的源文件夹，从头开始新一轮的拷贝
                CopyDirectory(strZiPath, strToPath + "\\" + strFolderName);
            }
        }

    }


}
