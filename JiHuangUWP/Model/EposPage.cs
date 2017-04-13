using System;

namespace JiHuangUWP.Model
{
    public class EposPage
    {
        public EposPage(string name, Type page)
        {
            Name = name;
            Page = page;
        }

        /// <summary>
        /// 显示的名字
        /// </summary>
        public string Name
        {
            get; set;
        }
        /// <summary>
        /// 这里是ViewModel
        /// </summary>
        public Type Page
        {
            get; set;
        }
    }
}