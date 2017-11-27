using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 饥荒百科全书CSharp.Class.DedicatedServers.Tools;


namespace 饥荒百科全书CSharp.Class.DedicatedServers.Tools

{
    class PathAll
    {



        /// <summary>
        /// 我的文档路径
        /// </summary>
        private string document_DirPath;
        /// <summary>
        /// 当前路径
        /// </summary>
        private string current_DirPath;
        /// <summary>
        /// 客户端exe文件路径
        /// </summary>
        private string client_FilePath;
        /// <summary>
        /// 服务端exe文件路径
        /// </summary>
        private string server_FilePath;
        /// <summary>
        /// yyServer路径
        /// </summary>
        private string yyServer_DirPath;
        /// <summary>
        /// 地上世界—设置文件-路径
        /// </summary>
        private string overworld_config_FilePath;
        /// <summary>
        /// 地下世界-设置文件-路径
        /// </summary>
        private string cave_config_FilePath;
 
        /// <summary>
        /// mod设置-地上世界-文件路径
        /// </summary>
        private string mod_config_overworld_FilePath;
        /// <summary>
        /// mod设置-地下世界-文件路径
        /// </summary>
        private string mod_config_cave_FilePath;
        /// <summary>
        /// DoNotStarveTogether
        /// </summary>
        private string doNotStarveTogether_DirPath;
        /// <summary>
        /// 客户端mods路径
        /// </summary>
        private string clientMods_DirPath;
        /// <summary>
        /// 服务端mods路径
        /// </summary>
        private string serverMods_DirPath;


        private string pic_DirPath;



    
        /// <summary>
        /// 我的文档
        /// </summary>
        public string Document_DirPath
        {
            get
            {
                return document_DirPath;
            }
            set
            {
                document_DirPath = value;
            }

        }

        /// <summary>
        /// 当前路径
        /// </summary>
        public string Current_DirPath
        {
            get
            {
                return current_DirPath;
            }
            set
            {
                current_DirPath = value;
              

            }


        }



        /// <summary>
        /// 客户端exe文件路径
        /// </summary>
        public string Client_FilePath
        {
            get
            {
                return client_FilePath;
            }

            set
            {

                if (value == null || value == "")
                {
                    client_FilePath = null;
                    ClientMods_DirPath = null;
                    XmlHelper.WriteClientPath(  "", GamePingTai);
                    return;
                }

                client_FilePath = value.Trim();
                XmlHelper.WriteClientPath( client_FilePath, GamePingTai);

                // 客户端mods路径 
                if (!String.IsNullOrEmpty(client_FilePath))
                {

                    ClientMods_DirPath = Path.GetDirectoryName(Path.GetDirectoryName(client_FilePath)) + @"\mods";
                    

                }
            }
        }

        /// <summary>
        /// 服务器exe文件路径
        /// </summary>
        public string Server_FilePath
        {
            get
            {
                return server_FilePath;
            }

            set
            {
                if (value == null || value == "")
                {
                    server_FilePath = null;
                    ServerMods_DirPath = null;
                    XmlHelper.WriteServerPath(  "", GamePingTai);

                    return;
                }
                // 判断文件名对不对
                if (value.Contains("dontstarve_dedicated_server_nullrenderer.exe"))
                {
                    server_FilePath = value.Trim();
                    XmlHelper.WriteServerPath(  server_FilePath, GamePingTai);


                }

                // 服务端mods路径 
                if (!String.IsNullOrEmpty(server_FilePath))
                {

                    ServerMods_DirPath = Path.GetDirectoryName(Path.GetDirectoryName(server_FilePath)) + @"\mods";

                }
            }
        }

        /// <summary>
        /// yyServer路径
        /// </summary>
        public string YyServer_DirPath
        {
            get
            {
                return yyServer_DirPath;
            }
            set
            {

                yyServer_DirPath = value;
                if (!String.IsNullOrEmpty(yyServer_DirPath))
                {
                    Mod_config_overworld_FilePath = yyServer_DirPath + "\\Master\\modoverrides.lua";
                    Mod_config_cave_FilePath = yyServer_DirPath + "\\Caves\\modoverrides.lua";
                    Overworld_config_FilePath = yyServer_DirPath + "\\Master\\leveldataoverride.lua";
                    Cave_config_FilePath = yyServer_DirPath + "\\Caves\\leveldataoverride.lua";
                }

            }


        }

