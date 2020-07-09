﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace FLOWeR_Garden_Ver02.FLOWeR.UIDesigner.Component
{
    class VisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool b = (bool)value;

            return b ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
