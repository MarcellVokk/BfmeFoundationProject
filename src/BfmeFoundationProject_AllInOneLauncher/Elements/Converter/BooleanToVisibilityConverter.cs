using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace BfmeFoundationProject.AllInOneLauncher.Elements.Converter
{
    /// <summary>
    /// Converts a boolean to Visibility.
    /// Supports optional inversion via ConverterParameter="invert" or ConverterParameter="!" (case-insensitive).
    /// Also supports hiding mode via ConverterParameter="hidden" to return Visibility.Hidden instead of Collapsed when false.
    /// You can combine parameters with a comma: "invert,hidden".
    /// </summary>
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isTrue = false;

            if (value is bool valueBool)
                isTrue = valueBool;
            else if (value is bool?)
                isTrue = (bool?)value ?? false;

            bool invert = false;
            bool useHidden = false;

            if (parameter is string paramString && !string.IsNullOrWhiteSpace(paramString))
            {
                var parts = paramString.Split([',', ';'], StringSplitOptions.RemoveEmptyEntries);
                foreach (var p in parts)
                {
                    var tp = p.Trim().ToLowerInvariant();
                    if (tp == "invert" || tp == "!" )
                        invert = true;
                    if (tp == "hidden")
                        useHidden = true;
                }
            }

            if (invert)
                isTrue = !isTrue;

            if (isTrue)
                return Visibility.Visible;

            return useHidden ? Visibility.Hidden : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility v)
                return v == Visibility.Visible;

            return DependencyProperty.UnsetValue;
        }
    }
}
