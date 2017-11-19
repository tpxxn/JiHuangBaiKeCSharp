using System.Windows;
using System.Windows.Controls;
using 饥荒百科全书CSharp.Class;

namespace 饥荒百科全书CSharp.MyUserControl
{
    /// <summary>
    /// ExpanderStackpanel 带Stackpanel的Expander
    /// </summary>
    public partial class ExpanderStackpanel : UserControl
    {

        //#region 属性：IsExPanded

        //public bool IsExPanded
        //{
        //    get => (bool)GetValue(IsExPandedProperty);
        //    set => SetValue(IsExPandedProperty, value);
        //}

        //public static readonly DependencyProperty IsExPandedProperty =
        //    DependencyProperty.Register("IsExPanded", typeof(bool), typeof(Expander), new PropertyMetadata(false, OnIsExPandedChanged));

        //private static void OnIsExPandedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    if (e.NewValue == null) return;
        //    var expander = (ExpanderStackpanel)d;
        //    if ((bool)e.NewValue)
        //    {
        //        expander.UcExpander.IsExpanded = true;
        //    }
        //    else
        //    {
        //        expander.UcExpander.IsExpanded = false;
        //    }
        //}
        //#endregion

        //#region 属性：Header

        //public object Header
        //{
        //    get => (object)GetValue(HeaderProperty);
        //    set => SetValue(HeaderProperty, value);
        //}

        //public static readonly DependencyProperty HeaderProperty =
        //    DependencyProperty.Register("Header", typeof(object), typeof(Expander), new PropertyMetadata(string.Empty));

        //#endregion

        //#region 属性：ExpandContent

        //public object ExpandContent
        //{
        //    get => (object)GetValue(ExpandContentProperty);
        //    set => SetValue(ExpandContentProperty, value);
        //}

        //public static readonly DependencyProperty ExpandContentProperty =
        //    DependencyProperty.Register("ExpandContent", typeof(object), typeof(Expander), new PropertyMetadata(null));

        //#endregion


        public ExpanderStackpanel()
        {
            InitializeComponent();
        }
    }
}
