using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MonsterDungeon.Domain.Entities;
using MonsterDungeon.Application.Services;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Threading;
using System;
using System.Windows.Media;

namespace MonsterDungeon.Application.ViewModels
{
    public class CombatViewModel : INotifyPropertyChanged
    {
  private readonly GridService _gridService;
   private readonly GameFlowService _gameFlowService;
      private bool _isRenderingActive = false;
    private DateTime _lastBulletUpdate = DateTime.Now;
        private DispatcherTimer _enemyMovementTimer;
     private bool _pendingEnemyMovement = false;
    
    private string _selectedContextMenu = "Attack";
private ObservableCollection<ObservableCollection<Tile>> _currentGrid;
  private Player _player;
     private int _moveCounter = 0;
        private int _nextSpawnThreshold;
      private System.Random _random = new System.Random();
        private ObservableCollection<Enemy> _enemies;
        private ObservableCollection<Bullet> _bullets;

        public event PropertyChangedEventHandler PropertyChanged;

    public string SelectedContextMenu
        {
       get => _selectedContextMenu;
        set
            {
                if (_selectedContextMenu != value)
  {
   _selectedContextMenu = value;
   OnPropertyChanged();
          }
      }
        }

        public ObservableCollection<ObservableCollection<Tile>> CurrentGrid
    {
      get => _currentGrid;
    set
            {
      if (_currentGrid != value)
    {
      _currentGrid = value;
   OnPropertyChanged();
            }
        }
        }

public Player Player
{
            get => _player;
  set
            {
                if (_player != value)
     {
          _player = value;
       OnPropertyChanged();
    OnPropertyChanged(nameof(PlayerGridX));
           OnPropertyChanged(nameof(PlayerGridY));
       }
        }
 }

      // Player position for UI rendering (column index)
        public int PlayerGridX
        {
            get => _player?.X ?? 0;
    set
    {
  if (_player != null && _player.X != value)
       {
        _player.X = value;
          OnPropertyChanged();
         }
 }
        }

        // Player position for UI rendering (row index)
        public int PlayerGridY
      {
  get => _player?.Y ?? 0;
         set
            {
       if (_player != null && _player.Y != value)
           {
     _player.Y = value;
 OnPropertyChanged();
    }
            }
        }

      public ObservableCollection<Enemy> Enemies
        {
      get => _enemies;
set
          {
     if (_enemies != value)
 {
   _enemies = value;
         OnPropertyChanged();
    }
        }
 }

        public ObservableCollection<Bullet> Bullets
     {
    get => _bullets;
 set
            {
          if (_bullets != value)
          {
           _bullets = value;
 OnPropertyChanged();
    }
     }
 }

      public ICommand AttackCommand { get; }
        public ICommand OpenSpellsCommand { get; }
        public ICommand OpenItemsCommand { get; }
        public ICommand MovePlayerCommand { get; }

        public CombatViewModel(GridService gridService, GameFlowService gameFlowService)
        {
 _gridService = gridService;
  _gameFlowService = gameFlowService;
   
     // Initialize grid
   _currentGrid = new ObservableCollection<ObservableCollection<Tile>>();

            // Initialize with empty grid structure (8 columns × 10 rows)
 for (int y = 0; y < 10; y++)
            {
var row = new ObservableCollection<Tile>();
           for (int x = 0; x < 8; x++)
       {
  row.Add(new Tile { X = x, Y = y });
      }
     _currentGrid.Add(row);
     }

 // Initialize enemies collection
            _enemies = new ObservableCollection<Enemy>();
     
    // Initialize bullets collection
  _bullets = new ObservableCollection<Bullet>();
    
    // Set initial spawn threshold (1-3 moves)
 _nextSpawnThreshold = _random.Next(1, 4);
   
    // Initialize enemy movement timer for delayed movement after attacks
       _enemyMovementTimer = new DispatcherTimer();
    _enemyMovementTimer.Interval = TimeSpan.FromMilliseconds(300); // 300ms delay
    _enemyMovementTimer.Tick += EnemyMovementTimer_Tick;
   
  // Start continuous rendering for smooth animations
    StartContinuousRendering();
    
        // Initialize commands
       AttackCommand = new RelayCommand(Attack);
         OpenSpellsCommand = new RelayCommand(OpenSpells);
            OpenItemsCommand = new RelayCommand(OpenItems);
 MovePlayerCommand = new RelayCommand<int>(MovePlayer);

// Initialize player at starting position (center-bottom)
      _player = new Player();
      _gridService.SetPlayer(_player);
        }

