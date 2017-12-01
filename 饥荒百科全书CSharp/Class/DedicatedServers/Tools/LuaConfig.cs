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
    class LuaConfig
    {
        /// <summary>
        /// 读取lua,并返回luaTable
        /// </summary>
        /// <param name="path"></param>
        /// <param name="encoding"></param>
        /// <param name="isReturn"></param>
        /// <returns></returns>
        public LuaTable ReadLua(string path, Encoding encoding, bool isReturn)
        {

            //lua读取
            using (var lua = new Lua())
            {
                // 创建环境
                var g = lua.CreateEnvironment();
                // 读取文件
                var reader = new StreamReader(path, encoding);
                var luaResult = new LuaResult();
                try
                {
                    luaResult = g.DoChunk(reader, "test.lua");
                }
                catch (Exception)
                {
                    // ignored
                }
                reader.Close();
                // 如果有返回值
                if (isReturn)
                {
                    var luaTable = (LuaTable)luaResult[0];
                    return luaTable;
                }
                LuaTable lt = g;
                return lt;
            }
        }
    }
}
