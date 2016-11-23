using System.Windows.Media;
using System.Windows.Controls;
using System.Windows;

namespace 饥荒百科全书CSharp.Class
{
    static class PG
    {
        public static Grid GridInterval(double height)
        {
            Grid grid = new Grid();
            grid.Background = Brushes.DarkOliveGreen;
            grid.Height = height;
            return grid;
        }

        public static Grid GridInit(double height = 0)
        {
            Grid grid = new Grid();
            grid.Background = Brushes.DarkOrange;
            if (height != 0)
            {
                grid.Height = height;
            }
            return grid;
        }

        public static TextBlock TextBlockInit(double height = 0)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Background = Brushes.Aqua;
            if (height != 0)
            {
                textBlock.Height = height;
            }
            return textBlock;
        }

        public static Grid GridTag(string TagString, double ThicknessLeft = 10)
        {
            Grid gTag = GridInit();
            TextBlock tb = new TextBlock();
            tb.Text = TagString;
            Thickness TTag = new Thickness();
            TTag.Left = ThicknessLeft;
            tb.Margin = TTag;
            gTag.Children.Add(tb);
            return gTag;
        }
    }
}
