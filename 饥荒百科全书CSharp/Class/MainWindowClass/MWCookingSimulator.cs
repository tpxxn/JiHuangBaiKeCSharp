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
            Image_CS_Food_Result.Source = null;
            CrockPotList.Clear();
            CrockPotListIndex = -1;
            CrockPotMaxPriority = -128;
            Visi.VisiCol(true, button_CS_Switch_Left, button_CS_Switch_Right);
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
                Image_CS_Food_1.Source = RSN.PictureShortName(Name);
            }
            else if (CS_Recipe_2 == "")
            {
                CS_Recipe_2 = Name;
                Image_CS_Food_2.Source = RSN.PictureShortName(Name);
            }
            else if (CS_Recipe_3 == "")
            {
                CS_Recipe_3 = Name;
                Image_CS_Food_3.Source = RSN.PictureShortName(Name);
            }
            else if (CS_Recipe_4 == "")
            {
                CS_Recipe_4 = Name;
                Image_CS_Food_4.Source = RSN.PictureShortName(Name);
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
            Image_CS_Food_1.Source = null;
        }
        private void Button_CS_Food_2_Click(object sender, RoutedEventArgs e)
        {
            CS_Recipe_2 = "";
            Image_CS_Food_2.Source = null;
        }
        private void Button_CS_Food_3_Click(object sender, RoutedEventArgs e)
        {
            CS_Recipe_3 = "";
            Image_CS_Food_3.Source = null;
        }
        private void Button_CS_Food_4_Click(object sender, RoutedEventArgs e)
        {
            CS_Recipe_4 = "";
            Image_CS_Food_4.Source = null;
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
            if (UI_gameversion.SelectedIndex == 2)
            {
                //便携式烹饪锅的四种食物
                if (ComboBox_CS_crockpot.SelectedIndex == 1)
                {
                    if (CS_FT_Fruit >= 2 && CS_FT_Butter >= 1 && CS_FT_Honey >= 1)
                        CS_CrockPotListAddFood("新鲜水果薄饼", 30);
                    if (CS_FT_MonsterFoods >= 2 && CS_FT_Eggs >= 1 && CS_FT_Vegetables >= 0.5)
                        CS_CrockPotListAddFood("怪物鞑靼", 30);
                    if (CS_FT_Mussel >= 2 && CS_FT_Vegetables >= 2)
                        CS_CrockPotListAddFood("贝类淡菜汤", 30);
                    if (CS_FT_SweetPotato >= 2 && CS_FT_Eggs >= 2)
                        CS_CrockPotListAddFood("薯蛋奶酥", 30);
                }
                if (CS_FT_Wobster >= 1 && CS_FT_Ice >= 1)
                    CS_CrockPotListAddFood("龙虾浓汤", 30);
                if (CS_FT_Limpets >= 3 && CS_FT_Ice >= 1)
                    CS_CrockPotListAddFood("汤", 30);
                if (CS_FT_RoastedCoffeeBeans >= 3 && (CS_FT_RoastedCoffeeBeans == 4 || CS_FT_Sweetener == 1 || CS_FT_DairyProduct == 1))
                    CS_CrockPotListAddFood("咖啡", 30);
                if (CS_FT_Meats >= 2.5 && CS_FT_Fishes >= 1.5 && CS_FT_Ice == 0)
                    CS_CrockPotListAddFood("海鲜牛排", 30);
                if (CS_FT_Wobster >= 1 && CS_FT_Butter >= 1 && CS_FT_Meats == 0 && CS_FT_Ice == 0)
                    CS_CrockPotListAddFood("龙虾正餐", 25);
                if (CS_FT_Banana >= 1 && CS_FT_Ice >= 1 && CS_FT_Twigs >= 1 && CS_FT_Meats == 0 && CS_FT_Fishes == 0)
                    CS_CrockPotListAddFood("香蕉冰淇淋", 20);
                if (CS_FT_Fishes >= 1 && CS_FT_Seaweed == 2)
                    CS_CrockPotListAddFood("加州卷", 20);
                if (CS_FT_Jellyfish >= 1 && CS_FT_Ice >= 1 && CS_FT_Twigs >= 1)
                    CS_CrockPotListAddFood("果冻冰淇淋", 20);
                if (CS_FT_Fishes >= 2 && CS_FT_Ice >= 1)
                    CS_CrockPotListAddFood("橘汁腌鱼", 20);
                if (CS_FT_SharkFin >= 1)
                    CS_CrockPotListAddFood("鱼翅汤", 20);
                if (CS_FT_Fishes >= 2.5)
                    CS_CrockPotListAddFood("海鲜汤", 10);
            }
            //------------------------其他------------------------
            if (CS_FT_RoyalJelly >= 1 && CS_FT_Twigs == 0 && CS_FT_MonsterFoods == 0)
                CS_CrockPotListAddFood("糖豆", 12);
            if (CS_FT_CactusFlesh >= 1 && CS_FT_Moleworm >= 1 && CS_FT_Fruit == 0)
                CS_CrockPotListAddFood("鼹鼠鳄梨酱", 10);
            if (CS_FT_CactusFlower >= 1 && CS_FT_Vegetables >= 2 && CS_FT_Fruit == 0 && CS_FT_Meats == 0 && CS_FT_Eggs == 0 && CS_FT_Sweetener == 0 && CS_FT_Twigs == 0)
                CS_CrockPotListAddFood("花瓣沙拉", 10);
            if (CS_FT_DairyProduct >= 1 && CS_FT_Ice >= 1 && CS_FT_Sweetener >= 1 && CS_FT_Meats == 0 && CS_FT_Eggs == 0 && CS_FT_Vegetables == 0 && CS_FT_Twigs == 0)
                CS_CrockPotListAddFood("冰淇淋", 10);
            if (CS_FT_Watermelon >= 1 && CS_FT_Ice >= 1 && CS_FT_Twigs >= 1 && CS_FT_Meats == 0 && CS_FT_Eggs == 0 && CS_FT_Vegetables == 0)
                CS_CrockPotListAddFood("西瓜冰", 10);
            if (CS_FT_RoastedBirchnut >= 1 && CS_FT_Berries >= 1 && CS_FT_Fruit >= 1 && CS_FT_Meats == 0 && CS_FT_Eggs == 0 && CS_FT_Vegetables == 0 && CS_FT_Sweetener == 0)
                CS_CrockPotListAddFood("水果杂烩", 10);
            if (CS_FT_Vegetables >= 1.5 && CS_FT_Meats >= 1.5)
                CS_CrockPotListAddFood("辣椒酱", 10);
            if (CS_FT_Eel >= 1 && CS_FT_Lichen >= 1)
                CS_CrockPotListAddFood("鳗鱼", 20);
            if (CS_FT_Pumpkin >= 1 && CS_FT_Sweetener >= 2)
                CS_CrockPotListAddFood("南瓜饼", 10);
            if (CS_FT_Corn >= 1 && CS_FT_Honey >= 1 && CS_FT_Twigs >= 1)
                CS_CrockPotListAddFood("芝士蛋糕", 10);
            if (CS_FT_Mandrake >= 1)
                CS_CrockPotListAddFood("曼德拉汤", 10);
            if (CS_FT_Fishes >= 0.5 && CS_FT_Twigs == 1)
                CS_CrockPotListAddFood("炸鱼条", 10);
            if (CS_FT_Fishes >= 0.5 && CS_FT_Corn >= 1)
                CS_CrockPotListAddFood("玉米饼包炸鱼", 10);
            if (CS_FT_Meats >= 1.5 && CS_FT_Eggs >= 2 && CS_FT_Vegetables == 0)
                CS_CrockPotListAddFood("培根煎蛋", 10);
            if (CS_FT_Drumstick >= 2 && CS_FT_Meats >= 1.5 && (CS_FT_Vegetables >= 0.5 || CS_FT_Fruit >= 0.5))
                CS_CrockPotListAddFood("火鸡正餐", 10);
            if (CS_FT_Sweetener >= 3 && CS_FT_Meats == 0)
                CS_CrockPotListAddFood("太妃糖", 10);
            if (CS_FT_Butter >= 1 && CS_FT_Eggs >= 1 && CS_FT_Berries >= 1)
                CS_CrockPotListAddFood("华夫饼", 10);
            if (CS_FT_MonsterFoods >= 2 && CS_FT_Twigs == 0)
                CS_CrockPotListAddFood("怪物千层饼", 10);
            if (CS_FT_Eggs >= 1 && CS_FT_Meats >= 0.5 && CS_FT_Vegetables >= 0.5 && CS_FT_Twigs == 0)
                CS_CrockPotListAddFood("饺子", 5);
            if (CS_FT_Meats >= 0.5 && CS_FT_Twigs == 1 && CS_FT_MonsterFoods <= 1)
                CS_CrockPotListAddFood("肉串", 5);
            if (CS_FT_Meats >= 2 && CS_FT_Honey >= 1 && CS_FT_Twigs == 0)
                CS_CrockPotListAddFood("蜜汁火腿", 2);
            if (CS_FT_Meats >= 0.5 && CS_FT_Meats < 2 && CS_FT_Honey >= 1 && CS_FT_Twigs == 0)
                CS_CrockPotListAddFood("甜蜜金砖", 2);
            if (CS_FT_Butterfly_wings >= 1 && CS_FT_Vegetables >= 0.5 && CS_FT_Meats == 0)
                CS_CrockPotListAddFood("奶油松饼", 1);
            if (CS_FT_FrogLegs >= 1 && CS_FT_Vegetables >= 0.5)
                CS_CrockPotListAddFood("青蛙圆面包三明治", 1);
            if (CS_FT_DragonFruit >= 1 && CS_FT_Meats == 0)
                CS_CrockPotListAddFood("火龙果派", 1);
            if (CS_FT_Eggplant >= 1 && CS_FT_Vegetables >= 0.5)
                CS_CrockPotListAddFood("香酥茄盒", 1);
            if (CS_FT_Vegetables >= 0.5 && CS_FT_Meats == 0 && CS_FT_Twigs == 0)
                CS_CrockPotListAddFood("蔬菜杂烩", 0);
            if (CS_FT_Fruit >= 0.5 && CS_FT_Meats == 0 && CS_FT_Vegetables == 0)
            {
                if (CS_FT_Fruit < 3)
                {
                    CS_CrockPotListAddFood("果酱蜜饯", 0);
                }
                else
                {
                    if (CS_FT_Twigs == 0)
                    {
                        CS_CrockPotListAddFood("果酱蜜饯", 0);
                        CS_CrockPotListAddFood("水果拼盘", 0);
                    }
                    else
                    {
                        CS_CrockPotListAddFood("水果拼盘", 0);
                    }
                }
            }
            if (CS_FT_Meats >= 3 && CS_FT_Twigs == 0)
            {
                CS_CrockPotListAddFood("肉汤", 0);
            }

            if (CS_FT_Meats >= 0.5 && CS_FT_Meats < 3 && CS_FT_Twigs == 0)
                CS_CrockPotListAddFood("肉丸", -1);
            #endregion
            #region 食物判断
            if (CrockPotListIndex == -1)
            {
                CS_F_name = "湿腻焦糊";
                CS_image_Food_Result_Source("F_wet_goop");
            }
            else
            {
                CS_F_name = CrockPotList[0];
                switch (CrockPotList[0])
                {
                    case "新鲜水果薄饼":
                        CS_image_Food_Result_Source("F_fresh_fruit_crepes");
                        break;
                    case "怪物鞑靼":
                        CS_image_Food_Result_Source("F_monster_tartare");
                        break;
                    case "贝类淡菜汤":
                        CS_image_Food_Result_Source("F_mussel_bouillabaise");
                        break;
                    case "薯蛋奶酥":
                        CS_image_Food_Result_Source("F_sweet_potato_souffle");
                        break;
                    case "龙虾浓汤":
                        CS_image_Food_Result_Source("F_lobster_bisque");
                        break;
                    case "汤":
                        CS_image_Food_Result_Source("F_bisque");
                        break;
                    case "咖啡":
                        CS_image_Food_Result_Source("F_coffee");
                        break;
                    case "海鲜牛排":
                        CS_image_Food_Result_Source("F_surf_'n'_turf");
                        break;
                    case "龙虾正餐":
                        CS_image_Food_Result_Source("F_lobster_dinner");
                        break;
                    case "香蕉冰淇淋":
                        CS_image_Food_Result_Source("F_banana_pop");
                        break;
                    case "加州卷":
                        CS_image_Food_Result_Source("F_california_roll");
                        break;
                    case "果冻冰淇淋":
                        CS_image_Food_Result_Source("F_jelly-O_pop");
                        break;
                    case "橘汁腌鱼":
                        CS_image_Food_Result_Source("F_ceviche");
                        break;
                    case "鱼翅汤":
                        CS_image_Food_Result_Source("F_shark_fin_soup");
                        break;
                    case "海鲜汤":
                        CS_image_Food_Result_Source("F_seafood_gumbo");
                        break;
                    case "鼹鼠鳄梨酱":
                        CS_image_Food_Result_Source("F_guacamole");
                        break;
                    case "花瓣沙拉":
                        CS_image_Food_Result_Source("F_flower_salad");
                        break;
                    case "冰淇淋":
                        CS_image_Food_Result_Source("F_ice_cream");
                        break;
                    case "西瓜冰":
                        CS_image_Food_Result_Source("F_melonsicle");
                        break;
                    case "水果杂烩":
                        CS_image_Food_Result_Source("F_trail_mix");
                        break;
                    case "辣椒酱":
                        CS_image_Food_Result_Source("F_spicy_chili");
                        break;
                    case "鳗鱼":
                        CS_image_Food_Result_Source("F_unagi");
                        break;
                    case "南瓜饼":
                        CS_image_Food_Result_Source("F_pumpkin_cookie");
                        break;
                    case "芝士蛋糕":
                        CS_image_Food_Result_Source("F_powdercake");
                        break;
                    case "曼德拉汤":
                        CS_image_Food_Result_Source("F_mandrake_soup");
                        break;
                    case "炸鱼条":
                        CS_image_Food_Result_Source("F_fishsticks");
                        break;
                    case "玉米饼包炸鱼":
                        CS_image_Food_Result_Source("F_fish_tacos");
                        break;
                    case "培根煎蛋":
                        CS_image_Food_Result_Source("F_bacon_and_eggs");
                        break;
                    case "火鸡正餐":
                        CS_image_Food_Result_Source("F_turkey_dinner");
                        break;
                    case "太妃糖":
                        CS_image_Food_Result_Source("F_taffy");
                        break;
                    case "华夫饼":
                        CS_image_Food_Result_Source("F_waffles");
                        break;
                    case "怪物千层饼":
                        CS_image_Food_Result_Source("F_monster_lasagna");
                        break;
                    case "饺子":
                        CS_image_Food_Result_Source("F_pierogi");
                        break;
                    case "肉串":
                        CS_image_Food_Result_Source("F_kabobs");
                        break;
                    case "蜜汁火腿":
                        CS_image_Food_Result_Source("F_honey_ham");
                        break;
                    case "甜蜜金砖":
                        CS_image_Food_Result_Source("F_honey_nuggets");
                        break;
                    case "奶油松饼":
                        CS_image_Food_Result_Source("F_butter_muffin");
                        break;
                    case "青蛙圆面包三明治":
                        CS_image_Food_Result_Source("F_froggle_bunwich");
                        break;
                    case "火龙果派":
                        CS_image_Food_Result_Source("F_dragonpie");
                        break;
                    case "香酥茄盒":
                        CS_image_Food_Result_Source("F_stuffed_eggplant");
                        break;
                    case "蔬菜杂烩":
                        CS_image_Food_Result_Source("F_ratatouille");
                        break;
                    case "果酱蜜饯":
                        CS_image_Food_Result_Source("F_fist_full_of_jam");
                        break;
                    case "水果拼盘":
                        CS_image_Food_Result_Source("F_fruit_medley");
                        break;
                    case "肉汤":
                        CS_image_Food_Result_Source("F_meaty_stew");
                        break;
                    case "肉丸":
                        CS_image_Food_Result_Source("F_meatballs");
                        break;
                }
            }
            #endregion
            #region 选择按钮显示判断
            if (CrockPotListIndex < 1)
            {
                button_CS_Switch_Left.Visibility = Visibility.Collapsed;
                button_CS_Switch_Right.Visibility = Visibility.Collapsed;
            }
            else
            {
                button_CS_Switch_Left.Visibility = Visibility.Visible;
                button_CS_Switch_Right.Visibility = Visibility.Visible;
                button_CS_Switch_Left.IsEnabled = false;
                button_CS_Switch_Right.IsEnabled = true;
            }
            #endregion
            //显示食物名称
            TextBlock_CS_FoodName.Text = CS_F_name;
            #region 自动清空材料
            if (checkBox_CS_AutoClean.IsChecked == true)
            {
                CS_Recipe_1 = "";
                CS_Recipe_2 = "";
                CS_Recipe_3 = "";
                CS_Recipe_4 = "";
                Image_CS_Food_1.Source = null;
                Image_CS_Food_2.Source = null;
                Image_CS_Food_3.Source = null;
                Image_CS_Food_4.Source = null;
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
            Image_CS_Food_Result.Source = RSN.PictureShortName(RSN.ShortName(source, CS_ResourceDir));
        }
        //左右切换按钮
        private void button_CS_Switch_Left_Click(object sender, RoutedEventArgs e)
        {
            button_CS_Switch_Right.IsEnabled = true;
            if (FoodIndex != 0)
            {
                FoodIndex -= 1;
                if (FoodIndex == 0)
                {
                    button_CS_Switch_Left.IsEnabled = false;
                }
                CS_F_name = CrockPotList[FoodIndex];
                switch (CrockPotList[FoodIndex])
                {
                    case "新鲜水果薄饼":
                        CS_image_Food_Result_Source("F_fresh_fruit_crepes");
                        break;
                    case "怪物鞑靼":
                        CS_image_Food_Result_Source("F_monster_tartare");
                        break;
                    case "贝类淡菜汤":
                        CS_image_Food_Result_Source("F_mussel_bouillabaise");
                        break;
                    case "薯蛋奶酥":
                        CS_image_Food_Result_Source("F_sweet_potato_souffle");
                        break;
                    case "龙虾浓汤":
                        CS_image_Food_Result_Source("F_lobster_bisque");
                        break;
                    case "汤":
                        CS_image_Food_Result_Source("F_bisque");
                        break;
                    case "咖啡":
                        CS_image_Food_Result_Source("F_coffee");
                        break;
                    case "海鲜牛排":
                        CS_image_Food_Result_Source("F_surf_'n'_turf");
                        break;
                    case "龙虾正餐":
                        CS_image_Food_Result_Source("F_lobster_dinner");
                        break;
                    case "香蕉冰淇淋":
                        CS_image_Food_Result_Source("F_banana_pop");
                        break;
                    case "加州卷":
                        CS_image_Food_Result_Source("F_california_roll");
                        break;
                    case "果冻冰淇淋":
                        CS_image_Food_Result_Source("F_jelly-O_pop");
                        break;
                    case "橘汁腌鱼":
                        CS_image_Food_Result_Source("F_ceviche");
                        break;
                    case "鱼翅汤":
                        CS_image_Food_Result_Source("F_shark_fin_soup");
                        break;
                    case "海鲜汤":
                        CS_image_Food_Result_Source("F_seafood_gumbo");
                        break;
                    case "糖豆":
                        CS_image_Food_Result_Source("F_jellybeans");
                        break;
                    case "鼹鼠鳄梨酱":
                        CS_image_Food_Result_Source("F_guacamole");
                        break;
                    case "花瓣沙拉":
                        CS_image_Food_Result_Source("F_flower_salad");
                        break;
                    case "冰淇淋":
                        CS_image_Food_Result_Source("F_ice_cream");
                        break;
                    case "西瓜冰":
                        CS_image_Food_Result_Source("F_melonsicle");
                        break;
                    case "水果杂烩":
                        CS_image_Food_Result_Source("F_trail_mix");
                        break;
                    case "辣椒酱":
                        CS_image_Food_Result_Source("F_spicy_chili");
                        break;
                    case "鳗鱼":
                        CS_image_Food_Result_Source("F_unagi");
                        break;
                    case "南瓜饼":
                        CS_image_Food_Result_Source("F_pumpkin_cookie");
                        break;
                    case "芝士蛋糕":
                        CS_image_Food_Result_Source("F_powdercake");
                        break;
                    case "曼德拉汤":
                        CS_image_Food_Result_Source("F_mandrake_soup");
                        break;
                    case "炸鱼条":
                        CS_image_Food_Result_Source("F_fishsticks");
                        break;
                    case "玉米饼包炸鱼":
                        CS_image_Food_Result_Source("F_fish_tacos");
                        break;
                    case "培根煎蛋":
                        CS_image_Food_Result_Source("F_bacon_and_eggs");
                        break;
                    case "火鸡正餐":
                        CS_image_Food_Result_Source("F_turkey_dinner");
                        break;
                    case "太妃糖":
                        CS_image_Food_Result_Source("F_taffy");
                        break;
                    case "华夫饼":
                        CS_image_Food_Result_Source("F_waffles");
                        break;
                    case "怪物千层饼":
                        CS_image_Food_Result_Source("F_monster_lasagna");
                        break;
                    case "饺子":
                        CS_image_Food_Result_Source("F_pierogi");
                        break;
                    case "肉串":
                        CS_image_Food_Result_Source("F_kabobs");
                        break;
                    case "蜜汁火腿":
                        CS_image_Food_Result_Source("F_honey_ham");
                        break;
                    case "甜蜜金砖":
                        CS_image_Food_Result_Source("F_honey_nuggets");
                        break;
                    case "奶油松饼":
                        CS_image_Food_Result_Source("F_butter_muffin");
                        break;
                    case "青蛙圆面包三明治":
                        CS_image_Food_Result_Source("F_froggle_bunwich");
                        break;
                    case "火龙果派":
                        CS_image_Food_Result_Source("F_dragonpie");
                        break;
                    case "香酥茄盒":
                        CS_image_Food_Result_Source("F_stuffed_eggplant");
                        break;
                    case "蔬菜杂烩":
                        CS_image_Food_Result_Source("F_ratatouille");
                        break;
                    case "果酱蜜饯":
                        CS_image_Food_Result_Source("F_fist_full_of_jam");
                        break;
                    case "水果拼盘":
                        CS_image_Food_Result_Source("F_fruit_medley");
                        break;
                    case "肉汤":
                        CS_image_Food_Result_Source("F_meaty_stew");
                        break;
                    case "肉丸":
                        CS_image_Food_Result_Source("F_meatballs");
                        break;
                }
            }
            TextBlock_CS_FoodName.Text = CS_F_name;
        }
        private void button_CS_Switch_Right_Click(object sender, RoutedEventArgs e)
        {
            button_CS_Switch_Left.IsEnabled = true;
            if (FoodIndex != CrockPotListIndex)
            {
                FoodIndex += 1;
                if (FoodIndex == CrockPotListIndex)
                {
                    button_CS_Switch_Right.IsEnabled = false;
                }
                CS_F_name = CrockPotList[FoodIndex];
                switch (CrockPotList[FoodIndex])
                {
                    case "新鲜水果薄饼":
                        CS_image_Food_Result_Source("F_fresh_fruit_crepes");
                        break;
                    case "怪物鞑靼":
                        CS_image_Food_Result_Source("F_monster_tartare");
                        break;
                    case "贝类淡菜汤":
                        CS_image_Food_Result_Source("F_mussel_bouillabaise");
                        break;
                    case "薯蛋奶酥":
                        CS_image_Food_Result_Source("F_sweet_potato_souffle");
                        break;
                    case "龙虾浓汤":
                        CS_image_Food_Result_Source("F_lobster_bisque");
                        break;
                    case "汤":
                        CS_image_Food_Result_Source("F_bisque");
                        break;
                    case "咖啡":
                        CS_image_Food_Result_Source("F_coffee");
                        break;
                    case "海鲜牛排":
                        CS_image_Food_Result_Source("F_surf_'n'_turf");
                        break;
                    case "龙虾正餐":
                        CS_image_Food_Result_Source("F_lobster_dinner");
                        break;
                    case "香蕉冰淇淋":
                        CS_image_Food_Result_Source("F_banana_pop");
                        break;
                    case "加州卷":
                        CS_image_Food_Result_Source("F_california_roll");
                        break;
                    case "果冻冰淇淋":
                        CS_image_Food_Result_Source("F_jelly-O_pop");
                        break;
                    case "橘汁腌鱼":
                        CS_image_Food_Result_Source("F_ceviche");
                        break;
                    case "鱼翅汤":
                        CS_image_Food_Result_Source("F_shark_fin_soup");
                        break;
                    case "海鲜汤":
                        CS_image_Food_Result_Source("F_seafood_gumbo");
                        break;
                    case "糖豆":
                        CS_image_Food_Result_Source("F_jellybeans");
                        break;
                    case "鼹鼠鳄梨酱":
                        CS_image_Food_Result_Source("F_guacamole");
                        break;
                    case "花瓣沙拉":
                        CS_image_Food_Result_Source("F_flower_salad");
                        break;
                    case "冰淇淋":
                        CS_image_Food_Result_Source("F_ice_cream");
                        break;
                    case "西瓜冰":
                        CS_image_Food_Result_Source("F_melonsicle");
                        break;
                    case "水果杂烩":
                        CS_image_Food_Result_Source("F_trail_mix");
                        break;
                    case "辣椒酱":
                        CS_image_Food_Result_Source("F_spicy_chili");
                        break;
                    case "鳗鱼":
                        CS_image_Food_Result_Source("F_unagi");
                        break;
                    case "南瓜饼":
                        CS_image_Food_Result_Source("F_pumpkin_cookie");
                        break;
                    case "芝士蛋糕":
                        CS_image_Food_Result_Source("F_powdercake");
                        break;
                    case "曼德拉汤":
                        CS_image_Food_Result_Source("F_mandrake_soup");
                        break;
                    case "炸鱼条":
                        CS_image_Food_Result_Source("F_fishsticks");
                        break;
                    case "玉米饼包炸鱼":
                        CS_image_Food_Result_Source("F_fish_tacos");
                        break;
                    case "培根煎蛋":
                        CS_image_Food_Result_Source("F_bacon_and_eggs");
                        break;
                    case "火鸡正餐":
                        CS_image_Food_Result_Source("F_turkey_dinner");
                        break;
                    case "太妃糖":
                        CS_image_Food_Result_Source("F_taffy");
                        break;
                    case "华夫饼":
                        CS_image_Food_Result_Source("F_waffles");
                        break;
                    case "怪物千层饼":
                        CS_image_Food_Result_Source("F_monster_lasagna");
                        break;
                    case "饺子":
                        CS_image_Food_Result_Source("F_pierogi");
                        break;
                    case "肉串":
                        CS_image_Food_Result_Source("F_kabobs");
                        break;
                    case "蜜汁火腿":
                        CS_image_Food_Result_Source("F_honey_ham");
                        break;
                    case "甜蜜金砖":
                        CS_image_Food_Result_Source("F_honey_nuggets");
                        break;
                    case "奶油松饼":
                        CS_image_Food_Result_Source("F_butter_muffin");
                        break;
                    case "青蛙圆面包三明治":
                        CS_image_Food_Result_Source("F_froggle_bunwich");
                        break;
                    case "火龙果派":
                        CS_image_Food_Result_Source("F_dragonpie");
                        break;
                    case "香酥茄盒":
                        CS_image_Food_Result_Source("F_stuffed_eggplant");
                        break;
                    case "蔬菜杂烩":
                        CS_image_Food_Result_Source("F_ratatouille");
                        break;
                    case "果酱蜜饯":
                        CS_image_Food_Result_Source("F_fist_full_of_jam");
                        break;
                    case "水果拼盘":
                        CS_image_Food_Result_Source("F_fruit_medley");
                        break;
                    case "肉汤":
                        CS_image_Food_Result_Source("F_meaty_stew");
                        break;
                    case "肉丸":
                        CS_image_Food_Result_Source("F_meatballs");
                        break;
                }
            }
            TextBlock_CS_FoodName.Text = CS_F_name;
        }

        //CookingSimulator面板Click事件(食材)
        private void CookingSimulator_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string[] BWTTag = (string[])button.Tag;//获取参数
            CookingSimulator_Click_Handle(BWTTag);
        }
        //WrapPanel_Left_Food控件创建事件(食材)
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
            int LeftCookingSimulatorWidth = (int)WrapPanel_Left_Cooking_Simulator.ActualWidth;
            foreach (UIElement uielement in WrapPanel_Left_Cooking_Simulator.Children)
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
            foreach (ExpanderStackpanel expanderStackpanel in WrapPanel_Right_Cooking_Simulator.Children)
            {
                expanderStackpanel.Width = (int)WrapPanel_Right_Cooking_Simulator.ActualWidth;
            }
        }
    }
}
