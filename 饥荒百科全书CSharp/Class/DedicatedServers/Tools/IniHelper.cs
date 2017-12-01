using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace 饥荒百科全书CSharp.Class.DedicatedServers.Tools
{
    /// <summary>
    /// ini文件读写类
    /// </summary>
    public class IniHelper
    {
        public readonly Dictionary<string, Dictionary<string, string>> Dictionary;
        public readonly string[] Filecontext;
        public readonly string Path;

        /// <summary>
        /// IniHelper
        /// </summary>
        /// <param name="path">ini文件路径</param>
        /// <param name="encoding"></param>
        public IniHelper(string path, Encoding encoding)
        {
            this.Path = path;
            if (File.Exists(path))
            {
                Filecontext = File.ReadAllLines(path, encoding);
            }
            Dictionary = new Dictionary<string, Dictionary<string, string>>();

            var f = new Dictionary<string, string>();
            var title = string.Empty;
            for (var i = 0; i < Filecontext.Length; i++)
            {
                var line = Filecontext[i].Trim();
                //如果以[] 开头结尾,其实用正则更好
                if (line.StartsWith("[") && line.EndsWith("]"))
                {
                    // 添加到d
                    if (title != string.Empty)
                    {
                        Dictionary.Add(title, f);
                        f = new Dictionary<string, string>();
                        title = string.Empty;
                    }
                    // 截取[] 中间
                    title = line.Substring(1, line.Length - 2);
                }
                //如果包含=号
                if (Filecontext[i].Trim().Contains("="))
                {
                    var s = line.Split('=');
                    f.Add(s[0].Trim(), s[1].Trim());
                }
                //如果读到最后一行,添加进去
                if (i == Filecontext.Length - 1)
                {
                    Dictionary.Add(title, f);
                    f = new Dictionary<string, string>();
                    title = string.Empty;
                }
            }
        }

        /// <summary>
        /// 读取
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <returns>value值</returns>
        public string ReadValue(string section, string key)
        {
            if (Dictionary != null)
            {
                return Dictionary[section][key];
            }
            return "";
        }

        /// <summary>
        /// 写入到list
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void WriteToList(string section, string key, string value)
        {
            Dictionary[section][key] = value;
        }

        /// <summary>
        /// 写入到文件
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Write(string section, string key, string value, Encoding encoding)
        {

            Dictionary[section][key] = value;
            File.WriteAllLines(Path, GetListStr(), encoding);

        }

        public List<string> GetListStr()
        {
            var listStr = new List<string>();
            foreach (var kvp in Dictionary)
            {
                listStr.Add("[" + kvp.Key + "]");
                listStr.AddRange(kvp.Value.Select(kvp1 => kvp1.Key + "=" + kvp1.Value));
            }
            return listStr;
        }
    }
}
