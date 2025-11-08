# Domain Layer

## Purpose
The Domain layer contains the core game logic, business rules, and entity definitions for Monster Dungeon. This layer is independent of any external frameworks or libraries and represents the heart of the application.

## Principles
- **Pure C# classes** - No dependencies on external frameworks
- **Framework-agnostic** - Can be reused in different application types
- **Business logic only** - Contains game rules and domain concepts
- **No infrastructure concerns** - No database, file I/O, or UI code

## Contents

### Entities
Core game objects that represent the fundamental concepts of the game:
- `Player` - Player character with stats, inventory, and abilities
- `Enemy` - Monster entities with behaviors and loot
- `Tile` - Dungeon tile information (floor, wall, trap, etc.)
- `Item` - Weapons, armor, potions, and other collectibles
- `Spell` - Magic abilities and their effects
- `Dungeon` - Dungeon structure and rooms
- `GameState` - Overall game state representation

### Enums
Game constants and type definitions:
- `EnemyType` - Types of monsters (Goblin, Orc, Dragon, etc.)
- `ItemType` - Categories of items (Weapon, Armor, Consumable, etc.)
- `TileType` - Types of dungeon tiles
- `SpellType` - Categories of magic spells
- `DamageType` - Physical, Fire, Ice, Lightning, etc.
- `Rarity` - Common, Uncommon, Rare, Epic, Legendary

### Interfaces
Contracts for domain services and behaviors:
- `ICombatService` - Combat calculation rules
- `ILootGenerator` - Loot generation algorithms
- `IDungeonGenerator` - Dungeon creation interface
- `IGameRules` - Core game rule validation

### Services
Pure domain logic implementations:
- **CombatService** - Damage calculations, hit chance, critical hits
- **LootService** - Item generation, rarity calculations
- **MovementService** - Player/enemy movement rules
- **EffectService** - Status effects and buffs/debuffs

## Design Guidelines
1. Keep entities simple and focused (Single Responsibility)
2. Use value objects for complex properties
3. Validate business rules within entities
4. Use domain events for important state changes
5. Keep services stateless when possible

## Dependencies
**None** - The Domain layer should not reference any other project layers.
