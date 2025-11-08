using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MonsterDungeon.Domain.Entities;

namespace MonsterDungeon.Application.ViewModels
{
    public class CombatViewModel : INotifyPropertyChanged
    {
        private string _selectedContextMenu = "Attack";
        private ObservableCollection<ObservableCollection<Tile>> _currentGrid;
        private object _playerInfo;

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

        public object PlayerInfo
        {
            get => _playerInfo;
            set
            {
                if (_playerInfo != value)
                {
                    _playerInfo = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand AttackCommand { get; }
        public ICommand OpenSpellsCommand { get; }
        public ICommand OpenItemsCommand { get; }

        public CombatViewModel()
        {
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

            // Initialize commands
            AttackCommand = new RelayCommand(Attack);
            OpenSpellsCommand = new RelayCommand(OpenSpells);
            OpenItemsCommand = new RelayCommand(OpenItems);
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
}
