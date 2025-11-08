namespace MonsterDungeon.Domain.Entities
{
  /// <summary>
    /// Represents the player character
    /// </summary>
    public class Player
    {
        public int X { get; set; }
        public int Y { get; set; }
     
        public int Health { get; set; }
     public int MaxHealth { get; set; }
  
  public int Attack { get; set; }
    public int Defense { get; set; }
   
public int Level { get; set; }
        public int Experience { get; set; }
     public int Gold { get; set; }
        
        public Inventory Inventory { get; set; }
        
    public Player()
        {
            // Default starting stats
            X = 4; // Center of grid
   Y = 8; // Near bottom
            
      Health = 100;
      MaxHealth = 100;

  Attack = 10;
         Defense = 5;
      
Level = 1;
       Experience = 0;
         Gold = 0;
     
     Inventory = new Inventory();
        }

   public bool IsAlive => Health > 0;

    public void TakeDamage(int damage)
  {
       int actualDamage = System.Math.Max(damage - Defense, 1);
  Health -= actualDamage;
        if (Health < 0) Health = 0;
        }

  public void Heal(int amount)
        {
 Health += amount;
       if (Health > MaxHealth) Health = MaxHealth;
        }

        public void GainExperience(int xp)
        {
 Experience += xp;
      
       // Level up at 100 XP per level
  while (Experience >= Level * 100)
       {
   LevelUp();
 }
   }

   private void LevelUp()
   {
      Level++;
            
      // Increase stats
  MaxHealth += 10;
       Health = MaxHealth;
  Attack += 2;
   Defense += 1;
    }
    }
}
