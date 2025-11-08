using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MonsterDungeon.Domain.Entities;
using MonsterDungeon.Domain.Services;
using MonsterDungeon.Domain.Enums;

namespace MonsterDungeon.Application.Services
{
    /// <summary>
    /// Manages the main gameplay flow and turn-based logic
    /// </summary>
    public class GameFlowService
    {
  private readonly GridService _grid;
        private readonly EnemyFactory _enemyFactory;
 private readonly ThemeManager _themeManager;

private Player _player;
   private List<Enemy> _enemies;
      private int _currentLevel;
  private int _turnCount;
  private bool _isGameActive;

        public Player Player => _player;
public IReadOnlyList<Enemy> Enemies => _enemies.AsReadOnly();
   public int CurrentLevel => _currentLevel;
 public bool IsGameActive => _isGameActive;

        public GameFlowService(GridService grid, EnemyFactory enemyFactory, ThemeManager themeManager)
    {
      _grid = grid;
      _enemyFactory = enemyFactory;
      _themeManager = themeManager;
  _enemies = new List<Enemy>();
        }

   /// <summary>
        /// Start a new game
/// </summary>
    public void StartNewGame()
    {
  _player = new Player();
       _grid.SetPlayer(_player);
      
      _enemies.Clear();
     _currentLevel = 1;
  _turnCount = 0;
      _isGameActive = true;
  
      // Set initial theme
  _themeManager.ChangeTheme(DungeonTheme.Caverns);
      
   // TODO: Initialize first encounter
   }

        /// <summary>
   /// Start a new encounter/level
/// </summary>
        public async Task StartEncounterAsync()
        {
     // Clear old enemies
    _enemies.Clear();
      
      // Spawn new enemies based on current level
            SpawnEnemies();
   
    // TODO: Animate encounter start
      await Task.Delay(100);
  }

    /// <summary>
   /// End current encounter
  /// </summary>
        public async Task EndEncounterAsync()
     {
       // Check if player won
     if (_player.IsAlive)
          {
       _currentLevel++;
    
  // Change theme every 5 levels
   if (_currentLevel % 5 == 0)
   {
   ChangeTheme();
         }
     
    // Start next encounter
  await StartEncounterAsync();
  }
else
            {
     // Game over
    _isGameActive = false;
      // TODO: Show game over screen
      }
   }

        /// <summary>
/// Process player movement turn
    /// </summary>
        public void ProcessPlayerMove(int newX, int newY)
        {
  if (!_isGameActive) return;

  // Move player
  if (_grid.MovePlayer(newX, newY))
          {
  _turnCount++;
       
      // Enemies descend
    _grid.DescendEnemies(_enemies);
       
       // Spawn new enemies at top
       if (_turnCount % 3 == 0)
    {
         SpawnEnemyAtTop();
     }
   
       // Check for collisions
     CheckCollisions();
       }
 }

   /// <summary>
   /// Process player attack
   /// </summary>
   public void ProcessPlayerAttack(int targetX, int targetY)
        {
   if (!_isGameActive) return;

  // Find enemy at target position
  Enemy target = _enemies.Find(e => e.X == targetX && e.Y == targetY);
  
       if (target != null)
{
     // Calculate damage
        int damage = _player.Attack + _player.Inventory.GetTotalAttackBonus();
           target.TakeDamage(damage);
  
   // Remove enemy if dead
if (!target.IsAlive)
    {
     _enemies.Remove(target);
          _player.GainExperience(target.ExperienceDrop);
          _player.Gold += target.GoldDrop;
    }
         
     // Enemy counterattacks
      if (target.IsAlive && !target.IsBlock)
                {
        int counterDamage = target.GetAttackDamage();
       _player.TakeDamage(counterDamage);
  }
  }

            // Process turn
 _turnCount++;
        _grid.DescendEnemies(_enemies);
        }

     private void SpawnEnemies()
        {
            int enemyCount = 3 + _currentLevel;
          
     for (int i = 0; i < enemyCount; i++)
       {
      SpawnEnemyAtTop();
       }
 }

        private void SpawnEnemyAtTop()
{
      var random = new Random();
    int x = random.Next(0, GridService.GridWidth);
      
     Enemy enemy = _enemyFactory.SpawnEnemy(_currentLevel, _themeManager.CurrentTheme);
   enemy.X = x;
  enemy.Y = 0;
       
      _enemies.Add(enemy);
}

    private void CheckCollisions()
   {
   // Check if any enemy reached the player
     foreach (var enemy in _enemies)
  {
       if (enemy.X == _player.X && enemy.Y == _player.Y)
        {
          // Enemy attacks player
      if (!enemy.IsBlock)
{
      int damage = enemy.GetAttackDamage();
      _player.TakeDamage(damage);
         }
    }
        }
        }

        private void ChangeTheme()
        {
      // Cycle through themes
            var themes = new[]
            {
           DungeonTheme.Caverns,
       DungeonTheme.Crypt,
   DungeonTheme.Volcano,
  DungeonTheme.IceCavern
    };
    
 int index = (_currentLevel / 5) % themes.Length;
_themeManager.ChangeTheme(themes[index]);
        }
    }
}
