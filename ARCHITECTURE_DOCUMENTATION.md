# Player Movement Architecture Documentation

## System Architecture

### Overview
The player movement system follows the MVVM (Model-View-ViewModel) pattern with dependency injection, providing clean separation of concerns and testability.

## Component Breakdown

### 1. Domain Layer (`Domain\Entities\Player.cs`)
**Purpose**: Represents the core player entity with position and stats

```csharp
public class Player
{
    public int X { get; set; }  // Grid column (0-7)
    public int Y { get; set; }  // Grid row (0-9)
    // ... other properties (Health, Attack, Defense, etc.)
}
```

**Key Points**:
- X and Y represent grid coordinates, NOT pixel positions
- Default starting position: X=4, Y=8 (center-bottom)
- Player state is independent of UI concerns

---

### 2. Application Layer - Grid Service (`Application\Services\GridService.cs`)
**Purpose**: Manages the grid state and tile occupancy

```csharp
public class GridService
{
    public const int GridWidth = 8;
    public const int GridHeight = 10;
    
    public bool MovePlayer(int newX, int newY);
    public bool IsValidPosition(int x, int y);
  public void SetPlayer(Player player);
}
```

**Key Responsibilities**:
- Validates movement boundaries
- Tracks tile occupancy
- Updates player position in grid state
- Prevents invalid moves (out of bounds, occupied tiles)

---

### 3. Application Layer - Game Flow Service (`Application\Services\GameFlowService.cs`)
**Purpose**: Orchestrates turn-based game mechanics

```csharp
public class GameFlowService
{
    public void ProcessPlayerMove(int newX, int newY);
}
```

**Key Responsibilities**:
- Triggers enemy descent after player moves
- Handles collision detection
- Manages turn counter
- Spawns new enemies periodically

---

### 4. Application Layer - ViewModel (`Application\ViewModels\CombatViewModel.cs`)
**Purpose**: Bridges UI and business logic, manages combat screen state

```csharp
public class CombatViewModel : INotifyPropertyChanged
{
    // Properties bound to UI
    public Player Player { get; set; }
    public int PlayerGridX { get; }  // For binding to Grid.Column
    public int PlayerGridY { get; }  // For binding to Grid.Row
  
    // Commands
    public ICommand MovePlayerCommand { get; }
    
    // Methods
    public void MovePlayer(int direction);
}
```

**Key Responsibilities**:
- Exposes player position for UI binding
- Implements `INotifyPropertyChanged` for data binding
- Coordinates between GridService and GameFlowService
- Provides commands for user actions

**Data Flow**:
1. User presses arrow key
2. View calls `MovePlayer(direction)`
3. ViewModel validates and calls `GridService.MovePlayer()`
4. ViewModel calls `GameFlowService.ProcessPlayerMove()`
5. ViewModel raises `PropertyChanged` events
6. UI updates via data binding

---

### 5. Presentation Layer - View Code-Behind (`Presentation\Views\Game\CombatScreen.xaml.cs`)
**Purpose**: Handles keyboard input and focuses management

```csharp
public partial class CombatScreen : UserControl
{
    private bool _keyPressed = false;
    
    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        if (_keyPressed) return; // Prevent continuous movement
    
   if (e.Key == Key.Left)
        {
    _viewModel.MovePlayer(-1);
            _keyPressed = true;
   }
        else if (e.Key == Key.Right)
{
            _viewModel.MovePlayer(1);
            _keyPressed = true;
      }
    }
    
    private void OnKeyUp(object sender, KeyEventArgs e)
    {
 _keyPressed = false;
    }
}
```

**Key Responsibilities**:
- Captures keyboard input (Left/Right arrow keys)
- Prevents continuous movement on key hold
- Translates key presses to directional commands
- Manages keyboard focus

---

### 6. Presentation Layer - XAML View (`Presentation\Views\Game\CombatScreen.xaml`)
**Purpose**: Visual representation of the combat grid and player sprite

```xml
<!-- Grid Container: 8 columns × 10 rows -->
<Grid Width="800" Height="1000">
    <!-- Grid tiles (80 Border elements) -->
 
  <!-- Player Sprite (dynamically positioned) -->
    <Border 
  Grid.Row="{Binding PlayerGridY}" 
        Grid.Column="{Binding PlayerGridX}"
        Background="#4CAF50">
        <TextBlock Text="P" />
    </Border>
</Grid>
```

**Key Features**:
- 8x10 grid (8 columns, 10 rows, each 100x100px)
- Player sprite uses data binding to `PlayerGridX` and `PlayerGridY`
- Grid.Row and Grid.Column automatically position the sprite
- Responsive to ViewModel property changes

---

## Data Flow Diagram

