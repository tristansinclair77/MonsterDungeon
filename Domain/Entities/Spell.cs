using MonsterDungeon.Domain.Enums;

namespace MonsterDungeon.Domain.Entities
{
 /// <summary>
/// Represents a magic spell
    /// </summary>
  public class Spell
    {
  public string Name { get; set; }
    public string Description { get; set; }
   
   public int ManaCost { get; set; }
public int Damage { get; set; }
     
        public ElementalAffinity Affinity { get; set; }
        public SpellType Type { get; set; }
        
     public int Cooldown { get; set; }
      public int CurrentCooldown { get; set; }

  public bool IsAvailable => CurrentCooldown == 0;

      public void Use()
        {
     CurrentCooldown = Cooldown;
      }

public void ReduceCooldown()
        {
     if (CurrentCooldown > 0)
     CurrentCooldown--;
   }
 }
}
