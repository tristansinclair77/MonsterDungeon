# Quick Start Guide - Testing Enemy Behavior

## Immediate Next Steps

### What I've Completed ?
1. ? Added enemy spawning logic (random 1-3 enemies every 1-3 moves)
2. ? Implemented enemy descent (enemies move down 1 tile per player move)
3. ? Added enemy removal at player row (enemies disappear instead of colliding)
4. ? Created visual rendering system (red circles with "E" on grid)
5. ? Verified build compiles successfully

### What You Need to Do Next ??

#### Step 1: Run the Application
1. Press **F5** in Visual Studio (or click the green Start button)
2. Wait for the application to launch

#### Step 2: Navigate to Combat Screen
- If you're not already on the Combat Screen, navigate there through the menu system

#### Step 3: Test the Implementation
Use your **keyboard arrow keys** to move the player:
- **LEFT ARROW** = Move player left
- **RIGHT ARROW** = Move player right

#### Step 4: Observe Enemy Behavior
After 1-3 moves, you should see:
- ?? Red circles with "E" appearing at the top row
- Enemies moving down one tile each time you move
- Enemies disappearing when they reach the bottom (player row)

## Expected Visual Result

```
Row 0: [Empty] [Enemy] [Empty] [Enemy] [Empty] [Empty] [Empty] [Empty]
Row 1: [Empty] [Empty] [Empty] [Empty] [Empty] [Empty] [Empty] [Empty]
Row 2: [Enemy] [Empty] [Empty] [Empty] [Empty] [Empty] [Empty] [Empty]
Row 3: [Empty] [Empty] [Empty] [Empty] [Empty] [Empty] [Empty] [Empty]
Row 4: [Empty] [Empty] [Empty] [Empty] [Empty] [Empty] [Empty] [Empty]
Row 5: [Empty] [Empty] [Empty] [Empty] [Empty] [Empty] [Empty] [Empty]
Row 6: [Empty] [Empty] [Empty] [Empty] [Empty] [Empty] [Empty] [Empty]
Row 7: [Empty] [Empty] [Empty] [Empty] [Empty] [Empty] [Empty] [Empty]
Row 8: [Empty] [Empty] [Empty] [Empty] [Empty] [Empty] [Empty] [Empty]
Row 9: [Empty] [Empty] [Empty] [Player] [Empty] [Empty] [Empty] [Empty]
```

## Quick Verification Checklist

After running the application, verify these behaviors:

### ? Player Movement
- [ ] Player moves left when LEFT arrow pressed
- [ ] Player moves right when RIGHT arrow pressed
- [ ] Player stops at grid edges (doesn't go off-screen)
- [ ] Movement is single-tap (not continuous)

### ? Enemy Spawning
- [ ] Enemies appear at top row after 1-3 player moves
- [ ] 1 to 3 enemies spawn each time
- [ ] Enemies appear at random horizontal positions
- [ ] Timing varies (sometimes 1 move, sometimes 2-3)

### ? Enemy Descent
- [ ] All enemies move down by 1 tile when player moves
- [ ] Enemies move synchronously (all at the same time)
- [ ] Enemies maintain their horizontal position while descending

### ? Enemy Removal
- [ ] Enemies disappear when they reach the player row (bottom)
- [ ] No error messages appear
- [ ] New enemies continue to spawn after removal

## Troubleshooting

### Problem: Application won't run
**Solution**: Make sure you've saved all files (Ctrl+Shift+S) and rebuild (Ctrl+Shift+B)

### Problem: Keyboard input not working
**Solution**: Click on the combat grid area to ensure it has focus

### Problem: No enemies appearing
**Solution**: 
1. Move the player at least 3 times using arrow keys
2. Enemies spawn randomly between 1-3 moves, so keep moving

### Problem: Enemies not moving
**Solution**: Each player move triggers enemy movement. Make sure you're moving the player with arrow keys.

### Problem: Enemies not disappearing
**Solution**: Keep playing until enemies reach row 9 (the player row). It takes 9 player moves for an enemy to travel from top to bottom.

## What to Report Back

When you've tested the implementation, let me know:

1. ? **Success**: "Everything works as expected"
2. ?? **Partial Success**: "Works but [describe issue]"
3. ? **Issue**: "Doesn't work - [describe what's happening]"

## Files Modified (For Reference)

1. `Application/ViewModels/CombatViewModel.cs` - Enemy logic
2. `Presentation/Views/Game/CombatScreen.xaml` - Enemy visuals
3. `ENEMY_BEHAVIOR_TESTING.md` - Detailed testing guide
4. `ENEMY_BEHAVIOR_IMPLEMENTATION.md` - Technical documentation

## Next Steps After Testing

Once you confirm everything works:
1. I can enhance enemy visuals (different sprites, animations)
2. I can add collision/combat mechanics
3. I can implement different enemy types
4. I can add sound effects and visual effects

---

## ?? Ready to Test!

**Current Status**: ? Build Successful - Ready for Runtime Testing

**Your Action Required**: Run the application (F5) and test with keyboard arrow keys

**When you're done testing**: Reply with "continue" and let me know how it went!
