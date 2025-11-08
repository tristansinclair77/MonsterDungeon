using System;
using System.Windows.Media;

namespace MonsterDungeon.Presentation.Helpers
{
    public static class ColorPickerHelper
    {
        public static Color HexToColor(string hex)
        {
            if (string.IsNullOrEmpty(hex))
  return Colors.Black;
          try
    {
                return (Color)ColorConverter.ConvertFromString(hex);
     }
        catch
            {
                return Colors.Black;
      }
        }

        public static string ColorToHex(Color color)
        {
            return string.Format("#{0:X2}{1:X2}{2:X2}", color.R, color.G, color.B);
        }
    }
}

