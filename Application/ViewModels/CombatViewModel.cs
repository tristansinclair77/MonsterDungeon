using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MonsterDungeon.Domain.Entities;
using MonsterDungeon.Application.Services;
using System.Collections.Generic;
using System.Linq;

namespace MonsterDungeon.Application.ViewModels
{
    public class CombatViewModel : INotifyPropertyChanged
    {
        private readonly GridService _gridService;
        private readonly GameFlowService _gameFlowService;
    
        private string _selectedContextMenu = "Attack";
        private ObservableCollection<ObservableCollection<Tile>> _currentGrid;
        private Player _player;
        private int _playerGridX;
        private int _playerGridY;
        private int _moveCounter = 0;
        private int _nextSpawnThreshold;
        private System.Random _random = new System.Random();
        private ObservableCollection<Enemy> _enemies;

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
    
        // Set initial spawn threshold (1-3 moves)
            _nextSpawnThreshold = _random.Next(1, 4);
       
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

    // Check if next move is player row
      if (newY >= GridService.GridHeight - 1)
     {
      _enemies.RemoveAt(i); // Enemy disappears instead of colliding
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
