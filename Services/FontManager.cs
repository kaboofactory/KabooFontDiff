using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;

namespace KabooFontDiff.Services
{
    public class FontManager
    {
        private InstalledFontCollection _installedFonts;
        private HashSet<string> _availableFontNames;

        public FontManager()
        {
            _installedFonts = new InstalledFontCollection();
            _availableFontNames = new HashSet<string>(
                _installedFonts.Families.Select(f => f.Name), 
                StringComparer.OrdinalIgnoreCase
            );
        }

        public List<string> GetInstalledFontNames()
        {
            return _installedFonts.Families.Select(f => f.Name).OrderBy(n => n).ToList();
        }

        public bool IsFontInstalled(string fontName)
        {
            return _availableFontNames.Contains(fontName);
        }

        /// <summary>
        /// Tries to create a font. If the font family is not found, returns a fallback font (Segoe UI or GenericSansSerif).
        /// </summary>
        public Font GetFont(string fontName, float size, FontStyle style = FontStyle.Regular)
        {
            if (IsFontInstalled(fontName))
            {
                return new Font(fontName, size, style);
            }
            
            // Fallback
            return new Font(FontFamily.GenericSansSerif, size, style);
        }

        public FontFamily GetFontFamily(string fontName)
        {
             if (IsFontInstalled(fontName))
            {
                // Inefficient to iterate but System.Drawing doesn't expose a Dictionary lookup for Families
                return _installedFonts.Families.FirstOrDefault(f => f.Name.Equals(fontName, StringComparison.OrdinalIgnoreCase)) 
                       ?? FontFamily.GenericSansSerif;
            }
            return FontFamily.GenericSansSerif;
        }
    }
}
