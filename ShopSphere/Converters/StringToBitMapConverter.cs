using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Converters
{
    public class StringToBitMapConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {

                if (value is string uri && !string.IsNullOrEmpty(uri))
            {

                try
                {
                    return new Bitmap(uri);
                }
                catch
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }
               

            }

            return null;


          }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
