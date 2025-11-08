namespace MonsterDungeon.Domain.Models
{
    /// <summary>
    /// Comprehensive theme color palette for all UI elements
    /// </summary>
    public class UITheme
    {
        // Button Colors
        public string ButtonCoreColor { get; set; }
        public string ButtonTextColor { get; set; }
        public string ButtonExtrasColor { get; set; }

 // Screen Background Colors
        public string ScreenBackgroundMainColor { get; set; }
        public string ScreenBackgroundSecondaryColor { get; set; }

// Header Colors
     public string HeaderMainColor { get; set; }
        public string HeaderTextColor { get; set; }
        public string HeaderExtrasColor { get; set; }

        // Screen Window Colors
        public string WindowBackgroundColor { get; set; }
 public string WindowTextColor { get; set; }
        public string WindowSecondaryColor { get; set; }

        // Text Colors
    public string TextMainHeaderColor { get; set; }
        public string TextSecondaryHeaderColor { get; set; }
        public string TextBodyColor { get; set; }

    // Theme Metadata
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