        /// <summary>
        /// Start continuous rendering loop for smooth UI updates
      /// </summary>
private void StartContinuousRendering()
        {
        if (!_isRenderingActive)
            {
           CompositionTarget.Rendering += OnRendering;
 _isRenderingActive = true;
       System.Diagnostics.Debug.WriteLine("Continuous rendering started");
          }
        }

        /// <summary>
        /// Stop continuous rendering (optional - for performance)
        /// </summary>
        private void StopContinuousRendering()
      {
            if (_isRenderingActive)
  {
     CompositionTarget.Rendering -= OnRendering;
                _isRenderingActive = false;
       System.Diagnostics.Debug.WriteLine("Continuous rendering stopped");
          }
    }
        
  /// <summary>
        /// Timer tick handler for delayed enemy movement after attacks
        /// </summary>
        private void EnemyMovementTimer_Tick(object sender, EventArgs e)
        {
        _enemyMovementTimer.Stop();
    
        if (_pendingEnemyMovement)
        {
      _pendingEnemyMovement = false;
       ProcessEnemyMovement();
    System.Diagnostics.Debug.WriteLine("Delayed enemy movement executed after attack");
   }
        }

        /// <summary>
        /// Called on every rendering frame (~60 FPS) for smooth UI updates
        /// </summary>
     private void OnRendering(object sender, EventArgs e)
    {
// Calculate delta time since last update
     DateTime now = DateTime.Now;
            double deltaTime = (now - _lastBulletUpdate).TotalSeconds;
          _lastBulletUpdate = now;

       // Update bullet positions with smooth interpolation
       if (_bullets.Count > 0)
    {
       ProcessBulletMovementSmooth(deltaTime);
   }

    // Force UI refresh for all bullets
            foreach (var bullet in _bullets)
            {
   // Trigger property change notification to update UI bindings
       bullet.OnPropertyChanged(nameof(bullet.PixelY));
 }

            // Force UI refresh for all enemies
    foreach (var enemy in _enemies)
       {
    enemy.OnPropertyChanged(nameof(enemy.Y));
}
        }

        /// <summary>
   /// Move bullets upward smoothly using delta time (called from rendering loop)
        /// </summary>
    private void ProcessBulletMovementSmooth(double deltaTime)
  {
        for (int i = _bullets.Count - 1; i >= 0; i--)
    {
    var bullet = _bullets[i];
            
 // Store the previous pixel position before updating
            double previousPixelY = bullet.PixelY;

         // Update pixel position based on velocity and delta time
    bullet.PixelY += bullet.VelocityY * deltaTime;

     // Check if bullet goes off screen (top boundary at Y=0 = 0 pixels)
  if (bullet.PixelY < 0)
    {
        _bullets.RemoveAt(i);
       System.Diagnostics.Debug.WriteLine($"Bullet removed at top boundary, was at PixelY={bullet.PixelY:F2}");
 continue;
  }

 // Check collision with enemies - check all grid cells the bullet passed through
// Convert pixel positions to grid positions to check for collisions
            int currentGridY = (int)(bullet.PixelY / 100);
            int previousGridY = (int)(previousPixelY / 100);
            
 // Check all grid Y positions the bullet passed through this frame
    bool hitDetected = false;
 for (int checkY = previousGridY; checkY >= currentGridY; checkY--)
            {
 var hitEnemy = _enemies.FirstOrDefault(e => e.X == bullet.X && e.Y == checkY);
        if (hitEnemy != null)
       {
            // Remove both bullet and enemy
     _bullets.RemoveAt(i);
  _enemies.Remove(hitEnemy);
           System.Diagnostics.Debug.WriteLine($"Bullet hit enemy at X={hitEnemy.X}, Y={hitEnemy.Y} (bullet PixelY: {bullet.PixelY:F2})");
               hitDetected = true;
          break;
   }
            }
      
        if (hitDetected)
       {
       continue;
  }
  }

     // Stop rendering if no bullets remain
       if (_bullets.Count == 0)
   {
   StopContinuousRendering();
     }
        }

