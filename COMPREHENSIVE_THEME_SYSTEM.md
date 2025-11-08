# Comprehensive Theme System Implementation

## Overview
Implemented a granular, comprehensive theme system where each UI element type has dedicated color properties that can be changed by selecting different themes.

## What Was Created

### 1. Domain/Models/UITheme.cs
**Purpose**: Data model representing a complete theme with all color properties

**Properties** (15 total):
- **Buttons**: ButtonCoreColor, ButtonTextColor, ButtonExtrasColor
- **Screen Background**: ScreenBackgroundMainColor, ScreenBackgroundSecondaryColor
- **Header**: HeaderMainColor, HeaderTextColor, HeaderExtrasColor
- **Windows**: WindowBackgroundColor, WindowTextColor, WindowSecondaryColor
- **Text**: TextMainHeaderColor, TextSecondaryHeaderColor, TextBodyColor
- **Metadata**: Name, Description

### 2. Domain/Services/ThemeManager.cs (Updated)
**Purpose**: Manages themes and provides bindable color properties

**New Properties** (14 bindable color properties):
Each property returns a hex color string from the current `UITheme` with fallback defaults:

```csharp
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
```

**New Method: `InitializeUIThemes()`**
Creates 7 complete theme presets with all 14 colors defined:

1. **DefaultDark** - Dark blue with red accents (original)
2. **DefaultLight** - Light theme with dark text
3. **Crimson** - Intense red theme
4. **Emerald** - Forest green theme
5. **Azure** - Ocean blue theme
6. **Amber** - Warm orange theme
7. **Amethyst** - Royal purple theme

**Updated Method: `ApplyDebugTheme(string themeName)`**
- Looks up theme in `_uiThemes` dictionary
- Sets `_currentUITheme` to selected theme
- Fires `PropertyChanged` for all 14 color properties
- **This triggers all UI bindings to update**

### 3. Resources/Styles.xaml (New File)
**Purpose**: Reusable WPF styles that bind to theme properties

**Styles Created**:
- `ThemedButton` - Button style with themed colors
- `ThemedWindowBackground` - Border style for windows/panels
- `ThemedMainHeader` - TextBlock style for main headers
- `ThemedSecondaryHeader` - TextBlock style for sub-headers
- `ThemedBodyText` - TextBlock style for body text

**Example Usage**:
```xaml
<Button Style="{StaticResource ThemedButton}" Content="Click Me"/>
<TextBlock Style="{StaticResource ThemedMainHeader}" Text="INVENTORY"/>
<Border Style="{StaticResource ThemedWindowBackground}">
    <!-- content -->
</Border>
```

### 4. App.xaml (Updated)
Added `Styles.xaml` to merged dictionaries so styles are available application-wide.

### 5. Presentation/Views/MainWindow.xaml (Updated)
**Bindings Added**:
```xaml
<!-- Window Background -->
<Window Background="{Binding ThemeManager.ScreenBackgroundMainColor, Converter={StaticResource HexColorToBrushConverter}}">

<!-- Header Background -->
<Border Background="{Binding ThemeManager.HeaderMainColor, Converter={StaticResource HexColorToBrushConverter}}">
    <!-- Header Title Text -->
    <TextBlock Foreground="{Binding ThemeManager.HeaderTextColor, Converter={StaticResource HexColorToBrushConverter}}"/>
    <!-- Header Version Text -->
    <TextBlock Foreground="{Binding ThemeManager.HeaderExtrasColor, Converter={StaticResource HexColorToBrushConverter}}"/>
</Border>
```

## How It Works

### Data Flow:
```
User selects theme in Debug Menu dropdown
    ?
DebugMenuViewModel.SelectedTheme property changes
    ?
Calls ThemeManager.ApplyDebugTheme(themeName)
    ?
ThemeManager._currentUITheme = _uiThemes[themeName]
    ?
ThemeManager fires PropertyChanged for all 14 properties:
    - ButtonCoreColor
    - ButtonTextColor
    - ButtonExtrasColor
    - ScreenBackgroundMainColor
    - ScreenBackgroundSecondaryColor
    - HeaderMainColor
    - HeaderTextColor
    - HeaderExtrasColor
    - WindowBackgroundColor
    - WindowTextColor
  - WindowSecondaryColor
    - TextMainHeaderColor
    - TextSecondaryHeaderColor
    - TextBodyColor
    ?
WPF Data Binding System detects changes
    ?
HexColorToBrushConverter converts hex strings to SolidColorBrush
    ?
UI elements update with new colors instantly
```

