using System.Collections.Generic;

namespace 饥荒百科全书CSharp.Class.DedicatedServers.DedicateServer
{

    /// <summary>
    /// 小细节的每一个选项
    /// </summary>
    class Option
    {

        /// <summary>
        /// 小选项的描述，要显示的是这个
        /// </summary>
        public string description;

        /// <summary>
        /// 小选项的数据
        /// </summary>
        public string data;

        /// <summary>
        /// 小选项的解释
        /// </summary>
        public string hover;

    }

    /// <summary>
    /// mod的每一个 小细节
    /// </summary>
    class ModXiJie
    {
        /// <summary>
        /// 小细节的名字
        /// </summary>
        private string name;

        /// <summary>
        /// 小细节的label
        /// </summary>
        private string label;

        /// <summary>
        /// 小细节的更多解释
        /// </summary>
        private string hover;

        /// <summary>
        /// 小细节的默认值
        /// </summary>
        private string default1;

        /// <summary>
        /// 小细节的当前值（先读默认值，之后用读取的当前值覆盖）
        /// </summary>
        private string current;

        /// <summary>
        /// 小细节都有哪些选项
        /// </summary>
        private List<Option> options;

        /// <summary>
        /// 小细节的当前值的翻译
        /// </summary>
        private string currentDescription;
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public string Label
        {
            get
            {
                return label;
            }

            set
            {
                label = value;
            }
        }

        public string Hover
        {
            get
            {
                return hover;
            }

            set
            {
                hover = value;
            }
        }

        public string Default1
        {
            get
            {
                return default1;
            }

            set
            {
                default1 = value;
            }
        }

        public string Current
        {
            get
            {
                return current;
            }

            set
            {
                current = value;
            }
        }

        internal List<Option> Options
        {
            get
            {
                return options;
            }

            set
            {
                options = value;
            }
        }

        public string CurrentDescription
        {
            get
            {
                for (int i = 0; i < options.Count; i++)
                {
                    if (options[i].data==current)
                    {
                        return options[i].description;
                    }
                }
                return current;
            }
 
        }
    }
}
