using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Windows.Forms;

namespace 饥荒百科全书CSharp
{
    public class RegeditReadWrite
    {
        public void RegWrite(string ValueName, double Value)
        {
            Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\JiHuangBaiKeCSharp", ValueName, Value, RegistryValueKind.DWord);
        }

        public void RegWrite(string ValueName, string Value)
        {
            Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\JiHuangBaiKeCSharp", ValueName, Value);

        }

        public double RegRead(string ValueName)
        {
            double GetValue = 0;
            string GetValueTemp = "";
            try
            {
                GetValueTemp = Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\JiHuangBaiKeCSharp", ValueName, 0).ToString();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            if (GetValueTemp != "")
            {
                GetValue = double.Parse(GetValueTemp);
            }
            return GetValue;
        }

        public string RegReadString(string ValueName)
        {
            string GetValue = "";
            string GetValueTemp = "";
            try
            {
                GetValueTemp = (string)Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\JiHuangBaiKeCSharp", ValueName, string.Empty);
            }
            catch { }
            if (GetValueTemp != "")
            {
                GetValue = GetValueTemp;
            }
            return GetValue;
        }
    }
}
