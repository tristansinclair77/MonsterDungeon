using MonsterDungeon.Domain.Enums;

namespace MonsterDungeon.Domain.Entities
{
    /// <summary>
    /// Represents an item (weapon, armor, consumable, etc.)
    /// </summary>
public class Item
    {
        public string Name { get; set; }
   public string Description { get; set; }
        
  public ItemType Type { get; set; }
   public ItemRarity Rarity { get; set; }
   
   public int AttackBonus { get; set; }
  public int DefenseBonus { get; set; }
   public int HealthBonus { get; set; }
        
public int Value { get; set; }
  public bool IsConsumable { get; set; }
   
   public Item()
{
 AttackBonus = 0;
   DefenseBonus = 0;
   HealthBonus = 0;
  Value = 0;
      IsConsumable = false;
   }
    }
}
