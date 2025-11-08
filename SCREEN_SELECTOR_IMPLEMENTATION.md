# Screen Selector Debug Feature - Implementation Summary

## Overview
Added a screen selector dropdown to the Debug Menu that allows switching between different screens (Main Menu and Combat Screen) for testing and development purposes.

## Changes Made

### 1. **Application/ViewModels/DebugMenuViewModel.cs**

#### New Properties Added:
- `SelectedScreen` (string) - Currently selected screen name
  - Default: "Combat Screen"
  - Notifies visibility property changes on update
  
- `AvailableScreens` (List<string>) - List of available screens
  - "Main Menu"
  - "Combat Screen"

#### New Visibility Properties:
- `IsCombatScreenVisible` (bool) - Returns true when "Combat Screen" is selected
- `IsMainMenuVisible` (bool) - Returns true when "Main Menu" is selected

These properties are calculated based on `SelectedScreen` and automatically trigger UI updates via `INotifyPropertyChanged`.

### 2. **Presentation/Views/Debug/DebugMenu.xaml**

#### Added Screen Selector Section:
- New section labeled "Screen Selector" between Color Scheme and Debug Actions
- ComboBox bound to `AvailableScreens` collection
- Two-way binding to `SelectedScreen` property
- Styled consistently with the existing Color Scheme ComboBox (dark theme with red accent)

UI Structure:
```
- Color Scheme (existing)
- [Separator]
- **Screen Selector** (NEW)
  ?? Dropdown with screen options
- [Separator]
- Debug Actions (existing placeholders)
```

### 3. **Presentation/Views/MainWindow.xaml**

#### Updated Content Visibility:
- `MainFrame` now has visibility bound to `DebugMenu.IsMainMenuVisible`
  - Hidden when Combat Screen is selected
  
- `CombatScreen` now has visibility bound to `DebugMenu.IsCombatScreenVisible`
  - Visible by default (Combat Screen is default selection)
  - Hidden when Main Menu is selected

Both use the existing `BooleanToVisibilityConverter` for proper WPF visibility binding.

## How It Works

1. **Default State**: Application starts with "Combat Screen" selected and visible
2. **User Interaction**: 
   - Press F12 or toggle key to open Debug Menu
   - Select "Screen Selector" dropdown
   - Choose between "Main Menu" or "Combat Screen"
3. **Screen Switching**: 
   - Selection change triggers `SelectedScreen` property update
   - Visibility properties (`IsCombatScreenVisible`, `IsMainMenuVisible`) recalculate
   - Bound screens show/hide automatically via `BooleanToVisibilityConverter`

## Benefits

? **Easy Testing**: Switch between screens instantly without navigation logic
? **Development Aid**: View and test Combat Screen layout during development
? **No Code Changes**: Pure MVVM binding approach, no code-behind required
? **Extensible**: Easy to add more screens to `AvailableScreens` list
? **Consistent UI**: Uses existing debug menu styling and patterns

## Future Enhancements

- Add more screens as they're developed (Inventory, Character Sheet, etc.)
- Add keyboard shortcuts for quick screen switching
- Remember last selected screen in debug settings
- Add screen-specific debug options when a screen is selected

## Build Status
? All files modified successfully
? Build completed without errors
? Feature ready for use

## Usage Instructions

1. Run the application
2. Press the debug menu toggle key (if configured) or use the ToggleMenuCommand
3. Locate the "Screen Selector" dropdown in the debug menu
4. Select "Combat Screen" to view the combat interface
5. Select "Main Menu" to view the main content frame

The Combat Screen should now be visible by default and can be toggled via the debug menu!
