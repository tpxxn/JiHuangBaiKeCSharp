using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using Newtonsoft.Json;
using 饥荒百科全书CSharp.Class.JsonDeserialize;
using 饥荒百科全书CSharp.MyUserControl;
using Application = System.Windows.Application;
using Button = System.Windows.Controls.Button;
using Clipboard = System.Windows.Clipboard;
using RadioButton = System.Windows.Controls.RadioButton;

namespace 饥荒百科全书CSharp.Class
{
    public static class Global
    {
        public delegate void ConsoleSendKeyEventHandler(object sender, RoutedEventArgs e);

        public static ConsoleSendKeyEventHandler ConsoleSendKey = null;

        [ComVisible(true)]
        [Flags]
        [TypeConverter(typeof(KeysConverter))]
        public enum KeyModifiers
        {
            None = 0,
            Alt = 1,
            Ctrl = 2,
            Shift = 4,
            WindowsKey = 8
        }

        /// <summary>
        /// 应用程序文件夹
        /// </summary>
//        public static readonly StorageFolder ApplicationFolder = ApplicationData.Current.LocalFolder;

        /// <summary>
        /// 错误堆栈字符串
        /// </summary>
        public static string ErrorStackString { get; set; }

        /// <summary>
        /// 工程名 [饥荒百科全书CSharp]
        /// </summary>
        public static string ProjectName = Assembly.GetExecutingAssembly().GetName().Name;

        /// <summary>
        /// 测试模式
        /// </summary>
        public static bool TestMode { get; set; }

        /// <summary>
        /// MainPage需要保存在Global里额几个控件对象
        /// </summary>
        public static FontFamily FontFamily { get; set; }
        public static FontWeight FontWeight { get; set; }
        public static Grid RootGrid { get; set; }
        public static TextBlock FrameTitle { get; set; }
        public static Frame RootFrame { get; set; }
        public static Frame SettingRootFrame { get; set; }
        public static List<RadioButton> MainPageHamburgerButton { get; set; } = new List<RadioButton>();
        public static Frame RightFrame { get; set; }
        public static Frame CharacterLeftFrame { get; set; }
        public static Frame FoodLeftFrame { get; set; }
        public static Frame ScienceLeftFrame { get; set; }
        public static Frame CreatureLeftFrame { get; set; }
        public static Frame NaturalLeftFrame { get; set; }
        public static Frame GoodLeftFrame { get; set; }

        /// <summary>
        /// 透明Style
        /// </summary>
        public static readonly Style Transparent = (Style)Application.Current.Resources["TransparentDialog"];

        #region 颜色常量

        public static SolidColorBrush ColorGreen = new SolidColorBrush(Color.FromArgb(255, 94, 182, 96));     //绿色
        public static SolidColorBrush ColorKhaki = new SolidColorBrush(Color.FromArgb(255, 237, 182, 96));    //卡其布色/土黄色
        public static SolidColorBrush ColorRed = new SolidColorBrush(Color.FromArgb(255, 216, 82, 79));       //红色
        public static SolidColorBrush ColorBlue = new SolidColorBrush(Color.FromArgb(255, 51, 122, 184));     //蓝色
        public static SolidColorBrush ColorPurple = new SolidColorBrush(Color.FromArgb(255, 162, 133, 240));   //紫色
        public static SolidColorBrush ColorPink = new SolidColorBrush(Color.FromArgb(255, 240, 133, 211));     //粉色
        public static SolidColorBrush ColorCyan = new SolidColorBrush(Color.FromArgb(255, 21, 227, 234));     //青色
        public static SolidColorBrush ColorOrange = new SolidColorBrush(Color.FromArgb(255, 246, 166, 11));     //橙色
        public static SolidColorBrush ColorYellow = new SolidColorBrush(Color.FromArgb(255, 238, 232, 21));     //黄色
        public static SolidColorBrush ColorBorderCyan = new SolidColorBrush(Color.FromArgb(255, 178, 236, 237));     //天蓝色
        public static SolidColorBrush ColorGray = new SolidColorBrush(Color.FromArgb(255, 244, 244, 245));     //灰色

        #endregion

        #region 游戏版本

        /// <summary>
        /// 游戏版本
        /// </summary>
        public static double GameVersion { get; set; }

        /// <summary>
        /// 内置游戏版本Json文件夹名
        /// </summary>
        public static string[] BuiltInGameVersionJsonFolder =
        {
            "DST", "Tencent", "DS", "ROG", "Shipwrecked"
        };

        #endregion

        #region 方法
        /// <summary>
        /// 设置剪贴板
        /// </summary>
        /// <param name="text">文本</param>
        public static void SetClipboard(string text)
        {
            Clipboard.SetText(text);
            var copySplashWindow = new CopySplashScreen("已复制");
            copySplashWindow.InitializeComponent();
            copySplashWindow.Show();
        }

        /// <summary>
        /// 遍历视觉树
        /// </summary>
        /// <typeparam name="T">泛型T</typeparam>
        /// <param name="results">结果List</param>
        /// <param name="startNode">开始节点</param>
        public static void FindChildren<T>(List<T> results, DependencyObject startNode) where T : DependencyObject
        {
            var count = VisualTreeHelper.GetChildrenCount(startNode);
            for (var i = 0; i < count; i++)
            {
                var current = VisualTreeHelper.GetChild(startNode, i);
                if (current.GetType() == typeof(T) || current.GetType().GetTypeInfo().IsSubclassOf(typeof(T)) || current.GetType().IsInstanceOfType(typeof(T)))
                {
                    var asType = (T)current;
                    results.Add(asType);
                }
                FindChildren(results, current);
            }
        }
        
        /// <summary>
        /// Button转换为PicButton
        /// </summary>
        /// <param name="button">需转换的Button</param>
        /// <returns>转换完成的PicButton</returns>
        public static PicButton ButtonToPicButton(Button button)
        {
            return (PicButton)((StackPanel)button.Parent).Parent;
        }

