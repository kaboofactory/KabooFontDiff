using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace KabooFontDiff.Controls
{
    public partial class FontPreviewControl : UserControl
    {
        private string _fontName = "Arial";
        private string _displayText = "";
        private float _fontSize = 12;
        private TextRenderingHint _renderingHint = TextRenderingHint.SystemDefault;
        private bool _isVertical;
        private bool _useColorEmoji;
        private Font _currentFont;
        
        // Metadata for display
        private bool _isInstalled = true;
        
        public string FontName => _fontName; // Expose for deletion logic
        
        // Native control for Color Emoji rendering (GDI/GDI+ doesn't support it well)
        private TextBox _emojiTextBox;
        
        // Remove button for weeding out
        private Button _btnRemove;
        
        public event EventHandler? RequestRemove;

        public FontPreviewControl()
        {
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;
            this.Padding = new Padding(10);
            this.MinimumSize = new Size(200, 150);
            this.BackColor = Color.White;
            
            _currentFont = new Font("Arial", 12);
            
            // Initialize Remove Button
            _btnRemove = new Button();
            _btnRemove.Text = "✕"; // Multiply sign or X
            _btnRemove.ForeColor = Color.Red;
            _btnRemove.FlatStyle = FlatStyle.Flat;
            _btnRemove.FlatAppearance.BorderSize = 0;
            _btnRemove.Size = new Size(24, 24);
            _btnRemove.Cursor = Cursors.Hand;
            _btnRemove.Click += (s, e) => RequestRemove?.Invoke(this, EventArgs.Empty);
            // Position handled in Resize
            this.Controls.Add(_btnRemove);
            
            // Initialize helper TextBox
            
            // Initialize helper TextBox
            _emojiTextBox = new TextBox();
            _emojiTextBox.Multiline = true;
            _emojiTextBox.ReadOnly = true;
            _emojiTextBox.BorderStyle = BorderStyle.None;
            _emojiTextBox.BackColor = this.BackColor;
            _emojiTextBox.ForeColor = this.ForeColor;
            _emojiTextBox.ScrollBars = ScrollBars.None;
            _emojiTextBox.Visible = false;
            _emojiTextBox.TabStop = false; // interactable? maybe better only for display
            _emojiTextBox.Cursor = Cursors.Default;
            this.Controls.Add(_emojiTextBox);
        }

        public void Configure(string fontName, string text, float size, Color foreColor, Color backColor, bool isVertical, TextRenderingHint hint, bool isInstalled, bool useColorEmoji = false, bool allowRemove = false)
        {
            _fontName = fontName;
            _displayText = text ?? "";
            
            if (_btnRemove != null) _btnRemove.Visible = allowRemove;
            _fontSize = size;
            _renderingHint = hint;
            _isVertical = isVertical;
            _isInstalled = isInstalled;
            _useColorEmoji = useColorEmoji;
            
            this.ForeColor = foreColor;
            this.BackColor = backColor;
            
            // Ensure TextBox exists (Defensive coding against init failure)
            if (_emojiTextBox == null)
            {
                _emojiTextBox = new TextBox();
                _emojiTextBox.Multiline = true;
                _emojiTextBox.ReadOnly = true;
                _emojiTextBox.BorderStyle = BorderStyle.None;
                _emojiTextBox.ScrollBars = ScrollBars.None;
                _emojiTextBox.TabStop = false;
                _emojiTextBox.Cursor = Cursors.Default;
                this.Controls.Add(_emojiTextBox);
            }

            // Update TextBox colors
            _emojiTextBox.BackColor = backColor;
            _emojiTextBox.ForeColor = foreColor;

            // Dispose old font if needed
            if (_currentFont != null) _currentFont.Dispose();
            
            try 
            {
                if (isInstalled)
                    _currentFont = new Font(_fontName, _fontSize);
                else
                    _currentFont = new Font(FontFamily.GenericSansSerif, _fontSize);
            }
            catch
            {
                _currentFont = new Font(FontFamily.GenericSansSerif, _fontSize);
            }

            // Configure Mode
            if (_useColorEmoji && !_isVertical)
            {
                // Use Native TextBox for Color rendering
                _emojiTextBox.Font = _currentFont;
                _emojiTextBox.Text = _displayText;
                _emojiTextBox.Visible = true;
                
                // Layout logic handled in Resize or here? 
                // Let's do it here + OnResize event?
                PerformLayoutHelper(); 
            }
            else
            {
                _emojiTextBox.Visible = false;
            }

            this.Invalidate();
        }

        private void PerformLayoutHelper()
        {
            // Position Remove Button (Top Right)
            if (_btnRemove != null)
            {
                _btnRemove.Location = new Point(this.Width - _btnRemove.Width - 2, 2);
                _btnRemove.BringToFront();
            }

            if (_emojiTextBox != null && _emojiTextBox.Visible)
            {
                int headerHeight = 25;
                _emojiTextBox.Location = new Point(this.Padding.Left, this.Padding.Top + headerHeight);
                _emojiTextBox.Size = new Size(
                    this.Width - this.Padding.Horizontal,
                    this.Height - this.Padding.Vertical - headerHeight
                );
            }
        }
        
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            PerformLayoutHelper();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var g = e.Graphics;
            g.TextRenderingHint = _renderingHint;

            // 1. Draw Header (Font Name)
            string headerText = _fontName + (_isInstalled ? "" : " (Not Installed)");
            using (var headerFont = new Font("Segoe UI", 9, FontStyle.Bold))
            using (var headerBrush = new SolidBrush(Color.Gray)) // Header always visible color? Or adaptable?
            {
                // Draw header at top-left
                g.DrawString(headerText, headerFont, headerBrush, 5, 5);
            }

            // 2. Draw Sample Text if not using Native TextBox
            if (!_emojiTextBox.Visible)
            {
                using (var textBrush = new SolidBrush(this.ForeColor))
                {
                    // Area for text
                    float headerHeight = 25;
                    RectangleF textArea = new RectangleF(
                        this.Padding.Left, 
                        this.Padding.Top + headerHeight, 
                        this.Width - this.Padding.Horizontal, 
                        this.Height - this.Padding.Vertical - headerHeight
                    );

                    string textToDraw = _displayText;
                    StringFormat sf = new StringFormat();
                    if (_isVertical)
                    {
                        sf.FormatFlags |= StringFormatFlags.DirectionVertical;
                        
                        // Japanese vertical text flows Right-to-Left. 
                        // GDI+ DirectionVertical draws Left-to-Right columns.
                        // We reverse the lines so the first logical line appears on the Right (last drawn column).
                        if (!string.IsNullOrEmpty(_displayText))
                        {
                            var lines = _displayText.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                            Array.Reverse(lines);
                            textToDraw = string.Join("\r\n", lines);
                        }

                        // GDI+ Vertical Drawing
                        g.DrawString(textToDraw, _currentFont, textBrush, textArea, sf);
                    }
                    else
                    {
                        // Horizontal (Standard GDI+)
                        g.DrawString(textToDraw, _currentFont, textBrush, textArea, sf);
                    }
                }
            }
            
            // Draw Border
            using (var pen = new Pen(Color.LightGray))
            {
                g.DrawRectangle(pen, 0, 0, this.Width - 1, this.Height - 1);
            }
        }

        // Clean up
        protected override void Dispose(bool disposing)
        {
            if (disposing && _currentFont != null)
            {
                _currentFont.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
