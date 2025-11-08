using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Media;
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
        private bool _isColorOptionsExpanded;

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

                        // Refresh all color properties for the color pickers
                        OnPropertyChanged(nameof(ButtonCoreColor));
                        OnPropertyChanged(nameof(ButtonTextColor));
                        OnPropertyChanged(nameof(ButtonExtrasColor));
                        OnPropertyChanged(nameof(ScreenBackgroundMainColor));
                        OnPropertyChanged(nameof(ScreenBackgroundSecondaryColor));
                        OnPropertyChanged(nameof(HeaderMainColor));
                        OnPropertyChanged(nameof(HeaderTextColor));
                        OnPropertyChanged(nameof(HeaderExtrasColor));
                        OnPropertyChanged(nameof(WindowBackgroundColor));
                        OnPropertyChanged(nameof(WindowTextColor));
                        OnPropertyChanged(nameof(WindowSecondaryColor));
                        OnPropertyChanged(nameof(TextMainHeaderColor));
                        OnPropertyChanged(nameof(TextSecondaryHeaderColor));
                        OnPropertyChanged(nameof(TextBodyColor));
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
            get => _selectedScreen;
            set
            {
                if (_selectedScreen != value)
                {
                    _selectedScreen = value;
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
        public bool IsCombatScreenVisible => _selectedScreen == "Combat Screen";
        public bool IsMainMenuVisible => _selectedScreen == "Main Menu";

        // Color Options Expander
        public bool IsColorOptionsExpanded
        {
            get => _isColorOptionsExpanded;
            set
            {
                if (_isColorOptionsExpanded != value)
                {
                    _isColorOptionsExpanded = value;
                    OnPropertyChanged();
                }
            }
        }

        // Color Properties for Binding to ColorPickers
        public Color ButtonCoreColor
        {
            get => HexToColor(_themeManager.ButtonCoreColor);
            set
            {
                string hex = ColorToHex(value);
                _themeManager.UpdateThemeColor(SelectedTheme, nameof(Domain.Models.UITheme.ButtonCoreColor), hex);
                OnPropertyChanged();
            }
        }

        public Color ButtonTextColor
        {
            get => HexToColor(_themeManager.ButtonTextColor);
            set
            {
                string hex = ColorToHex(value);
                _themeManager.UpdateThemeColor(SelectedTheme, nameof(Domain.Models.UITheme.ButtonTextColor), hex);
                OnPropertyChanged();
            }
        }

        public Color ButtonExtrasColor
        {
            get => HexToColor(_themeManager.ButtonExtrasColor);
            set
            {
                string hex = ColorToHex(value);
                _themeManager.UpdateThemeColor(SelectedTheme, nameof(Domain.Models.UITheme.ButtonExtrasColor), hex);
                OnPropertyChanged();
            }
        }

        public Color ScreenBackgroundMainColor
        {
            get => HexToColor(_themeManager.ScreenBackgroundMainColor);
            set
            {
                string hex = ColorToHex(value);
                _themeManager.UpdateThemeColor(SelectedTheme, nameof(Domain.Models.UITheme.ScreenBackgroundMainColor), hex);
                OnPropertyChanged();
            }
        }

        public Color ScreenBackgroundSecondaryColor
        {
            get => HexToColor(_themeManager.ScreenBackgroundSecondaryColor);
            set
            {
                string hex = ColorToHex(value);
                _themeManager.UpdateThemeColor(SelectedTheme, nameof(Domain.Models.UITheme.ScreenBackgroundSecondaryColor), hex);
                OnPropertyChanged();
            }
        }

        public Color HeaderMainColor
        {
            get => HexToColor(_themeManager.HeaderMainColor);
            set
            {
                string hex = ColorToHex(value);
                _themeManager.UpdateThemeColor(SelectedTheme, nameof(Domain.Models.UITheme.HeaderMainColor), hex);
                OnPropertyChanged();
            }
        }

        public Color HeaderTextColor
        {
            get => HexToColor(_themeManager.HeaderTextColor);
            set
            {
                string hex = ColorToHex(value);
                _themeManager.UpdateThemeColor(SelectedTheme, nameof(Domain.Models.UITheme.HeaderTextColor), hex);
                OnPropertyChanged();
            }
        }

        public Color HeaderExtrasColor
        {
            get => HexToColor(_themeManager.HeaderExtrasColor);
            set
            {
                string hex = ColorToHex(value);
                _themeManager.UpdateThemeColor(SelectedTheme, nameof(Domain.Models.UITheme.HeaderExtrasColor), hex);
                OnPropertyChanged();
            }
        }

        public Color WindowBackgroundColor
        {
            get => HexToColor(_themeManager.WindowBackgroundColor);
            set
            {
                string hex = ColorToHex(value);
                _themeManager.UpdateThemeColor(SelectedTheme, nameof(Domain.Models.UITheme.WindowBackgroundColor), hex);
                OnPropertyChanged();
            }
        }

        public Color WindowTextColor
        {
            get => HexToColor(_themeManager.WindowTextColor);
            set
            {
                string hex = ColorToHex(value);
                _themeManager.UpdateThemeColor(SelectedTheme, nameof(Domain.Models.UITheme.WindowTextColor), hex);
                OnPropertyChanged();
            }
        }

        public Color WindowSecondaryColor
        {
            get => HexToColor(_themeManager.WindowSecondaryColor);
            set
            {
                string hex = ColorToHex(value);
                _themeManager.UpdateThemeColor(SelectedTheme, nameof(Domain.Models.UITheme.WindowSecondaryColor), hex);
                OnPropertyChanged();
            }
        }

        public Color TextMainHeaderColor
        {
            get => HexToColor(_themeManager.TextMainHeaderColor);
            set
            {
                string hex = ColorToHex(value);
                _themeManager.UpdateThemeColor(SelectedTheme, nameof(Domain.Models.UITheme.TextMainHeaderColor), hex);
                OnPropertyChanged();
            }
        }

        public Color TextSecondaryHeaderColor
        {
            get => HexToColor(_themeManager.TextSecondaryHeaderColor);
            set
            {
                string hex = ColorToHex(value);
                _themeManager.UpdateThemeColor(SelectedTheme, nameof(Domain.Models.UITheme.TextSecondaryHeaderColor), hex);
                OnPropertyChanged();
            }
        }

        public Color TextBodyColor
        {
            get => HexToColor(_themeManager.TextBodyColor);
            set
            {
                string hex = ColorToHex(value);
                _themeManager.UpdateThemeColor(SelectedTheme, nameof(Domain.Models.UITheme.TextBodyColor), hex);
                OnPropertyChanged();
            }
        }

        public ICommand ToggleMenuCommand { get; }
        public ICommand HideMenuCommand { get; }
        public ICommand ApplyThemeCommand { get; }
        public ICommand ToggleFullscreenCommand { get; }
        public ICommand ResetThemeToDefaultCommand { get; }

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
            ResetThemeToDefaultCommand = new RelayCommand(ResetThemeToDefault);

            // Initialize to Main Menu and ensure property notifications fire
            SelectedScreen = "Main Menu";
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

        /// <summary>
        /// Reset the current theme to default values
        /// </summary>
        private void ResetThemeToDefault()
        {
            if (!string.IsNullOrEmpty(SelectedTheme))
            {
                _themeManager.ResetThemeToDefault(SelectedTheme);

                // Refresh all color properties
                OnPropertyChanged(nameof(ButtonCoreColor));
                OnPropertyChanged(nameof(ButtonTextColor));
                OnPropertyChanged(nameof(ButtonExtrasColor));
                OnPropertyChanged(nameof(ScreenBackgroundMainColor));
                OnPropertyChanged(nameof(ScreenBackgroundSecondaryColor));
                OnPropertyChanged(nameof(HeaderMainColor));
                OnPropertyChanged(nameof(HeaderTextColor));
                OnPropertyChanged(nameof(HeaderExtrasColor));
                OnPropertyChanged(nameof(WindowBackgroundColor));
                OnPropertyChanged(nameof(WindowTextColor));
                OnPropertyChanged(nameof(WindowSecondaryColor));
                OnPropertyChanged(nameof(TextMainHeaderColor));
                OnPropertyChanged(nameof(TextSecondaryHeaderColor));
                OnPropertyChanged(nameof(TextBodyColor));
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Helper methods for color conversion
        private Color HexToColor(string hex)
        {
            if (string.IsNullOrEmpty(hex))
                return Colors.Black;

            try
            {
                return (Color)ColorConverter.ConvertFromString(hex);
            }
            catch
            {
                return Colors.Black;
            }
        }

        private string ColorToHex(Color color)
        {
            return $"#{color.R:X2}{color.G:X2}{color.B:X2}";
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
