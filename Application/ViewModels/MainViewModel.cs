using CommunityToolkit.Mvvm.ComponentModel;

namespace MonsterDungeon.Application.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
  [ObservableProperty]
        private string title = "Monster Dungeon";

        [ObservableProperty]
        private string version = "v0.1.0-alpha";
    }
}
