using Neo.IronLua;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using 饥荒百科全书CSharp.Class.DedicatedServers.Tools;

namespace 饥荒百科全书CSharp.Class.DedicatedServers.DedicateServer
{
    /// <summary>
    /// 所有mod集合到一起
    /// </summary>
    class Mods
    {
        UTF8Encoding _utf8NoBom = new UTF8Encoding(false);

        #region 【属性】 modsPath modoverridePath listMod

        public string ModsDirPath { get; set; }

        public string ModoverrideFilePath { get; set; }

        internal List<Mod> ListMod { get; set; }

        #endregion


        #region 【初始化】的那个
        /// <summary>
        /// 读取所有的mod，放到listMod中
        /// </summary>
        /// <param name="modsDirPath">mods路径</param>
        public Mods(string modsDirPath)
        {

            // 赋值
            this.ModsDirPath = modsDirPath;
            ListMod = new List<Mod>();

            // 遍历modsPath中每一个文件modinfo.lua文件
            var dinfo = new DirectoryInfo(modsDirPath);

            // 标记：这里要保证mods文件夹下全部都是mod的文件夹，不能有其他的文件夹，不然后面可能会出错
            var strdir = dinfo.GetDirectories();

            // strdir长度为0的时候，没有mod的时候，listmod为null吧？
            foreach (var directoryInfo in strdir)
            {
                // modinfo的路径
                var modinfoPath = directoryInfo.FullName + "\\modinfo.lua";

                // 这个mod的配置lt1，可以为空，后面有判断
                //LuaTable lt1 = lt[strdir[i].Name] == null ? null : (LuaTable)lt[strdir[i].Name];

                // 创建mod
                var mod = new Mod(modinfoPath);

                // 添加
                ListMod.Add(mod);
            }
        }

        public void ReadModsOverrides(string modsDirPath, string modoverridesFilePath)
        {

            this.ModoverrideFilePath = modoverridesFilePath;
            LuaConfig luaconfig = new LuaConfig();
            LuaTable lt = luaconfig.ReadLua(modoverridesFilePath, Encoding.UTF8, true);

            // 遍历modsPath中每一个文件modinfo.lua文件
            DirectoryInfo dinfo = new DirectoryInfo(modsDirPath);

            // 标记：这里要保证mods文件夹下全部都是mod的文件夹，不能有其他的文件夹，不然后面可能会出错
            DirectoryInfo[] strdir = dinfo.GetDirectories();

            // strdir长度为0的时候，没有mod的时候，listmod为null吧？

            for (int i = 0; i < strdir.Length; i++)
            {
                // 这个mod的配置lt1，可以为空，后面有判断
                LuaTable lt1 = lt[strdir[i].Name] == null ? null : (LuaTable)lt[strdir[i].Name];
                // 标记..
                ListMod[i].ReadModoverrides(lt1);
            }
        }

        #endregion

        #region 【保存到文件】 把listmods保存到文件,保存的时候注意格式

        // 把listmods保存到文件,保存的时候注意格式
        public void SaveListmodsToFile(string toFilePath, Encoding encoding)
        {
            // 开始拼接字符串
            var stringBuilder = new StringBuilder();
            // 循环获取
            foreach (var mod in ListMod)
            {
                // mod的文件夹名字
                var dirName = mod.DirName;
                // mod是否开启
                var enabled = mod.Enabled;
                // 如果mod没有开启，则不拼接不写入文件。
                if (!enabled) { continue; }
                // mod的细节拼接,存在细节才会拼接,if判断
                var xijie = "";
                if (mod.Configuration_options.Count != 0)
                {

                    var sbXijie = new StringBuilder();
                    var dic = mod.Configuration_options;
                    foreach (var item in dic)
                    {
                        sbXijie.AppendFormat("{0} = {1},", Key类型(item.Key), value类型(item.Value.Current));

                    }
                    xijie = "configuration_options={{" + sbXijie + "}},";
                }
                // 大的拼接
                stringBuilder.AppendFormat("[\"{0}\"] = {{" + xijie + "enabled = {1} }},\r\n", dirName, enabled.ToString().ToLower());
            }
            // 拼接成最后的文件，创建，保存： modoverrides.lua
            File.WriteAllText(toFilePath, "return {\r\n" + stringBuilder + "\r\n}", encoding);
        }


        /// <summary>
        /// 按照格式来保存，必须转换
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private string value类型(string s)
        {

            // 判断是不是bool类型
            bool resultbool;
            if (bool.TryParse(s, out resultbool))
            {
                return s.ToLower();
            }

            // 判断是不是数字类型
            double resultdouble;
            if (double.TryParse(s, out resultdouble))
            {
                return s;
            }

            return "\"" + s + "\"";

        }
        /// <summary>
        /// 按照格式来保存，必须转换
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private static string Key类型(string s)
        {

            //if (s == "" || s.Contains(" ") || s.Contains("_"))
            //{
            //    return "[\"" + s + "\"]";
            //}

            //如果包含非英文
            if (Regex.IsMatch(s, "[^a-zA-Z0-9]") || s == "")
            {
                return "[\"" + s + "\"]";
            }


            return s;

        }


        #endregion


    }
}
