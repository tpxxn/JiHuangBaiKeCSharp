using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace 饥荒百科全书CSharp.Class.JsonDeserialize
{
    public static class SkinsColors
    {
        public static SolidColorBrush Common = new SolidColorBrush(Color.FromRgb(153, 179, 186));
        public static SolidColorBrush Classy = new SolidColorBrush(Color.FromRgb(65, 80, 120));
        public static SolidColorBrush Spiffy = new SolidColorBrush(Color.FromRgb(104, 69, 124));
        public static SolidColorBrush Distinguished = new SolidColorBrush(Color.FromRgb(186, 116, 166));
        public static SolidColorBrush Elegant = new SolidColorBrush(Color.FromRgb(189, 70, 70));
        public static SolidColorBrush Loyal = new SolidColorBrush(Color.FromRgb(146, 180, 95));
        public static SolidColorBrush Timeless = new SolidColorBrush(Color.FromRgb(108, 193, 126));
        public static SolidColorBrush Event = new SolidColorBrush(Color.FromRgb(180, 148, 0));
        public static SolidColorBrush ProofOfPurchase = new SolidColorBrush(Color.FromRgb(76, 122, 77));
        public static SolidColorBrush Reward = new SolidColorBrush(Color.FromRgb(232, 151, 81));
    }

    public class Skin
    {
        public string Picture { get; set; }
        public List<string> Pictures { get; set; }
        public string Name { get; set; }
        public string EnName { get; set; }
        public string Rarity { get; set; }
        public List<string> Colors { get; set; }
        public string Introduction { get; set; }
        public List<string> Introductions { get; set; }
        // 绑定前景色
        public SolidColorBrush Color { get; set; }

        public Skin()
        {
            Pictures = new List<string>();
            Colors = new List<string>();
            Introductions = new List<string>();
            Color = new SolidColorBrush();
        }
    }

    public class Body
    {
        public List<Skin> Skin { get; set; }

        public Body()
        {
            Skin = new List<Skin>();
        }
    }

    public class Hands
    {
        public List<Skin> Skin { get; set; }

        public Hands()
        {
            Skin = new List<Skin>();
        }
    }

    public class SkinsRootObject
    {
        public Body Body { get; set; }
        public Hands Hands { get; set; }

        public SkinsRootObject()
        {
            Body = new Body();
            Hands = new Hands();
        }
    }
}
