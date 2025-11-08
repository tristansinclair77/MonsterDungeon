using CommunityToolkit.Mvvm.ComponentModel;
using MonsterDungeon.Domain.Services;

namespace MonsterDungeon.Application.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private string title = "Monster Dungeon";

        [ObservableProperty]
        private string version = "v0.1.0-alpha";

        public DebugMenuViewModel DebugMenu { get; }
        public ThemeManager ThemeManager { get; }

        public MainViewModel(DebugMenuViewModel debugMenuViewModel, ThemeManager themeManager)
        {
            DebugMenu = debugMenuViewModel;
            ThemeManager = themeManager;
        }
    }
}
