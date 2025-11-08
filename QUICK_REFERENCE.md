# ?? Monster Dungeon - Quick Reference

## ?? Get Started in 3 Steps

### 1?? Add Files (2 minutes)
- Open Solution Explorer ? Click "Show All Files"
- Right-click "Domain" folder ? "Include In Project"
- Right-click "Application" folder ? "Include In Project"
- Right-click "Presentation" folder ? "Include In Project"

### 2?? Install Packages (3 minutes)
- Right-click project ? "Manage NuGet Packages"
- Install these 4 packages:
  ```
  CommunityToolkit.Mvvm
  Microsoft.Extensions.DependencyInjection
  Microsoft.Extensions.Hosting
  System.Text.Json
  ```

### 3?? Uncomment Code (2 minutes)
Edit `App.xaml.cs`:
- Uncomment `using` statements (lines 3-4)
- Uncomment `AppHost` property
- Uncomment DI setup in constructor
- Replace temporary `OnStartup` with DI version

**Then Build (Ctrl+Shift+B) and Run (F5)!**

---

## ?? Project Structure

```
MonsterDungeon/
?
??? Domain/         [Game Logic - No Dependencies]
?   ??? Entities/        Player, Enemy, Item, Spell
?   ??? Enums/     Game constants
?   ??? Services/   EnemyFactory, ThemeManager
?
??? Application/         [Coordination Layer]
?   ??? ViewModels/      MainViewModel
?   ??? Services/     GridService, GameFlowService
?
??? Infrastructure/   [External Concerns]
?   ??? Persistence/     (Ready for save/load)
?   ??? Services/        (Ready for assets)
?
??? Presentation/        [WPF UI]
    ??? Views/      MainWindow
    ??? Assets/Sprites, Icons, Music
```

---

## ?? Core Classes

| Class | Location | Purpose |
|-------|----------|---------|
| `Player` | Domain/Entities | Player stats, inventory, leveling |
| `Enemy` | Domain/Entities | Enemy stats, combat |
| `GridService` | Application/Services | 8×10 grid management |
| `GameFlowService` | Application/Services | Game loop, turns |
| `EnemyFactory` | Domain/Services | Spawn enemies |
| `ThemeManager` | Domain/Services | Dungeon themes |
| `MainViewModel` | Application/ViewModels | Main window state |

---

## ?? Game Mechanics

### Grid System
- **Size**: 8 tiles wide × 10 tiles tall
- **Movement**: Player moves, then enemies descend
- **Spawning**: Enemies spawn at top, move down each turn

### Combat
- **Damage**: Attack - Defense (minimum 1)
- **Leveling**: 100 XP per level
- **Drops**: Gold + Experience from enemies

### Themes
- **Caverns** (Earth) - Starting theme
- **Crypt** (Dark) - Undead enemies
- **Volcano** (Fire) - Molten caves
- **Ice Cavern** (Ice) - Frozen passages

### Affinities
- **Matching**: 1.5× damage bonus
- **Opposing**: 0.5× damage penalty
- Fire ? Ice, Light ? Dark

---

## ?? Common Tasks

### Add a New Enemy Type
1. Add to `EnemyType` enum in `Domain/Enums/GameEnums.cs`
2. Update `EnemyFactory.CalculateHealth/Attack/Defense()`
3. Create sprite in `Presentation/Assets/Sprites/`

### Add a New Spell
1. Create `Spell` instance in application code
2. Set `Name`, `Damage`, `ManaCost`, `Affinity`
3. Add spell icon to `Presentation/Assets/Icons/`

### Add a New Theme
1. Add to `DungeonTheme` enum
2. Add theme data in `ThemeManager.InitializeThemeData()`
3. Update `GameFlowService.ChangeTheme()` array

### Create a New View
1. Add XAML file in `Presentation/Views/`
2. Create ViewModel in `Application/ViewModels/`
3. Register in DI container in `App.xaml.cs`
4. Navigate using `MainFrame.Navigate()`

---

## ?? Troubleshooting

