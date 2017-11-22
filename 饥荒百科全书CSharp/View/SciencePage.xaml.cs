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
    /// SciencePage.xaml 的交互逻辑
    /// </summary>
    public partial class SciencePage : Page
    {
        private readonly ObservableCollection<Science> _scienceToolData = new ObservableCollection<Science>();
        private readonly ObservableCollection<Science> _scienceLightData = new ObservableCollection<Science>();
        private readonly ObservableCollection<Science> _scienceNauticalData = new ObservableCollection<Science>();
        private readonly ObservableCollection<Science> _scienceSurvivalData = new ObservableCollection<Science>();
        private readonly ObservableCollection<Science> _scienceFoodData = new ObservableCollection<Science>();
        private readonly ObservableCollection<Science> _scienceTechnologyData = new ObservableCollection<Science>();
        private readonly ObservableCollection<Science> _scienceFightData = new ObservableCollection<Science>();
        private readonly ObservableCollection<Science> _scienceStructureData = new ObservableCollection<Science>();
        private readonly ObservableCollection<Science> _scienceRefineData = new ObservableCollection<Science>();
        private readonly ObservableCollection<Science> _scienceMagicData = new ObservableCollection<Science>();
        private readonly ObservableCollection<Science> _scienceDressData = new ObservableCollection<Science>();
        private readonly ObservableCollection<Science> _scienceAncientData = new ObservableCollection<Science>();
        private readonly ObservableCollection<Science> _scienceBookData = new ObservableCollection<Science>();
        private readonly ObservableCollection<Science> _scienceShadowData = new ObservableCollection<Science>();
        private readonly ObservableCollection<Science> _scienceCritterData = new ObservableCollection<Science>();
        private readonly ObservableCollection<Science> _scienceSculptData = new ObservableCollection<Science>();
        private readonly ObservableCollection<Science> _scienceCartographyData = new ObservableCollection<Science>();
        private readonly ObservableCollection<Science> _scienceOfferingsData = new ObservableCollection<Science>();
        private readonly ObservableCollection<Science> _scienceVolcanoData = new ObservableCollection<Science>();

        private int _loadedTime;

        public void LoadCompleted(object sender, NavigationEventArgs e)
        {
            if (_loadedTime != 0) return;
            _loadedTime++;
            if (Global.FontFamily != null)
            {
                FontFamily = Global.FontFamily;
            }
            var extraData = (string[])e.ExtraData;
            Deserialize();
            if (extraData == null)
            {
                LeftFrame.NavigationService.Navigate(new ScienceDetail(), _scienceToolData[0]);
            }
            else
            {
                //导航到指定页面
                var suggestBoxItemPicture = extraData[1];
                switch (extraData[0])
                {
                    case "ScienceTool":
                        OnNavigatedToScienceDialog(_scienceToolData, suggestBoxItemPicture);
                        break;
                    case "ScienceLight":
                        OnNavigatedToScienceDialog(_scienceLightData, suggestBoxItemPicture);
                        break;
                    case "ScienceNautical":
                        OnNavigatedToScienceDialog(_scienceNauticalData, suggestBoxItemPicture);
                        break;
                    case "ScienceSurvival":
                        OnNavigatedToScienceDialog(_scienceSurvivalData, suggestBoxItemPicture);
                        break;
                    case "ScienceFood":
                        OnNavigatedToScienceDialog(_scienceFoodData, suggestBoxItemPicture);
                        break;
                    case "ScienceTechnology":
                        OnNavigatedToScienceDialog(_scienceTechnologyData, suggestBoxItemPicture);
                        break;
                    case "ScienceFight":
                        OnNavigatedToScienceDialog(_scienceFightData, suggestBoxItemPicture);
                        break;
                    case "ScienceStructure":
                        OnNavigatedToScienceDialog(_scienceStructureData, suggestBoxItemPicture);
                        break;
                    case "ScienceRefine":
                        OnNavigatedToScienceDialog(_scienceRefineData, suggestBoxItemPicture);
                        break;
                    case "ScienceMagic":
                        OnNavigatedToScienceDialog(_scienceMagicData, suggestBoxItemPicture);
                        break;
                    case "ScienceDress":
                        OnNavigatedToScienceDialog(_scienceDressData, suggestBoxItemPicture);
                        break;
                    case "ScienceAncient":
                        OnNavigatedToScienceDialog(_scienceAncientData, suggestBoxItemPicture);
                        break;
                    case "ScienceBook":
                        OnNavigatedToScienceDialog(_scienceBookData, suggestBoxItemPicture);
                        break;
                    case "ScienceShadow":
                        OnNavigatedToScienceDialog(_scienceShadowData, suggestBoxItemPicture);
                        break;
                    case "ScienceCritter":
                        OnNavigatedToScienceDialog(_scienceCritterData, suggestBoxItemPicture);
                        break;
                    case "ScienceSculpt":
                        OnNavigatedToScienceDialog(_scienceSculptData, suggestBoxItemPicture);
                        break;
                    case "ScienceCartography":
                        OnNavigatedToScienceDialog(_scienceCartographyData, suggestBoxItemPicture);
                        break;
                    case "ScienceOfferings":
                        OnNavigatedToScienceDialog(_scienceOfferingsData, suggestBoxItemPicture);
                        break;
                    case "ScienceVolcano":
                        OnNavigatedToScienceDialog(_scienceVolcanoData, suggestBoxItemPicture);
                        break;
                }
            }
        }

        private void OnNavigatedToScienceDialog(ObservableCollection<Science> scienceCollection, string suggestBoxItemPicture)
        {
            foreach (var itemsControlItem in scienceCollection)
            {
                var science = itemsControlItem;
                if (science == null || science.Picture != suggestBoxItemPicture) continue;
                LeftFrame.NavigationService.Navigate(new ScienceDetail(), science);
                break;
            }
        }

        public SciencePage()
        {
            InitializeComponent();
            Global.ScienceLeftFrame = LeftFrame;
            Global.RightFrame.NavigationService.LoadCompleted += LoadCompleted;
            if (Global.GameVersion != 4)
            {
                NauticalExpander.Visibility = Visibility.Collapsed;
                VolcanoExpander.Visibility = Visibility.Collapsed;
            }
            else
            {
                AncientExpander.Visibility = Visibility.Collapsed;
            }
            if (Global.GameVersion != 0 && Global.GameVersion != 1)
            {
                ShadowExpander.Visibility = Visibility.Collapsed;
                CritterExpaner.Visibility = Visibility.Collapsed;
                SculptExpander.Visibility = Visibility.Collapsed;
                CartographyExpander.Visibility = Visibility.Collapsed;
                OfferingsExpander.Visibility = Visibility.Collapsed;
            }
            if (Global.GameVersion == 1)
            {
                LightExpanderTextBolck.Text = "点燃";
                DressExpanderTextBolck.Text = "服装";
                ShadowExpanderTextBolck.Text = "影子";
                CritterExpanderTextBolck.Text = "小动物";
                CartographyExpanderTextBolck.Text = "制图学";
                OfferingsExpanderTextBolck.Text = "贡品";
            }
            Deserialize();
        }

        public void Deserialize()
        {
            _scienceToolData.Clear();
            _scienceLightData.Clear();
            _scienceNauticalData.Clear();
            _scienceSurvivalData.Clear();
            _scienceFoodData.Clear();
            _scienceTechnologyData.Clear();
            _scienceFightData.Clear();
            _scienceStructureData.Clear();
            _scienceRefineData.Clear();
            _scienceMagicData.Clear();
            _scienceDressData.Clear();
            _scienceAncientData.Clear();
            _scienceBookData.Clear();
            _scienceShadowData.Clear();
            _scienceCritterData.Clear();
            _scienceSculptData.Clear();
            _scienceCartographyData.Clear();
            _scienceOfferingsData.Clear();
            _scienceVolcanoData.Clear();
            var science = JsonConvert.DeserializeObject<ScienceRootObject>(StringProcess.GetJsonString("Sciences.json"));
            foreach (var scienceToolItems in science.Tool.Science)
            {
                _scienceToolData.Add(scienceToolItems);
            }
            foreach (var scienceToolItems in _scienceToolData)
            {
                scienceToolItems.Picture = StringProcess.GetGameResourcePath(scienceToolItems.Picture);
            }
            foreach (var scienceLightItems in science.Light.Science)
            {
                _scienceLightData.Add(scienceLightItems);
            }
            foreach (var scienceLightItems in _scienceLightData)
            {
                scienceLightItems.Picture = StringProcess.GetGameResourcePath(scienceLightItems.Picture);
            }
            foreach (var scienceNauticalItems in science.Nautical.Science)
            {
                _scienceNauticalData.Add(scienceNauticalItems);
            }
            foreach (var scienceNauticalItems in _scienceNauticalData)
            {
                scienceNauticalItems.Picture = StringProcess.GetGameResourcePath(scienceNauticalItems.Picture);
            }
            foreach (var scienceSurvivalItems in science.Survival.Science)
            {
                _scienceSurvivalData.Add(scienceSurvivalItems);
            }
            foreach (var scienceSurvivalItems in _scienceSurvivalData)
            {
                scienceSurvivalItems.Picture = StringProcess.GetGameResourcePath(scienceSurvivalItems.Picture);
            }
            foreach (var scienceFoodItems in science.Foods.Science)
            {
                _scienceFoodData.Add(scienceFoodItems);
            }
            foreach (var scienceFoodItems in _scienceFoodData)
            {
                scienceFoodItems.Picture = StringProcess.GetGameResourcePath(scienceFoodItems.Picture);
            }
            foreach (var scienceTechnologyItems in science.Technology.Science)
            {
                _scienceTechnologyData.Add(scienceTechnologyItems);
            }
            foreach (var scienceTechnologyItems in _scienceTechnologyData)
            {
                scienceTechnologyItems.Picture = StringProcess.GetGameResourcePath(scienceTechnologyItems.Picture);
            }
            foreach (var scienceFightItems in science.Fight.Science)
            {
                _scienceFightData.Add(scienceFightItems);
            }
            foreach (var scienceFightItems in _scienceFightData)
            {
                scienceFightItems.Picture = StringProcess.GetGameResourcePath(scienceFightItems.Picture);
            }
            foreach (var scienceStructureItems in science.Structure.Science)
            {
                _scienceStructureData.Add(scienceStructureItems);
            }
            foreach (var scienceStructureItems in _scienceStructureData)
            {
                scienceStructureItems.Picture = StringProcess.GetGameResourcePath(scienceStructureItems.Picture);
            }
            foreach (var scienceRefineItems in science.Refine.Science)
            {
                _scienceRefineData.Add(scienceRefineItems);
            }
            foreach (var scienceRefineItems in _scienceRefineData)
            {
                scienceRefineItems.Picture = StringProcess.GetGameResourcePath(scienceRefineItems.Picture);
            }
            foreach (var scienceMagicItems in science.Magic.Science)
            {
                _scienceMagicData.Add(scienceMagicItems);
            }
            foreach (var scienceMagicItems in _scienceMagicData)
            {
                scienceMagicItems.Picture = StringProcess.GetGameResourcePath(scienceMagicItems.Picture);
            }
            foreach (var scienceDressItems in science.Dress.Science)
            {
                _scienceDressData.Add(scienceDressItems);
            }
            foreach (var scienceDressItems in _scienceDressData)
            {
                scienceDressItems.Picture = StringProcess.GetGameResourcePath(scienceDressItems.Picture);
            }
            foreach (var scienceAncientItems in science.Ancient.Science)
            {
                _scienceAncientData.Add(scienceAncientItems);
            }
            foreach (var scienceAncientItems in _scienceAncientData)
            {
                scienceAncientItems.Picture = StringProcess.GetGameResourcePath(scienceAncientItems.Picture);
            }
            foreach (var scienceBookItems in science.Book.Science)
            {
                _scienceBookData.Add(scienceBookItems);
            }
            foreach (var scienceBookItems in _scienceBookData)
            {
                scienceBookItems.Picture = StringProcess.GetGameResourcePath(scienceBookItems.Picture);
            }
            foreach (var scienceShadowItems in science.Shadow.Science)
            {
                _scienceShadowData.Add(scienceShadowItems);
            }
            foreach (var scienceShadowItems in _scienceShadowData)
            {
                scienceShadowItems.Picture = StringProcess.GetGameResourcePath(scienceShadowItems.Picture);
            }
            foreach (var scienceCritterItems in science.Critter.Science)
            {
                _scienceCritterData.Add(scienceCritterItems);
            }
            foreach (var scienceCritterItems in _scienceCritterData)
            {
                scienceCritterItems.Picture = StringProcess.GetGameResourcePath(scienceCritterItems.Picture);
            }
            foreach (var scienceSculptItems in science.Sculpt.Science)
            {
                _scienceSculptData.Add(scienceSculptItems);
            }
            foreach (var scienceSculptItems in _scienceSculptData)
            {
                scienceSculptItems.Picture = StringProcess.GetGameResourcePath(scienceSculptItems.Picture);
            }
            foreach (var scienceCartographyItems in science.Cartography.Science)
            {
                _scienceCartographyData.Add(scienceCartographyItems);
            }
            foreach (var scienceCartographyItems in _scienceCartographyData)
            {
                scienceCartographyItems.Picture = StringProcess.GetGameResourcePath(scienceCartographyItems.Picture);
            }
            foreach (var scienceOfferingsItems in science.Offerings.Science)
            {
                _scienceOfferingsData.Add(scienceOfferingsItems);
            }
            foreach (var scienceOfferingsItems in _scienceOfferingsData)
            {
                scienceOfferingsItems.Picture = StringProcess.GetGameResourcePath(scienceOfferingsItems.Picture);
            }
            foreach (var scienceVolcanoItems in science.Volcano.Science)
            {
                _scienceVolcanoData.Add(scienceVolcanoItems);
            }
            foreach (var scienceVolcanoItems in _scienceVolcanoData)
            {
                scienceVolcanoItems.Picture = StringProcess.GetGameResourcePath(scienceVolcanoItems.Picture);
            }
            ToolExpander.DataContext = _scienceToolData;
            LightExpander.DataContext = _scienceLightData;
            NauticalExpander.DataContext = _scienceNauticalData;
            SurvivalExpander.DataContext = _scienceSurvivalData;
            FoodExpander.DataContext = _scienceFoodData;
            TechnologyExpander.DataContext = _scienceTechnologyData;
            FightExpander.DataContext = _scienceFightData;
            StructuresExpander.DataContext = _scienceStructureData;
            RefineExpander.DataContext = _scienceRefineData;
            MagicExpander.DataContext = _scienceMagicData;
            DressExpander.DataContext = _scienceDressData;
            AncientExpander.DataContext = _scienceAncientData;
            BooksExpander.DataContext = _scienceBookData;
            ShadowExpander.DataContext = _scienceShadowData;
            CritterExpaner.DataContext = _scienceCritterData;
            SculptExpander.DataContext = _scienceSculptData;
            CartographyExpander.DataContext = _scienceCartographyData;
            OfferingsExpander.DataContext = _scienceOfferingsData;
            VolcanoExpander.DataContext = _scienceVolcanoData;
        }

        private void ScienceButton_Click(object sender, RoutedEventArgs e)
        {
            var science = (Science)((Button)sender).DataContext;
            LeftFrame.NavigationService.Navigate(new ScienceDetail(), science);
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UiSplitter.Height = ActualHeight;
        }
    }
}
