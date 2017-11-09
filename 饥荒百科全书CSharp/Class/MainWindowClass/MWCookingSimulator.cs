using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using 饥荒百科全书CSharp.Class;
using 饥荒百科全书CSharp.MyUserControl;

namespace 饥荒百科全书CSharp
{
    /// <summary>
    /// MainWindowCookingSimulator
    /// </summary>
    public partial class MainWindow : Window
    {
        #region 变量初始化
        //重置
        private void button_CS_Reset_Click(object sender, RoutedEventArgs e)
        {
            Button_CS_Food_1_Click(null, null);
            Button_CS_Food_2_Click(null, null);
            Button_CS_Food_3_Click(null, null);
            Button_CS_Food_4_Click(null, null);
            ImageCsFoodResult.Source = null;
            TextBlockCsFoodName.Text = "";
            CrockPotList.Clear();
            CrockPotListIndex = -1;
            CrockPotMaxPriority = -128;
            Visi.VisiCol(true, ButtonCsSwitchLeft, ButtonCsSwitchRight);
        }
        //目录设置
        const string CS_ResourceDir = "GameResources/Food/";
        //四个位置
        public string CS_Recipe_1 = "";
        public string CS_Recipe_2 = "";
        public string CS_Recipe_3 = "";
        public string CS_Recipe_4 = "";
        //38种食材
        public double CS_FT_Eggs = 0;
        public double CS_FT_Vegetables = 0;
        public double CS_FT_Fruit = 0;
        public double CS_FT_Banana = 0;
        public double CS_FT_Berries = 0;
        public double CS_FT_Butter = 0;
        public double CS_FT_Butterfly_wings = 0;
        public double CS_FT_CactusFlesh = 0;
        public double CS_FT_CactusFlower = 0;
        public double CS_FT_Corn = 0;
        public double CS_FT_DairyProduct = 0;
        public double CS_FT_DragonFruit = 0;
        public double CS_FT_Drumstick = 0;
        public double CS_FT_Eel = 0;
        public double CS_FT_Eggplant = 0;
        public double CS_FT_Fishes = 0;
        public double CS_FT_FrogLegs = 0;
        public double CS_FT_Honey = 0;
        public double CS_FT_Ice = 0;
        public double CS_FT_Jellyfish = 0;
        public double CS_FT_Lichen = 0;
        public double CS_FT_Limpets = 0;
        public double CS_FT_Mandrake = 0;
        public double CS_FT_Meats = 0;
        public double CS_FT_Moleworm = 0;
        public double CS_FT_MonsterFoods = 0;
        public double CS_FT_Mussel = 0;
        public double CS_FT_Pumpkin = 0;
        public double CS_FT_RoastedBirchnut = 0;
        public double CS_FT_RoastedCoffeeBeans = 0;
        public double CS_FT_Seaweed = 0;
        public double CS_FT_SharkFin = 0;
        public double CS_FT_Sweetener = 0;
        public double CS_FT_SweetPotato = 0;
        public double CS_FT_Twigs = 0;
        public double CS_FT_Watermelon = 0;
        public double CS_FT_Wobster = 0;
        public double CS_FT_RoyalJelly = 0;

        public byte FoodIndex = 0;
        public string CS_F_name = "";

        public List<string> CrockPotList = new List<string>(); //食物列表
        public sbyte CrockPotListIndex = -1;//食物列表下标
        public sbyte CrockPotMaxPriority = -128; //优先度最大值
        #endregion

