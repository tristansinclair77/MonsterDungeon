using System;
using System.Globalization;
using System.Windows.Data;

namespace MonsterDungeon.Presentation.Converters
{
    /// <summary>
    /// Converts grid coordinates (0-7 for X, 0-9 for Y) to pixel coordinates
    /// Each grid cell is 100 pixels
    /// Subtracts 100 from Y to shift enemies up by one row
    /// </summary>
    public class GridToPixelConverter : IValueConverter
    {
        private const int TileSize = 100;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
   if (value is int gridCoordinate)
            {
              // Convert grid coordinate to pixel coordinate and shift up by one row for Y
            // Check if this is being used for Y coordinate (Canvas.Top) by checking parameter
    string param = parameter as string;
            if (param == "Y")
                {
   // Shift Y coordinates up by 100 pixels (one row)
         return (gridCoordinate * TileSize) - TileSize;
    }
       
            return gridCoordinate * TileSize;
   }
   
    return 0;
        }

   public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
 throw new NotImplementedException();
}
    }
}
