# Theme Customization System - Implementation Status

## ? Completed So Far:

### 1. Infrastructure/Services/ThemeStorageService.cs ?
**Purpose**: Handles JSON persistence of theme customizations

**Features**:
- Loads themes from `%AppData%/MonsterDungeon/Themes/*.json`
- Saves theme modifications to JSON files
- Stores default theme configurations
- `ResetThemeToDefault()` method to restore defaults
- Automatic file creation on first load

**Default Themes Stored**:
- DefaultDark, DefaultLight, Crimson, Emerald, Azure, Amber, Amethyst
- Each with all 14 color properties defined

### 2. Domain/Services/ThemeManager.cs ?
**Updated Features**:
- Injected `ThemeStorageService` via constructor
- `ApplyDebugTheme()` now loads from storage (includes customizations)
- New method: `UpdateThemeColor(themeName, propertyName, colorValue)`
  - Updates a specific color
  - Saves to JSON immediately
  - Reloads theme if it's currently active
- New method: `ResetThemeToDefault(themeName)`
  - Restores default colors
  - Saves to JSON
  - Reloads theme if currently active
- New method: `GetCurrentUITheme()` - returns current theme object for binding

### 3. App.xaml.cs ?
- Registered `ThemeStorageService` in DI container

### 4. Build Status ?
- All code compiles successfully
- No errors

## ?? Still Need to Implement:

### 1. DebugMenuViewModel Updates
Need to add:
- Properties for each of the 14 color values (for color picker binding)
- `ResetToDefaultCommand` - calls `ThemeManager.ResetThemeToDefault()`
- Command to update colors when changed
- Property for collapsible section visibility

### 2. DebugMenu.xaml - New "Color Theme Options" Section
Need to add collapsible section with:
- **Expandable/Collapsible Header** ("Color Theme Options")
- **Reset to Default Button** at top
- **14 Color Pickers** with labels:
  - Button Core Color
  - Button Text Color
  - Button Extras Color
  - Screen Background Main Color
  - Screen Background Secondary Color
  - Header Main Color
  - Header Text Color
  - Header Extras Color
  - Window Background Color
  - Window Text Color
  - Window Secondary Color
  - Text Main Header Color
  - Text Secondary Header Color
  - Text Body Color

### 3. WPF Color Picker Control
Need to implement or use existing color picker:
- Option A: Use `System.Windows.Forms.ColorDialog` (simple but not in-XAML)
- Option B: Create custom color picker control (complex)
- Option C: Use third-party library (e.g., Extended WPF Toolkit)
- **Recommended**: Use Extended WPF Toolkit `ColorPicker`

## ?? Implementation Plan - Next Steps:

### Step 1: Install Extended WPF Toolkit (for ColorPicker control)
```
Install-Package Extended.Wpf.Toolkit via NuGet
```

### Step 2: Update DebugMenuViewModel
Add properties and commands for color customization

### Step 3: Update DebugMenu.xaml
Add collapsible "Color Theme Options" section with:
- Expander control
- Reset button
- 14 ColorPicker controls with bindings

## ?? How JSON Storage Works:

### File Location:
```
%AppData%/MonsterDungeon/Themes/
??? DefaultDark.json
??? DefaultLight.json
??? Crimson.json
??? Emerald.json
??? Azure.json
??? Amber.json
??? Amethyst.json
```

### Example JSON File (Crimson.json):
```json
{
  "name": "Crimson",
  "description": "Intense red theme",
  "buttonCoreColor": "#d62828",
  "buttonTextColor": "#ffffff",
  "buttonExtrasColor": "#9d0208",
  "screenBackgroundMainColor": "#370617",
  "screenBackgroundSecondaryColor": "#6a040f",
  "headerMainColor": "#6a040f",
  "headerTextColor": "#f48c06",
  "headerExtrasColor": "#dc2f02",
  "windowBackgroundColor": "#6a040f",
  "windowTextColor": "#ffffff",
  "windowSecondaryColor": "#9d0208",
  "textMainHeaderColor": "#f48c06",
  "textSecondaryHeaderColor": "#dc2f02",
  "textBodyColor": "#ffffff"
}
```

### User Customization Flow:
1. User selects "Crimson" theme
2. `ThemeManager` loads `Crimson.json` (or creates with defaults)
3. User changes "Window Background Color" to `#123456` via color picker
4. `ThemeManager.UpdateThemeColor("Crimson", "WindowBackgroundColor", "#123456")` is called
5. Theme is updated in memory
6. JSON file is saved with new value
7. Theme is reloaded, UI updates instantly
8. Next time user selects "Crimson", customization is loaded

### Reset to Default:
1. User clicks "Reset to Default" button
2. `ThemeManager.ResetThemeToDefault("Crimson")` is called
3. Default colors are restored from `_defaultThemes` dictionary
4. JSON file is overwritten with defaults
5. Theme is reloaded, UI updates to original colors

## ?? Expected User Experience:

1. **Open Debug Menu** (backtick key)
2. **Expand "Color Theme Options"** (collapsible section)
3. **See 14 labeled color pickers** showing current theme colors
4. **Click a color picker** ? Color dialog opens
5. **Select new color** ? UI updates instantly, JSON saves automatically
6. **Switch themes** ? Customizations are remembered per-theme
7. **Click "Reset to Default"** ? Current theme reverts to original colors

## ?? Key Features:

? **Per-Theme Persistence** - Each theme's customizations stored separately  
? **Instant Updates** - Color changes apply immediately  
? **Default Restoration** - One-click reset to original colors  
? **Auto-Save** - No manual save button needed  
? **JSON Storage** - Easy to edit manually if needed  
? **Isolated Defaults** - Original themes never overwritten

## Build Status:
? Infrastructure layer complete  
? Domain layer updated
? DI configured  
? JSON storage functional  
? UI layer pending (DebugMenuViewModel + DebugMenu.xaml)
