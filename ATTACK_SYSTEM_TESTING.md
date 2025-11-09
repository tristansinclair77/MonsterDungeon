# Attack System Implementation - Testing Guide

## What Was Implemented

### New Features
1. **Bullet Entity** - New `Bullet.cs` class in `Domain\Entities`
   - Tracks X, Y position with INotifyPropertyChanged
   - Has a Damage property (set to 10)
   - Updates UI automatically when position changes

2. **Attack Button Functionality**
   - Click the "Attack" button to fire a bullet
   - Bullet spawns at the player's position (UPDATED)
   - Bullet travels upward automatically, moving one row per tick

3. **Bullet Animation System**
   - Uses DispatcherTimer for smooth 50ms frame updates
   - Bullets move up one tile per frame
   - Timer automatically stops when no bullets remain

4. **Collision Detection**
   - Bullets check for enemies at spawn position (current tile)
   - Bullets check for enemies in the next tile before moving
   - On collision: both bullet and enemy are removed
   - Collision logged to debug output

5. **Visual Rendering**
   - Yellow circular bullet sprite
   - Renders on top of enemies (ZIndex=3)
   - Positioned using the same GridToPixelConverter as enemies

## Recent Fixes

### ? Fixed Bullet Spawn Position (LATEST)
**Problem**: Bullets were appearing two rows above the player, skipping row Y=8
**Root Cause**: Bullet spawned at `player.Y - 1` (Y=8), then immediately moved to Y=7 on first timer tick before UI could render
**Solution**: Changed spawn position from `player.Y - 1` to `player.Y`
- Bullets now spawn at player position
- First movement takes bullet to Y-1 (one row above)
- No rows are skipped
- Can now hit enemies at any distance, including player's row

### ? Fixed Enemy Despawn Logic
**Problem**: Enemies were despawning one row above the player (at GridHeight - 1)
**Solution**: Changed check from `if (newY >= GridService.GridHeight - 1)` to `if (newY > _player.Y)`
- Enemies now correctly despawn when they reach the player's actual row
- Added debug logging to track enemy removal

### ? Fixed Bullet Spawn Collision
**Problem**: Bullets spawning on enemies in lower rows didn't remove them
**Solution**: Added collision check at bullet's current position before movement
- Checks for collision immediately after spawning
- Removes enemy if bullet spawns on same tile
- Prevents bullets from "phasing through" enemies

## How to Test

### Basic Attack Test
1. **Launch the game** and enter combat mode
2. **Wait for enemies to spawn** on the top row (after 1-3 player moves)
3. **Move the player** left/right using arrow keys to align with an enemy column
4. **Click the "Attack" button**
5. **Expected Result**: 
   - A yellow bullet appears at the player's position
   - Bullet travels upward quickly
   - If aligned with an enemy, both disappear on contact

### Close-Range Combat Test (NEW - UPDATED)
1. Position player in a column with an enemy
2. **Let enemy descend to row Y=8** (one row above player)
3. **Click "Attack" button**
4. **Expected Result**:
   - Bullet spawns at player position (Y=9)
   - Bullet immediately moves to Y=8
   - Enemy at Y=8 is destroyed ?
   - **No more skipping rows!**

### Spawn Collision Test
1. Position player in a column with an enemy
2. **Wait for enemy to descend** to the player's row (Y=9)
3. **Click "Attack" button**
4. **Expected Result**:
   - Bullet spawns at player position and immediately collides with enemy
   - Both bullet and enemy removed instantly
   - Debug message shows "Bullet hit enemy at spawn position"

### Enemy Despawn Test (FIXED)
1. Let an enemy descend without shooting it
2. **Watch as enemy approaches** player row
3. **Expected Result**:
   - Enemy appears on player's row (Y=9)
   - On next player move, enemy tries to move to Y=10 and is removed
   - Enemy does NOT disappear before reaching player's tile

### Rapid Fire Test
1. Position player in a column with multiple enemies
2. **Click "Attack" multiple times quickly**
3. **Expected Result**:
   - Multiple bullets spawn and travel upward
   - Each bullet can destroy one enemy
   - Bullets that miss enemies disappear at the top of the screen

### Edge Cases to Test
1. **Fire with no enemies** - Bullet should travel to top and disappear
2. **Fire from different columns** - Bullets only hit enemies in the same column
3. **Multiple bullets, one enemy** - First bullet removes enemy, second continues upward
4. **Bullet near screen top** - Should cleanly remove from collection
5. **Enemy at player.Y (same row), fire attack** - Should remove enemy immediately ?
6. **Enemy at player.Y - 1, fire attack** - Should hit on first movement ?

## Debug Output
Watch the Output window for these messages:
- `"Bullet fired from player position X=<column>, Y=<row>"` (UPDATED)
- `"Bullet hit enemy at spawn position X=<column>, Y=<row>"`
- `"Bullet hit enemy at X=<column>, Y=<row>"`
- `"Enemy removed - collided with player: X=<column>, Y=<oldY> -> <newY>"`
- `"Enemy spawned at X=<column>, Y=0 (TOP ROW)"`

## Technical Details

### Bullet Movement Speed
- Timer interval: 50ms (20 frames per second)
- Movement: 1 grid cell per frame
- Full screen traversal: ~500ms (0.5 seconds)

### Bullet Spawn Logic (UPDATED)
```csharp
// Bullet spawns at player position
var bullet = new Bullet
{
    X = _player.X,
  Y = _player.Y  // Starts here, moves up on first tick
};
```

**Movement Timeline:**
- **T=0ms**: Spawn at player.Y (e.g., Y=9)
- **T=50ms**: Move to Y=8
- **T=100ms**: Move to Y=7
- **T=150ms**: Move to Y=6
- ...continues until off-screen (Y < 0)

### Collision Logic (UPDATED)
- **Phase 1**: Checks current position (handles spawn collisions and player-row enemies)
- **Phase 2**: Checks next position before moving (handles normal collisions)
- Only destroys enemies on exact grid coordinate match
- Removes both entities immediately on collision

### Enemy Despawn Logic (FIXED)
- Checks if `newY > _player.Y` instead of hardcoded grid height
- Dynamically adjusts to player position
- Enemies appear on player's row before removal

### Performance
- Timer only runs when bullets exist
- No performance impact when idle
- Efficient ObservableCollection updates

## Next Steps / Potential Enhancements
- Add bullet damage to enemy health (instead of instant kill)
- Add cooldown timer to prevent spam
- Add mana/ammo cost per bullet
- Add visual effects on collision
- Add bullet sound effects
- Add different bullet types/weapons
- Add bullet spread/multi-shot abilities
- Add player damage when enemy reaches player row

## Known Issues
~~Bullets spawning on enemies don't remove them~~ ? FIXED
~~Enemies despawn one row too early~~ ? FIXED
~~Bullets spawn two rows above player~~ ? FIXED

**No known issues currently.** If problems occur:
1. Check that `Bullet.cs` is included in the project
2. Verify XAML has the BulletContainer ItemsControl
3. Check Output window for debug messages
4. Verify player Y position is correctly set (default Y=9 for bottom row)
5. Verify bullets appear at player position before moving upward
