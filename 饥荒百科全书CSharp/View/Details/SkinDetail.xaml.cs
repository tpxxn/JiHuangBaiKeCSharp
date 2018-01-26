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
    /// SkinDetail.xaml 的交互逻辑
    /// </summary>
    public partial class SkinDetail : Page
    {

        private int _loadedTime;

        public void LoadCompleted(object sender, NavigationEventArgs e)
        {
            if (e.ExtraData == null || _loadedTime != 0) return;
            _loadedTime++;
            LoadData((Skin)e.ExtraData);
            if (Global.FontFamily != null)
            {
                FontFamily = Global.FontFamily;
            }
            SkinLeftScrollViewer.FontWeight = Global.FontWeight;
        }

        public SkinDetail()
        {
            InitializeComponent();
            Global.SkinLeftFrame.NavigationService.LoadCompleted += LoadCompleted;
        }

        private byte _skinIndex;
        private int _skinMaxIndex;
        private readonly List<string> _skinColorList = new List<string>();
        private readonly List<string> _skinList = new List<string>();
        private readonly List<string> _skinIntroductionList = new List<string>();

        private void LoadData(Skin c)
        {
            // 颜色
            switch (c.Colors.Count)
            {
                case 0:
                    ColorTextBlock.Visibility = Visibility.Collapsed;
                    break;
                case 1:
                    ColorTextBlock.Text = "颜色：" + c.Colors[0];
                    break;
                default:
                    SwitchLeftButton.Visibility = Visibility.Visible;
                    SwitchRightButton.Visibility = Visibility.Visible;
                    _skinMaxIndex = c.Colors.Count - 1;
                    foreach (var color in c.Colors)
                    {
                        _skinColorList.Add(color);
                    }
                    SwitchLeftButton.IsEnabled = false;
                    ColorTextBlock.Text = "颜色：" + _skinColorList[0];
                    break;
            }
            // 图片
            if (c.Colors.Count == 0)
            {
                // 套装
                if (string.IsNullOrEmpty(c.Introduction) && string.IsNullOrEmpty(c.Rarity))
                {
                    ImageButtonGrid.Width = 258;
                    ImageButtonGrid.Height = 170;
                    SkinImage.Height = 170;
                    ImageColumnDefinition.Width = new GridLength(170);
                }
                SkinImage.Source = new BitmapImage(new Uri(c.Picture, UriKind.Relative));
            }
            else
            {
                foreach (var color in c.Colors)
                {
                    _skinList.Add(StringProcess.GetGameResourcePath("P_" + color.Replace(" ", "_").Replace("-", "_") + "_" + c.EnName.Replace(" ", "_").Replace("-", "_")));
                }
                SkinImage.Source = new BitmapImage(new Uri(_skinList[0], UriKind.Relative));
            }
            //中英文名
            SkinName.Text = c.Name;
            SkinEnName.Text = c.EnName;
            // 稀有度
            switch (c.Rarity)
            {
                case "Common":
                    RarityTextBlock.Text = "普通 Common";
                    break;
                case "Classy":
                    RarityTextBlock.Text = "上等 Classy";
                    break;
                case "Spiffy":
                    RarityTextBlock.Text = "出色 Spiffy";
                    break;
                case "Distinguished":
                    RarityTextBlock.Text = "卓越 Distinguished";
                    break;
                case "Elegant":
                    RarityTextBlock.Text = "高雅 Elegant";
                    break;
                case "Loyal":
                    RarityTextBlock.Text = "忠诚 Loyal";
                    break;
                case "Timeless":
                    RarityTextBlock.Text = "永恒 Timeless";
                    break;
                case "Event":
                    RarityTextBlock.Text = "事件 Event";
                    break;
                case "Proof of Purchase":
                    RarityTextBlock.Text = "购买证明 Proof of Purchase";
                    break;
                case "Reward":
                    RarityTextBlock.Text = "奖励 Reward";
                    break;
                default:
                    RarityTextBlock.Visibility = Visibility.Collapsed;
                    break;
            }
            RarityTextBlock.Foreground = Global.GetSkinColor(c.Rarity);
            // 介绍
            if (c.Colors.Count == 0)
            {
                SkinIntroduction.Text = c.Introduction;
            }
            else
            {
                foreach (var color in c.Colors)
                {
                    _skinIntroductionList.Add(c.Introduction.Replace("{Color}", color));
                }
                SkinIntroduction.Text = _skinIntroductionList[0];
            }
        }

        /// <summary>
        /// 左右切换按钮
        /// </summary>
        private void SwitchLeftButton_Click(object sender, RoutedEventArgs e)
        {
            SwitchRightButton.IsEnabled = true;
            if (_skinIndex != 0)
            {
                _skinIndex -= 1;
                if (_skinIndex == 0)
                {
                    SwitchLeftButton.IsEnabled = false;
                }
                SkinImage.Source = new BitmapImage(new Uri(_skinList[_skinIndex], UriKind.Relative));
                ColorTextBlock.Text = "颜色：" + _skinColorList[_skinIndex];
                SkinIntroduction.Text = _skinIntroductionList[_skinIndex];
            }
        }

        private void SwitchRightButton_Click(object sender, RoutedEventArgs e)
        {
            SwitchLeftButton.IsEnabled = true;
            if (_skinIndex != _skinMaxIndex)
            {
                _skinIndex += 1;
                if (_skinIndex == _skinMaxIndex)
                {
                    SwitchRightButton.IsEnabled = false;
                }
                SkinImage.Source = new BitmapImage(new Uri(_skinList[_skinIndex], UriKind.Relative));
                ColorTextBlock.Text = "颜色：" + _skinColorList[_skinIndex];
                SkinIntroduction.Text = _skinIntroductionList[_skinIndex];
            }
        }
    }
}
