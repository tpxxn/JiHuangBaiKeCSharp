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
        public string Description;

        /// <summary>
        /// 小选项的数据
        /// </summary>
        public string Data;

        /// <summary>
        /// 小选项的解释
        /// </summary>
        public string Hover;

    }

    /// <summary>
    /// mod的每一个 小细节
    /// </summary>
    class ModXiJie
    {
        /// <summary>
        /// 小细节的名字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 小细节的label
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// 小细节的更多解释
        /// </summary>
        public string Hover { get; set; }

        /// <summary>
        /// 小细节的默认值
        /// </summary>
        public string Default1 { get; set; }

        /// <summary>
        /// 小细节的当前值（先读默认值，之后用读取的当前值覆盖）
        /// </summary>
        public string Current { get; set; }

        /// <summary>
        /// 小细节都有哪些选项
        /// </summary>
        internal List<Option> Options { get; set; }

        /// <summary>
        /// 小细节的当前值的翻译
        /// </summary>
        private string _currentDescription;

        public string CurrentDescription
        {
            get
            {
                foreach (var option in Options)
                {
                    if (option.Data == Current)
                    {
                        return option.Description;
                    }
                }
                return Current;
            }

        }
    }
}
