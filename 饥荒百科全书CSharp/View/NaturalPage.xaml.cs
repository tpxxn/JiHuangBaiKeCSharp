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
    /// NaturalPage.xaml 的交互逻辑
    /// </summary>
    public partial class NaturalPage : Page
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
                if(LeftFrame.CanGoBack)LeftFrame.RemoveBackEntry();
                LeftFrame.NavigationService.Navigate(new NaturalBiomesDetail(), Global.NaturalBiomesData[0]);
            }
            else
            {
                var suggestBoxItemPicture = extraData[1];
                //导航到指定页面
                switch (extraData[0])
                {
                    case "NaturalBiomes":
                        OnNavigatedToBiomesDialog(Global.NaturalBiomesData, suggestBoxItemPicture);
                        break;
                    case "NaturalSmallPlants":
                        OnNavigatedToSmallPlantDialog(Global.NaturalSmallPlantsData, suggestBoxItemPicture);
                        break;
                    case "NaturalTrees":
                        OnNavigatedToTreeDialog(Global.NaturalTreesData, suggestBoxItemPicture);
                        break;
                    case "NaturalCreatureNests":
                        OnNavigatedToCreatureNestDialog(Global.NaturalCreatureNestData, suggestBoxItemPicture);
                        break;
                    case "NaturalInanimate":
                        OnNavigatedToInanimateDialog(Global.NaturalInanimatesData, suggestBoxItemPicture);
                        break;
                }
            }
        }

        private void OnNavigatedToBiomesDialog(List<NatureBiomes> naturalCollection, string suggestBoxItemPicture)
        {
            foreach (var itemsControlItem in naturalCollection)
            {
                var naturalBiomes = itemsControlItem;
                if (naturalBiomes == null || naturalBiomes.Picture != suggestBoxItemPicture) continue;
                RightScrollViewer.UpdateLayout();
                var resultList = new List<Button>();
                Global.FindChildren(resultList, RightScrollViewer);
                foreach (var button in resultList)
                {
                    var imageSource = ((Image)((Grid)button.Content).Children[0]).Source.ToString();
                    var imageSourceShort = imageSource.Substring(22, imageSource.Length - 22);
                    if (imageSourceShort != naturalBiomes.Picture) continue;
                    var naturalBiomesButton = button;
                    var currentScrollPosition = RightScrollViewer.VerticalOffset;
                    var point = new Point(0, currentScrollPosition);
                    var targetPosition = naturalBiomesButton.TransformToVisual(RightScrollViewer).Transform(point);
                    RightScrollViewer.ScrollToVerticalOffset(targetPosition.Y);
                    break;
                }
                if(LeftFrame.CanGoBack)LeftFrame.RemoveBackEntry();
                LeftFrame.NavigationService.Navigate(new NaturalBiomesDetail(), naturalBiomes);
                break;
            }
        }

        private void OnNavigatedToSmallPlantDialog(List<NatureSmallPlant> naturalCollection, string suggestBoxItemPicture)
        {
            foreach (var itemsControlItem in naturalCollection)
            {
                var naturalSmallPlant = itemsControlItem;
                if (naturalSmallPlant == null || naturalSmallPlant.Picture != suggestBoxItemPicture) continue;
                RightScrollViewer.UpdateLayout();
                var resultList = new List<Button>();
                Global.FindChildren(resultList, RightScrollViewer);
                foreach (var button in resultList)
                {
                    var imageSource = ((Image)((Grid)button.Content).Children[0]).Source.ToString();
                    var imageSourceShort = imageSource.Substring(22, imageSource.Length - 22);
                    if (imageSourceShort != naturalSmallPlant.Picture) continue;
                    var naturalSmallPlantButton = button;
                    var currentScrollPosition = RightScrollViewer.VerticalOffset;
                    var point = new Point(0, currentScrollPosition);
                    var targetPosition = naturalSmallPlantButton.TransformToVisual(RightScrollViewer).Transform(point);
                    RightScrollViewer.ScrollToVerticalOffset(targetPosition.Y);
                    break;
                }
                if(LeftFrame.CanGoBack)LeftFrame.RemoveBackEntry();
                LeftFrame.NavigationService.Navigate(new NaturalSmallPlantDetail(), naturalSmallPlant);
                break;
            }
        }

        private void OnNavigatedToTreeDialog(List<NatureTree> naturalCollection, string suggestBoxItemPicture)
        {
            foreach (var itemsControlItem in naturalCollection)
            {
                var naturalSmallPlant = itemsControlItem;
                if (naturalSmallPlant == null || naturalSmallPlant.Picture != suggestBoxItemPicture) continue;
                RightScrollViewer.UpdateLayout();
                var resultList = new List<Button>();
                Global.FindChildren(resultList, RightScrollViewer);
                foreach (var button in resultList)
                {
                    var imageSource = ((Image)((Grid)button.Content).Children[0]).Source.ToString();
                    var imageSourceShort = imageSource.Substring(22, imageSource.Length - 22);
                    if (imageSourceShort != naturalSmallPlant.Picture) continue;
                    var naturalSmallPlantButton = button;
                    var currentScrollPosition = RightScrollViewer.VerticalOffset;
                    var point = new Point(0, currentScrollPosition);
                    var targetPosition = naturalSmallPlantButton.TransformToVisual(RightScrollViewer).Transform(point);
                    RightScrollViewer.ScrollToVerticalOffset(targetPosition.Y);
                    break;
                }
                if(LeftFrame.CanGoBack)LeftFrame.RemoveBackEntry();
                LeftFrame.NavigationService.Navigate(new NaturalTreesDetail(), naturalSmallPlant);
                break;
            }
        }

        private void OnNavigatedToCreatureNestDialog(List<NatureCreatureNest> naturalCollection, string suggestBoxItemPicture)
        {
            foreach (var itemsControlItem in naturalCollection)
            {
                var naturalCreatureNest = itemsControlItem;
                if (naturalCreatureNest == null || naturalCreatureNest.Picture != suggestBoxItemPicture) continue;
                RightScrollViewer.UpdateLayout();
                var resultList = new List<Button>();
                Global.FindChildren(resultList, RightScrollViewer);
                foreach (var button in resultList)
                {
                    var imageSource = ((Image)((Grid)button.Content).Children[0]).Source.ToString();
                    var imageSourceShort = imageSource.Substring(22, imageSource.Length - 22);
                    if (imageSourceShort != naturalCreatureNest.Picture) continue;
                    var naturalCreatureNestButton = button;
                    var currentScrollPosition = RightScrollViewer.VerticalOffset;
                    var point = new Point(0, currentScrollPosition);
                    var targetPosition = naturalCreatureNestButton.TransformToVisual(RightScrollViewer).Transform(point);
                    RightScrollViewer.ScrollToVerticalOffset(targetPosition.Y);
                    break;
                }
                if(LeftFrame.CanGoBack)LeftFrame.RemoveBackEntry();
                LeftFrame.NavigationService.Navigate(new NaturalCreatureNestDetail(), naturalCreatureNest);
                break;
            }
        }

        private void OnNavigatedToInanimateDialog(List<NatureInanimate> naturalCollection, string suggestBoxItemPicture)
        {
            foreach (var itemsControlItem in naturalCollection)
            {
                var naturalInanimate = itemsControlItem;
                if (naturalInanimate == null || naturalInanimate.Picture != suggestBoxItemPicture) continue;
                RightScrollViewer.UpdateLayout();
                var resultList = new List<Button>();
                Global.FindChildren(resultList, RightScrollViewer);
                foreach (var button in resultList)
                {
                    var imageSource = ((Image)((Grid)button.Content).Children[0]).Source.ToString();
                    var imageSourceShort = imageSource.Substring(22, imageSource.Length - 22);
                    if (imageSourceShort != naturalInanimate.Picture) continue;
                    var naturalInanimateButton = button;
                    var currentScrollPosition = RightScrollViewer.VerticalOffset;
                    var point = new Point(0, currentScrollPosition);
                    var targetPosition = naturalInanimateButton.TransformToVisual(RightScrollViewer).Transform(point);
                    RightScrollViewer.ScrollToVerticalOffset(targetPosition.Y);
                    break;
                }
                if(LeftFrame.CanGoBack)LeftFrame.RemoveBackEntry();
                LeftFrame.NavigationService.Navigate(new NaturalInanimateDetail(), naturalInanimate);
                break;
            }
        }

        public NaturalPage()
        {
            InitializeComponent();
            Global.NaturalLeftFrame = LeftFrame;
            Global.RightFrame.NavigationService.LoadCompleted += LoadCompleted;
        }

        public void Deserialize()
        {
            BiomesExpander.DataContext = Global.NaturalBiomesData;
            SmallPlantsExpander.DataContext = Global.NaturalSmallPlantsData;
            TreesExpander.DataContext = Global.NaturalTreesData;
            CreatureNestExpander.DataContext = Global.NaturalCreatureNestData;
            //InanimateExpander.DataContext = Global.NaturalInanimatesData;
        }

        private void NaturalBiomesButton_Click(object sender, RoutedEventArgs e)
        {
            var nature = (NatureBiomes)((Button)sender).DataContext;
            if(LeftFrame.CanGoBack)LeftFrame.RemoveBackEntry();
            LeftFrame.NavigationService.Navigate(new NaturalBiomesDetail(), nature);
        }

        private void NaturalSmallPlantButton_Click(object sender, RoutedEventArgs e)
        {
            var nature = (NatureSmallPlant)((Button)sender).DataContext;
            if(LeftFrame.CanGoBack)LeftFrame.RemoveBackEntry();
            LeftFrame.NavigationService.Navigate(new NaturalSmallPlantDetail(), nature);
        }

        private void NaturalTreeButton_Click(object sender, RoutedEventArgs e)
        {
            var nature = (NatureTree)((Button)sender).DataContext;
            if(LeftFrame.CanGoBack)LeftFrame.RemoveBackEntry();
            LeftFrame.NavigationService.Navigate(new NaturalTreesDetail(), nature);
        }

        private void NaturalCreatureNestButton_Click(object sender, RoutedEventArgs e)
        {
            var nature = (NatureCreatureNest)((Button)sender).DataContext;
            if(LeftFrame.CanGoBack)LeftFrame.RemoveBackEntry();
            LeftFrame.NavigationService.Navigate(new NaturalCreatureNestDetail(), nature);
        }

        private void NaturalInanimateButton_Click(object sender, RoutedEventArgs e)
        {
            var nature = (NatureInanimate)((Button)sender).DataContext;
            if(LeftFrame.CanGoBack)LeftFrame.RemoveBackEntry();
            LeftFrame.NavigationService.Navigate(new NaturalInanimateDetail(), nature);
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
