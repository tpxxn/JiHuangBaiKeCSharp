using Neo.IronLua;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using 饥荒百科全书CSharp.Class.DedicatedServers.Tools;

namespace 饥荒百科全书CSharp.Class.DedicatedServers.DedicateServer
{
    /// <summary>
    /// 世界类
    /// </summary>
    class Leveldataoverride
    {
        private readonly UTF8Encoding _utf8 = new UTF8Encoding(false);

        #region 字段 
        private readonly PathAll _pathall;
        private readonly bool _isCave;

        #endregion

        #region (public)构造,保存,showWorlddic


        /// <summary>
        /// 最终的world
        /// </summary>
        public Dictionary<string, ShowWorld> ShowWorldDic { get; set; } = new Dictionary<string, ShowWorld>();

        /// <summary>
        /// 保存
        /// </summary>
        public void SaveWorld()
        {

            if (!Directory.Exists(_pathall.YyServerDirPath))
            {
                return;
            }
            //   System.Text.UTF8Encoding utf8 = new System.Text.UTF8Encoding(false);
            // 保存到文件
            string savePath;
            if (!_isCave)
            {
                savePath = _pathall.YyServerDirPath + @"\Master\leveldataoverride.lua";
            }
            else
            {
                savePath = _pathall.YyServerDirPath + @"\Caves\leveldataoverride.lua";

            }

            var fs = new FileStream(savePath, FileMode.Create);
            var sw = new StreamWriter(fs, _utf8);
            //开始写入
            var sb = new StringBuilder();
            //   string sb = "";

            // 地上世界
            // 读取模板中地上世界配置的前半部分和后半部分dishangStrQ，dishangStrH，用于拼接字符串，保存用
            var dishangWorld = Tool.ReadResources("Server模板.Master.leveldataoverride.lua");

            dishangWorld = dishangWorld.Replace("\r\n", "\n").Replace("\n", "\r\n");
            var regex = new Regex(@".*overrides\s*=\s*{|random_set_pieces.*", RegexOptions.Singleline);
            var mcdishang = regex.Matches(dishangWorld);
            var dishangStrQ = mcdishang[0].Value + "\r\n";
            var dishangStrH = "\r\n},\r\n" + mcdishang[1].Value + "\r\n";

            // 地下
            var caveWorld = Tool.ReadResources("Server模板.Caves.leveldataoverride.lua");
            caveWorld = caveWorld.Replace("\r\n", "\n").Replace("\n", "\r\n");
            var regex1 = new Regex(@".*overrides\s*=\s*{|required_prefabs.*", RegexOptions.Singleline);
            var mcdixia = regex1.Matches(caveWorld);
            var caveStrQ = mcdixia[0].Value + "\r\n";
            var caveStrH = "\r\n},\r\n" + mcdixia[1].Value + "\r\n";


            if (!_isCave)
            {
                sb.Append(dishangStrQ);
                // sb += dishangStrQ;
            }
            else
            {

                sb.Append(caveStrQ);
                //  sb += caveStrQ;

            }


            foreach (var kvp in ShowWorldDic)
            {
                // sb+= string.Format("{0}=\"{1}\",", kvp.Key, kvp.Value);
                sb.AppendFormat("{0}=\"{1}\",", kvp.Key, kvp.Value.Worldconfig);
                sb.Append("\r\n");
                // sb += "\r\n";
            }

            var s = sb.ToString();

            s = s.Substring(0, s.Length - 3);

            if (!_isCave)
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
            this._pathall = pathall;
            this._isCave = isCave;


            // 初始化，就是，读取地上地下世界，放到 Dictionary<string（世界的key）,List<string>（世界的value）> 类型中，
            // 但是以后如何在里面取值赋值 

            // init(worldconfigPath, worldselectPath);
            Init();
        }

        #endregion

        #region 其他
        /// <summary>
        /// 读取的选项文件
        /// </summary>
        private readonly Dictionary<string, List<string>> _selectConfigWorld = new Dictionary<string, List<string>>();

        private string Init()
        {
            if (string.IsNullOrEmpty(_pathall.CaveConfigFilePath))
            {
                return "世界配置文件路径不对";
            }
            //给【世界配置文件】初始化
            ReadConfigWorld();
            // 给【世界选项文件】初始化
            ReadSelectConfigWorld();
            // 将上面读取到的两个融到一起，到world中
            SetWorld();
            return "初始化成功";
        }

        /// <summary>
        /// 赋值world
        /// </summary>
        private void SetWorld()
        {
            ShowWorldDic.Clear();
            var configWorld = ReadConfigWorld();

            // 遍历configworld
            foreach (var item in configWorld)
            {

                var picPath = @"Resources/DedicatedServer/World/" + item.Key + ".png";

                // 如果包含，选项中有，则添加选项中的
                if (_selectConfigWorld.ContainsKey(item.Key))
                {

                    //string picPath = pathall.Pic_DirPath + @"\" + item.Key + ".png";
                    ShowWorldDic[item.Key] = new ShowWorld(picPath, _selectConfigWorld[item.Key], item.Value, item.Key);

                }
                //如果不包含，说明ServerConfig中没有写，没有配置，就用当前的值临时替代
                else
                {

                    var l = new List<string> { item.Value };
                    //string picPath = pathall.Pic_DirPath + @"\" + item.Key + ".png";
                    ShowWorldDic[item.Key] = new ShowWorld(picPath, l, item.Value, item.Key);

                }
            }

        }


        /// <summary>
        /// 读取世界选项的文件,赋值到selectConfigWorld，类型
        /// </summary>
        private void ReadSelectConfigWorld()
        {
            // 先清空,再赋值
            _selectConfigWorld.Clear();

            //读取文件,填入到字典
            var listStr = XmlHelper.ReadWorldSelect(_isCave);

            foreach (var str in listStr)
            {
                var a = str.Split('=');
                var b = a[1].Split(',').ToList();
                _selectConfigWorld.Add(a[0], b);
            }
        }

        /// <summary>
        /// 读取世界配置文件，赋值到configWorld
        /// </summary>
        private Dictionary<string, string> ReadConfigWorld()
        {
            // 清空
            var configWorld = new Dictionary<string, string>();
            // 先读文件
            var luaconfig = new LuaConfig();
            var lt = luaconfig.ReadLua(_isCave ? _pathall.CaveConfigFilePath : _pathall.OverworldConfigFilePath, Encoding.UTF8, true);


            // 初始化override世界配置
            var overridesLt = (LuaTable)lt["overrides"];

            var worldDic = overridesLt.Members;


            var l = worldDic.Keys.ToList();
            var lc = worldDic.Values.ToList();


            for (var j = 0; j < worldDic.Count; j++)
            {
                configWorld.Add(l[j], lc[j].ToString());
            }

            return configWorld;

        }

        #endregion


    }
}
