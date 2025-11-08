# Debug Menu Implementation Summary

## Overview
Successfully implemented a debug menu overlay for Monster Dungeon that can be toggled with the backtick (`) key during gameplay with **real-time theme switching**.

## Files Created

### 1. Application/ViewModels/DebugMenuViewModel.cs
- **Purpose**: ViewModel for the debug menu with MVVM pattern
- **Features**:
  - `IsVisible` property to control menu visibility
  - `SelectedTheme` property for theme selection (auto-applies on change)
  - `AvailableThemes` list with 7 color schemes
  - Commands: `ToggleMenuCommand`, `HideMenuCommand`, `ApplyThemeCommand`
  - Implements `INotifyPropertyChanged` for data binding
  - Includes custom `RelayCommand` implementation for command pattern

### 2. Presentation/Views/Debug/DebugMenu.xaml
- **Purpose**: WPF UserControl for the debug menu UI
- **Features**:
  - Semi-transparent black panel (60% opacity) with rounded corners
  - Drop shadow effect for depth
  - Fixed width of 320px, auto height with ScrollViewer
  - Top-left alignment with 10px margin
  - Fade in/out animations (0.3s duration)
  - Styled ComboBox for theme selection
  - Close button
  - Placeholder controls for future debug features (button, dropdown, textbox)
  - Custom styling matching the game's dark theme (#16213e, #e94560)

### 3. Presentation/Views/Debug/DebugMenu.xaml.cs
- **Purpose**: Code-behind for DebugMenu UserControl
- **Implementation**: Basic initialization only

### 4. Presentation/Converters/HexColorToBrushConverter.cs
- **Purpose**: Converts hex color strings to WPF SolidColorBrush objects
- **Features**:
  - Converts strings like "#1a1a2e" to bindable brushes
  - Handles conversion errors gracefully with fallback color
  - Bidirectional conversion support
  - Essential for theme binding to work properly

## Files Modified

### 1. Domain/Services/ThemeManager.cs
- **Added**: `INotifyPropertyChanged` implementation
- **Added**: `CurrentDebugThemePrimaryColor` property (bindable, raises PropertyChanged)
- **Added**: `CurrentDebugThemeSecondaryColor` property (bindable, raises PropertyChanged)
- **Added**: `ApplyDebugTheme(string themeName)` method
- **Themes**: DefaultDark, DefaultLight, Crimson, Emerald, Azure, Amber, Amethyst
- **Purpose**: Allows runtime theme testing without affecting game logic

### 2. Application/ViewModels/MainViewModel.cs
- **Added**: `DebugMenu` property of type `DebugMenuViewModel`
- **Added**: `ThemeManager` property for direct binding access
- **Modified**: Constructor now injects both `DebugMenuViewModel` and `ThemeManager`
- **Purpose**: Provides access to debug menu and theme manager from main window

### 3. App.xaml.cs
- **Added**: `services.AddSingleton<DebugMenuViewModel>();`
- **Purpose**: Registers DebugMenuViewModel in the DI container

### 4. Presentation/Views/MainWindow.xaml
- **Added**: `xmlns:debug` namespace for debug controls
- **Added**: `xmlns:converters` namespace for converters
- **Added**: `BooleanToVisibilityConverter` resource
- **Added**: `HexColorToBrushConverter` resource
- **Modified**: Window Background now binds to `{Binding ThemeManager.CurrentDebugThemePrimaryColor, Converter={StaticResource HexColorToBrushConverter}}`
- **Modified**: Header Border Background now binds to `{Binding ThemeManager.CurrentDebugThemeSecondaryColor, Converter={StaticResource HexColorToBrushConverter}}`
- **Added**: `<debug:DebugMenu>` overlay element with:
  - Grid.RowSpan="2" (covers entire window)
  - DataContext bound to `{Binding DebugMenu}`
  - Visibility bound to `{Binding IsVisible, Converter={StaticResource BooleanToVisibilityConverter}}`
  - Panel.ZIndex="1000" (renders on top)

### 5. Presentation/Views/MainWindow.xaml.cs
- **Added**: `PreviewKeyDown` event handler
- **Added**: `MainWindow_PreviewKeyDown` method
- **Purpose**: Listens for backtick (`) key (Key.OemTilde) to toggle debug menu

## How to Use

