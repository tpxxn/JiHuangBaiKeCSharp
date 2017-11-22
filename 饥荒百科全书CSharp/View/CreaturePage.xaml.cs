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
    /// CreaturePage.xaml 的交互逻辑
    /// </summary>
    public partial class CreaturePage : Page
    {
        private readonly ObservableCollection<Creature> _creatureLandData = new ObservableCollection<Creature>();
        private readonly ObservableCollection<Creature> _creatureOceanData = new ObservableCollection<Creature>();
        private readonly ObservableCollection<Creature> _creatureFlyData = new ObservableCollection<Creature>();
        private readonly ObservableCollection<Creature> _creatureCaveData = new ObservableCollection<Creature>();
        private readonly ObservableCollection<Creature> _creatureEvilData = new ObservableCollection<Creature>();
        private readonly ObservableCollection<Creature> _creatureOthersData = new ObservableCollection<Creature>();
        private readonly ObservableCollection<Creature> _creatureBossData = new ObservableCollection<Creature>();

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

        public CreaturePage()
        {
            InitializeComponent();
            Global.CreatureLeftFrame = LeftFrame;
            Global.RightFrame.NavigationService.LoadCompleted += LoadCompleted;
            if (Global.GameVersion != 4)
            {
                OceanExpander.Visibility = Visibility.Collapsed;
            }
            else
            {
                CaveExpander.Visibility = Visibility.Collapsed;
            }
            Deserialize();
        }

        public void Deserialize()
        {
            _creatureLandData.Clear();
            _creatureOceanData.Clear();
            _creatureFlyData.Clear();
            _creatureCaveData.Clear();
            _creatureEvilData.Clear();
            _creatureOthersData.Clear();
            _creatureBossData.Clear();
            var creature = JsonConvert.DeserializeObject<CreaturesRootObject>(StringProcess.GetJsonString("Creatures.json"));
            foreach (var creatureLandItems in creature.Land.Creature)
            {
                _creatureLandData.Add(creatureLandItems);
            }
            foreach (var creatureLandItems in _creatureLandData)
            {
                creatureLandItems.Picture = StringProcess.GetGameResourcePath(creatureLandItems.Picture);
            }
            foreach (var creatureOceanItems in creature.Ocean.Creature)
            {
                _creatureOceanData.Add(creatureOceanItems);
            }
            foreach (var creatureOceanItems in _creatureOceanData)
            {
                creatureOceanItems.Picture = StringProcess.GetGameResourcePath(creatureOceanItems.Picture);
            }
            foreach (var creatureFlyItems in creature.Fly.Creature)
            {
                _creatureFlyData.Add(creatureFlyItems);
            }
            foreach (var creatureFlyItems in _creatureFlyData)
            {
                creatureFlyItems.Picture = StringProcess.GetGameResourcePath(creatureFlyItems.Picture);
            }
            foreach (var creatureCaveItems in creature.Cave.Creature)
            {
                _creatureCaveData.Add(creatureCaveItems);
            }
            foreach (var creatureCaveItems in _creatureCaveData)
            {
                creatureCaveItems.Picture = StringProcess.GetGameResourcePath(creatureCaveItems.Picture);
            }
            foreach (var creatureEvilItems in creature.Evil.Creature)
            {
                _creatureEvilData.Add(creatureEvilItems);
            }
            foreach (var creatureEvilItems in _creatureEvilData)
            {
                creatureEvilItems.Picture = StringProcess.GetGameResourcePath(creatureEvilItems.Picture);
            }
            foreach (var creatureOthersItems in creature.Others.Creature)
            {
                _creatureOthersData.Add(creatureOthersItems);
            }
            foreach (var creatureOthersItems in _creatureOthersData)
            {
                creatureOthersItems.Picture = StringProcess.GetGameResourcePath(creatureOthersItems.Picture);
            }
            foreach (var creatureBossItems in creature.Boss.Creature)
            {
                _creatureBossData.Add(creatureBossItems);
            }
            foreach (var creatureBossItems in _creatureBossData)
            {
                creatureBossItems.Picture = StringProcess.GetGameResourcePath(creatureBossItems.Picture);
            }
            LandExpander.DataContext = _creatureLandData;
            OceanExpander.DataContext = _creatureOceanData;
            FlyExpander.DataContext = _creatureFlyData; 
            CaveExpander.DataContext = _creatureCaveData;
            EvilExpander.DataContext = _creatureEvilData;
            OthersExpander.DataContext = _creatureOthersData;
            BossExpander.DataContext = _creatureBossData;
            LeftFrame.NavigationService.Navigate(new CreatureDetail(), _creatureLandData[0]);
        }

        private void CreatureButton_Click(object sender, RoutedEventArgs e)
        {
            var creature = (Creature)((Button)sender).DataContext;
            LeftFrame.NavigationService.Navigate(new CreatureDetail(), creature);
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UiSplitter.Height = ActualHeight;
        }
    }
}