        /// <summary>
        /// 页面跳转
        /// </summary>
        /// <param name="index">页面序号</param>
        public static void PageJump(int index)
        {
            foreach (var radioButton in MainPageHamburgerButton)
            {
                radioButton.IsChecked = false;
            }
            MainPageHamburgerButton[index].IsChecked = true;
        }

        /// <summary>
        /// 控件可视性设置
        /// </summary>
        /// <param name="visibility">visibility</param>
        /// <param name="obj">控件Name</param>
        public static void UiElementVisibility(Visibility visibility, params UIElement[] obj)
        {
            foreach (UIElement uiElement in obj)
            {
                uiElement.Visibility = visibility;
            }
        }

        /// <summary>
        /// 在对程序集的解析失败时发生(嵌入DLL)
        /// </summary>
        public static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            var dllName = new AssemblyName(args.Name).Name;
            try
            {
                //if (!dllName.Contains("PresentationFramework"))
                //{
                var resourceName = ProjectName + ".DynamicLinkLibrary." + dllName + ".dll";
                using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
                {
                    // ReSharper disable once PossibleNullReferenceException
                    var assemblyData = new byte[stream.Length];
                    stream.Read(assemblyData, 0, assemblyData.Length);
                    return Assembly.Load(assemblyData);
                }
                //}
            }
            catch (Exception e)
            {
                //if (dllName != "饥荒百科全书CSharp.resources")
                    //MessageBox.Show("DLL未能正确加载，详细信息：\r\n" + e.Message + "\r\n" + dllName);
                return null;
            }
        }
        #endregion

        #region 搜索
        /// <summary>
        /// 自动建议框Item集合
        /// </summary>
        public static ObservableCollection<SuggestBoxItem> AutoSuggestBoxItem { get; set; } = new ObservableCollection<SuggestBoxItem>();

        /// <summary>
        /// 自动建议框Item集合数据源
        /// </summary>
        public static List<SuggestBoxItem> AutoSuggestBoxItemSource { get; set; } = new List<SuggestBoxItem>();

        #region 自动搜索List
        public static readonly List<Character> CharacterData = new List<Character>();
        public static readonly List<FoodRecipe2> FoodRecipeData = new List<FoodRecipe2>();
        public static readonly List<Food> FoodMeatData = new List<Food>();
        public static readonly List<Food> FoodVegetableData = new List<Food>();
        public static readonly List<Food> FoodFruitData = new List<Food>();
        public static readonly List<Food> FoodEggData = new List<Food>();
        public static readonly List<Food> FoodOtherData = new List<Food>();
        public static readonly List<Food> FoodNoFcData = new List<Food>();
        public static readonly List<Science> ScienceToolData = new List<Science>();
        public static readonly List<Science> ScienceLightData = new List<Science>();
        public static readonly List<Science> ScienceNauticalData = new List<Science>();
        public static readonly List<Science> ScienceSurvivalData = new List<Science>();
        public static readonly List<Science> ScienceFoodData = new List<Science>();
        public static readonly List<Science> ScienceTechnologyData = new List<Science>();
        public static readonly List<Science> ScienceFightData = new List<Science>();
        public static readonly List<Science> ScienceStructureData = new List<Science>();
        public static readonly List<Science> ScienceRefineData = new List<Science>();
        public static readonly List<Science> ScienceMagicData = new List<Science>();
        public static readonly List<Science> ScienceDressData = new List<Science>();
        public static readonly List<Science> ScienceAncientData = new List<Science>();
        public static readonly List<Science> ScienceBookData = new List<Science>();
        public static readonly List<Science> ScienceShadowData = new List<Science>();
        public static readonly List<Science> ScienceCritterData = new List<Science>();
        public static readonly List<Science> ScienceSculptData = new List<Science>();
        public static readonly List<Science> ScienceCartographyData = new List<Science>();
        public static readonly List<Science> ScienceOfferingsData = new List<Science>();
        public static readonly List<Science> ScienceVolcanoData = new List<Science>();
        public static readonly List<Creature> CreatureLandData = new List<Creature>();
        public static readonly List<Creature> CreatureOceanData = new List<Creature>();
        public static readonly List<Creature> CreatureFlyData = new List<Creature>();
        public static readonly List<Creature> CreatureCaveData = new List<Creature>();
        public static readonly List<Creature> CreatureEvilData = new List<Creature>();
        public static readonly List<Creature> CreatureOthersData = new List<Creature>();
        public static readonly List<Creature> CreatureBossData = new List<Creature>();
        public static readonly List<NatureBiomes> NaturalBiomesData = new List<NatureBiomes>();
        public static readonly List<NatureSmallPlant> NaturalSmallPlantsData = new List<NatureSmallPlant>();
        public static readonly List<NatureTree> NaturalTreesData = new List<NatureTree>();
        public static readonly List<NatureCreatureNest> NaturalCreatureNestData = new List<NatureCreatureNest>();
        public static readonly List<GoodMaterial> GoodMaterialData = new List<GoodMaterial>();
        public static readonly List<GoodEquipment> GoodEquipmentData = new List<GoodEquipment>();
        public static readonly List<GoodSapling> GoodSaplingData = new List<GoodSapling>();
        public static readonly List<GoodCreatures> GoodCreaturesData = new List<GoodCreatures>();
        public static readonly List<Good> GoodTrinketsData = new List<Good>();
        public static readonly List<GoodTurf> GoodTurfData = new List<GoodTurf>();
        public static readonly List<GoodPet> GoodPetData = new List<GoodPet>();
        public static readonly List<GoodUnlock> GoodUnlockData = new List<GoodUnlock>();
        public static readonly List<Good> GoodHallowedNightsData = new List<Good>();
        public static readonly List<Good> GoodWintersFeastData = new List<Good>();
        public static readonly List<Good> GoodYearOfTheGobblerData = new List<Good>();
        public static readonly List<Good> GoodComponentData = new List<Good>();
        public static readonly List<Good> GoodOthersData = new List<Good>();
        #endregion

