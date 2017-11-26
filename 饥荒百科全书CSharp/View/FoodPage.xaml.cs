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
    /// FoodPage.xaml 的交互逻辑
    /// </summary>
    public partial class FoodPage : Page
    {
        private readonly ObservableCollection<FoodRecipe2> _foodRecipeData = new ObservableCollection<FoodRecipe2>();
        private readonly ObservableCollection<Food> _foodMeatData = new ObservableCollection<Food>();
        private readonly ObservableCollection<Food> _foodVegetableData = new ObservableCollection<Food>();
        private readonly ObservableCollection<Food> _foodFruitData = new ObservableCollection<Food>();
        private readonly ObservableCollection<Food> _foodEggData = new ObservableCollection<Food>();
        private readonly ObservableCollection<Food> _foodOtherData = new ObservableCollection<Food>();
        private readonly ObservableCollection<Food> _foodNoFcData = new ObservableCollection<Food>();

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
                LeftFrame.NavigationService.Navigate(new FoodRecipeDetail(), _foodRecipeData[0]);
            }
            else
            {
                var suggestBoxItemPicture = extraData[1];
                //导航到指定页面
                switch (extraData[0])
                {
                    case "FoodRecipe":
                        OnNavigatedToFoodRecipeDialog(suggestBoxItemPicture);
                        break;
                    case "FoodMeats":
                        OnNavigatedToFoodDialog(_foodMeatData, suggestBoxItemPicture);
                        break;
                    case "FoodVegetables":
                        OnNavigatedToFoodDialog(_foodVegetableData, suggestBoxItemPicture);
                        break;
                    case "FoodFruits":
                        OnNavigatedToFoodDialog(_foodFruitData, suggestBoxItemPicture);
                        break;
                    case "FoodEggs":
                        OnNavigatedToFoodDialog(_foodEggData, suggestBoxItemPicture);
                        break;
                    case "FoodOthers":
                        OnNavigatedToFoodDialog(_foodOtherData, suggestBoxItemPicture);
                        break;
                    case "FoodNoFc":
                        OnNavigatedToFoodDialog(_foodNoFcData, suggestBoxItemPicture);
                        break;
                }
            }
        }

        private void OnNavigatedToFoodRecipeDialog(string suggestBoxItemPicture)
        {
            foreach (var itemsControlItem in _foodRecipeData)
            {
                var food = itemsControlItem;
                if (food == null || food.Picture != suggestBoxItemPicture) continue;
                LeftFrame.NavigationService.Navigate(new FoodRecipeDetail(), food);
                break;
            }
        }

        private void OnNavigatedToFoodDialog(ObservableCollection<Food> foodCollection, string suggestBoxItemPicture)
        {
            foreach (var itemsControlItem in foodCollection)
            {
                var food = itemsControlItem;
                if (food == null || food.Picture != suggestBoxItemPicture) continue;
                LeftFrame.NavigationService.Navigate(new FoodDetail(), food);
                break;
            }
        }

        public FoodPage()
        {
            InitializeComponent();
            Global.FoodLeftFrame = LeftFrame;
            Global.RightFrame.NavigationService.LoadCompleted += LoadCompleted;
        }

        public void Deserialize()
        {
            _foodRecipeData.Clear();
            _foodMeatData.Clear();
            _foodVegetableData.Clear();
            _foodFruitData.Clear();
            _foodEggData.Clear();
            _foodOtherData.Clear();
            _foodNoFcData.Clear();
            var food = JsonConvert.DeserializeObject<FoodRootObject>(StringProcess.GetJsonString("Foods.json"));
            foreach (var foodRecipeItems in food.FoodRecipe.FoodRecipes)
            {
                _foodRecipeData.Add(foodRecipeItems);
            }
            foreach (var foodRecipeItems in _foodRecipeData)
            {
                foodRecipeItems.Picture = StringProcess.GetGameResourcePath(foodRecipeItems.Picture);
            }
            foreach (var foodMeatsItems in food.FoodMeats.Foods)
            {
                _foodMeatData.Add(foodMeatsItems);
            }
            foreach (var foodMeatsItems in _foodMeatData)
            {
                foodMeatsItems.Picture = StringProcess.GetGameResourcePath(foodMeatsItems.Picture);
            }
            foreach (var foodVegetablesItems in food.FoodVegetables.Foods)
            {
                _foodVegetableData.Add(foodVegetablesItems);
            }
            foreach (var foodVegetablesItems in _foodVegetableData)
            {
                foodVegetablesItems.Picture = StringProcess.GetGameResourcePath(foodVegetablesItems.Picture);
            }
            foreach (var foodFruitItems in food.FoodFruit.Foods)
            {
                _foodFruitData.Add(foodFruitItems);
            }
            foreach (var foodFruitItems in _foodFruitData)
            {
                foodFruitItems.Picture = StringProcess.GetGameResourcePath(foodFruitItems.Picture);
            }
            foreach (var foodEggsItems in food.FoodEggs.Foods)
            {
                _foodEggData.Add(foodEggsItems);
            }
            foreach (var foodEggsItems in _foodEggData)
            {
                foodEggsItems.Picture = StringProcess.GetGameResourcePath(foodEggsItems.Picture);
            }
            foreach (var foodOthersItems in food.FoodOthers.Foods)
            {
                _foodOtherData.Add(foodOthersItems);
            }
            foreach (var foodOthersItems in _foodOtherData)
            {
                foodOthersItems.Picture = StringProcess.GetGameResourcePath(foodOthersItems.Picture);
            }
            foreach (var foodNoFcItems in food.FoodNoFc.Foods)
            {
                _foodNoFcData.Add(foodNoFcItems);
            }
            foreach (var foodNoFcItems in _foodNoFcData)
            {
                foodNoFcItems.Picture = StringProcess.GetGameResourcePath(foodNoFcItems.Picture);
            }
            RecipesExpander.DataContext = _foodRecipeData;
            MeatsExpander.DataContext = _foodMeatData;
            VegetablesExpander.DataContext = _foodVegetableData;
            FruitsExpander.DataContext = _foodFruitData;
            EggsExpander.DataContext = _foodEggData;
            OtherExpander.DataContext = _foodOtherData;
            NoFcExpander.DataContext = _foodNoFcData;
        }

        private void FoodRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            var foodRecipe = (FoodRecipe2)((Button)sender).DataContext;
            LeftFrame.NavigationService.Navigate(new FoodRecipeDetail(), foodRecipe);
        }

        private void FoodButton_Click(object sender, RoutedEventArgs e)
        {
            var food = (Food)((Button)sender).DataContext;
            LeftFrame.NavigationService.Navigate(new FoodDetail(), food);
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UiSplitter.Height = ActualHeight;
        }
    }
}
