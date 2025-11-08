# Presentation Layer

## Purpose
The Presentation layer contains the WPF user interface implementation using the MVVM (Model-View-ViewModel) pattern. This layer is responsible for all visual aspects and user interactions.

## Principles
- **MVVM Pattern** - Separation of UI and logic
- **Data Binding** - Use WPF data binding extensively
- **Commands** - Use ICommand for user actions
- **Responsive UI** - Keep UI thread responsive
- **Styling** - Consistent look and feel across the application

## Contents

### Views
XAML-based user interface windows and pages:
- **MainWindow.xaml** - Main game window
- **MainMenuView.xaml** - Main menu screen
- **GameView.xaml** - Main gameplay view
- **InventoryView.xaml** - Inventory management screen
- **CombatView.xaml** - Combat encounter screen
- **CharacterView.xaml** - Character stats and equipment
- **SettingsView.xaml** - Game settings and options

### Controls
Custom WPF controls and user controls:
- **PlayerStatsControl** - Display player statistics
- **InventoryGrid** - Grid-based inventory display
- **MiniMapControl** - Mini-map of the dungeon
- **HealthBarControl** - Animated health bar
- **AbilityButtonControl** - Spell/ability buttons
- **TooltipControl** - Enhanced tooltips for items

### Resources
Game assets and resources:
- **Images/** - Sprites, icons, backgrounds
- **Sounds/** - Sound effects and music
- **Fonts/** - Custom fonts for the game
- **Data/** - Static data files (if needed)

### Styles
XAML styling and themes:
- **Colors.xaml** - Color palette definitions
- **Buttons.xaml** - Button styles
- **TextBlocks.xaml** - Text styles
- **Borders.xaml** - Border and panel styles
- **Animations.xaml** - Storyboards and animations
- **DarkTheme.xaml** - Dark theme (default)

## MVVM Pattern

### Views (XAML)
- Define UI layout and structure
- Bind to ViewModel properties
- No code-behind logic (minimal if necessary)

### ViewModels (C#)
- Implement `INotifyPropertyChanged`
- Expose properties for data binding
- Implement `ICommand` for user actions
- Handle UI state logic

### Models
- Provided by Application layer
- Read-only from View perspective

## WPF Features Used
- **Data Binding** - Two-way binding for user input
- **Commands** - RelayCommand/DelegateCommand
- **Styles** - Consistent UI appearance
- **Templates** - Custom control templates
- **Animations** - Visual feedback
- **Resources** - Reusable XAML resources

## Design Guidelines
1. Keep Views simple - logic belongs in ViewModels
2. Use data binding instead of code-behind
3. Use Commands for all user actions
4. Follow naming conventions (e.g., `MainViewModel` for `MainView`)
5. Use ResourceDictionaries for reusable styles
6. Keep UI responsive with async operations
7. Provide visual feedback for all user actions

## Dependencies
- **Application** - ViewModels and application services
- **System.Windows** - WPF framework
- **PresentationFramework** - WPF components
- **PresentationCore** - WPF core functionality

## User Experience
- Intuitive navigation
- Clear visual hierarchy
- Responsive controls
- Smooth animations
- Helpful tooltips and feedback
- Keyboard shortcuts for common actions
