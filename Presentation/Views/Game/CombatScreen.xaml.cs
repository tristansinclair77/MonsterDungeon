using System.Windows.Controls;

namespace MonsterDungeon.Presentation.Views.Game
{
    /// <summary>
    /// Interaction logic for CombatScreen.xaml
    /// </summary>
    public partial class CombatScreen : UserControl
    {
        public CombatScreen()
        {
            System.Diagnostics.Debug.WriteLine("[CombatScreen] Constructor called");
            InitializeComponent();
            System.Diagnostics.Debug.WriteLine("[CombatScreen] InitializeComponent completed");

            this.Loaded += (s, e) =>
            {
                System.Diagnostics.Debug.WriteLine("[CombatScreen] Loaded event fired");
            };

            this.IsVisibleChanged += (s, e) =>
            {
                System.Diagnostics.Debug.WriteLine($"[CombatScreen] IsVisibleChanged: {this.IsVisible}, Visibility: {this.Visibility}");
            };
        }
    }
}
