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
        System.Text.UTF8Encoding utf8 = new System.Text.UTF8Encoding(false);

        #region 字段 
        private PathAll pathall;
        private bool isCave;
        private Dictionary<string, ShowWorld> showWorldDic = new Dictionary<string, ShowWorld>();
        #endregion

        #region (public)构造,保存,showWorlddic


        /// <summary>
        /// 最终的world
        /// </summary>
        public Dictionary<string, ShowWorld> ShowWorldDic
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
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="l">key的值,就是label的值</param>
        /// <param name="n">select中的序号</param>
        public void SaveWorld()
        {

            if (!Directory.Exists(pathall.YyServer_DirPath))
            {
                return;
            }
            //   System.Text.UTF8Encoding utf8 = new System.Text.UTF8Encoding(false);
            // 保存到文件
            string savePath;
            if (!isCave)
            {
                savePath = pathall.YyServer_DirPath + @"\Master\leveldataoverride.lua";
            }
            else
            {
                savePath = pathall.YyServer_DirPath + @"\Caves\leveldataoverride.lua";

            }

            FileStream fs = new FileStream(savePath, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs, utf8);
            //开始写入
            StringBuilder sb = new StringBuilder();
            //   string sb = "";

            // 地上世界
            string dishangWorld = File.ReadAllText(pathall.ServerMoBanPath + @"\Master\leveldataoverride.lua");
            dishangWorld = dishangWorld.Replace("\r\n", "\n").Replace("\n", "\r\n");
            Regex regex = new Regex(@".*overrides\s*=\s*{|random_set_pieces.*", RegexOptions.Singleline);
            MatchCollection mcdishang = regex.Matches(dishangWorld);
            string dishangStrQ = mcdishang[0].Value + "\r\n";
            string dishangStrH = "\r\n},\r\n" + mcdishang[1].Value + "\r\n";

            // 地下
            string caveWorld = File.ReadAllText(pathall.ServerMoBanPath + @"\Caves\leveldataoverride.lua");
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


            foreach (KeyValuePair<string, ShowWorld> kvp in ShowWorldDic)
            {
                // sb+= string.Format("{0}=\"{1}\",", kvp.Key, kvp.Value);
                sb.AppendFormat("{0}=\"{1}\",", kvp.Key, kvp.Value.Worldconfig);
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
        public Leveldataoverride(PathAll pathall, bool isCave)
        {
            this.pathall = pathall;
            this.isCave = isCave;


            // 初始化，就是，读取地上地下世界，放到 Dictionary<string（世界的key）,List<string>（世界的value）> 类型中，
            // 但是以后如何在里面取值赋值 

            // init(worldconfigPath, worldselectPath);
            init();
        }

        #endregion

        #region 其他
        /// <summary>
        /// 读取的选项文件
        /// </summary>
        private Dictionary<string, List<string>> selectConfigWorld = new Dictionary<string, List<string>>();



        private string init()
        {


            if (String.IsNullOrEmpty(pathall.Cave_config_FilePath))
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
            Dictionary<string, string> configWorld = readConfigWorld();

            // 遍历configworld
            foreach (KeyValuePair<string, string> item in configWorld)
            {

                string picPath = @"Resources/DedicatedServer/World/" + item.Key + ".png";

                // 如果包含，选项中有，则添加选项中的
                if (selectConfigWorld.ContainsKey(item.Key))
                {
                  
                    //string picPath = pathall.Pic_DirPath + @"\" + item.Key + ".png";
                    ShowWorldDic[item.Key]= new ShowWorld(picPath, selectConfigWorld[item.Key], item.Value, item.Key);

                }
                //如果不包含，说明ServerConfig中没有写，没有配置，就用当前的值临时替代
                else
                {

                    List<string> l = new List<string>();
                    l.Add(item.Value);
                    //string picPath = pathall.Pic_DirPath + @"\" + item.Key + ".png";
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
            List<string> listStr= XmlHelper.ReadWorldSelect("ServerConfig.xml",isCave);

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
        private Dictionary<string,string> readConfigWorld()
        {
            // 清空
            Dictionary<string, string> configWorld = new Dictionary<string, string>();
            // 先读文件
            LuaConfig luaconfig = new LuaConfig();
            LuaTable lt = luaconfig.readLua(isCave ? pathall.Cave_config_FilePath:pathall.Overworld_config_FilePath, Encoding.UTF8, true);


            // 初始化override世界配置
            LuaTable overridesLt = (LuaTable)lt["overrides"];

            IDictionary<string, object> worldDic = overridesLt.Members;


            List<string> l = worldDic.Keys.ToList();
            List<object> lc = worldDic.Values.ToList();


            for (int j = 0; j < worldDic.Count; j++)
            {
                configWorld.Add(l[j], lc[j].ToString());
            }

            return configWorld;

        }

        #endregion


    }
}
