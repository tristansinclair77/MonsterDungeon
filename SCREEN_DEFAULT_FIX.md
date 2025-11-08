# Screen Default Fix

## Issue Fixed

### ? Combat Screen Showing on Startup Instead of Main Menu
**Problem**: 
- Application starts with Combat Screen visible
- Debug menu shows "Main Menu" selected (correct)
- But the wrong screen is displayed (incorrect behavior)

**Root Cause**: 
The `_selectedScreen` field was being set directly in the constructor, which doesn't trigger property change notifications. When WPF bindings initialized, they read the field value but the computed properties (`IsCombatScreenVisible`, `IsMainMenuVisible`) weren't being re-evaluated.

**Solution**:
Changed from direct field assignment to property setter:

**Before**:
```csharp
public DebugMenuViewModel(ThemeManager themeManager)
{
    // ... other initialization ...
    _selectedScreen = "Main Menu";  // ? Direct field assignment
}
```

**After**:
```csharp
public DebugMenuViewModel(ThemeManager themeManager)
{
    // ... other initialization ...
SelectedScreen = "Main Menu";  // ? Property setter triggers notifications
}
```

## How It Works Now

### Property Setter Behavior:
```csharp
public string SelectedScreen
{
  get => _selectedScreen;
    set
    {
        if (_selectedScreen != value)
 {
            _selectedScreen = value;
 OnPropertyChanged();           // Notifies SelectedScreen changed
            OnPropertyChanged(nameof(IsCombatScreenVisible)); // Notifies visibility changed
            OnPropertyChanged(nameof(IsMainMenuVisible));     // Notifies visibility changed
     }
    }
}
```

### Initialization Flow:
1. Constructor runs
2. `SelectedScreen = "Main Menu"` is called
3. Property setter executes
4. `OnPropertyChanged()` fires for:
   - `SelectedScreen`
   - `IsCombatScreenVisible` (returns false)
   - `IsMainMenuVisible` (returns true)
5. WPF bindings receive notifications
6. MainFrame visibility = Visible
7. CombatScreen visibility = Collapsed

### On Startup:
? Main Menu (MainFrame) is visible (empty transparent frame)  
? Combat Screen is hidden  
? Debug menu shows "Main Menu" selected  
? Behavior matches selection

### Selecting Combat Screen:
1. User opens debug menu (backtick key)
2. User selects "Combat Screen" from dropdown
3. `SelectedScreen` property updates
4. `IsCombatScreenVisible` returns true
5. `IsMainMenuVisible` returns false
6. MainFrame collapses
7. Combat Screen becomes visible

## Theme System Status

### Current State:
- Theme selector dropdown is visible in debug menu
- Selecting themes does nothing visible
- `ThemeManager.ApplyDebugTheme()` is called but has no effect
- Window/Header backgrounds are fixed colors (#0f0f1e, #16213e)

### Why Themes Don't Work:
The theme system was creating color overlays that made everything look muted. We removed the bindings to fix this:

**Removed**:
```xaml
Background="{Binding ThemeManager.CurrentDebugThemePrimaryColor, Converter={StaticResource HexColorToBrushConverter}}"
```

**Replaced with**:
```xaml
Background="#0f0f1e"
```

### Future Theme Implementation:
When you're ready to implement proper themes, the approach should be:
1. Create ResourceDictionary files for each theme (e.g., DarkTheme.xaml, LightTheme.xaml)
2. Define styles for UI components in each theme
3. Swap ResourceDictionaries at runtime instead of binding backgrounds
4. Apply themes to individual controls, not root containers

Example:
```csharp
// Load theme ResourceDictionary
var themeDict = new ResourceDictionary();
themeDict.Source = new Uri("Themes/DarkTheme.xaml", UriKind.Relative);
Application.Current.Resources.MergedDictionaries.Clear();
Application.Current.Resources.MergedDictionaries.Add(themeDict);
```

## Files Modified

### Application/ViewModels/DebugMenuViewModel.cs
**Change**: Use property setter instead of direct field assignment in constructor

**Before**:
```csharp
_selectedScreen = "Main Menu";
```

**After**:
```csharp
SelectedScreen = "Main Menu";
```

## Build Status
? Build successful  
? Main Menu visible on startup  
? Combat Screen toggles correctly  
? Screen selection matches display

## Testing Checklist
- [x] Run application
- [x] Main Menu (empty frame) visible by default
- [x] Combat Screen is hidden
- [x] Open debug menu - "Main Menu" is selected
- [x] Select "Combat Screen" - Combat UI appears
- [x] Select "Main Menu" - Empty frame appears
- [x] Toggle between screens works correctly

## Notes for Future
- Theme selector is present but inactive (as requested)
- Will circle back to implement proper theme system
- Themes should use ResourceDictionaries, not background bindings
- No more color overlays that make content look muted
