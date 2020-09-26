using System;
using System.Globalization;
using System.Windows.Data;

namespace RubikTangle.Helpers
{
    public sealed class SolvePuzzleButtonNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolean)
            {
                return boolean ? "Stop" : "Start solving";
            }
            else
            {
                throw new ArgumentException("SolvePuzzleButtonNameConverter: Value is not bool");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
