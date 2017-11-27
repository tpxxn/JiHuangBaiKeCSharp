using Neo.IronLua;
using System.Collections.Generic;
using System.IO;
using System.Text;
using 饥荒百科全书CSharp.Class.DedicatedServers.Tools;

namespace 饥荒百科全书CSharp.Class.DedicatedServers.DedicateServer
{
    /// <summary>
    /// mod的类型
    /// </summary>
    public enum ModType
    {
        客户端 = 0,
        服务端 = 1,
        所有人 = 2
    }


    /// <summary>
    /// 2016.11.28 重新写的mod类,代表每单个mod，注意两点：  
    /// 客户端不能显示
    /// 另外看看地窖这个mod是不是有毒，还会总出错吗
    /// </summary>
    class Mod
    {

        #region mod属性
        /// <summary>
        /// mod的文件夹名
        /// </summary>
        private string dirName;


        /// <summary>
        /// mod的全路径
        /// </summary>
        private string modinfoPath;

        /// <summary>
        /// mod的name名字
        /// </summary>
        private string name;

        /// <summary>
        /// mod的描述
        /// </summary>
        private string description;

        /// <summary>
        /// mod的作者
        /// </summary>
        private string author;

        /// <summary>
        /// mod的版本
        /// </summary>
        private string version;

 

        /// <summary>
        /// mod的类型
        /// </summary>
        private ModType tyype;

        /// <summary>
        /// mod的细节    <name,modxijie>
        /// </summary>
        private Dictionary<string,ModXiJie> configuration_options;

        /// <summary>
        /// 这个是否开启了
        /// </summary>
        private bool enabled;

        public string DirName
        {
            get
            {
                return dirName;
            }

            set
            {
                dirName = value;
            }
        }

        public string ModinfoPath
        {
            get
            {
                return modinfoPath;
            }

            set
            {
                modinfoPath = value;
            }
        }

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

        public string Description
        {
            get
            {
                return description;
            }

            set
            {
                description = value;
            }
        }

        public string Author
        {
            get
            {
                return author;
            }

            set
            {
                author = value;
            }
        }

        public string Version
        {
            get
            {
                return version;
            }

            set
            {
                version = value;
            }
        }

        public ModType Tyype
        {
            get
            {
                return tyype;
            }

            set
            {
                tyype = value;
            }
        }

        internal Dictionary<string, ModXiJie> Configuration_options
        {
            get
            {
                return configuration_options;
            }

            set
            {
                configuration_options = value;
            }
        }

        public bool Enabled
        {
            get
            {
                return enabled;
            }

            set
            {
                enabled = value;
            }
        }