        /// <summary>
        /// 设置自动搜索框数据源
        /// </summary>
        public static void SetAutoSuggestBoxItem()
        {
            #region 清空列表
            AutoSuggestBoxItem.Clear();
            AutoSuggestBoxItemSource.Clear();
            CharacterData.Clear();
            FoodRecipeData.Clear();
            FoodMeatData.Clear();
            FoodVegetableData.Clear();
            FoodFruitData.Clear();
            FoodEggData.Clear();
            FoodOtherData.Clear();
            FoodNoFcData.Clear();
            ScienceToolData.Clear();
            ScienceLightData.Clear();
            ScienceNauticalData.Clear();
            ScienceSurvivalData.Clear();
            ScienceFoodData.Clear();
            ScienceTechnologyData.Clear();
            ScienceFightData.Clear();
            ScienceStructureData.Clear();
            ScienceRefineData.Clear();
            ScienceMagicData.Clear();
            ScienceDressData.Clear();
            ScienceAncientData.Clear();
            ScienceBookData.Clear();
            ScienceShadowData.Clear();
            ScienceCritterData.Clear();
            ScienceSculptData.Clear();
            ScienceCartographyData.Clear();
            ScienceOfferingsData.Clear();
            ScienceVolcanoData.Clear();
            CreatureLandData.Clear();
            CreatureOceanData.Clear();
            CreatureFlyData.Clear();
            CreatureCaveData.Clear();
            CreatureEvilData.Clear();
            CreatureOthersData.Clear();
            CreatureBossData.Clear();
            NaturalBiomesData.Clear();
            NaturalSmallPlantsData.Clear();
            NaturalTreesData.Clear();
            GoodMaterialData.Clear();
            GoodEquipmentData.Clear();
            GoodSaplingData.Clear();
            GoodCreaturesData.Clear();
            GoodTrinketsData.Clear();
            GoodTurfData.Clear();
            GoodPetData.Clear();
            GoodUnlockData.Clear();
            GoodHallowedNightsData.Clear();
            GoodWintersFeastData.Clear();
            GoodYearOfTheGobblerData.Clear();
            GoodComponentData.Clear();
            GoodOthersData.Clear();
            #endregion
            #region 人物
            var character = JsonConvert.DeserializeObject<CharacterRootObject>(StringProcess.GetJsonString("Characters.json"));
            foreach (var characterItems in character.Character)
            {
                CharacterData.Add(characterItems);
            }
            foreach (var characterItems in CharacterData)
            {
                characterItems.Picture = StringProcess.GetGameResourcePath(characterItems.Picture);
            }
            foreach (var characterItems in CharacterData)
            {
                AutoSuggestBoxItemSourceAdd(characterItems, "Character");
            }
            #endregion
            #region 食物
            var food = JsonConvert.DeserializeObject<FoodRootObject>(StringProcess.GetJsonString("Foods.json"));
            foreach (var foodRecipeItems in food.FoodRecipe.FoodRecipes)
            {
                FoodRecipeData.Add(foodRecipeItems);
            }
            foreach (var foodRecipeItems in FoodRecipeData)
            {
                foodRecipeItems.Picture = StringProcess.GetGameResourcePath(foodRecipeItems.Picture);
            }
            foreach (var foodMeatsItems in food.FoodMeats.Foods)
            {
                FoodMeatData.Add(foodMeatsItems);
            }
            foreach (var foodMeatsItems in FoodMeatData)
            {
                foodMeatsItems.Picture = StringProcess.GetGameResourcePath(foodMeatsItems.Picture);
            }
            foreach (var foodVegetablesItems in food.FoodVegetables.Foods)
            {
                FoodVegetableData.Add(foodVegetablesItems);
            }
            foreach (var foodVegetablesItems in FoodVegetableData)
            {
                foodVegetablesItems.Picture = StringProcess.GetGameResourcePath(foodVegetablesItems.Picture);
            }
            foreach (var foodFruitItems in food.FoodFruit.Foods)
            {
                FoodFruitData.Add(foodFruitItems);
            }
            foreach (var foodFruitItems in FoodFruitData)
            {
                foodFruitItems.Picture = StringProcess.GetGameResourcePath(foodFruitItems.Picture);
            }
            foreach (var foodEggsItems in food.FoodEggs.Foods)
            {
                FoodEggData.Add(foodEggsItems);
            }
            foreach (var foodEggsItems in FoodEggData)
            {
                foodEggsItems.Picture = StringProcess.GetGameResourcePath(foodEggsItems.Picture);
            }
            foreach (var foodOthersItems in food.FoodOthers.Foods)
            {
                FoodOtherData.Add(foodOthersItems);
            }
            foreach (var foodOthersItems in FoodOtherData)
            {
                foodOthersItems.Picture = StringProcess.GetGameResourcePath(foodOthersItems.Picture);
            }
            foreach (var foodNoFcItems in food.FoodNoFc.Foods)
            {
                FoodNoFcData.Add(foodNoFcItems);
            }
            foreach (var foodNoFcItems in FoodNoFcData)
            {
                foodNoFcItems.Picture = StringProcess.GetGameResourcePath(foodNoFcItems.Picture);
            }
            foreach (var foodRecipeItems in FoodRecipeData)
            {
                AutoSuggestBoxItemSourceAdd(foodRecipeItems, "FoodRecipe");
            }
            foreach (var foodItems in FoodMeatData)
            {
                AutoSuggestBoxItemSourceAdd(foodItems, "FoodMeats");
            }
            foreach (var foodItems in FoodVegetableData)
            {
                AutoSuggestBoxItemSourceAdd(foodItems, "FoodVegetables");
            }
            foreach (var foodItems in FoodFruitData)
            {
                AutoSuggestBoxItemSourceAdd(foodItems, "FoodFruits");
            }
            foreach (var foodItems in FoodEggData)
            {
                AutoSuggestBoxItemSourceAdd(foodItems, "FoodEggs");
            }
            foreach (var foodItems in FoodOtherData)
            {
                AutoSuggestBoxItemSourceAdd(foodItems, "FoodOthers");
            }
            foreach (var foodItems in FoodNoFcData)
            {
                AutoSuggestBoxItemSourceAdd(foodItems, "FoodNoFc");
            }
            #endregion
            #region 科技
            var science = JsonConvert.DeserializeObject<ScienceRootObject>(StringProcess.GetJsonString("Sciences.json"));
            foreach (var scienceToolItems in science.Tool.Science)
            {
                ScienceToolData.Add(scienceToolItems);
            }
            foreach (var scienceToolItems in ScienceToolData)
            {
                scienceToolItems.Picture = StringProcess.GetGameResourcePath(scienceToolItems.Picture);
            }
            foreach (var scienceLightItems in science.Light.Science)
            {
                ScienceLightData.Add(scienceLightItems);
            }
            foreach (var scienceLightItems in ScienceLightData)
            {
                scienceLightItems.Picture = StringProcess.GetGameResourcePath(scienceLightItems.Picture);
            }
            foreach (var scienceNauticalItems in science.Nautical.Science)
            {
                ScienceNauticalData.Add(scienceNauticalItems);
            }
            foreach (var scienceNauticalItems in ScienceNauticalData)
            {
                scienceNauticalItems.Picture = StringProcess.GetGameResourcePath(scienceNauticalItems.Picture);
            }
            foreach (var scienceSurvivalItems in science.Survival.Science)
            {
                ScienceSurvivalData.Add(scienceSurvivalItems);
            }
            foreach (var scienceSurvivalItems in ScienceSurvivalData)
            {
                scienceSurvivalItems.Picture = StringProcess.GetGameResourcePath(scienceSurvivalItems.Picture);
            }
            foreach (var scienceFoodItems in science.Foods.Science)
            {
                ScienceFoodData.Add(scienceFoodItems);
            }
            foreach (var scienceFoodItems in ScienceFoodData)
            {
                scienceFoodItems.Picture = StringProcess.GetGameResourcePath(scienceFoodItems.Picture);
            }
            foreach (var scienceTechnologyItems in science.Technology.Science)
            {
                ScienceTechnologyData.Add(scienceTechnologyItems);
            }
            foreach (var scienceTechnologyItems in ScienceTechnologyData)
            {
                scienceTechnologyItems.Picture = StringProcess.GetGameResourcePath(scienceTechnologyItems.Picture);
            }
            foreach (var scienceFightItems in science.Fight.Science)
            {
                ScienceFightData.Add(scienceFightItems);
            }
            foreach (var scienceFightItems in ScienceFightData)
            {
                scienceFightItems.Picture = StringProcess.GetGameResourcePath(scienceFightItems.Picture);
            }
            foreach (var scienceStructureItems in science.Structure.Science)
            {
                ScienceStructureData.Add(scienceStructureItems);
            }
            foreach (var scienceStructureItems in ScienceStructureData)
            {
                scienceStructureItems.Picture = StringProcess.GetGameResourcePath(scienceStructureItems.Picture);
            }
            foreach (var scienceRefineItems in science.Refine.Science)
            {
                ScienceRefineData.Add(scienceRefineItems);
            }
            foreach (var scienceRefineItems in ScienceRefineData)
            {
                scienceRefineItems.Picture = StringProcess.GetGameResourcePath(scienceRefineItems.Picture);
            }
            foreach (var scienceMagicItems in science.Magic.Science)
            {
                ScienceMagicData.Add(scienceMagicItems);
            }
            foreach (var scienceMagicItems in ScienceMagicData)
            {
                scienceMagicItems.Picture = StringProcess.GetGameResourcePath(scienceMagicItems.Picture);
            }
            foreach (var scienceDressItems in science.Dress.Science)
            {
                ScienceDressData.Add(scienceDressItems);
            }
            foreach (var scienceDressItems in ScienceDressData)
            {
                scienceDressItems.Picture = StringProcess.GetGameResourcePath(scienceDressItems.Picture);
            }
            foreach (var scienceAncientItems in science.Ancient.Science)
            {
                ScienceAncientData.Add(scienceAncientItems);
            }
            foreach (var scienceAncientItems in ScienceAncientData)
            {
                scienceAncientItems.Picture = StringProcess.GetGameResourcePath(scienceAncientItems.Picture);
            }
            foreach (var scienceBookItems in science.Book.Science)
            {
                ScienceBookData.Add(scienceBookItems);
            }
            foreach (var scienceBookItems in ScienceBookData)
            {
                scienceBookItems.Picture = StringProcess.GetGameResourcePath(scienceBookItems.Picture);
            }
            foreach (var scienceShadowItems in science.Shadow.Science)
            {
                ScienceShadowData.Add(scienceShadowItems);
            }
            foreach (var scienceShadowItems in ScienceShadowData)
            {
                scienceShadowItems.Picture = StringProcess.GetGameResourcePath(scienceShadowItems.Picture);
            }
            foreach (var scienceCritterItems in science.Critter.Science)
            {
                ScienceCritterData.Add(scienceCritterItems);
            }
            foreach (var scienceCritterItems in ScienceCritterData)
            {
                scienceCritterItems.Picture = StringProcess.GetGameResourcePath(scienceCritterItems.Picture);
            }
            foreach (var scienceSculptItems in science.Sculpt.Science)
            {
                ScienceSculptData.Add(scienceSculptItems);
            }
            foreach (var scienceSculptItems in ScienceSculptData)
            {
                scienceSculptItems.Picture = StringProcess.GetGameResourcePath(scienceSculptItems.Picture);
            }
            foreach (var scienceCartographyItems in science.Cartography.Science)
            {
                ScienceCartographyData.Add(scienceCartographyItems);
            }
            foreach (var scienceCartographyItems in ScienceCartographyData)
            {
                scienceCartographyItems.Picture = StringProcess.GetGameResourcePath(scienceCartographyItems.Picture);
            }
            foreach (var scienceOfferingsItems in science.Offerings.Science)
            {
                ScienceOfferingsData.Add(scienceOfferingsItems);
            }
            foreach (var scienceOfferingsItems in ScienceOfferingsData)
            {
                scienceOfferingsItems.Picture = StringProcess.GetGameResourcePath(scienceOfferingsItems.Picture);
            }
            foreach (var scienceVolcanoItems in science.Volcano.Science)
            {
                ScienceVolcanoData.Add(scienceVolcanoItems);
            }
            foreach (var scienceVolcanoItems in ScienceVolcanoData)
            {
                scienceVolcanoItems.Picture = StringProcess.GetGameResourcePath(scienceVolcanoItems.Picture);
            }
            foreach (var scienceItems in ScienceToolData)
            {
                AutoSuggestBoxItemSourceAdd(scienceItems, "ScienceTool");
            }
            foreach (var scienceItems in ScienceLightData)
            {
                AutoSuggestBoxItemSourceAdd(scienceItems, "ScienceLight");
            }
            foreach (var scienceItems in ScienceNauticalData)
            {
                AutoSuggestBoxItemSourceAdd(scienceItems, "ScienceNautical");
            }
            foreach (var scienceItems in ScienceSurvivalData)
            {
                AutoSuggestBoxItemSourceAdd(scienceItems, "ScienceSurvival");
            }
            foreach (var scienceItems in ScienceFoodData)
            {
                AutoSuggestBoxItemSourceAdd(scienceItems, "ScienceFood");
            }
            foreach (var scienceItems in ScienceTechnologyData)
            {
                AutoSuggestBoxItemSourceAdd(scienceItems, "ScienceTechnology");
            }
            foreach (var scienceItems in ScienceFightData)
            {
                AutoSuggestBoxItemSourceAdd(scienceItems, "ScienceFight");
            }
            foreach (var scienceItems in ScienceStructureData)
            {
                AutoSuggestBoxItemSourceAdd(scienceItems, "ScienceStructure");
            }
            foreach (var scienceItems in ScienceRefineData)
            {
                AutoSuggestBoxItemSourceAdd(scienceItems, "ScienceRefine");
            }
            foreach (var scienceItems in ScienceMagicData)
            {
                AutoSuggestBoxItemSourceAdd(scienceItems, "ScienceMagic");
            }
            foreach (var scienceItems in ScienceDressData)
            {
                AutoSuggestBoxItemSourceAdd(scienceItems, "ScienceDress");
            }
            foreach (var scienceItems in ScienceAncientData)
            {
                AutoSuggestBoxItemSourceAdd(scienceItems, "ScienceAncient");
            }
            foreach (var scienceItems in ScienceBookData)
            {
                AutoSuggestBoxItemSourceAdd(scienceItems, "ScienceBook");
            }
            foreach (var scienceItems in ScienceShadowData)
            {
                AutoSuggestBoxItemSourceAdd(scienceItems, "ScienceShadow");
            }
            foreach (var scienceItems in ScienceCritterData)
            {
                AutoSuggestBoxItemSourceAdd(scienceItems, "ScienceCritter");
            }
            foreach (var scienceItems in ScienceSculptData)
            {
                AutoSuggestBoxItemSourceAdd(scienceItems, "ScienceSculpt");
            }
            foreach (var scienceItems in ScienceCartographyData)
            {
                AutoSuggestBoxItemSourceAdd(scienceItems, "ScienceCartography");
            }
            foreach (var scienceItems in ScienceOfferingsData)
            {
                AutoSuggestBoxItemSourceAdd(scienceItems, "ScienceOfferings");
            }
            foreach (var scienceItems in ScienceVolcanoData)
            {
                AutoSuggestBoxItemSourceAdd(scienceItems, "ScienceVolcano");
            }
            #endregion
            #region 生物
            var creature = JsonConvert.DeserializeObject<CreaturesRootObject>(StringProcess.GetJsonString("Creatures.json"));
            foreach (var creatureLandItems in creature.Land.Creature)
            {
                CreatureLandData.Add(creatureLandItems);
            }
            foreach (var creatureLandItems in CreatureLandData)
            {
                creatureLandItems.Picture = StringProcess.GetGameResourcePath(creatureLandItems.Picture);
            }
            foreach (var creatureOceanItems in creature.Ocean.Creature)
            {
                CreatureOceanData.Add(creatureOceanItems);
            }
            foreach (var creatureOceanItems in CreatureOceanData)
            {
                creatureOceanItems.Picture = StringProcess.GetGameResourcePath(creatureOceanItems.Picture);
            }
            foreach (var creatureFlyItems in creature.Fly.Creature)
            {
                CreatureFlyData.Add(creatureFlyItems);
            }
            foreach (var creatureFlyItems in CreatureFlyData)
            {
                creatureFlyItems.Picture = StringProcess.GetGameResourcePath(creatureFlyItems.Picture);
            }
            foreach (var creatureCaveItems in creature.Cave.Creature)
            {
                CreatureCaveData.Add(creatureCaveItems);
            }
            foreach (var creatureCaveItems in CreatureCaveData)
            {
                creatureCaveItems.Picture = StringProcess.GetGameResourcePath(creatureCaveItems.Picture);
            }
            foreach (var creatureEvilItems in creature.Evil.Creature)
            {
                CreatureEvilData.Add(creatureEvilItems);
            }
            foreach (var creatureEvilItems in CreatureEvilData)
            {
                creatureEvilItems.Picture = StringProcess.GetGameResourcePath(creatureEvilItems.Picture);
            }
            foreach (var creatureOthersItems in creature.Others.Creature)
            {
                CreatureOthersData.Add(creatureOthersItems);
            }
            foreach (var creatureOthersItems in CreatureOthersData)
            {
                creatureOthersItems.Picture = StringProcess.GetGameResourcePath(creatureOthersItems.Picture);
            }
            foreach (var creatureBossItems in creature.Boss.Creature)
            {
                CreatureBossData.Add(creatureBossItems);
            }
            foreach (var creatureBossItems in CreatureBossData)
            {
                creatureBossItems.Picture = StringProcess.GetGameResourcePath(creatureBossItems.Picture);
            }
            foreach (var creatureItems in CreatureLandData)
            {
                AutoSuggestBoxItemSourceAdd(creatureItems, "CreatureLand");
            }
            foreach (var creatureItems in CreatureOceanData)
            {
                AutoSuggestBoxItemSourceAdd(creatureItems, "CreatureOcean");
            }
            foreach (var creatureItems in CreatureFlyData)
            {
                AutoSuggestBoxItemSourceAdd(creatureItems, "CreatureFly");
            }
            foreach (var creatureItems in CreatureCaveData)
            {
                AutoSuggestBoxItemSourceAdd(creatureItems, "CreatureCave");
            }
            foreach (var creatureItems in CreatureEvilData)
            {
                AutoSuggestBoxItemSourceAdd(creatureItems, "CreatureEvil");
            }
            foreach (var creatureItems in CreatureOthersData)
            {
                AutoSuggestBoxItemSourceAdd(creatureItems, "CreatureOther");
            }
            foreach (var creatureItems in CreatureBossData)
            {
                AutoSuggestBoxItemSourceAdd(creatureItems, "CreatureBoss");
            }
            #endregion
            #region 自然
            var natural = JsonConvert.DeserializeObject<NaturalRootObject>(StringProcess.GetJsonString("Natural.json"));
            foreach (var naturalBiomesItems in natural.Biomes.NatureBiomes)
            {
                NaturalBiomesData.Add(naturalBiomesItems);
            }
            foreach (var naturalBiomesItems in NaturalBiomesData)
            {
                naturalBiomesItems.Picture = StringProcess.GetGameResourcePath(naturalBiomesItems.Picture);
            }
            foreach (var naturalSmallPlantItems in natural.SmallPlants.NatureSmallPlant)
            {
                NaturalSmallPlantsData.Add(naturalSmallPlantItems);
            }
            foreach (var naturalSmallPlantItems in NaturalSmallPlantsData)
            {
                naturalSmallPlantItems.Picture = StringProcess.GetGameResourcePath(naturalSmallPlantItems.Picture);
            }
            foreach (var naturalTreeItems in natural.Trees.NatureTree)
            {
                NaturalTreesData.Add(naturalTreeItems);
            }
            foreach (var naturalTreeItems in NaturalTreesData)
            {
                naturalTreeItems.Picture = StringProcess.GetGameResourcePath(naturalTreeItems.Picture);
            }
            foreach (var naturalCreatureNestItems in natural.CreatureNests.NatureCreatureNest)
            {
                NaturalCreatureNestData.Add(naturalCreatureNestItems);
            }
            foreach (var naturalCreatureNestItems in NaturalCreatureNestData)
            {
                naturalCreatureNestItems.Picture = StringProcess.GetGameResourcePath(naturalCreatureNestItems.Picture);
            }
            foreach (var naturalItems in NaturalBiomesData)
            {
                AutoSuggestBoxItemSourceAdd(naturalItems, "NaturalBiomes");
            }
            foreach (var naturalItems in NaturalSmallPlantsData)
            {
                AutoSuggestBoxItemSourceAdd(naturalItems, "NaturalSmallPlants");
            }
            foreach (var naturalItems in NaturalTreesData)
            {
                AutoSuggestBoxItemSourceAdd(naturalItems, "NaturalTrees");
            }
            foreach (var naturalItems in NaturalCreatureNestData)
            {
                AutoSuggestBoxItemSourceAdd(naturalItems, "NaturalCreatureNests");
            }
            #endregion
            #region 物品
            var good = JsonConvert.DeserializeObject<GoodsRootObject>(StringProcess.GetJsonString("Goods.json"));
            foreach (var goodMaterialItems in good.Material.GoodMaterial)
            {
                GoodMaterialData.Add(goodMaterialItems);
            }
            foreach (var goodMaterialItems in GoodMaterialData)
            {
                goodMaterialItems.Picture = StringProcess.GetGameResourcePath(goodMaterialItems.Picture);
            }
            foreach (var goodEquipmentItems in good.Equipment.GoodEquipment)
            {
                GoodEquipmentData.Add(goodEquipmentItems);
            }
            foreach (var goodEquipmentItems in GoodEquipmentData)
            {
                goodEquipmentItems.Picture = StringProcess.GetGameResourcePath(goodEquipmentItems.Picture);
            }
            foreach (var goodSaplingItems in good.Sapling.GoodSapling)
            {
                GoodSaplingData.Add(goodSaplingItems);
            }
            foreach (var goodSaplingItems in GoodSaplingData)
            {
                goodSaplingItems.Picture = StringProcess.GetGameResourcePath(goodSaplingItems.Picture);
            }
            foreach (var goodCreaturesItems in good.Creatures.GoodCreatures)
            {
                GoodCreaturesData.Add(goodCreaturesItems);
            }
            foreach (var goodCreaturesItems in GoodCreaturesData)
            {
                goodCreaturesItems.Picture = StringProcess.GetGameResourcePath(goodCreaturesItems.Picture);
            }
            foreach (var goodTrinketsItems in good.Trinkets.GoodTrinkets)
            {
                GoodTrinketsData.Add(goodTrinketsItems);
            }
            foreach (var goodTrinketsItems in GoodTrinketsData)
            {
                goodTrinketsItems.Picture = StringProcess.GetGameResourcePath(goodTrinketsItems.Picture);
            }
            foreach (var goodTurfItems in good.Turf.GoodTurf)
            {
                GoodTurfData.Add(goodTurfItems);
            }
            foreach (var goodTurfItems in GoodTurfData)
            {
                goodTurfItems.Picture = StringProcess.GetGameResourcePath(goodTurfItems.Picture);
            }
            foreach (var goodPetItems in good.Pet.GoodPet)
            {
                GoodPetData.Add(goodPetItems);
            }
            foreach (var goodPetItems in GoodPetData)
            {
                goodPetItems.Picture = StringProcess.GetGameResourcePath(goodPetItems.Picture);
            }
            foreach (var goodUnlockItems in good.Unlock.GoodUnlock)
            {
                GoodUnlockData.Add(goodUnlockItems);
            }
            foreach (var goodUnlockItems in GoodUnlockData)
            {
                goodUnlockItems.Picture = StringProcess.GetGameResourcePath(goodUnlockItems.Picture);
            }
            foreach (var goodHallowedNightsItems in good.HallowedNights.Good)
            {
                GoodHallowedNightsData.Add(goodHallowedNightsItems);
            }
            foreach (var goodHallowedNightsItems in GoodHallowedNightsData)
            {
                goodHallowedNightsItems.Picture = StringProcess.GetGameResourcePath(goodHallowedNightsItems.Picture);
            }
            foreach (var goodWinterwsFeastItems in good.WintersFeast.Good)
            {
                GoodWintersFeastData.Add(goodWinterwsFeastItems);
            }
            foreach (var goodWinterwsFeastItems in GoodWintersFeastData)
            {
                goodWinterwsFeastItems.Picture = StringProcess.GetGameResourcePath(goodWinterwsFeastItems.Picture);
            }
            foreach (var goodYearOfTheGobblerItems in good.YearOfTheGobbler.Good)
            {
                GoodYearOfTheGobblerData.Add(goodYearOfTheGobblerItems);
            }
            foreach (var goodYearOfTheGobblerItems in GoodYearOfTheGobblerData)
            {
                goodYearOfTheGobblerItems.Picture = StringProcess.GetGameResourcePath(goodYearOfTheGobblerItems.Picture);
            }
            foreach (var goodComponentItems in good.Component.Good)
            {
                GoodComponentData.Add(goodComponentItems);
            }
            foreach (var goodComponentItems in GoodComponentData)
            {
                goodComponentItems.Picture = StringProcess.GetGameResourcePath(goodComponentItems.Picture);
            }
            foreach (var goodOthersItems in good.GoodOthers.Good)
            {
                GoodOthersData.Add(goodOthersItems);
            }
            foreach (var goodOthersItems in GoodOthersData)
            {
                goodOthersItems.Picture = StringProcess.GetGameResourcePath(goodOthersItems.Picture);
            }
            foreach (var goodMaterialItems in GoodMaterialData)
            {
                AutoSuggestBoxItemSourceAdd(goodMaterialItems, "GoodMaterial");
            }
            foreach (var goodEquipmentItems in GoodEquipmentData)
            {
                AutoSuggestBoxItemSourceAdd(goodEquipmentItems, "GoodEquipment");
            }
            foreach (var goodSaplingItems in GoodSaplingData)
            {
                AutoSuggestBoxItemSourceAdd(goodSaplingItems, "GoodSapling");
            }
            foreach (var goodCreaturesItems in GoodCreaturesData)
            {
                AutoSuggestBoxItemSourceAdd(goodCreaturesItems, "GoodCreatures");
            }
            foreach (var goodTrinketsItems in GoodTrinketsData)
            {
                AutoSuggestBoxItemSourceAdd(goodTrinketsItems, "GoodTrinkets");
            }
            foreach (var goodTurfItems in GoodTurfData)
            {
                AutoSuggestBoxItemSourceAdd(goodTurfItems, "GoodTurf");
            }
            foreach (var goodPetItems in GoodPetData)
            {
                AutoSuggestBoxItemSourceAdd(goodPetItems, "GoodPet");
            }
            foreach (var goodUnlockItems in GoodUnlockData)
            {
                AutoSuggestBoxItemSourceAdd(goodUnlockItems, "GoodUnlock");
            }
            foreach (var goodHallowedNightsItems in GoodHallowedNightsData)
            {
                AutoSuggestBoxItemSourceAdd(goodHallowedNightsItems, "GoodHallowedNights");
            }
            foreach (var goodWintersFeastItems in GoodWintersFeastData)
            {
                AutoSuggestBoxItemSourceAdd(goodWintersFeastItems, "GoodWintersFeast");
            }
            foreach (var goodYearOfTheGobblerItems in GoodYearOfTheGobblerData)
            {
                AutoSuggestBoxItemSourceAdd(goodYearOfTheGobblerItems, "GoodYearOfTheGobbler");
            }
            foreach (var goodComponentItems in GoodComponentData)
            {
                AutoSuggestBoxItemSourceAdd(goodComponentItems, "GoodComponent");
            }
            foreach (var goodOthersItems in GoodOthersData)
            {
                AutoSuggestBoxItemSourceAdd(goodOthersItems, "GoodOthers");
            }
            #endregion
            #region 把AutoSuggestBoxItemSource数据源加入到AutoSuggestBoxItem
            foreach (var item in AutoSuggestBoxItemSource)
            {
                AutoSuggestBoxItem.Add(item);
            }
            #endregion
        }

