using System;
using System.Windows;
using System.Windows.Input;
using MonsterDungeon.Application.ViewModels;
using WpfApplication = System.Windows.Application;

namespace MonsterDungeon.Presentation.Views
{
    /// <summary>
  /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _viewModel;
        private const double AspectRatio = 16.0 / 9.0;

        public MainWindow(MainViewModel vm)
        {
 InitializeComponent();
            DataContext = vm;
            _viewModel = vm;

            // Add key listener for debug menu toggle
            this.PreviewKeyDown += MainWindow_PreviewKeyDown;

       // Allow dragging the window by clicking anywhere on the GameRoot
 this.MouseLeftButtonDown += MainWindow_MouseLeftButtonDown;
        }

        private void MainWindow_PreviewKeyDown(object sender, KeyEventArgs e)
      {
            // Toggle debug menu with backtick (`) key
            if (e.Key == Key.OemTilde)
         {
   _viewModel.DebugMenu.ToggleMenuCommand.Execute(null);
                e.Handled = true;
       }

            // Close application with Escape key (useful for borderless window)
            if (e.Key == Key.Escape && !_viewModel.DebugMenu.IsVisible)
  {
                WpfApplication.Current.Shutdown();
}
      }

        private void MainWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
  {
            // Allow dragging the borderless window
 if (e.ButtonState == MouseButtonState.Pressed)
        {
        this.DragMove();
         }
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo info)
 {
            // Lock aspect ratio to 16:9
          if (info.WidthChanged)
       {
         double newHeight = info.NewSize.Width / AspectRatio;
     if (Math.Abs(info.NewSize.Height - newHeight) > 1)
      {
         this.Height = newHeight;
 }
    }
     else if (info.HeightChanged)
      {
           double newWidth = info.NewSize.Height * AspectRatio;
    if (Math.Abs(info.NewSize.Width - newWidth) > 1)
       {
    this.Width = newWidth;
    }
     }

 base.OnRenderSizeChanged(info);
      }
    }
}