| Problem | Solution |
|---------|----------|
| Build fails with "namespace not found" | Include all files in project |
| "No service for type X" | Register service in `App.xaml.cs` DI |
| NuGet packages missing | Right-click solution ? Restore NuGet |
| App won't start | Check App.xaml StartupUri path |
| Property changes don't update UI | Implement INotifyPropertyChanged |

---

## ?? Coding Conventions

### Namespaces
```csharp
MonsterDungeon.Domain.*
MonsterDungeon.Application.*
MonsterDungeon.Infrastructure.*
MonsterDungeon.Presentation.*
```

### Dependency Flow
```
Presentation ? Application ? Domain
     ?
      Infrastructure ? Domain
```

### File Organization
- One class per file
- File name = Class name
- Use proper folder structure

---

## ?? UI Theme

### Colors
- Background: `#1a1a2e` (Dark blue-black)
- Accent: `#e94560` (Red)
- Secondary: `#16213e` (Dark blue)
- Text: `#ffffff` (White)

### Assets
- Sprite size: 32×32 or 16×16 pixels
- Style: 8-bit retro pixel art
- Format: PNG with transparency

---

## ?? Documentation Files

- `README.md` - Project overview
- `SETUP_SUMMARY.md` - Complete setup status
- `VISUAL_STUDIO_SETUP.md` - Step-by-step instructions
- `PROJECT_SETUP_STATUS.md` - Detailed completion status
- `Domain/README.md` - Domain layer guide
- `Application/README.md` - Application layer guide
- `Infrastructure/README.md` - Infrastructure layer guide
- `Presentation/README.md` - Presentation layer guide

---

## ?? Quick Commands

| Action | Shortcut |
|--------|----------|
| Build Solution | `Ctrl+Shift+B` |
| Run/Debug | `F5` |
| Run without Debug | `Ctrl+F5` |
| Go to Definition | `F12` |
| Find All References | `Shift+F12` |
| Rename Symbol | `Ctrl+R, R` |
| Comment/Uncomment | `Ctrl+K, C` / `Ctrl+K, U` |

---

## ? Validation Checklist

Before starting development, verify:

- [ ] All folders visible in Solution Explorer
- [ ] All `.cs` files have C# icon (not generic file icon)
- [ ] 4 NuGet packages installed (check References node)
- [ ] DI code uncommented in `App.xaml.cs`
- [ ] Project builds without errors (`Ctrl+Shift+B`)
- [ ] Application launches (`F5`)
- [ ] MainWindow displays "Monster Dungeon v0.1.0-alpha"
- [ ] No exceptions in Output window

**All checked? You're ready to code! ??**

---

## ?? Next Development Tasks

### Phase 1: Core UI
1. Create game grid visualization
2. Add player sprite display
3. Implement keyboard movement
4. Show enemy sprites

### Phase 2: Combat
1. Add attack targeting UI
2. Implement damage numbers
3. Add health bars
4. Show combat animations

### Phase 3: Systems
1. Inventory screen
2. Equipment management
3. Spell casting UI
4. Level progression display

### Phase 4: Content
1. Add more enemy types
2. Create item catalog
3. Design spell library
4. Build boss encounters

### Phase 5: Polish
1. Add sound effects
2. Background music
3. Particle effects
4. Menu system
5. Save/Load functionality

---

## ?? Tips

- **Start Small**: Get one feature working completely before adding more
- **Test Often**: Build and run frequently to catch issues early
- **Use MVVM**: Keep all logic out of code-behind files
- **Follow Patterns**: Use Command pattern for user actions
- **Document**: Add XML comments to public methods
- **Git Commits**: Commit after each working feature

---

## ?? Pro Tips

1. **Grid Rendering**: Use ItemsControl with UniformGrid for tile display
2. **Animations**: Use WPF Storyboards for smooth movement
3. **Data Binding**: Bind collections to ObservableCollection for auto-updates
4. **Commands**: Use RelayCommand from CommunityToolkit.Mvvm
5. **Assets**: Load sprites using BitmapImage and pack URIs

---

**?? Happy Coding!**

*Your dungeon crawler adventure begins now!* ??????
