using System;
using System.Collections.Generic;
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
using 饥荒百科全书CSharp.Class;
using 饥荒百科全书CSharp.MyUserControl;

namespace 饥荒百科全书CSharp.View.Details
{
    /// <summary>
    /// CreatureDetail.xaml 的交互逻辑
    /// </summary>
    public partial class CreatureDetail : Page
    {
        private int _loadedTime;

        public void LoadCompleted(object sender, NavigationEventArgs e)
        {
            if (e.ExtraData == null || _loadedTime != 0) return;
            _loadedTime++;
            LoadData((Creature)e.ExtraData);
            if (Global.FontFamily != null)
            {
                FontFamily = Global.FontFamily;
            }
        }

        public CreatureDetail()
        {
            InitializeComponent();
            Global.CreatureLeftFrame.NavigationService.LoadCompleted += LoadCompleted;
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        private void LoadData(Creature c)
        {
            CreatureImage.Source = new BitmapImage(new Uri(c.Picture, UriKind.Relative));
            CreatureName.Text = c.Name;
            CreatureEnName.Text = c.EnName;
            if (c.Health != 0)
            {
                CreatureHealth.Value = c.Health;
                CreatureHealth.BarColor = Global.ColorGreen;
            }
            else
            {
                CreatureHealth.Visibility = Visibility.Collapsed;
            }
            if (c.Attack != 0)
            {
                CreatureAttack.Value = c.Attack;
                CreatureAttack.BarColor = Global.ColorRed;
            }
            else
            {
                CreatureAttack.Visibility = Visibility.Collapsed;
            }
            if (c.AttackInterval != 0)
            {
                CreatureAttackInterval.Value = c.AttackInterval;
                CreatureAttackInterval.BarColor = Global.ColorBlue;
            }
            else
            {
                CreatureAttackInterval.Visibility = Visibility.Collapsed;
            }
            if (c.AttackScope != 0)
            {
                CreatureAttackScope.Value = c.AttackScope;
                CreatureAttackScope.BarColor = Global.ColorPurple;
            }
            else
            {
                CreatureAttackScope.Visibility = Visibility.Collapsed;
            }
            if (c.MoveSpeed != 0)
            {
                CreatureMoveSpeed.Value = c.MoveSpeed;
                CreatureMoveSpeed.BarColor = Global.ColorPink;
            }
            else
            {
                CreatureMoveSpeed.Visibility = Visibility.Collapsed;
            }
            if (c.RunSpeed != 0)
            {
                CreatureRunSpeed.Value = c.RunSpeed;
                CreatureRunSpeed.BarColor = Global.ColorCyan;
            }
            else
            {
                CreatureRunSpeed.Visibility = Visibility.Collapsed;
            }
            if (c.Dangerous != 0)
            {
                CreatureDangerous.Value = c.Dangerous;
                CreatureDangerous.BarColor = Global.ColorYellow;
            }
            else
            {
                CreatureDangerous.Visibility = Visibility.Collapsed;
            }
            if (c.SanityEffect != 0)
            {
                CreatureSanityEffect.Value = c.SanityEffect;
                CreatureSanityEffect.BarColor = Global.ColorKhaki;
            }
            else
            {
                CreatureSanityEffect.Visibility = Visibility.Collapsed;
            }
            // 主动攻击、团队合作
            if (c.EnName == "Pig King" || c.EnName == "Yaarctopus")
            {
                ActiveAttackAndTeamWorkStackPanel.Visibility = Visibility.Collapsed;
            }
            if (c.IsActiveAttack)
            {
                ActiveAttackCheckBox.IsChecked = true;
            }
            if (c.IsTeamWork)
            {
                TeamWorkCheckBox.IsChecked = true;
            }
            // 战利品
            if (c.Goods.Count == 0)
            {
                GoodsTextBlock.Visibility = Visibility.Collapsed;
                GoodsWrapPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                var thickness = new Thickness(20, 0, 0, 0);
                foreach (var str in c.Goods)
                {
                    var breakPosition = str.IndexOf('|');
                    var goodSource = str.Substring(0, breakPosition);
                    var goodText = str.Substring(breakPosition + 1);
                    if (goodSource == "")
                    {
                        var textBlock = new TextBlock
                        {
                            HorizontalAlignment = HorizontalAlignment.Left,
                            Margin = thickness,
                            Text = goodText
                        };
                        GoodsWrapPanel.Children.Add(textBlock);
                    }
                    else if (goodText == "")
                    {
                        var picButton = new PicButton
                        {
                            HorizontalAlignment = HorizontalAlignment.Left,
                            Margin = thickness,
                            Source = StringProcess.GetGameResourcePath(goodSource),
                        };
                        picButton.Click += Creature_Jump_Click;
                        GoodsWrapPanel.Children.Add(picButton);
                    }
                    else
                    {
                        if (goodText.Substring(0, 1) == "S")
                        {
                            var stackPanel = new StackPanel
                            {
                                Orientation = Orientation.Horizontal,
                                Margin = thickness
                            };
                            var picButton1 = new PicButton
                            {
                                HorizontalAlignment = HorizontalAlignment.Left,
                                Source = StringProcess.GetGameResourcePath(goodSource),
                                Text = "（"
                            };
                            var picButton2 = new PicButton
                            {
                                Source = StringProcess.GetGameResourcePath(goodText),
                                Text = "）"
                            };
                            picButton1.Click += Creature_Jump_Click;
                            picButton2.Click += Creature_Jump_Click;
                            stackPanel.Children.Add(picButton1);
                            stackPanel.Children.Add(picButton2);
                            GoodsWrapPanel.Children.Add(stackPanel);
                        }
                        else
                        {
                            var picButton = new PicButton
                            {
                                HorizontalAlignment = HorizontalAlignment.Left,
                                Margin = thickness,
                                Source = StringProcess.GetGameResourcePath(goodSource),
                                Text = goodText
                            };
                            picButton.Click += Creature_Jump_Click;
                            GoodsWrapPanel.Children.Add(picButton);
                        }
                    }
                }
            }
            // 特殊能力
            if (c.Ability.Count == 0)
            {
                AbilityTextBlock.Visibility = Visibility.Collapsed;
                AbilityStackPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                foreach (var str in c.Ability)
                {
                    var textBlock = new TextBlock
                    {
                        HorizontalAlignment = HorizontalAlignment.Left,
                        TextWrapping = TextWrapping.Wrap,
                        Text = str
                    };
                    AbilityStackPanel.Children.Add(textBlock);
                }
            }
            // 坎普斯顽皮值表、鹈鹕食物表、猪王、亚克章鱼的特殊内容
            switch (c.EnName)
            {
                case "Krampus":
                    ShowKrampus(Global.GameVersion);
                    break;
                case "Packim Baggims":
                case "Fat Packim Baggims":
                    ShowPackimBaggims();
                    break;
                case "Pig King":
                    ShowPigKing(Global.GameVersion);
                    break;
                case "Yaarctopus":
                    ShowYaarctopus();
                    break;
            }
            // 介绍
            CreatureIntroduction.Text = c.Introduction;
            // 控制台
            if (c.Console != null)
            {
                ConsolePre.Text = $"c_spawn(\"{c.Console}\",";
                if (c.ConsoleStateValue != null)
                    ConsolePos.Text = $"):{c.ConsoleStateValue}";
            }
            else
            {
                CopyGrid.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// 顽皮值表
        /// </summary>
        /// <param name="gameVersion">游戏版本</param>
        private void ShowKrampus(double gameVersion)
        {
            var rootStackPanel = new StackPanel
            {
                Margin = new Thickness(0, 10, 0, 10)
            };
            var titleTextBlock = new TextBlock
            {
                Text = "顽皮值表：",
                Margin = new Thickness(10, 0, 0, 10),
                Background = new SolidColorBrush(Color.FromArgb(85, 178, 236, 237))
            };
            rootStackPanel.Children.Add(titleTextBlock);
            var stackPanel = new StackPanel
            {
                Margin = new Thickness(50, 0, 50, 0)
            };
            // 50
            var value50TextBlock = new TextBlock
            {
                Margin = new Thickness(0, 0, 0, 5),
                Text = "50"
            };
            var value50WrapPanel = new WrapPanel();
            if (gameVersion != 2)
                value50WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("A_glommer") });
            if (gameVersion == 4)
                value50WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("A_seal") });
            // 1-50
            var value1To50TextBlock = new TextBlock
            {
                Margin = new Thickness(0, 0, 0, 5),
                Text = "1-50"
            };
            var value1To50WrapPanel = new WrapPanel();
            value1To50WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("A_doydoy") });
            // 7
            var value7TextBlock = new TextBlock
            {
                Margin = new Thickness(0, 0, 0, 5),
                Text = "7"
            };
            var value7WrapPanel = new WrapPanel();
            value7WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("A_blue_whale") });
            // 6
            var value6TextBlock = new TextBlock
            {
                Margin = new Thickness(0, 0, 0, 5),
                Text = "6"
            };
            var value6WrapPanel = new WrapPanel();
            if (gameVersion != 4)
                value6WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("A_baby_beefalo") });
            value6WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("A_smallbird") });
            if (gameVersion == 4)
            {
                value6WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("A_parrot_pirate") });
                value6WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("A_white_whale") });
            }
            // 5
            var value5TextBlock = new TextBlock
            {
                Margin = new Thickness(0, 0, 0, 5),
                Text = "5"
            };
            var value5WrapPanel = new WrapPanel();
            value5WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("A_catcoon") });
            // 4
            var value4TextBlock = new TextBlock
            {
                Margin = new Thickness(0, 0, 0, 5),
                Text = "4"
            };
            var value4WrapPanel = new WrapPanel();
            if (gameVersion != 4)
            {
                value4WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("A_beefalo") });
            }
            else
            {
                value4WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("A_water_beefalo") });
                value4WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("A_swordfish") });
            }
            if (gameVersion == 0 || gameVersion == 1)
            {
                value4WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("A_no_eyed_deer") });
            }
            // 3
            var value3TextBlock = new TextBlock
            {
                Margin = new Thickness(0, 0, 0, 5),
                Text = "3"
            };
            var value3WrapPanel = new WrapPanel();
            value3WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("A_pig_man") });
            if (gameVersion != 4)
            {
                value3WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("A_bunnyman") });
                value3WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("A_beardlord") });
            }
            // 2
            var value2TextBlock = new TextBlock
            {
                Margin = new Thickness(0, 0, 0, 5),
                Text = "2"
            };
            var value2WrapPanel = new WrapPanel();
            if (gameVersion != 4)
            {
                value2WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("A_redbird") });
                value2WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("A_snowbird") });
                value2WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("A_pengull") });
            }
            value2WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("A_tallbird") });
            value2WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("A_teentallbird") });
            if (gameVersion == 4)
            {
                value2WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("A_parrot_pirate") });
                value2WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("A_dogfish") });
                value2WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("A_bottenosed_ballphin") });
                value2WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("A_prime_ape") });
                value2WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("A_wobster") });
            }
            if (gameVersion == 0 || gameVersion == 1)
            {
                value2WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("A_canary") });
            }
            // 1
            var value1TextBlock = new TextBlock
            {
                Margin = new Thickness(0, 0, 0, 5),
                Text = "1"
            };
            var value1WrapPanel = new WrapPanel();
            if (gameVersion != 4)
            {
                value1WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("A_rabbit") });
                value1WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("A_beardling") });
                value1WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("A_crow") });
                value1WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("A_butterfly") });
                value1WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("A_moleworm") });
            }
            value1WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("A_bee") });
            if (gameVersion == 4)
            {
                value1WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("A_butterfly_sw") });
                value1WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("A_crabbit") });
                value1WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("A_jellyfish") });
                value1WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("A_seagull") });
                value1WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("A_toucan") });
            }
            // 添加控件
            // 非DS
            if (gameVersion != 2)
            {
                stackPanel.Children.Add(value50TextBlock);
                stackPanel.Children.Add(value50WrapPanel);
            }
            // SW
            if (gameVersion == 4)
            {
                stackPanel.Children.Add(value1To50TextBlock);
                stackPanel.Children.Add(value1To50WrapPanel);
                stackPanel.Children.Add(value7TextBlock);
                stackPanel.Children.Add(value7WrapPanel);
            }
            stackPanel.Children.Add(value6TextBlock);
            stackPanel.Children.Add(value6WrapPanel);
            // 非DS非SW
            if (gameVersion != 2 && gameVersion != 4)
            {
                stackPanel.Children.Add(value5TextBlock);
                stackPanel.Children.Add(value5WrapPanel);
            }
            stackPanel.Children.Add(value4TextBlock);
            stackPanel.Children.Add(value4WrapPanel);
            stackPanel.Children.Add(value3TextBlock);
            stackPanel.Children.Add(value3WrapPanel);
            stackPanel.Children.Add(value2TextBlock);
            stackPanel.Children.Add(value2WrapPanel);
            stackPanel.Children.Add(value1TextBlock);
            stackPanel.Children.Add(value1WrapPanel);
            rootStackPanel.Children.Add(stackPanel);
            CreaturesRootGrid.Children.Add(rootStackPanel);
            Grid.SetRow(rootStackPanel, 9);
        }

        /// <summary>
        /// 鹈鹕食物表
        /// </summary>
        private void ShowPackimBaggims()
        {
            var rootStackPanel = new StackPanel
            {
                Margin = new Thickness(0, 10, 0, 10)
            };
            var titleTextBlock = new TextBlock
            {
                Margin = new Thickness(10, 0, 0, 5),
                Text = "鹈鹕食物表：",
                Background = new SolidColorBrush(Color.FromArgb(85, 178, 236, 237))
            };
            rootStackPanel.Children.Add(titleTextBlock);
            var wrapPanel = new WrapPanel { HorizontalAlignment = HorizontalAlignment.Center, Width = 170 };
            wrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("F_fish") });
            wrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("F_cooked_fish") });
            wrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("F_tropical_fish") });
            wrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("F_limpets") });
            wrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("F_cooked_limpets") });
            wrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("F_wobster") });
            wrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("F_dead_wobster") });
            wrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("F_delicious_wobster") });
            wrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("F_mussel") });
            wrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("F_cooked_mussel") });
            wrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("F_fish_morsel") });
            wrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("F_cooked_fish_morsel") });
            wrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("F_raw_fish") });
            wrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("F_fish_steak") });
            wrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("F_dead_dogfish") });
            wrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("F_dead_swordfish") });
            rootStackPanel.Children.Add(wrapPanel);
            CreaturesRootGrid.Children.Add(rootStackPanel);
            Grid.SetRow(rootStackPanel, 9);
        }

        /// <summary>
        /// 猪王交换表
        /// </summary>
        /// <param name="gameVersion">游戏版本</param>
        private void ShowPigKing(double gameVersion)
        {
            var rootStackPanel = new StackPanel
            {
                Margin = new Thickness(0, 10, 0, 10)
            };
            var titleTextBlock = new TextBlock
            {
                Margin = new Thickness(10, 0, 0, 5),
                Text = "金块交换表：",
                Background = new SolidColorBrush(Color.FromArgb(85, 178, 236, 237))
            };
            rootStackPanel.Children.Add(titleTextBlock);
            var stackPanel = new StackPanel
            {
                Margin = new Thickness(10, 0, 50, 0)
            };
            // 1
            var goldNugget1PicButton = new PicButton
            {
                Margin = new Thickness(0, 0, 0, 5),
                Source = StringProcess.GetGameResourcePath("S_gold_nugget"),
                Text = "×1"
            };
            var goldNugget1WrapPanel = new WrapPanel();
            goldNugget1WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("F_egg") });
            goldNugget1WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("F_cooked_egg") });
            goldNugget1WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("F_morsel") });
            goldNugget1WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("F_cooked_morsel") });
            goldNugget1WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("F_meat") });
            goldNugget1WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("F_cooked_meat") });
            goldNugget1WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("F_drumstick") });
            goldNugget1WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("F_fried_drumstick") });
            goldNugget1WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("F_leafy_meat") });
            goldNugget1WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("F_cooked_leafy_meat") });
            goldNugget1WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("F_fish") });
            goldNugget1WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("F_cooked_fish") });
            goldNugget1WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("F_batilisk_wing") });
            goldNugget1WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("F_cooked_batilisk_wing") });
            goldNugget1WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("F_koalefant_trunk") });
            goldNugget1WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("F_winter_koalefant_trunk") });
            goldNugget1WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("F_koalefant_trunk_steak") });
            goldNugget1WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("G_slurper_pelt") });
            goldNugget1WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("F_small_jerky") });
            goldNugget1WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("F_jerky") });
            goldNugget1WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("F_monster_jerky") });
            goldNugget1WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("G_pig_skin") });
            if (gameVersion == 0 || gameVersion == 1)
            {
                goldNugget1WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_beach_toy") });
            }
            else
            {
                goldNugget1WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_sea_worther") });
            }
            // 2
            var goldNugget2PicButton = new PicButton
            {
                Margin = new Thickness(0, 0, 0, 5),
                Source = StringProcess.GetGameResourcePath("S_gold_nugget"),
                Text = "×2"
            };
            var goldNugget2WrapPanel = new WrapPanel();
            goldNugget2WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("G_bunny_puff") });
            goldNugget2WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_second_hand_dentures") });
            if (gameVersion == 0 || gameVersion == 1)
            {
                goldNugget2WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_bent_spork") });
                goldNugget2WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_air_unfreshener") });
            }
            else
            {
                goldNugget2WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_sextant") });
                goldNugget2WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_toy_boat") });
            }
            // 3
            var goldNugget3PicButton = new PicButton
            {
                Margin = new Thickness(0, 0, 0, 5),
                Source = StringProcess.GetGameResourcePath("S_gold_nugget"),
                Text = "×3"
            };
            var goldNugget3WrapPanel = new WrapPanel();
            if (gameVersion == 0 || gameVersion == 1)
            {
                goldNugget3WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_leaky_teacup") });
                goldNugget3WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_shoe_horn") });
            }
            else
            {
                goldNugget3WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_one_true_earring") });
            }
            // 4
            var goldNugget4PicButton = new PicButton
            {
                Margin = new Thickness(0, 0, 0, 5),
                Source = StringProcess.GetGameResourcePath("S_gold_nugget"),
                Text = "×4"
            };
            var goldNugget4WrapPanel = new WrapPanel();
            goldNugget4WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_ball_and_cup") });
            goldNugget4WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_gord's_knot") });
            goldNugget4WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_melty_marbles") });
            goldNugget4WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_tiny_rocketship") });
            if (gameVersion == 0 || gameVersion == 1)
            {
                goldNugget4WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_white_bishop") });
                goldNugget4WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_black_bishop") });
                goldNugget4WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_frayed_yarn") });
                goldNugget4WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_wire_hanger") });
                goldNugget4WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_white_rook") });
                goldNugget4WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_black_rook") });
                goldNugget4WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_white_knight") });
                goldNugget4WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_black_knight") });
            }
            else
            {
                goldNugget4WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_old_boot") });
                goldNugget4WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_wine_bottle_candle") });
            }
            // 5
            var goldNugget5PicButton = new PicButton
            {
                Margin = new Thickness(0, 0, 0, 5),
                Source = StringProcess.GetGameResourcePath("S_gold_nugget"),
                Text = "×5"
            };
            var goldNugget5WrapPanel = new WrapPanel();
            goldNugget5WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("F_eel") });
            goldNugget5WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("F_cooked_eel") });
            goldNugget5WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_frazzled_wires") });
            goldNugget5WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_gnome_1") });
            goldNugget5WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_lying_robot") });
            if (gameVersion == 0 || gameVersion == 1)
            {
                goldNugget5WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_gnome_2") });
                goldNugget5WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_beaten_beater") });
            }
            // 6
            var goldNugget6PicButton = new PicButton
            {
                Margin = new Thickness(0, 0, 0, 5),
                Source = StringProcess.GetGameResourcePath("S_gold_nugget"),
                Text = "×6"
            };
            var goldNugget6WrapPanel = new WrapPanel();
            goldNugget6WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_fake_kazoo") });
            if (gameVersion == 0 || gameVersion == 1)
            {
                goldNugget6WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_toy_trojan_horse") });
                goldNugget6WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_unbalanced_top") });
            }
            else
            {
                goldNugget6WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_brain_cloud_pill") });
                goldNugget6WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_orange_soda") });
                goldNugget6WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_ukulele") });
            }
            // 7
            var goldNugget7PicButton = new PicButton
            {
                Margin = new Thickness(0, 0, 0, 5),
                Source = StringProcess.GetGameResourcePath("S_gold_nugget"),
                Text = "×7"
            };
            var goldNugget7WrapPanel = new WrapPanel();
            goldNugget7WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_mismatched_buttons") });
            if (gameVersion == 0 || gameVersion == 1)
            {
                goldNugget7WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_back_scratcher") });
            }
            else
            {
                goldNugget7WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_soaked_candle") });
                goldNugget7WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_license_plate") });
                goldNugget7WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_ancient_vase") });
            }
            // 8
            var goldNugget8PicButton = new PicButton
            {
                Margin = new Thickness(0, 0, 0, 5),
                Source = StringProcess.GetGameResourcePath("S_gold_nugget"),
                Text = "×8"
            };
            var goldNugget8WrapPanel = new WrapPanel();
            goldNugget8WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_dessicated_tentacle") });
            goldNugget8WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_hardened_rubber_bung") });
            if (gameVersion == 0 || gameVersion == 1)
            {
                goldNugget8WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_lucky_cat_jar") });
            }
            else
            {
                goldNugget8WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_voodoo_doll") });
            }
            // 9
            var goldNugget9PicButton = new PicButton
            {
                Margin = new Thickness(0, 0, 0, 5),
                Source = StringProcess.GetGameResourcePath("S_gold_nugget"),
                Text = "×9"
            };
            var goldNugget9WrapPanel = new WrapPanel();
            goldNugget9WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_potato_cup") });
            // 10
            var goldNugget10PicButton = new PicButton
            {
                Margin = new Thickness(0, 0, 0, 5),
                Source = StringProcess.GetGameResourcePath("S_gold_nugget"),
                Text = "×10"
            };
            var goldNugget10WrapPanel = new WrapPanel();
            goldNugget10WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_broken_AAC_device") });
            stackPanel.Children.Add(goldNugget1PicButton);
            stackPanel.Children.Add(goldNugget1WrapPanel);
            stackPanel.Children.Add(goldNugget2PicButton);
            stackPanel.Children.Add(goldNugget2WrapPanel);
            stackPanel.Children.Add(goldNugget3PicButton);
            stackPanel.Children.Add(goldNugget3WrapPanel);
            stackPanel.Children.Add(goldNugget4PicButton);
            stackPanel.Children.Add(goldNugget4WrapPanel);
            stackPanel.Children.Add(goldNugget5PicButton);
            stackPanel.Children.Add(goldNugget5WrapPanel);
            stackPanel.Children.Add(goldNugget6PicButton);
            stackPanel.Children.Add(goldNugget6WrapPanel);
            stackPanel.Children.Add(goldNugget7PicButton);
            stackPanel.Children.Add(goldNugget7WrapPanel);
            stackPanel.Children.Add(goldNugget8PicButton);
            stackPanel.Children.Add(goldNugget8WrapPanel);
            if (gameVersion == 0 || gameVersion == 1)
            {
                stackPanel.Children.Add(goldNugget9PicButton);
                stackPanel.Children.Add(goldNugget9WrapPanel);
            }
            if (gameVersion == 2 || gameVersion == 3)
            {
                stackPanel.Children.Add(goldNugget10PicButton);
                stackPanel.Children.Add(goldNugget10WrapPanel);
            }
            rootStackPanel.Children.Add(stackPanel);
            CreaturesRootGrid.Children.Add(rootStackPanel);
            Grid.SetRow(rootStackPanel, 9);
        }

        /// <summary>
        /// 亚克章鱼交换表
        /// </summary>
        private void ShowYaarctopus()
        {
            var rootStackPanel = new StackPanel
            {
                Margin = new Thickness(0, 10, 0, 10)
            };
            var dubloonsTitleTextBlock = new TextBlock
            {
                Margin = new Thickness(10, 0, 0, 5),
                Text = "金币交换表：",
                Background = new SolidColorBrush(Color.FromArgb(85, 178, 236, 237))
            };
            rootStackPanel.Children.Add(dubloonsTitleTextBlock);
            var dubloonsStackPanel = new StackPanel
            {
                Margin = new Thickness(50, 0, 50, 0)
            };
            // 5
            var dubloons5PicButton = new PicButton
            {
                Margin = new Thickness(0, 0, 0, 5),
                Source = StringProcess.GetGameResourcePath("G_dubloons"),
                Text = "×5"
            };
            var dubloons5WrapPanel = new WrapPanel();
            dubloons5WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_old_boot") });
            dubloons5WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_license_plate") });
            dubloons5WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_wine_bottle_candle") });
            // 7
            var dubloons7PicButton = new PicButton
            {
                Margin = new Thickness(0, 0, 0, 5),
                Source = StringProcess.GetGameResourcePath("G_dubloons"),
                Text = "×7"
            };
            var dubloons7WrapPanel = new WrapPanel();
            dubloons7WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_orange_soda") });
            dubloons7WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_voodoo_doll") });
            dubloons7WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_second_hand_dentures") });
            // 8
            var dubloons8PicButton = new PicButton
            {
                Margin = new Thickness(0, 0, 0, 5),
                Source = StringProcess.GetGameResourcePath("G_dubloons"),
                Text = "×8"
            };
            var dubloons8WrapPanel = new WrapPanel();
            dubloons8WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_brain_cloud_pill") });
            dubloons8WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_toy_boat") });
            dubloons8WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_ukulele") });
            // 9
            var dubloons9PicButton = new PicButton
            {
                Margin = new Thickness(0, 0, 0, 5),
                Source = StringProcess.GetGameResourcePath("G_dubloons"),
                Text = "×9"
            };
            var dubloons9WrapPanel = new WrapPanel();
            dubloons9WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_ball_and_cup") });
            dubloons9WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_gord's_knot") });
            dubloons9WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_melty_marbles") });
            dubloons9WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_tiny_rocketship") });
            // 10
            var dubloons10PicButton = new PicButton
            {
                Margin = new Thickness(0, 0, 0, 5),
                Source = StringProcess.GetGameResourcePath("G_dubloons"),
                Text = "×10"
            };
            var dubloons10WrapPanel = new WrapPanel();
            dubloons10WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_frazzled_wires") });
            dubloons10WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_gnome_1") });
            dubloons10WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_lying_robot") });
            // 11
            var dubloons11PicButton = new PicButton
            {
                Margin = new Thickness(0, 0, 0, 5),
                Source = StringProcess.GetGameResourcePath("G_dubloons"),
                Text = "×11"
            };
            var dubloons11WrapPanel = new WrapPanel();
            dubloons11WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_ancient_vase") });
            dubloons11WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_broken_AAC_device") });
            dubloons11WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_fake_kazoo") });
            dubloons11WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_sextant") });
            // 12
            var dubloons12PicButton = new PicButton
            {
                Margin = new Thickness(0, 0, 0, 5),
                Source = StringProcess.GetGameResourcePath("G_dubloons"),
                Text = "×12"
            };
            var dubloons12WrapPanel = new WrapPanel();
            dubloons12WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_one_true_earring") });
            dubloons12WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_dessicated_tentacle") });
            dubloons12WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_hardened_rubber_bung") });
            dubloons12WrapPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("T_mismatched_buttons") });
            // 添加控件
            dubloonsStackPanel.Children.Add(dubloons5PicButton);
            dubloonsStackPanel.Children.Add(dubloons5WrapPanel);
            dubloonsStackPanel.Children.Add(dubloons7PicButton);
            dubloonsStackPanel.Children.Add(dubloons7WrapPanel);
            dubloonsStackPanel.Children.Add(dubloons8PicButton);
            dubloonsStackPanel.Children.Add(dubloons8WrapPanel);
            dubloonsStackPanel.Children.Add(dubloons9PicButton);
            dubloonsStackPanel.Children.Add(dubloons9WrapPanel);
            dubloonsStackPanel.Children.Add(dubloons10PicButton);
            dubloonsStackPanel.Children.Add(dubloons10WrapPanel);
            dubloonsStackPanel.Children.Add(dubloons11PicButton);
            dubloonsStackPanel.Children.Add(dubloons11WrapPanel);
            dubloonsStackPanel.Children.Add(dubloons12PicButton);
            dubloonsStackPanel.Children.Add(dubloons12WrapPanel);
            rootStackPanel.Children.Add(dubloonsStackPanel);
            var fishsTitleTextBlock = new TextBlock
            {
                Margin = new Thickness(10, 5, 0, 5),
                Text = "用鱼类交换：",
                Background = new SolidColorBrush(Color.FromArgb(85, 178, 236, 237))
            };
            rootStackPanel.Children.Add(fishsTitleTextBlock);
            var fishsStackPanel = new StackPanel
            {
                Margin = new Thickness(20, 0, 10, 0)
            };
            fishsStackPanel.Children.Add(new PicButton
            {
                Source = StringProcess.GetGameResourcePath("G_dubloons"),
                Text = "×1"
            });
            fishsStackPanel.Children.Add(new PicButton
            {
                Source = StringProcess.GetGameResourcePath("F_seaweed"),
                Text = "×1（38.46%）"
            });
            fishsStackPanel.Children.Add(new PicButton
            {
                Source = StringProcess.GetGameResourcePath("G_seashell"),
                Text = "×1（23.08%）"
            });
            fishsStackPanel.Children.Add(new PicButton
            {
                Source = StringProcess.GetGameResourcePath("G_coral"),
                Text = "×1（23.08%）"
            });
            fishsStackPanel.Children.Add(new PicButton
            {
                Source = StringProcess.GetGameResourcePath("F_blubber"),
                Text = "×1（7.69%）"
            });
            fishsStackPanel.Children.Add(new PicButton
            {
                Source = StringProcess.GetGameResourcePath("F_shark_fin"),
                Text = "×1（7.69%）"
            });
            fishsStackPanel.Children.Add(new PicButton
            {
                Source = StringProcess.GetGameResourcePath("G_golden_key"),
                Text = "×1（10%）（未解锁伍德莱格）"
            });
            rootStackPanel.Children.Add(fishsStackPanel);
            var recipeToScienceTitleTextBlock = new TextBlock
            {
                Margin = new Thickness(10, 5, 0, 5),
                Text = "食谱食物换航海科技物品：",
                Background = new SolidColorBrush(Color.FromArgb(85, 178, 236, 237))
            };
            rootStackPanel.Children.Add(recipeToScienceTitleTextBlock);
            var recipeToScienceWrapPanel = new WrapPanel
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(50, 0, 50, 0)
            };

            // 箭头图片
            var yarrctopusBitmapImage = new BitmapImage(new Uri("/Resources/Yarrctopus_arrow_1.png", UriKind.Relative));
            // 1
            var rTs1StackPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal
            };
            var recipe1PicButton = new PicButton
            {
                Source = StringProcess.GetGameResourcePath("F_california_roll")
            };
            var arrowPicture1 = new Image { Source = yarrctopusBitmapImage, Width = 45, Height = 45 };
            var science1PicButton = new PicButton
            {
                Source = StringProcess.GetGameResourcePath("S_thatch_sail")
            };
            rTs1StackPanel.Children.Add(recipe1PicButton);
            rTs1StackPanel.Children.Add(arrowPicture1);
            rTs1StackPanel.Children.Add(science1PicButton);
            recipeToScienceWrapPanel.Children.Add(rTs1StackPanel);
            // 2
            var rTs2StackPanel = new StackPanel
            {
                Margin = new Thickness(5, 0, 0, 0),
                Orientation = Orientation.Horizontal
            };
            var recipe2PicButton = new PicButton
            {
                Source = StringProcess.GetGameResourcePath("F_seafood_gumbo")
            };
            var arrowPicture2 = new Image { Source = yarrctopusBitmapImage, Width = 45, Height = 45 };
            var science2PicButton = new PicButton
            {
                Source = StringProcess.GetGameResourcePath("S_cloth_sail")
            };
            rTs2StackPanel.Children.Add(recipe2PicButton);
            rTs2StackPanel.Children.Add(arrowPicture2);
            rTs2StackPanel.Children.Add(science2PicButton);
            recipeToScienceWrapPanel.Children.Add(rTs2StackPanel);
            // 3
            var rTs3StackPanel = new StackPanel
            {
                Margin = new Thickness(5, 0, 0, 0),
                Orientation = Orientation.Horizontal
            };
            var recipe3PicButton = new PicButton
            {
                Source = StringProcess.GetGameResourcePath("F_bisque")
            };
            var arrowPicture3 = new Image { Source = yarrctopusBitmapImage, Width = 45, Height = 45 };
            var science3PicButton = new PicButton
            {
                Source = StringProcess.GetGameResourcePath("S_trawl_net")
            };
            rTs3StackPanel.Children.Add(recipe3PicButton);
            rTs3StackPanel.Children.Add(arrowPicture3);
            rTs3StackPanel.Children.Add(science3PicButton);
            recipeToScienceWrapPanel.Children.Add(rTs3StackPanel);
            // 4
            var rTs4StackPanel = new StackPanel
            {
                Margin = new Thickness(5, 0, 0, 0),
                Orientation = Orientation.Horizontal
            };
            var recipe4PicButton = new PicButton
            {
                Source = StringProcess.GetGameResourcePath("F_jelly-O_pop")
            };
            var arrowPicture4 = new Image { Source = yarrctopusBitmapImage, Width = 45, Height = 45 };
            var science4PicButton = new PicButton
            {
                Source = StringProcess.GetGameResourcePath("S_sea_trap")
            };
            rTs4StackPanel.Children.Add(recipe4PicButton);
            rTs4StackPanel.Children.Add(arrowPicture4);
            rTs4StackPanel.Children.Add(science4PicButton);
            recipeToScienceWrapPanel.Children.Add(rTs4StackPanel);
            // 5
            var rTs5StackPanel = new StackPanel
            {
                Margin = new Thickness(5, 0, 0, 0),
                Orientation = Orientation.Horizontal
            };
            var recipe5PicButton = new PicButton
            {
                Source = StringProcess.GetGameResourcePath("F_ceviche")
            };
            var arrowPicture5 = new Image { Source = yarrctopusBitmapImage, Width = 45, Height = 45 };
            var science5PicButton = new PicButton
            {
                Source = StringProcess.GetGameResourcePath("S_spyglass")
            };
            rTs5StackPanel.Children.Add(recipe5PicButton);
            rTs5StackPanel.Children.Add(arrowPicture5);
            rTs5StackPanel.Children.Add(science5PicButton);
            recipeToScienceWrapPanel.Children.Add(rTs5StackPanel);
            // 6
            var rTs6StackPanel = new StackPanel
            {
                Margin = new Thickness(5, 0, 0, 0),
                Orientation = Orientation.Horizontal
            };
            var recipe6PicButton = new PicButton
            {
                Source = StringProcess.GetGameResourcePath("F_surf_'n'_turf")
            };
            var arrowPicture6 = new Image { Source = yarrctopusBitmapImage, Width = 45, Height = 45 };
            var science6PicButton = new PicButton
            {
                Source = StringProcess.GetGameResourcePath("S_boat_lantern")
            };
            rTs6StackPanel.Children.Add(recipe6PicButton);
            rTs6StackPanel.Children.Add(arrowPicture6);
            rTs6StackPanel.Children.Add(science6PicButton);
            recipeToScienceWrapPanel.Children.Add(rTs6StackPanel);
            // 7
            var rTs7StackPanel = new StackPanel
            {
                Margin = new Thickness(5, 0, 0, 0),
                Orientation = Orientation.Horizontal
            };
            var recipe7PicButton = new PicButton
            {
                Source = StringProcess.GetGameResourcePath("F_lobster_bisque")
            };
            var arrowPicture7 = new Image { Source = yarrctopusBitmapImage, Width = 45, Height = 45 };
            var science7PicButton = new PicButton
            {
                Source = StringProcess.GetGameResourcePath("S_pirate_hat")
            };
            rTs7StackPanel.Children.Add(recipe7PicButton);
            rTs7StackPanel.Children.Add(arrowPicture7);
            rTs7StackPanel.Children.Add(science7PicButton);
            recipeToScienceWrapPanel.Children.Add(rTs7StackPanel);
            // 8
            var rTs8StackPanel = new StackPanel
            {
                Margin = new Thickness(5, 0, 0, 0),
                Orientation = Orientation.Horizontal
            };
            var recipe8PicButton = new PicButton
            {
                Source = StringProcess.GetGameResourcePath("F_lobster_dinner")
            };
            var arrowPicture8 = new Image { Source = yarrctopusBitmapImage, Width = 45, Height = 45 };
            var science8PicButton = new PicButton
            {
                Source = StringProcess.GetGameResourcePath("S_boat_cannon")
            };
            rTs8StackPanel.Children.Add(recipe8PicButton);
            rTs8StackPanel.Children.Add(arrowPicture8);
            rTs8StackPanel.Children.Add(science8PicButton);
            recipeToScienceWrapPanel.Children.Add(rTs8StackPanel);

            rootStackPanel.Children.Add(recipeToScienceWrapPanel);
            CreaturesRootGrid.Children.Add(rootStackPanel);
            Grid.SetRow(rootStackPanel, 9);
        }

        private static void Creature_Jump_Click(object sender, RoutedEventArgs e)
        {
            var picturePath = Global.ButtonToPicButton((Button)sender).Source;
            var rightFrame = Global.RightFrame;
            var shortName = StringProcess.GetFileName(picturePath);
            Global.SetAutoSuggestBoxItem();
            foreach (var suggestBoxItem in Global.AutoSuggestBoxItemSource)
            {
                if (picturePath == suggestBoxItem.Picture)
                {
                    var picHead = shortName.Substring(0, 1);
                    string[] extraData = { suggestBoxItem.SourcePath, suggestBoxItem.Picture }; ;
                    switch (picHead)
                    {
                        case "F":
                            Global.PageJump(2);
                            rightFrame.NavigationService.Navigate(new FoodPage(), extraData);
                            return;
                        case "S":
                            Global.PageJump(4);
                            rightFrame.NavigationService.Navigate(new SciencePage(), extraData);
                            return;
                        case "A":
                        case "G":
                            Global.PageJump(7);
                            rightFrame.NavigationService.Navigate(new GoodPage(), extraData);
                            return;
                    }
                }
            }
        }

        private void ConsoleNum_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textbox = (TextBox)sender;
            StringProcess.ConsoleNumTextCheck(textbox);
        }

        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(ConsoleNum.Text) || double.Parse(ConsoleNum.Text) == 0)
            {
                ConsoleNum.Text = "1";
            }
            Clipboard.SetText(ConsolePre.Text + ConsoleNum.Text + ")");
        }
    }
}
