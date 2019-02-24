using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using 饥荒百科全书CSharp.Class;
using 饥荒百科全书CSharp.Class.JsonDeserialize;
using 饥荒百科全书CSharp.MyUserControl;
using 饥荒百科全书CSharp.View.Details;

namespace 饥荒百科全书CSharp.View
{
    /// <summary>
    /// CharacterPage.xaml 的交互逻辑
    /// </summary>
    public partial class CharacterPage : Page
    {
        //private int _loadedTime;

        public void LoadCompleted(object sender, NavigationEventArgs e)
        {
            //if (_loadedTime != 0) return;
            //_loadedTime++;
            if (Global.FontFamily != null)
            {
                FontFamily = Global.FontFamily;
            }
            RightScrollViewer.FontWeight = Global.FontWeight;
            var extraData = (string[])e.ExtraData;
            Deserialize();
            // 小图标
            if (Settings.SmallButtonMode)
            {
                UpdateLayout();
                var resultList = new List<Button>();
                Global.FindChildren(resultList, RightScrollViewer);
                foreach (var button in resultList)
                {
                    button.Width = 120;
                    button.Height = 200;
                    ((Grid)button.Content).Margin = new Thickness(0);
                    ((Grid)button.Content).RowDefinitions[0].Height = new GridLength(180);
                    ((Grid)button.Content).Width = 120;
                    ((Grid)button.Content).Height = 200;
                    ((HrlTextBlock)((Grid)button.Content).Children[1]).HrlWidth = 120;
                }
            }
            if (extraData == null) return;
            var suggestBoxItemPicture = extraData[1];
            foreach (var itemsControlItem in Global.CharacterData)
            {
                var character = itemsControlItem;
                if (character == null || character.Picture != suggestBoxItemPicture) continue;
                if(LeftFrame.CanGoBack)LeftFrame.RemoveBackEntry();
                LeftFrame.NavigationService.Navigate(new CharacterDetail(), character);
                break;
            }
        }

        public CharacterPage()
        {
            InitializeComponent();
            Global.CharacterLeftFrame = LeftFrame;
            Global.RightFrame.NavigationService.LoadCompleted += LoadCompleted;
        }
        
        public void Deserialize()
        {
            //foreach (var character in (Global.CharacterData))
            //{
            //    if (character.EnName != "Warbucks") continue;
            //    Global.CharacterData.Remove(character);
            //    break;
            //}
            CharacterItemsControl.DataContext = Global.CharacterData;
            if(LeftFrame.CanGoBack)LeftFrame.RemoveBackEntry();
            LeftFrame.NavigationService.Navigate(new CharacterDetail(), Global.CharacterData[0]);
        }
        
        private void CharacterButton_Click(object sender, RoutedEventArgs e)
        {
            var character = (Character)((Button)sender).DataContext;
            if(LeftFrame.CanGoBack)LeftFrame.RemoveBackEntry();
            LeftFrame.NavigationService.Navigate(new CharacterDetail(), character);
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UiSplitter.Height = ActualHeight;
        }

        private void UIElement_OnMouseEnter(object sender, MouseEventArgs e)
        {
            var button = (Button)sender;
            ((HrlTextBlock)((Grid)button.Content).Children[1]).HrlTextBlock_OnMouseEnter(null, null);
        }

        private void UIElement_OnMouseLeave(object sender, MouseEventArgs e)
        {
            var button = (Button)sender;
            ((HrlTextBlock)((Grid)button.Content).Children[1]).HrlTextBlock_OnMouseLeave(null, null);
        }
    }
}
