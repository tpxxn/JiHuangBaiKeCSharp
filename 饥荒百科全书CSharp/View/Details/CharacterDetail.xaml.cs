using System;
using System.Collections.Generic;
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
using 饥荒百科全书CSharp.Class;
using 饥荒百科全书CSharp.Class.JsonDeserialize;

namespace 饥荒百科全书CSharp.View.Details
{
    /// <summary>
    /// CharacterDetail.xaml 的交互逻辑
    /// </summary>
    public partial class CharacterDetail : Page
    {
        private int _loadedTime;

        public void LoadCompleted(object sender, NavigationEventArgs e)
        {
            if (e.ExtraData == null || _loadedTime != 0) return;
            _loadedTime++;
            LoadData((Character)e.ExtraData);
            if (Global.FontFamily != null)
            {
                FontFamily = Global.FontFamily;
            }
            CharacterLeftScrollViewer.FontWeight = Global.FontWeight;
        }

        public CharacterDetail()
        {
            InitializeComponent();
            Global.CharacterLeftFrame.NavigationService.LoadCompleted += LoadCompleted;
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
            CharacterHealth.Value = c.Health;
            CharacterHealth.BarColor = Global.ColorGreen;
            CharacterHunger.Value = c.Hunger;
            CharacterHunger.BarColor = Global.ColorKhaki;
            CharacterSanity.Value = c.Sanity;
            CharacterSanity.BarColor = Global.ColorRed;
            if (c.Name == "阿比盖尔")
            {
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
            if (c.Name == "海獭伍迪" || c.Name == "疯猪威尔巴")
            {
                CharacterDamage.Text = $"伤害：{c.Damage} 点";
                CharacterHealth.LabelWidth = 45;
                CharacterHunger.LabelWidth = 45;
                CharacterSanity.LabelWidth = 45;
                CharacterLog.Visibility = Visibility.Visible;
                CharacterLog.Value = c.LogMeter;
                CharacterLog.BarColor = Global.ColorPurple;
            }
            else
            {
                CharacterDamage.Text = $"伤害：{c.Damage} 倍";
            }
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

    }
}
