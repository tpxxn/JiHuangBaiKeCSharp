using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace 饥荒百科全书CSharp.Class.DedicatedServers.DedicateServer
{
    /// <summary>
    /// Server_log.txt 服务器信息类
    /// </summary>
    class Server
    {
        /// <summary>
        ///  服务器版本
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 服务器Session
        /// </summary>
        public string Session { get; set; }

        /// <summary>
        /// 服务器创建时间
        /// </summary>
        public string CreateTime { get; set; }

        /// <summary>
        /// 玩家列表
        /// </summary>
        internal List<Player> ListPlayer { get; set; } = new List<Player>();

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
                var str = File.ReadAllLines(path);

                var ip = "";
                var name = "";
                var port = "";
                var id = "";
                var numOfId = "";

                foreach (var server in str)
                {
                    // 服务器版本
                    if (server.Contains(": Version: "))
                    {
                        Version = server.Substring(server.Trim().Length - 6);
                    }
                    // Session
                    if (server.Contains(": Begin Session:"))
                    {
                        Session = server.Substring(server.Trim().Length - 16);
                    }
                    // 服务器创建时间(英语)
                    if (server.Contains(": Current time:"))
                    {
                        CreateTime = server.Replace("time:", "$").Split('$')[1];
                    }
                    // 玩家ip
                    if (server.Contains(": New incoming connection"))
                    {
                        const string regexStr = @"\d+\.\d+\.\d+\.\d+";
                        ip = Regex.Match(server, regexStr).Value;
                    }
                    // 玩家端口
                    if (server.Contains(": New incoming connection"))
                    {

                        port = server.Split('|')[1].Trim().Split(' ')[0];
                    }
                    // name id
                    if (server.Contains(": Client authenticated:"))
                    {

                        id = server.Split('(', ')')[1].Trim();
                        name = server.Split('(', ')')[2].Trim();
                    }
                    // numOfId
                    if (server.Contains(": [Steam] Authenticated "))
                    {
                        const string regexStr = @"'\d+'";
                        var ff = Regex.Match(server, regexStr).Value;
                        numOfId = ff.Substring(1, ff.Length - 2);
                    }
                    if (ip != "" && name != "" && port != "" && id != "" && numOfId != "")
                    {
                        var p = new Player(name, ip, port, numOfId, id);
                        var isHave = false;
                        foreach (var player in ListPlayer)
                        {
                            if (player.Id == id)
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
                // ignored
            }
        }
    }
}
