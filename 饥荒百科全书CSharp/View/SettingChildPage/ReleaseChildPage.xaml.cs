using System;
using System.IO;
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

namespace 饥荒百科全书CSharp.View.SettingChildPage
{
    /// <summary>
    /// ReleaseChildPage.xaml 的交互逻辑
    /// </summary>
    public partial class ReleaseChildPage : Page
    {
        public void LoadCompleted(object sender, NavigationEventArgs e)
        {
            if (Global.FontFamily != null)
            {
                FontFamily = Global.FontFamily;
            }
            RootScrollViewer.FontWeight = Global.FontWeight;
        }

        public ReleaseChildPage()
        {
            InitializeComponent();
            Global.SettingRootFrame.NavigationService.LoadCompleted += LoadCompleted;
            Deserialize();
        }

        public void Deserialize()
        {
            var releaseData = new Collection<Release>();
            var src2 = Application.GetResourceStream(new Uri("/饥荒百科全书CSharp;component/Json/Release.json", UriKind.RelativeOrAbsolute))?.Stream;
            var str = new StreamReader(src2 ?? throw new InvalidOperationException(), Encoding.UTF8).ReadToEnd();
            var release = JsonConvert.DeserializeObject<ReleaseRootObject>(str);
            foreach (var releaseItems in release.Release)
            {
                releaseData.Add(releaseItems);
            }
            for (var i = 0; i < releaseData.Count; i++)
            {
                //添加版本号和发布时间
                var rootGrid = new Grid();
                rootGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                rootGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                rootGrid.Margin = i == 0 ? new Thickness(0) : new Thickness(0, 50, 0, 0);
                rootGrid.Children.Add(new Border
                {
                    Width = 75,
                    Background = new SolidColorBrush(Color.FromArgb(255, 111, 66, 193)),
                    CornerRadius = new CornerRadius(5),
                    Child = new TextBlock
                    {
                        FontSize = 23,
                        Text = releaseData[i].Version,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    }
                });
                var dataTextBlock = new TextBlock
                {
                    FontSize = 23,
                    Text = releaseData[i].Data,
                    Margin = new Thickness(15, 0, 0, 0)
                };
                rootGrid.Children.Add(dataTextBlock);
                Grid.SetColumn(dataTextBlock, 1);
                ReleaseStackPanel.Children.Add(rootGrid);
                //添加更新内容
                foreach (var updataContent in releaseData[i].UpdataContent)
                {
                    var updateContentRootGrid = new Grid { Margin = new Thickness(75, 15, 0, 0) };
                    updateContentRootGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                    updateContentRootGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                    var solidColorBrush = updataContent.Label == "新增" ? new SolidColorBrush(Color.FromArgb(255, 40, 167, 69)) : new SolidColorBrush(Color.FromArgb(255, 3, 102, 214));
                    updateContentRootGrid.Children.Add(new Border
                    {
                        Width = 55,
                        Background = solidColorBrush,
                        CornerRadius = new CornerRadius(5),
                        Child = new TextBlock
                        {
                            FontSize = 17,
                            Text = updataContent.Label,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center
                        }
                    });
                    var updataContentDataTextBlock = new TextBlock
                    {
                        FontSize = 17,
                        Text = updataContent.Content,
                        Margin = new Thickness(15, 0, 0, 0)
                    };
                    updateContentRootGrid.Children.Add(updataContentDataTextBlock);
                    Grid.SetColumn(updataContentDataTextBlock, 1);
                    ReleaseStackPanel.Children.Add(updateContentRootGrid);
                }
            }
        }
    }
}
