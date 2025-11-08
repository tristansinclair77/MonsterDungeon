using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MonsterDungeon.Domain.Services;

namespace MonsterDungeon.Application.ViewModels
{
    /// <summary>
    /// ViewModel for the Debug Menu overlay
    /// Provides theme testing and debugging functionality
    /// </summary>
    public class DebugMenuViewModel : INotifyPropertyChanged
    {
        private readonly ThemeManager _themeManager;
        private bool _isVisible;
        private string _selectedTheme;
        private List<string> _availableThemes;
        private string _selectedScreen;
        private List<string> _availableScreens;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                if (_isVisible != value)
                {
                    _isVisible = value;
                    OnPropertyChanged();
                }
            }
        }

        public string SelectedTheme
        {
            get => _selectedTheme;
            set
            {
                if (_selectedTheme != value)
                {
                    _selectedTheme = value;
                    OnPropertyChanged();

                    // Automatically apply the theme when selection changes
                    if (!string.IsNullOrEmpty(value))
                    {
                        _themeManager.ApplyDebugTheme(value);
                    }
                }
            }
        }

        public List<string> AvailableThemes
        {
            get => _availableThemes;
            set
            {
                if (_availableThemes != value)
                {
                    _availableThemes = value;
                    OnPropertyChanged();
                }
            }
        }

        public string SelectedScreen
        {
            get
            {
                System.Diagnostics.Debug.WriteLine($"[DebugMenu] SelectedScreen getter called, returning: '{_selectedScreen}'");
                return _selectedScreen;
            }
            set
            {
                System.Diagnostics.Debug.WriteLine($"[DebugMenu] SelectedScreen setter called with value: '{value}', current: '{_selectedScreen}'");
                if (_selectedScreen != value)
                {
                    _selectedScreen = value;
                    System.Diagnostics.Debug.WriteLine($"[DebugMenu] SelectedScreen changed to: '{value}'");
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsCombatScreenVisible));
                    OnPropertyChanged(nameof(IsMainMenuVisible));
                }
            }
        }

        public List<string> AvailableScreens
        {
            get => _availableScreens;
            set
            {
                if (_availableScreens != value)
                {
                    _availableScreens = value;
                    OnPropertyChanged();
                }
            }
        }

        // Visibility properties for screens
        public bool IsCombatScreenVisible
        {
            get
            {
                var result = _selectedScreen == "Combat Screen";
                System.Diagnostics.Debug.WriteLine($"[DebugMenu] IsCombatScreenVisible getter called: _selectedScreen='{_selectedScreen}', returning {result}");
                return result;
            }
        }

        public bool IsMainMenuVisible
        {
            get
            {
                var result = _selectedScreen == "Main Menu";
                System.Diagnostics.Debug.WriteLine($"[DebugMenu] IsMainMenuVisible getter called: _selectedScreen='{_selectedScreen}', returning {result}");
                return result;
            }
        }

        public ICommand ToggleMenuCommand { get; }
        public ICommand HideMenuCommand { get; }
        public ICommand ApplyThemeCommand { get; }
        public ICommand ToggleFullscreenCommand { get; }

        public DebugMenuViewModel(ThemeManager themeManager)
        {
            System.Diagnostics.Debug.WriteLine("[DebugMenu] Constructor started");
            _themeManager = themeManager;
            _selectedTheme = "DefaultDark";

            // Initialize available themes
            _availableThemes = new List<string>
            {
                "DefaultDark",
                "DefaultLight",
                "Crimson",
                "Emerald",
                "Azure",
                "Amber",
                "Amethyst"
            };

            // Initialize available screens
            _availableScreens = new List<string>
            {
                "Main Menu",
                "Combat Screen"
            };

            // Initialize commands
            ToggleMenuCommand = new RelayCommand(ToggleMenu);
            HideMenuCommand = new RelayCommand(HideMenu);
            ApplyThemeCommand = new RelayCommand(ApplyTheme);
            ToggleFullscreenCommand = new RelayCommand(ToggleFullscreen);

            // Initialize to Main Menu and ensure property notifications fire
            System.Diagnostics.Debug.WriteLine("[DebugMenu] About to set SelectedScreen to 'Main Menu'");
            SelectedScreen = "Main Menu";
            System.Diagnostics.Debug.WriteLine("[DebugMenu] Constructor completed");
        }

        /// <summary>
        /// Toggle the visibility of the debug menu
        /// </summary>
        private void ToggleMenu()
        {
            IsVisible = !IsVisible;
        }

        /// <summary>
        /// Hide the debug menu
        /// </summary>
        private void HideMenu()
        {
            IsVisible = false;
        }

        /// <summary>
        /// Apply the selected theme
        /// </summary>
        private void ApplyTheme()
        {
            if (!string.IsNullOrEmpty(SelectedTheme))
            {
                _themeManager.ApplyDebugTheme(SelectedTheme);
            }
        }

        /// <summary>
        /// Toggle fullscreen mode (future implementation)
        /// </summary>
        private void ToggleFullscreen()
        {
            // TODO: Implement fullscreen toggle
            // This will be connected to MainWindow to switch between:
            // - Windowed: 1280x720, centered, WindowStyle.None
            // - Fullscreen: WindowState.Maximized, WindowStyle.None
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    /// <summary>
    /// Simple implementation of ICommand for relay commands
    /// </summary>
    public class RelayCommand : ICommand
    {
        private readonly System.Action _execute;
        private readonly System.Func<bool> _canExecute;

        public event System.EventHandler CanExecuteChanged
        {
            add { System.Windows.Input.CommandManager.RequerySuggested += value; }
            remove { System.Windows.Input.CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(System.Action execute, System.Func<bool> canExecute = null)
        {
            _execute = execute ?? throw new System.ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute();
        }

        public void Execute(object parameter)
        {
            _execute();
        }
    }
}
