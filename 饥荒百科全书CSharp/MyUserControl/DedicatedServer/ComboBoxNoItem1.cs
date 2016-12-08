using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace WpfLearn.UserControls
{
    /// <summary>
    /// 
    /// 【2016.12.6】 希望不要出现bug，出现也搞不定了。。
    /// 配合Controls_Themes.xaml样式文件使用
    /// 不知道为什么，想把LeftClickCommand改名字成ButtonClickCommand，可是就是不行。
    /// 其实左右两个按钮，都是调用了LeftClickCommand。
    /// 唯一的判断就是在LeftClick函数，有判断按钮的name值，来做不同的事情。
    /// </summary>
    public class ComboBoxNoItem1 : ComboBox
    {
        #region 静态 构造函数
        static ComboBoxNoItem1()
        {

            //LeftClickCommand
            LeftClickCommand = new RoutedUICommand();
            LeftClickCommandBinding = new CommandBinding(LeftClickCommand);
            LeftClickCommandBinding.Executed += LeftClick;
   
        }
        #endregion

        #region 按钮
        /// <summary>
        /// 按钮是否可用，附加属性
        /// </summary>
        public static readonly DependencyProperty IsLeftEnabledProperty = DependencyProperty.RegisterAttached("IsLeftEnabled"
            , typeof(bool), typeof(ComboBoxNoItem1), new FrameworkPropertyMetadata(false, IsLeftEnabledChanged));

        private static void IsLeftEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
           // Debug.WriteLine("IsLeftEnabledChanged");
            var button = d as DediImageButton;
            if (e.OldValue != e.NewValue && button != null)
            {
               
                //绑定
                if (!button.CommandBindings.Contains(LeftClickCommandBinding))
                {
                  //  Debug.WriteLine("444444");
                    button.CommandBindings.Add(LeftClickCommandBinding);
                }
            }
           
        }
       
        
        // get.set 但是从来就没用过，没有不行
        [AttachedPropertyBrowsableForType(typeof(ComboBoxNoItem1))]
        public static bool GetIsLeftEnabled(DependencyObject d)
        {
            return (bool)d.GetValue(IsLeftEnabledProperty);
        }

        public static void SetIsLeftEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(IsLeftEnabledProperty, value);
        }

        // 点击命令
        public static RoutedUICommand LeftClickCommand { get; private set; }
        /// <summary>
        /// 绑定命令
        /// </summary>
        private static readonly CommandBinding LeftClickCommandBinding;

        private static void LeftClick(object sender, ExecutedRoutedEventArgs e)
        {
            // Debug.WriteLine("左边按钮按下了");
            
            // 空，返回
            if (e.Parameter == null) return;

            ComboBoxNoItem1 comboBoxNoItem1 = (ComboBoxNoItem1)e.Parameter;
            if (comboBoxNoItem1.Items.Count<=0)
            {
                return;
            }

            // 点击左边按钮
            if (((DediImageButton)sender).Name=="imageButtonLeft")
            {
                Debug.WriteLine(comboBoxNoItem1.SelectedIndex);
                // 循环显示
                if (comboBoxNoItem1.SelectedIndex == -1 || comboBoxNoItem1.SelectedIndex == 0)
                {
                    comboBoxNoItem1.SelectedIndex = comboBoxNoItem1.Items.Count - 1;
                    //SetIsLeftEnabled( comboBoxNoItem1, false);
                }
                else
                {
                    comboBoxNoItem1.SelectedIndex -= 1;
                }
            }

            // 点击右边按钮
            if (((DediImageButton)sender).Name == "imageButtonRight")
            {
                // 循环显示
                if (comboBoxNoItem1.SelectedIndex == -1 || comboBoxNoItem1.SelectedIndex == comboBoxNoItem1.Items.Count-1)
                {
                    comboBoxNoItem1.SelectedIndex = 0;
                    //SetIsLeftEnabled( comboBoxNoItem1, false);
                }
                else
                {
                    comboBoxNoItem1.SelectedIndex += 1;
                }
            }


        }

        #endregion

    }
}
