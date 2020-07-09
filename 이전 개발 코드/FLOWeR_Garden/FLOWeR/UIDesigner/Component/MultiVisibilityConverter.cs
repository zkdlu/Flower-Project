using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace FLOWeR_Garden_Ver02.FLOWeR.UIDesigner.Component
{
    class MultiVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            object obj = DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue;
            
            if ((bool)obj)
            {
                return Visibility.Visible;
            }

            bool b1 = (bool)values[0];
            bool b2 = false;

            if (values.Length >= 2)
            {
                b2 = (bool)values[1];
            }
            if (values.Length >= 3)
            {
                b2 = b2 && (bool)values[2];
            }

            if (!b2)
            {
                return Visibility.Collapsed;
            }

            return b1 ? Visibility.Visible : Visibility.Collapsed;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
