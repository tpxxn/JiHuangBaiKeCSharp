using System;
using System.Reflection;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;

namespace JiHuangUWP.ViewModel
{
    public class ViewModelPage
    {
        public string Key
        {
            set;
            get;
        }
        public ViewModelPage()
        {
            //if (ViewModel == null)
            //{
            //    //ViewModel=View.GetConstructor(null)
            //}
        }

        public async Task Navigate(Frame content, object paramter)
        {
            if (ViewModel == null)
            {
                ViewModel = (ViewModelBase)View.GetConstructor(Type.EmptyTypes).Invoke(null);
            }
            ViewModel.OnNavigatedTo(paramter);
#if NOGUI
            return;
#endif
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                () =>
                {
                    content.Navigate(Page);
                });
        }

        private Type View
        {
            set;
            get;
        }


        public ViewModelPage(ViewModelBase viewModel, Type page)
        {
            ViewModel = viewModel;
            Page = page;
        }


        public ViewModelBase ViewModel
        {
            set;
            get;
        }

        public Type Page
        {
            set;
            get;
        }
    }
}