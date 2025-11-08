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
        private readonly Dictionary<string, UITheme> _uiThemes;
        private UITheme _currentUITheme;

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

        public ThemeManager()
        {
            _themeData = InitializeThemeData();
            _uiThemes = InitializeUIThemes();
            _currentTheme = DungeonTheme.Caverns;

            // Set default theme
            _currentUITheme = _uiThemes["DefaultDark"];
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
        /// Initialize UI theme presets
        /// </summary>
        private Dictionary<string, UITheme> InitializeUIThemes()
        {
            return new Dictionary<string, UITheme>
            {
                {
                    "DefaultDark",
                    new UITheme
                    {
                        Name = "Default Dark",
                        Description = "Dark blue theme with red accents",
                        // Buttons
                        ButtonCoreColor = "#e94560",
                        ButtonTextColor = "#ffffff",
                        ButtonExtrasColor = "#b36b3d",
                        // Screen Background
                        ScreenBackgroundMainColor = "#0f0f1e",
                        ScreenBackgroundSecondaryColor = "#16213e",
                        // Header
                        HeaderMainColor = "#16213e",
                        HeaderTextColor = "#e94560",
                        HeaderExtrasColor = "#a0a0a0",
                        // Windows
                        WindowBackgroundColor = "#1a1a2e",
                        WindowTextColor = "#ffffff",
                        WindowSecondaryColor = "#b36b3d",
                        // Text
                        TextMainHeaderColor = "#e94560",
                        TextSecondaryHeaderColor = "#a0a0a0",
                        TextBodyColor = "#ffffff"
                    }
                },
                {
                    "DefaultLight",
                    new UITheme
                    {
                        Name = "Default Light",
                        Description = "Light theme with dark text",
                        // Buttons
                        ButtonCoreColor = "#d62828",
                        ButtonTextColor = "#000000",
                        ButtonExtrasColor = "#b36b3d",
                        // Screen Background
                        ScreenBackgroundMainColor = "#f8f9fa",
                        ScreenBackgroundSecondaryColor = "#e9ecef",
                        // Header
                        HeaderMainColor = "#e9ecef",
                        HeaderTextColor = "#d62828",
                        HeaderExtrasColor = "#6c757d",
                        // Windows
                        WindowBackgroundColor = "#ffffff",
                        WindowTextColor = "#000000",
                        WindowSecondaryColor = "#6c757d",
                        // Text
                        TextMainHeaderColor = "#d62828",
                        TextSecondaryHeaderColor = "#6c757d",
                        TextBodyColor = "#000000"
                    }
                },
                {
                    "Crimson",
                    new UITheme
                    {
                        Name = "Crimson",
                        Description = "Intense red theme",
                        // Buttons
                        ButtonCoreColor = "#d62828",
                        ButtonTextColor = "#ffffff",
                        ButtonExtrasColor = "#9d0208",
                        // Screen Background
                        ScreenBackgroundMainColor = "#370617",
                        ScreenBackgroundSecondaryColor = "#6a040f",
                        // Header
                        HeaderMainColor = "#6a040f",
                        HeaderTextColor = "#f48c06",
                        HeaderExtrasColor = "#dc2f02",
                        // Windows
                        WindowBackgroundColor = "#6a040f",
                        WindowTextColor = "#ffffff",
                        WindowSecondaryColor = "#9d0208",
                        // Text
                        TextMainHeaderColor = "#f48c06",
                        TextSecondaryHeaderColor = "#dc2f02",
                        TextBodyColor = "#ffffff"
                    }
                },
                {
                    "Emerald",
                    new UITheme
                    {
                        Name = "Emerald",
                        Description = "Forest green theme",
                        // Buttons
                        ButtonCoreColor = "#40916c",
                        ButtonTextColor = "#ffffff",
                        ButtonExtrasColor = "#1b4332",
                        // Screen Background
                        ScreenBackgroundMainColor = "#081c15",
                        ScreenBackgroundSecondaryColor = "#1b4332",
                        // Header
                        HeaderMainColor = "#1b4332",
                        HeaderTextColor = "#52b788",
                        HeaderExtrasColor = "#95d5b2",
                        // Windows
                        WindowBackgroundColor = "#1b4332",
                        WindowTextColor = "#ffffff",
                        WindowSecondaryColor = "#2d6a4f",
                        // Text
                        TextMainHeaderColor = "#52b788",
                        TextSecondaryHeaderColor = "#95d5b2",
                        TextBodyColor = "#d8f3dc"
                    }
                },
                {
                    "Azure",
                    new UITheme
                    {
                        Name = "Azure",
                        Description = "Ocean blue theme",
                        // Buttons
                        ButtonCoreColor = "#0096c7",
                        ButtonTextColor = "#ffffff",
                        ButtonExtrasColor = "#023e8a",
                        // Screen Background
                        ScreenBackgroundMainColor = "#03045e",
                        ScreenBackgroundSecondaryColor = "#023e8a",
                        // Header
                        HeaderMainColor = "#023e8a",
                        HeaderTextColor = "#00b4d8",
                        HeaderExtrasColor = "#90e0ef",
                        // Windows
                        WindowBackgroundColor = "#023e8a",
                        WindowTextColor = "#ffffff",
                        WindowSecondaryColor = "#0077b6",
                        // Text
                        TextMainHeaderColor = "#00b4d8",
                        TextSecondaryHeaderColor = "#90e0ef",
                        TextBodyColor = "#caf0f8"
                    }
                },
                {
                    "Amber",
                    new UITheme
                    {
                        Name = "Amber",
                        Description = "Warm orange theme",
                        // Buttons
                        ButtonCoreColor = "#f77f00",
                        ButtonTextColor = "#000000",
                        ButtonExtrasColor = "#d62828",
                        // Screen Background
                        ScreenBackgroundMainColor = "#370617",
                        ScreenBackgroundSecondaryColor = "#6a040f",
                        // Header
                        HeaderMainColor = "#6a040f",
                        HeaderTextColor = "#fcbf49",
                        HeaderExtrasColor = "#f77f00",
                        // Windows
                        WindowBackgroundColor = "#6a040f",
                        WindowTextColor = "#ffffff",
                        WindowSecondaryColor = "#dc2f02",
                        // Text
                        TextMainHeaderColor = "#fcbf49",
                        TextSecondaryHeaderColor = "#f77f00",
                        TextBodyColor = "#ffffff"
                    }
                },
                {
                    "Amethyst",
                    new UITheme
                    {
                        Name = "Amethyst",
                        Description = "Royal purple theme",
                        // Buttons
                        ButtonCoreColor = "#7209b7",
                        ButtonTextColor = "#ffffff",
                        ButtonExtrasColor = "#560bad",
                        // Screen Background
                        ScreenBackgroundMainColor = "#10002b",
                        ScreenBackgroundSecondaryColor = "#3c096c",
                        // Header
                        HeaderMainColor = "#3c096c",
                        HeaderTextColor = "#c77dff",
                        HeaderExtrasColor = "#e0aaff",
                        // Windows
                        WindowBackgroundColor = "#3c096c",
                        WindowTextColor = "#ffffff",
                        WindowSecondaryColor = "#5a189a",
                        // Text
                        TextMainHeaderColor = "#c77dff",
                        TextSecondaryHeaderColor = "#e0aaff",
                        TextBodyColor = "#ffffff"
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
        /// Get current UI theme
        /// </summary>
        public UITheme GetCurrentUITheme()
        {
            return _currentUITheme;
        }

        /// <summary>
        /// Set current UI theme
        /// </summary>
        public void SetCurrentUITheme(string themeKey)
        {
            if (_uiThemes.ContainsKey(themeKey))
            {
                _currentUITheme = _uiThemes[themeKey];
            }
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

        /// <summary>
        /// Apply a UI theme by name
        /// </summary>
        public void ApplyDebugTheme(string themeName)
        {
            if (_uiThemes.ContainsKey(themeName))
            {
                _currentUITheme = _uiThemes[themeName];

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
