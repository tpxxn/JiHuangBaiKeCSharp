using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace 饥荒百科全书CSharp.Class.DedicatedServerClass.DedicateServer
{
    /// <summary>
    /// Server_log.txt 服务器信息类
    /// </summary>
    class Server
    {
     
        private string version;
        private string session;
        private string createTime;
        List<Player> listPlayer = new List<Player>();
        /// <summary>
        ///  服务器版本
        /// </summary>
        public string Version
        {
            get
            {
                return version;
            }

            set
            {
                version = value;
            }
        }
        /// <summary>
        /// 服务器Session
        /// </summary>
        public string Session
        {
            get
            {
                return session;
            }

            set
            {
                session = value;
            }
        }
        /// <summary>
        /// 服务器创建时间
        /// </summary>
        public string CreateTime
        {
            get
            {
                return createTime;
            }

            set
            {
                createTime = value;
            }
        }
        /// <summary>
        /// 玩家列表
        /// </summary>
        internal List<Player> ListPlayer
        {
            get
            {
                return listPlayer;
            }

            set
            {
                listPlayer = value;
            }
        }

        /// <summary>
        /// 获取玩家
        /// </summary>
        /// <returns></returns>
        public void GetPlayers(string path)
        {
            ListPlayer.Clear();
            try
            {
                // 读文件
                string[] str = File.ReadAllLines(path);

                string ip = "";
                string name = "";
                string port = "";
                string id = "";
                string numOfId = "";

                for (int i = 0; i < str.Length; i++)
                {


                    // 服务器版本
                    if (str[i].Contains(": Version: "))
                    {
                        Version = str[i].Substring(str[i].Trim().Length - 6);
                    }
                    // Session
                    if (str[i].Contains(": Begin Session:"))
                    {
                        Session = str[i].Substring(str[i].Trim().Length - 16);
                    }
                    // 服务器创建时间(英语)
                    if (str[i].Contains(": Current time:"))
                    {
                        CreateTime = str[i].Replace("time:", "$").Split('$')[1];
                    }
                    // 玩家ip
                    if (str[i].Contains(": New incoming connection"))
                    {
                        string RegexStr = @"\d+\.\d+\.\d+\.\d+";
                        ip = Regex.Match(str[i], RegexStr).Value;
                    }
                    // 玩家端口
                    if (str[i].Contains(": New incoming connection"))
                    {

                        port = str[i].Split('|')[1].Trim().Split(' ')[0];
                    }
                    // name id
                    if (str[i].Contains(": Client authenticated:"))
                    {

                        id = str[i].Split('(', ')')[1].Trim();
                        name = str[i].Split('(', ')')[2].Trim();
                    }
                    // numOfId
                    if (str[i].Contains(": [Steam] Authenticated "))
                    {
                        string RegexStr = @"'\d+'";
                        string ff = Regex.Match(str[i], RegexStr).Value;
                        numOfId = ff.Substring(1, ff.Length - 2);
                    }
                    if (ip != "" && name != "" && port != "" && id != "" && numOfId != "")
                    {
                        Player p = new Player(name, ip, port, numOfId, id);
                        bool isHave = false;
                        for (int j = 0; j < ListPlayer.Count; j++)
                        {
                            if (ListPlayer[j].Id == id)
                            {
                                isHave = true;
                            }

                        }

                        if (isHave == false)
                        {
                            ListPlayer.Add(p);
                        }

                        ip = "";
                        name = "";
                        port = "";
                        id = "";
                        numOfId = "";

                    }


                }
                ip = "";
                name = "";
                port = "";
                id = "";
                numOfId = "";
            }
            catch (Exception)
            {


            }

             
        }


    }
}
