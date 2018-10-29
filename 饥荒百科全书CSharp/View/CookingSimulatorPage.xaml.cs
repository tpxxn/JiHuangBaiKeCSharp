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

namespace 饥荒百科全书CSharp.View
{
    /// <summary>
    /// CookingSimulatorPage.xaml 的交互逻辑
    /// </summary>
    public partial class CookingSimulatorPage : Page
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
            CookingSimulatorLeftScrollViewer.FontWeight = Global.FontWeight;
            RightScrollViewer.FontWeight = Global.FontWeight;
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
        }

        public CookingSimulatorPage()
        {
            InitializeComponent();
            if (Global.GameVersion != 4)
            {
                CrockpotComboBox.Visibility = Visibility.Collapsed;
            }
            FoodHealth.ShowIfZero = true;
            FoodHunger.ShowIfZero = true;
            FoodSanity.ShowIfZero = true;
            FoodRecipeHealth.ShowIfZero = true;
            FoodRecipeHunger.ShowIfZero = true;
            FoodRecipeSanity.ShowIfZero = true;
            FoodHealth.Value = 0;
            FoodHunger.Value = 0;
            FoodSanity.Value = 0;
            FoodRecipeHealth.Value = 0;
            FoodRecipeHunger.Value = 0;
            FoodRecipeSanity.Value = 0;
            FoodHealth.BarColor = Global.ColorGreen;
            FoodHunger.BarColor = Global.ColorKhaki;
            FoodSanity.BarColor = Global.ColorRed;
            FoodRecipeHealth.BarColor = Global.ColorGreen;
            FoodRecipeHunger.BarColor = Global.ColorKhaki;
            FoodRecipeSanity.BarColor = Global.ColorRed;
            Global.RightFrame.NavigationService.LoadCompleted += LoadCompleted;
            Deserialize();
        }

        public void Deserialize()
        {
            MeatsExpander.DataContext = Global.FoodMeatData;
            VegetablesExpander.DataContext = Global.FoodVegetableData;
            FruitsExpander.DataContext = Global.FoodFruitData;
            EggsExpander.DataContext = Global.FoodEggData;
            OtherExpander.DataContext = Global.FoodOtherData;
        }

        private void FoodButton_Click(object sender, RoutedEventArgs e)
        {
            var food = (Food)((Button)sender).DataContext;
            AddFood(food.Picture);
        }

        #region 变量初始化
        /// <summary>
        /// 重置
        /// </summary>
        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteFood(null, null);
            FoodResultImage.Source = null;
            FoodResultTextBlock.Text = "";
            _crockPotList.Clear();
            _crockPotListIndex = -1;
            _crockPotMaxPriority = -128;
            FoodHealth.Value = 0;
            FoodHunger.Value = 0;
            FoodSanity.Value = 0;
            FoodRecipeHealth.Value = 0;
            FoodRecipeHunger.Value = 0;
            FoodRecipeSanity.Value = 0;
        }
        /// <summary>
        /// 四个位置
        /// </summary>
        private string _csRecipe1 = "";
        private string _csRecipe2 = "";
        private string _csRecipe3 = "";
        private string _csRecipe4 = "";
        /// <summary>
        /// 43种食材
        /// </summary>
        private double _csFtEggs;
        private double _csFtVegetables;
        private double _csFtFruit;
        private double _csFtBanana;
        private double _csFtBerries;
        private double _csFtButter;
        private double _csFtButterflyWings;
        private double _csFtCactusFlesh;
        private double _csFtCactusFlower;
        private double _csFtCorn;
        private double _csFtDairyProduct;
        private double _csFtDragonFruit;
        private double _csFtDrumstick;
        private double _csFtEel;
        private double _csFtEggplant;
        private double _csFtFishes;
        private double _csFtFrogLegs;
        private double _csFtHoney;
        private double _csFtIce;
        private double _csFtJellyfish;
        private double _csFtLichen;
        private double _csFtLimpets;
        private double _csFtMandrake;
        private double _csFtMeats;
        private double _csFtMoleworm;
        private double _csFtMonsterFoods;
        private double _csFtMussel;
        private double _csFtPumpkin;
        private double _csFtRoastedBirchnut;
        private double _csFtRoastedCoffeeBeans;
        private double _csFtSeaweed;
        private double _csFtSharkFin;
        private double _csFtSweetener;
        private double _csFtSweetPotato;
        private double _csFtTwigs;
        private double _csFtWatermelon;
        private double _csFtWobster;
        private double _csFtRoyalJelly;
        private double _csFtRoe;
        private double _csFtRoeCooked;
        private double _csFtNeonQuattro;
        private double _csFtPierrotFish;
        private double _csFtPurpleGrouper;

        private byte _foodIndex;
        private string _csFoodName = "";

        /// <summary>
        /// 食物列表
        /// </summary>
        private readonly List<string> _crockPotList = new List<string>();
        /// <summary>
        /// 食物列表下标
        /// </summary>
        private sbyte _crockPotListIndex = -1;
        /// <summary>
        /// 优先度最大值
        /// </summary>
        private sbyte _crockPotMaxPriority = -128;
        #endregion

        /// <summary>
        /// 添加食材
        /// </summary>
        /// <param name="foodName">食材名称</param>
        private void AddFood(string foodName)
        {
            if (string.IsNullOrEmpty(_csRecipe1) && string.IsNullOrEmpty(_csRecipe2) && string.IsNullOrEmpty(_csRecipe3) && string.IsNullOrEmpty(_csRecipe4))
            {
                FoodHealth.Value = 0;
                FoodHunger.Value = 0;
                FoodSanity.Value = 0;
            }
            if (string.IsNullOrEmpty(_csRecipe1) || string.IsNullOrEmpty(_csRecipe2) || string.IsNullOrEmpty(_csRecipe3) || string.IsNullOrEmpty(_csRecipe4))
            {
                CS_Food_Property(foodName);
            }
            if (string.IsNullOrEmpty(_csRecipe1))
            {
                _csRecipe1 = StringProcess.GetFileName(foodName);
                Food1Image.Source = new BitmapImage(new Uri(foodName, UriKind.Relative));
            }
            else if (string.IsNullOrEmpty(_csRecipe2))
            {
                _csRecipe2 = StringProcess.GetFileName(foodName);
                Food2Image.Source = new BitmapImage(new Uri(foodName, UriKind.Relative));
            }
            else if (string.IsNullOrEmpty(_csRecipe3))
            {
                _csRecipe3 = StringProcess.GetFileName(foodName);
                Food3Image.Source = new BitmapImage(new Uri(foodName, UriKind.Relative));
            }
            else if (string.IsNullOrEmpty(_csRecipe4))
            {
                _csRecipe4 = StringProcess.GetFileName(foodName);
                Food4Image.Source = new BitmapImage(new Uri(foodName, UriKind.Relative));
            }
            if (_csRecipe1 != "" && _csRecipe2 != "" && _csRecipe3 != "" && _csRecipe4 != "")
            {
                CS_CrockPotCalculation();
            }
        }

        /// <summary>
        /// 删除食材
        /// </summary>
        private void DeleteFood(object sender, RoutedEventArgs e)
        {
            if (sender == null)
            {
                CS_Food_Property(_csRecipe1, false);
                _csRecipe1 = "";
                Food1Image.Source = null;
                CS_Food_Property(_csRecipe2, false);
                _csRecipe2 = "";
                Food2Image.Source = null;
                CS_Food_Property(_csRecipe3, false);
                _csRecipe3 = "";
                Food3Image.Source = null;
                CS_Food_Property(_csRecipe4, false);
                _csRecipe4 = "";
                Food4Image.Source = null;
            }
            else
            {
                var foodButtonName = ((Button)sender).Name;
                switch (foodButtonName)
                {
                    case "Food1Button":
                        CS_Food_Property(_csRecipe1, false);
                        _csRecipe1 = "";
                        Food1Image.Source = null;
                        break;
                    case "Food2Button":
                        CS_Food_Property(_csRecipe2, false);
                        _csRecipe2 = "";
                        Food2Image.Source = null;
                        break;
                    case "Food3Button":
                        CS_Food_Property(_csRecipe3, false);
                        _csRecipe3 = "";
                        Food3Image.Source = null;
                        break;
                    case "Food4Button":
                        CS_Food_Property(_csRecipe4, false);
                        _csRecipe4 = "";
                        Food4Image.Source = null;
                        break;
                }
            }
        }

        /// <summary>
        /// 烹饪食材属性
        /// </summary>
        /// <param name="source">食物代码</param>
        /// <param name="plus"></param>
        private void CS_Food_Property(string source, bool plus = true)
        {
            if (string.IsNullOrEmpty(source)) return;
            source = StringProcess.GetGameResourcePath(source);
            foreach (var foodMeat in Global.FoodMeatData.Where(temp => temp.Picture == source))
            {
                if (plus)
                {
                    FoodHealth.Value += foodMeat.Health;
                    FoodHunger.Value += foodMeat.Hunger;
                    FoodSanity.Value += foodMeat.Sanity;
                }
                else
                {
                    FoodHealth.Value -= foodMeat.Health;
                    FoodHunger.Value -= foodMeat.Hunger;
                    FoodSanity.Value -= foodMeat.Sanity;
                }
            }
            foreach (var foodVegetable in Global.FoodVegetableData.Where(temp => temp.Picture == source))
            {
                if (plus)
                {
                    FoodHealth.Value += foodVegetable.Health;
                    FoodHunger.Value += foodVegetable.Hunger;
                    FoodSanity.Value += foodVegetable.Sanity;
                }
                else
                {
                    FoodHealth.Value -= foodVegetable.Health;
                    FoodHunger.Value -= foodVegetable.Hunger;
                    FoodSanity.Value -= foodVegetable.Sanity;
                }
            }
            foreach (var foodFruit in Global.FoodFruitData.Where(temp => temp.Picture == source))
            {
                if (plus)
                {
                    FoodHealth.Value += foodFruit.Health;
                    FoodHunger.Value += foodFruit.Hunger;
                    FoodSanity.Value += foodFruit.Sanity;
                }
                else
                {
                    FoodHealth.Value -= foodFruit.Health;
                    FoodHunger.Value -= foodFruit.Hunger;
                    FoodSanity.Value -= foodFruit.Sanity;
                }
            }
            foreach (var foodEgg in Global.FoodEggData.Where(temp => temp.Picture == source))
            {
                if (plus)
                {
                    FoodHealth.Value += foodEgg.Health;
                    FoodHunger.Value += foodEgg.Hunger;
                    FoodSanity.Value += foodEgg.Sanity;
                }
                else
                {
                    FoodHealth.Value -= foodEgg.Health;
                    FoodHunger.Value -= foodEgg.Hunger;
                    FoodSanity.Value -= foodEgg.Sanity;
                }
            }
            foreach (var foodOther in Global.FoodOtherData.Where(temp => temp.Picture == source))
            {
                if (plus)
                {
                    FoodHealth.Value += foodOther.Health;
                    FoodHunger.Value += foodOther.Hunger;
                    FoodSanity.Value += foodOther.Sanity;
                }
                else
                {
                    FoodHealth.Value -= foodOther.Health;
                    FoodHunger.Value -= foodOther.Hunger;
                    FoodSanity.Value -= foodOther.Sanity;
                }
            }
        }

        /// <summary>
        /// 食材属性统计
        /// </summary>
        /// <param name="name">食材名</param>
        private void CS_RecipeStatistics(string name)
        {
            switch (name)
            {
                #region 肉类
                case "F_meat":
                    _csFtMeats += 1;
                    break;
                case "F_cooked_meat":
                    _csFtMeats += 1;
                    break;
                case "F_jerky":
                    _csFtMeats += 1;
                    break;
                case "F_monster_meat":
                    _csFtMeats += 1;
                    _csFtMonsterFoods += 1;
                    break;
                case "F_cooked_monster_meat":
                    _csFtMeats += 1;
                    _csFtMonsterFoods += 1;
                    break;
                case "F_monster_jerky":
                    _csFtMeats += 1;
                    _csFtMonsterFoods += 1;
                    break;
                case "F_morsel":
                    _csFtMeats += 0.5;
                    break;
                case "F_cooked_morsel":
                    _csFtMeats += 0.5;
                    break;
                case "F_small_jerky":
                    _csFtMeats += 0.5;
                    break;
                case "F_drumstick":
                    _csFtMeats += 0.5;
                    _csFtDrumstick += 1;
                    break;
                case "F_fried_drumstick":
                    _csFtMeats += 0.5;
                    _csFtDrumstick += 1;
                    break;
                case "F_frog_legs":
                    _csFtMeats += 0.5;
                    _csFtFrogLegs += 1;
                    break;
                case "F_cooked_frog_legs":
                    _csFtMeats += 0.5;
                    _csFtFrogLegs += 1;
                    break;
                case "F_fish":
                    _csFtFishes += 1;
                    _csFtMeats += 0.5;
                    break;
                case "F_cooked_fish":
                    _csFtFishes += 1;
                    _csFtMeats += 0.5;
                    break;
                case "F_eel":
                    _csFtFishes += 1;
                    _csFtEel += 1;
                    _csFtMeats += 0.5;
                    break;
                case "F_cooked_eel":
                    _csFtFishes += 1;
                    _csFtEel += 1;
                    _csFtMeats += 0.5;
                    break;
                case "F_limpets":
                    _csFtFishes += 0.5;
                    _csFtLimpets += 1;
                    break;
                case "F_cooked_limpets":
                    _csFtFishes += 0.5;
                    break;
                case "F_roe":
                    _csFtMeats += 0.5;
                    _csFtFishes += 1;
                    _csFtRoe += 1;
                    break;
                case "F_cooked_roe":
                    _csFtMeats += 0.5;
                    _csFtFishes += 1;
                    _csFtRoeCooked += 1;
                    break;
                case "F_tropical_fish":
                    _csFtMeats += 0.5;
                    _csFtFishes += 1;
                    break;
                case "F_neon_quattro":
                    _csFtFishes += 1;
                    _csFtNeonQuattro += 1;
                    break;
                case "F_cooked_neon_quattro":
                    _csFtFishes += 1;
                    _csFtNeonQuattro += 1;
                    break;
                case "F_pierrot_fish":
                    _csFtFishes += 1;
                    _csFtPierrotFish += 1;
                    break;
                case "F_cooked_pierrot_fish":
                    _csFtFishes += 1;
                    _csFtPierrotFish += 1;
                    break;
                case "F_purple_grouper":
                    _csFtFishes += 1;
                    _csFtPurpleGrouper += 1;
                    break;
                case "F_cooked_purple_grouper":
                    _csFtFishes += 1;
                    _csFtPurpleGrouper += 1;
                    break;
                case "F_fish_morsel":
                    _csFtFishes += 0.5;
                    break;
                case "F_cooked_fish_morsel":
                    _csFtFishes += 0.5;
                    break;
                case "F_jellyfish":
                    _csFtFishes += 1;
                    _csFtMonsterFoods += 1;
                    _csFtJellyfish += 1;
                    break;
                case "F_dead_jellyfish":
                    _csFtFishes += 1;
                    _csFtMonsterFoods += 1;
                    break;
                case "F_cooked_jellyfish":
                    _csFtFishes += 1;
                    _csFtMonsterFoods += 1;
                    break;
                case "F_dried_jellyfish":
                    _csFtFishes += 1;
                    _csFtMonsterFoods += 1;
                    break;
                case "F_mussel":
                    _csFtFishes += 0.5;
                    _csFtMussel += 1;
                    break;
                case "F_cooked_mussel":
                    _csFtFishes += 0.5;
                    _csFtMussel += 1;
                    break;
                case "F_dead_dogfish":
                    _csFtFishes += 1;
                    _csFtMeats += 0.5;
                    break;
                case "F_wobster":
                    _csFtFishes += 2;
                    _csFtWobster += 1;
                    break;
                case "F_raw_fish":
                    _csFtFishes += 1;
                    _csFtMeats += 0.5;
                    break;
                case "F_fish_steak":
                    _csFtFishes += 1;
                    _csFtMeats += 0.5;
                    break;
                case "F_shark_fin":
                    _csFtFishes += 1;
                    _csFtMeats += 0.5;
                    _csFtSharkFin += 1;
                    break;
                #endregion
                #region 蔬菜
                case "F_blue_cap":
                    _csFtVegetables += 0.5;
                    break;
                case "F_cooked_blue_cap":
                    _csFtVegetables += 0.5;
                    break;
                case "F_green_cap":
                    _csFtVegetables += 0.5;
                    break;
                case "F_cooked_green_cap":
                    _csFtVegetables += 0.5;
                    break;
                case "F_red_cap":
                    _csFtVegetables += 0.5;
                    break;
                case "F_cooked_red_cap":
                    _csFtVegetables += 0.5;
                    break;
                case "F_eggplant":
                    _csFtVegetables += 1;
                    _csFtEggplant += 1;
                    break;
                case "F_braised_eggplant":
                    _csFtVegetables += 1;
                    _csFtEggplant += 1;
                    break;
                case "F_carrot":
                    _csFtVegetables += 1;
                    break;
                case "F_roasted_carrot":
                    _csFtVegetables += 1;
                    break;
                case "F_corn":
                    _csFtVegetables += 1;
                    _csFtCorn += 1;
                    break;
                case "F_popcorn":
                    _csFtVegetables += 1;
                    _csFtCorn += 1;
                    break;
                case "F_pumpkin":
                    _csFtVegetables += 1;
                    _csFtPumpkin += 1;
                    break;
                case "F_hot_pumpkin":
                    _csFtVegetables += 1;
                    _csFtPumpkin += 1;
                    break;
                case "F_cactus_flesh":
                    _csFtVegetables += 1;
                    _csFtCactusFlesh += 1;
                    break;
                case "F_cooked_cactus_flesh":
                    _csFtVegetables += 1;
                    break;
                case "F_cactus_flower":
                    _csFtVegetables += 0.5;
                    _csFtCactusFlower += 1;
                    break;
                case "F_sweet_potato":
                    _csFtVegetables += 1;
                    _csFtSweetPotato += 1;
                    break;
                case "F_cooked_sweet_potato":
                    _csFtVegetables += 1;
                    break;
                case "F_seaweed":
                    _csFtVegetables += 1;
                    _csFtSeaweed += 1;
                    break;
                case "F_roasted_seaweed":
                    _csFtVegetables += 1;
                    break;
                case "F_dried_seaweed":
                    _csFtVegetables += 1;
                    break;
                #endregion
                #region 水果
                case "F_juicy_berries":
                    _csFtFruit += 0.5;
                    break;
                case "F_roasted_juicy_berries":
                    _csFtFruit += 0.5;
                    break;
                case "F_berries":
                    _csFtFruit += 0.5;
                    _csFtBerries += 1;
                    break;
                case "F_roasted_berrie":
                    _csFtFruit += 0.5;
                    _csFtBerries += 1;
                    break;
                case "F_banana":
                    _csFtFruit += 1;
                    _csFtBanana += 1;
                    break;
                case "F_cooked_banana":
                    _csFtFruit += 1;
                    break;
                case "F_dragon_fruit":
                    _csFtFruit += 1;
                    _csFtDragonFruit += 1;
                    break;
                case "F_prepared_dragon_fruit":
                    _csFtFruit += 1;
                    _csFtDragonFruit += 1;
                    break;
                case "F_durian":
                    _csFtFruit += 1;
                    _csFtMonsterFoods += 1;
                    break;
                case "F_extra_smelly_durian":
                    _csFtFruit += 1;
                    _csFtMonsterFoods += 1;
                    break;
                case "F_pomegranate":
                    _csFtFruit += 1;
                    break;
                case "F_sliced_pomegranate":
                    _csFtFruit += 1;
                    break;
                case "F_watermelon":
                    _csFtFruit += 1;
                    _csFtWatermelon += 1;
                    break;
                case "F_grilled_watermelon":
                    _csFtFruit += 1;
                    break;
                case "F_halved_coconut":
                    _csFtFruit += 1;
                    break;
                case "F_roasted_coconut":
                    _csFtFruit += 1;
                    break;
                case "F_coffee_beans":
                    _csFtFruit += 0.5;
                    break;
                case "F_roasted_coffee_beans":
                    _csFtFruit += 1;
                    _csFtRoastedCoffeeBeans += 1;
                    break;
                #endregion
                #region 蛋类
                case "F_egg":
                    _csFtEggs += 1;
                    break;
                case "F_cooked_egg":
                    _csFtEggs += 1;
                    break;
                case "F_tallbird_egg":
                    _csFtEggs += 4;
                    break;
                case "F_fried_tallbird_egg":
                    _csFtEggs += 4;
                    break;
                case "F_doydoy_egg":
                    _csFtEggs += 1;
                    break;
                case "F_fried_doydoy_egg":
                    _csFtEggs += 1;
                    break;
                #endregion
                #region 其他
                case "F_butterfly_wing":
                    _csFtButterflyWings += 1;
                    break;
                case "F_butterfly_wing_sw":
                    _csFtButterflyWings += 1;
                    break;
                case "F_butter":
                    _csFtDairyProduct += 1;
                    _csFtButter += 1;
                    break;
                case "F_honey":
                    _csFtSweetener += 1;
                    _csFtHoney += 1;
                    break;
                case "F_honeycomb":
                    _csFtSweetener += 1;
                    break;
                case "F_lichen":
                    _csFtVegetables += 1;
                    _csFtLichen += 1;
                    break;
                case "F_mandrake":
                    _csFtVegetables += 1;
                    _csFtMandrake += 1;
                    break;
                case "F_electric_milk":
                    _csFtDairyProduct += 1;
                    break;
                case "F_ice":
                    _csFtIce += 1;
                    break;
                case "F_roasted_birchnut":
                    _csFtRoastedBirchnut += 1;
                    break;
                case "F_royal_jelly":
                    _csFtRoyalJelly += 1;
                    _csFtSweetener += 3;
                    break;
                case "F_twigs":
                    _csFtTwigs += 1;
                    break;
                case "F_moleworm":
                    _csFtMoleworm += 1;
                    break;
                    #endregion
            }
        }

        /// <summary>
        /// 烹饪计算
        /// </summary>
        private void CS_CrockPotCalculation()
        {
            _foodIndex = 0;
            #region 食物列表初始化
            _crockPotList.Clear();
            _crockPotListIndex = -1;
            _crockPotMaxPriority = -128;
            #endregion
            #region 食材属性初始化
            _csFtBanana = 0;
            _csFtBerries = 0;
            _csFtButter = 0;
            _csFtButterflyWings = 0;
            _csFtCactusFlesh = 0;
            _csFtCactusFlower = 0;
            _csFtCorn = 0;
            _csFtDairyProduct = 0;
            _csFtDragonFruit = 0;
            _csFtDrumstick = 0;
            _csFtEel = 0;
            _csFtEggplant = 0;
            _csFtEggs = 0;
            _csFtFishes = 0;
            _csFtFrogLegs = 0;
            _csFtFruit = 0;
            _csFtHoney = 0;
            _csFtIce = 0;
            _csFtJellyfish = 0;
            _csFtLichen = 0;
            _csFtLimpets = 0;
            _csFtMandrake = 0;
            _csFtMeats = 0;
            _csFtMoleworm = 0;
            _csFtMonsterFoods = 0;
            _csFtMussel = 0;
            _csFtPumpkin = 0;
            _csFtRoastedBirchnut = 0;
            _csFtRoastedCoffeeBeans = 0;
            _csFtSeaweed = 0;
            _csFtSharkFin = 0;
            _csFtSweetener = 0;
            _csFtSweetPotato = 0;
            _csFtTwigs = 0;
            _csFtVegetables = 0;
            _csFtWatermelon = 0;
            _csFtWobster = 0;
            _csFtRoyalJelly = 0;
            _csFtRoe = 0;
            _csFtRoeCooked = 0;
            _csFtNeonQuattro = 0;
            _csFtPierrotFish = 0;
            _csFtPurpleGrouper = 0;
            #endregion
            #region 属性统计
            CS_RecipeStatistics(_csRecipe1);
            CS_RecipeStatistics(_csRecipe2);
            CS_RecipeStatistics(_csRecipe3);
            CS_RecipeStatistics(_csRecipe4);
            #endregion
            #region 烹饪
            // ------------------------SW------------------------
            // 便携式烹饪锅的四种食物
            if (Global.GameVersion == 4)
            {
                if (_csFtVegetables >= 0.5 && _csFtNeonQuattro == 1 && _csFtPierrotFish == 1 && _csFtPurpleGrouper == 1)
                    CS_CrockPotListAddFood("F_tropical_bouillabaisse", 40);
                if (CrockpotComboBox.SelectedIndex == 1)
                {
                    if (_csFtFruit >= 2 && _csFtButter >= 1 && _csFtHoney >= 1)
                        CS_CrockPotListAddFood("F_fresh_fruit_crepes", 30);
                    if (_csFtMonsterFoods >= 2 && _csFtEggs >= 1 && _csFtVegetables >= 0.5)
                        CS_CrockPotListAddFood("F_monster_tartare", 30);
                    if (_csFtMussel >= 2 && _csFtVegetables >= 2)
                        CS_CrockPotListAddFood("F_mussel_bouillabaise", 30);
                    if (_csFtSweetPotato >= 2 && _csFtEggs >= 2)
                        CS_CrockPotListAddFood("F_sweet_potato_souffle", 30);
                }
                if (_csFtWobster >= 1 && _csFtIce >= 1)
                    CS_CrockPotListAddFood("F_lobster_bisque", 30);
                if (_csFtLimpets >= 3 && _csFtIce >= 1)
                    CS_CrockPotListAddFood("F_bisque", 30);
                if (_csFtRoastedCoffeeBeans >= 3 && (_csFtRoastedCoffeeBeans == 4 || _csFtSweetener == 1 || _csFtDairyProduct == 1))
                    CS_CrockPotListAddFood("F_coffee", 30);
                if (_csFtMeats >= 2.5 && _csFtFishes >= 1.5 && _csFtIce == 0)
                    CS_CrockPotListAddFood("F_surf_'n'_turf", 30);
                if (_csFtWobster >= 1 && _csFtButter >= 1 && _csFtMeats == 0 && _csFtIce == 0)
                    CS_CrockPotListAddFood("F_lobster_dinner", 25);
                if (_csFtVegetables >= 1 && (_csFtRoe >= 1 || _csFtRoeCooked >= 3))
                    CS_CrockPotListAddFood("F_caviar", 20);
                if (_csFtBanana >= 1 && _csFtIce >= 1 && _csFtTwigs >= 1 && _csFtMeats == 0 && _csFtFishes == 0)
                    CS_CrockPotListAddFood("F_banana_pop", 20);
                if (_csFtFishes >= 1 && _csFtSeaweed == 2)
                    CS_CrockPotListAddFood("F_california_roll", 20);
                if (_csFtJellyfish >= 1 && _csFtIce >= 1 && _csFtTwigs >= 1)
                    CS_CrockPotListAddFood("F_jelly-O_pop", 20);
                if (_csFtFishes >= 2 && _csFtIce >= 1)
                    CS_CrockPotListAddFood("F_ceviche", 20);
                if (_csFtSharkFin >= 1)
                    CS_CrockPotListAddFood("F_shark_fin_soup", 20);
                if (_csFtFishes >= 2.5)
                    CS_CrockPotListAddFood("F_seafood_gumbo", 10);
            }
            // ------------------------其他------------------------
            if (_csFtRoyalJelly >= 1 && _csFtTwigs == 0 && _csFtMonsterFoods == 0)
                CS_CrockPotListAddFood("F_jellybeans", 12);
            if (_csFtCactusFlesh >= 1 && _csFtMoleworm >= 1 && _csFtFruit == 0)
                CS_CrockPotListAddFood("F_guacamole", 10);
            if (_csFtCactusFlower >= 1 && _csFtVegetables >= 2 && _csFtFruit == 0 && _csFtMeats == 0 && _csFtEggs == 0 && _csFtSweetener == 0 && _csFtTwigs == 0)
                CS_CrockPotListAddFood("F_flower_salad", 10);
            if (_csFtDairyProduct >= 1 && _csFtIce >= 1 && _csFtSweetener >= 1 && _csFtMeats == 0 && _csFtEggs == 0 && _csFtVegetables == 0 && _csFtTwigs == 0)
                CS_CrockPotListAddFood("F_ice_cream", 10);
            if (_csFtWatermelon >= 1 && _csFtIce >= 1 && _csFtTwigs >= 1 && _csFtMeats == 0 && _csFtEggs == 0 && _csFtVegetables == 0)
                CS_CrockPotListAddFood("F_melonsicle", 10);
            if (_csFtRoastedBirchnut >= 1 && _csFtBerries >= 1 && _csFtFruit >= 1 && _csFtMeats == 0 && _csFtEggs == 0 && _csFtVegetables == 0 && _csFtSweetener == 0)
                CS_CrockPotListAddFood("F_trail_mix", 10);
            if (_csFtVegetables >= 1.5 && _csFtMeats >= 1.5)
                CS_CrockPotListAddFood("F_spicy_chili", 10);
            if (_csFtEel >= 1 && _csFtLichen >= 1)
                CS_CrockPotListAddFood("F_unagi", 20);
            if (_csFtPumpkin >= 1 && _csFtSweetener >= 2)
                CS_CrockPotListAddFood("F_pumpkin_cookie", 10);
            if (_csFtCorn >= 1 && _csFtHoney >= 1 && _csFtTwigs >= 1)
                CS_CrockPotListAddFood("F_powdercake", 10);
            if (_csFtMandrake >= 1)
                CS_CrockPotListAddFood("F_mandrake_soup", 10);
            if (_csFtFishes >= 0.5 && _csFtTwigs == 1)
                CS_CrockPotListAddFood("F_fishsticks", 10);
            if (_csFtFishes >= 0.5 && _csFtCorn >= 1)
                CS_CrockPotListAddFood("F_fish_tacos", 10);
            if (_csFtMeats >= 1.5 && _csFtEggs >= 2 && _csFtVegetables == 0)
                CS_CrockPotListAddFood("F_bacon_and_eggs", 10);
            if (_csFtDrumstick >= 2 && _csFtMeats >= 1.5 && (_csFtVegetables >= 0.5 || _csFtFruit >= 0.5))
                CS_CrockPotListAddFood("F_turkey_dinner", 10);
            if (_csFtSweetener >= 3 && _csFtMeats == 0)
                CS_CrockPotListAddFood("F_taffy", 10);
            if (_csFtButter >= 1 && _csFtEggs >= 1 && _csFtBerries >= 1)
                CS_CrockPotListAddFood("F_waffles", 10);
            if (_csFtMonsterFoods >= 2 && _csFtTwigs == 0)
                CS_CrockPotListAddFood("F_monster_lasagna", 10);
            if (_csFtEggs >= 1 && _csFtMeats >= 0.5 && _csFtVegetables >= 0.5 && _csFtTwigs == 0)
                CS_CrockPotListAddFood("F_pierogi", 5);
            if (_csFtMeats >= 0.5 && _csFtTwigs == 1 && _csFtMonsterFoods <= 1)
                CS_CrockPotListAddFood("F_kabobs", 5);
            if (_csFtMeats >= 2 && _csFtHoney >= 1 && _csFtTwigs == 0)
                CS_CrockPotListAddFood("F_honey_ham", 2);
            if (_csFtMeats >= 0.5 && _csFtMeats < 2 && _csFtHoney >= 1 && _csFtTwigs == 0)
                CS_CrockPotListAddFood("F_honey_nuggets", 2);
            if (_csFtButterflyWings >= 1 && _csFtVegetables >= 0.5 && _csFtMeats == 0)
                CS_CrockPotListAddFood("F_butter_muffin", 1);
            if (_csFtFrogLegs >= 1 && _csFtVegetables >= 0.5)
                CS_CrockPotListAddFood("F_froggle_bunwich", 1);
            if (_csFtDragonFruit >= 1 && _csFtMeats == 0)
                CS_CrockPotListAddFood("F_dragonpie", 1);
            if (_csFtEggplant >= 1 && _csFtVegetables >= 1.5)
                CS_CrockPotListAddFood("F_stuffed_eggplant", 1);
            if (_csFtVegetables >= 0.5 && _csFtMeats == 0 && _csFtTwigs == 0)
                CS_CrockPotListAddFood("F_ratatouille", 0);
            if (_csFtFruit >= 0.5 && _csFtMeats == 0 && _csFtVegetables == 0)
            {
                if (_csFtFruit < 3)
                {
                    CS_CrockPotListAddFood("F_fist_full_of_jam", 0);
                }
                else
                {
                    if (_csFtTwigs == 0)
                    {
                        CS_CrockPotListAddFood("F_fist_full_of_jam", 0);
                        CS_CrockPotListAddFood("F_fruit_medley", 0);
                    }
                    else
                    {
                        CS_CrockPotListAddFood("F_fruit_medley", 0);
                    }
                }
            }
            if (_csFtMeats >= 3 && _csFtTwigs == 0)
            {
                CS_CrockPotListAddFood("F_meaty_stew", 0);
            }

            if (_csFtMeats >= 0.5 && _csFtMeats < 3 && _csFtTwigs == 0)
                CS_CrockPotListAddFood("F_meatballs", -1);
            #endregion
            #region 食物判断
            if (_crockPotListIndex == -1)
            {
                CS_CrockPotListAddFood("F_wet_goop", -2);
            }
            _csFoodName = _crockPotList[0];
            //显示食物图片
            CS_image_Food_Result_Source(_csFoodName);
            //显示食物名称
            FoodResultTextBlock.Text = CS_Food_Text(_csFoodName);
            //显示食物属性
            CS_FoodRecipe_Property(_csFoodName);
            #endregion
            #region 选择按钮显示判断
            if (_crockPotListIndex < 1)
            {
                SwitchLeftButton.Visibility = Visibility.Collapsed;
                SwitchRightButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                SwitchLeftButton.Visibility = Visibility.Visible;
                SwitchRightButton.Visibility = Visibility.Visible;
                SwitchLeftButton.IsEnabled = false;
                SwitchRightButton.IsEnabled = true;
            }
            #endregion
            #region 自动清空材料
            if (AutoCleanCheckBox.IsChecked == true)
            {
                _csRecipe1 = "";
                _csRecipe2 = "";
                _csRecipe3 = "";
                _csRecipe4 = "";
                Food1Image.Source = null;
                Food2Image.Source = null;
                Food3Image.Source = null;
                Food4Image.Source = null;
            }
            #endregion
        }

        /// <summary>
        /// 向食物列表添加食物
        /// </summary>
        /// <param name="foodName">食物名</param>
        /// <param name="foodPriority">食物优先度</param>
        private void CS_CrockPotListAddFood(string foodName, sbyte foodPriority)
        {
            if (foodPriority >= _crockPotMaxPriority)
            {
                _crockPotMaxPriority = foodPriority;
                _crockPotListIndex += 1;
                _crockPotList.Add(foodName);
            }
        }

        #region 烹饪结果
        /// <summary>
        /// 烹饪结果图片
        /// </summary>
        /// <param name="source">食物代码</param>
        private void CS_image_Food_Result_Source(string source)
        {
            FoodResultImage.Source = new BitmapImage(new Uri(StringProcess.GetGameResourcePath(source), UriKind.Relative));
        }

        /// <summary>
        /// 烹饪结果文字
        /// </summary>
        /// <param name="source">食物图片(短名)</param>
        /// <returns>烹饪结果</returns>
        private static string CS_Food_Text(string source)
        {
            return (from foodRecipe 
                    in Global.FoodRecipeData
                    where StringProcess.GetFileName(foodRecipe.Picture) == source
                    select foodRecipe.Name).FirstOrDefault();
        }

        /// <summary>
        /// 烹饪结果属性
        /// </summary>
        /// <param name="source">食物图片(短名)</param>
        private void CS_FoodRecipe_Property(string source)
        {
            foreach (var foodRecipe in Global.FoodRecipeData)
            {
                if (StringProcess.GetGameResourcePath(source) == foodRecipe.Picture)
                {
                    FoodRecipeHealth.Value = foodRecipe.Health;
                    FoodRecipeHunger.Value = foodRecipe.Hunger;
                    FoodRecipeSanity.Value = foodRecipe.Sanity;
                }
            }
        }

        /// <summary>
        /// 烹饪结果跳转
        /// </summary>
        private void ResultButton_Click(object sender, RoutedEventArgs e)
        {
            if (FoodResultImage.Source == null) return;
            var picturePath = _crockPotList[_foodIndex];
            
            Global.SetAutoSuggestBoxItem();
            foreach (var suggestBoxItem in Global.AutoSuggestBoxItemSource)
            {
                if (picturePath != StringProcess.GetFileName(suggestBoxItem.Picture)) continue;
                var extraData = new[] { suggestBoxItem.SourcePath, suggestBoxItem.Picture };
                Global.PageJump(2,extraData);
                break;
            }
        }

        /// <summary>
        /// 左右切换按钮
        /// </summary>
        private void SwitchLeftButton_Click(object sender, RoutedEventArgs e)
        {
            SwitchRightButton.IsEnabled = true;
            if (_foodIndex != 0)
            {
                _foodIndex -= 1;
                if (_foodIndex == 0)
                {
                    SwitchLeftButton.IsEnabled = false;
                }
                _csFoodName = _crockPotList[_foodIndex];
                CS_image_Food_Result_Source(_crockPotList[_foodIndex]);
                CS_FoodRecipe_Property(_crockPotList[_foodIndex]);
            }
            FoodResultTextBlock.Text = CS_Food_Text(_csFoodName);
        }

        private void SwitchRightButton_Click(object sender, RoutedEventArgs e)
        {
            SwitchLeftButton.IsEnabled = true;
            if (_foodIndex != _crockPotListIndex)
            {
                _foodIndex += 1;
                if (_foodIndex == _crockPotListIndex)
                {
                    SwitchRightButton.IsEnabled = false;
                }
                _csFoodName = _crockPotList[_foodIndex];
                CS_image_Food_Result_Source(_crockPotList[_foodIndex]);
                CS_FoodRecipe_Property(_crockPotList[_foodIndex]);
            }
            FoodResultTextBlock.Text = CS_Food_Text(_csFoodName);
        }
        #endregion

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
