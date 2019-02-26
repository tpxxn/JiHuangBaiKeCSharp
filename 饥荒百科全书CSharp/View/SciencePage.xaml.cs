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
using 饥荒百科全书CSharp.MyUserControl;
using 饥荒百科全书CSharp.View.Details;

namespace 饥荒百科全书CSharp.View
{
    /// <summary>
    /// SciencePage.xaml 的交互逻辑
    /// </summary>
    public partial class SciencePage : Page
    {
        //private int _loadedTime;

        public void LoadCompleted(object sender, NavigationEventArgs e)
        {
            //if (_loadedTime != 0) return;
            //_loadedTime++;
            if (Global.FontFamily != null)
            {
                FontFamily = Global.FontFamily;
            }
            RightScrollViewer.FontWeight = Global.FontWeight;
            var extraData = (string[])e.ExtraData;
            Deserialize();
            // 小图标
            if (Settings.SmallButtonMode)
            {
                UpdateLayout();
                var resultList = new List<Button>();
                Global.FindChildren(resultList, RightScrollViewer);
                foreach (var button in resultList)
                {
                    button.Width = 65;
                    button.Height = 70;
                    ((Grid)button.Content).Margin = new Thickness(0);
                    ((Grid)button.Content).RowDefinitions[0].Height = new GridLength(50);
                    ((Grid)button.Content).Width = 65;
                    ((Grid)button.Content).Height = 70;
                    ((HrlTextBlock)((Grid)button.Content).Children[1]).HrlWidth = 65;
                }
            }
            if (extraData == null)
            {
                if(LeftFrame.CanGoBack)LeftFrame.RemoveBackEntry();
                LeftFrame.NavigationService.Navigate(new ScienceDetail(), Global.ScienceToolData[0]);
            }
            else
            {
                //导航到指定页面
                var suggestBoxItemPicture = extraData[1];
                switch (extraData[0])
                {
                    case "ScienceTool":
                        OnNavigatedToScienceDialog(Global.ScienceToolData, suggestBoxItemPicture);
                        break;
                    case "ScienceLight":
                        OnNavigatedToScienceDialog(Global.ScienceLightData, suggestBoxItemPicture);
                        break;
                    case "ScienceTreasureHunting":
                        OnNavigatedToScienceDialog(Global.ScienceTreasureHuntingData, suggestBoxItemPicture);
                        break;
                    case "ScienceNautical":
                        OnNavigatedToScienceDialog(Global.ScienceNauticalData, suggestBoxItemPicture);
                        break;
                    case "ScienceSurvival":
                        OnNavigatedToScienceDialog(Global.ScienceSurvivalData, suggestBoxItemPicture);
                        break;
                    case "ScienceFood":
                        OnNavigatedToScienceDialog(Global.ScienceFoodData, suggestBoxItemPicture);
                        break;
                    case "ScienceTechnology":
                        OnNavigatedToScienceDialog(Global.ScienceTechnologyData, suggestBoxItemPicture);
                        break;
                    case "ScienceFight":
                        OnNavigatedToScienceDialog(Global.ScienceFightData, suggestBoxItemPicture);
                        break;
                    case "ScienceStructure":
                        OnNavigatedToScienceDialog(Global.ScienceStructureData, suggestBoxItemPicture);
                        break;
                    case "ScienceRefine":
                        OnNavigatedToScienceDialog(Global.ScienceRefineData, suggestBoxItemPicture);
                        break;
                    case "ScienceMagic":
                        OnNavigatedToScienceDialog(Global.ScienceMagicData, suggestBoxItemPicture);
                        break;
                    case "ScienceDress":
                        OnNavigatedToScienceDialog(Global.ScienceDressData, suggestBoxItemPicture);
                        break;
                    case "ScienceCelestial":
                        OnNavigatedToScienceDialog(Global.ScienceCelestialData, suggestBoxItemPicture);
                        break;
                    case "ScienceMadScience":
                        OnNavigatedToScienceDialog(Global.ScienceMadScienceData, suggestBoxItemPicture);
                        break;
                    case "ScienceAncient":
                        OnNavigatedToScienceDialog(Global.ScienceAncientData, suggestBoxItemPicture);
                        break;
                    case "ScienceBook":
                        OnNavigatedToScienceDialog(Global.ScienceBookData, suggestBoxItemPicture);
                        break;
                    case "ScienceShadow":
                        OnNavigatedToScienceDialog(Global.ScienceShadowData, suggestBoxItemPicture);
                        break;
                    case "ScienceCritter":
                        OnNavigatedToScienceDialog(Global.ScienceCritterData, suggestBoxItemPicture);
                        break;
                    case "ScienceSculpt":
                        OnNavigatedToScienceDialog(Global.ScienceSculptData, suggestBoxItemPicture);
                        break;
                    case "ScienceCartography":
                        OnNavigatedToScienceDialog(Global.ScienceCartographyData, suggestBoxItemPicture);
                        break;
                    case "ScienceOfferings":
                        OnNavigatedToScienceDialog(Global.ScienceOfferingsData, suggestBoxItemPicture);
                        break;
                    case "ScienceVolcano":
                        OnNavigatedToScienceDialog(Global.ScienceVolcanoData, suggestBoxItemPicture);
                        break;
                    case "ScienceCityPlanning":
                        OnNavigatedToScienceDialog(Global.ScienceCityPlanningData, suggestBoxItemPicture);
                        break;
                    case "ScienceGreenThumb":
                        OnNavigatedToScienceDialog(Global.ScienceGreenThumbData, suggestBoxItemPicture);
                        break;
                    case "ScienceRenovateFlooring":
                        OnNavigatedToScienceDialog(Global.ScienceRenovateFlooringData, suggestBoxItemPicture);
                        break;
                    case "ScienceRenovateShelves":
                        OnNavigatedToScienceDialog(Global.ScienceRenovateShelvesData, suggestBoxItemPicture);
                        break;
                    case "ScienceRenovatePlantholders":
                        OnNavigatedToScienceDialog(Global.ScienceRenovatePlantholdersData, suggestBoxItemPicture);
                        break;
                    case "ScienceRenovateColumns":
                        OnNavigatedToScienceDialog(Global.ScienceRenovateColumnsData, suggestBoxItemPicture);
                        break;
                    case "ScienceRenovateWallPapers":
                        OnNavigatedToScienceDialog(Global.ScienceRenovateWallPapersData, suggestBoxItemPicture);
                        break;
                    case "ScienceRenovateCeilingLights":
                        OnNavigatedToScienceDialog(Global.ScienceRenovateCeilingLightsData, suggestBoxItemPicture);
                        break;
                    case "ScienceRenovateWallDecorations":
                        OnNavigatedToScienceDialog(Global.ScienceRenovateWallDecorationsData, suggestBoxItemPicture);
                        break;
                    case "ScienceRenovateChairs":
                        OnNavigatedToScienceDialog(Global.ScienceRenovateChairsData, suggestBoxItemPicture);
                        break;
                    case "ScienceRenovateHouseUpgrades":
                        OnNavigatedToScienceDialog(Global.ScienceRenovateHouseUpgradesData, suggestBoxItemPicture);
                        break;
                    case "ScienceRenovateWindows":
                        OnNavigatedToScienceDialog(Global.ScienceRenovateWindowsData, suggestBoxItemPicture);
                        break;
                    case "ScienceRenovateRugs":
                        OnNavigatedToScienceDialog(Global.ScienceRenovateRugsData, suggestBoxItemPicture);
                        break;
                    case "ScienceRenovateLamps":
                        OnNavigatedToScienceDialog(Global.ScienceRenovateLampsData, suggestBoxItemPicture);
                        break;
                    case "ScienceRenovateTables":
                        OnNavigatedToScienceDialog(Global.ScienceRenovateTablesData, suggestBoxItemPicture);
                        break;
                }
            }
        }

