using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

namespace JiHuangUWP.ViewModel
{

    public abstract class ViewModelBase : NotifyProperty, INavigable, INavigato
    {
        public abstract void OnNavigatedFrom(object obj);
        public abstract void OnNavigatedTo(object obj);

        public List<ViewModelPage> ViewModel
        {
            set;
            get;
        } = new List<ViewModelPage>();

        public Frame Content
        {
            set;
            get;
        }

        //当前ViewModel
        private ViewModelBase _viewModel;

        public async void Navigateto(Type viewModel, object paramter)
        {
            _viewModel?.OnNavigatedFrom(null);
            ViewModelPage view = ViewModel.Find(temp => temp.ViewModel.GetType() == viewModel);
            await view.Navigate(Content, paramter);
            _viewModel = view.ViewModel;
        }
    }
    //跳转回
    //向根目录通信

    /// <summary>
    /// 使用Key获得ViewModel
    /// </summary>
    public interface IKeyNavigato : INavigato
    {
        void Navigateto(string key, object parameter);
    }

    public interface INavigato
    {
        void Navigateto(Type viewModel, object parameter);

        Frame Content
        {
            set;
            get;
        }
    }

    public interface INavigable
    {
        /// <summary>
        /// 不使用这个页面
        /// 清理页面
        /// </summary>
        /// <param name="obj"></param>
        void OnNavigatedFrom(object obj);
        /// <summary>
        /// 跳转到
        /// </summary>
        /// <param name="obj"></param>
        void OnNavigatedTo(object obj);
    }
}