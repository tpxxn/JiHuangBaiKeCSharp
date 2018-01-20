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
using 饥荒百科全书CSharp.Class.JsonDeserialize;
using 饥荒百科全书CSharp.MyUserControl;

namespace 饥荒百科全书CSharp.View.Details
{
    /// <summary>
    /// NaturalTreesDetail.xaml 的交互逻辑
    /// </summary>
    public partial class NaturalTreesDetail : Page
    {
        private int _loadedTime;

        public void LoadCompleted(object sender, NavigationEventArgs e)
        {
            if (e.ExtraData == null || _loadedTime != 0) return;
            _loadedTime++;
            LoadData((NatureTree)e.ExtraData);
            if (Global.FontFamily != null)
            {
                FontFamily = Global.FontFamily;
            }
            NaturalLeftScrollViewer.FontWeight = Global.FontWeight;
        }

        public NaturalTreesDetail()
        {
            InitializeComponent();
            Global.NaturalLeftFrame.NavigationService.LoadCompleted += LoadCompleted;
            Global.ConsoleSendKey = Console_Click;
        }

        private List<List<string>> _treeResourceListStringList;
        private List<List<string>> _treeResourcesBurnedListStringList;
        private readonly List<string> _treeConsoleStringList = new List<string>();
        private byte _treeIndex;
        private int _treeMaxIndex;
        private readonly List<string> _treeList = new List<string>();

