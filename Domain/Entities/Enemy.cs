using MonsterDungeon.Domain.Enums;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MonsterDungeon.Domain.Entities
{
    /// <summary>
 /// Represents an enemy or obstacle
    /// </summary>
    public class Enemy : INotifyPropertyChanged
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
        
        public EnemyType Type { get; set; }
        
        public int Health { get; set; }
        public int MaxHealth { get; set; }
  
        public int Attack { get; set; }
        public int Defense { get; set; }
        
        public bool IsBlock { get; set; }
   public bool IsBoss { get; set; }
      
        public int GoldDrop { get; set; }
        public int ExperienceDrop { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;

     public Enemy()
        {
        MaxHealth = Health;
      GoldDrop = 10;
 ExperienceDrop = 25;
        }

        public bool IsAlive => Health > 0;

     public void TakeDamage(int damage)
        {
          int actualDamage = System.Math.Max(damage - Defense, 1);
            Health -= actualDamage;
            if (Health < 0) Health = 0;
        }

        public int GetAttackDamage()
        {
  // Add some randomness to attacks
         var random = new System.Random();
int variance = random.Next(-2, 3);
      return Attack + variance;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
  {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
