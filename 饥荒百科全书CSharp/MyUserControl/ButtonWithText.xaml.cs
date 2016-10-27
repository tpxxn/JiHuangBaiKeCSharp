using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace 饥荒百科全书CSharp.MyUserControl
{
    /// <summary>
    /// ButtonWithText.xaml 的交互逻辑
    /// </summary>
    public partial class ButtonWithText : UserControl
    {
        public ButtonWithText()
        {
            InitializeComponent();
        }

        #region "TextP属性"
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "TextP",
            typeof(string),
            typeof(ButtonWithText),
            new PropertyMetadata("TextBox", new PropertyChangedCallback(OnTextChanged))
            );

        public string TextP
        {
            get { return (string)GetValue(TextProperty); }

            set { SetValue(TextProperty, value); }
        }

        static void OnTextChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            ButtonWithText source = (ButtonWithText)sender;
            source.UCTextBlock.Text = (string)args.NewValue;
        }
        #endregion

        #region "TextWidthP属性"
        public static readonly DependencyProperty TextWidthProperty = DependencyProperty.Register(
            "TextWidthP",
            typeof(bool),
            typeof(ButtonWithText),
            new PropertyMetadata(false, new PropertyChangedCallback(OnWidthChanged))
            );

        public bool TextWidthP
        {
            get { return (bool)GetValue(TextWidthProperty); }

            set { SetValue(TextWidthProperty, value); }
        }

        static void OnWidthChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            ButtonWithText source = (ButtonWithText)sender;
            if ((bool)args.NewValue == true)
            {
                source.UCTextBlock.Height = 30;
                source.UCTextBlock.Margin = new Thickness(0, 0, 0, 0);
            }
        }
        #endregion

        #region "ImageP属性"
        public static readonly DependencyProperty ImageProperty = DependencyProperty.Register(
            "ImageP",
            typeof(string),
            typeof(ButtonWithText),
            new PropertyMetadata("Image", new PropertyChangedCallback(OnImageChanged))
            );

        public string ImageP
        {
            get { return (string)GetValue(ImageProperty); }

            set { SetValue(ImageProperty, value); }
        }

        static void OnImageChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            ButtonWithText source = (ButtonWithText)sender;
            source.UCImage.Source = ResourceShortName.PictureShortName(ResourceShortName.ShortName((string)args.NewValue));
        }
        #endregion

        #region "Click事件"
        public static readonly RoutedEvent MyButtonClickEvent =
            EventManager.RegisterRoutedEvent(
                "MyButtonClick",
                RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<object>),
                typeof(ButtonWithText)
                );

        public event RoutedPropertyChangedEventHandler<object> MyButtonClick
        {
            add
            {
                AddHandler(MyButtonClickEvent, value);
            }
            remove
            {
                RemoveHandler(MyButtonClickEvent, value);
            }
        }

        public void OnMyButtonClick(object oldValue, object newValue)
        {
            RoutedPropertyChangedEventArgs<object> arg = new RoutedPropertyChangedEventArgs<object>(oldValue, newValue, MyButtonClickEvent);
            RaiseEvent(arg);
        }
        #endregion
    }
}
