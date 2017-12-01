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
    class ShowWorld
    {
        /// <summary>
        /// 显示世界每个小项
        /// </summary>
        /// <param name="picPath">图片地址</param>
        /// <param name="worldconfigList">"选项" 例如,[少,默认,多,很多]</param>
        /// <param name="worldconfig">当前选项显示的值[例如,默认]</param>
        /// <param name="toolTip">toolTip</param>
        public ShowWorld(string picPath, List<string> worldconfigList, string worldconfig, string toolTip)
        {

            this.PicPath = picPath;
            this.WorldconfigList = worldconfigList;
            this.Worldconfig = worldconfig;
            this.ToolTip = toolTip;
        }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string PicPath { get; set; }

        /// <summary>
        /// "选项" 例如,[少,默认,多,很多]
        /// </summary>
        public List<string> WorldconfigList { get; set; }


        /// <summary>
        /// 选项中当前显示的值[例如,默认]
        /// </summary>
        public string Worldconfig { get; set; }

        /// <summary>
        /// toolTip
        /// </summary>
        public string ToolTip { get; set; }
    }
}
