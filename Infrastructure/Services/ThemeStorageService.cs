using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using MonsterDungeon.Domain.Models;

namespace MonsterDungeon.Infrastructure.Services
{
  /// <summary>
    /// Manages loading, saving, and resetting theme configurations from JSON files
    /// </summary>
  public class ThemeStorageService
  {
        private readonly string _themesDirectory;
  private readonly Dictionary<string, UITheme> _defaultThemes;
 private readonly JsonSerializerOptions _jsonOptions;

        public ThemeStorageService()
        {
    // Store themes in AppData/MonsterDungeon/Themes/
   string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
       _themesDirectory = Path.Combine(appDataPath, "MonsterDungeon", "Themes");
          
 // Create directory if it doesn't exist
            Directory.CreateDirectory(_themesDirectory);

   _jsonOptions = new JsonSerializerOptions
      {
    WriteIndented = true,
     PropertyNamingPolicy = JsonNamingPolicy.CamelCase
   };

          _defaultThemes = InitializeDefaultThemes();
     }

        /// <summary>
        /// Load theme from JSON file, or return default if file doesn't exist
     /// </summary>
        public UITheme LoadTheme(string themeName)
  {
        string filePath = GetThemeFilePath(themeName);

   if (File.Exists(filePath))
         {
       try
          {
     string json = File.ReadAllText(filePath);
     return JsonSerializer.Deserialize<UITheme>(json, _jsonOptions);
 }
          catch
          {
   // If load fails, return default
      return GetDefaultTheme(themeName);
    }
   }

          // File doesn't exist, create it with defaults
    UITheme defaultTheme = GetDefaultTheme(themeName);
 SaveTheme(themeName, defaultTheme);
   return defaultTheme;
  }

        /// <summary>
        /// Save theme to JSON file
        /// </summary>
     public void SaveTheme(string themeName, UITheme theme)
        {
       string filePath = GetThemeFilePath(themeName);
   string json = JsonSerializer.Serialize(theme, _jsonOptions);
File.WriteAllText(filePath, json);
        }

        /// <summary>
        /// Reset theme to default settings
    /// </summary>
        public UITheme ResetThemeToDefault(string themeName)
        {
         UITheme defaultTheme = GetDefaultTheme(themeName);
      SaveTheme(themeName, defaultTheme);
            return defaultTheme;
   }

        /// <summary>
 /// Get default theme configuration
        /// </summary>
        private UITheme GetDefaultTheme(string themeName)
     {
       if (_defaultThemes.ContainsKey(themeName))
       {
// Return a clone so the default isn't modified
     return CloneTheme(_defaultThemes[themeName]);
      }

            // Fallback to DefaultDark if theme not found
            return CloneTheme(_defaultThemes["DefaultDark"]);
        }

        /// <summary>
        /// Clone a theme object
        /// </summary>
        private UITheme CloneTheme(UITheme source)
        {
   return new UITheme
   {
                Name = source.Name,
       Description = source.Description,
      ButtonCoreColor = source.ButtonCoreColor,
 ButtonTextColor = source.ButtonTextColor,
    ButtonExtrasColor = source.ButtonExtrasColor,
  ScreenBackgroundMainColor = source.ScreenBackgroundMainColor,
    ScreenBackgroundSecondaryColor = source.ScreenBackgroundSecondaryColor,
       HeaderMainColor = source.HeaderMainColor,
         HeaderTextColor = source.HeaderTextColor,
     HeaderExtrasColor = source.HeaderExtrasColor,
    WindowBackgroundColor = source.WindowBackgroundColor,
   WindowTextColor = source.WindowTextColor,
      WindowSecondaryColor = source.WindowSecondaryColor,
      TextMainHeaderColor = source.TextMainHeaderColor,
         TextSecondaryHeaderColor = source.TextSecondaryHeaderColor,
             TextBodyColor = source.TextBodyColor
            };
      }

        private string GetThemeFilePath(string themeName)
        {
   return Path.Combine(_themesDirectory, $"{themeName}.json");
        }

