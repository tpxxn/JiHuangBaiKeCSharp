using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using JiHuangUWP.Model;
using JiHuangUWP.ViewModel;
using lindexi.uwp.Framework.ViewModel;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace JiHuangUWP.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    [ViewModel(ViewModel = typeof(DstidModel))]
    public sealed partial class DstitPage : Page
    {
        public DstitPage()
        {
            this.InitializeComponent();
            View = (DstidModel) DataContext;
        }



        private DstidModel View
        {
            set;
            get;
        }


        private void Text_OnKeyDown(object sender, KeyRoutedEventArgs e)
        {
            var temp = sender as TextBox;
            if (temp == null)
            {
                return;
            }
            if (e.Key == VirtualKey.Enter)
            {
                View.DebugStr = temp.Text;
                View.Screen();
            }
        }
    }

}
