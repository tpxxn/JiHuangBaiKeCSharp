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
        public string DirName { get; set; }

        /// <summary>
        /// mod的全路径
        /// </summary>
        public string ModinfoPath { get; set; }

        /// <summary>
        /// mod的name名字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// mod的描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// mod的作者
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// mod的版本
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// mod的类型
        /// </summary>
        public ModType Tyype { get; set; }

        /// <summary>
        /// mod的细节
        /// </summary>
        internal Dictionary<string, ModXiJie> Configuration_options { get; set; }

        /// <summary>
        /// 这个是否开启了
        /// </summary>
        public bool Enabled { get; set; }

        #endregion

        /// <summary>
        /// mod
        /// </summary> 
        /// <param name="modinfoPath">这个modinfo的路径</param>
        /// <param name="thisModConfig">这个mod的细节配置，可能为空，说明没有</param>
        public Mod(string modinfoPath)
        {
            #region Mod除了细节的各种信息
            // 路径
            this.ModinfoPath = modinfoPath;

            //文件夹名字

            var dinfo = new DirectoryInfo(Path.GetDirectoryName(modinfoPath));
            this.DirName = dinfo.Name;

            // 读取modinfo文件,各种判断是否为空
            var luaReader = new LuaConfig();
            var ltModinfo = luaReader.ReadLua(modinfoPath, Encoding.UTF8, false);

            this.Name = ltModinfo["name"]?.ToString() ?? "";
            this.Description = ltModinfo["description"]?.ToString() ?? "";
            this.Author = ltModinfo["author"]?.ToString() ?? "";
            this.Version = ltModinfo["version"]?.ToString() ?? "";

            // mod类型，modType
            if (ltModinfo["client_only_mod"] == null || (ltModinfo["client_only_mod"].ToString().Trim().ToLower() == "false"))
            {

                if (ltModinfo["all_clients_require_mod"] == null)
                {

                    Tyype = ModType.所有人;

                }
                else
                {
                    Tyype = ltModinfo["all_clients_require_mod"].ToString().Trim().ToLower() == "true" ? ModType.所有人 : ModType.服务端;
                }
            }
            else
            {
                Tyype = ModType.客户端;
            }



            #endregion

            #region mod的细节
            // mod的细节 configuration_options

            Configuration_options = new Dictionary<string, ModXiJie>();

            // 如果没有细节。返回
            if (ltModinfo["configuration_options"] == null) { return; };

            // go
            var ltConfigurationOptions = (LuaTable)ltModinfo["configuration_options"];

            //    private Dictionary<string, ModXiJie> configuration_options;
            // lua下标从1开始
            for (var i = 1; i <= ltConfigurationOptions.Length; i++)
            {

                // 获取name的值，如果name值为空，干脆不储存，直接到下一个循环,mod中经常会有这种空的东西，不知道是作者故意的还是什么
                string name1;
                var name = ((LuaTable)ltConfigurationOptions[i])["name"];
                if (name == null || name.ToString().Trim() == "")
                {
                    continue;
                }
                else
                {

                    name1 = name.ToString();
                }

                // label的值
                string label1;
                var label = ((LuaTable)ltConfigurationOptions[i])["label"];

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
                var hover = ((LuaTable)ltConfigurationOptions[i])["hover"];

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
                var default1 = ((LuaTable)ltConfigurationOptions[i])["default"];

                if (default1 == null || default1.ToString().Trim() == "")
                {
                    default11 = "";
                    current11 = "";
                }
                else
                {

                    default11 = default1.ToString();
                    current11 = default1.ToString();

                }

                // options,每个细节的选项
                var listOptions = new List<Option>();
                var options = ((LuaTable)ltConfigurationOptions[i])["options"];


                if (options == null)
                {
                    listOptions = null;
                    // bug,感谢 零下依渡 
                    continue;
                }
                else
                {
                    var ltOptions = (LuaTable)options;

                    // lua从1开始
                    for (var j = 1; j <= ltOptions.Length; j++)
                    {
                        var option = new Option
                        {
                            Description = ((LuaTable)ltOptions[j])["description"].ToString(),
                            Data = ((LuaTable)ltOptions[j])["data"].ToString()
                        };
                        // 标记，这里没有判断description是否为空，绝大多数都不会出错的，除非作者瞎写。
                        // 其实这个data值是有数据类型的，bool,int，string.但是这里全部都是string了，在保存到文件的时候要判断类型保存

                        listOptions.Add(option);
                    }

                }

                // 判断default是否存在于data中，有的作者瞎写。。 只能判断下
                var isDefaultIndata = false;
                foreach (var option in listOptions)
                {
                    if (default11 == option.Data)
                    {
                        isDefaultIndata = true;
                    }
                }

                // 标记（listOptions[0]没有判断是否为空） 如果不存在，赋值第一个data的值
                if (!isDefaultIndata)
                {
                    default11 = listOptions[0].Data;
                    current11 = listOptions[0].Data;
                }

                // 赋值到mod细节中
                var modxijie = new ModXiJie
                {
                    Current = current11,
                    Default1 = default11,
                    Name = name1,
                    Label = label1,
                    Options = listOptions
                };

                // 添加到总的configuration_options
                Configuration_options[name1] = modxijie;


            }


            #endregion

            //#region 读取modoverrides，赋值到current值中，用current覆盖default
            //ReadModoverrides(thisModConfig);
            //#endregion

        }


        #region 读取modoverrides，赋值到current值中，用current覆盖default

        public void ReadModoverrides(LuaTable thisModConfig)
        {

            if (thisModConfig != null)
            {
                this.Enabled = (bool)thisModConfig["enabled"];
            }

            // 如果为空，说明没有开启此mod，返回
            if (thisModConfig == null) { return; }

            // 储存enabled

            //// enable 为false，说明没有开启mod，返回

            //if (this.Enabled == false) { return; }


            var thisModConfigurationOptions = thisModConfig["configuration_options"];
            // 如果没有细节配置，还是返回
            if (thisModConfigurationOptions == null) { return; }

            // 格式转换
            var ltThisModConfigurationOptions = (LuaTable)thisModConfigurationOptions;

            // 再转换成字典
            var d = ltThisModConfigurationOptions.Members;

            foreach (var item in d)
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
