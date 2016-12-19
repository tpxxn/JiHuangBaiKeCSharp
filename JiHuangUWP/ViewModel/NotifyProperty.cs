// lindexi

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;

namespace JiHuangUWP
{
    /// <summary>
    ///     提供继承通知UI改变值
    /// </summary>
    public class NotifyProperty : INotifyPropertyChanged
    {
        public NotifyProperty()
        {
        }

        public void UpdateProper<T>(ref T properValue, T newValue, [CallerMemberName] string properName = "")
        {
            if (Equals(properValue, newValue))
            {
                return;
            }

            properValue = newValue;
            OnPropertyChanged(properName);
        }

        public async void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                () =>
                {
                    handler?.Invoke(this, new PropertyChangedEventArgs(name));
                });
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}