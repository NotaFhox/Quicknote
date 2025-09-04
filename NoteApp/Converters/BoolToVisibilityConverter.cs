using System.Globalization;

namespace NoteApp.Converters
{
    // |---------------------|
    // |                     |
    // | Visibility Converter|
    // |                     |
    // |---------------------|
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                bool invert = parameter?.ToString()?.ToLowerInvariant() == "invert";
                return invert ? !boolValue : boolValue;
            }
            return false;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                bool invert = parameter?.ToString()?.ToLowerInvariant() == "invert";
                return invert ? !boolValue : boolValue;
            }
            return false;
        }
    }
}