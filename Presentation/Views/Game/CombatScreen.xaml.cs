using System.Windows.Controls;
using System.Windows.Input;
using MonsterDungeon.Application.ViewModels;

namespace MonsterDungeon.Presentation.Views.Game
{
    /// <summary>
    /// Interaction logic for CombatScreen.xaml
    /// </summary>
    public partial class CombatScreen : UserControl
    {
        private bool _keyPressed = false;
        private CombatViewModel _viewModel;

        public CombatScreen()
        {
            InitializeComponent();
            
            // Enable keyboard focus
            this.Focusable = true;
            this.Loaded += CombatScreen_Loaded;
            this.IsVisibleChanged += CombatScreen_IsVisibleChanged;
            
            // Subscribe to keyboard events
            this.KeyDown += OnKeyDown;
            this.KeyUp += OnKeyUp;
        }

        private void CombatScreen_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            // Get the ViewModel reference
            _viewModel = this.DataContext as CombatViewModel;
       
            // Set focus when loaded so keyboard input works
            if (this.IsVisible)
            {
                this.Focus();
            }
        }

        private void CombatScreen_IsVisibleChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            // Set focus whenever the control becomes visible
            if (this.IsVisible)
            {
                this.Focus();
            }
        }

        /// <summary>
        /// Handle key press for player movement
        /// </summary>
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (_keyPressed || _viewModel == null) return; // Prevent continuous movement

            if (e.Key == Key.Left)
            {
                _viewModel.MovePlayer(-1); // Move left
                _keyPressed = true;
                e.Handled = true;
            }
            else if (e.Key == Key.Right)
            {
                _viewModel.MovePlayer(1); // Move right
                _keyPressed = true;
                e.Handled = true;
            }
        }

        /// <summary>
        /// Reset key press flag when key is released
        /// </summary>
        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left || e.Key == Key.Right)
            {
                _keyPressed = false;
                e.Handled = true;
            }
        }
    }
}
