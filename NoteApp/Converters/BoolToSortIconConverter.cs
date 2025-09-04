using System.Globalization;

namespace NoteApp.Converters
{
    // |---------------------|
    // |                     |
    // | String Converter    |
    // |                     |
    // |---------------------|
    public class StringIsNotNullOrEmptyConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is string stringValue)
            {
                return !string.IsNullOrEmpty(stringValue);
            }
            return false;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    // |---------------------|
    // |                     |
    // |   Sort Converter    |
    // |                     |
    // |---------------------|
    public class BoolToSortIconConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool isAscending && parameter is string icons)
            {
                var iconArray = icons.ToCharArray();
                if (iconArray.Length == 2)
                {
                    return isAscending ? iconArray[1].ToString() : iconArray[0].ToString();
                }
            }
            return "↓";
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    // |---------------------|
    // |                     |
    // | Integer Converter   |
    // |                     |
    // |---------------------|
    public class IntToBoolConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is int intValue)
            {
                return intValue > 0;
            }
            return false;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    // |---------------------|
    // |                     |
    // | DateTime Converter  |
    // |                     |
    // |---------------------|
    public class DateTimeToRelativeTimeConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is DateTime dateTime)
            {
                var timeSpan = DateTime.Now - dateTime;
                
                if (timeSpan.TotalDays >= 365)
                    return $"{(int)(timeSpan.TotalDays / 365)}y";
                if (timeSpan.TotalDays >= 30)
                    return $"{(int)(timeSpan.TotalDays / 30)}mo";
                if (timeSpan.TotalDays >= 1)
                    return $"{(int)timeSpan.TotalDays}d";
                if (timeSpan.TotalHours >= 1)
                    return $"{(int)timeSpan.TotalHours}h";
                if (timeSpan.TotalMinutes >= 1)
                    return $"{(int)timeSpan.TotalMinutes}m";
                
                return "now";
            }
            return string.Empty;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}