        private void OnNavigatedToScienceDialog(List<Science> scienceCollection, string suggestBoxItemPicture)
        {
            foreach (var itemsControlItem in scienceCollection)
            {
                var science = itemsControlItem;
                if (science == null || science.Picture != suggestBoxItemPicture) continue;
                RightScrollViewer.UpdateLayout();
                var resultList = new List<Button>();
                Global.FindChildren(resultList, RightScrollViewer);
                foreach (var button in resultList)
                {
                    var imageSource = ((Image)((Grid)button.Content).Children[0]).Source.ToString();
                    var imageSourceShort = imageSource.Substring(22, imageSource.Length - 22);
                    if (imageSourceShort != science.Picture) continue;
                    var scienceButton = button;
                    var currentScrollPosition = RightScrollViewer.VerticalOffset;
                    var point = new Point(0, currentScrollPosition);
                    var targetPosition = scienceButton.TransformToVisual(RightScrollViewer).Transform(point);
                    RightScrollViewer.ScrollToVerticalOffset(targetPosition.Y);
                    break;
                }
                if(LeftFrame.CanGoBack)LeftFrame.RemoveBackEntry();
                LeftFrame.NavigationService.Navigate(new ScienceDetail(), science);
                break;
            }
        }

        public SciencePage()
        {
            InitializeComponent();
            Global.ScienceLeftFrame = LeftFrame;
            Global.RightFrame.NavigationService.LoadCompleted += LoadCompleted;
            // SW/Hamlet
            if (Global.GameVersion != 4 && Global.GameVersion != 5)
            {
                NauticalExpander.Visibility = Visibility.Collapsed;
            }
            else
            {
                AncientExpander.Visibility = Visibility.Collapsed;
            }
            //SW
            if (Global.GameVersion != 4)
            {
                VolcanoExpander.Visibility = Visibility.Collapsed;
            }
            //Hamlet
            if (Global.GameVersion != 5)
            {
                TreasureHuntingExpander.Visibility = Visibility.Collapsed;
                CityPlanningExpander.Visibility = Visibility.Collapsed;
                GreenThumbExpander.Visibility = Visibility.Collapsed;
                RenovateExpander.Visibility = Visibility.Collapsed;
            }
            if (Global.GameVersion != 0 && Global.GameVersion != 1)
            {
                ShadowExpander.Visibility = Visibility.Collapsed;
                CritterExpaner.Visibility = Visibility.Collapsed;
                SculptExpander.Visibility = Visibility.Collapsed;
                CartographyExpander.Visibility = Visibility.Collapsed;
                CelestialExpander.Visibility = Visibility.Collapsed;
                MadScienceExpander.Visibility = Visibility.Collapsed;
                OfferingsExpander.Visibility = Visibility.Collapsed;
            }
            if (Global.GameVersion == 1)
            {
                LightExpanderTextBolck.Text = "点燃";
                DressExpanderTextBolck.Text = "服装";
                CelestialExpanderTextBolck.Text = "天空";
                MadScienceExpanderTextBolck.Text = "疯狂科学";
                ShadowExpanderTextBolck.Text = "影子";
                CritterExpanderTextBolck.Text = "小动物";
                CartographyExpanderTextBolck.Text = "制图学";
                OfferingsExpanderTextBolck.Text = "贡品";
            }
        }