        /// <summary>
        /// 自动搜索框数据添加
        /// </summary>
        /// <param name="obj">Items对象</param>
        /// <param name="sourcePath">源路径</param>
        public static void AutoSuggestBoxItemSourceAdd(object obj, string sourcePath)
        {
            var suggestBoxItem = new SuggestBoxItem();
            var type = obj.GetType();
            if (type == typeof(Character))
            {
                suggestBoxItem.Picture = ((Character)obj).Picture;
                suggestBoxItem.Name = ((Character)obj).Name;
                suggestBoxItem.EnName = ((Character)obj).EnName;
                suggestBoxItem.Category = "人物";
            }
            else if (type == typeof(FoodRecipe2))
            {
                suggestBoxItem.Picture = ((FoodRecipe2)obj).Picture;
                suggestBoxItem.Name = ((FoodRecipe2)obj).Name;
                suggestBoxItem.EnName = ((FoodRecipe2)obj).EnName;
                suggestBoxItem.Category = "食物";
            }
            else if (type == typeof(Food))
            {
                suggestBoxItem.Picture = ((Food)obj).Picture;
                suggestBoxItem.Name = ((Food)obj).Name;
                suggestBoxItem.EnName = ((Food)obj).EnName;
                suggestBoxItem.Category = "食物";
            }
            else if (type == typeof(Science))
            {
                suggestBoxItem.Picture = ((Science)obj).Picture;
                suggestBoxItem.Name = ((Science)obj).Name;
                suggestBoxItem.EnName = ((Science)obj).EnName;
                suggestBoxItem.Category = "科技";
            }
            else if (type == typeof(Creature))
            {
                suggestBoxItem.Picture = ((Creature)obj).Picture;
                suggestBoxItem.Name = ((Creature)obj).Name;
                suggestBoxItem.EnName = ((Creature)obj).EnName;
                suggestBoxItem.Category = "生物";
            }
            else if (type == typeof(NatureBiomes))
            {
                suggestBoxItem.Picture = ((NatureBiomes)obj).Picture;
                suggestBoxItem.Name = ((NatureBiomes)obj).Name;
                suggestBoxItem.EnName = ((NatureBiomes)obj).EnName;
                suggestBoxItem.Category = "自然";
            }
            else if (type == typeof(NatureSmallPlant))
            {
                suggestBoxItem.Picture = ((NatureSmallPlant)obj).Picture;
                suggestBoxItem.Name = ((NatureSmallPlant)obj).Name;
                suggestBoxItem.EnName = ((NatureSmallPlant)obj).EnName;
                suggestBoxItem.Category = "自然";
            }
            else if (type == typeof(NatureTree))
            {
                suggestBoxItem.Picture = ((NatureTree)obj).Picture;
                suggestBoxItem.Name = ((NatureTree)obj).Name;
                suggestBoxItem.EnName = ((NatureTree)obj).EnName;
                suggestBoxItem.Category = "自然";
            }
            else if (type == typeof(NatureCreatureNest))
            {
                suggestBoxItem.Picture = ((NatureCreatureNest)obj).Picture;
                suggestBoxItem.Name = ((NatureCreatureNest)obj).Name;
                suggestBoxItem.EnName = ((NatureCreatureNest)obj).EnName;
                suggestBoxItem.Category = "自然";
            }
            else if (type == typeof(GoodMaterial))
            {
                suggestBoxItem.Picture = ((GoodMaterial)obj).Picture;
                suggestBoxItem.Name = ((GoodMaterial)obj).Name;
                suggestBoxItem.EnName = ((GoodMaterial)obj).EnName;
                suggestBoxItem.Category = "物品";
            }
            else if (type == typeof(GoodEquipment))
            {
                suggestBoxItem.Picture = ((GoodEquipment)obj).Picture;
                suggestBoxItem.Name = ((GoodEquipment)obj).Name;
                suggestBoxItem.EnName = ((GoodEquipment)obj).EnName;
                suggestBoxItem.Category = "物品";
            }
            else if (type == typeof(GoodSapling))
            {
                suggestBoxItem.Picture = ((GoodSapling)obj).Picture;
                suggestBoxItem.Name = ((GoodSapling)obj).Name;
                suggestBoxItem.EnName = ((GoodSapling)obj).EnName;
                suggestBoxItem.Category = "物品";
            }
            else if (type == typeof(GoodCreatures))
            {
                suggestBoxItem.Picture = ((GoodCreatures)obj).Picture;
                suggestBoxItem.Name = ((GoodCreatures)obj).Name;
                suggestBoxItem.EnName = ((GoodCreatures)obj).EnName;
                suggestBoxItem.Category = "物品";
            }
            else if (type == typeof(GoodTurf))
            {
                suggestBoxItem.Picture = ((GoodTurf)obj).Picture;
                suggestBoxItem.Name = ((GoodTurf)obj).Name;
                suggestBoxItem.EnName = ((GoodTurf)obj).EnName;
                suggestBoxItem.Category = "物品";
            }
            else if (type == typeof(GoodPet))
            {
                suggestBoxItem.Picture = ((GoodPet)obj).Picture;
                suggestBoxItem.Name = ((GoodPet)obj).Name;
                suggestBoxItem.EnName = ((GoodPet)obj).EnName;
                suggestBoxItem.Category = "物品";
            }
            else if (type == typeof(GoodUnlock))
            {
                suggestBoxItem.Picture = ((GoodUnlock)obj).Picture;
                suggestBoxItem.Name = ((GoodUnlock)obj).Name;
                suggestBoxItem.EnName = ((GoodUnlock)obj).EnName;
                suggestBoxItem.Category = "物品";
            }
            else if (type == typeof(Good))
            {
                suggestBoxItem.Picture = ((Good)obj).Picture;
                suggestBoxItem.Name = ((Good)obj).Name;
                suggestBoxItem.EnName = ((Good)obj).EnName;
                suggestBoxItem.Category = "物品";
            }
            suggestBoxItem.SourcePath = sourcePath;
            AutoSuggestBoxItemSource.Add(suggestBoxItem);
        }

        #endregion
    }
}
