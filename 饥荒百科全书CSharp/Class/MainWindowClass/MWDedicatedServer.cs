using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Threading;
using 饥荒百科全书CSharp.Class;
using 饥荒百科全书CSharp.Class.DedicatedServerClass.DedicateServer;
using 饥荒百科全书CSharp.MyUserControl;

namespace 饥荒百科全书CSharp
{
    /// <summary>
    /// MainWindowFood
    /// </summary>
    public partial class MainWindow : Window
    {
  

        public void InitServer() {

            //-1.读取上一次用户选择的版本TGP or Steam

            // 0.读取各种路径信息

            // 1.检查yyServer是否存在，不存在从模板中复制一份过去

            // 2.读取【基本设置】
            string clusterIni_FilePath = @"C:\Users\yy\Documents\Klei\DoNotStarveTogether\yyServer\cluster.ini";
            if (!File.Exists(clusterIni_FilePath))
            {
                MessageBox.Show("文件不存在,请在MWDedicatedServer.cs 第31行设置正确路径.以便测试基本设置");
                return;
            }
            BaseSet baseSet = new BaseSet(clusterIni_FilePath);

            DediBaseSetGamemodeSelect.DataContext = baseSet;
            DediBaseSetPvpSelect.DataContext = baseSet;
            DediBaseSetMaxPlayerSelect.DataContext = baseSet;
            DediBaseOfflineSelect.DataContext = baseSet;
            DediBaseSetHouseName.DataContext = baseSet;
            DediBaseSetDescribe.DataContext = baseSet;
            DediBaseSetSecret.DataContext = baseSet;
            DediBaseOfflineSelect.DataContext = baseSet;
            DediBaseIsPause.DataContext = baseSet;
            DediBaseSetIntentionButton.DataContext = baseSet;
        }
    }
}