```
???????????????????????????????????????????????????????????????
?           USER INPUT      ?
?  (Keyboard: ? or ?)         ?
???????????????????????????????????????????????????????????????
      ?
     ?
???????????????????????????????????????????????????????????????
?          CombatScreen.xaml.cs     ?
?          (Presentation Layer - Code-Behind)       ?
?          ?
?  OnKeyDown(e) { ?
?    if (e.Key == Left)  ? _viewModel.MovePlayer(-1)         ?
?    if (e.Key == Right) ? _viewModel.MovePlayer(+1) ?
?  }   ?
???????????????????????????????????????????????????????????????
        ?
           ?
???????????????????????????????????????????????????????????????
?       CombatViewModel      ?
?          (Application Layer - ViewModel)            ?
?           ?
?  MovePlayer(direction) {       ?
?    newX = player.X + direction   ?
?    if (valid bounds) ?
?      ??? GridService.MovePlayer(newX, Y)        ?
?      ??? GameFlowService.ProcessPlayerMove(newX, Y)     ?
?      ??? OnPropertyChanged("PlayerGridX")     ?
?  }     ?
????????????????????????????????????????????????????????????????
     ? ?
     ?  ?
??????????????????????      ??????????????????????????????????
?   GridService      ?      ?   GameFlowService   ?
?   (Validates)      ?      ?   (Turn Processing)         ?
?   - Boundaries     ?      ?   - Enemy Descent  ?
?   - Occupancy      ?      ?   - Collision Detection        ?
?   - Updates Grid   ?      ?   - Turn Counter     ?
??????????????????????   ??????????????????????????????????
   ?
     ?
???????????????????????????????????????????????????????????????
?         Player Entity   ?
?       (Domain Layer)       ?
?   - X, Y coordinates updated   ?
???????????????????????????????????????????????????????????????
     ?
       ? (PropertyChanged Event)
   ?
???????????????????????????????????????????????????????????????
?       CombatScreen.xaml               ?
?  (Presentation Layer - XAML)           ?
?           ?
?  <Border Grid.Row="{Binding PlayerGridY}"  ?
?          Grid.Column="{Binding PlayerGridX}">    ?
?  <TextBlock Text="P" />    ?
?  </Border>         ?
?          ?
?  ? UI updates automatically via data binding    ?
???????????????????????????????????????????????????????????????
```

---

## Dependency Injection Configuration

**File**: `App.xaml.cs`

```csharp
services.AddSingleton<MainViewModel>();
services.AddSingleton<CombatViewModel>();
services.AddSingleton<GameFlowService>();
services.AddSingleton<GridService>();
```

**Injection Flow**:
1. `GridService` and `GameFlowService` are created as singletons
2. `CombatViewModel` receives both services via constructor injection
3. `MainViewModel` receives `CombatViewModel`
4. `MainWindow` receives `MainViewModel`
5. `CombatScreen` binds to `CombatViewModel` via DataContext

---

## Key Design Decisions

### 1. **Why Discrete Movement?**
The `_keyPressed` flag ensures one key press = one tile movement. This prevents:
- Rapid continuous movement on key hold
- Unintended movements
- Turn-based gameplay breaking

### 2. **Why Grid Coordinates Instead of Pixels?**
- Simplifies collision detection
- Aligns with turn-based game mechanics
- Makes grid state management easier
- Separates game logic from rendering

### 3. **Why MVVM Pattern?**
- **Testability**: ViewModels can be unit tested without UI
- **Separation of Concerns**: UI logic separate from business logic
- **Data Binding**: Automatic UI updates reduce code complexity
- **Maintainability**: Changes to UI don't affect game logic

### 4. **Why Two Services (Grid + GameFlow)?**
- **Single Responsibility**: Each service has one clear purpose
- **Reusability**: GridService can be used by other systems (AI, etc.)
- **Testability**: Can test grid logic independently of game flow

---

## Extension Points

### Adding New Movement Types
To add up/down or diagonal movement:

1. Update `OnKeyDown` in `CombatScreen.xaml.cs`:
```csharp
if (e.Key == Key.Up)
    _viewModel.MovePlayer(0, -1);
```

2. Modify `MovePlayer` in `CombatViewModel.cs` to accept X and Y deltas:
```csharp
public void MovePlayer(int deltaX, int deltaY)
{
    int newX = _player.X + deltaX;
    int newY = _player.Y + deltaY;
    // ... validation and movement
}
```

### Adding Movement Animations
1. Use WPF animations in XAML
2. Trigger on `PropertyChanged` event
3. Animate `RenderTransform` from old to new position

### Adding Movement Validation Rules
Extend `GridService.MovePlayer()`:
```csharp
public bool MovePlayer(int newX, int newY)
{
    if (!IsValidPosition(newX, newY)) return false;
    if (IsTileOccupied(newX, newY)) return false;
  if (IsTileTrap(newX, newY)) { TriggerTrap(); return false; }
    // ... more rules
}
```

---

## Common Issues and Solutions

### Issue: Player sprite doesn't move
**Solution**: Check that:
1. DataContext is set correctly on CombatScreen
2. PlayerGridX/PlayerGridY properties raise PropertyChanged
3. Grid.Row and Grid.Column bindings are correct

### Issue: Continuous movement on key hold
**Solution**: Verify `_keyPressed` flag is working and `OnKeyUp` is called

### Issue: Player moves off grid
**Solution**: Check `GridService.IsValidPosition()` logic

### Issue: Movement feels sluggish
**Solution**: Consider adding animations or transition effects
