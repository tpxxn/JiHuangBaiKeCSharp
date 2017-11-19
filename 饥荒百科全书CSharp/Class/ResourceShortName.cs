using System;
using System.Windows.Media.Imaging;

namespace 饥荒百科全书CSharp.Class
{
    static class RSN
    {
        /// <summary>
        /// 资源文件短名
        /// </summary>
        /// <param name="rUrl">资源文件第一层目录</param>
        /// <param name="rUrlSecond">资源文件第二层目录(可选)</param>
        /// <param name="extensionName">资源文件扩展名(可选)</param>
        /// <returns>资源文件路径</returns>
        public static string ShortName(string rUrl, string rUrlSecond = "" ,string extensionName = "png")
        {
            rUrl = "../Resources/" + rUrlSecond + rUrl + "." +extensionName;
            return rUrl;
        }

        /// <summary>
        /// 获取扩展名
        /// </summary>
        /// <param name="shortName">长字符串</param>
        /// <returns>资源文件路径</returns>
        public static string GetFileName(string shortName)
        {
            shortName = shortName.Substring(shortName.LastIndexOf('/') + 1, shortName.Length - shortName.LastIndexOf('/') - 5);
            return shortName;
        }

        /// <summary>
        /// 图片短名
        /// </summary>
        /// <param name="source">资源文件路径</param>
        /// <returns>BitmapImage类</returns>
        public static BitmapImage PictureShortName(string source = "")
        {
            var picture = new BitmapImage();
            picture.BeginInit();
            picture.UriSource = source == "" ? new Uri("{x:Null}", UriKind.Relative) : new Uri(source, UriKind.Relative);
            picture.EndInit();
            return picture;
        }


    }
}
