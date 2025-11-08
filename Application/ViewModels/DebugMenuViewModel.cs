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

        public ICommand ToggleMenuCommand { get; }
        public ICommand HideMenuCommand { get; }
        public ICommand ApplyThemeCommand { get; }

        public DebugMenuViewModel(ThemeManager themeManager)
        {
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

            // Initialize commands
            ToggleMenuCommand = new RelayCommand(ToggleMenu);
            HideMenuCommand = new RelayCommand(HideMenu);
            ApplyThemeCommand = new RelayCommand(ApplyTheme);
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
