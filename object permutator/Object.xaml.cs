using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace object_permutator
{
    public sealed partial class Object : UserControl
    {
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(Object), new PropertyMetadata(null));

        public ImageSource src
        {
            get { return (ImageSource)GetValue(srcProperty); }
            set { SetValue(srcProperty, value); }
        }
        public static readonly DependencyProperty srcProperty =
            DependencyProperty.Register("src", typeof(ImageSource), typeof(Object), new PropertyMetadata(null));


        public SolidColorBrush bgColor
        {
            get { return (SolidColorBrush)GetValue(bgColorProperty); }
            set { SetValue(bgColorProperty, value); }
        }
        public static readonly DependencyProperty bgColorProperty =
            DependencyProperty.Register("bgColor", typeof(string), typeof(Object), new PropertyMetadata(null));


        int state = 0;

        public Object()
        {
            this.InitializeComponent();
        }

        public void rotate90()
        {
            state = 90;
            spin90.Begin();
        }
        public void rotate0()
        {
            state = 0;
            spin0.Begin();
        }

        public int spinangle()
        {
            return state;
        }
    }
}