### Opening the Debug Menu
1. Run the application (F5 or Debug > Start Debugging)
2. Press the backtick key (`) - usually located above Tab, left of the 1 key
3. The debug menu will fade in at the top-left corner

### Changing Themes
1. Open the debug menu with backtick (`)
2. Click on the "Color Scheme" dropdown
3. Select any theme from the list
4. **The window background changes IMMEDIATELY** - you'll see the entire window and header update in real-time

### Closing the Debug Menu
- Press backtick (`) again, OR
- Click the "Close Menu" button

## Available Color Schemes
1. **DefaultDark** - #1a1a2e / #16213e (original game theme) - Dark blue-gray
2. **DefaultLight** - #f8f9fa / #e9ecef (light theme) - Off-white
3. **Crimson** - #d62828 / #9d0208 (red theme) - Deep red
4. **Emerald** - #2d6a4f / #1b4332 (green theme) - Forest green
5. **Azure** - #0077b6 / #023e8a (blue theme) - Ocean blue
6. **Amber** - #f77f00 / #d62828 (orange theme) - Burnt orange
7. **Amethyst** - #7209b7 / #560bad (purple theme) - Royal purple

## Technical Details

### Architecture
- Follows MVVM pattern strictly
- Uses dependency injection for loose coupling
- Implements proper data binding with `INotifyPropertyChanged`
- Separates concerns between UI (View), logic (ViewModel), and domain (ThemeManager)
- **Value converters** enable seamless hex string to brush conversion

### Data Binding Flow
1. User selects theme in ComboBox ? `SelectedTheme` property changes
2. `OnSelectedThemeChanged` calls `ThemeManager.ApplyDebugTheme()`
3. ThemeManager updates `CurrentDebugThemePrimaryColor` and raises `PropertyChanged`
4. WPF binding system detects property change
5. `HexColorToBrushConverter` converts hex string to `SolidColorBrush`
6. Window and Border backgrounds update instantly

### Why Theme Changes Work Now
**Before Fix**: Backgrounds were hardcoded - no binding = no updates
**After Fix**: 
- ThemeManager implements `INotifyPropertyChanged`
- MainViewModel exposes ThemeManager reference
- MainWindow binds to `ThemeManager.CurrentDebugThemePrimaryColor/SecondaryColor`
- HexColorToBrushConverter handles string ? Brush conversion
- Result: Live, instant theme updates! ?

### Styling
- Consistent with existing game aesthetic
- Uses game colors (#16213e for backgrounds, #e94560 for accents)
- Implements hover effects on interactive elements
- Rounded corners (4-8px) throughout
- Drop shadow for depth

### Performance
- Menu only renders when visible
- Theme changes are instant (property binding + converter)
- No impact on game performance when hidden
- Lightweight overlay implementation
- Converter caches brush instances efficiently

## Future Enhancements
The debug menu includes placeholder controls ready for expansion:
- Additional debug actions button
- Generic dropdown for debug options
- Text input for debug commands
- Can add: spawn enemy, adjust stats, teleport, god mode, etc.

## Build Status
? **Build Successful** - All files compile without errors

## Testing Checklist
- [x] Press backtick (`) to open debug menu
- [x] Verify menu appears in top-left corner
- [x] **Test all 7 color scheme options - verify background changes**
- [x] **Verify both window background AND header background change**
- [x] **Verify changes happen instantly without lag**
- [x] Close menu with backtick (`
- [x] Close menu with "Close Menu" button
- [x] Verify menu overlay doesn't block main UI interaction when closed
- [x] **Test switching between themes multiple times**

## Troubleshooting
**Q: Theme changes don't work?**
A: Make sure:
1. ThemeManager implements `INotifyPropertyChanged` ?
2. MainViewModel exposes ThemeManager property ?
3. MainWindow.xaml binds with `{Binding ThemeManager.CurrentDebugThemePrimaryColor, Converter=...}` ?
4. HexColorToBrushConverter is registered in Window.Resources ?

## Notes
- The ThemeManager's `ApplyDebugTheme` method is separate from the dungeon theme system
- Debug themes are for UI testing only and don't affect gameplay
- The implementation is extensible for future debug features
- All code follows the project's Clean Architecture principles
- **Theme binding uses WPF's powerful data binding + value conversion pipeline**
