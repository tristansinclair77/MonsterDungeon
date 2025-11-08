using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MonsterDungeon.Presentation.Views;
using MonsterDungeon.Application.ViewModels;
using MonsterDungeon.Application.Services;
using MonsterDungeon.Domain.Services;

namespace MonsterDungeon
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        public static IHost AppHost { get; private set; }

        public App()
        {
            AppHost = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
     {
                 // Register Windows
         services.AddSingleton<MainWindow>();

       // Register ViewModels
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<DebugMenuViewModel>();
services.AddSingleton<CombatViewModel>();

   // Register Application Services
       services.AddSingleton<GameFlowService>();
        services.AddSingleton<GridService>();

             // Register Domain Services
         services.AddSingleton<EnemyFactory>();
       services.AddSingleton<ThemeManager>();

        // Register Infrastructure Services
       services.AddSingleton<MonsterDungeon.Infrastructure.Services.ThemeStorageService>();
    })
     .Build();
  }

        protected override async void OnStartup(StartupEventArgs e)
      {
      await AppHost.StartAsync();
            var mainWindow = AppHost.Services.GetRequiredService<MainWindow>();
     mainWindow.Show();
      base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
  {
  AppHost?.Dispose();
          base.OnExit(e);
        }
    }
}
