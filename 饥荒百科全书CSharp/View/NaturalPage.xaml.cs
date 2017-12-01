using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Newtonsoft.Json;
using 饥荒百科全书CSharp.Class;
using 饥荒百科全书CSharp.Class.JsonDeserialize;
using 饥荒百科全书CSharp.View.Details;

namespace 饥荒百科全书CSharp.View
{
    /// <summary>
    /// NaturalPage.xaml 的交互逻辑
    /// </summary>
    public partial class NaturalPage : Page
    {
        private int _loadedTime;

        public void LoadCompleted(object sender, NavigationEventArgs e)
        {
            if (_loadedTime != 0) return;
            _loadedTime++;
            if (Global.FontFamily != null)
            {
                FontFamily = Global.FontFamily;
            }
        }

        public NaturalPage()
        {
            InitializeComponent();
            Global.NaturalLeftFrame = LeftFrame;
            Global.RightFrame.NavigationService.LoadCompleted += LoadCompleted;
            Deserialize();
        }

        public void Deserialize()
        {
            BiomesExpander.DataContext = Global.NaturalBiomesData;
            LeftFrame.NavigationService.Navigate(new NaturalDetail(), Global.NaturalBiomesData[0]);
        }

        private void NaturalButton_Click(object sender, RoutedEventArgs e)
        {
            var nature = (Nature)((Button)sender).DataContext;
            LeftFrame.NavigationService.Navigate(new NaturalDetail(), nature);
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UiSplitter.Height = ActualHeight;
        }
    }
}
