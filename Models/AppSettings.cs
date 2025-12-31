using System.Collections.Generic;
using System.Drawing.Text;

namespace KabooFontDiff.Models
{
    public class AppSettings
    {
        public string CurrentFontSetName { get; set; } = "Default";
        public List<FontSet> FontSets { get; set; } = new List<FontSet>();
        public string SampleText { get; set; } = "The quick brown fox jumps over the lazy dog.\r\n1234567890";
        
        // Font size in Points
        public float FontSize { get; set; } = 24.0f;
        
        // Storing colors as ARGB int for easy JSON serialization
        public int ForeColorArgb { get; set; } = System.Drawing.Color.Black.ToArgb();
        public int BackColorArgb { get; set; } = System.Drawing.Color.White.ToArgb();
        
        public bool IsVertical { get; set; } = false;
        public bool UseColorEmoji { get; set; } = false;
        
        // Using int for enum serialization safety, casting property helper below
        public int RenderingHintValue { get; set; } = (int)TextRenderingHint.ClearTypeGridFit;

        [System.Text.Json.Serialization.JsonIgnore]
        public System.Drawing.Color ForeColor
        {
            get => System.Drawing.Color.FromArgb(ForeColorArgb);
            set => ForeColorArgb = value.ToArgb();
        }

        [System.Text.Json.Serialization.JsonIgnore]
        public System.Drawing.Color BackColor
        {
            get => System.Drawing.Color.FromArgb(BackColorArgb);
            set => BackColorArgb = value.ToArgb();
        }

        [System.Text.Json.Serialization.JsonIgnore]
        public TextRenderingHint RenderingHint
        {
            get => (TextRenderingHint)RenderingHintValue;
            set => RenderingHintValue = (int)value;
        }

        public AppSettings()
        {
            // Ensure at least one default set exists
            FontSets.Add(new FontSet("Default") { FontNames = new List<string> { "Arial", "Segoe UI", "Meiryo UI" } });
        }
    }
}
