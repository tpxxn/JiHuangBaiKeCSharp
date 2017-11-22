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
    /// GoodPage.xaml 的交互逻辑
    /// </summary>
    public partial class GoodPage : Page
    {

        private readonly ObservableCollection<GoodMaterial> _goodMaterialData = new ObservableCollection<GoodMaterial>();
        private readonly ObservableCollection<GoodEquipment> _goodEquipmentData = new ObservableCollection<GoodEquipment>();
        private readonly ObservableCollection<GoodSapling> _goodSaplingData = new ObservableCollection<GoodSapling>();
        private readonly ObservableCollection<GoodCreatures> _goodCreaturesData = new ObservableCollection<GoodCreatures>();
        private readonly ObservableCollection<Good> _goodTrinketsData = new ObservableCollection<Good>();
        private readonly ObservableCollection<GoodTurf> _goodTurfData = new ObservableCollection<GoodTurf>();
        private readonly ObservableCollection<GoodPet> _goodPetData = new ObservableCollection<GoodPet>();
        private readonly ObservableCollection<GoodUnlock> _goodUnlockData = new ObservableCollection<GoodUnlock>();
        private readonly ObservableCollection<Good> _goodHallowedNightsData = new ObservableCollection<Good>();
        private readonly ObservableCollection<Good> _goodWintersFeastData = new ObservableCollection<Good>();
        private readonly ObservableCollection<Good> _goodYearOfTheGobblerData = new ObservableCollection<Good>();
        private readonly ObservableCollection<Good> _goodComponentData = new ObservableCollection<Good>();
        private readonly ObservableCollection<Good> _goodOthersData = new ObservableCollection<Good>();

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
                LeftFrame.NavigationService.Navigate(new GoodMaterialDetail(), _goodMaterialData[0]);
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
                        OnNavigatedToGoodDialog(_goodTrinketsData, suggestBoxItemPicture);
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
                        OnNavigatedToGoodDialog(_goodHallowedNightsData, suggestBoxItemPicture);
                        break;
                    case "GoodWintersFeast":
                        OnNavigatedToGoodDialog(_goodWintersFeastData, suggestBoxItemPicture);
                        break;
                    case "GoodYearOfTheGobbler":
                        OnNavigatedToGoodDialog(_goodYearOfTheGobblerData, suggestBoxItemPicture);
                        break;
                    case "GoodComponent":
                        OnNavigatedToGoodDialog(_goodComponentData, suggestBoxItemPicture);
                        break;
                    case "GoodOthers":
                        OnNavigatedToGoodDialog(_goodOthersData, suggestBoxItemPicture);
                        break;
                }
            }
        }

        private void OnNavigatedToGoodMaterialDialog(string suggestBoxItemPicture)
        {
            foreach (var gridViewItem in _goodMaterialData)
            {
                var good = gridViewItem;
                if (good == null || good.Picture != suggestBoxItemPicture) continue;
                LeftFrame.NavigationService.Navigate(new GoodMaterialDetail(), good);
                break;
            }
        }

        private void OnNavigatedToGoodEquipmentDialog(string suggestBoxItemPicture)
        {
            foreach (var gridViewItem in _goodEquipmentData)
            {
                var good = gridViewItem;
                if (good == null || good.Picture != suggestBoxItemPicture) continue;
                LeftFrame.NavigationService.Navigate(new GoodEquipmentDetail(), good);
                break;
            }
        }

        private void OnNavigatedToGoodSaplingDialog(string suggestBoxItemPicture)
        {
            foreach (var gridViewItem in _goodSaplingData)
            {
                var good = gridViewItem;
                if (good == null || good.Picture != suggestBoxItemPicture) continue;
                LeftFrame.NavigationService.Navigate(new GoodSaplingDetail(), good);
                break;
            }
        }

        private void OnNavigatedToGoodCreaturesDialog(string suggestBoxItemPicture)
        {
            foreach (var gridViewItem in _goodCreaturesData)
            {
                var good = gridViewItem;
                if (good == null || good.Picture != suggestBoxItemPicture) continue;
                LeftFrame.NavigationService.Navigate(new GoodCreaturesDetail(), good);
                break;
            }
        }

        private void OnNavigatedToGoodTurfDialog(string suggestBoxItemPicture)
        {
            foreach (var gridViewItem in _goodTurfData)
            {
                var good = gridViewItem;
                if (good == null || good.Picture != suggestBoxItemPicture) continue;
                LeftFrame.NavigationService.Navigate(new GoodTurfDetail(), good);
                break;
            }
        }

        private void OnNavigatedToGoodPetDialog(string suggestBoxItemPicture)
        {
            foreach (var gridViewItem in _goodPetData)
            {
                var good = gridViewItem;
                if (good == null || good.Picture != suggestBoxItemPicture) continue;
                LeftFrame.NavigationService.Navigate(new GoodPetDetail(), good);
                break;
            }
        }

        private void OnNavigatedToGoodUnlockDialog(string suggestBoxItemPicture)
        {
            foreach (var gridViewItem in _goodUnlockData)
            {
                var good = gridViewItem;
                if (good == null || good.Picture != suggestBoxItemPicture) continue;
                LeftFrame.NavigationService.Navigate(new GoodUnlockDetail(), good);
                break;
            }
        }

        private void OnNavigatedToGoodDialog(ObservableCollection<Good> goodCollection, string suggestBoxItemPicture)
        {
            foreach (var gridViewItem in goodCollection)
            {
                var good = gridViewItem;
                if (good == null || good.Picture != suggestBoxItemPicture) continue;
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
            Deserialize();
        }

        public void Deserialize()
        {
            _goodMaterialData.Clear();
            _goodEquipmentData.Clear();
            _goodSaplingData.Clear();
            _goodCreaturesData.Clear();
            _goodTrinketsData.Clear();
            _goodTurfData.Clear();
            _goodPetData.Clear();
            _goodUnlockData.Clear();
            _goodHallowedNightsData.Clear();
            _goodWintersFeastData.Clear();
            _goodYearOfTheGobblerData.Clear();
            _goodComponentData.Clear();
            _goodOthersData.Clear();
            var good = JsonConvert.DeserializeObject<GoodsRootObject>(StringProcess.GetJsonString("Goods.json"));
            foreach (var goodMaterialItems in good.Material.GoodMaterial)
            {
                _goodMaterialData.Add(goodMaterialItems);
            }
            foreach (var goodMaterialItems in _goodMaterialData)
            {
                goodMaterialItems.Picture = StringProcess.GetGameResourcePath(goodMaterialItems.Picture);
            }
            foreach (var goodEquipmentItems in good.Equipment.GoodEquipment)
            {
                _goodEquipmentData.Add(goodEquipmentItems);
            }
            foreach (var goodEquipmentItems in _goodEquipmentData)
            {
                goodEquipmentItems.Picture = StringProcess.GetGameResourcePath(goodEquipmentItems.Picture);
            }
            foreach (var goodSaplingItems in good.Sapling.GoodSapling)
            {
                _goodSaplingData.Add(goodSaplingItems);
            }
            foreach (var goodSaplingItems in _goodSaplingData)
            {
                goodSaplingItems.Picture = StringProcess.GetGameResourcePath(goodSaplingItems.Picture);
            }
            foreach (var goodCreaturesItems in good.Creatures.GoodCreatures)
            {
                _goodCreaturesData.Add(goodCreaturesItems);
            }
            foreach (var goodCreaturesItems in _goodCreaturesData)
            {
                goodCreaturesItems.Picture = StringProcess.GetGameResourcePath(goodCreaturesItems.Picture);
            }
            foreach (var goodTrinketsItems in good.Trinkets.GoodTrinkets)
            {
                _goodTrinketsData.Add(goodTrinketsItems);
            }
            foreach (var goodTrinketsItems in _goodTrinketsData)
            {
                goodTrinketsItems.Picture = StringProcess.GetGameResourcePath(goodTrinketsItems.Picture);
            }
            foreach (var goodTurfItems in good.Turf.GoodTurf)
            {
                _goodTurfData.Add(goodTurfItems);
            }
            foreach (var goodTurfItems in _goodTurfData)
            {
                goodTurfItems.Picture = StringProcess.GetGameResourcePath(goodTurfItems.Picture);
            }
            foreach (var goodPetItems in good.Pet.GoodPet)
            {
                _goodPetData.Add(goodPetItems);
            }
            foreach (var goodPetItems in _goodPetData)
            {
                goodPetItems.Picture = StringProcess.GetGameResourcePath(goodPetItems.Picture);
            }
            foreach (var goodUnlockItems in good.Unlock.GoodUnlock)
            {
                _goodUnlockData.Add(goodUnlockItems);
            }
            foreach (var goodUnlockItems in _goodUnlockData)
            {
                goodUnlockItems.Picture = StringProcess.GetGameResourcePath(goodUnlockItems.Picture);
            }
            foreach (var goodHallowedNightsItems in good.HallowedNights.Good)
            {
                _goodHallowedNightsData.Add(goodHallowedNightsItems);
            }
            foreach (var goodHallowedNightsItems in _goodHallowedNightsData)
            {
                goodHallowedNightsItems.Picture = StringProcess.GetGameResourcePath(goodHallowedNightsItems.Picture);
            }
            foreach (var goodWintersFeastItems in good.WintersFeast.Good)
            {
                _goodWintersFeastData.Add(goodWintersFeastItems);
            }
            foreach (var goodWintersFeastItems in _goodWintersFeastData)
            {
                goodWintersFeastItems.Picture = StringProcess.GetGameResourcePath(goodWintersFeastItems.Picture);
            }
            foreach (var goodYearOfTheGobblerItems in good.YearOfTheGobbler.Good)
            {
                _goodYearOfTheGobblerData.Add(goodYearOfTheGobblerItems);
            }
            foreach (var goodYearOfTheGobblerItems in _goodYearOfTheGobblerData)
            {
                goodYearOfTheGobblerItems.Picture = StringProcess.GetGameResourcePath(goodYearOfTheGobblerItems.Picture);
            }
            foreach (var goodComponentItems in good.Component.Good)
            {
                _goodComponentData.Add(goodComponentItems);
            }
            foreach (var goodComponentItems in _goodComponentData)
            {
                goodComponentItems.Picture = StringProcess.GetGameResourcePath(goodComponentItems.Picture);
            }
            foreach (var goodGoodOthersItems in good.GoodOthers.Good)
            {
                _goodOthersData.Add(goodGoodOthersItems);
            }
            foreach (var goodGoodOthersItems in _goodOthersData)
            {
                goodGoodOthersItems.Picture = StringProcess.GetGameResourcePath(goodGoodOthersItems.Picture);
            }
            MaterialExpander.DataContext = _goodMaterialData;
            EquipmentExpander.DataContext = _goodEquipmentData;
            SaplingExpander.DataContext = _goodSaplingData;
            CreaturesExpander.DataContext = _goodCreaturesData;
            TrinketsExpander.DataContext = _goodTrinketsData;
            TurfExpander.DataContext = _goodTurfData;
            PetExpander.DataContext = _goodPetData;
            UnlockExpander.DataContext = _goodUnlockData;
            HallowedNightsExpander.DataContext = _goodHallowedNightsData;
            WintersFeastExpander.DataContext = _goodWintersFeastData;
            YearOfTheGobblerExpander.DataContext = _goodYearOfTheGobblerData;
            ComponentExpander.DataContext = _goodComponentData;
            GoodOthersExpander.DataContext = _goodOthersData;
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
    }
}
