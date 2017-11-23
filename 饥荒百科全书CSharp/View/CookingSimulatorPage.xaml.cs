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

namespace 饥荒百科全书CSharp.View
{
    /// <summary>
    /// CookingSimulatorPage.xaml 的交互逻辑
    /// </summary>
    public partial class CookingSimulatorPage : Page
    {
        private readonly ObservableCollection<FoodRecipe2> _foodRecipeData = new ObservableCollection<FoodRecipe2>();
        private readonly ObservableCollection<Food> _foodMeatData = new ObservableCollection<Food>();
        private readonly ObservableCollection<Food> _foodVegetableData = new ObservableCollection<Food>();
        private readonly ObservableCollection<Food> _foodFruitData = new ObservableCollection<Food>();
        private readonly ObservableCollection<Food> _foodEggData = new ObservableCollection<Food>();
        private readonly ObservableCollection<Food> _foodOtherData = new ObservableCollection<Food>();

        private int _loadedTime;

        public void LoadCompleted(object sender, NavigationEventArgs e)
        {
            if (_loadedTime != 0) return;
            _loadedTime++;
            if (Global.FontFamily != null)
            {
                FontFamily = Global.FontFamily;
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
            var food = JsonConvert.DeserializeObject<FoodRootObject>(StringProcess.GetJsonString("Foods.json"));
            foreach (var foodRecipeItems in food.FoodRecipe.FoodRecipes)
            {
                _foodRecipeData.Add(foodRecipeItems);
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
            MeatsExpander.DataContext = _foodMeatData;
            VegetablesExpander.DataContext = _foodVegetableData;
            FruitsExpander.DataContext = _foodFruitData;
            EggsExpander.DataContext = _foodEggData;
            OtherExpander.DataContext = _foodOtherData;
        }

        private void FoodButton_Click(object sender, RoutedEventArgs e)
        {
            var food = (Food)((Button)sender).DataContext;
            CS_Add(food.Picture);
        }

        #region 变量初始化
        /// <summary>
        /// 重置
        /// </summary>
        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            Food1Button_Click(null, null);
            Food2Button_Click(null, null);
            Food3Button_Click(null, null);
            Food4Button_Click(null, null);
            FoodResultImage.Source = null;
            FoodResultTextBlock.Text = "";
            CrockPotList.Clear();
            CrockPotListIndex = -1;
            CrockPotMaxPriority = -128;
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
        public string CsRecipe1 = "";
        public string CsRecipe2 = "";
        public string CsRecipe3 = "";
        public string CsRecipe4 = "";
        /// <summary>
        /// 43种食材
        /// </summary>
        public double CsFtEggs = 0;
        public double CsFtVegetables = 0;
        public double CsFtFruit = 0;
        public double CsFtBanana = 0;
        public double CsFtBerries = 0;
        public double CsFtButter = 0;
        public double CsFtButterflyWings = 0;
        public double CsFtCactusFlesh = 0;
        public double CsFtCactusFlower = 0;
        public double CsFtCorn = 0;
        public double CsFtDairyProduct = 0;
        public double CsFtDragonFruit = 0;
        public double CsFtDrumstick = 0;
        public double CsFtEel = 0;
        public double CsFtEggplant = 0;
        public double CsFtFishes = 0;
        public double CsFtFrogLegs = 0;
        public double CsFtHoney = 0;
        public double CsFtIce = 0;
        public double CsFtJellyfish = 0;
        public double CsFtLichen = 0;
        public double CsFtLimpets = 0;
        public double CsFtMandrake = 0;
        public double CsFtMeats = 0;
        public double CsFtMoleworm = 0;
        public double CsFtMonsterFoods = 0;
        public double CsFtMussel = 0;
        public double CsFtPumpkin = 0;
        public double CsFtRoastedBirchnut = 0;
        public double CsFtRoastedCoffeeBeans = 0;
        public double CsFtSeaweed = 0;
        public double CsFtSharkFin = 0;
        public double CsFtSweetener = 0;
        public double CsFtSweetPotato = 0;
        public double CsFtTwigs = 0;
        public double CsFtWatermelon = 0;
        public double CsFtWobster = 0;
        public double CsFtRoyalJelly = 0;
        public double CsFtRoe = 0;
        public double CsFtRoeCooked = 0;
        public double CsFtNeonQuattro = 0;
        public double CsFtPierrotFish = 0;
        public double CsFtPurpleGrouper = 0;

        public byte FoodIndex = 0;
        public string CsFoodName = "";

        /// <summary>
        /// 食物列表
        /// </summary>
        public List<string> CrockPotList = new List<string>();
        /// <summary>
        /// 食物列表下标
        /// </summary>
        public sbyte CrockPotListIndex = -1;
        /// <summary>
        /// 优先度最大值
        /// </summary>
        public sbyte CrockPotMaxPriority = -128;
        #endregion

        /// <summary>
        /// 添加食材
        /// </summary>
        /// <param name="foodName">食材名称</param>
        private void CS_Add(string foodName)
        {
            if (CsRecipe1 == "" && CsRecipe2 == "" && CsRecipe3 == "" && CsRecipe4 == "")
            {
                FoodHealth.Value = 0;
                FoodHunger.Value = 0;
                FoodSanity.Value = 0;
            }
            if (CsRecipe1 == "" || CsRecipe2 == "" || CsRecipe3 == "" || CsRecipe4 == "")
            {
                CS_Food_Property(foodName);
            }
            if (CsRecipe1 == "")
            {
                CsRecipe1 = StringProcess.GetFileName(foodName);
                Food1Image.Source = new BitmapImage(new Uri(foodName, UriKind.Relative));
            }
            else if (CsRecipe2 == "")
            {
                CsRecipe2 = StringProcess.GetFileName(foodName);
                Food2Image.Source = new BitmapImage(new Uri(foodName, UriKind.Relative));
            }
            else if (CsRecipe3 == "")
            {
                CsRecipe3 = StringProcess.GetFileName(foodName);
                Food3Image.Source = new BitmapImage(new Uri(foodName, UriKind.Relative));
            }
            else if (CsRecipe4 == "")
            {
                CsRecipe4 = StringProcess.GetFileName(foodName);
                Food4Image.Source = new BitmapImage(new Uri(foodName, UriKind.Relative));
            }
            if (CsRecipe1 != "" && CsRecipe2 != "" && CsRecipe3 != "" && CsRecipe4 != "")
            {
                CS_CrockPotCalculation();
            }
        }

        /// <summary>
        /// 删除食材
        /// </summary>
        private void Food1Button_Click(object sender, RoutedEventArgs e)
        {
            CS_Food_Property(CsRecipe1, false);
            CsRecipe1 = "";
            Food1Image.Source = null;
        }

        private void Food2Button_Click(object sender, RoutedEventArgs e)
        {
            CS_Food_Property(CsRecipe2, false);
            CsRecipe2 = "";
            Food2Image.Source = null;
        }

        private void Food3Button_Click(object sender, RoutedEventArgs e)
        {
            CS_Food_Property(CsRecipe3, false);
            CsRecipe3 = "";
            Food3Image.Source = null;
        }

        private void Food4Button_Click(object sender, RoutedEventArgs e)
        {
            CS_Food_Property(CsRecipe4, false);
            CsRecipe4 = "";
            Food4Image.Source = null;
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
            foreach (var foodMeat in _foodMeatData)
            {
                if (source == foodMeat.Picture)
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
            }
            foreach (var foodVegetable in _foodVegetableData)
            {
                if (source == foodVegetable.Picture)
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
            }
            foreach (var foodFruit in _foodFruitData)
            {
                if (source == foodFruit.Picture)
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
            }
            foreach (var foodEgg in _foodEggData)
            {
                if (source == foodEgg.Picture)
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
            }
            foreach (var foodOther in _foodOtherData)
            {
                if (source == foodOther.Picture)
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
                    CsFtMeats += 1;
                    break;
                case "F_cooked_meat":
                    CsFtMeats += 1;
                    break;
                case "F_jerky":
                    CsFtMeats += 1;
                    break;
                case "F_monster_meat":
                    CsFtMeats += 1;
                    CsFtMonsterFoods += 1;
                    break;
                case "F_cooked_monster_meat":
                    CsFtMeats += 1;
                    CsFtMonsterFoods += 1;
                    break;
                case "F_monster_jerky":
                    CsFtMeats += 1;
                    CsFtMonsterFoods += 1;
                    break;
                case "F_morsel":
                    CsFtMeats += 0.5;
                    break;
                case "F_cooked_morsel":
                    CsFtMeats += 0.5;
                    break;
                case "F_small_jerky":
                    CsFtMeats += 0.5;
                    break;
                case "F_drumstick":
                    CsFtMeats += 0.5;
                    CsFtDrumstick += 1;
                    break;
                case "F_fried_drumstick":
                    CsFtMeats += 0.5;
                    CsFtDrumstick += 1;
                    break;
                case "F_frog_legs":
                    CsFtMeats += 0.5;
                    CsFtFrogLegs += 1;
                    break;
                case "F_cooked_frog_legs":
                    CsFtMeats += 0.5;
                    CsFtFrogLegs += 1;
                    break;
                case "F_fish":
                    CsFtFishes += 1;
                    CsFtMeats += 0.5;
                    break;
                case "F_cooked_fish":
                    CsFtFishes += 1;
                    CsFtMeats += 0.5;
                    break;
                case "F_eel":
                    CsFtFishes += 1;
                    CsFtEel += 1;
                    CsFtMeats += 0.5;
                    break;
                case "F_cooked_eel":
                    CsFtFishes += 1;
                    CsFtEel += 1;
                    CsFtMeats += 0.5;
                    break;
                case "F_limpets":
                    CsFtFishes += 0.5;
                    CsFtLimpets += 1;
                    break;
                case "F_cooked_limpets":
                    CsFtFishes += 0.5;
                    break;
                case "F_roe":
                    CsFtMeats += 0.5;
                    CsFtFishes += 1;
                    CsFtRoe += 1;
                    break;
                case "F_cooked_roe":
                    CsFtMeats += 0.5;
                    CsFtFishes += 1;
                    CsFtRoeCooked += 1;
                    break;
                case "F_tropical_fish":
                    CsFtMeats += 0.5;
                    CsFtFishes += 1;
                    break;
                case "F_neon_quattro":
                    CsFtFishes += 1;
                    CsFtNeonQuattro += 1;
                    break;
                case "F_cooked_neon_quattro":
                    CsFtFishes += 1;
                    CsFtNeonQuattro += 1;
                    break;
                case "F_pierrot_fish":
                    CsFtFishes += 1;
                    CsFtPierrotFish += 1;
                    break;
                case "F_cooked_pierrot_fish":
                    CsFtFishes += 1;
                    CsFtPierrotFish += 1;
                    break;
                case "F_purple_grouper":
                    CsFtFishes += 1;
                    CsFtPurpleGrouper += 1;
                    break;
                case "F_cooked_purple_grouper":
                    CsFtFishes += 1;
                    CsFtPurpleGrouper += 1;
                    break;
                case "F_fish_morsel":
                    CsFtFishes += 0.5;
                    break;
                case "F_cooked_fish_morsel":
                    CsFtFishes += 0.5;
                    break;
                case "F_jellyfish":
                    CsFtFishes += 1;
                    CsFtMonsterFoods += 1;
                    CsFtJellyfish += 1;
                    break;
                case "F_dead_jellyfish":
                    CsFtFishes += 1;
                    CsFtMonsterFoods += 1;
                    break;
                case "F_cooked_jellyfish":
                    CsFtFishes += 1;
                    CsFtMonsterFoods += 1;
                    break;
                case "F_dried_jellyfish":
                    CsFtFishes += 1;
                    CsFtMonsterFoods += 1;
                    break;
                case "F_mussel":
                    CsFtFishes += 0.5;
                    CsFtMussel += 1;
                    break;
                case "F_cooked_mussel":
                    CsFtFishes += 0.5;
                    CsFtMussel += 1;
                    break;
                case "F_dead_dogfish":
                    CsFtFishes += 1;
                    CsFtMeats += 0.5;
                    break;
                case "F_wobster":
                    CsFtFishes += 2;
                    CsFtWobster += 1;
                    break;
                case "F_raw_fish":
                    CsFtFishes += 1;
                    CsFtMeats += 0.5;
                    break;
                case "F_fish_steak":
                    CsFtFishes += 1;
                    CsFtMeats += 0.5;
                    break;
                case "F_shark_fin":
                    CsFtFishes += 1;
                    CsFtMeats += 0.5;
                    CsFtSharkFin += 1;
                    break;
                #endregion
                #region 蔬菜
                case "F_blue_cap":
                    CsFtVegetables += 0.5;
                    break;
                case "F_cooked_blue_cap":
                    CsFtVegetables += 0.5;
                    break;
                case "F_green_cap":
                    CsFtVegetables += 0.5;
                    break;
                case "F_cooked_green_cap":
                    CsFtVegetables += 0.5;
                    break;
                case "F_red_cap":
                    CsFtVegetables += 0.5;
                    break;
                case "F_cooked_red_cap":
                    CsFtVegetables += 0.5;
                    break;
                case "F_eggplant":
                    CsFtVegetables += 1;
                    CsFtEggplant += 1;
                    break;
                case "F_braised_eggplant":
                    CsFtVegetables += 1;
                    CsFtEggplant += 1;
                    break;
                case "F_carrot":
                    CsFtVegetables += 1;
                    break;
                case "F_roasted_carrot":
                    CsFtVegetables += 1;
                    break;
                case "F_corn":
                    CsFtVegetables += 1;
                    CsFtCorn += 1;
                    break;
                case "F_popcorn":
                    CsFtVegetables += 1;
                    CsFtCorn += 1;
                    break;
                case "F_pumpkin":
                    CsFtVegetables += 1;
                    CsFtPumpkin += 1;
                    break;
                case "F_hot_pumpkin":
                    CsFtVegetables += 1;
                    CsFtPumpkin += 1;
                    break;
                case "F_cactus_flesh":
                    CsFtVegetables += 1;
                    CsFtCactusFlesh += 1;
                    break;
                case "F_cooked_cactus_flesh":
                    CsFtVegetables += 1;
                    break;
                case "F_cactus_flower":
                    CsFtVegetables += 0.5;
                    CsFtCactusFlower += 1;
                    break;
                case "F_sweet_potato":
                    CsFtVegetables += 1;
                    CsFtSweetPotato += 1;
                    break;
                case "F_cooked_sweet_potato":
                    CsFtVegetables += 1;
                    break;
                case "F_seaweed":
                    CsFtVegetables += 0.5;
                    CsFtSeaweed += 1;
                    break;
                case "F_roasted_seaweed":
                    CsFtVegetables += 0.5;
                    break;
                case "F_dried_seaweed":
                    CsFtVegetables += 0.5;
                    break;
                #endregion
                #region 水果
                case "F_juicy_berries":
                    CsFtFruit += 0.5;
                    break;
                case "F_roasted_juicy_berries":
                    CsFtFruit += 0.5;
                    break;
                case "F_berries":
                    CsFtFruit += 0.5;
                    CsFtBerries += 1;
                    break;
                case "F_roasted_berrie":
                    CsFtFruit += 0.5;
                    CsFtBerries += 1;
                    break;
                case "F_banana":
                    CsFtFruit += 1;
                    CsFtBanana += 1;
                    break;
                case "F_cooked_banana":
                    CsFtFruit += 1;
                    break;
                case "F_dragon_fruit":
                    CsFtFruit += 1;
                    CsFtDragonFruit += 1;
                    break;
                case "F_prepared_dragon_fruit":
                    CsFtFruit += 1;
                    CsFtDragonFruit += 1;
                    break;
                case "F_durian":
                    CsFtFruit += 1;
                    CsFtMonsterFoods += 1;
                    break;
                case "F_extra_smelly_durian":
                    CsFtFruit += 1;
                    CsFtMonsterFoods += 1;
                    break;
                case "F_pomegranate":
                    CsFtFruit += 1;
                    break;
                case "F_sliced_pomegranate":
                    CsFtFruit += 1;
                    break;
                case "F_watermelon":
                    CsFtFruit += 1;
                    CsFtWatermelon += 1;
                    break;
                case "F_grilled_watermelon":
                    CsFtFruit += 1;
                    break;
                case "F_halved_coconut":
                    CsFtFruit += 1;
                    break;
                case "F_roasted_coconut":
                    CsFtFruit += 1;
                    break;
                case "F_coffee_beans":
                    CsFtFruit += 0.5;
                    break;
                case "F_roasted_coffee_beans":
                    CsFtFruit += 1;
                    CsFtRoastedCoffeeBeans += 1;
                    break;
                #endregion
                #region 蛋类
                case "F_egg":
                    CsFtEggs += 1;
                    break;
                case "F_cooked_egg":
                    CsFtEggs += 1;
                    break;
                case "F_tallbird_egg":
                    CsFtEggs += 4;
                    break;
                case "F_fried_tallbird_egg":
                    CsFtEggs += 4;
                    break;
                case "F_doydoy_egg":
                    CsFtEggs += 1;
                    break;
                case "F_fried_doydoy_egg":
                    CsFtEggs += 1;
                    break;
                #endregion
                #region 其他
                case "F_butterfly_wing":
                    CsFtButterflyWings += 1;
                    break;
                case "F_butterfly_wing_sw":
                    CsFtButterflyWings += 1;
                    break;
                case "F_butter":
                    CsFtDairyProduct += 1;
                    CsFtButter += 1;
                    break;
                case "F_honey":
                    CsFtSweetener += 1;
                    CsFtHoney += 1;
                    break;
                case "F_honeycomb":
                    CsFtSweetener += 1;
                    break;
                case "F_lichen":
                    CsFtVegetables += 1;
                    CsFtLichen += 1;
                    break;
                case "F_mandrake":
                    CsFtVegetables += 1;
                    CsFtMandrake += 1;
                    break;
                case "F_electric_milk":
                    CsFtDairyProduct += 1;
                    break;
                case "F_ice":
                    CsFtIce += 1;
                    break;
                case "F_roasted_birchnut":
                    CsFtRoastedBirchnut += 1;
                    break;
                case "F_royal_jelly":
                    CsFtRoyalJelly += 1;
                    break;
                case "F_twigs":
                    CsFtTwigs += 1;
                    break;
                case "F_moleworm":
                    CsFtMoleworm += 1;
                    break;
                    #endregion
            }
        }

        /// <summary>
        /// 烹饪计算
        /// </summary>
        private void CS_CrockPotCalculation()
        {
            FoodIndex = 0;
            #region 食物列表初始化
            CrockPotList.Clear();
            CrockPotListIndex = -1;
            CrockPotMaxPriority = -128;
            #endregion
            #region 食材属性初始化
            CsFtBanana = 0;
            CsFtBerries = 0;
            CsFtButter = 0;
            CsFtButterflyWings = 0;
            CsFtCactusFlesh = 0;
            CsFtCactusFlower = 0;
            CsFtCorn = 0;
            CsFtDairyProduct = 0;
            CsFtDragonFruit = 0;
            CsFtDrumstick = 0;
            CsFtEel = 0;
            CsFtEggplant = 0;
            CsFtEggs = 0;
            CsFtFishes = 0;
            CsFtFrogLegs = 0;
            CsFtFruit = 0;
            CsFtHoney = 0;
            CsFtIce = 0;
            CsFtJellyfish = 0;
            CsFtLichen = 0;
            CsFtLimpets = 0;
            CsFtMandrake = 0;
            CsFtMeats = 0;
            CsFtMoleworm = 0;
            CsFtMonsterFoods = 0;
            CsFtMussel = 0;
            CsFtPumpkin = 0;
            CsFtRoastedBirchnut = 0;
            CsFtRoastedCoffeeBeans = 0;
            CsFtSeaweed = 0;
            CsFtSharkFin = 0;
            CsFtSweetener = 0;
            CsFtSweetPotato = 0;
            CsFtTwigs = 0;
            CsFtVegetables = 0;
            CsFtWatermelon = 0;
            CsFtWobster = 0;
            CsFtRoyalJelly = 0;
            CsFtRoe = 0;
            CsFtRoeCooked = 0;
            CsFtNeonQuattro = 0;
            CsFtPierrotFish = 0;
            CsFtPurpleGrouper = 0;
            #endregion
            #region 属性统计
            CS_RecipeStatistics(CsRecipe1);
            CS_RecipeStatistics(CsRecipe2);
            CS_RecipeStatistics(CsRecipe3);
            CS_RecipeStatistics(CsRecipe4);
            #endregion
            #region 烹饪
            // ------------------------SW------------------------
            // 便携式烹饪锅的四种食物
            if (Global.GameVersion == 4)
            {
                if (CsFtVegetables == 1 && CsFtNeonQuattro == 1 && CsFtPierrotFish == 1 && CsFtPurpleGrouper == 1)
                    CS_CrockPotListAddFood("F_tropical_bouillabaisse", 40);
                if (CrockpotComboBox.SelectedIndex == 1)
                {
                    if (CsFtFruit >= 2 && CsFtButter >= 1 && CsFtHoney >= 1)
                        CS_CrockPotListAddFood("F_fresh_fruit_crepes", 30);
                    if (CsFtMonsterFoods >= 2 && CsFtEggs >= 1 && CsFtVegetables >= 0.5)
                        CS_CrockPotListAddFood("F_monster_tartare", 30);
                    if (CsFtMussel >= 2 && CsFtVegetables >= 2)
                        CS_CrockPotListAddFood("F_mussel_bouillabaise", 30);
                    if (CsFtSweetPotato >= 2 && CsFtEggs >= 2)
                        CS_CrockPotListAddFood("F_sweet_potato_souffle", 30);
                }
                if (CsFtWobster >= 1 && CsFtIce >= 1)
                    CS_CrockPotListAddFood("F_lobster_bisque", 30);
                if (CsFtLimpets >= 3 && CsFtIce >= 1)
                    CS_CrockPotListAddFood("F_bisque", 30);
                if (CsFtRoastedCoffeeBeans >= 3 && (CsFtRoastedCoffeeBeans == 4 || CsFtSweetener == 1 || CsFtDairyProduct == 1))
                    CS_CrockPotListAddFood("F_coffee", 30);
                if (CsFtMeats >= 2.5 && CsFtFishes >= 1.5 && CsFtIce == 0)
                    CS_CrockPotListAddFood("F_surf_'n'_turf", 30);
                if (CsFtWobster >= 1 && CsFtButter >= 1 && CsFtMeats == 0 && CsFtIce == 0)
                    CS_CrockPotListAddFood("F_lobster_dinner", 25);
                if (CsFtVegetables >= 1 && (CsFtRoe >= 1 || CsFtRoeCooked >= 3))
                    CS_CrockPotListAddFood("F_caviar", 20);
                if (CsFtBanana >= 1 && CsFtIce >= 1 && CsFtTwigs >= 1 && CsFtMeats == 0 && CsFtFishes == 0)
                    CS_CrockPotListAddFood("F_banana_pop", 20);
                if (CsFtFishes >= 1 && CsFtSeaweed == 2)
                    CS_CrockPotListAddFood("F_california_roll", 20);
                if (CsFtJellyfish >= 1 && CsFtIce >= 1 && CsFtTwigs >= 1)
                    CS_CrockPotListAddFood("F_jelly-O_pop", 20);
                if (CsFtFishes >= 2 && CsFtIce >= 1)
                    CS_CrockPotListAddFood("F_ceviche", 20);
                if (CsFtSharkFin >= 1)
                    CS_CrockPotListAddFood("F_shark_fin_soup", 20);
                if (CsFtFishes >= 2.5)
                    CS_CrockPotListAddFood("F_seafood_gumbo", 10);
            }
            // ------------------------其他------------------------
            if (CsFtRoyalJelly >= 1 && CsFtTwigs == 0 && CsFtMonsterFoods == 0)
                CS_CrockPotListAddFood("F_jellybeans", 12);
            if (CsFtCactusFlesh >= 1 && CsFtMoleworm >= 1 && CsFtFruit == 0)
                CS_CrockPotListAddFood("F_guacamole", 10);
            if (CsFtCactusFlower >= 1 && CsFtVegetables >= 2 && CsFtFruit == 0 && CsFtMeats == 0 && CsFtEggs == 0 && CsFtSweetener == 0 && CsFtTwigs == 0)
                CS_CrockPotListAddFood("F_flower_salad", 10);
            if (CsFtDairyProduct >= 1 && CsFtIce >= 1 && CsFtSweetener >= 1 && CsFtMeats == 0 && CsFtEggs == 0 && CsFtVegetables == 0 && CsFtTwigs == 0)
                CS_CrockPotListAddFood("F_ice_cream", 10);
            if (CsFtWatermelon >= 1 && CsFtIce >= 1 && CsFtTwigs >= 1 && CsFtMeats == 0 && CsFtEggs == 0 && CsFtVegetables == 0)
                CS_CrockPotListAddFood("F_melonsicle", 10);
            if (CsFtRoastedBirchnut >= 1 && CsFtBerries >= 1 && CsFtFruit >= 1 && CsFtMeats == 0 && CsFtEggs == 0 && CsFtVegetables == 0 && CsFtSweetener == 0)
                CS_CrockPotListAddFood("F_trail_mix", 10);
            if (CsFtVegetables >= 1.5 && CsFtMeats >= 1.5)
                CS_CrockPotListAddFood("F_spicy_chili", 10);
            if (CsFtEel >= 1 && CsFtLichen >= 1)
                CS_CrockPotListAddFood("F_unagi", 20);
            if (CsFtPumpkin >= 1 && CsFtSweetener >= 2)
                CS_CrockPotListAddFood("F_pumpkin_cookie", 10);
            if (CsFtCorn >= 1 && CsFtHoney >= 1 && CsFtTwigs >= 1)
                CS_CrockPotListAddFood("F_powdercake", 10);
            if (CsFtMandrake >= 1)
                CS_CrockPotListAddFood("F_mandrake_soup", 10);
            if (CsFtFishes >= 0.5 && CsFtTwigs == 1)
                CS_CrockPotListAddFood("F_fishsticks", 10);
            if (CsFtFishes >= 0.5 && CsFtCorn >= 1)
                CS_CrockPotListAddFood("F_fish_tacos", 10);
            if (CsFtMeats >= 1.5 && CsFtEggs >= 2 && CsFtVegetables == 0)
                CS_CrockPotListAddFood("F_bacon_and_eggs", 10);
            if (CsFtDrumstick >= 2 && CsFtMeats >= 1.5 && (CsFtVegetables >= 0.5 || CsFtFruit >= 0.5))
                CS_CrockPotListAddFood("F_turkey_dinner", 10);
            if (CsFtSweetener >= 3 && CsFtMeats == 0)
                CS_CrockPotListAddFood("F_taffy", 10);
            if (CsFtButter >= 1 && CsFtEggs >= 1 && CsFtBerries >= 1)
                CS_CrockPotListAddFood("F_waffles", 10);
            if (CsFtMonsterFoods >= 2 && CsFtTwigs == 0)
                CS_CrockPotListAddFood("F_monster_lasagna", 10);
            if (CsFtEggs >= 1 && CsFtMeats >= 0.5 && CsFtVegetables >= 0.5 && CsFtTwigs == 0)
                CS_CrockPotListAddFood("F_pierogi", 5);
            if (CsFtMeats >= 0.5 && CsFtTwigs == 1 && CsFtMonsterFoods <= 1)
                CS_CrockPotListAddFood("F_kabobs", 5);
            if (CsFtMeats >= 2 && CsFtHoney >= 1 && CsFtTwigs == 0)
                CS_CrockPotListAddFood("F_honey_ham", 2);
            if (CsFtMeats >= 0.5 && CsFtMeats < 2 && CsFtHoney >= 1 && CsFtTwigs == 0)
                CS_CrockPotListAddFood("F_honey_nuggets", 2);
            if (CsFtButterflyWings >= 1 && CsFtVegetables >= 0.5 && CsFtMeats == 0)
                CS_CrockPotListAddFood("F_butter_muffin", 1);
            if (CsFtFrogLegs >= 1 && CsFtVegetables >= 0.5)
                CS_CrockPotListAddFood("F_froggle_bunwich", 1);
            if (CsFtDragonFruit >= 1 && CsFtMeats == 0)
                CS_CrockPotListAddFood("F_dragonpie", 1);
            if (CsFtEggplant >= 1 && CsFtVegetables >= 0.5)
                CS_CrockPotListAddFood("F_stuffed_eggplant", 1);
            if (CsFtVegetables >= 0.5 && CsFtMeats == 0 && CsFtTwigs == 0)
                CS_CrockPotListAddFood("F_ratatouille", 0);
            if (CsFtFruit >= 0.5 && CsFtMeats == 0 && CsFtVegetables == 0)
            {
                if (CsFtFruit < 3)
                {
                    CS_CrockPotListAddFood("F_fist_full_of_jam", 0);
                }
                else
                {
                    if (CsFtTwigs == 0)
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
            if (CsFtMeats >= 3 && CsFtTwigs == 0)
            {
                CS_CrockPotListAddFood("F_meaty_stew", 0);
            }

            if (CsFtMeats >= 0.5 && CsFtMeats < 3 && CsFtTwigs == 0)
                CS_CrockPotListAddFood("F_meatballs", -1);
            #endregion
            #region 食物判断
            if (CrockPotListIndex == -1)
            {
                CS_CrockPotListAddFood("F_wet_goop", -2);
            }
            CsFoodName = CrockPotList[0];
            //显示食物图片
            CS_image_Food_Result_Source(CsFoodName);
            //显示食物名称
            FoodResultTextBlock.Text = CS_Food_Text(CsFoodName);
            //显示食物属性
            CS_FoodRecipe_Property(CsFoodName);
            #endregion
            #region 选择按钮显示判断
            if (CrockPotListIndex < 1)
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
                CsRecipe1 = "";
                CsRecipe2 = "";
                CsRecipe3 = "";
                CsRecipe4 = "";
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
            if (foodPriority >= CrockPotMaxPriority)
            {
                CrockPotMaxPriority = foodPriority;
                CrockPotListIndex += 1;
                CrockPotList.Add(foodName);
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
        /// <param name="source">食物代码</param>
        /// <returns>烹饪结果</returns>
        private string CS_Food_Text(string source)
        {
            switch (source)
            {
                case "F_tropical_bouillabaisse":
                    return "热带鱼羹";
                case "F_caviar":
                    return "鱼子酱";
                case "F_fresh_fruit_crepes":
                    return "新鲜水果薄饼";
                case "F_monster_tartare":
                    return "怪物鞑靼";
                case "F_mussel_bouillabaise":
                    return "贝类淡菜汤";
                case "F_sweet_potato_souffle":
                    return "薯蛋奶酥";
                case "F_lobster_bisque":
                    return "龙虾浓汤";
                case "F_bisque":
                    return "汤";
                case "F_coffee":
                    return "咖啡";
                case "F_surf_'n'_turf":
                    return "海鲜牛排";
                case "F_lobster_dinner":
                    return "龙虾正餐";
                case "F_banana_pop":
                    return "香蕉冰淇淋";
                case "F_california_roll":
                    return "加州卷";
                case "F_jelly-O_pop":
                    return "果冻冰淇淋";
                case "F_ceviche":
                    return "橘汁腌鱼";
                case "F_shark_fin_soup":
                    return "鱼翅汤";
                case "F_seafood_gumbo":
                    return "海鲜汤";
                case "F_jellybeans":
                    return "糖豆";
                case "F_guacamole":
                    return Global.GameVersion == 4 ? "鼹梨沙拉酱" : "鼹鼠鳄梨酱";
                case "F_flower_salad":
                    return Global.GameVersion == 4 ? "花沙拉" : "花瓣沙拉";
                case "F_ice_cream":
                    return "冰淇淋";
                case "F_melonsicle":
                    return Global.GameVersion == 4 ? "西瓜冰棍" : "西瓜冰";
                case "F_trail_mix":
                    return Global.GameVersion == 4 ? "什锦干果" : "水果杂烩";
                case "F_spicy_chili":
                    return Global.GameVersion == 4 ? "辣椒炖肉" : "辣椒酱";
                case "F_unagi":
                    return Global.GameVersion == 4 ? "鳗鱼料理" : "鳗鱼";
                case "F_pumpkin_cookie":
                    return "南瓜饼";
                case "F_powdercake":
                    return "芝士蛋糕";
                case "F_mandrake_soup":
                    return Global.GameVersion == 4 ? "曼德拉草汤" : "曼德拉汤";
                case "F_fishsticks":
                    return Global.GameVersion == 4 ? "炸鱼排" : "炸鱼条";
                case "F_fish_tacos":
                    return Global.GameVersion == 4 ? "鱼肉玉米卷" : "玉米饼包炸鱼";
                case "F_bacon_and_eggs":
                    return "培根煎蛋";
                case "F_turkey_dinner":
                    return Global.GameVersion == 4 ? "火鸡大餐" : "火鸡正餐";
                case "F_taffy":
                    return "太妃糖";
                case "F_waffles":
                    return "华夫饼";
                case "F_monster_lasagna":
                    return "怪物千层饼";
                case "F_pierogi":
                    return Global.GameVersion == 4 ? "波兰水饺" : "饺子";
                case "F_kabobs":
                    return "肉串";
                case "F_honey_ham":
                    return "蜜汁火腿";
                case "F_honey_nuggets":
                    return Global.GameVersion == 4 ? "蜜汁卤肉" : "甜蜜金砖";
                case "F_butter_muffin":
                    return Global.GameVersion == 4 ? "奶油玛芬" : "奶油松饼";
                case "F_froggle_bunwich":
                    return Global.GameVersion == 4 ? "蛙腿三明治" : "青蛙圆面包三明治";
                case "F_dragonpie":
                    return "火龙果派";
                case "F_stuffed_eggplant":
                    return Global.GameVersion == 4 ? "酿茄子" : "香酥茄盒";
                case "F_ratatouille":
                    return Global.GameVersion == 4 ? "蔬菜大杂烩" : "蔬菜杂烩";
                case "F_fist_full_of_jam":
                    return Global.GameVersion == 4 ? "满满的果酱" : "果酱蜜饯";
                case "F_fruit_medley":
                    return Global.GameVersion == 4 ? "水果圣代" : "水果沙拉";
                case "F_meaty_stew":
                    return "肉汤";
                case "F_meatballs":
                    return "肉丸";
                case "F_wet_goop":
                    return Global.GameVersion == 4 ? "失败料理" : "湿腻焦糊";
                default:
                    return null;
            }
        }

        /// <summary>
        /// 烹饪结果属性
        /// </summary>
        /// <param name="source">食物代码</param>
        private void CS_FoodRecipe_Property(string source)
        {
            foreach (var foodRecipe in _foodRecipeData)
            {
                if (source == foodRecipe.Picture)
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
            var picturePath = CrockPotList[FoodIndex];
            var rightFrame = Global.RightFrame;
            Global.SetAutoSuggestBoxItem();
            foreach (var suggestBoxItem in Global.AutoSuggestBoxItemSource)
            {
                if (picturePath != StringProcess.GetFileName(suggestBoxItem.Picture)) continue;
                Global.PageJump(2);
                var extraData = new[] { suggestBoxItem.SourcePath, suggestBoxItem.Picture };
                rightFrame.NavigationService.Navigate(new FoodPage(), extraData);
                break;
            }
        }

        /// <summary>
        /// 左右切换按钮
        /// </summary>
        private void SwitchLeftButton_Click(object sender, RoutedEventArgs e)
        {
            SwitchRightButton.IsEnabled = true;
            if (FoodIndex != 0)
            {
                FoodIndex -= 1;
                if (FoodIndex == 0)
                {
                    SwitchLeftButton.IsEnabled = false;
                }
                CsFoodName = CrockPotList[FoodIndex];
                CS_image_Food_Result_Source(CrockPotList[FoodIndex]);
                CS_FoodRecipe_Property(CrockPotList[FoodIndex]);
            }
            FoodResultTextBlock.Text = CS_Food_Text(CsFoodName);
        }

        private void SwitchRightButton_Click(object sender, RoutedEventArgs e)
        {
            SwitchLeftButton.IsEnabled = true;
            if (FoodIndex != CrockPotListIndex)
            {
                FoodIndex += 1;
                if (FoodIndex == CrockPotListIndex)
                {
                    SwitchRightButton.IsEnabled = false;
                }
                CsFoodName = CrockPotList[FoodIndex];
                CS_image_Food_Result_Source(CrockPotList[FoodIndex]);
                CS_FoodRecipe_Property(CrockPotList[FoodIndex]);
            }
            FoodResultTextBlock.Text = CS_Food_Text(CsFoodName);
        }
        #endregion

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UiSplitter.Height = ActualHeight;
        }
    }
}
