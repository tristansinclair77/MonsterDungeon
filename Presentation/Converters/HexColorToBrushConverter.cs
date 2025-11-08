using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace MonsterDungeon.Presentation.Converters
{
    /// <summary>
    /// Converts hex color string to SolidColorBrush
    /// Example: "#1a1a2e" -> SolidColorBrush
    /// </summary>
  public class HexColorToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
          if (value is string hexColor && !string.IsNullOrEmpty(hexColor))
    {
        try
     {
              // Parse hex color and create brush
     var color = (Color)ColorConverter.ConvertFromString(hexColor);
         return new SolidColorBrush(color);
   }
     catch
        {
      // Return default dark color if conversion fails
        return new SolidColorBrush(Color.FromRgb(0x1a, 0x1a, 0x2e));
            }
            }
   
    // Default fallback
            return new SolidColorBrush(Color.FromRgb(0x1a, 0x1a, 0x2e));
  }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
     {
   if (value is SolidColorBrush brush)
            {
    var color = brush.Color;
  return $"#{color.R:X2}{color.G:X2}{color.B:X2}";
            }
  
     return "#1a1a2e";
        }
    }
}
