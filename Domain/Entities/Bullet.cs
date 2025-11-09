using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MonsterDungeon.Domain.Entities
{
    /// <summary>
    /// Represents a bullet projectile fired by the player
    /// </summary>
    public class Bullet : INotifyPropertyChanged
    {
        private int _x;
        private int _y;
        private double _pixelY;

        public int X
        {
            get => _x;
            set
            {
                if (_x != value)
                {
                    _x = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Y
        {
            get => _y;
            set
            {
                if (_y != value)
                {
                    _y = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Precise pixel position for smooth animation (not grid-based)
        /// </summary>
        public double PixelY
        {
            get => _pixelY;
            set
            {
                if (_pixelY != value)
                {
                    _pixelY = value;
                    OnPropertyChanged();

                    // Update grid Y based on pixel position
                    int newGridY = (int)(_pixelY / 100); // 100 pixels per grid cell
                    if (newGridY != _y)
                    {
                        _y = newGridY;
                        OnPropertyChanged(nameof(Y));
                    }
                }
            }
        }

        public int Damage { get; set; } = 10;

        /// <summary>
        /// Velocity in pixels per second
        /// </summary>
        public double VelocityY { get; set; } = -500; // Move upward at 500 pixels/second

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
