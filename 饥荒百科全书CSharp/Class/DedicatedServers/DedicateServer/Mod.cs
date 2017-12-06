using System;
using Neo.IronLua;
using System.Collections.Generic;
using System.IO;
using System.Text;
using 饥荒百科全书CSharp.Class.DedicatedServers.Tools;

namespace 饥荒百科全书CSharp.Class.DedicatedServers.DedicateServer
{
    /// <summary>
    /// mod的类型
    /// </summary>
    public enum ModType
    {
        客户端 = 0,
        服务端 = 1,
        所有人 = 2
    }

    /// <summary>
    /// 2016.11.28 重新写的mod类,代表每单个mod，注意两点：  
    /// 客户端不能显示
    /// 另外看看地窖这个mod是不是有毒，还会总出错吗
    /// </summary>
    internal class Mod
    {

        #region mod属性
        /// <summary>
        /// mod的文件夹名
        /// </summary>
        public string DirName { get; set; }

        /// <summary>
        /// mod的全路径
        /// </summary>
        public string ModinfoPath { get; set; }

        /// <summary>
        /// mod的name名字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// mod的描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// mod的作者
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// mod的版本
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// mod的类型
        /// </summary>
        public ModType ModType { get; set; }

        /// <summary>
        /// mod的设置
        /// </summary>
        internal Dictionary<string, ModSetting> ConfigurationOptions { get; set; }

        /// <summary>
        /// Mod是否启用
        /// </summary>
        public bool Enabled { get; set; }
        #endregion

