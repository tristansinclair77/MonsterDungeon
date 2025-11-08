# Player Movement Testing Checklist

## Pre-Test Setup
- [ ] Build solution successfully (verify no compilation errors)
- [ ] Run the application
- [ ] Navigate to Combat Screen (use Debug Menu with ` key if needed)

## Basic Movement Tests

### Test 1: Initial Position
- [ ] Player sprite "P" is visible on the grid
- [ ] Player is positioned in column 4 (center)
- [ ] Player is positioned in row 8 (near bottom)

### Test 2: Left Movement
- [ ] Press Left Arrow key once
- [ ] Player moves exactly one tile to the left
- [ ] Player position updates immediately
- [ ] No delay or lag in movement

### Test 3: Right Movement
- [ ] Press Right Arrow key once
- [ ] Player moves exactly one tile to the right
- [ ] Player position updates immediately
- [ ] No delay or lag in movement

### Test 4: Multiple Movements
- [ ] Press Left Arrow 3 times in succession
- [ ] Player moves 3 tiles to the left
- [ ] Press Right Arrow 2 times
- [ ] Player moves 2 tiles to the right

### Test 5: Left Boundary
- [ ] Move player all the way to the left (column 0)
- [ ] Press Left Arrow again
- [ ] Player stays at column 0 (doesn't go off grid)
- [ ] No errors or exceptions occur

### Test 6: Right Boundary
- [ ] Move player all the way to the right (column 7)
- [ ] Press Right Arrow again
- [ ] Player stays at column 7 (doesn't go off grid)
- [ ] No errors or exceptions occur

### Test 7: Key Hold Prevention
- [ ] Press and HOLD Left Arrow key
- [ ] Player should move only once, not continuously
- [ ] Release the key
- [ ] Press and HOLD Right Arrow key
- [ ] Player should move only once, not continuously

### Test 8: Rapid Key Tapping
- [ ] Rapidly tap Left Arrow key 5 times
- [ ] Player moves exactly 5 tiles (one per tap)
- [ ] No movement is lost or skipped

## Advanced Tests (If Implemented)

### Test 9: Turn Processing (Future)
- [ ] After each movement, enemies should descend one row
- [ ] Check if turn counter increments
- [ ] Verify collision detection works

### Test 10: Visual Feedback (Future)
- [ ] Tile highlighting on player position
- [ ] Movement animations
- [ ] Valid/invalid move indicators

## Bug Reporting
If any test fails, document:
- Test number and description
- Expected behavior
- Actual behavior
- Steps to reproduce
- Any error messages or console output

## Test Results Summary
Date: _____________
Tester: ___________
Total Tests: ______
Passed: ___________
Failed: ___________
Notes: ____________________________________________
___________________________________________________
___________________________________________________
