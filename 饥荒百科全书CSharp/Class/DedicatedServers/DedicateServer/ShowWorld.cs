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
        private string picPath;
        private List<string> worldconfigList;
 
        private string worldconfig;
        private string toolTip;

      
        /// <summary>
        /// 显示世界每个小项
        /// </summary>
        /// <param name="picPath">图片地址</param>
        /// <param name="worldconfigList">"选项" 例如,[少,默认,多,很多]</param>
        /// <param name="worldconfig">当前选项显示的值[例如,默认]</param>
        /// <param name="toolTip">toolTip</param>
        public ShowWorld(string picPath, List<string> worldconfigList,string worldconfig,string toolTip) {

            this.picPath = picPath;
            this.worldconfigList = worldconfigList;
            this.worldconfig = worldconfig;
            this.toolTip = toolTip;         
        }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string PicPath
        {
            get
            {
                return picPath;
            }

            set
            {
                picPath = value;
            }
        }

        /// <summary>
        /// "选项" 例如,[少,默认,多,很多]
        /// </summary>
        public List<string> WorldconfigList
        {
            get
            {
                return worldconfigList;
            }

            set
            {
                worldconfigList = value;
            }
        }


        /// <summary>
        /// 选项中当前显示的值[例如,默认]
        /// </summary>
        public string Worldconfig
        {
            get
            {
                return worldconfig;
            }

            set
            {
                worldconfig = value;
            }
        }

        /// <summary>
        /// toolTip
        /// </summary>
        public string ToolTip
        {
            get
            {
             
                return toolTip;
                
            }

            set
            {
                toolTip = value;
            }
        }
    }
}
