using System;
using System.Collections.Generic;
using MonsterDungeon.Domain.Enums;

namespace MonsterDungeon.Domain.Services
{
    /// <summary>
    /// Manages dungeon themes, affinities, and visual transitions
    /// Based on Dungeon Themes Document
    /// </summary>
    public class ThemeManager
    {
        private DungeonTheme _currentTheme;
        private readonly Dictionary<DungeonTheme, ThemeData> _themeData;

        public DungeonTheme CurrentTheme => _currentTheme;

        public ThemeManager()
        {
       _themeData = InitializeThemeData();
      _currentTheme = DungeonTheme.Caverns; // Start with Caverns
        }

   /// <summary>
        /// Initialize theme data with properties and modifiers
        /// </summary>
        private Dictionary<DungeonTheme, ThemeData> InitializeThemeData()
        {
 return new Dictionary<DungeonTheme, ThemeData>
            {
         {
                 DungeonTheme.Caverns,
   new ThemeData
         {
        Name = "Caverns",
  PrimaryColor = "#4a4e69",
        SecondaryColor = "#9a8c98",
      Affinity = ElementalAffinity.Earth,
            DifficultyModifier = 1.0,
           Description = "Dark stone caverns with earthen enemies"
    }
        },
    {
        DungeonTheme.Crypt,
        new ThemeData
       {
         Name = "Crypt",
  PrimaryColor = "#2b2d42",
     SecondaryColor = "#8d99ae",
             Affinity = ElementalAffinity.Dark,
  DifficultyModifier = 1.2,
     Description = "Ancient tombs filled with undead"
 }
         },
{
          DungeonTheme.Volcano,
        new ThemeData
          {
               Name = "Volcano",
       PrimaryColor = "#d62828",
      SecondaryColor = "#f77f00",
             Affinity = ElementalAffinity.Fire,
     DifficultyModifier = 1.5,
           Description = "Molten caves with fire-based enemies"
                  }
                },
          {
         DungeonTheme.IceCavern,
   new ThemeData
 {
          Name = "Ice Cavern",
        PrimaryColor = "#0077b6",
         SecondaryColor = "#90e0ef",
     Affinity = ElementalAffinity.Ice,
        DifficultyModifier = 1.4,
           Description = "Frozen passages with ice enemies"
     }
       }
            };
 }

        /// <summary>
        /// Change to a new theme
        /// </summary>
    public void ChangeTheme(DungeonTheme newTheme)
     {
            _currentTheme = newTheme;
     }

        /// <summary>
        /// Get current theme data
        /// </summary>
        public ThemeData GetCurrentThemeData()
        {
            return _themeData[_currentTheme];
        }

     /// <summary>
    /// Get theme-specific enemy spawn modifier
        /// </summary>
        public double GetThemeSpawnModifier()
        {
            return _themeData[_currentTheme].DifficultyModifier;
        }

    /// <summary>
        /// Check if spell has affinity bonus in current theme
        /// </summary>
        public bool HasAffinityBonus(ElementalAffinity spellAffinity)
        {
    return _themeData[_currentTheme].Affinity == spellAffinity;
      }

     /// <summary>
/// Calculate affinity damage multiplier
        /// </summary>
   public double GetAffinityMultiplier(ElementalAffinity spellAffinity)
        {
   if (HasAffinityBonus(spellAffinity))
  return 1.5; // 50% bonus for matching affinity

 // Check for opposing affinities
   if (IsOpposingAffinity(spellAffinity, _themeData[_currentTheme].Affinity))
     return 0.5; // 50% penalty for opposing affinity

            return 1.0; // Neutral
     }

        /// <summary>
        /// Check if two affinities oppose each other
      /// </summary>
        private bool IsOpposingAffinity(ElementalAffinity a, ElementalAffinity b)
        {
     return (a == ElementalAffinity.Fire && b == ElementalAffinity.Ice) ||
           (a == ElementalAffinity.Ice && b == ElementalAffinity.Fire) ||
     (a == ElementalAffinity.Light && b == ElementalAffinity.Dark) ||
         (a == ElementalAffinity.Dark && b == ElementalAffinity.Light);
        }
    }

    /// <summary>
    /// Data structure for theme information
    /// </summary>
    public class ThemeData
    {
        public string Name { get; set; }
        public string PrimaryColor { get; set; }
        public string SecondaryColor { get; set; }
        public ElementalAffinity Affinity { get; set; }
        public double DifficultyModifier { get; set; }
        public string Description { get; set; }
    }
}