        //添加食材
        private void CS_Add(string Name)
        {
            if (CS_Recipe_1 == "")
            {
                CS_Recipe_1 = Name;
                ImageCsFood1.Source = RSN.PictureShortName(Name);
            }
            else if (CS_Recipe_2 == "")
            {
                CS_Recipe_2 = Name;
                ImageCsFood2.Source = RSN.PictureShortName(Name);
            }
            else if (CS_Recipe_3 == "")
            {
                CS_Recipe_3 = Name;
                ImageCsFood3.Source = RSN.PictureShortName(Name);
            }
            else if (CS_Recipe_4 == "")
            {
                CS_Recipe_4 = Name;
                ImageCsFood4.Source = RSN.PictureShortName(Name);
            }
            if (CS_Recipe_1 != "" && CS_Recipe_2 != "" && CS_Recipe_3 != "" && CS_Recipe_4 != "")
            {
                CS_CrockPotCalculation();
            }
        }
        //删除食材
        private void Button_CS_Food_1_Click(object sender, RoutedEventArgs e)
        {
            CS_Recipe_1 = "";
            ImageCsFood1.Source = null;
        }
        private void Button_CS_Food_2_Click(object sender, RoutedEventArgs e)
        {
            CS_Recipe_2 = "";
            ImageCsFood2.Source = null;
        }
        private void Button_CS_Food_3_Click(object sender, RoutedEventArgs e)
        {
            CS_Recipe_3 = "";
            ImageCsFood3.Source = null;
        }
        private void Button_CS_Food_4_Click(object sender, RoutedEventArgs e)
        {
            CS_Recipe_4 = "";
            ImageCsFood4.Source = null;
        }
        //食材属性统计
        private void CS_RecipeStatistics(string Name)
        {
            switch (RSN.GetFileName(Name))
            {
                #region 肉类
                case "F_meat":
                    CS_FT_Meats += 1;
                    break;
                case "F_cooked_meat":
                    CS_FT_Meats += 1;
                    break;
                case "F_jerky":
                    CS_FT_Meats += 1;
                    break;
                case "F_monster_meat":
                    CS_FT_Meats += 1;
                    CS_FT_MonsterFoods += 1;
                    break;
                case "F_cooked_monster_meat":
                    CS_FT_Meats += 1;
                    CS_FT_MonsterFoods += 1;
                    break;
                case "F_monster_jerky":
                    CS_FT_Meats += 1;
                    CS_FT_MonsterFoods += 1;
                    break;
                case "F_morsel":
                    CS_FT_Meats += 0.5;
                    break;
                case "F_cooked_morsel":
                    CS_FT_Meats += 0.5;
                    break;
                case "F_small_jerky":
                    CS_FT_Meats += 0.5;
                    break;
                case "F_drumstick":
                    CS_FT_Meats += 0.5;
                    CS_FT_Drumstick += 1;
                    break;
                case "F_fried_drumstick":
                    CS_FT_Meats += 0.5;
                    CS_FT_Drumstick += 1;
                    break;
                case "F_frog_legs":
                    CS_FT_Meats += 0.5;
                    CS_FT_FrogLegs += 1;
                    break;
                case "F_cooked_frog_legs":
                    CS_FT_Meats += 0.5;
                    CS_FT_FrogLegs += 1;
                    break;
                case "F_fish":
                    CS_FT_Fishes += 1;
                    CS_FT_Meats += 0.5;
                    break;
                case "F_cooked_fish":
                    CS_FT_Fishes += 1;
                    CS_FT_Meats += 0.5;
                    break;
                case "F_eel":
                    CS_FT_Fishes += 1;
                    CS_FT_Eel += 1;
                    CS_FT_Meats += 0.5;
                    break;
                case "F_cooked_eel":
                    CS_FT_Fishes += 1;
                    CS_FT_Eel += 1;
                    CS_FT_Meats += 0.5;
                    break;
                case "F_moleworm":
                    CS_FT_Moleworm += 1;
                    break;
                case "F_limpets":
                    CS_FT_Fishes += 0.5;
                    CS_FT_Limpets += 1;
                    break;
                case "F_cooked_limpets":
                    CS_FT_Fishes += 0.5;
                    break;
                case "F_tropical_fish":
                    CS_FT_Meats += 0.5;
                    CS_FT_Fishes += 1;
                    break;
                case "F_fish_morsel":
                    CS_FT_Fishes += 0.5;
                    break;
                case "F_cooked_fish_morsel":
                    CS_FT_Fishes += 0.5;
                    break;
                case "F_jellyfish":
                    CS_FT_Fishes += 1;
                    CS_FT_MonsterFoods += 1;
                    CS_FT_Jellyfish += 1;
                    break;
                case "F_dead_jellyfish":
                    CS_FT_Fishes += 1;
                    CS_FT_MonsterFoods += 1;
                    break;
                case "F_cooked_jellyfish":
                    CS_FT_Fishes += 1;
                    CS_FT_MonsterFoods += 1;
                    break;
                case "F_dried_jellyfish":
                    CS_FT_Fishes += 1;
                    CS_FT_MonsterFoods += 1;
                    break;
                case "F_mussel":
                    CS_FT_Fishes += 0.5;
                    CS_FT_Mussel += 1;
                    break;
                case "F_cooked_mussel":
                    CS_FT_Fishes += 0.5;
                    CS_FT_Mussel += 1;
                    break;
                case "F_dead_dogfish":
                    CS_FT_Fishes += 1;
                    CS_FT_Meats += 0.5;
                    break;
                case "F_wobster":
                    CS_FT_Fishes += 2;
                    CS_FT_Wobster += 1;
                    break;
                case "F_raw_fish":
                    CS_FT_Fishes += 1;
                    CS_FT_Meats += 0.5;
                    break;
                case "F_fish_steak":
                    CS_FT_Fishes += 1;
                    CS_FT_Meats += 0.5;
                    break;
                case "F_shark_fin":
                    CS_FT_Fishes += 1;
                    CS_FT_Meats += 0.5;
                    CS_FT_SharkFin += 1;
                    break;
                #endregion
                #region 蔬菜
                case "F_blue_cap":
                    CS_FT_Vegetables += 0.5;
                    break;
                case "F_cooked_blue_cap":
                    CS_FT_Vegetables += 0.5;
                    break;
                case "F_green_cap":
                    CS_FT_Vegetables += 0.5;
                    break;
                case "F_cooked_green_cap":
                    CS_FT_Vegetables += 0.5;
                    break;
                case "F_red_cap":
                    CS_FT_Vegetables += 0.5;
                    break;
                case "F_cooked_red_cap":
                    CS_FT_Vegetables += 0.5;
                    break;
                case "F_eggplant":
                    CS_FT_Vegetables += 1;
                    CS_FT_Eggplant += 1;
                    break;
                case "F_braised_eggplant":
                    CS_FT_Vegetables += 1;
                    CS_FT_Eggplant += 1;
                    break;
                case "F_carrot":
                    CS_FT_Vegetables += 1;
                    break;
                case "F_roasted_carrot":
                    CS_FT_Vegetables += 1;
                    break;
                case "F_corn":
                    CS_FT_Vegetables += 1;
                    CS_FT_Corn += 1;
                    break;
                case "F_popcorn":
                    CS_FT_Vegetables += 1;
                    CS_FT_Corn += 1;
                    break;
                case "F_pumpkin":
                    CS_FT_Vegetables += 1;
                    CS_FT_Pumpkin += 1;
                    break;
                case "F_hot_pumpkin":
                    CS_FT_Vegetables += 1;
                    CS_FT_Pumpkin += 1;
                    break;
                case "F_cactus_flesh":
                    CS_FT_Vegetables += 1;
                    CS_FT_CactusFlesh += 1;
                    break;
                case "F_cooked_cactus_flesh":
                    CS_FT_Vegetables += 1;
                    break;
                case "F_cactus_flower":
                    CS_FT_Vegetables += 0.5;
                    CS_FT_CactusFlower += 1;
                    break;
                case "F_sweet_potato":
                    CS_FT_Vegetables += 1;
                    CS_FT_SweetPotato += 1;
                    break;
                case "F_cooked_sweet_potato":
                    CS_FT_Vegetables += 1;
                    break;
                case "F_seaweed":
                    CS_FT_Vegetables += 0.5;
                    CS_FT_Seaweed += 1;
                    break;
                case "F_roasted_seaweed":
                    CS_FT_Vegetables += 0.5;
                    break;
                case "F_dried_seaweed":
                    CS_FT_Vegetables += 0.5;
                    break;
                #endregion
                #region 水果
                case "F_juicy_berries":
                    CS_FT_Fruit += 0.5;
                    break;
                case "F_roasted_juicy_berries":
                    CS_FT_Fruit += 0.5;
                    break;
                case "F_berries":
                    CS_FT_Fruit += 0.5;
                    CS_FT_Berries += 1;
                    break;
                case "F_roasted_berrie":
                    CS_FT_Fruit += 0.5;
                    CS_FT_Berries += 1;
                    break;
                case "F_banana":
                    CS_FT_Fruit += 1;
                    CS_FT_Banana += 1;
                    break;
                case "F_cooked_banana":
                    CS_FT_Fruit += 1;
                    break;
                case "F_dragon_fruit":
                    CS_FT_Fruit += 1;
                    CS_FT_DragonFruit += 1;
                    break;
                case "F_prepared_dragon_fruit":
                    CS_FT_Fruit += 1;
                    CS_FT_DragonFruit += 1;
                    break;
                case "F_durian":
                    CS_FT_Fruit += 1;
                    CS_FT_MonsterFoods += 1;
                    break;
                case "F_extra_smelly_durian":
                    CS_FT_Fruit += 1;
                    CS_FT_MonsterFoods += 1;
                    break;
                case "F_pomegranate":
                    CS_FT_Fruit += 1;
                    break;
                case "F_sliced_pomegranate":
                    CS_FT_Fruit += 1;
                    break;
                case "F_watermelon":
                    CS_FT_Fruit += 1;
                    CS_FT_Watermelon += 1;
                    break;
                case "F_grilled_watermelon":
                    CS_FT_Fruit += 1;
                    break;
                case "F_halved_coconut":
                    CS_FT_Fruit += 1;
                    break;
                case "F_roasted_coconut":
                    CS_FT_Fruit += 1;
                    break;
                case "F_coffee_beans":
                    CS_FT_Fruit += 0.5;
                    break;
                case "F_roasted_coffee_beans":
                    CS_FT_Fruit += 1;
                    CS_FT_RoastedCoffeeBeans += 1;
                    break;
                #endregion
                #region 蛋类
                case "F_egg":
                    CS_FT_Eggs += 1;
                    break;
                case "F_cooked_egg":
                    CS_FT_Eggs += 1;
                    break;
                case "F_tallbird_egg":
                    CS_FT_Eggs += 4;
                    break;
                case "F_fried_tallbird_egg":
                    CS_FT_Eggs += 4;
                    break;
                case "F_doydoy_egg":
                    CS_FT_Eggs += 1;
                    break;
                case "F_fried_doydoy_egg":
                    CS_FT_Eggs += 1;
                    break;
                #endregion
                #region 其他
                case "F_butterfly_wing":
                    CS_FT_Butterfly_wings += 1;
                    break;
                case "F_butterfly_wing_sw":
                    CS_FT_Butterfly_wings += 1;
                    break;
                case "F_butter":
                    CS_FT_DairyProduct += 1;
                    CS_FT_Butter += 1;
                    break;
                case "F_honey":
                    CS_FT_Sweetener += 1;
                    CS_FT_Honey += 1;
                    break;
                case "F_honeycomb":
                    CS_FT_Sweetener += 1;
                    break;
                case "F_lichen":
                    CS_FT_Vegetables += 1;
                    CS_FT_Lichen += 1;
                    break;
                case "F_mandrake":
                    CS_FT_Vegetables += 1;
                    CS_FT_Mandrake += 1;
                    break;
                case "F_electric_milk":
                    CS_FT_DairyProduct += 1;
                    break;
                case "F_ice":
                    CS_FT_Ice += 1;
                    break;
                case "F_roasted_birchnut":
                    CS_FT_RoastedBirchnut += 1;
                    break;
                case "F_royal_jelly":
                    CS_FT_RoyalJelly += 1;
                    break;
                case "F_twigs":
                    CS_FT_Twigs += 1;
                    break;
                    #endregion
            }
        }
        //烹饪计算
        private void CS_CrockPotCalculation()
        {
            //// 判断食材是否足够
            //if (CS_Recipe_1 == "" || CS_Recipe_2 == "" || CS_Recipe_3 == "" || CS_Recipe_4 == "")
            //{
            //    MessageBox.Show("食材不足，请添加！");
            //}
            FoodIndex = 0;
            #region 食物列表初始化
            CrockPotList.Clear();
            CrockPotListIndex = -1;
            CrockPotMaxPriority = -128;
            #endregion
            #region 食材属性初始化
            CS_FT_Banana = 0;
            CS_FT_Berries = 0;
            CS_FT_Butter = 0;
            CS_FT_Butterfly_wings = 0;
            CS_FT_CactusFlesh = 0;
            CS_FT_CactusFlower = 0;
            CS_FT_Corn = 0;
            CS_FT_DairyProduct = 0;
            CS_FT_DragonFruit = 0;
            CS_FT_Drumstick = 0;
            CS_FT_Eel = 0;
            CS_FT_Eggplant = 0;
            CS_FT_Eggs = 0;
            CS_FT_Fishes = 0;
            CS_FT_FrogLegs = 0;
            CS_FT_Fruit = 0;
            CS_FT_Honey = 0;
            CS_FT_Ice = 0;
            CS_FT_Jellyfish = 0;
            CS_FT_Lichen = 0;
            CS_FT_Limpets = 0;
            CS_FT_Mandrake = 0;
            CS_FT_Meats = 0;
            CS_FT_Moleworm = 0;
            CS_FT_MonsterFoods = 0;
            CS_FT_Mussel = 0;
            CS_FT_Pumpkin = 0;
            CS_FT_RoastedBirchnut = 0;
            CS_FT_RoastedCoffeeBeans = 0;
            CS_FT_Seaweed = 0;
            CS_FT_SharkFin = 0;
            CS_FT_Sweetener = 0;
            CS_FT_SweetPotato = 0;
            CS_FT_Twigs = 0;
            CS_FT_Vegetables = 0;
            CS_FT_Watermelon = 0;
            CS_FT_Wobster = 0;
            CS_FT_RoyalJelly = 0;
            #endregion
            #region 属性统计
            CS_RecipeStatistics(CS_Recipe_1);
            CS_RecipeStatistics(CS_Recipe_2);
            CS_RecipeStatistics(CS_Recipe_3);
            CS_RecipeStatistics(CS_Recipe_4);
            #endregion
            #region 烹饪
            //------------------------SW------------------------
            if (UiGameversion.SelectedIndex == 2)
            {
                //便携式烹饪锅的四种食物
                if (ComboBoxCsCrockpot.SelectedIndex == 1)
                {
                    if (CS_FT_Fruit >= 2 && CS_FT_Butter >= 1 && CS_FT_Honey >= 1)
                        CS_CrockPotListAddFood("F_fresh_fruit_crepes", 30);
                    if (CS_FT_MonsterFoods >= 2 && CS_FT_Eggs >= 1 && CS_FT_Vegetables >= 0.5)
                        CS_CrockPotListAddFood("F_monster_tartare", 30);
                    if (CS_FT_Mussel >= 2 && CS_FT_Vegetables >= 2)
                        CS_CrockPotListAddFood("F_mussel_bouillabaise", 30);
                    if (CS_FT_SweetPotato >= 2 && CS_FT_Eggs >= 2)
                        CS_CrockPotListAddFood("F_sweet_potato_souffle", 30);
                }
                if (CS_FT_Wobster >= 1 && CS_FT_Ice >= 1)
                    CS_CrockPotListAddFood("F_lobster_bisque", 30);
                if (CS_FT_Limpets >= 3 && CS_FT_Ice >= 1)
                    CS_CrockPotListAddFood("F_bisque", 30);
                if (CS_FT_RoastedCoffeeBeans >= 3 && (CS_FT_RoastedCoffeeBeans == 4 || CS_FT_Sweetener == 1 || CS_FT_DairyProduct == 1))
                    CS_CrockPotListAddFood("F_coffee", 30);
                if (CS_FT_Meats >= 2.5 && CS_FT_Fishes >= 1.5 && CS_FT_Ice == 0)
                    CS_CrockPotListAddFood("F_surf_'n'_turf", 30);
                if (CS_FT_Wobster >= 1 && CS_FT_Butter >= 1 && CS_FT_Meats == 0 && CS_FT_Ice == 0)
                    CS_CrockPotListAddFood("F_lobster_dinner", 25);
                if (CS_FT_Banana >= 1 && CS_FT_Ice >= 1 && CS_FT_Twigs >= 1 && CS_FT_Meats == 0 && CS_FT_Fishes == 0)
                    CS_CrockPotListAddFood("F_banana_pop", 20);
                if (CS_FT_Fishes >= 1 && CS_FT_Seaweed == 2)
                    CS_CrockPotListAddFood("F_california_roll", 20);
                if (CS_FT_Jellyfish >= 1 && CS_FT_Ice >= 1 && CS_FT_Twigs >= 1)
                    CS_CrockPotListAddFood("F_jelly-O_pop", 20);
                if (CS_FT_Fishes >= 2 && CS_FT_Ice >= 1)
                    CS_CrockPotListAddFood("F_ceviche", 20);
                if (CS_FT_SharkFin >= 1)
                    CS_CrockPotListAddFood("F_shark_fin_soup", 20);
                if (CS_FT_Fishes >= 2.5)
                    CS_CrockPotListAddFood("F_seafood_gumbo", 10);
            }
            //------------------------其他------------------------
            if (CS_FT_RoyalJelly >= 1 && CS_FT_Twigs == 0 && CS_FT_MonsterFoods == 0)
                CS_CrockPotListAddFood("F_jellybeans", 12);
            if (CS_FT_CactusFlesh >= 1 && CS_FT_Moleworm >= 1 && CS_FT_Fruit == 0)
                CS_CrockPotListAddFood("F_guacamole", 10);
            if (CS_FT_CactusFlower >= 1 && CS_FT_Vegetables >= 2 && CS_FT_Fruit == 0 && CS_FT_Meats == 0 && CS_FT_Eggs == 0 && CS_FT_Sweetener == 0 && CS_FT_Twigs == 0)
                CS_CrockPotListAddFood("F_flower_salad", 10);
            if (CS_FT_DairyProduct >= 1 && CS_FT_Ice >= 1 && CS_FT_Sweetener >= 1 && CS_FT_Meats == 0 && CS_FT_Eggs == 0 && CS_FT_Vegetables == 0 && CS_FT_Twigs == 0)
                CS_CrockPotListAddFood("F_ice_cream", 10);
            if (CS_FT_Watermelon >= 1 && CS_FT_Ice >= 1 && CS_FT_Twigs >= 1 && CS_FT_Meats == 0 && CS_FT_Eggs == 0 && CS_FT_Vegetables == 0)
                CS_CrockPotListAddFood("F_melonsicle", 10);
            if (CS_FT_RoastedBirchnut >= 1 && CS_FT_Berries >= 1 && CS_FT_Fruit >= 1 && CS_FT_Meats == 0 && CS_FT_Eggs == 0 && CS_FT_Vegetables == 0 && CS_FT_Sweetener == 0)
                CS_CrockPotListAddFood("F_trail_mix", 10);
            if (CS_FT_Vegetables >= 1.5 && CS_FT_Meats >= 1.5)
                CS_CrockPotListAddFood("F_spicy_chili", 10);
            if (CS_FT_Eel >= 1 && CS_FT_Lichen >= 1)
                CS_CrockPotListAddFood("F_unagi", 20);
            if (CS_FT_Pumpkin >= 1 && CS_FT_Sweetener >= 2)
                CS_CrockPotListAddFood("F_pumpkin_cookie", 10);
            if (CS_FT_Corn >= 1 && CS_FT_Honey >= 1 && CS_FT_Twigs >= 1)
                CS_CrockPotListAddFood("F_powdercake", 10);
            if (CS_FT_Mandrake >= 1)
                CS_CrockPotListAddFood("F_mandrake_soup", 10);
            if (CS_FT_Fishes >= 0.5 && CS_FT_Twigs == 1)
                CS_CrockPotListAddFood("F_fishsticks", 10);
            if (CS_FT_Fishes >= 0.5 && CS_FT_Corn >= 1)
                CS_CrockPotListAddFood("F_fish_tacos", 10);
            if (CS_FT_Meats >= 1.5 && CS_FT_Eggs >= 2 && CS_FT_Vegetables == 0)
                CS_CrockPotListAddFood("F_bacon_and_eggs", 10);
            if (CS_FT_Drumstick >= 2 && CS_FT_Meats >= 1.5 && (CS_FT_Vegetables >= 0.5 || CS_FT_Fruit >= 0.5))
                CS_CrockPotListAddFood("F_turkey_dinner", 10);
            if (CS_FT_Sweetener >= 3 && CS_FT_Meats == 0)
                CS_CrockPotListAddFood("F_taffy", 10);
            if (CS_FT_Butter >= 1 && CS_FT_Eggs >= 1 && CS_FT_Berries >= 1)
                CS_CrockPotListAddFood("F_waffles", 10);
            if (CS_FT_MonsterFoods >= 2 && CS_FT_Twigs == 0)
                CS_CrockPotListAddFood("F_monster_lasagna", 10);
            if (CS_FT_Eggs >= 1 && CS_FT_Meats >= 0.5 && CS_FT_Vegetables >= 0.5 && CS_FT_Twigs == 0)
                CS_CrockPotListAddFood("F_pierogi", 5);
            if (CS_FT_Meats >= 0.5 && CS_FT_Twigs == 1 && CS_FT_MonsterFoods <= 1)
                CS_CrockPotListAddFood("F_kabobs", 5);
            if (CS_FT_Meats >= 2 && CS_FT_Honey >= 1 && CS_FT_Twigs == 0)
                CS_CrockPotListAddFood("F_honey_ham", 2);
            if (CS_FT_Meats >= 0.5 && CS_FT_Meats < 2 && CS_FT_Honey >= 1 && CS_FT_Twigs == 0)
                CS_CrockPotListAddFood("F_honey_nuggets", 2);
            if (CS_FT_Butterfly_wings >= 1 && CS_FT_Vegetables >= 0.5 && CS_FT_Meats == 0)
                CS_CrockPotListAddFood("F_butter_muffin", 1);
            if (CS_FT_FrogLegs >= 1 && CS_FT_Vegetables >= 0.5)
                CS_CrockPotListAddFood("F_froggle_bunwich", 1);
            if (CS_FT_DragonFruit >= 1 && CS_FT_Meats == 0)
                CS_CrockPotListAddFood("F_dragonpie", 1);
            if (CS_FT_Eggplant >= 1 && CS_FT_Vegetables >= 0.5)
                CS_CrockPotListAddFood("F_stuffed_eggplant", 1);
            if (CS_FT_Vegetables >= 0.5 && CS_FT_Meats == 0 && CS_FT_Twigs == 0)
                CS_CrockPotListAddFood("F_ratatouille", 0);
            if (CS_FT_Fruit >= 0.5 && CS_FT_Meats == 0 && CS_FT_Vegetables == 0)
            {
                if (CS_FT_Fruit < 3)
                {
                    CS_CrockPotListAddFood("F_fist_full_of_jam", 0);
                }
                else
                {
                    if (CS_FT_Twigs == 0)
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
            if (CS_FT_Meats >= 3 && CS_FT_Twigs == 0)
            {
                CS_CrockPotListAddFood("F_meaty_stew", 0);
            }

            if (CS_FT_Meats >= 0.5 && CS_FT_Meats < 3 && CS_FT_Twigs == 0)
                CS_CrockPotListAddFood("F_meatballs", -1);
            #endregion
            #region 食物判断
            if (CrockPotListIndex == -1)
            {
                CS_CrockPotListAddFood("F_wet_goop", -2);
            }
            CS_F_name = CrockPotList[0];
            CS_image_Food_Result_Source(CrockPotList[0]);
            #endregion
            #region 选择按钮显示判断
            if (CrockPotListIndex < 1)
            {
                ButtonCsSwitchLeft.Visibility = Visibility.Collapsed;
                ButtonCsSwitchRight.Visibility = Visibility.Collapsed;
            }
            else
            {
                ButtonCsSwitchLeft.Visibility = Visibility.Visible;
                ButtonCsSwitchRight.Visibility = Visibility.Visible;
                ButtonCsSwitchLeft.IsEnabled = false;
                ButtonCsSwitchRight.IsEnabled = true;
            }
            #endregion
            //显示食物名称
            TextBlockCsFoodName.Text = CS_Food_Text(CS_F_name);
            #region 自动清空材料
            if (CheckBoxCsAutoClean.IsChecked == true)
            {
                CS_Recipe_1 = "";
                CS_Recipe_2 = "";
                CS_Recipe_3 = "";
                CS_Recipe_4 = "";
                ImageCsFood1.Source = null;
                ImageCsFood2.Source = null;
                ImageCsFood3.Source = null;
                ImageCsFood4.Source = null;
            }
            #endregion
        }
        //向食物列表添加食物
        private void CS_CrockPotListAddFood(string FoodName, sbyte FoodPriority)
        {
            if (FoodPriority >= CrockPotMaxPriority)
            {
                CrockPotMaxPriority = FoodPriority;
                CrockPotListIndex += 1;
                CrockPotList.Add(FoodName);
            }
        }
        //烹饪结果图片
        private void CS_image_Food_Result_Source(string source)
        {
            ImageCsFoodResult.Source = RSN.PictureShortName(RSN.ShortName(source, CS_ResourceDir));
        }
        //烹饪结果文字
        private string CS_Food_Text(string source)
        {
            switch (source)
            {
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
                    if (UiGameversion.SelectedIndex ==4)
                        return "鼹梨沙拉酱";
                    else
                        return "鼹鼠鳄梨酱";
                case "F_flower_salad":
                    if (UiGameversion.SelectedIndex == 4)
                        return "花沙拉";
                    else
                        return "花瓣沙拉";
                case "F_ice_cream":
                    return "冰淇淋";
                case "F_melonsicle":
                    if (UiGameversion.SelectedIndex == 4)
                        return "西瓜冰棍";
                    else
                        return "西瓜冰";
                case "F_trail_mix":
                    if (UiGameversion.SelectedIndex == 4)
                        return "什锦干果";
                    else
                        return "水果杂烩";
                case "F_spicy_chili":
                    if (UiGameversion.SelectedIndex == 4)
                        return "辣椒炖肉";
                    else
                        return "辣椒酱";
                case "F_unagi":
                    if (UiGameversion.SelectedIndex == 4)
                        return "鳗鱼料理";
                    else
                        return "鳗鱼";
                case "F_pumpkin_cookie":
                    return "南瓜饼";
                case "F_powdercake":
                    return "芝士蛋糕";
                case "F_mandrake_soup":
                    if (UiGameversion.SelectedIndex == 4)
                        return "曼德拉草汤";
                    else
                        return "曼德拉汤";
                case "F_fishsticks":
                    if (UiGameversion.SelectedIndex == 4)
                        return "炸鱼排";
                    else
                        return "炸鱼条";
                case "F_fish_tacos":
                    if (UiGameversion.SelectedIndex == 4)
                        return "鱼肉玉米卷";
                    else
                        return "玉米饼包炸鱼";
                case "F_bacon_and_eggs":
                    return "培根煎蛋";
                case "F_turkey_dinner":
                    if (UiGameversion.SelectedIndex == 4)
                        return "火鸡大餐";
                    else
                        return "火鸡正餐";
                case "F_taffy":
                    return "太妃糖";
                case "F_waffles":
                    return "华夫饼";
                case "F_monster_lasagna":
                    return "怪物千层饼";
                case "F_pierogi":
                    if (UiGameversion.SelectedIndex == 4)
                        return "波兰水饺";
                    else
                        return "饺子";
                case "F_kabobs":
                    return "肉串";
                case "F_honey_ham":
                    return "蜜汁火腿";
                case "F_honey_nuggets":
                    if (UiGameversion.SelectedIndex == 4)
                        return "蜜汁卤肉";
                    else
                        return "甜蜜金砖";
                case "F_butter_muffin":
                    if (UiGameversion.SelectedIndex == 4)
                        return "奶油玛芬";
                    else
                        return "奶油松饼";
                case "F_froggle_bunwich":
                    if (UiGameversion.SelectedIndex == 4)
                        return "蛙腿三明治";
                    else
                        return "青蛙圆面包三明治";
                case "F_dragonpie":
                    return "火龙果派";
                case "F_stuffed_eggplant":
                    if (UiGameversion.SelectedIndex == 4)
                        return "酿茄子";
                    else
                        return "香酥茄盒";
                case "F_ratatouille":
                    if (UiGameversion.SelectedIndex == 4)
                        return "蔬菜大杂烩";
                    else
                        return "蔬菜杂烩";
                case "F_fist_full_of_jam":
                    if (UiGameversion.SelectedIndex == 4)
                        return "满满的果酱";
                    else
                        return "果酱蜜饯";
                case "F_fruit_medley":
                    if (UiGameversion.SelectedIndex == 4)
                        return "水果圣代";
                    else
                        return "水果沙拉";
                case "F_meaty_stew":
                    return "肉汤";
                case "F_meatballs":
                    return "肉丸";
                case "F_wet_goop":
                    if (UiGameversion.SelectedIndex == 4)
                        return "失败料理";
                    else
                        return "湿腻焦糊";
                default:
                    return null;
            }
        }
        //左右切换按钮
        private void button_CS_Switch_Left_Click(object sender, RoutedEventArgs e)
        {
            ButtonCsSwitchRight.IsEnabled = true;
            if (FoodIndex != 0)
            {
                FoodIndex -= 1;
                if (FoodIndex == 0)
                {
                    ButtonCsSwitchLeft.IsEnabled = false;
                }
                CS_F_name = CrockPotList[FoodIndex];
                CS_image_Food_Result_Source(CrockPotList[FoodIndex]);
            }
            TextBlockCsFoodName.Text = CS_Food_Text(CS_F_name);
        }
        private void button_CS_Switch_Right_Click(object sender, RoutedEventArgs e)
        {
            ButtonCsSwitchLeft.IsEnabled = true;
            if (FoodIndex != CrockPotListIndex)
            {
                FoodIndex += 1;
                if (FoodIndex == CrockPotListIndex)
                {
                    ButtonCsSwitchRight.IsEnabled = false;
                }
                CS_F_name = CrockPotList[FoodIndex];
                CS_image_Food_Result_Source(CrockPotList[FoodIndex]);
            }
            TextBlockCsFoodName.Text = CS_Food_Text(CS_F_name);
        }
        //烹饪结果跳转
        private void button_CS_Food_Result_Click(object sender, RoutedEventArgs e)
        {
            foreach (UIElement expanderStackpanel in WrapPanelRightFood.Children)
            {
                foreach (UIElement buttonWithText in ((ExpanderStackpanel)expanderStackpanel).UcWrapPanel.Children)
                {
                    string[] RightButtonTag = (string[])(((ButtonWithText)buttonWithText).UCButton.Tag);
                    string RightButtonTag0 = RightButtonTag[0];
                    RightButtonTag0 = RSN.GetFileName(RightButtonTag0);
                    if (CrockPotList[FoodIndex] == RightButtonTag0)
                    {
                        Sidebar_Food_Click(null, null);
                        SidebarFood.IsChecked = true;
                        WrapPanelLeftFood.UpdateLayout();
                        Food_Click(((ButtonWithText)buttonWithText).UCButton, null);
                        WrapPanelLeftFood.UpdateLayout();
                        //Point point = ((ButtonWithText)buttonWithText).TransformToVisual(WrapPanel_Right_Food).Transform(new Point(0, 0));
                        //ScrollViewer_Right_Food.ScrollToVerticalOffset(point.Y);
                    }
                }
            }
        }

        //CookingSimulator面板Click事件
        private void CookingSimulator_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string[] BWTTag = (string[])button.Tag;//获取参数
            CookingSimulator_Click_Handle(BWTTag);
        }
        //WrapPanel_Left_CookingSimulator控件创建事件
        private void CookingSimulator_Click_Handle(string[] BWTTag)
        {
            //BWTTag = { Picture, Name, Health, Hunger, Sanity, Perish, Attribute, AttributeValue, Attribute_2, AttributeValue_2, Introduce };

            try
            {
                CS_Add(BWTTag[0]);
            }
            catch { }
            WrapPanel_Left_Food_SizeChanged(null, null);//调整位置
        }

        //WrapPanel_Left_Cooking_Simulator内Grid.Width设置为WrapPanel_Left_Cooking_Simulator.ActualWidth
        private void WrapPanel_Left_Cooking_Simulator_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            int LeftCookingSimulatorWidth = (int)WrapPanelLeftCookingSimulator.ActualWidth;
            foreach (UIElement uielement in WrapPanelLeftCookingSimulator.Children)
            {
                if (uielement.GetType().ToString() == "System.Windows.Controls.Grid")
                {
                    ((Grid)uielement).Width = LeftCookingSimulatorWidth;
                }
                if (uielement.GetType().ToString() == "System.Windows.Controls.WrapPanel")
                {
                    ((WrapPanel)uielement).Width = LeftCookingSimulatorWidth;
                }
                if (uielement.GetType().ToString() == "System.Windows.Controls.StackPanel")
                {
                    ((StackPanel)uielement).Width = LeftCookingSimulatorWidth;
                }
            }
        }
        //WrapPanel_Right_Cooking_Simulator内Expander.Width设置为WrapPanel_Right_Cooking_Simulator.ActualWidth
        private void WrapPanel_Right_Cooking_Simulator_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            foreach (ExpanderStackpanel expanderStackpanel in WrapPanelRightCookingSimulator.Children)
            {
                expanderStackpanel.Width = (int)WrapPanelRightCookingSimulator.ActualWidth;
            }
        }
    }
}
