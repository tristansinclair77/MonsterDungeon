# Application Layer

## Purpose
The Application layer orchestrates the game flow, manages application state, and coordinates interactions between the Domain and Infrastructure layers. It implements the use cases and game logic flow.

## Principles
- **Orchestration** - Coordinates between Domain and Infrastructure
- **State Management** - Manages application and game state
- **Use Case Implementation** - Implements game scenarios and workflows
- **MVVM Support** - Provides ViewModels for the Presentation layer

## Contents

### Models
Application-specific data transfer objects and models:
- DTOs for communication between layers
- Application-specific data structures
- Game state snapshots
- Configuration models

### Services
Application coordination services:
- **GameLoopService** - Main game loop coordination
- **GameStateManager** - Manages current game state
- **DungeonService** - Dungeon management and progression
- **InventoryService** - Player inventory management
- **SaveGameService** - Coordinates save/load operations

### Commands
Command pattern implementations for user actions:
- **MovePlayerCommand** - Handle player movement
- **AttackCommand** - Execute combat actions
- **UseItemCommand** - Use items from inventory
- **CastSpellCommand** - Cast magic spells
- **SaveGameCommand** - Save current game
- **LoadGameCommand** - Load saved game

### Queries
Query handlers for retrieving information:
- **GetPlayerStatsQuery** - Retrieve player statistics
- **GetInventoryQuery** - Get current inventory
- **GetDungeonMapQuery** - Retrieve dungeon layout
- **GetEnemiesInRangeQuery** - Find nearby enemies

### ViewModels
MVVM ViewModels for UI binding:
- **MainGameViewModel** - Main game view state
- **PlayerViewModel** - Player information display
- **InventoryViewModel** - Inventory management UI
- **CombatViewModel** - Combat screen state
- **MapViewModel** - Dungeon map display

## Design Patterns
- **Command Pattern** - For user actions
- **Query Pattern** - For data retrieval (CQRS lite)
- **Observer Pattern** - For state change notifications
- **MVVM** - For UI binding and separation

## Dependencies
- **Domain** - For business logic and entities
- **Infrastructure** - For data persistence and external services

## Communication Flow
```
User Input ? ViewModel ? Command/Query ? Application Service ? Domain Service ? Entity
```
