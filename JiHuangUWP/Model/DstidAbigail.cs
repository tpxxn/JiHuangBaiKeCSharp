using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;
using lindexi.uwp.Framework.ViewModel;

namespace JiHuangUWP.Model
{
    public class DstidAbigail : NotifyProperty
    {
        public string Name
        {
            set;
            get;
        }

        public string Chinese
        {
            set;
            get;
        }

        public string Image
        {
            set;
            get;
        }

        public BitmapImage ImageSource
        {
            set
            {
                _imageSource = value;
                OnPropertyChanged();
            }
            get
            {
                return _imageSource;
            }
        }

        public void Clipboard()
        {
            var temp=new DataPackage();
            temp.SetText(Debug);
            Windows.ApplicationModel.DataTransfer.Clipboard.SetContent(temp);
        }

        private BitmapImage _imageSource;

        public string Debug
        {
            set;
            get;
        }

        private static BitmapImage _image=new BitmapImage(new Uri("ms-appx:///GameResource/DLC_DST.png"));

        public async Task Read()
        {
            if (ImageSource != null)
            {
                return;
            }
            if (Image == ".png" || string.IsNullOrEmpty(Image))
            {
                ImageSource = _image;
               return;
            }
            try
            {
                var url = "ms-appx:///GameResource/" + Image;
                var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(url));

                var image = new BitmapImage();
                await image.SetSourceAsync(await file.OpenAsync(FileAccessMode.Read));
                ImageSource = image;
            }
            catch (Exception)
            {
                
            }
        }

     
    }
}
