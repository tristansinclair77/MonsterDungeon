# Enemy Despawn Fix - Visual Guide

## The Problem: >= vs >

### What Was Happening (WRONG)
```csharp
if (newY >= _player.Y)  // Using >= (greater than or equal)
```

**Visual Timeline:**
```
Turn 1: Enemy at Y=8, Player at Y=9
???????????????????
? Row 7: [ ][ ]  ?
? Row 8: [ ][E]  ?  ? Enemy here
? Row 9: [ ][P]  ?  ? Player here
???????????????????

Player moves ? Enemy tries to descend
  - enemy.Y = 8
  - newY = enemy.Y + 1 = 9
  - Check: if (9 >= 9) ? TRUE ?
  - Enemy REMOVED before moving
  
Turn 2: Enemy gone!
???????????????????
? Row 7: [ ][ ]  ?
? Row 8: [ ][ ]  ?  ? Enemy removed here
? Row 9: [ ][P]  ?  ? Enemy never reached player
???????????????????
```

**Result**: ? Enemy disappears one row above player

---

## The Solution: Use >

### What Should Happen (CORRECT)
```csharp
if (newY > _player.Y)  // Using > (greater than only)
```

**Visual Timeline:**
```
Turn 1: Enemy at Y=8, Player at Y=9
???????????????????
? Row 7: [ ][ ]  ?
? Row 8: [ ][E]  ?  ? Enemy here
? Row 9: [ ][P]  ?  ? Player here
???????????????????

Player moves ? Enemy descends
  - enemy.Y = 8
  - newY = enemy.Y + 1 = 9
  - Check: if (9 > 9) ? FALSE ?
  - Enemy NOT removed, continues to move
  - enemy.Y = 9 (updated)
  
Turn 2: Enemy ON player row!
???????????????????
? Row 7: [ ][ ]  ?
? Row 8: [ ][ ]  ?  
? Row 9: [ ][EP] ?  ? Enemy reached player row! ?
???????????????????

Player moves again ? Enemy tries to move past
  - enemy.Y = 9
  - newY = enemy.Y + 1 = 10
  - Check: if (10 > 9) ? TRUE ?
  - Enemy REMOVED (trying to go off-screen)

Turn 3: Enemy removed after being visible
???????????????????
? Row 7: [ ][ ]  ?
? Row 8: [ ][ ]  ?  
? Row 9: [ ][P]  ?  ? Enemy removed after collision
???????????????????
```

**Result**: ? Enemy appears on player row before removal

---

## Side-by-Side Comparison

### With >= (WRONG)
| Turn | Enemy Y | newY | Check (newY >= 9) | Result |
|------|---------|------|-------------------|--------|
| 1    | 8       | 9    | 9 >= 9 = TRUE ?  | REMOVED (too early!) |
| 2    | -       | - | -      | Gone|

### With > (CORRECT)
| Turn | Enemy Y | newY | Check (newY > 9) | Result |
|------|---------|------|------------------|--------|
| 1    | 8       | 9    | 9 > 9 = FALSE ? | Moves to Y=9 |
| 2    | 9    | 10   | 10 > 9 = TRUE ? | REMOVED (correct!) |

---

## Why This Matters

1. **Gameplay**: Players should SEE enemies reach their row before collision
2. **Visual Feedback**: Enemies disappearing "early" feels like a bug
3. **Game Feel**: Proper timing makes the threat feel real
4. **Future Features**: If you add collision damage, this ensures it triggers correctly

---

## Code Change

**Before:**
```csharp
if (newY >= _player.Y)  // 9 >= 9 is TRUE (removes too early)
{
    _enemies.RemoveAt(i);
    continue;
}
enemy.Y = newY;
```

**After:**
```csharp
if (newY > _player.Y)  // 9 > 9 is FALSE (allows movement)
{
    _enemies.RemoveAt(i);
    continue;
}
enemy.Y = newY;  // Enemy successfully moves to player row
```

---

## Testing Instructions

1. **Run the game**
2. **Move until an enemy spawns**
3. **Stop moving** and let the enemy descend toward you
4. **Watch carefully** as the enemy approaches the bottom row
5. **Expected**: Enemy should appear on your row (Y=9) for ONE TURN before disappearing
6. **Previously**: Enemy would vanish at Y=8 (row above you)

---

## Grid Reference

```
Y=0  [TOP ROW - Enemies spawn here]
Y=1  
Y=2  
Y=3  
Y=4  
Y=5  
Y=6  
Y=7  
Y=8  [Enemy should be visible here]
Y=9  [PLAYER ROW - Enemy should reach here before removal]
Y=10 [OFF GRID - Enemy tries to go here, gets removed]
```

The key insight: We want to remove enemies when they try to move to Y=10 (off-grid), not when they try to move to Y=9 (player's row).
