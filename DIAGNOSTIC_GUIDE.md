# Diagnostic Guide - Enemy Behavior Troubleshooting

## Quick Verification Steps

### Test 1: Verify Spawning Works
1. Run application (F5)
2. Navigate to Combat Screen
3. Press LEFT or RIGHT arrow key **exactly 3 times**
4. **Expected**: You should see 1-3 red circles with "E" appear at the top row

**If enemies appear**: ? Spawning works!
**If no enemies**: See "Spawning Issues" section below

---

### Test 2: Verify Movement Works
1. After enemies spawn (from Test 1)
2. Press arrow key 1 more time
3. **Expected**: All enemies should move down by 1 row

**If enemies move**: ? Movement works!
**If enemies don't move**: See "Movement Issues" section below

---

### Test 3: Verify Continuous Gameplay
1. Continue pressing arrow keys
2. **Expected**: 
   - More enemies spawn after 1-3 moves
   - All enemies keep descending
   - Enemies disappear at bottom row

**If all works**: ? Complete success!
**If issues**: See debugging sections below

---

## Debugging: Spawning Issues

### Symptom: No enemies appear after 3 moves

#### Check 1: Verify XAML Binding
The ItemsControl must be bound to the Enemies collection:

```xaml
<ItemsControl ItemsSource="{Binding Enemies}" x:Name="EnemyContainer">
```

**Location**: `Presentation/Views/Game/CombatScreen.xaml`
**Look for**: Enemy rendering section after Player sprite

#### Check 2: Verify DataContext
The CombatScreen must have CombatViewModel as DataContext.

**How to verify**: 
- Check `Presentation/Views/MainWindow.xaml.cs`
- Or check where CombatScreen is instantiated

#### Check 3: Add Debug Output
Add to `TrySpawnEnemies()` method:

```csharp
private void TrySpawnEnemies()
{
    System.Diagnostics.Debug.WriteLine($"MoveCounter: {_moveCounter}, Threshold: {_nextSpawnThreshold}");
  
    if (_moveCounter >= _nextSpawnThreshold)
    {
        System.Diagnostics.Debug.WriteLine($"Spawning enemies! Count before: {_enemies.Count}");
  
        int spawnCount = _random.Next(1, 4);
        System.Diagnostics.Debug.WriteLine($"Attempting to spawn {spawnCount} enemies");
        
        // ...rest of spawn logic...
        
  System.Diagnostics.Debug.WriteLine($"Count after spawn: {_enemies.Count}");
    }
}
```

**Check Output Window** in Visual Studio while playing.

---

## Debugging: Movement Issues

### Symptom: Enemies spawn but don't move

#### Check 1: Verify INotifyPropertyChanged
Ensure `Enemy.cs` has:
```csharp
public class Enemy : INotifyPropertyChanged
{
    private int _y;
 
public int Y
    {
      get => _y;
     set
        {
            if (_y != value)
   {
       _y = value;
     OnPropertyChanged();
   }
        }
    }
    
    public event PropertyChangedEventHandler PropertyChanged;
    
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
  PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
```

#### Check 2: Verify ProcessEnemyMovement is Called
Add debug output to `MovePlayer()`:

```csharp
public void MovePlayer(int direction)
{
    // ...existing code...
    
    if (_gridService.MovePlayer(newX, _player.Y))
    {
        System.Diagnostics.Debug.WriteLine("Player moved! Processing enemies...");
        ProcessEnemyMovement();
   System.Diagnostics.Debug.WriteLine($"Enemies after processing: {_enemies.Count}");
    }
}
```

#### Check 3: Verify Enemy Position Updates
Add to `ProcessEnemyMovement()`:

```csharp
private void ProcessEnemyMovement()
{
    _moveCounter++;
    TrySpawnEnemies();
    
    System.Diagnostics.Debug.WriteLine($"Processing {_enemies.Count} enemies");

    for (int i = _enemies.Count - 1; i >= 0; i--)
    {
        var enemy = _enemies[i];
     int newY = enemy.Y + 1;
        
        System.Diagnostics.Debug.WriteLine($"Enemy {i}: Moving from Y={enemy.Y} to Y={newY}");
        
        if (newY >= GridService.GridHeight - 1)
        {
    System.Diagnostics.Debug.WriteLine($"Enemy {i}: Removed (reached player row)");
      _enemies.RemoveAt(i);
    continue;
        }
        
        enemy.Y = newY;
    }
}
```

---

## Debugging: XAML Binding Issues

