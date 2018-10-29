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
    /// GoodPage.xaml 的交互逻辑
    /// </summary>
    public partial class GoodPage : Page
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
                LeftFrame.NavigationService.Navigate(new GoodMaterialDetail(), Global.GoodMaterialData[0]);
            }
            else
            {
                //导航到指定页面
                var suggestBoxItemPicture = extraData[1];
                switch (extraData[0])
                {
                    case "GoodMaterial":
                        OnNavigatedToGoodMaterialDialog(suggestBoxItemPicture);
                        break;
                    case "GoodEquipment":
                        OnNavigatedToGoodEquipmentDialog(suggestBoxItemPicture);
                        break;
                    case "GoodSapling":
                        OnNavigatedToGoodSaplingDialog(suggestBoxItemPicture);
                        break;
                    case "GoodCreatures":
                        OnNavigatedToGoodCreaturesDialog(suggestBoxItemPicture);
                        break;
                    case "GoodTrinkets":
                        OnNavigatedToGoodDialog(Global.GoodTrinketsData, suggestBoxItemPicture);
                        break;
                    case "GoodTurf":
                        OnNavigatedToGoodTurfDialog(suggestBoxItemPicture);
                        break;
                    case "GoodPet":
                        OnNavigatedToGoodPetDialog(suggestBoxItemPicture);
                        break;
                    case "GoodUnlock":
                        OnNavigatedToGoodUnlockDialog(suggestBoxItemPicture);
                        break;
                    case "GoodHallowedNights":
                        OnNavigatedToGoodDialog(Global.GoodHallowedNightsData, suggestBoxItemPicture);
                        break;
                    case "GoodWintersFeast":
                        OnNavigatedToGoodDialog(Global.GoodWintersFeastData, suggestBoxItemPicture);
                        break;
                    case "GoodYearOfTheGobbler":
                        OnNavigatedToGoodDialog(Global.GoodYearOfTheGobblerData, suggestBoxItemPicture);
                        break;
                    case "GoodComponent":
                        OnNavigatedToGoodDialog(Global.GoodComponentData, suggestBoxItemPicture);
                        break;
                    case "GoodOthers":
                        OnNavigatedToGoodDialog(Global.GoodOthersData, suggestBoxItemPicture);
                        break;
                }
            }
        }

        private void OnNavigatedToGoodMaterialDialog(string suggestBoxItemPicture)
        {
            foreach (var gridViewItem in Global.GoodMaterialData)
            {
                var good = gridViewItem;
                if (good == null || good.Picture != suggestBoxItemPicture) continue;
                RightScrollViewer.UpdateLayout();
                var resultList = new List<Button>();
                Global.FindChildren(resultList, RightScrollViewer);
                foreach (var button in resultList)
                {
                    var imageSource = ((Image)((Grid)button.Content).Children[0]).Source.ToString();
                    var imageSourceShort = imageSource.Substring(22, imageSource.Length - 22);
                    if (imageSourceShort != good.Picture) continue;
                    var goodButton = button;
                    var currentScrollPosition = RightScrollViewer.VerticalOffset;
                    var point = new Point(0, currentScrollPosition);
                    var targetPosition = goodButton.TransformToVisual(RightScrollViewer).Transform(point);
                    RightScrollViewer.ScrollToVerticalOffset(targetPosition.Y);
                    break;
                }
                LeftFrame.NavigationService.Navigate(new GoodMaterialDetail(), good);
                break;
            }
        }

        private void OnNavigatedToGoodEquipmentDialog(string suggestBoxItemPicture)
        {
            foreach (var gridViewItem in Global.GoodEquipmentData)
            {
                var good = gridViewItem;
                if (good == null || good.Picture != suggestBoxItemPicture) continue;
                RightScrollViewer.UpdateLayout();
                var resultList = new List<Button>();
                Global.FindChildren(resultList, RightScrollViewer);
                foreach (var button in resultList)
                {
                    var imageSource = ((Image)((Grid)button.Content).Children[0]).Source.ToString();
                    var imageSourceShort = imageSource.Substring(22, imageSource.Length - 22);
                    if (imageSourceShort != good.Picture) continue;
                    var goodButton = button;
                    var currentScrollPosition = RightScrollViewer.VerticalOffset;
                    var point = new Point(0, currentScrollPosition);
                    var targetPosition = goodButton.TransformToVisual(RightScrollViewer).Transform(point);
                    RightScrollViewer.ScrollToVerticalOffset(targetPosition.Y);
                    break;
                }
                LeftFrame.NavigationService.Navigate(new GoodEquipmentDetail(), good);
                break;
            }
        }

        private void OnNavigatedToGoodSaplingDialog(string suggestBoxItemPicture)
        {
            foreach (var gridViewItem in Global.GoodSaplingData)
            {
                var good = gridViewItem;
                if (good == null || good.Picture != suggestBoxItemPicture) continue;
                RightScrollViewer.UpdateLayout();
                var resultList = new List<Button>();
                Global.FindChildren(resultList, RightScrollViewer);
                foreach (var button in resultList)
                {
                    var imageSource = ((Image)((Grid)button.Content).Children[0]).Source.ToString();
                    var imageSourceShort = imageSource.Substring(22, imageSource.Length - 22);
                    if (imageSourceShort != good.Picture) continue;
                    var goodButton = button;
                    var currentScrollPosition = RightScrollViewer.VerticalOffset;
                    var point = new Point(0, currentScrollPosition);
                    var targetPosition = goodButton.TransformToVisual(RightScrollViewer).Transform(point);
                    RightScrollViewer.ScrollToVerticalOffset(targetPosition.Y);
                    break;
                }
                LeftFrame.NavigationService.Navigate(new GoodSaplingDetail(), good);
                break;
            }
        }

        private void OnNavigatedToGoodCreaturesDialog(string suggestBoxItemPicture)
        {
            foreach (var gridViewItem in Global.GoodCreaturesData)
            {
                var good = gridViewItem;
                if (good == null || good.Picture != suggestBoxItemPicture) continue;
                RightScrollViewer.UpdateLayout();
                var resultList = new List<Button>();
                Global.FindChildren(resultList, RightScrollViewer);
                foreach (var button in resultList)
                {
                    var imageSource = ((Image)((Grid)button.Content).Children[0]).Source.ToString();
                    var imageSourceShort = imageSource.Substring(22, imageSource.Length - 22);
                    if (imageSourceShort != good.Picture) continue;
                    var goodButton = button;
                    var currentScrollPosition = RightScrollViewer.VerticalOffset;
                    var point = new Point(0, currentScrollPosition);
                    var targetPosition = goodButton.TransformToVisual(RightScrollViewer).Transform(point);
                    RightScrollViewer.ScrollToVerticalOffset(targetPosition.Y);
                    break;
                }
                LeftFrame.NavigationService.Navigate(new GoodCreaturesDetail(), good);
                break;
            }
        }

        private void OnNavigatedToGoodTurfDialog(string suggestBoxItemPicture)
        {
            foreach (var gridViewItem in Global.GoodTurfData)
            {
                var good = gridViewItem;
                if (good == null || good.Picture != suggestBoxItemPicture) continue;
                RightScrollViewer.UpdateLayout();
                var resultList = new List<Button>();
                Global.FindChildren(resultList, RightScrollViewer);
                foreach (var button in resultList)
                {
                    var imageSource = ((Image)((Grid)button.Content).Children[0]).Source.ToString();
                    var imageSourceShort = imageSource.Substring(22, imageSource.Length - 22);
                    if (imageSourceShort != good.Picture) continue;
                    var goodButton = button;
                    var currentScrollPosition = RightScrollViewer.VerticalOffset;
                    var point = new Point(0, currentScrollPosition);
                    var targetPosition = goodButton.TransformToVisual(RightScrollViewer).Transform(point);
                    RightScrollViewer.ScrollToVerticalOffset(targetPosition.Y);
                    break;
                }
                LeftFrame.NavigationService.Navigate(new GoodTurfDetail(), good);
                break;
            }
        }

        private void OnNavigatedToGoodPetDialog(string suggestBoxItemPicture)
        {
            foreach (var gridViewItem in Global.GoodPetData)
            {
                var good = gridViewItem;
                if (good == null || good.Picture != suggestBoxItemPicture) continue;
                RightScrollViewer.UpdateLayout();
                var resultList = new List<Button>();
                Global.FindChildren(resultList, RightScrollViewer);
                foreach (var button in resultList)
                {
                    var imageSource = ((Image)((Grid)button.Content).Children[0]).Source.ToString();
                    var imageSourceShort = imageSource.Substring(22, imageSource.Length - 22);
                    if (imageSourceShort != good.Picture) continue;
                    var goodButton = button;
                    var currentScrollPosition = RightScrollViewer.VerticalOffset;
                    var point = new Point(0, currentScrollPosition);
                    var targetPosition = goodButton.TransformToVisual(RightScrollViewer).Transform(point);
                    RightScrollViewer.ScrollToVerticalOffset(targetPosition.Y);
                    break;
                }
                LeftFrame.NavigationService.Navigate(new GoodPetDetail(), good);
                break;
            }
        }

        private void OnNavigatedToGoodUnlockDialog(string suggestBoxItemPicture)
        {
            foreach (var gridViewItem in Global.GoodUnlockData)
            {
                var good = gridViewItem;
                if (good == null || good.Picture != suggestBoxItemPicture) continue;
                RightScrollViewer.UpdateLayout();
                var resultList = new List<Button>();
                Global.FindChildren(resultList, RightScrollViewer);
                foreach (var button in resultList)
                {
                    var imageSource = ((Image)((Grid)button.Content).Children[0]).Source.ToString();
                    var imageSourceShort = imageSource.Substring(22, imageSource.Length - 22);
                    if (imageSourceShort != good.Picture) continue;
                    var goodButton = button;
                    var currentScrollPosition = RightScrollViewer.VerticalOffset;
                    var point = new Point(0, currentScrollPosition);
                    var targetPosition = goodButton.TransformToVisual(RightScrollViewer).Transform(point);
                    RightScrollViewer.ScrollToVerticalOffset(targetPosition.Y);
                    break;
                }
                LeftFrame.NavigationService.Navigate(new GoodUnlockDetail(), good);
                break;
            }
        }

        private void OnNavigatedToGoodDialog(List<Good> goodCollection, string suggestBoxItemPicture)
        {
            foreach (var gridViewItem in goodCollection)
            {
                var good = gridViewItem;
                if (good == null || good.Picture != suggestBoxItemPicture) continue;
                RightScrollViewer.UpdateLayout();
                var resultList = new List<Button>();
                Global.FindChildren(resultList, RightScrollViewer);
                foreach (var button in resultList)
                {
                    var imageSource = ((Image)((Grid)button.Content).Children[0]).Source.ToString();
                    var imageSourceShort = imageSource.Substring(22, imageSource.Length - 22);
                    if (imageSourceShort != good.Picture) continue;
                    var goodButton = button;
                    var currentScrollPosition = RightScrollViewer.VerticalOffset;
                    var point = new Point(0, currentScrollPosition);
                    var targetPosition = goodButton.TransformToVisual(RightScrollViewer).Transform(point);
                    RightScrollViewer.ScrollToVerticalOffset(targetPosition.Y);
                    break;
                }
                LeftFrame.NavigationService.Navigate(new GoodDetail(), good);
                break;
            }
        }

        public GoodPage()
        {
            InitializeComponent();
            Global.GoodLeftFrame = LeftFrame;
            Global.RightFrame.NavigationService.LoadCompleted += LoadCompleted;
            if (Global.GameVersion == 0 || Global.GameVersion == 1)
            {
                UnlockExpander.Visibility = Visibility.Collapsed;
                ComponentExpander.Visibility = Visibility.Collapsed;
            }
            else
            {
                HallowedNightsExpander.Visibility = Visibility.Collapsed;
                WintersFeastExpander.Visibility = Visibility.Collapsed;
                YearOfTheGobblerExpander.Visibility = Visibility.Collapsed;
            }
            // DS版隐藏解锁
            if (Global.GameVersion == 2)
            {
                UnlockExpander.Visibility = Visibility.Collapsed;
            }
        }

        public void Deserialize()
        {
            MaterialExpander.DataContext = Global.GoodMaterialData;
            EquipmentExpander.DataContext = Global.GoodEquipmentData;
            SaplingExpander.DataContext = Global.GoodSaplingData;
            CreaturesExpander.DataContext = Global.GoodCreaturesData;
            TrinketsExpander.DataContext = Global.GoodTrinketsData;
            TurfExpander.DataContext = Global.GoodTurfData;
            PetExpander.DataContext = Global.GoodPetData;
            UnlockExpander.DataContext = Global.GoodUnlockData;
            HallowedNightsExpander.DataContext = Global.GoodHallowedNightsData;
            WintersFeastExpander.DataContext = Global.GoodWintersFeastData;
            YearOfTheGobblerExpander.DataContext = Global.GoodYearOfTheGobblerData;
            ComponentExpander.DataContext = Global.GoodComponentData;
            GoodOthersExpander.DataContext = Global.GoodOthersData;
        }

        private void GoodMaterialButton_Click(object sender, RoutedEventArgs e)
        {
            var good = (GoodMaterial)((Button)sender).DataContext;
            LeftFrame.NavigationService.Navigate(new GoodMaterialDetail(), good);
        }

        private void GoodEquipmentButton_Click(object sender, RoutedEventArgs e)
        {
            var good = (GoodEquipment)((Button)sender).DataContext;
            LeftFrame.NavigationService.Navigate(new GoodEquipmentDetail(), good);
        }

        private void GoodSaplingButton_Click(object sender, RoutedEventArgs e)
        {
            var good = (GoodSapling)((Button)sender).DataContext;
            LeftFrame.NavigationService.Navigate(new GoodSaplingDetail(), good);
        }

        private void GoodCreaturesButton_Click(object sender, RoutedEventArgs e)
        {
            var good = (GoodCreatures)((Button)sender).DataContext;
            LeftFrame.NavigationService.Navigate(new GoodCreaturesDetail(), good);
        }

        private void GoodTurfButton_Click(object sender, RoutedEventArgs e)
        {
            var good = (GoodTurf)((Button)sender).DataContext;
            LeftFrame.NavigationService.Navigate(new GoodTurfDetail(), good);
        }

        private void GoodPetButton_Click(object sender, RoutedEventArgs e)
        {
            var good = (GoodPet)((Button)sender).DataContext;
            LeftFrame.NavigationService.Navigate(new GoodPetDetail(), good);
        }

        private void GoodUnlockButton_Click(object sender, RoutedEventArgs e)
        {
            var good = (GoodUnlock)((Button)sender).DataContext;
            LeftFrame.NavigationService.Navigate(new GoodUnlockDetail(), good);
        }

        private void GoodButton_Click(object sender, RoutedEventArgs e)
        {
            var good = (Good)((Button)sender).DataContext;
            LeftFrame.NavigationService.Navigate(new GoodDetail(), good);
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
