# Monster Dungeon

A dungeon-crawler game built with WPF and .NET Framework 4.7.2, following Clean Architecture principles.

## Project Overview

Monster Dungeon is a turn-based dungeon crawler where players navigate through procedurally generated dungeons, battle monsters, collect loot, and progress through increasingly challenging levels.

## Architecture

This project follows a layered architecture pattern to ensure separation of concerns, testability, and maintainability:

```
/MonsterDungeon
?
??? Domain           - Core game logic and business rules
??? Application      - Application services and coordination
??? Infrastructure   - External concerns (data, I/O)
??? Presentation     - WPF UI layer
```

### Layer Responsibilities

#### Domain Layer
**Purpose:** Contains the core game logic, combat rules, and business entities.

- **Entities:** Core game objects (Player, Enemy, Tile, Spell, Item, etc.)
- **Enums:** Game constants and enumerations (EnemyType, ItemType, etc.)
- **Interfaces:** Contracts for domain services
- **Services:** Pure domain logic (combat calculations, game rules)

**Dependencies:** None (pure C# classes)

#### Application Layer
**Purpose:** Orchestrates gameplay, manages game state, and coordinates between layers.

- **Models:** DTOs and application-specific models
- **Services:** Game loop, state management, coordination logic
- **Commands:** Command pattern implementations for user actions
- **Queries:** Query handlers for retrieving game state
- **ViewModels:** MVVM ViewModels for UI binding

**Dependencies:** Domain, Infrastructure

#### Infrastructure Layer
**Purpose:** Handles external concerns like data persistence, file I/O, and asset loading.

- **Persistence:** Save/load game state
- **Repositories:** Data access implementations
- **Services:** Asset loading, configuration management

**Dependencies:** Domain

#### Presentation Layer
**Purpose:** WPF user interface using MVVM pattern.

- **Views:** XAML views and windows
- **Controls:** Custom WPF controls
- **Resources:** Images, sounds, and other assets
- **Styles:** XAML styles and themes

**Dependencies:** Application

## Project References

The reference structure enforces proper dependency flow:

- **Presentation** ? References **Application**
- **Application** ? References **Domain** and **Infrastructure**
- **Infrastructure** ? References **Domain**
- **Domain** ? No external project references

## Technology Stack

- .NET Framework 4.7.2
- WPF (Windows Presentation Foundation)
- MVVM Pattern
- C# 7.3

## Getting Started

### Prerequisites
- Visual Studio 2019 or later
- .NET Framework 4.7.2 SDK

### Building the Project
1. Open `MonsterDungeon.sln` in Visual Studio
2. Restore NuGet packages (if any)
3. Build the solution (Ctrl+Shift+B)
4. Run the application (F5)

## Development Guidelines

- Follow SOLID principles
- Keep domain logic pure and testable
- Use dependency injection where appropriate
- Follow MVVM pattern in the Presentation layer
- Write clean, self-documenting code

## License

Copyright © Starforge Systems 2025
