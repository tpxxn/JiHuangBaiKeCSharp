using System.Windows.Media;

namespace 饥荒百科全书CSharp.Class
{
    static class BC
    {
        static BrushConverter bC = new BrushConverter();
        /// <summary>
        /// 字符串转换为Brush
        /// </summary>
        /// <param name="RGB">十六进制颜色 R=Red G=Green B=Blue</param>
        /// <param name="Alpha">透明度 Alpha</param>
        /// <returns></returns>
        public static Brush brushConverter(string RGB, string Alpha = "FF")
        {
            return (Brush)bC.ConvertFromString("#" + Alpha + RGB);
        }
    }
}