### Symptom: Data is updating but UI doesn't show changes

#### Check 1: Verify ItemsControl Configuration
Ensure your XAML has:

```xaml
<ItemsControl ItemsSource="{Binding Enemies}" x:Name="EnemyContainer">
    <ItemsControl.ItemsPanel>
        <ItemsPanelTemplate>
          <Grid/>
        </ItemsPanelTemplate>
    </ItemsControl.ItemsPanel>
    <ItemsControl.ItemContainerStyle>
        <Style>
            <Setter Property="Grid.Row" Value="{Binding Y}"/>
            <Setter Property="Grid.Column" Value="{Binding X}"/>
        </Style>
    </ItemsControl.ItemContainerStyle>
 <ItemsControl.ItemTemplate>
        <DataTemplate>
            <Border Background="#F44336" CornerRadius="45" Margin="10" 
 BorderBrush="#E57373" BorderThickness="3">
    <TextBlock Text="E" FontSize="48" FontWeight="Bold" 
      Foreground="White" HorizontalAlignment="Center" 
     VerticalAlignment="Center"/>
            </Border>
        </DataTemplate>
    </ItemsControl.ItemTemplate>
</ItemsControl>
```

#### Check 2: Verify Grid Definitions
The parent Grid must have proper Row/Column definitions (0-9 rows, 0-7 columns).

#### Check 3: Check Output Window for Binding Errors
Look for messages like:
- "BindingExpression path error"
- "Cannot find governing FrameworkElement"

---

## Common Fixes

### Fix 1: Clean and Rebuild
1. Close Visual Studio
2. Delete `bin` and `obj` folders
3. Reopen solution
4. Build ? Clean Solution
5. Build ? Rebuild Solution
6. Run (F5)

### Fix 2: Reset Debugging
1. Stop debugging
2. Debug ? Delete All Breakpoints
3. Start fresh (F5)

### Fix 3: Check Keyboard Focus
1. When application runs, **click on the combat grid**
2. Then press arrow keys
3. Focus must be on the control for keyboard input

---

## Advanced Diagnostics

### Enable Detailed Binding Logging
Add to `App.xaml.cs` in `OnStartup`:

```csharp
protected override void OnStartup(StartupEventArgs e)
{
    PresentationTraceSources.DataBindingSource.Switch.Level = SourceLevels.All;
    base.OnStartup(e);
}
```

Add using:
```csharp
using System.Diagnostics;
```

### Monitor ObservableCollection Changes
Add to CombatViewModel constructor:

```csharp
_enemies.CollectionChanged += (s, e) =>
{
    System.Diagnostics.Debug.WriteLine($"Enemies collection changed: {e.Action}, Count: {_enemies.Count}");
};
```

---

## Expected Debug Output

### Normal Gameplay Output:
```
Player moved! Processing enemies...
MoveCounter: 1, Threshold: 2
Processing 0 enemies
Enemies after processing: 0

Player moved! Processing enemies...
MoveCounter: 2, Threshold: 2
Spawning enemies! Count before: 0
Attempting to spawn 3 enemies
Count after spawn: 3
Processing 3 enemies
Enemy 2: Moving from Y=0 to Y=1
Enemy 1: Moving from Y=0 to Y=1
Enemy 0: Moving from Y=0 to Y=1
Enemies after processing: 3
```

---

## If Still Having Issues

### Provide This Information:
1. **What you see**: Describe exactly what happens
2. **Debug output**: Copy from Output window
3. **Steps taken**: What did you try?
4. **Errors**: Any error messages or exceptions?

### Quick Diagnostic Test:
Add this button to test spawning directly:

```xaml
<Button Content="Test Spawn" Click="TestSpawn_Click"/>
```

```csharp
private void TestSpawn_Click(object sender, RoutedEventArgs e)
{
    var vm = DataContext as CombatViewModel;
    if (vm != null)
    {
        var enemy = new Enemy { X = 3, Y = 0 };
      vm.Enemies.Add(enemy);
        MessageBox.Show($"Enemy added! Total: {vm.Enemies.Count}");
    }
}
```

If this works but arrow keys don't, the issue is with input handling, not rendering.

---

## Success Criteria

After fixes, you should see:
- ? Enemies spawn after 1-3 moves
- ? 1-3 enemies per spawn wave
- ? Enemies at random X positions
- ? All enemies descend simultaneously
- ? Enemies disappear at bottom row
- ? New waves spawn continuously

---

**Status**: Ready for testing with full diagnostic support
**Next**: Run application and report results!
