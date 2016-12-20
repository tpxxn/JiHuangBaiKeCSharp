using Neo.IronLua;
using ServerTools;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace 饥荒百科全书CSharp.Class.DedicatedServerClass.DedicateServer
{
    /// <summary>
    /// 所有mod集合到一起
    /// </summary>
    class Mods
    {
        UTF8Encoding utf8NoBom = new UTF8Encoding(false);

        #region 【属性】 modsPath modoverridePath listMod
        private string modsPath;
        private string modoverridePath;
        private List<Mod> listMod;

        public string Mods_dir_Path
        {
            get
            {
                return modsPath;
            }

            set
            {
                modsPath = value;
            }
        }

        public string Modoverride_file_Path
        {
            get
            {
                return modoverridePath;
            }

            set
            {
                modoverridePath = value;
            }
        }

        internal List<Mod> ListMod
        {
            get
            {
                return listMod;
            }

            set
            {
                listMod = value;
            }
        }

        #endregion


        #region 【初始化】的那个
        /// <summary>
        /// 读取所有的mod，放到listMod中
        /// </summary>
        /// <param name="mods_dir_Path">mods路径</param>
        /// <param name="modoverrides_file_Path">mod配置文件路径</param>
        public Mods(string mods_dir_Path)
        {

            // 赋值
            this.Mods_dir_Path = mods_dir_Path;
            ListMod = new List<Mod>();

            // 遍历modsPath中每一个文件modinfo.lua文件
            DirectoryInfo dinfo = new DirectoryInfo(mods_dir_Path);

            // 标记：这里要保证mods文件夹下全部都是mod的文件夹，不能有其他的文件夹，不然后面可能会出错
            DirectoryInfo[] strdir = dinfo.GetDirectories();

            // strdir长度为0的时候，没有mod的时候，listmod为null吧？
            for (int i = 0; i < strdir.Length; i++)
            {

                // modinfo的路径
                string modinfoPath = strdir[i].FullName + "\\modinfo.lua";

                // 这个mod的配置lt1，可以为空，后面有判断
                //LuaTable lt1 = lt[strdir[i].Name] == null ? null : (LuaTable)lt[strdir[i].Name];

                // 创建mod
                Mod mod = new Mod(modinfoPath);

                // 添加
                ListMod.Add(mod);


            }


        }

      

        public void ReadModsOverrides(string mods_dir_Path,string modoverrides_file_Path) {

            this.Modoverride_file_Path = modoverrides_file_Path;
            LuaConfig luaconfig = new LuaConfig();
            LuaTable lt = luaconfig.ReadLua(modoverrides_file_Path, Encoding.UTF8, true);

            // 遍历modsPath中每一个文件modinfo.lua文件
            DirectoryInfo dinfo = new DirectoryInfo(mods_dir_Path);

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
        public void saveListmodsToFile(string ToFilePath,Encoding encoding)
        {


            // 开始拼接字符串
            StringBuilder sb = new StringBuilder();

            // 循环获取
            for (int i = 0; i < ListMod.Count; i++)
            {
                // mod的文件夹名字
                string dirName = ListMod[i].DirName;

                // mod是否开启
                bool enabled = ListMod[i].Enabled;
                // 如果mod没有开启，则不拼接不写入文件。
                if (!enabled) { continue; }

               
           

                // mod的细节拼接,存在细节才会拼接,if判断
                string xijie = "";
                if (ListMod[i].Configuration_options.Count != 0)
                {

                    StringBuilder sbXijie = new StringBuilder();
                    Dictionary<string, ModXiJie> dic = ListMod[i].Configuration_options;
                    foreach (KeyValuePair<string, ModXiJie> item in dic)
                    {
                        sbXijie.AppendFormat("{0} = {1},", key类型(item.Key), value类型(item.Value.Current));

                    }
                    xijie = "configuration_options={{" + sbXijie.ToString() + "}},";
                }


                // 大的拼接
                sb.AppendFormat("[\"{0}\"] = {{" + xijie + "enabled = {1} }},\r\n", dirName, enabled.ToString().ToLower());


            }
            // 拼接成最后的文件，创建，保存： modoverrides.lua
            File.WriteAllText(ToFilePath, "return {\r\n" + sb.ToString() + "\r\n}",encoding);

         


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

            return "\""+s+"\"";

        }
        /// <summary>
        /// 按照格式来保存，必须转换
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private string key类型(string s)
        {

            //if (s == "" || s.Contains(" ") || s.Contains("_"))
            //{
            //    return "[\"" + s + "\"]";
            //}

            //如果包含非英文
            if (Regex.IsMatch(s, "[^a-zA-Z0-9]")||s == "" )
            {
                return "[\"" + s + "\"]";
            }


             return s;

        }


        #endregion


    }
}
