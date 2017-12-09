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
    internal class Leveldataoverride
    {
        #region 字段和属性
        private readonly UTF8Encoding _utf8 = new UTF8Encoding(false);
        private readonly PathAll _pathall;
        private readonly bool _isCave;

        /// <summary>
        /// 读取的选项文件
        /// </summary>
        private readonly Dictionary<string, List<string>> _selectConfigWorld = new Dictionary<string, List<string>>();

        /// <summary>
        /// 最终的world
        /// </summary>
        public Dictionary<string, ShowWorld> FinalWorldDictionary { get; set; } = new Dictionary<string, ShowWorld>();
        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="pathall">所有路径</param>
        /// <param name="isCave">是否为洞穴</param>
        public Leveldataoverride(PathAll pathall, bool isCave)
        {
            _pathall = pathall;
            _isCave = isCave;
            // 初始化，就是，读取地上地下世界，放到 Dictionary<string（世界的key）,List<string>（世界的value）> 类型中，
            // 但是以后如何在里面取值赋值
            var result = Init();
        }

        // 初始化
        private string Init()
        {
            if (string.IsNullOrEmpty(_pathall.CaveConfigFilePath))
            {
                return "世界配置文件路径不对";
            }
            // 给【世界选项文件】初始化
            ReadSelectConfigWorld();
            // 将上面读取到的两个融到一起，到world中
            SetWorld();
            return "初始化成功";
        }

        /// <summary>
        /// 读取世界选项的文件,赋值到selectConfigWorld，类型
        /// </summary>
        private void ReadSelectConfigWorld()
        {
            // 先清空,再赋值
            _selectConfigWorld.Clear();
            //读取文件,填入到字典
            var listStr = JsonHelper.ReadWorldSelect(_isCave);
            foreach (var str in listStr)
            {
                // 分类和分类中的所有选项
                var keyAndValueStrings = str.Split('=');
                // 所有选项
                var valueList = keyAndValueStrings[1].Split(',').ToList();
                _selectConfigWorld.Add(keyAndValueStrings[0], valueList);
            }
        }

        /// <summary>
        /// 赋值world
        /// </summary>
        private void SetWorld()
        {
            FinalWorldDictionary.Clear();
            var configWorld = ReadConfigWorld();
            // 遍历configworld
            foreach (var item in _selectConfigWorld)
            {
                var picturePath = @"Resources/DedicatedServer/World/" + item.Key + ".png";
                // 如果包含，选项中有，则添加选项中的
                if (configWorld.ContainsKey(item.Key))
                {
                    FinalWorldDictionary[item.Key] = new ShowWorld(picturePath, item.Value, configWorld[item.Key], item.Key);
                }
                //如果不包含，说明ServerConfig中没有写，没有配置，就用当前的值临时替代
                //else
                //{
                //    var valueList = new List<string> { configWorld[item.Key] };
                //    FinalWorldDictionary[item.Key] = new ShowWorld(picturePath, valueList, configWorld[item.Key], item.Key);
                //}
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
            var luaTable = LuaConfig.ReadLua(_isCave ? _pathall.CaveConfigFilePath : _pathall.OverworldConfigFilePath, Encoding.UTF8, true);
            // 初始化override世界配置
            var worldDictionary = ((LuaTable)luaTable["overrides"]).Members;
            var keyList = worldDictionary.Keys.ToList();
            var valuesList = worldDictionary.Values.ToList();
            for (var i = 0; i < worldDictionary.Count; i++)
            {
                configWorld.Add(keyList[i], valuesList[i].ToString());
            }
            return configWorld;
        }

        /// <summary>
        /// 保存
        /// </summary>
        public void SaveWorld()
        {
            if (!Directory.Exists(_pathall.ServerDirPath))
            {
                return;
            }
            // 保存到文件
            string savePath;
            if (!_isCave)
            {
                savePath = _pathall.ServerDirPath + @"\Master\leveldataoverride.lua";
            }
            else
            {
                savePath = _pathall.ServerDirPath + @"\Caves\leveldataoverride.lua";

            }
            var fileStream = new FileStream(savePath, FileMode.Create);
            var streamWriter = new StreamWriter(fileStream, _utf8);
            // 开始写入
            var stringBuilder = new StringBuilder();
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
            stringBuilder.Append(!_isCave ? dishangStrQ : caveStrQ);
            foreach (var kvp in FinalWorldDictionary)
            {
                // stringBuilder += string.Format("{0}=\"{1}\",", kvp.Key, kvp.Value);
                stringBuilder.AppendFormat("{0}=\"{1}\",", kvp.Key, kvp.Value.WorldConfig);
                stringBuilder.Append("\r\n");
                // stringBuilder += "\r\n";
            }
            var str = stringBuilder.ToString();
            str = str.Substring(0, str.Length - 3);
            if (!_isCave)
            {
                str += dishangStrH;
            }
            else
            {
                str += caveStrH;
            }
            streamWriter.Write(str);
            //清空缓冲区
            streamWriter.Flush();
            //关闭流
            streamWriter.Close();
            fileStream.Close();
        }

    }
}
