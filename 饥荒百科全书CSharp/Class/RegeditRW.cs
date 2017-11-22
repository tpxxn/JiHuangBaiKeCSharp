using System;
using Microsoft.Win32;

namespace 饥荒百科全书CSharp.Class
{
    static class RegeditRw
    {
        /// <summary>
        /// 写入注册表(double值)
        /// </summary>
        /// <param name="valueName">注册表值</param>
        /// <param name="value">值</param>
        public static void RegWrite(string valueName, double value)
        {
            Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\JiHuangBaiKeCSharp", valueName, value, RegistryValueKind.DWord);
        }

        /// <summary>
        /// 写入注册表(string值)
        /// </summary>
        /// <param name="valueName">注册表值</param>
        /// <param name="value">值</param>
        public static void RegWrite(string valueName, string value)
        {
            Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\JiHuangBaiKeCSharp", valueName, value);

        }

        /// <summary>
        /// 读取注册表(double值)
        /// </summary>
        /// <param name="valueName">注册表值</param>
        /// <returns>值</returns>
        public static double RegRead(string valueName)
        {
            double getValue = 0;
            string getValueTemp = "";
            try
            {
                getValueTemp = Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\JiHuangBaiKeCSharp", valueName, 0).ToString();
            }
            catch 
            //(Exception e)
            {
                //MessageBox.Show(e.ToString());
            }
            if (getValueTemp != "")
            {
                getValue = double.Parse(getValueTemp);
            }
            return getValue;
        }

        /// <summary>
        /// 读取注册表(string值)
        /// </summary>
        /// <param name="valueName">注册表值</param>
        /// <returns>值</returns>
        public static string RegReadString(string valueName)
        {
            string GetValue = "";
            string GetValueTemp = "";
            try
            {
                GetValueTemp = (string)Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\JiHuangBaiKeCSharp", valueName, string.Empty);
            }
            catch { }
            if (GetValueTemp != "")
            {
                GetValue = GetValueTemp;
            }
            return GetValue;
        }

        /// <summary>
        /// 清空注册表
        /// </summary>
        public static void ClearReg()
        {
            try
            {
                var key = Registry.CurrentUser;
                key.DeleteSubKey(@"SOFTWARE\JiHuangBaiKeCSharp", true); //该方法无返回值，直接调用即可
                key.Close();
            }
            catch (Exception)
            {
                //ignore
            }
        }
    }
}
