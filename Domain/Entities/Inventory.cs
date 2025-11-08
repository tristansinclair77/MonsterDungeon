using System.Collections.Generic;
using System.Linq;
using MonsterDungeon.Domain.Enums;

namespace MonsterDungeon.Domain.Entities
{
    /// <summary>
    /// Player's inventory system
 /// </summary>
    public class Inventory
    {
   private readonly List<Item> _items;
    public const int MaxCapacity = 20;

public IReadOnlyList<Item> Items => _items.AsReadOnly();
   public int Count => _items.Count;
  public bool IsFull => _items.Count >= MaxCapacity;

      public Item EquippedWeapon { get; set; }
public Item EquippedArmor { get; set; }

        public Inventory()
   {
  _items = new List<Item>();
        }

 public bool AddItem(Item item)
        {
 if (IsFull)
     return false;

   _items.Add(item);
     return true;
        }

   public bool RemoveItem(Item item)
    {
   return _items.Remove(item);
  }

        public Item GetItem(int index)
  {
 if (index < 0 || index >= _items.Count)
  return null;

       return _items[index];
   }

 public void EquipWeapon(Item weapon)
        {
   if (weapon.Type == ItemType.Weapon)
        {
   EquippedWeapon = weapon;
     }
        }

  public void EquipArmor(Item armor)
   {
       if (armor.Type == ItemType.Armor)
 {
           EquippedArmor = armor;
      }
     }

        public int GetTotalAttackBonus()
  {
   int total = 0;
  if (EquippedWeapon != null)
    total += EquippedWeapon.AttackBonus;
 return total;
        }

 public int GetTotalDefenseBonus()
        {
     int total = 0;
if (EquippedArmor != null)
total += EquippedArmor.DefenseBonus;
return total;
    }

 public List<Item> GetConsumables()
    {
            return _items.Where(i => i.IsConsumable).ToList();
        }
    }
}
