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

namespace 饥荒百科全书CSharp.View
{
    /// <summary>
    /// CharacterPage.xaml 的交互逻辑
    /// </summary>
    public partial class CharacterPage : Page
    {
        private readonly ObservableCollection<Character> _characterData = new ObservableCollection<Character>();

        public CharacterPage()
        {
            InitializeComponent();
            Deserialize();
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
            LoadData(_characterData[0]);
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        public void LoadData(Character c)
        {
            CharacterImage.Source = new BitmapImage(new Uri(c.Picture, UriKind.Relative));
            CharacterName.Text = c.Name;
            CharacterEnName.Text = c.EnName;
            if (c.Motto != null)
            {
                CharacterMotto.Text = c.Motto;
            }
            else
            {
                CharacterMotto.Visibility = Visibility.Collapsed;
            }
            CharacterHealth.LabelWidth = 30;
            CharacterSanity.LabelWidth = 30;
            CharacterHealth.Value = c.Health;
            CharacterHealth.BarColor = Global.ColorGreen;
            CharacterHunger.Value = c.Hunger;
            CharacterHunger.BarColor = Global.ColorKhaki;
            CharacterSanity.Value = c.Sanity;
            CharacterSanity.BarColor = Global.ColorRed;
            CharacterHunger.Visibility = Visibility.Visible;
            CharacterSanity.Visibility = Visibility.Visible;
            CharacterDayDamage.Visibility = Visibility.Collapsed;
            CharacterDuskDamage.Visibility = Visibility.Collapsed;
            CharacterNightDamage.Visibility = Visibility.Collapsed;
            if (c.Name == "阿比盖尔")
            {
                CharacterHunger.Visibility = Visibility.Collapsed;
                CharacterSanity.Visibility = Visibility.Collapsed;
                CharacterDamage.Visibility = Visibility.Collapsed;
                CharacterHealth.LabelWidth = 75;
                CharacterDayDamage.Visibility = Visibility.Visible;
                CharacterDayDamage.Value = c.DamageDay;
                CharacterDayDamage.BarColor = Global.ColorBlue;
                CharacterDuskDamage.Visibility = Visibility.Visible;
                CharacterDuskDamage.Value = c.DamageDusk;
                CharacterDuskDamage.BarColor = Global.ColorBlue;
                CharacterNightDamage.Visibility = Visibility.Visible;
                CharacterNightDamage.Value = c.DamageNight;
                CharacterNightDamage.BarColor = Global.ColorBlue;
            }
            CharacterLog.Visibility = Visibility.Collapsed;
            if (c.Name == "海獭伍迪")
            {
                CharacterHunger.Visibility = Visibility.Collapsed;
                CharacterDamage.Text = $"伤害：{c.Damage} 点";
                CharacterHealth.LabelWidth = 45;
                CharacterSanity.LabelWidth = 45;
                CharacterLog.Visibility = Visibility.Visible;
                CharacterLog.Value = c.LogMeter;
                CharacterLog.BarColor = Global.ColorPurple;
            }
            else
            {
                CharacterDamage.Text = $"伤害：{c.Damage} 倍";
            }
            CharacterDescription1.Visibility = Visibility.Collapsed;
            CharacterDescription2.Visibility = Visibility.Collapsed;
            CharacterDescription3.Visibility = Visibility.Collapsed;
            if (c.Descriptions != null)
            {
                CharacterDescription1.Text = c.Descriptions[0];
                CharacterDescription1.Visibility = Visibility.Visible;
                if (c.Descriptions.Count >= 2)
                {
                    CharacterDescription2.Text = c.Descriptions[1];
                    CharacterDescription2.Visibility = Visibility.Visible;
                }
                if (c.Descriptions.Count == 3)
                {
                    CharacterDescription3.Text = c.Descriptions[2];
                    CharacterDescription3.Visibility = Visibility.Visible;
                }
            }
            if (c.Unlock != null)
            {
                CharacterUnlockStackPanel.Visibility = Visibility.Visible;
                UnlockTextBlock.Text = c.Unlock;
            }
            CharacterIntroduction.Text = c.Introduce;
        }

        private void CharacterButton_Click(object sender, RoutedEventArgs e)
        {
            var character = (Character)((Button)sender).DataContext;
            LoadData(character);
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UiSplitter.Height = ActualHeight;
        }
    }
}
