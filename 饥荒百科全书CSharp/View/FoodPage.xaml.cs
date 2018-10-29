using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
    /// FoodPage.xaml 的交互逻辑
    /// </summary>
    public partial class FoodPage : Page
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
                LeftFrame.NavigationService.Navigate(new FoodRecipeDetail(), Global.FoodRecipeData[0]);
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
                        OnNavigatedToFoodDialog(Global.FoodMeatData, suggestBoxItemPicture);
                        break;
                    case "FoodVegetables":
                        OnNavigatedToFoodDialog(Global.FoodVegetableData, suggestBoxItemPicture);
                        break;
                    case "FoodFruits":
                        OnNavigatedToFoodDialog(Global.FoodFruitData, suggestBoxItemPicture);
                        break;
                    case "FoodEggs":
                        OnNavigatedToFoodDialog(Global.FoodEggData, suggestBoxItemPicture);
                        break;
                    case "FoodOthers":
                        OnNavigatedToFoodDialog(Global.FoodOtherData, suggestBoxItemPicture);
                        break;
                    case "FoodNoFc":
                        OnNavigatedToFoodDialog(Global.FoodNoFcData, suggestBoxItemPicture);
                        break;
                }
            }
        }

        private void OnNavigatedToFoodRecipeDialog(string suggestBoxItemPicture)
        {
            foreach (var itemsControlItem in Global.FoodRecipeData)
            {
                var food = itemsControlItem;
                if (food == null || food.Picture != suggestBoxItemPicture) continue;
                RightScrollViewer.UpdateLayout();
                var resultList = new List<Button>();
                Global.FindChildren(resultList, RightScrollViewer);
                foreach (var button in resultList)
                {
                    var imageSource = ((Image)((Grid)button.Content).Children[0]).Source.ToString();
                    var imageSourceShort = imageSource.Substring(22, imageSource.Length - 22);
                    if (imageSourceShort != food.Picture) continue;
                    var foodButton = button;
                    var currentScrollPosition = RightScrollViewer.VerticalOffset;
                    var point = new Point(0, currentScrollPosition);
                    var targetPosition = foodButton.TransformToVisual(RightScrollViewer).Transform(point);
                    RightScrollViewer.ScrollToVerticalOffset(targetPosition.Y);
                    break;
                }
                if(LeftFrame.CanGoBack)LeftFrame.RemoveBackEntry();
                LeftFrame.NavigationService.Navigate(new FoodRecipeDetail(), food);
                break;
            }
        }

        private void OnNavigatedToFoodDialog(List<Food> foodCollection, string suggestBoxItemPicture)
        {
            foreach (var itemsControlItem in foodCollection)
            {
                var food = itemsControlItem;
                if (food == null || food.Picture != suggestBoxItemPicture) continue;
                RightScrollViewer.UpdateLayout();
                var resultList = new List<Button>();
                Global.FindChildren(resultList, RightScrollViewer);
                foreach (var button in resultList)
                {
                    var imageSource = ((Image)((Grid)button.Content).Children[0]).Source.ToString();
                    var imageSourceShort = imageSource.Substring(22, imageSource.Length - 22);
                    if (imageSourceShort != food.Picture) continue;
                    var foodButton = button;
                    var currentScrollPosition = RightScrollViewer.VerticalOffset;
                    var point = new Point(0, currentScrollPosition);
                    var targetPosition = foodButton.TransformToVisual(RightScrollViewer).Transform(point);
                    RightScrollViewer.ScrollToVerticalOffset(targetPosition.Y);
                    break;
                }
                if(LeftFrame.CanGoBack)LeftFrame.RemoveBackEntry();
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
                RecipesExpander.DataContext = Global.FoodRecipeData;
                MeatsExpander.DataContext = Global.FoodMeatData;
                VegetablesExpander.DataContext = Global.FoodVegetableData;
                FruitsExpander.DataContext = Global.FoodFruitData;
                EggsExpander.DataContext = Global.FoodEggData;
                OtherExpander.DataContext = Global.FoodOtherData;
                NoFcExpander.DataContext = Global.FoodNoFcData;
        }

        private void FoodRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            var foodRecipe = (FoodRecipe2)((Button)sender).DataContext;
            if(LeftFrame.CanGoBack)LeftFrame.RemoveBackEntry();
            LeftFrame.NavigationService.Navigate(new FoodRecipeDetail(), foodRecipe);
        }

        private void FoodButton_Click(object sender, RoutedEventArgs e)
        {
            var food = (Food)((Button)sender).DataContext;
            if(LeftFrame.CanGoBack)LeftFrame.RemoveBackEntry();
            LeftFrame.NavigationService.Navigate(new FoodDetail(), food);
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
