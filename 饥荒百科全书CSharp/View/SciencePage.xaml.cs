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
    /// SciencePage.xaml 的交互逻辑
    /// </summary>
    public partial class SciencePage : Page
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
            RightScrollViewer.FontWeight = Global.FontWeight;
            var extraData = (string[])e.ExtraData;
            Deserialize();
            if (extraData == null)
            {
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
        }

        public void Deserialize()
        {
            ToolExpander.DataContext = Global.ScienceToolData;
            LightExpander.DataContext = Global.ScienceLightData;
            NauticalExpander.DataContext = Global.ScienceNauticalData;
            SurvivalExpander.DataContext = Global.ScienceSurvivalData;
            FoodExpander.DataContext = Global.ScienceFoodData;
            TechnologyExpander.DataContext = Global.ScienceTechnologyData;
            FightExpander.DataContext = Global.ScienceFightData;
            StructuresExpander.DataContext = Global.ScienceStructureData;
            RefineExpander.DataContext = Global.ScienceRefineData;
            MagicExpander.DataContext = Global.ScienceMagicData;
            DressExpander.DataContext = Global.ScienceDressData;
            AncientExpander.DataContext = Global.ScienceAncientData;
            BooksExpander.DataContext = Global.ScienceBookData;
            ShadowExpander.DataContext = Global.ScienceShadowData;
            CritterExpaner.DataContext = Global.ScienceCritterData;
            SculptExpander.DataContext = Global.ScienceSculptData;
            CartographyExpander.DataContext = Global.ScienceCartographyData;
            OfferingsExpander.DataContext = Global.ScienceOfferingsData;
            VolcanoExpander.DataContext = Global.ScienceVolcanoData;
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
