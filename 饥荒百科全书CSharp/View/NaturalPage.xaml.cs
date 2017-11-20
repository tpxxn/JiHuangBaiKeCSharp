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
using 饥荒百科全书CSharp.View.Details;

namespace 饥荒百科全书CSharp.View
{
    /// <summary>
    /// NaturalPage.xaml 的交互逻辑
    /// </summary>
    public partial class NaturalPage : Page
    {
        private readonly ObservableCollection<Nature> _naturalBiomesData = new ObservableCollection<Nature>();

        public NaturalPage()
        {
            InitializeComponent();
            Global.NaturalLeftFrame = LeftFrame;
            Deserialize();
        }

        public void Deserialize()
        {
            _naturalBiomesData.Clear();
            var natural = JsonConvert.DeserializeObject<NaturalRootObject>(StringProcess.GetJsonString("Natural.json"));
            foreach (var natureBiomesItems in natural.Biomes.Nature)
            {
                _naturalBiomesData.Add(natureBiomesItems);
            }
            foreach (var natureBiomesItems in _naturalBiomesData)
            {
                natureBiomesItems.Picture = StringProcess.GetGameResourcePath(natureBiomesItems.Picture);
            }
            BiomesExpander.DataContext = _naturalBiomesData;
            LeftFrame.NavigationService.Navigate(new NaturalDetail(), _naturalBiomesData[0]);
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
