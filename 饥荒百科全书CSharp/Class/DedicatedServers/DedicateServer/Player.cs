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
        private string name;
        private string ip;
        private string port;
        private string numOfId;
        private string id;

        public Player(string name, string ip, string port, string numOfId, string id)
        {
            this.name = name;
            this.ip = ip;
            this.port = port;
            this.numOfId = numOfId;
            this.id = id;
        }
        /// <summary>
        /// 玩家名字
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }
        /// <summary>
        /// 玩家ip
        /// </summary>
        public string Ip
        {
            get
            {
                return ip;
            }

            set
            {
                ip = value;
            }
        }
        /// <summary>
        /// 玩家端口
        /// </summary>
        public string Port
        {
            get
            {
                return port;
            }

            set
            {
                port = value;
            }
        }
        /// <summary>
        /// 玩家数字ID  OU_XX KU_XX
        /// </summary>
        public string NumOfId
        {
            get
            {
                return numOfId;
            }

            set
            {
                numOfId = value;
            }
        }
        /// <summary>
        /// 玩家ID
        /// </summary>
        public string Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }
    }
}
