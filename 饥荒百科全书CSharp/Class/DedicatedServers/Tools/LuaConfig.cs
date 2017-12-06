using Neo.IronLua;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 饥荒百科全书CSharp.Class.DedicatedServers.Tools
{

    /// <summary>
    /// lua的设置文件
    /// </summary>
    public static class LuaConfig
    {
        /// <summary>
        /// 读取lua,并返回luaTable
        /// </summary>
        /// <param name="path"></param>
        /// <param name="encoding"></param>
        /// <param name="isReturn"></param>
        /// <returns></returns>
        public static LuaTable ReadLua(string path, Encoding encoding, bool isReturn)
        {
            //lua读取
            using (var lua = new Lua())
            {
                // 创建环境
                var luaGlobalPortable = lua.CreateEnvironment();
                // 读取文件
                var streamReader = new StreamReader(path, encoding);
                var luaResult = new LuaResult();
                try
                {
                    luaResult = luaGlobalPortable.DoChunk(streamReader, "test.lua");
                }
                catch (Exception)
                {
                    // ignored
                }
                streamReader.Close();
                // 如果有返回值
                if (isReturn)
                {
                    var luaTable = (LuaTable)luaResult[0];
                    return luaTable;
                }
                else
                {
                    LuaTable luaTable = luaGlobalPortable;
                    return luaTable;
                }
            }
        }
    }
}
