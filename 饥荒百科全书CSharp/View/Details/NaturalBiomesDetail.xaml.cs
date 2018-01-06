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
    /// NaturalDetail.xaml 的交互逻辑
    /// </summary>
    public partial class NaturalBiomesDetail : Page
    {
        private int _loadedTime;

        public void LoadCompleted(object sender, NavigationEventArgs e)
        {
            if (e.ExtraData == null || _loadedTime != 0) return;
            _loadedTime++;
            LoadData((NatureBiomes)e.ExtraData);
            if (Global.FontFamily != null)
            {
                FontFamily = Global.FontFamily;
            }
            NaturalLeftScrollViewer.FontWeight = Global.FontWeight;
        }

        public NaturalBiomesDetail()
        {
            InitializeComponent();
            Global.NaturalLeftFrame.NavigationService.LoadCompleted += LoadCompleted;
        }

        private void LoadData(NatureBiomes c)
        {
            NatureImage.Source = new BitmapImage(new Uri(c.Picture, UriKind.Relative));
            NatureName.Text = c.Name;
            NatureEnName.Text = c.EnName;
            // 富含/偶尔/稀有
            var thickness = new Thickness(5, 0, 0, 0);
            if (c.Abundant == null || c.Abundant.Count == 0)
            {
                NaturalAbundantTextBlock.Visibility = Visibility.Collapsed;
                NaturalAbundantWrapPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                foreach (var picPath in c.Abundant)
                {
                    var picButton = new PicButton
                    {
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = thickness,
                        Source = StringProcess.GetGameResourcePath(picPath)
                    };
                    NaturalAbundantWrapPanel.Children.Add(picButton);
                }
            }
            if (c.Occasional == null || c.Occasional.Count == 0)
            {
                NaturalOccasionalTextBlock.Visibility = Visibility.Collapsed;
                NaturalOccasionalWrapPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                foreach (var picPath in c.Occasional)
                {
                    var picButton = new PicButton
                    {
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = thickness,
                        Source = StringProcess.GetGameResourcePath(picPath)
                    };
                    NaturalOccasionalWrapPanel.Children.Add(picButton);
                }
            }
            if (c.Rare == null || c.Rare.Count == 0)
            {
                NaturalRareTextBlock.Visibility = Visibility.Collapsed;
                NaturalRareWrapPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                foreach (var picPath in c.Rare)
                {
                    var picButton = new PicButton
                    {
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = thickness,
                        Source = StringProcess.GetGameResourcePath(picPath)
                    };
                    NaturalRareWrapPanel.Children.Add(picButton);
                }
            }
            // 介绍
            NatureIntroduction.Text = c.Introduction;
        }

    }
}
