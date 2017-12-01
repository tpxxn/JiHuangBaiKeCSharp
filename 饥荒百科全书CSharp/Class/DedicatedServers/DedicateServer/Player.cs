using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 饥荒百科全书CSharp.Class.DedicatedServers.DedicateServer
{
    /// <summary>
    /// 玩家类
    /// </summary>
    class Player
    {
        //server_log_2016-11-27-13-01-40
        public Player(string name, string ip, string port, string numOfId, string id)
        {
            Name = name;
            Ip = ip;
            Port = port;
            NumOfId = numOfId;
            Id = id;
        }

        /// <summary>
        /// 玩家名字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 玩家ip
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// 玩家端口
        /// </summary>
        public string Port { get; set; }

        /// <summary>
        /// 玩家数字ID  OU_XX KU_XX
        /// </summary>
        public string NumOfId { get; set; }

        /// <summary>
        /// 玩家ID
        /// </summary>
        public string Id { get; set; }
    }
}