        /// <summary>
        /// Mod构造事件
        /// </summary> 
        /// <param name="modinfoPath">这个modinfo的路径</param>
        public Mod(string modinfoPath)
        {
            #region Mod除了设置的各种信息
            // 路径
            ModinfoPath = modinfoPath;
            //文件夹名字
            var directoryInfo = new DirectoryInfo(Path.GetDirectoryName(modinfoPath) ?? throw new InvalidOperationException());
            DirName = directoryInfo.Name;
            // 读取modinfo文件,各种判断是否为空
            var modInfoLuaTable = LuaConfig.ReadLua(modinfoPath, Encoding.UTF8, false);
            Name = modInfoLuaTable["name"]?.ToString() ?? "";
            Description = modInfoLuaTable["description"]?.ToString() ?? "";
            Author = modInfoLuaTable["author"]?.ToString() ?? "";
            Version = modInfoLuaTable["version"]?.ToString() ?? "";
            // mod类型，modType
            if (modInfoLuaTable["client_only_mod"] == null || modInfoLuaTable["client_only_mod"].ToString().Trim().ToLower() == "false")
            {
                if (modInfoLuaTable["all_clients_require_mod"] == null)
                {
                    ModType = ModType.所有人;
                }
                else
                {
                    ModType = modInfoLuaTable["all_clients_require_mod"].ToString().Trim().ToLower() == "true" ? ModType.所有人 : ModType.服务端;
                }
            }
            else
            {
                ModType = ModType.客户端;
            }
            #endregion
            #region mod的设置
            // mod的设置 configuration_options
            ConfigurationOptions = new Dictionary<string, ModSetting>();
            // 如果没有设置。返回
            if (modInfoLuaTable["configuration_options"] == null) { return; }
            var configurationOptionsLuaTable = (LuaTable)modInfoLuaTable["configuration_options"];
            //    private Dictionary<string, ModSetting> configuration_options;
            // lua下标从1开始
            for (var i = 1; i <= configurationOptionsLuaTable.Length; i++)
            {
                // 获取name的值，如果name值为空，干脆不储存，直接到下一个循环,mod中经常会有这种空的东西，不知道是作者故意的还是什么
                var nameLuaTable = ((LuaTable)configurationOptionsLuaTable[i])["name"];
                if (nameLuaTable == null || nameLuaTable.ToString().Trim() == "")
                {
                    continue;
                }
                var modName = nameLuaTable.ToString();
                // Label的值
                string modLabel;
                var label = ((LuaTable)configurationOptionsLuaTable[i])["label"];

                if (label == null || label.ToString().Trim() == "")
                {
                    modLabel = "";
                }
                else
                {
                    modLabel = label.ToString();
                }
                // Hover的值
                string modHover;
                var hover = ((LuaTable)configurationOptionsLuaTable[i])["hover"];
                if (hover == null || hover.ToString().Trim() == "")
                {
                    modHover = "";
                }
                else
                {
                    modHover = hover.ToString();
                }
                // Default的值
                string modDefault;
                string modCurrent;
                var defaultValue = ((LuaTable)configurationOptionsLuaTable[i])["default"];
                if (defaultValue == null || defaultValue.ToString().Trim() == "")
                {
                    modDefault = "";
                    modCurrent = "";
                }
                else
                {
                    modDefault = defaultValue.ToString();
                    modCurrent = defaultValue.ToString();
                }
                // Options,每个设置的选项
                var optionsList = new List<Option>();
                var options = ((LuaTable)configurationOptionsLuaTable[i])["options"];
                if (options == null)
                {
                    optionsList = null;
                }
                else
                {
                    var optionsLuaTable = (LuaTable)options;
                    // lua从1开始
                    for (var j = 1; j <= optionsLuaTable.Length; j++)
                    {
                        var option = new Option
                        {
                            Description = ((LuaTable)optionsLuaTable[j])["description"].ToString(),
                            Data = ((LuaTable)optionsLuaTable[j])["data"].ToString()
                        };
                        // 标记，这里没有判断description是否为空，绝大多数都不会出错的，除非作者瞎写。
                        // 其实这个data值是有数据类型的，bool,int，string.但是这里全部都是string了，在保存到文件的时候要判断类型保存
                        optionsList.Add(option);
                    }
                }
                // 判断default是否存在于data中，有的作者瞎写。。 只能判断下
                var isDefaultIndata = false;
                // ReSharper disable once PossibleNullReferenceException
                foreach (var option in optionsList)
                {
                    if (modDefault == option.Data)
                    {
                        isDefaultIndata = true;
                    }
                }
                // 标记（listOptions[0]没有判断是否为空） 如果不存在，赋值第一个data的值
                if (!isDefaultIndata)
                {
                    modDefault = optionsList[0].Data;
                    modCurrent = optionsList[0].Data;
                }
                // 赋值到mod设置中
                var modSetting = new ModSetting
                {
                    Name = modName,
                    Label = modLabel,
                    Hover = modHover,
                    Current = modCurrent,
                    Default1 = modDefault,
                    Options = optionsList
                };
                // 添加到总的configuration_options
                ConfigurationOptions[modName] = modSetting;
            }
            #endregion
            //#region 读取modoverrides，赋值到current值中，用current覆盖default
            //ReadModoverrides(thisModConfig);
            //#endregion
        }

        /// <summary>
        /// 读取modoverrides，赋值到current值中，用current覆盖default
        /// </summary>
        /// <param name="modConfig"></param>
        public void ReadModoverrides(LuaTable modConfig)
        {
            // 如果为空，说明没有开启此mod，返回
            if (modConfig == null) return;
            Enabled = (bool)modConfig["enabled"];
            // 储存enabled
            //// enable 为false，说明没有开启mod，返回
            //if (Enabled == false) { return; }
            var modConfigurationOptions = modConfig["configuration_options"];
            // 如果没有设置配置，还是返回
            if (modConfigurationOptions == null) return;
            // 格式转换
            var iDictionary = ((LuaTable)modConfigurationOptions).Members;
            foreach (var item in iDictionary)
            {
                //  如果不存在，下一循环
                if (!ConfigurationOptions.ContainsKey(item.Key))
                {
                    continue;
                }
                // 赋值到当前值,【到这里，用当前值覆盖了default，如果没有被覆盖的就是默认值】
                if (item.Value != null)
                {
                    ConfigurationOptions[item.Key].Current = item.Value.ToString();
                }
            }
        }
    }
}
