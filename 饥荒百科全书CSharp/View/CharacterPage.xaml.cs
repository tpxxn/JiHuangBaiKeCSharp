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
using 饥荒百科全书CSharp.View.Details;

namespace 饥荒百科全书CSharp.View
{
    /// <summary>
    /// CharacterPage.xaml 的交互逻辑
    /// </summary>
    public partial class CharacterPage : Page
    {
        private readonly ObservableCollection<Character> _characterData = new ObservableCollection<Character>();

        private int _loadedTime;

        public void LoadCompleted(object sender, NavigationEventArgs e)
        {
            if (_loadedTime != 0) return;
            _loadedTime++;
            if (Global.FontFamily != null)
            {
                FontFamily = Global.FontFamily;
            }
            var extraData = (string[])e.ExtraData;
            Deserialize();
            if (extraData == null) return;
            var suggestBoxItemPicture = extraData[1];
            foreach (var itemsControlItem in _characterData)
            {
                var character = itemsControlItem;
                if (character == null || character.Picture != suggestBoxItemPicture) continue;
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
            _characterData.Clear();
            var character = JsonConvert.DeserializeObject<CharacterRootObject>(StringProcess.GetJsonString("Characters.json"));
            foreach (var characterItems in character.Character)
            {
                _characterData.Add(characterItems);
            }
            foreach (var characterItems in _characterData)
            {
                characterItems.Picture = StringProcess.GetGameResourcePath(characterItems.Picture);
            }
            CharacterItemsControl.DataContext = _characterData;
            LeftFrame.NavigationService.Navigate(new CharacterDetail(), _characterData[0]);
        }
        
        private void CharacterButton_Click(object sender, RoutedEventArgs e)
        {
            var character = (Character)((Button)sender).DataContext;
            LeftFrame.NavigationService.Navigate(new CharacterDetail(), character);
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UiSplitter.Height = ActualHeight;
        }
    }
}