        #endregion

        
        /// <summary>
        /// mod
        /// </summary> 
        /// <param name="modinfoPath">这个modinfo的路径</param>
        /// <param name="thisModConfig">这个mod的细节配置，可能为空，说明没有</param>
        public Mod(string modinfoPath )
        {
            #region Mod除了细节的各种信息
            // 路径
            this.ModinfoPath = modinfoPath;

            //文件夹名字

             DirectoryInfo dinfo =new DirectoryInfo(Path.GetDirectoryName(modinfoPath));
            this.DirName = dinfo.Name;

            // 读取modinfo文件,各种判断是否为空
            LuaConfig luaReader = new LuaConfig();
            LuaTable lt_modinfo= luaReader.ReadLua(modinfoPath, Encoding.UTF8, false);

            this.Name = lt_modinfo["name"] == null ? "" : lt_modinfo["name"].ToString();
            this.Description = lt_modinfo["description"] == null ? "" : lt_modinfo["description"].ToString();
            this.Author = lt_modinfo["author"] == null ? "" : lt_modinfo["author"].ToString();
            this.Version = lt_modinfo["version"] == null ? "" : lt_modinfo["version"].ToString();

            // mod类型，modType
            if (lt_modinfo["client_only_mod"] == null || (lt_modinfo["client_only_mod"].ToString().Trim().ToLower() == "false"))
            {

                if (lt_modinfo["all_clients_require_mod"] == null)
                {

                    this.Tyype= ModType.所有人;

                }
                else
                {
                    if (lt_modinfo["all_clients_require_mod"].ToString().Trim().ToLower() == "true")
                    {
                        this.Tyype = ModType.所有人;
                    }
                    else
                    {
                        this.Tyype = ModType.服务端;
                    }

                }
            }
            else
            {
                this.Tyype = ModType.客户端;
            }
     


            #endregion

            #region mod的细节
            // mod的细节 configuration_options

            Configuration_options = new Dictionary<string, ModXiJie>();

            // 如果没有细节。返回
            if (lt_modinfo["configuration_options"] == null) { return; };

            // go
            LuaTable lt_configuration_options = (LuaTable)lt_modinfo["configuration_options"];

            //    private Dictionary<string, ModXiJie> configuration_options;
            // lua下标从1开始
            for (int i = 1; i <= lt_configuration_options.Length; i++)
            {

                // 获取name的值，如果name值为空，干脆不储存，直接到下一个循环,mod中经常会有这种空的东西，不知道是作者故意的还是什么
                string name1;
                var name = ((LuaTable)lt_configuration_options[i])["name"];
                if (name == null || name.ToString().Trim() == "")
                {
                    continue;
                }
                else {

                      name1 = name.ToString();
                }

                // label的值
                string label1;
                var label= ((LuaTable)lt_configuration_options[i])["label"];

                if (label == null || label.ToString().Trim() == "")
                {
                    label1 = "";
                }
                else
                {

                    label1 = label.ToString();
                }

                // hover的值
                string hover1;
                var hover = ((LuaTable)lt_configuration_options[i])["hover"];

                if (hover == null || hover.ToString().Trim() == "")
                {
                    hover1 = "";
                }
                else
                {

                    hover1 = hover.ToString();
                }

                // default的值
                string default11;
                string current11;
                var default1 = ((LuaTable)lt_configuration_options[i])["default"];

                if (default1 == null || default1.ToString().Trim() == "")
                {
                    default11 = "";
                    current11 = "";
                }
                else
                {

                    default11 = default1.ToString();
                    current11= default1.ToString(); 

                }

                // options,每个细节的选项
                List<Option> listOptions = new List<Option>();
                var options = ((LuaTable)lt_configuration_options[i])["options"];


                if (options == null)
                {
                    listOptions = null;
                    // bug,感谢 零下依渡 
                    continue;
                }
                else
                {
                    LuaTable ltOptions = (LuaTable)options;
                   
                    // lua从1开始
                    for (int j = 1; j <= ltOptions.Length; j++)
                    {
                        Option option = new Option();
                        // 标记，这里没有判断description是否为空，绝大多数都不会出错的，除非作者瞎写。
                        option.description = ((LuaTable)ltOptions[j])["description"].ToString();
                        // 其实这个data值是有数据类型的，bool,int，string.但是这里全部都是string了，在保存到文件的时候要判断类型保存
                        option.data = ((LuaTable)ltOptions[j])["data"].ToString();

                        listOptions.Add(option);
                    } 
                        
                }

                // 判断default是否存在于data中，有的作者瞎写。。 只能判断下
                bool isDefaultIndata = false;
                for (int k = 0; k < listOptions.Count; k++)
                {

                    if (default11== listOptions[k].data)
                    {
                        isDefaultIndata = true;
                    } 
                }

                // 标记（listOptions[0]没有判断是否为空） 如果不存在，赋值第一个data的值
                if (!isDefaultIndata)
                {
                    default11 = listOptions[0].data;
                    current11= listOptions[0].data;
                }

                // 赋值到mod细节中
                ModXiJie modxijie = new ModXiJie();
                modxijie.Current = current11;
                modxijie.Default1 = default11;
                modxijie.Name = name1;
                modxijie.Label = label1;
                modxijie.Options = listOptions;

                // 添加到总的configuration_options
                Configuration_options[name1]= modxijie;


            }


            #endregion

            //#region 读取modoverrides，赋值到current值中，用current覆盖default
            //ReadModoverrides(thisModConfig);
            //#endregion

        }


        #region 读取modoverrides，赋值到current值中，用current覆盖default

        public void  ReadModoverrides(LuaTable thisModConfig) {

            if (thisModConfig != null)
            {
                this.Enabled = (bool)thisModConfig["enabled"];
            }

            // 如果为空，说明没有开启此mod，返回
            if (thisModConfig == null) { return; }

            // 储存enabled

            //// enable 为false，说明没有开启mod，返回

            //if (this.Enabled == false) { return; }


            var thisMod_configuration_options = thisModConfig["configuration_options"];
            // 如果没有细节配置，还是返回
            if (thisMod_configuration_options == null) { return; }

            // 格式转换
            LuaTable lt_thisMod_configuration_options = (LuaTable)thisMod_configuration_options;

            // 再转换成字典
            IDictionary<string, object> d = (IDictionary<string, object>)lt_thisMod_configuration_options.Members;

            foreach (KeyValuePair<string, object> item in d)
            {
                //  如果不存在，下一循环
                if (!Configuration_options.ContainsKey(item.Key))
                {
                    continue;
                }

                // 赋值到当前值,【到这里，用当前值覆盖了default，如果没有被覆盖的就是默认值】
                if (item.Value != null)
                {
                    Configuration_options[item.Key].Current = item.Value.ToString();
                }

            }

        }
            #endregion

    }
}
