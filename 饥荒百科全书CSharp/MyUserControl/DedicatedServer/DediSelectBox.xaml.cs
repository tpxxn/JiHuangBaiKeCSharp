using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace 饥荒百科全书CSharp.MyUserControl.DedicatedServer
{
    /// <summary>
    /// DediSelectBox.xaml 的交互逻辑
    /// </summary>
    public partial class DediSelectBox : UserControl
    {
        public DediSelectBox()
        {
            InitializeComponent();
        }

        #region "依赖属性"
        public static readonly DependencyProperty DataListProperty = DependencyProperty.Register("DataList", typeof(List<string>), typeof(DediSelectBox), new PropertyMetadata(new List<string>(), new PropertyChangedCallback(OnDataListChanged)));

        private static void OnDataListChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            int selectItemIndex = ((int)((DediSelectBox)sender).SelectItemIndex);
            if (selectItemIndex == ((List<string>)args.NewValue).Count - 1)
            {
                ((DediSelectBox)sender).UCButtonLeft.Visibility = Visibility.Visible;
                ((DediSelectBox)sender).UCButtonRight.Visibility = Visibility.Collapsed;
            }
            else if (selectItemIndex == 0)
            {
                ((DediSelectBox)sender).UCButtonLeft.Visibility = Visibility.Collapsed;
                ((DediSelectBox)sender).UCButtonRight.Visibility = Visibility.Visible;
            }
            else
            {
                ((DediSelectBox)sender).UCButtonLeft.Visibility = Visibility.Visible;
                ((DediSelectBox)sender).UCButtonRight.Visibility = Visibility.Visible;
            }
            ((DediSelectBox)sender).UCValue.Content = ((List<string>)args.NewValue)[selectItemIndex];
        }

        public List<string> DataList
        {
            get
            {
                return GetValue(DataListProperty) as List<string>;
            }
            set
            {
                SetValue(DataListProperty, value);
            }
        }

        public static readonly DependencyProperty SelectItemIndexProperty = DependencyProperty.Register("SelectItemIndex", typeof(int), typeof(DediSelectBox), new PropertyMetadata(new int(), new PropertyChangedCallback(OnSelectItemIndexChanged)));

        private static void OnSelectItemIndexChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            if ((int)args.NewValue == (((DediSelectBox)sender).DataList.Count - 1))
            {
                ((DediSelectBox)sender).UCButtonLeft.Visibility = Visibility.Visible;
                ((DediSelectBox)sender).UCButtonRight.Visibility = Visibility.Collapsed;
            }
            else if ((int)args.NewValue == 0)
            {
                ((DediSelectBox)sender).UCButtonLeft.Visibility = Visibility.Collapsed;
                ((DediSelectBox)sender).UCButtonRight.Visibility = Visibility.Visible;
            }
            else
            {
                ((DediSelectBox)sender).UCButtonLeft.Visibility = Visibility.Visible;
                ((DediSelectBox)sender).UCButtonRight.Visibility = Visibility.Visible;
            }
            if (((DediSelectBox)sender).DataList.Count != 0)
            {
                ((DediSelectBox)sender).UCValue.Content = (((DediSelectBox)sender).DataList)[(int)args.NewValue];
            }
        }

        public int? SelectItemIndex
        {
            get
            {
                return GetValue(SelectItemIndexProperty) as int?;
            }
            set
            {
                SetValue(SelectItemIndexProperty, value);
            }
        }
        #endregion

        #region "成员方法"
        /// <summary>
        /// 设置SelectBox的数据列表和已选项序号
        /// </summary>
        /// <param name="dataList">数据列表(string[]类型)</param>
        /// <param name="selectItemIndex">已选择序号</param>
        public void Init(string[] dataList, int? selectItemIndex = 0)
        {
            if (dataList.Length != 0)
            {
                DataList = new List<string>(dataList);
            }
            if (selectItemIndex != null && selectItemIndex >= 0)
            {
                SelectItemIndex = selectItemIndex;
            }
        }
        #endregion

        #region "控件事件"
        private void UCButtonLeft_Click(object sender, RoutedEventArgs e)
        {
            SelectItemIndex -= 1;
        }

        private void UCButtonRight_Click(object sender, RoutedEventArgs e)
        {
            SelectItemIndex += 1;
        }
        #endregion
    }
}
