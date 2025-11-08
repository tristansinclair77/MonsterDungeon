using MonsterDungeon.Domain.Enums;

namespace MonsterDungeon.Domain.Entities
{
    /// <summary>
 /// Represents an enemy or obstacle
    /// </summary>
    public class Enemy
    {
     public int X { get; set; }
public int Y { get; set; }
        
        public EnemyType Type { get; set; }
        
   public int Health { get; set; }
   public int MaxHealth { get; set; }
  
        public int Attack { get; set; }
   public int Defense { get; set; }
        
        public bool IsBlock { get; set; }
   public bool IsBoss { get; set; }
      
    public int GoldDrop { get; set; }
     public int ExperienceDrop { get; set; }

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
 }
}
