using System;
using System.Windows;
using System.Windows.Input;
using MonsterDungeon.Application.ViewModels;

namespace MonsterDungeon.Presentation.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _viewModel;

        public MainWindow(MainViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
            _viewModel = vm;

            // Add key listener for debug menu toggle
            this.PreviewKeyDown += MainWindow_PreviewKeyDown;
        }

        private void MainWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Toggle debug menu with backtick (`) key
            if (e.Key == Key.OemTilde)
            {
                _viewModel.DebugMenu.ToggleMenuCommand.Execute(null);
                e.Handled = true;
            }
        }
    }
}
