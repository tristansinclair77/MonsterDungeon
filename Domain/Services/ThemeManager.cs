using System;
using System.Collections.Generic;
using System.ComponentModel;
using MonsterDungeon.Domain.Enums;
using MonsterDungeon.Domain.Models;

namespace MonsterDungeon.Domain.Services
{
    /// <summary>
    /// Manages dungeon themes, affinities, and visual transitions
    /// Based on Dungeon Themes Document
    /// </summary>
    public class ThemeManager : INotifyPropertyChanged
    {
        private DungeonTheme _currentTheme;
        private readonly Dictionary<DungeonTheme, ThemeData> _themeData;
        private UITheme _currentUITheme;
        private readonly MonsterDungeon.Infrastructure.Services.ThemeStorageService _storageService;

        public event PropertyChangedEventHandler PropertyChanged;

        public DungeonTheme CurrentTheme => _currentTheme;

        // Button Colors
        public string ButtonCoreColor => _currentUITheme?.ButtonCoreColor ?? "#e94560";
        public string ButtonTextColor => _currentUITheme?.ButtonTextColor ?? "#ffffff";
        public string ButtonExtrasColor => _currentUITheme?.ButtonExtrasColor ?? "#b36b3d";

        // Screen Background Colors
        public string ScreenBackgroundMainColor => _currentUITheme?.ScreenBackgroundMainColor ?? "#0f0f1e";
        public string ScreenBackgroundSecondaryColor => _currentUITheme?.ScreenBackgroundSecondaryColor ?? "#16213e";

        // Header Colors
        public string HeaderMainColor => _currentUITheme?.HeaderMainColor ?? "#16213e";
        public string HeaderTextColor => _currentUITheme?.HeaderTextColor ?? "#e94560";
        public string HeaderExtrasColor => _currentUITheme?.HeaderExtrasColor ?? "#a0a0a0";

        // Screen Window Colors
        public string WindowBackgroundColor => _currentUITheme?.WindowBackgroundColor ?? "#1a1a2e";
        public string WindowTextColor => _currentUITheme?.WindowTextColor ?? "#ffffff";
        public string WindowSecondaryColor => _currentUITheme?.WindowSecondaryColor ?? "#b36b3d";

        // Text Colors
        public string TextMainHeaderColor => _currentUITheme?.TextMainHeaderColor ?? "#e94560";
        public string TextSecondaryHeaderColor => _currentUITheme?.TextSecondaryHeaderColor ?? "#a0a0a0";
        public string TextBodyColor => _currentUITheme?.TextBodyColor ?? "#ffffff";

        public ThemeManager(MonsterDungeon.Infrastructure.Services.ThemeStorageService storageService)
        {
            _storageService = storageService;
            _themeData = InitializeThemeData();
            _currentTheme = DungeonTheme.Caverns;

            // Load DefaultDark theme from storage
            _currentUITheme = _storageService.LoadTheme("DefaultDark");
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
        /// Apply a UI theme by name
        /// </summary>
        public void ApplyDebugTheme(string themeName)
        {
            // Load theme from storage (includes any customizations)
            _currentUITheme = _storageService.LoadTheme(themeName);

            // Notify all UI color properties have changed
            OnPropertyChanged(nameof(ButtonCoreColor));
            OnPropertyChanged(nameof(ButtonTextColor));
            OnPropertyChanged(nameof(ButtonExtrasColor));

            OnPropertyChanged(nameof(ScreenBackgroundMainColor));
            OnPropertyChanged(nameof(ScreenBackgroundSecondaryColor));

            OnPropertyChanged(nameof(HeaderMainColor));
            OnPropertyChanged(nameof(HeaderTextColor));
            OnPropertyChanged(nameof(HeaderExtrasColor));

            OnPropertyChanged(nameof(WindowBackgroundColor));
            OnPropertyChanged(nameof(WindowTextColor));
            OnPropertyChanged(nameof(WindowSecondaryColor));

            OnPropertyChanged(nameof(TextMainHeaderColor));
            OnPropertyChanged(nameof(TextSecondaryHeaderColor));
            OnPropertyChanged(nameof(TextBodyColor));
        }

        /// <summary>
        /// Update a specific color property and save to storage
        /// </summary>
        public void UpdateThemeColor(string themeName, string propertyName, string colorValue)
        {
            // Get current theme (might be from memory or storage)
            UITheme theme = _currentUITheme.Name == themeName ? _currentUITheme : _storageService.LoadTheme(themeName);

            // Update the property
            switch (propertyName)
            {
                case nameof(UITheme.ButtonCoreColor): theme.ButtonCoreColor = colorValue; break;
                case nameof(UITheme.ButtonTextColor): theme.ButtonTextColor = colorValue; break;
                case nameof(UITheme.ButtonExtrasColor): theme.ButtonExtrasColor = colorValue; break;
                case nameof(UITheme.ScreenBackgroundMainColor): theme.ScreenBackgroundMainColor = colorValue; break;
                case nameof(UITheme.ScreenBackgroundSecondaryColor): theme.ScreenBackgroundSecondaryColor = colorValue; break;
                case nameof(UITheme.HeaderMainColor): theme.HeaderMainColor = colorValue; break;
                case nameof(UITheme.HeaderTextColor): theme.HeaderTextColor = colorValue; break;
                case nameof(UITheme.HeaderExtrasColor): theme.HeaderExtrasColor = colorValue; break;
                case nameof(UITheme.WindowBackgroundColor): theme.WindowBackgroundColor = colorValue; break;
                case nameof(UITheme.WindowTextColor): theme.WindowTextColor = colorValue; break;
                case nameof(UITheme.WindowSecondaryColor): theme.WindowSecondaryColor = colorValue; break;
                case nameof(UITheme.TextMainHeaderColor): theme.TextMainHeaderColor = colorValue; break;
                case nameof(UITheme.TextSecondaryHeaderColor): theme.TextSecondaryHeaderColor = colorValue; break;
                case nameof(UITheme.TextBodyColor): theme.TextBodyColor = colorValue; break;
            }

            // Save to storage
            _storageService.SaveTheme(themeName, theme);

            // If this is the current theme, apply it
            if (_currentUITheme.Name == themeName)
            {
                ApplyDebugTheme(themeName);
            }
        }

        /// <summary>
        /// Reset theme to default settings
        /// </summary>
        public void ResetThemeToDefault(string themeName)
        {
            UITheme defaultTheme = _storageService.ResetThemeToDefault(themeName);

            // If this is the current theme, apply it
            if (_currentUITheme.Name == themeName)
            {
                _currentUITheme = defaultTheme;
                ApplyDebugTheme(themeName);
            }
        }

        /// <summary>
        /// Get the current UITheme object (for binding to color pickers)
        /// </summary>
        public UITheme GetCurrentUITheme()
        {
            return _currentUITheme;
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

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