        public void Deserialize()
        {
            ToolExpander.DataContext = Global.ScienceToolData;
            LightExpander.DataContext = Global.ScienceLightData;
            TreasureHuntingExpander.DataContext = Global.ScienceTreasureHuntingData;
            NauticalExpander.DataContext = Global.ScienceNauticalData;
            SurvivalExpander.DataContext = Global.ScienceSurvivalData;
            FoodExpander.DataContext = Global.ScienceFoodData;
            TechnologyExpander.DataContext = Global.ScienceTechnologyData;
            FightExpander.DataContext = Global.ScienceFightData;
            StructuresExpander.DataContext = Global.ScienceStructureData;
            RefineExpander.DataContext = Global.ScienceRefineData;
            MagicExpander.DataContext = Global.ScienceMagicData;
            DressExpander.DataContext = Global.ScienceDressData;
            CelestialExpander.DataContext = Global.ScienceCelestialData;
            MadScienceExpander.DataContext = Global.ScienceMadScienceData;
            AncientExpander.DataContext = Global.ScienceAncientData;
            BooksExpander.DataContext = Global.ScienceBookData;
            ShadowExpander.DataContext = Global.ScienceShadowData;
            CritterExpaner.DataContext = Global.ScienceCritterData;
            SculptExpander.DataContext = Global.ScienceSculptData;
            CartographyExpander.DataContext = Global.ScienceCartographyData;
            OfferingsExpander.DataContext = Global.ScienceOfferingsData;
            VolcanoExpander.DataContext = Global.ScienceVolcanoData;
            CityPlanningExpander.DataContext = Global.ScienceCityPlanningData;
            GreenThumbExpander.DataContext = Global.ScienceGreenThumbData;
            RenovateFlooringExpander.DataContext = Global.ScienceRenovateFlooringData;
            RenovateShelvesExpander.DataContext = Global.ScienceRenovateShelvesData;
            RenovatePlantholdersExpander.DataContext = Global.ScienceRenovatePlantholdersData;
            RenovateColumnsExpander.DataContext = Global.ScienceRenovateColumnsData;
            RenovateWallPapersExpander.DataContext = Global.ScienceRenovateWallPapersData;
            RenovateCeilingLightsExpander.DataContext = Global.ScienceRenovateCeilingLightsData;
            RenovateWallDecorationsExpander.DataContext = Global.ScienceRenovateWallDecorationsData;
            RenovateChairsExpander.DataContext = Global.ScienceRenovateChairsData;
            RenovateHouseUpgradesExpander.DataContext = Global.ScienceRenovateHouseUpgradesData;
            RenovateWindowsExpander.DataContext = Global.ScienceRenovateWindowsData;
            RenovateRugsExpander.DataContext = Global.ScienceRenovateRugsData;
            RenovateLampsExpander.DataContext = Global.ScienceRenovateLampsData;
            RenovateTablesExpander.DataContext = Global.ScienceRenovateTablesData;
        }

        private void ScienceButton_Click(object sender, RoutedEventArgs e)
        {
            var science = (Science)((Button)sender).DataContext;
            if(LeftFrame.CanGoBack)LeftFrame.RemoveBackEntry();
            LeftFrame.NavigationService.Navigate(new ScienceDetail(), science);
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UiSplitter.Height = ActualHeight;
        }

        private void UIElement_OnMouseEnter(object sender, MouseEventArgs e)
        {
            var button = (Button)sender;
            ((HrlTextBlock)((Grid)button.Content).Children[1]).HrlTextBlock_OnMouseEnter(null, null);
        }

        private void UIElement_OnMouseLeave(object sender, MouseEventArgs e)
        {
            var button = (Button)sender;
            ((HrlTextBlock)((Grid)button.Content).Children[1]).HrlTextBlock_OnMouseLeave(null, null);
        }
    }
}
