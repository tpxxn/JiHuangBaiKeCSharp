using System;
using System.Collections.Generic;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using 饥荒百科全书CSharp.Class;

namespace 饥荒百科全书CSharp.MyUserControl
{
    /// <summary>
    /// HrlTextBlock.xaml 的交互逻辑
    /// </summary>
    public partial class HrlTextBlock : UserControl
    {
        #region 属性：Text

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(HrlTextBlock), new PropertyMetadata(string.Empty, OnTextChange));

        private static void OnTextChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null) return;
            var hrlTextBlock = (HrlTextBlock)d;
            if ((string)e.NewValue == null)
            {
                hrlTextBlock.TextBlock.Visibility = Visibility.Collapsed;
            }
            else
            {
                var textWidth = hrlTextBlock.MeasureTextWidth(hrlTextBlock.TextBlock.FontSize);
                hrlTextBlock.TextBlock.Text = (string)e.NewValue;
                hrlTextBlock.Grid.Width = textWidth;
                hrlTextBlock.TextBlock.Width = textWidth;
                if (textWidth > hrlTextBlock.HrlWidth)
                    hrlTextBlock.CeaterAnimation();
            }
        }

        #endregion

        #region 属性：HrlWidth

        public double HrlWidth
        {
            get => (double)GetValue(HrlWidthProperty);
            set => SetValue(HrlWidthProperty, value);
        }

        public static readonly DependencyProperty HrlWidthProperty =
            DependencyProperty.Register("HrlWidth", typeof(double), typeof(HrlTextBlock), new PropertyMetadata((double)80, OnHrlWidthChange));

        private static void OnHrlWidthChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null) return;
            var hrlTextBlock = (HrlTextBlock)d;
            if ((double)e.NewValue > 0)
            {
                hrlTextBlock.RectangleGeometry.Rect = new Rect(0, 0, (double)e.NewValue, 25.6);
                var textWidth = hrlTextBlock.MeasureTextWidth(hrlTextBlock.TextBlock.FontSize);
                hrlTextBlock.Grid.Width = textWidth;
                hrlTextBlock.TextBlock.Width = textWidth;
                if (textWidth > hrlTextBlock.HrlWidth)
                    hrlTextBlock.CeaterAnimation();
            }
            else
            {
                hrlTextBlock.RectangleGeometry.Rect = new Rect(0, 0, 80, 25.6);
            }
        }

        #endregion

        /// <summary>
        /// 获取文字长度
        /// </summary>
        /// <param name="fontSize">字体大小</param>
        /// <returns>文字长度</returns>
        private double MeasureTextWidth(double fontSize)
        {
            var formattedText = new FormattedText(
                Text,
                System.Globalization.CultureInfo.InvariantCulture,
                FlowDirection.LeftToRight,
                new Typeface(Global.FontFamily.ToString()),
                fontSize,
                Brushes.Black
            );
            return formattedText.WidthIncludingTrailingWhitespace;
        }

        /// <summary>
        /// 创建动画
        /// </summary>
        private void CeaterAnimation()
        {
            //创建动画资源
            var storyboard = new Storyboard();
            var length = MeasureTextWidth(TextBlock.FontSize);
            //移动动画
            {
                var widthMove = new DoubleAnimationUsingKeyFrames();
                Storyboard.SetTarget(widthMove, TextBlock);
                object[] propertyChain = {
                    TextBlock.RenderTransformProperty,
                    TransformGroup.ChildrenProperty,
                    TranslateTransform.XProperty,
                };
                Storyboard.SetTargetProperty(widthMove, new PropertyPath("(0).(1)[3].(2)", propertyChain));//设置动画类型
                widthMove.KeyFrames.Add(new EasingDoubleKeyFrame(5, KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0))));//添加时间线
                widthMove.KeyFrames.Add(new EasingDoubleKeyFrame(HrlWidth - length - 5, KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 2))));//添加时间线
                widthMove.KeyFrames.Add(new EasingDoubleKeyFrame(5, KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 4))));//添加时间线
                storyboard.Children.Add(widthMove);
            }
            storyboard.RepeatBehavior = RepeatBehavior.Forever;
            storyboard.Begin();
        }

        public HrlTextBlock()
        {
            InitializeComponent();
        }

    }
}
