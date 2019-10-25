using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using TaskConstructor.EFModels;

namespace SimulationPTSystem.Views
{
    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            byte[] img = null;
            if (!string.IsNullOrEmpty(value.ToString()))
            {
                img = (byte[])value;
            }
            if (img == null)
            {
                return @"H:\DarkField\TaskConstructor\TaskConstructor\Resources\images\确定_0.png";
            }
            return ShowSelectedIMG(img);                //以流的方式显示图片的方法
        }
        //转换器中二进制转化为BitmapImage  datagrid绑定仙石的
        private BitmapImage ShowSelectedIMG(byte[] img)
        {
            if (img != null)
            {
                //img是从数据库中读取出来的字节数组
                System.IO.MemoryStream ms = new System.IO.MemoryStream(img);

                {
                    ms.Seek(0, System.IO.SeekOrigin.Begin);
                    BitmapImage newBitmapImage = new BitmapImage();
                    newBitmapImage.BeginInit();
                    newBitmapImage.StreamSource = ms;
                    newBitmapImage.EndInit();
                    return newBitmapImage;
                }

            }
            return null;

        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

    public class BytesToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            byte[] img = null;
            if (!string.IsNullOrEmpty(value.ToString()))
            {
                img = (byte[])value;
            }
            if (img == null)
            {
                //   return @"H:\DarkField\TaskConstructor\TaskConstructor\Resources\images\确定_0.png";
                return "STRING";
            }
            return System.Text.Encoding.Default.GetString(img);             //以流的方式显示图片的方法
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

    public class PersonTemplateSelector : System.Windows.Controls.DataTemplateSelector
    {

        private DataTemplate _childTemplate = null;

        public DataTemplate ChildTemplate
        {
            get { return _childTemplate; }
            set { _childTemplate = value; }
        }

        private DataTemplate _adultTemplate = null;
        public DataTemplate AdultTemplate
        {
            get { return _adultTemplate; }
            set { _adultTemplate = value; }
        }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is ItemBank)
            {
                //Age < 18岁 -> 孩子， Age > 18 -> 成年  
                // return (item as Question).QuestionGenre == "String" ? _adultTemplate : _childTemplate;
                return (item as ItemBank).ItemGenre == "Picture" ? _childTemplate : _adultTemplate;
            }

            return base.SelectTemplate(item, container);
        }
    }
}