        /// <summary>
  /// Move player by direction offset (-1 for left, +1 for right)
        /// </summary>
        public void MovePlayer(int direction)
        {
 if (_player == null) return;

            int newX = _player.X + direction;

         // Validate bounds
     if (newX >= 0 && newX < GridService.GridWidth)
            {
       // Attempt to move through GridService
       if (_gridService.MovePlayer(newX, _player.Y))
     {
      // Update UI bindings
        OnPropertyChanged(nameof(PlayerGridX));
      OnPropertyChanged(nameof(PlayerGridY));
 
       // Process the turn (enemies descend, spawn, etc.)
 ProcessEnemyMovement();
                }
        }
        }

        /// <summary>
        /// Process enemy spawning and descent after player moves
        /// </summary>
        private void ProcessEnemyMovement()
        {
            _moveCounter++;
      TrySpawnEnemies();

    // Move enemies down - Enemy class implements INotifyPropertyChanged
        // so UI will update automatically when Y changes
            for (int i = _enemies.Count - 1; i >= 0; i--)
        {
          var enemy = _enemies[i];
          int newY = enemy.Y + 1;

                // Check if enemy would move PAST the player's row (collision)
                if (newY > _player.Y)
                {
     _enemies.RemoveAt(i); // Enemy disappears when trying to move past player
                  System.Diagnostics.Debug.WriteLine($"Enemy removed - collided with player: X={enemy.X}, Y={enemy.Y} -> {newY}");
        continue;
         }

                // Update enemy position - triggers PropertyChanged
     enemy.Y = newY;
   }
   }

        /// <summary>
      /// Spawn 1-3 enemies randomly on top row every 1-3 player moves
        /// </summary>
        private void TrySpawnEnemies()
        {
      if (_moveCounter >= _nextSpawnThreshold)
     {
     int spawnCount = _random.Next(1, 4); // 1–3 enemies

                for (int i = 0; i < spawnCount; i++)
   {
    int x = _random.Next(0, GridService.GridWidth);
    
            // Only spawn if no enemy already at that top-row position
        if (!_enemies.Any(e => e.Y == 0 && e.X == x))
         {
         var newEnemy = new Enemy
                  {
     X = x,
      Y = 0,  // TOP ROW
    Health = 30,
 MaxHealth = 30,
         Attack = 5,
    Defense = 2
            };
       _enemies.Add(newEnemy);
 
     // DEBUG: Confirm spawn position
                   System.Diagnostics.Debug.WriteLine($"Enemy spawned at X={x}, Y=0 (TOP ROW)");
        }
 }
    
  // Reset counter and set next spawn threshold
     _moveCounter = 0;
      _nextSpawnThreshold = _random.Next(1, 4);
            }
      }

 private void Attack()
        {
    SelectedContextMenu = "Attack";
   
   // Fire a bullet from the player's current position
            if (_player != null)
      {
      var bullet = new Bullet
             {
      X = _player.X,
     Y = _player.Y, // Start at player position
           PixelY = _player.Y * 100 // Initialize pixel position (100 pixels per grid cell)
       };

        _bullets.Add(bullet);
    System.Diagnostics.Debug.WriteLine($"Bullet fired from player position X={_player.X}, Y={_player.Y}, PixelY={bullet.PixelY}");

            // Start continuous rendering for smooth animation
     if (!_isRenderingActive)
 {
      _lastBulletUpdate = DateTime.Now;
      StartContinuousRendering();
   }
        
        // Delay enemy movement to allow bullet to travel and potentially hit enemies
    // This prevents enemies from moving immediately and disappearing before bullet collision
 _pendingEnemyMovement = true;
        _enemyMovementTimer.Start();
        System.Diagnostics.Debug.WriteLine("Enemy movement delayed by 300ms to allow bullet travel");
  }
        }

      private void OpenSpells()
        {
   SelectedContextMenu = "Spells";
        }

        private void OpenItems()
  {
   SelectedContextMenu = "Items";
        }

   protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    
    /// <summary>
    /// Generic relay command that accepts a parameter
    /// </summary>
    public class RelayCommand<T> : ICommand
 {
        private readonly System.Action<T> _execute;
        private readonly System.Func<T, bool> _canExecute;

        public event System.EventHandler CanExecuteChanged
        {
    add { CommandManager.RequerySuggested += value; }
  remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(System.Action<T> execute, System.Func<T, bool> canExecute = null)
        {
            _execute = execute ?? throw new System.ArgumentNullException(nameof(execute));
  _canExecute = canExecute;
    }

        public bool CanExecute(object parameter)
   {
            return _canExecute == null || _canExecute((T)parameter);
        }

        public void Execute(object parameter)
        {
_execute((T)parameter);
   }
    }
}