        /// <summary>
        /// 地上世界-配置-路径
        /// </summary>
        public string Overworld_config_FilePath
        {
            get
            {
                return overworld_config_FilePath;
            }
            set
            {

                overworld_config_FilePath = value;
            }

        }

        /// <summary>
        /// 地下世界-配置-路径
        /// </summary>
        public string Cave_config_FilePath
        {
            get
            {
                return cave_config_FilePath;
            }
            set
            {

                cave_config_FilePath = value;
            }

        }
 

  

        /// <summary>
        /// mod-设置-地上
        /// </summary>
        public string Mod_config_overworld_FilePath
        {
            get
            {
                return mod_config_overworld_FilePath;
            }
            set
            {

                mod_config_overworld_FilePath = value;
            }

        }

        /// <summary>
        /// mod-设置-地下
        /// </summary>
        public string Mod_config_cave_FilePath
        {
            get
            {
                return mod_config_cave_FilePath;
            }
            set
            {

                mod_config_cave_FilePath = value;
            }
        }

        /// <summary>
        /// DoNotStarveTogether
        /// </summary>
        public string DoNotStarveTogether_DirPath
        {
            get
            {
                return doNotStarveTogether_DirPath;
            }
            set
            {

                doNotStarveTogether_DirPath = value;
                if (!String.IsNullOrEmpty(doNotStarveTogether_DirPath))
                {
                    YyServer_DirPath = doNotStarveTogether_DirPath + @"\Server_" + GamePingTai+"_" + CunDangCao;
                }

            }
        }

        /// <summary>
        /// 客户端mods路径
        /// </summary>
        public string ClientMods_DirPath
        {
            get
            {
                return clientMods_DirPath;
            }
            set
            {
                clientMods_DirPath = value;

            }


        }

        /// <summary>
        /// 服务器mods路径
        /// </summary>
        public string ServerMods_DirPath
        {
            get
            {
                return serverMods_DirPath;
            }
            set
            {

                serverMods_DirPath = value;
            }

        }

 

        public string GamePingTai { get; set; }
        private int cunDangCao;
        public int CunDangCao {

            get
            {
                return cunDangCao;
            }

            set
            {
                cunDangCao = value;
                YyServer_DirPath = doNotStarveTogether_DirPath + @"\Server_" + GamePingTai + "_" + value;

            }
        }

 

        public string Pic_DirPath
        {
            get
            {
                return pic_DirPath;
            }

            set
            {
                pic_DirPath = value;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="GamePingTai">游戏平台</param>
        /// <param name="CunDangCao">存档槽</param>
        public PathAll(string GamePingTai, int CunDangCao)
        {

            this.GamePingTai = GamePingTai;
            this.CunDangCao = CunDangCao;
            setAllPath(GamePingTai, CunDangCao);

        }

        /// <summary>
        /// 设置所有路径
        /// </summary>
        /// <param name="GamePingTai">游戏平台</param>
        /// <param name="CunDangChao">第几个存档槽,从0开始</param>
        public void setAllPath(string GamePingTai, int CunDangCao = 0)
        {

            this.GamePingTai = GamePingTai;
            this.CunDangCao = CunDangCao;

            // 我的文档
            Document_DirPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);

            // 当前路径
            Current_DirPath = System.Environment.CurrentDirectory;

            // 世界图片路径
             Pic_DirPath= Current_DirPath + "\\世界图片";

            // DoNotStarveTogether
            if (GamePingTai.ToLower() == "tgp")
            {
                DoNotStarveTogether_DirPath = Document_DirPath + @"\Klei\DoNotStarveTogetherRail";

            }
            if (GamePingTai.ToLower() == "youxia" || GamePingTai.ToLower() == "steam")
            {
                DoNotStarveTogether_DirPath = Document_DirPath + @"\Klei\DoNotStarveTogether";
            }

            // 客户端服务器路径
           Client_FilePath=  XmlHelper.ReadClientPath(  GamePingTai);
           Server_FilePath=   XmlHelper.ReadServerPath(  GamePingTai);

        }

    }
}
