# Attack System - Quick Reference

## Files Modified/Created

### Created
- `Domain\Entities\Bullet.cs` - Bullet entity class

### Modified
- `Application\ViewModels\CombatViewModel.cs` - Added bullet logic
- `Presentation\Views\Game\CombatScreen.xaml` - Added bullet rendering

## Key Code Changes

### CombatViewModel.cs
```csharp
// Added Properties
private ObservableCollection<Bullet> _bullets;
public ObservableCollection<Bullet> Bullets { get; set; }
private DispatcherTimer _bulletTimer;

// New Methods
- InitializeBulletTimer() - Sets up 50ms animation timer
- BulletTimer_Tick() - Timer event handler
- ProcessBulletMovement() - Moves bullets and detects collisions
- Attack() - Fires bullet from player position
```

### CombatScreen.xaml
```xaml
<!-- New ItemsControl for bullets -->
<ItemsControl ItemsSource="{Binding Bullets}" Panel.ZIndex="3">
  - Uses Canvas positioning
  - Yellow circular sprite (30x30)
  - Higher ZIndex than enemies
</ItemsControl>
```

## How It Works

1. **User clicks "Attack" button** ? Triggers `Attack()` method
2. **Bullet spawns** at (PlayerX, PlayerY - 1)
3. **Bullet added** to `Bullets` ObservableCollection
4. **Timer starts** if not already running (50ms interval)
5. **Each tick** calls `ProcessBulletMovement()`:
   - Moves each bullet Y -= 1
   - Checks for enemy collision
   - Removes bullet if off-screen or hits enemy
   - Removes enemy if hit
6. **Timer stops** when bullets collection is empty

## Visual Flow
```
Player Position: (X=3, Y=9)
        ?
Attack Button Clicked
        ?
Bullet Spawned: (X=3, Y=8)
?
Timer Tick ? Y=7 ? Check collision
        ?
Timer Tick ? Y=6 ? Check collision
        ?
Timer Tick ? Y=5 ? HIT! Enemy at (3,5)
        ?
Remove bullet & enemy
```

## Configuration

### Bullet Speed
Change timer interval in `InitializeBulletTimer()`:
```csharp
_bulletTimer.Interval = TimeSpan.FromMilliseconds(50); // Current: 20 FPS
```

### Bullet Appearance
Modify XAML DataTemplate in BulletContainer:
```xaml
<Border Background="#FFEB3B" ... /> <!-- Yellow background -->
<Ellipse Fill="#FDD835" ... />      <!-- Inner glow -->
```

### Bullet Damage
Modify in `Bullet.cs`:
```csharp
public int Damage { get; set; } = 10; // Currently unused (instant kill)
```

## Testing Checklist
- ? Click Attack button
- ? Bullet spawns above player
- ? Bullet travels upward
- ? Bullet removes enemy on collision
- ? Bullet disappears at top edge
- ? Multiple bullets can be fired
- ? Debug messages appear in Output window