### Current Bindings:
? **Window Background** - MainWindow
? **Header Background** - MainWindow header
? **Header Text** - Title text
? **Header Extras** - Version text

### Where To Add More Bindings:
You can add bindings to:
- **CombatScreen buttons** - Use `ThemedButton` style or bind directly
- **CombatScreen panels** - Use `ThemedWindowBackground` style
- **Text elements** - Use `ThemedMainHeader`, `ThemedSecondaryHeader`, or `ThemedBodyText` styles

## Theme Color Categories

### Button Colors
- **Core**: Main button background
- **Text**: Button text color
- **Extras**: Border, hover effects, decorative details

### Screen Background
- **Main**: Primary background color
- **Secondary**: Secondary background for gradients, layers

### Header
- **Main**: Header background color
- **Text**: Main header text
- **Extras**: Secondary text, icons, details

### Window
- **Background**: Panel/window background
- **Text**: Text displayed in windows
- **Secondary**: Borders, accents, gradients

### Text
- **Main Header**: Large section headers ("INVENTORY", "STATS")
- **Secondary Header**: Sub-section headers ("Health", "Mana")
- **Body**: Description text, body content

## Available Themes

| Theme | Description | Color Scheme |
|-------|-------------|--------------|
| DefaultDark | Original dark blue with red accents | Blue-gray + Red |
| DefaultLight | Light theme with dark text | Off-white + Red |
| Crimson | Intense red theme | Dark red + Orange |
| Emerald | Forest green theme | Dark green + Light green |
| Azure | Ocean blue theme | Navy + Cyan |
| Amber | Warm orange theme | Dark red + Orange/Yellow |
| Amethyst | Royal purple theme | Deep purple + Light purple |

## How To Use in Your XAML

### Option 1: Use Predefined Styles
```xaml
<!-- Button -->
<Button Style="{StaticResource ThemedButton}" Content="Attack"/>

<!-- Window/Panel Background -->
<Border Style="{StaticResource ThemedWindowBackground}">
    <StackPanel>
        <!-- Main Header -->
   <TextBlock Style="{StaticResource ThemedMainHeader}" Text="INVENTORY"/>
        
        <!-- Secondary Header -->
     <TextBlock Style="{StaticResource ThemedSecondaryHeader}" Text="Weapons"/>
        
        <!-- Body Text -->
   <TextBlock Style="{StaticResource ThemedBodyText}" Text="Description here"/>
    </StackPanel>
</Border>
```

### Option 2: Bind Directly
```xaml
<Button Background="{Binding ThemeManager.ButtonCoreColor, 
         Converter={StaticResource HexColorToBrushConverter}, 
    RelativeSource={RelativeSource AncestorType=Window}}"
        Foreground="{Binding ThemeManager.ButtonTextColor, 
       Converter={StaticResource HexColorToBrushConverter}, 
       RelativeSource={RelativeSource AncestorType=Window}}"
        BorderBrush="{Binding ThemeManager.ButtonExtrasColor, 
      Converter={StaticResource HexColorToBrushConverter}, 
     RelativeSource={RelativeSource AncestorType=Window}}"
        Content="Attack"/>
```

**Note**: When using `RelativeSource`, make sure the element's DataContext isn't overridden.

## Benefits

? **Granular Control** - 14 separate color properties for fine-tuned theming
? **No Overlays** - Direct property binding, no semi-transparent layers
? **Live Updates** - Themes change instantly when selected
? **Extensible** - Easy to add new themes by creating new `UITheme` objects
? **Reusable Styles** - Predefined styles for common elements
? **Type-Safe** - All colors defined in code, not scattered in XAML
? **Fallback Values** - Default colors if binding fails

## Next Steps

1. **Apply themed styles to CombatScreen** - Replace static resources with themed ones
2. **Create more screens** - Inventory, Character Sheet, etc. using themed styles
3. **Add more themes** - Create additional `UITheme` presets
4. **Dynamic theme creation** - Allow users to customize colors
5. **Theme persistence** - Save/load user's selected theme

## Build Status
? All files created successfully  
? Build successful with no errors  
? Theme system fully functional
? MainWindow demonstrates working theme bindings  
? 7 themes ready to use
