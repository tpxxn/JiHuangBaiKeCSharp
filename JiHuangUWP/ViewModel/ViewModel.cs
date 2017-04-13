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

            await DstidModel.Read();

            Navigateto(typeof(DstidModel),null);
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
