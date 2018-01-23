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
    /// CreaturePage.xaml 的交互逻辑
    /// </summary>
    public partial class CreaturePage : Page
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
            // 小图标
            if (Settings.SmallButtonMode)
            {
                UpdateLayout();
                var resultList = new List<Button>();
                Global.FindChildren(resultList, RightScrollViewer);
                foreach (var button in resultList)
                {
                    button.Width = 85;
                    button.Height = 90;
                    ((Grid)button.Content).Margin = new Thickness(0);
                    ((Grid)button.Content).RowDefinitions[0].Height = new GridLength(70);
                    ((Grid)button.Content).Width = 85;
                    ((Grid)button.Content).Height = 90;
                    ((HrlTextBlock)((Grid)button.Content).Children[1]).HrlWidth = 85;
                }
            }
            if (extraData == null)
            {
                LeftFrame.NavigationService.Navigate(new CreatureDetail(), Global.CreatureLandData[0]);
            }
            else
            {
                //导航到指定页面
                var suggestBoxItemPicture = extraData[1];
                switch (extraData[0])
                {
                    case "CreatureLand":
                        OnNavigatedToCreatureDialog(Global.CreatureLandData, suggestBoxItemPicture);
                        break;
                    case "CreatureOcean":
                        OnNavigatedToCreatureDialog(Global.CreatureOceanData, suggestBoxItemPicture);
                        break;
                    case "CreatureFly":
                        OnNavigatedToCreatureDialog(Global.CreatureFlyData, suggestBoxItemPicture);
                        break;
                    case "CreatureCave":
                        OnNavigatedToCreatureDialog(Global.CreatureCaveData, suggestBoxItemPicture);
                        break;
                    case "CreatureEvil":
                        OnNavigatedToCreatureDialog(Global.CreatureEvilData, suggestBoxItemPicture);
                        break;
                    case "CreatureOther":
                        OnNavigatedToCreatureDialog(Global.CreatureOthersData, suggestBoxItemPicture);
                        break;
                    case "CreatureBoss":
                        OnNavigatedToCreatureDialog(Global.CreatureBossData, suggestBoxItemPicture);
                        break;
                }
            }
        }

        private void OnNavigatedToCreatureDialog(List<Creature> creatureCollection, string suggestBoxItemPicture)
        {
            foreach (var gridViewItem in creatureCollection)
            {
                var creature = gridViewItem;
                if (creature == null || creature.Picture != suggestBoxItemPicture) continue;
                RightScrollViewer.UpdateLayout();
                var resultList = new List<Button>();
                Global.FindChildren(resultList, RightScrollViewer);
                foreach (var button in resultList)
                {
                    var imageSource = ((Image)((Grid)button.Content).Children[0]).Source.ToString();
                    var imageSourceShort = imageSource.Substring(22, imageSource.Length - 22);
                    if (imageSourceShort != creature.Picture) continue;
                    var creatureButton = button;
                    var currentScrollPosition = RightScrollViewer.VerticalOffset;
                    var point = new Point(0, currentScrollPosition);
                    var targetPosition = creatureButton.TransformToVisual(RightScrollViewer).Transform(point);
                    RightScrollViewer.ScrollToVerticalOffset(targetPosition.Y);
                    break;
                }
                LeftFrame.NavigationService.Navigate(new CreatureDetail(), creature);
                break;
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
        }

        public void Deserialize()
        {
            LandExpander.DataContext = Global.CreatureLandData;
            OceanExpander.DataContext = Global.CreatureOceanData;
            FlyExpander.DataContext = Global.CreatureFlyData;
            CaveExpander.DataContext = Global.CreatureCaveData;
            EvilExpander.DataContext = Global.CreatureEvilData;
            OthersExpander.DataContext = Global.CreatureOthersData;
            BossExpander.DataContext = Global.CreatureBossData;
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
