using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 饥荒百科全书CSharp.Class
{
    static class ADRD
    {
        public static string[] DelRepeatData(string[] a)
        {
            string[] b = a.GroupBy(p => p).Select(p => p.Key).ToArray();
            if (b.Length == 1)
            {
                List<string> temp = new List<string>();
                temp.Add(b[0]);
                temp.Add("");
                b = temp.ToArray();
            }
            return b;
        }
    }
}
