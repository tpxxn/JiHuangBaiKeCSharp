using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;

namespace 饥荒百科全书CSharp.Class
{
    class ResourceShortName
    {
        public partial class SNClass
        {
            public string ShortName(string R_URL)
            {
                R_URL = "../Resources/" + R_URL + ".png";
                return R_URL;
            }

            public BitmapImage PictureShortName(string source = "")
            {
                var Picture = new BitmapImage();
                Picture.BeginInit();
                if (source == "")
                {
                    Picture.UriSource = new Uri("{x:Null}", UriKind.Relative);
                }
                else
                {
                    Picture.UriSource = new Uri(source, UriKind.Relative);
                }
                Picture.EndInit();
                return Picture;
            }
        }
    }
}
