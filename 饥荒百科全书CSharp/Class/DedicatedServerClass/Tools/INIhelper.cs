using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerTools
{
    /// <summary>
    /// ini文件读写类
    /// </summary>
    class INIhelper
    {
        Dictionary<string, Dictionary<string, string>> d;
        string[] filecontext;
        string path;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path">ini文件路径</param>
        public INIhelper(string path, Encoding encoding)
        {
            this.path = path;
            if (File.Exists(path))
            {
                filecontext = File.ReadAllLines(path, encoding);
            }
              d = new Dictionary<string, Dictionary<string, string>>();

            Dictionary<string, string> f = new Dictionary<string, string>();
            string title = String.Empty;
            for (int i = 0; i < filecontext.Length; i++)
            {

                string line = filecontext[i].Trim();
             
                //如果以[] 开头结尾,其实用正则更好
                if (line.StartsWith("[") && line.EndsWith("]"))
                {
                    // 添加到d
                    if (title != String.Empty)
                    {
                        d.Add(title, f);
                        f = new Dictionary<string, string>();
                        title = String.Empty;
                    }

                    // 截取[] 中间
                    title = line.Substring(1, line.Length - 2);
                }

                //如果包含=号
                if (filecontext[i].Trim().Contains("="))
                {
                    string[] s = line.Split('=');
                    f.Add(s[0].Trim(), s[1].Trim());
                }

                //如果读到最后一行,添加进去
                if (i==filecontext.Length-1)
                {
                    d.Add(title, f);
                    f = new Dictionary<string, string>();
                    title = String.Empty;
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
            if (d!=null)
            {

                return d[section][key];
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
           
            d[section][key] = value;

             
        }

        /// <summary>
        /// 写入到文件
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void write(string section, string key, string value,Encoding encoding) {

            d[section][key] = value;
            File.WriteAllLines(path, getListStr(), encoding);

        }


        public List<string>  getListStr() {

            List<string> listStr = new List<string>();
            foreach (KeyValuePair<string, Dictionary<string, string>> kvp in d)
            {

                listStr.Add("[" + kvp.Key + "]");

                foreach (KeyValuePair<string, string> kvp1 in kvp.Value)
                {
                    listStr.Add(kvp1.Key + "=" + kvp1.Value);



                }

            }

            return listStr;
        }







    }
}
