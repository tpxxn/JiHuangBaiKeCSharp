using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using JiHuangUWP.Model;
using JiHuangUWP.View;
using lindexi.uwp.Framework.ViewModel;

namespace JiHuangUWP.ViewModel
{
    public class ViewModel : NavigateViewModel
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

        public ViewModel View
        {
            set;
            get;
        }

        public List<EposPage> EposPage
        {
            get;
        } = new List<EposPage>()
        {
            new EposPage("物品",typeof(DstidModel)),
        };

        public void Epos_OnItemClick(object sender, ItemClickEventArgs e)
        {

        }

        public void NavigateToInfo()
        {

        }

        public void NavigateToAccount()
        {

        }

        public override void OnNavigatedFrom(object sender, object obj)
        {
        }

        public override async void OnNavigatedTo(object sender, object obj)
        {
            FrameVisibility = Visibility.Collapsed;
#if NOGUI
            
#else
            Content.Navigate(typeof(SplashPage));
#endif

            if (ViewModel == null)
            {
                ViewModel = new List<ViewModelPage>();
                //加载所有ViewModel
                var applacationAssembly = Application.Current.GetType().GetTypeInfo().Assembly;

                foreach (var temp in applacationAssembly.DefinedTypes.Where(temp => temp.IsSubclassOf(typeof(ViewModelBase))))
                {
                    ViewModel.Add(new ViewModelPage(temp.AsType()));
                }

                foreach (var temp in applacationAssembly.DefinedTypes.Where(temp => temp.IsSubclassOf(typeof(Page))))
                {
                    //获取特性，特性有包含ViewModel
                    var p = temp.GetCustomAttribute<ViewModelAttribute>();

                    var viewmodel = ViewModel.FirstOrDefault(t => t.Equals(p?.ViewModel));
                    if (viewmodel != null)
                    {
                        viewmodel.Page = temp.AsType();
                    }
                }
            }

            DstidModel = (DstidModel)this[typeof(DstidModel).Name];

            //ViewModel
            //DstidModel = new DstidModel();
            //JiHuangUWP.ViewModel.ViewModel.Add(new ViewModelPage()
            //{
            //    Key = nameof(DstidModel),
            //    Page = typeof(DstitPage),
            //    ViewModel = DstidModel
            //});

            await DstidModel.Read();

            Navigate(typeof(DstidModel), null);
            FrameVisibility = Visibility.Visible;
        }
    }
}
