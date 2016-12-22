using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage;
using Windows.UI.Xaml;
using JiHuangUWP.Model;
using JiHuangUWP.View;

namespace JiHuangUWP.ViewModel
{
    public class ViewModel : ViewModelBase
    {
        public ViewModel()
        {
            View = this;
        }

        public Visibility FrameVisibility
        {
            set
            {
                _frameVisibility = value;
                OnPropertyChanged();
            }
            get
            {
                return _frameVisibility;
            }
        }

        public DstidModel DstidModel
        {
            set;
            get;
        }

        private Visibility _frameVisibility;

        public async void Read()
        {
            FrameVisibility = Visibility.Collapsed;
#if NOGUI
            
#else
            Content.Navigate(typeof(SplashPage));
#endif
            //ViewModel
            DstidModel = new DstidModel();
            ViewModel.Add(new ViewModelPage()
            {
                Key = nameof(DstidModel),
                Page = typeof(DstitPage),
                ViewModel = DstidModel
            });

            List<DstidAbigail> dstidAbigail = new List<DstidAbigail>();
            try
            {


                string url = "ms-appx:///GameResource/DstidAbigail.txt";
                StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(url));

                StorageFolder folder = await file.GetParentAsync();

                var str = (await FileIO.ReadTextAsync(file)).Split('\n');
                for (int i = 0; i < str.Length; i++)
                {
                    dstidAbigail.Add(new DstidAbigail()
                    {
                        Name = str[i++].Trim(),
                        Chinese = str[i++].Trim(),
                        Debug = str[i++].Trim(),
                        Image = str[i++].Trim()
                    });
                }

                DsBitmapRead(dstidAbigail, await folder.GetFilesAsync());

            }
            catch (Exception)
            {

            }

            Navigateto(typeof(DstidModel), dstidAbigail);
        }

        private static void DsBitmapRead(List<DstidAbigail> dstidAbigail, IReadOnlyList<StorageFile> folder)
        {
            //foreach (var temp in await folder)
            //{
            //    for (int i = 0; i < dstidAbigail.Count; i++)
            //    {
            //        string str = dstidAbigail[i].Name.Replace(" ", "_");

            //    }
            //}


            foreach (var temp in dstidAbigail)
            {
                for (int i = 0; i < folder.Count; i++)
                {
                    string str = temp.Name.Replace(" ", "_").ToLower();
                    if (folder[i].Name.Substring(2).ToLower() == str + ".png")
                    {
                        temp.Image = folder[i].Name;
                    }
                }
            }

        }

        public ViewModel View
        {
            set;
            get;
        }

        public void NavigateToInfo()
        {

        }

        public void NavigateToAccount()
        {

        }

        public override void OnNavigatedFrom(object obj)
        {
            throw new NotImplementedException();
        }

        public override void OnNavigatedTo(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
