using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 饥荒百科全书CSharp.Class.DedicatedServerClass.DedicateServer
{
    /// <summary>
    /// 代表世界每个一对
    /// </summary>
    class ShowWorld
    {
        private string path;
        private List<string> worldconfigList;
        private int num;
        private string worldconfig;
        private string toolTip;

        public ShowWorld(string path, List<string> worldconfigList,string worldconfig,string toolTip) {

            this.path = path;
            this.worldconfigList = worldconfigList;
            this.worldconfig = worldconfig;
            this.toolTip = toolTip;

        }

        public string Path
        {
            get
            {
                return path;
            }

            set
            {
                path = value;
            }
        }

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

        public int Num
        {
            get
            {
                return num;
            }

            set
            {
                num = value;
            }
        }

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