        private void LoadData(NatureTree c)
        {
            // 图片
            if (c.Pictures.Count == 0)
            {
                NatureImage.Source = new BitmapImage(new Uri(c.Picture, UriKind.Relative));
            }
            else
            {
                SwitchLeftButton.Visibility = Visibility.Visible;
                SwitchRightButton.Visibility = Visibility.Visible;
                // 数量分割点
                var breakPosition = c.Pictures[0].IndexOf('|');
                // 多名称多图
                if (breakPosition == -1)
                {
                    _treeMaxIndex = c.Pictures.Count - 1;
                    foreach (var pic in c.Pictures)
                    {
                        _treeList.Add(StringProcess.GetGameResourcePath(pic));
                    }
                    NatureImage.Source = new BitmapImage(new Uri(_treeList[0], UriKind.Relative));
                    SwitchLeftButton.IsEnabled = false;
                }
                // 单名称多图
                else
                {
                    var pictureText = c.Pictures[0].Substring(0, breakPosition);
                    var pictureNum = int.Parse(c.Pictures[0].Substring(breakPosition + 1));
                    _treeMaxIndex = pictureNum - 1;
                    for (var i = 1; i <= pictureNum; i++)
                    {
                        _treeList.Add(StringProcess.GetGameResourcePath(pictureText + i));
                    }
                    NatureImage.Source = new BitmapImage(new Uri(_treeList[0], UriKind.Relative));
                    SwitchLeftButton.IsEnabled = false;
                }
            }
            //中英文名
            NatureName.Text = c.Name;
            NatureEnName.Text = c.EnName;
            // 可再生、可燃
            if (c.IsRegenerate)
            {
                RegenerateCheckBox.IsChecked = true;
            }
            if (c.IsCombustible)
            {
                CombustibleCheckBox.IsChecked = true;
            }
            // 砍伐需求工具
            if (string.IsNullOrEmpty(c.CutDownTools))
            {
                NaturalCutDownTextBlock.Visibility = Visibility.Collapsed;
                NaturalCutDownStackPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                if (c.CutDownTools == "Axe")
                {
                    NaturalCutDownStackPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("S_axe") });
                    NaturalCutDownStackPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("S_goldenaxe") });
                    NaturalCutDownStackPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("S_pickaxe_1") });
                    NaturalCutDownStackPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("G_lucy_the_axe") });
                }
                else if (c.CutDownTools == "Pickaxe")
                {
                    NaturalCutDownStackPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("S_pickaxe") });
                    NaturalCutDownStackPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("S_goldenpickaxe") });
                    NaturalCutDownStackPanel.Children.Add(new PicButton { Source = StringProcess.GetGameResourcePath("S_pickaxe_1") });
                }
            }
            // 资源
            if (c.Resources.Count == 0)
            {
                NaturalResourceTextBlock.Visibility = Visibility.Collapsed;
                NaturalResourceStackPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                _treeResourceListStringList = new List<List<string>>();
                if (c.Resources.Count == 1)
                {
                    for (var i = 0; i <= _treeMaxIndex; i++)
                        _treeResourceListStringList.Add(c.Resources[0]);
                }
                else
                {
                    foreach (var strList in c.Resources)
                    {
                        _treeResourceListStringList.Add(strList);
                    }
                }
                ShowResources(0);
            }
            // 燃烧后资源
            if (c.ResourcesBurned.Count == 0)
            {
                NaturalResourcesBurnedTextBlock.Visibility = Visibility.Collapsed;
                NaturalResourcesBurnedStackPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                _treeResourcesBurnedListStringList = new List<List<string>>();
                if (c.ResourcesBurned.Count == 1)
                {
                    for (var i = 0; i <= _treeMaxIndex; i++)
                        _treeResourcesBurnedListStringList.Add(c.ResourcesBurned[0]);
                }
                else
                {
                    foreach (var strList in c.ResourcesBurned)
                    {
                        _treeResourcesBurnedListStringList.Add(strList);
                    }
                }
                ShowResourcesBurned(0);

            }
            //特殊能力
            if (c.Ability.Count == 0)
            {
                NaturalAbilityTextBlock.Visibility = Visibility.Collapsed;
                NaturalAbilityStackPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                var thickness = new Thickness(20, 0, 0, 0);
                foreach (var str in c.Ability)
                {
                    if (str.Substring(0, 2) == "A_")
                    {
                        var stackPanel = new StackPanel
                        {
                            Orientation = Orientation.Horizontal,
                            Margin = thickness
                        };
                        var textBlock = new TextBlock
                        {
                            Text = "生成",
                            VerticalAlignment = VerticalAlignment.Center
                        };
                        var picButton = new PicButton
                        {
                            HorizontalAlignment = HorizontalAlignment.Left,
                            Margin = new Thickness(5, 0, 0, 0),
                            Source = StringProcess.GetGameResourcePath(str)
                        };
                        stackPanel.Children.Add(textBlock);
                        stackPanel.Children.Add(picButton);
                        NaturalAbilityStackPanel.Children.Add(stackPanel);
                    }
                    else
                    {
                        var textBlock = new TextBlock
                        {
                            Text = str,
                            Margin = thickness
                        };
                        NaturalAbilityStackPanel.Children.Add(textBlock);
                    }
                    //picButton.Click += Creature_Jump_Click;
                }
            }
            // 主要生物群落
            var biomesThickness = new Thickness(2, 0, 2, 0);
            if (c.Biomes == null)
            {
                NaturalBiomesTextBlock.Visibility = Visibility.Collapsed;
                NaturalBiomesWrapPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                NaturalBiomesTextBlock.Visibility = Visibility.Visible;
                NaturalBiomesWrapPanel.Visibility = Visibility.Visible;
                foreach (var str in c.Biomes)
                {
                    var picButton = new PicButton
                    {
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = biomesThickness,
                        Source = StringProcess.GetGameResourcePath(str),
                        PictureSize = 90
                    };
                    //picButton.Click += Creature_Jump_Click;
                    NaturalBiomesWrapPanel.Children.Add(picButton);
                }
            }
            // 介绍
            NatureIntroduction.Text = c.Introduction;
            // 控制台
            ConsolePre.Text = $"c_spawn(\"{c.Console[0]}\",";
            foreach (var console in c.Console)
            {
                _treeConsoleStringList.Add(console);
            }
        }

        /// <summary>
        /// 显示资源
        /// </summary>
        /// <param name="index">索引序号</param>
        private void ShowResources(int index)
        {
            NaturalResourceStackPanel.Children.Clear();
            var thickness = new Thickness(20, 0, 0, 0);
            if (_treeResourceListStringList[index].Count == 0)
            {
                NaturalResourceTextBlock.Visibility = Visibility.Collapsed;
                NaturalResourceStackPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                NaturalResourceTextBlock.Visibility = Visibility.Visible;
                NaturalResourceStackPanel.Visibility = Visibility.Visible;
                foreach (var str in _treeResourceListStringList[index])
                {
                    // 数量分割点
                    var breakPosition = str.IndexOf('|');
                    // 工具分割点
                    var toolBreakPosition = str.IndexOf('&');
                    // 图片
                    var resourceSource = str.Substring(0, breakPosition);
                    // 数量文本
                    var resourceText = str.Substring(breakPosition + 1, toolBreakPosition - breakPosition - 1);
                    // 工具
                    var toolText = str.Substring(toolBreakPosition + 1);
                    // 使用工具
                    if (toolText.Length > 0)
                    {
                        var stackPanel = new StackPanel
                        {
                            Orientation = Orientation.Horizontal,
                            Margin = thickness
                        };
                        var picButton1 = new PicButton
                        {
                            HorizontalAlignment = HorizontalAlignment.Left,
                            Source = StringProcess.GetGameResourcePath(resourceSource),
                            Text = resourceText + "（"
                        };
                        stackPanel.Children.Add(picButton1);
                        PicButton picButton2;
                        PicButton picButton3;
                        if (toolText == "Shovel")
                        {
                            picButton2 = new PicButton
                            {
                                Source = StringProcess.GetGameResourcePath("S_shovel")
                            };
                            picButton3 = new PicButton
                            {
                                Source = StringProcess.GetGameResourcePath("S_goldenshovel"),
                                Text = "）"
                            };
                            stackPanel.Children.Add(picButton2);
                            stackPanel.Children.Add(picButton3);
                        }
                        else if (toolText == "Machete")
                        {
                            picButton2 = new PicButton
                            {
                                Source = StringProcess.GetGameResourcePath("S_machete")
                            };
                            picButton3 = new PicButton
                            {
                                Source = StringProcess.GetGameResourcePath("S_luxury_machete"),
                            };
                            var picButton4 = new PicButton
                            {
                                Source = StringProcess.GetGameResourcePath("S_obsidian_machete"),
                                Text = "）"
                            };
                            stackPanel.Children.Add(picButton2);
                            stackPanel.Children.Add(picButton3);
                            stackPanel.Children.Add(picButton4);
                        }
                        else if (toolText == "Hammer")
                        {
                            picButton2 = new PicButton
                            {
                                Source = StringProcess.GetGameResourcePath("S_hammer"),
                                Text = "）"
                            };
                            stackPanel.Children.Add(picButton2);
                        }
                        //picButton1.Click += Creature_Jump_Click;
                        //picButton2.Click += Creature_Jump_Click;
                        NaturalResourceStackPanel.Children.Add(stackPanel);
                    }
                    else
                    {
                        var picButton = new PicButton
                        {
                            HorizontalAlignment = HorizontalAlignment.Left,
                            Margin = thickness,
                            Source = StringProcess.GetGameResourcePath(resourceSource),
                            Text = resourceText
                        };
                        //picButton.Click += Creature_Jump_Click;
                        NaturalResourceStackPanel.Children.Add(picButton);
                    }
                }
            }
        }

        /// <summary>
        /// 显示燃烧后资源
        /// </summary>
        /// <param name="index">索引序号</param>
        private void ShowResourcesBurned(int index)
        {
            NaturalResourcesBurnedStackPanel.Children.Clear();
            var thickness = new Thickness(20, 0, 0, 0);
            if (_treeResourcesBurnedListStringList[index].Count == 0)
            {
                NaturalResourcesBurnedTextBlock.Visibility = Visibility.Collapsed;
                NaturalResourcesBurnedStackPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                NaturalResourcesBurnedTextBlock.Visibility = Visibility.Visible;
                NaturalResourcesBurnedTextBlock.Visibility = Visibility.Visible;
                foreach (var str in _treeResourcesBurnedListStringList[index])
                {
                    //数量分割点
                    var breakPosition = str.IndexOf('|');
                    // 图片
                    var resourceSource = str.Substring(0, breakPosition);
                    // 数量文本
                    var resourceText = str.Substring(breakPosition + 1);
                    var picButton = new PicButton
                    {
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = thickness,
                        Source = StringProcess.GetGameResourcePath(resourceSource),
                        Text = resourceText
                    };
                    //picButton.Click += Creature_Jump_Click;
                    NaturalResourcesBurnedStackPanel.Children.Add(picButton);
                }
            }
        }

        /// <summary>
        /// 左右切换按钮
        /// </summary>
        private void SwitchLeftButton_Click(object sender, RoutedEventArgs e)
        {
            SwitchRightButton.IsEnabled = true;
            if (_treeIndex != 0)
            {
                _treeIndex -= 1;
                if (_treeIndex == 0)
                {
                    SwitchLeftButton.IsEnabled = false;
                }
                NatureImage.Source = new BitmapImage(new Uri(_treeList[_treeIndex], UriKind.Relative));
                ShowResources(_treeIndex);
                if (_treeResourcesBurnedListStringList != null)
                    ShowResourcesBurned(_treeIndex);
                if (_treeConsoleStringList.Count != 1)
                {
                    ConsolePre.Text = $"c_spawn(\"{_treeConsoleStringList[_treeIndex]}\",";
                }
            }
        }

        private void SwitchRightButton_Click(object sender, RoutedEventArgs e)
        {
            SwitchLeftButton.IsEnabled = true;
            if (_treeIndex != _treeMaxIndex)
            {
                _treeIndex += 1;
                if (_treeIndex == _treeMaxIndex)
                {
                    SwitchRightButton.IsEnabled = false;
                }
                NatureImage.Source = new BitmapImage(new Uri(_treeList[_treeIndex], UriKind.Relative));
                ShowResources(_treeIndex);
                if (_treeResourcesBurnedListStringList != null)
                    ShowResourcesBurned(_treeIndex);
                if (_treeConsoleStringList.Count != 1)
                {
                    ConsolePre.Text = $"c_spawn(\"{_treeConsoleStringList[_treeIndex]}\",";
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
            Global.SetClipboard(ConsolePre.Text + ConsoleNum.Text + ConsolePos.Text);
        }

        private void Console_Click(object sender, RoutedEventArgs e)
        {
            SendKey.SendMessage(ConsolePre.Text + ConsoleNum.Text + ConsolePos.Text);
        }
    }
}
