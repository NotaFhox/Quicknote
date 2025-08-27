using System.Globalization;

namespace NoteApp.Converters
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                // Check if parameter requests inversion
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