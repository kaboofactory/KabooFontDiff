using System.Collections.Generic;

namespace KabooFontDiff.Models
{
    public class FontSet
    {
        public string Name { get; set; } = "Default";
        public List<string> FontNames { get; set; } = new List<string>();

        public FontSet() { }

        public FontSet(string name)
        {
            Name = name;
        }
    }
}
