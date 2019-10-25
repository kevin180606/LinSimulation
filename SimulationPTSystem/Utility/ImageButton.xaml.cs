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

namespace LinnerWPFTools
{
    /// <summary>
    /// ImageButton.xaml 的交互逻辑
    /// </summary>
    public partial class ImageButton : Button
    {
        public ImageButton()
        {
            InitializeComponent();
            this.FocusVisualStyle = null;
        }

        public static DependencyProperty NormalImageProperty =
            DependencyProperty.Register("NormalImage", typeof(ImageSource), typeof(ImageButton), new FrameworkPropertyMetadata(null));
        public ImageSource NormalImage
        {
            get
            {
                return (ImageSource)this.GetValue(NormalImageProperty);
            }
            set
            {
                this.SetValue(NormalImageProperty, value);
            }
        }

        public static DependencyProperty PressedImageProperty =
            DependencyProperty.Register("PressedImage", typeof(ImageSource), typeof(ImageButton), new FrameworkPropertyMetadata(null));
        public ImageSource PressedImage
        {
            get
            {
                return (ImageSource)this.GetValue(PressedImageProperty);
            }
            set
            {
                this.SetValue(PressedImageProperty, value);
            }
        }

        public static DependencyProperty DisabledImageProperty =
            DependencyProperty.Register("DisabledImage", typeof(ImageSource), typeof(ImageButton), new FrameworkPropertyMetadata(null));
        public ImageSource DisabledImage
        {
            get
            {
                return (ImageSource)this.GetValue(DisabledImageProperty);
            }
            set
            {
                this.SetValue(DisabledImageProperty, value);
            }
        }

    }
}
