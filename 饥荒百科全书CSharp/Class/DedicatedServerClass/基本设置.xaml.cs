using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using 饥荒百科全书CSharp.Class.DedicatedServerClass.DedicateServer;

namespace 饥荒百科全书CSharp.Class.DedicatedServerClass
{
    /// <summary>
    /// 基本设置.xaml 的交互逻辑
    /// </summary>
    public partial class 基本设置  
    {
        public 基本设置()
        {
           
            Init();
        }

        // 初始化
        public void Init() {

            // 标记：这里要获取，先这样写，路径
            string clusterIni_FilePath = @"C:\Users\yy\Documents\Klei\DoNotStarveTogether\yyServer\cluster.ini";
            if (!File.Exists(clusterIni_FilePath))
            {
                MessageBox.Show("cluster.ini文件不存在，请在【基本设置.xaml.cs】下35行设置正确的路径和basic.cs 358行。");
                return;
            }

            // 初始化基本设置
            BaseSet basic = new BaseSet(clusterIni_FilePath);

            // 给显示的数据绑定值
            //textBox_HouseName.DataContext = basic;
            //textBox_Describe.DataContext = basic;
            //textBox_LimitNumOfPerple.DataContext = basic;
            //textBox_Secret.DataContext = basic;
            //checkBox_IsCave.DataContext = basic;
            //checkBox_IsConsole.DataContext = basic;
            //checkBox_PauseNoPeople.DataContext = basic;
            //checkBox_PVP.DataContext = basic;
            //comboBox_GameMode.DataContext = basic;
            //comboBox_GameStyle.DataContext = basic;

            // 测试basic属性值是否改变
            //basic.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(basic_PropertyChanged);


        }

        //void basic_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        //{
        //    MessageBox.Show(e.PropertyName.ToString() + "改变");
        //}


    }
}
