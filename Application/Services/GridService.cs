using System;
using System.Collections.Generic;
using MonsterDungeon.Domain.Entities;

namespace MonsterDungeon.Application.Services
{
    /// <summary>
    /// Manages the game grid system - 8 tiles wide, enemies descend each turn
    /// Based on GDD Section 2: Grid and Tile Movement
    /// </summary>
    public class GridService
    {
        public const int GridWidth = 8;
        public const int GridHeight = 10; // Visible rows

        private Tile[,] _grid;
        private Player _player;

        public GridService()
        {
  InitializeGrid();
        }

        /// <summary>
        /// Initialize the game grid
  /// </summary>
  private void InitializeGrid()
        {
        _grid = new Tile[GridWidth, GridHeight];

     for (int x = 0; x < GridWidth; x++)
     {
        for (int y = 0; y < GridHeight; y++)
     {
         _grid[x, y] = new Tile
                 {
         X = x,
         Y = y,
          IsOccupied = false
          };
   }
       }
   }

 /// <summary>
        /// Move player to a new position
    /// </summary>
    public bool MovePlayer(int newX, int newY)
        {
          if (!IsValidPosition(newX, newY))
   return false;

     if (_grid[newX, newY].IsOccupied)
                return false;

   // Clear old position
            if (_player != null)
            {
  _grid[_player.X, _player.Y].IsOccupied = false;
        }

    // Set new position
            _player.X = newX;
      _player.Y = newY;
        _grid[newX, newY].IsOccupied = true;

            return true;
        }

    /// <summary>
        /// Enemies descend one tile after each player movement
        /// </summary>
      public void DescendEnemies(List<Enemy> enemies)
        {
      foreach (var enemy in enemies)
            {
      // Clear old position
      if (IsValidPosition(enemy.X, enemy.Y))
 {
       _grid[enemy.X, enemy.Y].IsOccupied = false;
              }

            // Move down
   enemy.Y += 1;

     // Set new position if valid
          if (IsValidPosition(enemy.X, enemy.Y))
   {
       _grid[enemy.X, enemy.Y].IsOccupied = true;
           }
}
        }

     /// <summary>
        /// Check if position is within grid bounds
        /// </summary>
        public bool IsValidPosition(int x, int y)
      {
        return x >= 0 && x < GridWidth && y >= 0 && y < GridHeight;
        }

/// <summary>
    /// Get tile at position
   /// </summary>
    public Tile GetTile(int x, int y)
{
        if (!IsValidPosition(x, y))
   return null;

  return _grid[x, y];
        }

        /// <summary>
        /// Check if tile is occupied
        /// </summary>
public bool IsTileOccupied(int x, int y)
        {
            if (!IsValidPosition(x, y))
        return true;

            return _grid[x, y].IsOccupied;
        }

public void SetPlayer(Player player)
        {
            _player = player;
       if (IsValidPosition(player.X, player.Y))
     {
       _grid[player.X, player.Y].IsOccupied = true;
      }
        }
    }
}
