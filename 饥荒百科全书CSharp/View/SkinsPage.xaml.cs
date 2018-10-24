using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Newtonsoft.Json;
using 饥荒百科全书CSharp.Class;
using 饥荒百科全书CSharp.Class.JsonDeserialize;
using 饥荒百科全书CSharp.MyUserControl;
using 饥荒百科全书CSharp.View.Details;

namespace 饥荒百科全书CSharp.View
{
    /// <summary>
    /// SkinsPage.xaml 的交互逻辑
    /// </summary>
    public partial class SkinsPage : Page
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
            //RightScrollViewer.FontWeight = Global.FontWeight;
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
                LeftFrame.NavigationService.Navigate(new SkinDetail(), Global.SkinsBodyData[0]);
            }
            else
            {
                //导航到指定页面
                var suggestBoxItemPicture = extraData[1];
                switch (extraData[0])
                {
                    case "SkinsWatchTwitchLive":
                        OnNavigatedToSkinDialog(Global.SkinsWatchTwitchLiveData, suggestBoxItemPicture);
                        break;
                    case "SkinsBody":
                        OnNavigatedToSkinDialog(Global.SkinsBodyData, suggestBoxItemPicture);
                        break;
                    case "SkinsHands":
                        OnNavigatedToSkinDialog(Global.SkinsHandsData, suggestBoxItemPicture);
                        break;
                    case "SkinsLegs":
                        OnNavigatedToSkinDialog(Global.SkinsLegsData, suggestBoxItemPicture);
                        break;
                    case "SkinsFeet":
                        OnNavigatedToSkinDialog(Global.SkinsFeetData, suggestBoxItemPicture);
                        break;
                    case "SkinsCharacters":
                        OnNavigatedToSkinDialog(Global.SkinsCharactersData, suggestBoxItemPicture);
                        break;
                    case "SkinsItems":
                        OnNavigatedToSkinDialog(Global.SkinsItemsData, suggestBoxItemPicture);
                        break;
                    case "SkinsStructures":
                        OnNavigatedToSkinDialog(Global.SkinsStructuresData, suggestBoxItemPicture);
                        break;
                    case "SkinsCritters":
                        OnNavigatedToSkinDialog(Global.SkinsCrittersData, suggestBoxItemPicture);
                        break;
                    case "SkinsSpecial":
                        OnNavigatedToSkinDialog(Global.SkinsSpecialData, suggestBoxItemPicture);
                        break;
                    case "SkinsHallowedNightsSkins":
                        OnNavigatedToSkinDialog(Global.SkinsHallowedNightsSkinsData, suggestBoxItemPicture);
                        break;
                    case "SkinsWintersFeastSkins":
                        OnNavigatedToSkinDialog(Global.SkinsWintersFeastSkinsData, suggestBoxItemPicture);
                        break;
                    case "SkinsYearOfTheGobblerSkins":
                        OnNavigatedToSkinDialog(Global.SkinsYearOfTheGobblerSkinsData, suggestBoxItemPicture);
                        break;
                    case "SkinsTheForge":
                        OnNavigatedToSkinDialog(Global.SkinsTheForgeData, suggestBoxItemPicture);
                        break;
                    case "SkinsYearOfTheVarg":
                        OnNavigatedToSkinDialog(Global.SkinsYearOfTheVargData, suggestBoxItemPicture);
                        break;
                    case "SkinsTheGorge":
                        OnNavigatedToSkinDialog(Global.SkinsTheGorgeData, suggestBoxItemPicture);
                        break;
                    case "SkinsEmotes":
                        OnNavigatedToSkinDialog(Global.SkinsEmotesData, suggestBoxItemPicture);
                        break;
                    case "SkinsOutfitSets":
                        OnNavigatedToSkinDialog(Global.SkinsOutfitSetsData, suggestBoxItemPicture);
                        break;
                }
            }
        }

        private void OnNavigatedToSkinDialog(List<Skin> SkinCollection, string suggestBoxItemPicture)
        {
            foreach (var itemsControlItem in SkinCollection)
            {
                var Skin = itemsControlItem;
                if (Skin == null || Skin.Picture != suggestBoxItemPicture) continue;
                RightScrollViewer.UpdateLayout();
                var resultList = new List<Button>();
                Global.FindChildren(resultList, RightScrollViewer);
                foreach (var button in resultList)
                {
                    var imageSource = ((Image)((Grid)button.Content).Children[0]).Source.ToString();
                    var imageSourceShort = imageSource.Substring(22, imageSource.Length - 22);
                    if (imageSourceShort != Skin.Picture) continue;
                    var SkinButton = button;
                    var currentScrollPosition = RightScrollViewer.VerticalOffset;
                    var point = new Point(0, currentScrollPosition);
                    var targetPosition = SkinButton.TransformToVisual(RightScrollViewer).Transform(point);
                    RightScrollViewer.ScrollToVerticalOffset(targetPosition.Y);
                    break;
                }
                LeftFrame.NavigationService.Navigate(new SkinDetail(), Skin);
                break;
            }
        }

        public SkinsPage()
        {
            InitializeComponent();
            Global.SkinLeftFrame = LeftFrame;
            Global.RightFrame.NavigationService.LoadCompleted += LoadCompleted;
        }

        public void Deserialize()
        {
            WatchTwitchLiveExpander.DataContext = Global.SkinsWatchTwitchLiveData;
            BodyExpander.DataContext = Global.SkinsBodyData;
            HandsExpander.DataContext = Global.SkinsHandsData;
            LegsExpander.DataContext = Global.SkinsLegsData;
            FeetExpander.DataContext = Global.SkinsFeetData;
            CharactersExpander.DataContext = Global.SkinsCharactersData;
            ItemsExpander.DataContext = Global.SkinsItemsData;
            StructuresExpander.DataContext = Global.SkinsStructuresData;
            CrittersExpander.DataContext = Global.SkinsCrittersData;
            SpecialExpander.DataContext = Global.SkinsSpecialData;
            HallowedNightsSkinsExpander.DataContext = Global.SkinsHallowedNightsSkinsData;
            WintersFeastSkinsExpander.DataContext = Global.SkinsWintersFeastSkinsData;
            YearOfTheGobblerSkinsExpander.DataContext = Global.SkinsYearOfTheGobblerSkinsData;
            TheForgeExpander.DataContext = Global.SkinsTheForgeData;
            YearOfTheVargExpander.DataContext = Global.SkinsYearOfTheVargData;
            TheGorgeExpander.DataContext = Global.SkinsTheGorgeData;
            EmotesExpander.DataContext = Global.SkinsEmotesData;
            OutfitSetsExpander.DataContext = Global.SkinsOutfitSetsData;
        }

        private void SkinsButton_Click(object sender, RoutedEventArgs e)
        {
            var skin = (Skin)((Button)sender).DataContext;
            LeftFrame.NavigationService.Navigate(new SkinDetail(), skin);
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UiSplitter.Height = ActualHeight;
        }
    }
}
