using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 饥荒百科全书CSharp.Class.DedicatedServers.DedicateServer
{
    /// <summary>
    /// 代表世界每个一对
    /// </summary>
    internal class ShowWorld
    {
        /// <summary>
        /// 显示世界每个小项
        /// </summary>
        /// <param name="picturePath">图片地址</param>
        /// <param name="worldConfigList">"选项" 例如,[少,默认,多,很多]</param>
        /// <param name="worldConfig">当前选项显示的值[例如,默认]</param>
        /// <param name="toolTip">toolTip</param>
        public ShowWorld(string picturePath, List<string> worldConfigList, string worldConfig, string toolTip)
        {
            PicturePath = picturePath;
            WorldConfigList = worldConfigList;
            WorldConfig = worldConfig;
            ToolTip = toolTip;
        }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string PicturePath { get; set; }

        /// <summary>
        /// "选项" 例如,[少,默认,多,很多]
        /// </summary>
        public List<string> WorldConfigList { get; set; }

        /// <summary>
        /// 选项中当前显示的值[例如,默认]
        /// </summary>
        public string WorldConfig { get; set; }

        /// <summary>
        /// ToolTip
        /// </summary>
        public string ToolTip { get; set; }
    }
}
