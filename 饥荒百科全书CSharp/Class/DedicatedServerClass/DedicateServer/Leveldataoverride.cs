using Neo.IronLua;
using ServerTools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace 饥荒百科全书CSharp.Class.DedicatedServerClass.DedicateServer
{
    /// <summary>
    /// 世界类
    /// </summary>
    class Leveldataoverride
    {

        string currentDir = System.Environment.CurrentDirectory;

        //世界配置路径,是否是地下
        private string worldconfigPath;
        private bool isCave;
        private string picDirPath;


        /// <summary>
        /// 当前世界配置
        /// </summary>
        private Dictionary<string, string> configWorld = new Dictionary<string, string>();

        /// <summary>
        /// 读取的选项文件
        /// </summary>
        private Dictionary<string, List<string>> selectConfigWorld = new Dictionary<string, List<string>>();

        /// <summary>
        /// 最终的world
        /// </summary>
        private Dictionary<string, ShowWorld> showWorldDic = new Dictionary<string, ShowWorld>();


        System.Text.UTF8Encoding utf8 = new System.Text.UTF8Encoding(false);

        public Dictionary<string, string> ConfigWorld
        {
            get
            {
                return configWorld;
            }

            set
            {
                configWorld = value;
            }
        }

        public Dictionary<string, List<string>> SelectConfigWorld
        {
            get
            {
                return selectConfigWorld;
            }

            set
            {
                selectConfigWorld = value;
            }
        }


        public bool IsCave
        {
            get
            {
                return isCave;
            }

            set
            {
                isCave = value;
            }
        }

        internal Dictionary<string, ShowWorld> ShowWorldDic
        {
            get
            {
                return showWorldDic;
            }

            set
            {
                showWorldDic = value;
            }
        }

        //internal List<ShowWorld> ShowWorldList
        //{
        //    get
        //    {
        //        return showWorldList;
        //    }

        //    set
        //    {
        //        showWorldList = value;
        //    }
        //}




        /// <summary>
        /// 设置当前的世界
        /// </summary>
        /// <param name="l">key的值,就是label的值</param>
        /// <param name="n">select中的序号</param>
        public void setCurrentWorld(List<string> l, List<int> n, string path)
        {

            // 赋值道currentWorld
            for (int i = 0; i < l.Count; i++)
            {
                int nn = n[i];
                configWorld[l[i]] = World[l[i]][nn];
            }
            //   System.Text.UTF8Encoding utf8 = new System.Text.UTF8Encoding(false);
            // 保存到文件
            FileStream fs = new FileStream(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs, utf8);
            //开始写入
            StringBuilder sb = new StringBuilder();
            //   string sb = "";

            // 地上世界
            string dishangWorld = File.ReadAllText(currentDir + @"\yyServer模板\yyServer\Master\leveldataoverride.lua");
            dishangWorld = dishangWorld.Replace("\r\n", "\n").Replace("\n", "\r\n");
            Regex regex = new Regex(@".*overrides\s*=\s*{|random_set_pieces.*", RegexOptions.Singleline);
            MatchCollection mcdishang = regex.Matches(dishangWorld);
            string dishangStrQ = mcdishang[0].Value + "\r\n";
            string dishangStrH = "\r\n},\r\n" + mcdishang[1].Value + "\r\n";

            // 地下
            string caveWorld = File.ReadAllText(currentDir + @"\yyServer模板\yyServer\Caves\leveldataoverride.lua");
            caveWorld = caveWorld.Replace("\r\n", "\n").Replace("\n", "\r\n");
            Regex regex1 = new Regex(@".*overrides\s*=\s*{|required_prefabs.*", RegexOptions.Singleline);
            MatchCollection mcdixia = regex1.Matches(caveWorld);
            string caveStrQ = mcdixia[0].Value + "\r\n";
            string caveStrH = "\r\n},\r\n" + mcdixia[1].Value + "\r\n";


            if (!isCave)
            {
                sb.Append(dishangStrQ);
                // sb += dishangStrQ;
            }
            else
            {

                sb.Append(caveStrQ);
                //  sb += caveStrQ;

            }

            foreach (KeyValuePair<string, string> kvp in configWorld)
            {
                // sb+= string.Format("{0}=\"{1}\",", kvp.Key, kvp.Value);
                sb.AppendFormat("{0}=\"{1}\",", kvp.Key, kvp.Value);
                sb.Append("\r\n");
                // sb += "\r\n";
            }

            string s = sb.ToString();

            s = s.Substring(0, s.Length - 3);

            if (!isCave)
            {
                s += dishangStrH;

            }
            else
            {

                s += caveStrH;

            }

            sw.Write(s);
            //清空缓冲区
            sw.Flush();
            //关闭流
            sw.Close();
            fs.Close();

        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="worldconfigPath">世界的配置文件</param>
        /// <param name="worldselectPath">世界的选项文件</param>
        public Leveldataoverride(string worldconfigPath,string picdirpath, bool isCave)
        {
            this.worldconfigPath = worldconfigPath;
            this.isCave = isCave;
            this.picDirPath = picdirpath;

            // 初始化，就是，读取地上地下世界，放到 Dictionary<string（世界的key）,List<string>（世界的value）> 类型中，
            // 但是以后如何在里面取值赋值 

            // init(worldconfigPath, worldselectPath);
            init();
        }


        private string init()
        {
            if (String.IsNullOrEmpty(worldconfigPath))
            {
                return "世界配置文件路径不对";
            }
   

            //给【世界配置文件】初始化
            readConfigWorld();

            // 给【世界选项文件】初始化
            readSelectConfigWorld();

            // 将上面读取到的两个融到一起，到world中
            setWorld();


            return "初始化成功";
        }

        /// <summary>
        /// 赋值world
        /// </summary>
        private void setWorld()
        {
            ShowWorldDic.Clear();
            // 遍历configworld
            foreach (KeyValuePair<string, string> item in ConfigWorld)
            {

                // 如果包含，选项中有，则添加选项中的
                if (SelectConfigWorld.ContainsKey(item.Key))
                { 
                    string picPath = picDirPath + @"\" + item.Key + ".png";
                    ShowWorldDic[item.Key]= new ShowWorld(picPath, selectConfigWorld[item.Key], item.Value, item.Key);

                }
                //如果不包含，说明ServerConfig中没有写，没有配置，就用当前的值临时替代
                else
                {

                    List<string> l = new List<string>();
                    l.Add(item.Value);
                    string picPath = picDirPath + @"\" + item.Key + ".png";
                    ShowWorldDic[item.Key] = new ShowWorld(picPath,l, item.Value, item.Key);

                }
            }

        }


        /// <summary>
        /// 读取世界选项的文件,赋值到selectConfigWorld，类型
        /// </summary>
        private void readSelectConfigWorld()
        {
            // 先清空,再赋值
            selectConfigWorld.Clear();

            //读取文件,填入到字典
            List<string> listStr= XmlHelper.ReadworldSelect("ServerConfig.xml",IsCave);

            for (int i = 0; i < listStr.Count; i++)
            {
                string[] a = listStr[i].Split('=');
                List<string> b = a[1].Split(',').ToList<string>();
                selectConfigWorld.Add(a[0], b);
            }
        }

        /// <summary>
        /// 读取世界配置文件，赋值到configWorld
        /// </summary>
        private void readConfigWorld()
        {
            // 清空
            configWorld.Clear();
            // 先读文件
            LuaConfig luaconfig = new LuaConfig();
            LuaTable lt = luaconfig.readLua(worldconfigPath, Encoding.UTF8, true);


            // 初始化override世界配置
            LuaTable overridesLt = (LuaTable)lt["overrides"];

            IDictionary<string, object> worldDic = overridesLt.Members;


            List<string> l = worldDic.Keys.ToList();
            List<object> lc = worldDic.Values.ToList();


            for (int j = 0; j < worldDic.Count; j++)
            {
                configWorld.Add(l[j], lc[j].ToString());
            }

        }


    }
}
