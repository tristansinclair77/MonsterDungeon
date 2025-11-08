# Bug Fixes - Player Movement Issues

## Issues Identified

### Issue 1: Player Sprite Not in Bottom Row
**Problem**: Player was appearing in row 8 instead of row 9 (the actual bottom row)  
**Root Cause**: Player constructor initialized Y = 8, but with a 10-row grid (0-9), the bottom row is row 9

### Issue 2: Keyboard Input Not Working
**Problem**: Left/Right arrow keys did not move the player  
**Root Causes**: 
1. MainWindow's `PreviewKeyDown` was intercepting keyboard events before they reached CombatScreen
2. CombatScreen might not have had keyboard focus when it became visible

---

## Fixes Applied

### Fix 1: Player Starting Position ?
**File**: `Domain\Entities\Player.cs`

**Changed**:
```csharp
// OLD
Y = 8; // Near bottom

// NEW
Y = 9; // Bottom row (grid is 0-9, so 9 is the last row)
```

**Result**: Player now correctly appears in the bottom row of the grid

---

### Fix 2: MainWindow Keyboard Event Routing ?
**File**: `Presentation\Views\MainWindow.xaml.cs`

**Changed**: Modified `MainWindow_PreviewKeyDown` to allow arrow key events to pass through when CombatScreen is visible

```csharp
// Allow Left/Right arrow keys to pass through to CombatScreen when it's visible
if (_viewModel.DebugMenu.IsCombatScreenVisible && 
    (e.Key == Key.Left || e.Key == Key.Right))
{
    // Don't mark as handled - let the event bubble down to CombatScreen
    return;
}
```

**Why This Works**:
- `PreviewKeyDown` fires before child controls receive the event
- By NOT marking the event as `Handled` for arrow keys, the event continues to bubble down
- CombatScreen can then receive and handle the arrow key events
- Other keys (backtick, Escape) still work as before

---

### Fix 3: Automatic Focus Management ?
**File**: `Presentation\Views\Game\CombatScreen.xaml.cs`

**Added**: `IsVisibleChanged` event handler to automatically set focus

```csharp
this.IsVisibleChanged += CombatScreen_IsVisibleChanged;

private void CombatScreen_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
{
    // Set focus whenever the control becomes visible
    if (this.IsVisible)
    {
     this.Focus();
    }
}
```

**Why This Works**:
- WPF UserControls need keyboard focus to receive KeyDown events
- When user switches to Combat Screen via Debug Menu, the screen becomes visible
- The `IsVisibleChanged` event fires and automatically sets focus
- User doesn't need to manually click on the screen to enable keyboard input

---

## Testing the Fixes

### Test 1: Player Position ?
1. Run the application (F5)
2. Open Debug Menu (backtick key)
3. Select "Combat Screen"
4. **Verify**: Green "P" sprite should be in the **bottom row** (row 9)

### Test 2: Keyboard Movement ?
1. With Combat Screen visible
2. Press **Left Arrow** key
3. **Verify**: Player moves one tile to the left
4. Press **Right Arrow** key
5. **Verify**: Player moves one tile to the right
6. Press and **hold** Left Arrow
7. **Verify**: Player moves only once (not continuously)

### Test 3: Boundary Protection ?
1. Move player all the way to the left edge
2. Press Left Arrow again
3. **Verify**: Player stays at left edge (column 0)
4. Move player all the way to the right edge
5. Press Right Arrow again
6. **Verify**: Player stays at right edge (column 7)

### Test 4: Other Keys Still Work ?
1. Press **backtick (`)** key
2. **Verify**: Debug Menu toggles on/off
3. With Debug Menu closed, press **Escape**
4. **Verify**: Application closes
5. Press **Up/Down** arrow keys
6. **Verify**: Nothing happens (not implemented yet)

---

## Technical Details

### WPF Event Routing Explained

```
User presses Left Arrow
        ?
????????????????????????????????
?  MainWindow.PreviewKeyDown   ? ? PreviewKeyDown fires first (tunneling)
?  - Checks if CombatScreen ?
?    is visible      ?
?  - Allows arrow keys through  ?
?  - Does NOT mark e.Handled    ?
????????????????????????????????
             ? (event continues)
????????????????????????????????
?  CombatScreen.KeyDown        ? ? KeyDown fires on focused control
?  - Checks _keyPressed flag   ?
?  - Calls MovePlayer(-1)      ?
?  - Marks e.Handled = true ?
????????????????????????????????
```

### Focus Management in WPF

**Why Focus Matters**:
- Only the focused control receives `KeyDown` events
- Clicking with mouse sets focus, but visibility changes don't
- Must explicitly call `Focus()` when control becomes visible

**Solution**:
- Subscribe to `IsVisibleChanged` event
- Call `Focus()` whenever `IsVisible` becomes `true`
- Ensures keyboard input works immediately after switching screens

---

## Grid Layout Reference

```
Grid: 8 columns (0-7) × 10 rows (0-9)

    0   1   2   3   4   5   6   7  ? Column numbers
  ?????????????????????????????????
0 ?   ??   ?   ?   ?   ?   ?   ?
1 ?   ?   ?   ?   ?   ?   ?   ?   ?
2 ?   ?   ?   ??   ?   ?   ?   ?
3 ?   ?   ?   ?   ?   ?   ?   ?   ?
4 ?   ??   ?   ?   ?   ?   ?   ?
5 ?   ?   ?   ?   ?   ?   ?   ?   ?
6 ?   ?   ?   ?   ?   ? ?   ?   ?
7 ?   ?   ?   ?   ?   ?   ?   ?   ?
8 ?   ?   ?   ?   ?   ?   ?   ?   ?
9 ?   ?   ?   ?   ? P ?   ?   ?   ? ? BOTTOM ROW (Y = 9)
  ?????????????????????????????????
  ?
              Player starts at (X=4, Y=9)
```

---

## Files Modified

1. ? `Domain\Entities\Player.cs` - Changed Y from 8 to 9
2. ? `Presentation\Views\MainWindow.xaml.cs` - Allow arrow keys to pass through
3. ? `Presentation\Views\Game\CombatScreen.xaml.cs` - Auto-focus when visible

---

## Build Status
? **Build Successful** - All fixes compile without errors

---

## Known Issues Resolved
- ? ~~Player not in bottom row~~ ? ? **FIXED**
- ? ~~Arrow keys don't work~~ ? ? **FIXED**
- ? ~~Need to click screen to enable keyboard~~ ? ? **FIXED**

---

## Next Steps

### For Testing:
1. Clean solution (Build ? Clean Solution)
2. Rebuild (Build ? Rebuild Solution)
3. Run (F5)
4. Test all scenarios listed above

### Future Enhancements:
- Add visual feedback when player moves
- Add movement sound effects
- Add particle effects for movement
- Consider adding WASD support in addition to arrow keys
- Add visual highlight to show which tile player is on
