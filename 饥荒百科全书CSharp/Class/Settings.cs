using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 饥荒百科全书CSharp.Class
{
    public static class Settings
    {
        /// <summary>
        /// 是否隐藏到托盘图标
        /// </summary>
        public static bool HideToNotifyIcon { get; set; }

        /// <summary>
        /// 是否显示“是否隐藏到托盘图标”提示
        /// </summary>
        public static bool HideToNotifyIconPrompt { get; set; }

        /// <summary>
        /// 小图标模式
        /// </summary>
        public static bool SmallButtonMode { get; set; }
    }
}
