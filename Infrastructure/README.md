# Infrastructure Layer

## Purpose
The Infrastructure layer handles external concerns such as data persistence, file I/O, asset loading, and other technical implementations that are not core business logic.

## Principles
- **External Concerns** - Deals with databases, files, and external APIs
- **Implementation Details** - Concrete implementations of domain interfaces
- **Persistence** - Save and load game state
- **Resource Management** - Handle game assets and resources

## Contents

### Persistence
Save/load game state management:
- **SaveGameManager** - Handles saving game state to disk
- **LoadGameManager** - Handles loading game state from disk
- **Serializers** - JSON/XML serialization for game data
- **FileSystem** - File operations and directory management

### Repositories
Data access implementations:
- **PlayerRepository** - Player data persistence
- **DungeonRepository** - Dungeon layout persistence
- **ConfigurationRepository** - Game configuration storage
- **HighScoreRepository** - High score tracking

### Services
Infrastructure service implementations:
- **AssetLoader** - Load images, sounds, and other assets
- **ConfigurationService** - Manage game settings
- **LoggingService** - Application logging
- **AudioService** - Sound and music playback

## Technologies
- **System.IO** - File operations
- **JSON.NET** (if needed) - Serialization
- **System.Xml** - XML parsing for configuration
- **File System** - Local storage

## Design Guidelines
1. Implement domain interfaces defined in the Domain layer
2. Keep infrastructure code isolated from business logic
3. Use dependency injection for loose coupling
4. Handle all I/O exceptions gracefully
5. Make services testable through interfaces

## Data Storage
- **Save Files** - Located in `%AppData%/MonsterDungeon/Saves/`
- **Configuration** - Located in `%AppData%/MonsterDungeon/Config/`
- **Assets** - Bundled with application in `Resources/` folder

## Dependencies
- **Domain** - Implements domain interfaces and uses domain entities

## Error Handling
- All I/O operations should include proper exception handling
- Log errors appropriately
- Provide meaningful error messages to the user
