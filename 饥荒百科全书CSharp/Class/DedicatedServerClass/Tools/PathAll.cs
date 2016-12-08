using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerTools

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
        /// 地上世界-选择文件-路径
        /// </summary>
        private string overworld_select_FilePath;
        /// <summary>
        /// 地下世界-选择文件-路径
        /// </summary>
        private string cave_select_FilePath;
        /// <summary>
        /// 汉化文件路径
        /// </summary>
        private string hanhuaPath;
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
        /// <summary>
        /// 世界资源分类路径
        /// </summary>
        private string worldFenLei_FilePath;


        /// <summary>
        /// 我的配置文件夹，所有配置都放在这里了
        /// </summary>
        private string myConfig_DirPath;

        /// <summary>
        /// 我的文档
        /// </summary>
        public string Document_DirPath
        {
            get
            {
                return document_DirPath;
            }
            set {
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
            set {
                current_DirPath = value;
                MyConfig_DirPath = current_DirPath + @"\config";
                
            }

  
        }
        /// <summary>
        /// 我的配置文件路径
        /// </summary>
        public string MyConfig_DirPath
        {
            get
            {
                return myConfig_DirPath;
            }

            set
            {
                myConfig_DirPath = value;

                //地上世界地下世界选项
                Overworld_select_FilePath = myConfig_DirPath + "\\地上世界选项.lua";

                Cave_select_FilePath = myConfig_DirPath + "\\地下世界选项.lua";


                // 汉化
                HanhuaPath = myConfig_DirPath + "\\汉化.lua";

               
                // 客户端exe路径,服务端exe路径
                string[] k = File.ReadAllLines(myConfig_DirPath + "\\客户端路径.lua", Encoding.UTF8);
                if (k.Length > 0)
                {
                 
                        Client_FilePath = k[0].Trim();
                   

                }

                string[] f = File.ReadAllLines(myConfig_DirPath + "\\服务器路径.lua", Encoding.UTF8);
                if (f.Length > 0)
                {
                    // 判断文件名对不对
                    if (f[0].Trim().Contains("dontstarve_dedicated_server_nullrenderer.exe"))
                    {
                        Server_FilePath = f[0].Trim();
                    }

                }

                // 世界资源分类
                WorldFenLei_FilePath = myConfig_DirPath + "\\世界资源分类.lua";

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

                if (value==null || value=="")
                {
                    client_FilePath = null;
                    ClientMods_DirPath = null;
                    return;
                }
               
                    client_FilePath = value.Trim();
                

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
                    return;
                }
                // 判断文件名对不对
                if (value.Contains("dontstarve_dedicated_server_nullrenderer.exe"))
                {
                    server_FilePath = value.Trim();
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
            set {

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
            set {

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
            set {

                cave_config_FilePath = value;
            }
 
        }

        /// <summary>
        /// 地上世界-选项-路径
        /// </summary>
        public string Overworld_select_FilePath
        {
            get
            {
                return overworld_select_FilePath;
            }
            set {

                overworld_select_FilePath = value;
            }

        }

        /// <summary>
        /// 地下世界-选项-路径
        /// </summary>
        public string Cave_select_FilePath
        {
            get
            {
                return cave_select_FilePath;
            }
            set {
                cave_select_FilePath = value;
            }
 
        }

        /// <summary>
        /// 汉化-路径
        /// </summary>
        public string HanhuaPath
        {
            get
            {
                return hanhuaPath;
            }
            set {
                hanhuaPath = value;
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
            set {

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
            set {

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
            set {

                doNotStarveTogether_DirPath = value;
                if (!String.IsNullOrEmpty(doNotStarveTogether_DirPath))
                {
                    YyServer_DirPath = doNotStarveTogether_DirPath + "\\yyServer";
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
            set {

                serverMods_DirPath = value;
            }

        }

        public string WorldFenLei_FilePath
        {
            get
            {
                return worldFenLei_FilePath;
            }

            set
            {
                worldFenLei_FilePath = value;
            }
        }

        public PathAll() {

            //设置这些路径
            setAllPath();

        }



        ///// <summary>
        ///// 设置客户端exe路径
        ///// </summary>
        ///// <param name="path"></param>
        //public void setClient_FilePath(string path) {


        //    client_FilePath = path.Trim();
            

        //    // 客户端mods路径 
        //    if (!String.IsNullOrEmpty(client_FilePath.Trim()))
        //    {

        //        clientMods_DirPath = Path.GetDirectoryName(client_FilePath);

        //    }
        //}

        ///// <summary>
        ///// 设置服务端exe路径
        ///// </summary>
        ///// <param name="path"></param>
        //public void setServer_FilePath(string path)
        //{


        //    server_FilePath = path.Trim();


        //    // 服务端mods路径 
        //    if (!String.IsNullOrEmpty(server_FilePath.Trim()))
        //    {

        //        serverMods_DirPath = Path.GetDirectoryName(server_FilePath);

        //    }
        //}


        /// <summary>
        /// 初始化设置所有路径的值
        /// </summary>
        public void setAllPath()
        {

            // 我的文档
            Document_DirPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);

            // 当前路径
            Current_DirPath = System.Environment.CurrentDirectory;

 
            // DoNotStarveTogether
            DoNotStarveTogether_DirPath = Document_DirPath + @"\Klei\DoNotStarveTogether";
       
  
        }

    }
}
