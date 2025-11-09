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

        public int Damage { get; set; } = 10;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
      {
   PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