        /// <summary>
        /// Initialize default theme configurations
        /// </summary>
      private Dictionary<string, UITheme> InitializeDefaultThemes()
     {
   return new Dictionary<string, UITheme>
       {
       {
     "DefaultDark",
        new UITheme
       {
     Name = "Default Dark",
       Description = "Dark blue theme with red accents",
          ButtonCoreColor = "#e94560",
     ButtonTextColor = "#ffffff",
        ButtonExtrasColor = "#b36b3d",
     ScreenBackgroundMainColor = "#0f0f1e",
        ScreenBackgroundSecondaryColor = "#16213e",
             HeaderMainColor = "#16213e",
HeaderTextColor = "#e94560",
   HeaderExtrasColor = "#a0a0a0",
     WindowBackgroundColor = "#1a1a2e",
                 WindowTextColor = "#ffffff",
            WindowSecondaryColor = "#b36b3d",
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
  ButtonCoreColor = "#d62828",
    ButtonTextColor = "#000000",
   ButtonExtrasColor = "#b36b3d",
              ScreenBackgroundMainColor = "#f8f9fa",
     ScreenBackgroundSecondaryColor = "#e9ecef",
          HeaderMainColor = "#e9ecef",
      HeaderTextColor = "#d62828",
     HeaderExtrasColor = "#6c757d",
    WindowBackgroundColor = "#ffffff",
                   WindowTextColor = "#000000",
   WindowSecondaryColor = "#6c757d",
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
       ButtonCoreColor = "#d62828",
         ButtonTextColor = "#ffffff",
      ButtonExtrasColor = "#9d0208",
ScreenBackgroundMainColor = "#370617",
     ScreenBackgroundSecondaryColor = "#6a040f",
   HeaderMainColor = "#6a040f",
           HeaderTextColor = "#f48c06",
         HeaderExtrasColor = "#dc2f02",
            WindowBackgroundColor = "#6a040f",
         WindowTextColor = "#ffffff",
            WindowSecondaryColor = "#9d0208",
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
   ButtonCoreColor = "#40916c",
   ButtonTextColor = "#ffffff",
 ButtonExtrasColor = "#1b4332",
               ScreenBackgroundMainColor = "#081c15",
  ScreenBackgroundSecondaryColor = "#1b4332",
HeaderMainColor = "#1b4332",
  HeaderTextColor = "#52b788",
                HeaderExtrasColor = "#95d5b2",
            WindowBackgroundColor = "#1b4332",
              WindowTextColor = "#ffffff",
         WindowSecondaryColor = "#2d6a4f",
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
          ButtonCoreColor = "#0096c7",
        ButtonTextColor = "#ffffff",
    ButtonExtrasColor = "#023e8a",
   ScreenBackgroundMainColor = "#03045e",
       ScreenBackgroundSecondaryColor = "#023e8a",
      HeaderMainColor = "#023e8a",
  HeaderTextColor = "#00b4d8",
               HeaderExtrasColor = "#90e0ef",
    WindowBackgroundColor = "#023e8a",
        WindowTextColor = "#ffffff",
         WindowSecondaryColor = "#0077b6",
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
      ButtonCoreColor = "#f77f00",
          ButtonTextColor = "#000000",
     ButtonExtrasColor = "#d62828",
            ScreenBackgroundMainColor = "#370617",
               ScreenBackgroundSecondaryColor = "#6a040f",
                 HeaderMainColor = "#6a040f",
 HeaderTextColor = "#fcbf49",
           HeaderExtrasColor = "#f77f00",
            WindowBackgroundColor = "#6a040f",
      WindowTextColor = "#ffffff",
    WindowSecondaryColor = "#dc2f02",
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
         ButtonCoreColor = "#7209b7",
 ButtonTextColor = "#ffffff",
        ButtonExtrasColor = "#560bad",
               ScreenBackgroundMainColor = "#10002b",
 ScreenBackgroundSecondaryColor = "#3c096c",
         HeaderMainColor = "#3c096c",
   HeaderTextColor = "#c77dff",
         HeaderExtrasColor = "#e0aaff",
   WindowBackgroundColor = "#3c096c",
             WindowTextColor = "#ffffff",
WindowSecondaryColor = "#5a189a",
        TextMainHeaderColor = "#c77dff",
    TextSecondaryHeaderColor = "#e0aaff",
    TextBodyColor = "#ffffff"
     }
   }
   };
        }
    }
}
