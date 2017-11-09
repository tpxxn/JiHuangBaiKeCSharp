using System.Windows.Media;

namespace 饥荒百科全书CSharp.Class
{
    internal static class Bc
    {
        private static readonly BrushConverter brushConverter = new BrushConverter();
        /// <summary>
        /// 字符串转换为Brush
        /// </summary>
        /// <param name="rgb">十六进制颜色 R=Red G=Green B=Blue</param>
        /// <param name="alpha">透明度 Alpha</param>
        /// <returns></returns>
        public static Brush BrushConverter(string rgb, string alpha = "FF")
        {
            return (Brush)brushConverter.ConvertFromString("#" + alpha + rgb);
        }
    }
}
