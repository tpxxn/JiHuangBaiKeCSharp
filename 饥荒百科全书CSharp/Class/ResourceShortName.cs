using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace 饥荒百科全书CSharp.Class
{
    static class ResourceShortName
    {
        /// <summary>
        /// 资源文件短名
        /// </summary>
        /// <param name="RUrl">资源文件第一层目录</param>
        /// <param name="RUrlSecond">资源文件第二层目录(可选)</param>
        /// <param name="ExtensionName">资源文件扩展名(可选)</param>
        /// <returns>资源文件路径</returns>
        public static string ShortName(string RUrl, string RUrlSecond = "" ,string ExtensionName = "png")
        {
            RUrl = "Resources/" + RUrlSecond + RUrl + "." +ExtensionName;
            return RUrl;
        }

        /// <summary>
        /// 图片短名
        /// </summary>
        /// <param name="source">资源文件路径</param>
        /// <returns>BitmapImage类</returns>
        public static BitmapImage PictureShortName(string source = "")
        {
            var Picture = new BitmapImage();
            Picture.BeginInit();
            if (source == "")
            {
                Picture.UriSource = new Uri("{x:Null}", UriKind.Relative);
            }
            else
            {
                Picture.UriSource = new Uri(source, UriKind.Relative);
            }
            Picture.EndInit();
            return Picture;
        }
    }
}
