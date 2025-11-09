using System;
using System.Globalization;
using System.Windows.Data;

namespace MonsterDungeon.Presentation.Converters
{
    /// <summary>
    /// Converts pixel Y coordinate directly (for smooth bullet animation)
    /// No conversion needed - just pass through the pixel value
    /// </summary>
    public class PixelYConverter : IValueConverter
    {
      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
    if (value is double pixelY)
   {
     // Bullets use precise pixel positions for smooth movement
       // Offset by -100 to shift into visible grid (same as GridToPixelConverter)
  return pixelY - 100;
            }

            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
 throw new NotImplementedException();
   }
    }
}
