using System.Windows.Media;
using System.Windows.Controls;
using System.Windows;

namespace 饥荒百科全书CSharp.Class
{
    internal static class Pg
    {
        public static Grid GridInterval(double height)
        {
            var grid = new Grid { Height = height };
            //grid.Background = Brushes.DarkOliveGreen;
            return grid;
        }

        public static Grid GridInit(double height = 0)
        {
            var grid = new Grid();
            //grid.Background = Brushes.DarkOrange;
            if (height != 0)
            {
                grid.Height = height;
            }
            return grid;
        }

        public static TextBlock TextBlockInit(double height = 0)
        {
            var textBlock = new TextBlock {Background = Brushes.Aqua};
            if (height != 0)
            {
                textBlock.Height = height;
            }
            return textBlock;
        }

        public static Grid GridTag(string tagString, double thicknessLeft = 10)
        {
            var gTag = GridInit();
            gTag.Background = Bc.BrushConverter("B2ECED", "55");
            var tb = new TextBlock {Text = tagString};
            var tag = new Thickness {Left = thicknessLeft};
            tb.Margin = tag;
            gTag.Children.Add(tb);
            return gTag;
        }
    }
}
