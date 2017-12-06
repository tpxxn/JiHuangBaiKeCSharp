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

        #region 字段和属性
        private UTF8Encoding _utf8NoBom = new UTF8Encoding(false);

        /// <summary>
        /// Mod目录路径
        /// </summary>
        public string ModsDirPath { get; set; }

        public string ModoverrideFilePath { get; set; }

        /// <summary>
        /// Mod列表
        /// </summary>
        internal List<Mod> ListMod { get; set; }
        #endregion


        #region 【初始化】的那个
        /// <summary>
        /// 读取所有的mod，放到listMod中
        /// </summary>
        /// <param name="modsDirPath">mods路径</param>
        public Mods(string modsDirPath)
        {
            ModsDirPath = modsDirPath;
            ListMod = new List<Mod>();
            // 遍历modsPath中每一个文件modinfo.lua文件
            var directoryInfo = new DirectoryInfo(modsDirPath);
            // TODO：这里要保证mods文件夹下全部都是mod的文件夹，不能有其他的文件夹，不然后面可能会出错
            var directoryInfos = directoryInfo.GetDirectories();
            foreach (var directoryInfoInDirectoryInfos in directoryInfos)
            {
                // modinfo的路径
                var modinfoPath = directoryInfoInDirectoryInfos.FullName + "\\modinfo.lua";
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
            ModoverrideFilePath = modoverridesFilePath;
            var serverluaTable = LuaConfig.ReadLua(modoverridesFilePath, Encoding.UTF8, true);
            // 遍历modsPath中每一个文件modinfo.lua文件
            var directoryInfo = new DirectoryInfo(modsDirPath);
            // TODO：这里要保证mods文件夹下全部都是mod的文件夹，不能有其他的文件夹，不然后面可能会出错
            var directoryInfos = directoryInfo.GetDirectories();
            for (var i = 0; i < directoryInfos.Length; i++)
            {
                // 这个mod的配置luaTable，可以为空，后面有判断
                var luaTable = serverluaTable[directoryInfos[i].Name] == null ? null : (LuaTable)serverluaTable[directoryInfos[i].Name];
                // 读取modoverrides，赋值到current值中，用current覆盖default
                ListMod[i].ReadModoverrides(luaTable);
            }
        }

        #endregion

        #region 【保存到文件】 把listmods保存到文件,保存的时候注意格式

        /// <summary>
        /// 把listmods保存到文件,保存的时候注意格式
        /// </summary>
        /// <param name="toFilePath">文件路径</param>
        /// <param name="encoding">编码</param>
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
                if (!enabled) continue;
                // mod的设置拼接,存在设置才会拼接,if判断
                var setting = "";
                if (mod.ConfigurationOptions.Count != 0)
                {

                    var stringBuilderXijie = new StringBuilder();
                    var dic = mod.ConfigurationOptions;
                    foreach (var item in dic)
                    {
                        stringBuilderXijie.AppendFormat("{0} = {1},", KeyType(item.Key), ValueType(item.Value.Current));

                    }
                    setting = "configuration_options={{" + stringBuilderXijie + "}},";
                }
                // 大的拼接
                stringBuilder.AppendFormat("[\"{0}\"] = {{" + setting + "enabled = {1} }},\r\n", dirName, "true");
            }
            // 拼接成最后的文件，创建，保存： modoverrides.lua
            File.WriteAllText(toFilePath, "return {\r\n" + stringBuilder + "\r\n}", encoding);
        }

        /// <summary>
        /// 按照格式来保存，必须转换
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private static string ValueType(string s)
        {
            // 判断是不是bool类型
            if (bool.TryParse(s, out _))
            {
                return s.ToLower();
            }
            // 判断是不是数字类型
            if (double.TryParse(s, out _))
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
        private static string KeyType(string s)
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
