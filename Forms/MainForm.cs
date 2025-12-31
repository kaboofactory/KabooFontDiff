using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Forms;
using KabooFontDiff.Controls;
using KabooFontDiff.Models;
using KabooFontDiff.Services;
using Microsoft.VisualBasic; // For InputBox (quick solution for new set name)

namespace KabooFontDiff.Forms
{
    public partial class MainForm : Form
    {
        private readonly FontManager _fontManager;
        private AppSettings _settings;
        private List<string> _filteredInstalledFonts;
        
        // Timer to debounce selection updates
        private System.Windows.Forms.Timer _selectionTimer;
        
        // Timer to debounce text input updates
        private System.Windows.Forms.Timer _typeTimer;

        // Persist preview state for setting adjustments
        private List<string>? _previewFonts = null;
        
        // Pagination
        private int _currentPage = 0;
        private const int PAGE_SIZE = 50;

        public MainForm()
        {
            InitializeComponent();
            
            _fontManager = new FontManager();
            _settings = SettingsService.Load();

            // Initialize before InitUI to satisfy compiler
            _filteredInstalledFonts = new List<string>();

            _typeTimer = new System.Windows.Forms.Timer();
            _typeTimer.Interval = 500;
            _typeTimer.Tick += (s, e) => { _typeTimer.Stop(); RefreshPreview(); };

            _selectionTimer = new System.Windows.Forms.Timer();
            _selectionTimer.Interval = 300;
            _selectionTimer.Tick += (s, e) => 
            { 
                _selectionTimer.Stop(); 
                // Preview selected available fonts
                if (lstAllFonts.SelectedItems.Count > 0)
                {
                    var selected = lstAllFonts.SelectedItems.Cast<string>().ToList();
                    RefreshPreview(selected);
                }
            };

            InitUI();
            
            // Event Wiring
            this.FormClosing += MainForm_FormClosing;
            this.Resize += MainForm_Resize;

            // Font Sets
            cboFontSets.SelectedIndexChanged += CboFontSets_SelectedIndexChanged;
            btnNewSet.Click += BtnNewSet_Click;
            btnDeleteSet.Click += BtnDeleteSet_Click;
            btnSaveSet.Click += BtnSaveSet_Click;

            // Font Management
            txtSearchFont.TextChanged += TxtSearchFont_TextChanged;
            btnAddFont.Click += BtnAddFont_Click;
            btnAddAllFiltered.Click += BtnAddAllFiltered_Click;
            btnRemoveFont.Click += BtnRemoveFont_Click;
            
            // Preview logic
            // Preview logic
            lstAllFonts.SelectedIndexChanged += (s, e) => _selectionTimer.Start();
            
            // Revert preview immediately when focus leaves the list
            lstAllFonts.Leave += (s, e) => {
                _selectionTimer.Stop(); // Cancel any pending check
                _previewFonts = null;
                _currentPage = 0; // Reset
                RefreshPreview();
            };
            
            // Also revert when Set changes
            cboFontSets.SelectedIndexChanged += (s, e) => {
                 _previewFonts = null;
                 // Handler calls Refresh logic below separately
            };

            // Display
            numFontSize.ValueChanged += (s, e) => UpdateSetting(val => _settings.FontSize = (float)val, (float)numFontSize.Value);
            
            chkVertical.CheckedChanged += (s, e) => 
            {
                UpdateSetting(val => _settings.IsVertical = val, chkVertical.Checked);
            };

            chkColorEmoji.CheckedChanged += (s, e) => 
            {
                bool isChecked = chkColorEmoji.Checked;
                
                // Color Emoji relies on TextRenderer which doesn't support vertical layout well
                // So we disable Vertical Layout option when Color Emoji is active
                if (isChecked)
                {
                    if (chkVertical.Checked) chkVertical.Checked = false; // This will trigger CheckedChanged above
                    chkVertical.Enabled = false;
                }
                else
                {
                    chkVertical.Enabled = true;
                }

                UpdateSetting(val => _settings.UseColorEmoji = val, isChecked);
            };

            cboRenderingHint.SelectedIndexChanged += (s, e) => {
                if(cboRenderingHint.SelectedItem is TextRenderingHint hint)
                    UpdateSetting(val => _settings.RenderingHint = val, hint);
            };
            
            btnForeColor.Click += (s,e) => PickColor(c => { _settings.ForeColor = c; btnForeColor.ForeColor = c; });
            btnBackColor.Click += (s,e) => PickColor(c => { _settings.BackColor = c; btnBackColor.BackColor = c; });

            // Input
            txtInput.TextChanged += (s,e) => { _settings.SampleText = txtInput.Text; _typeTimer.Stop(); _typeTimer.Start(); }; // Re-trigger
            
            // Presets ... (Rest is same)
            
            // Presets
            btnPresetNum.Click += (s,e) => txtInput.Text = "0123456789";
            btnPresetAlpha.Click += (s,e) => txtInput.Text = "The quick brown fox jumps over the lazy dog.\r\nABCDEFGHIJKLMNOPQRSTUVWXYZ";
            btnPresetKanji.Click += (s,e) => txtInput.Text = "あかさたなはまんアカサタナハマン漢字表示";
            btnPresetSent.Click += (s,e) => txtInput.Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.";
            btnPresetEmoji.Click += (s,e) => txtInput.Text = "❤️ 🧡 💛 💚 💙 💜 🖤 🤍 🤎\r\n⭐ ✨ ⚡ ❄️ 🔥\r\n😀 😂 🥰 😎 🤔 😭";
            
            // Pagination
            btnPrevPage.Click += (s, e) => {
                if (_currentPage > 0)
                {
                    _currentPage--;
                    RefreshPreview();
                }
            };
            btnNextPage.Click += (s, e) => {
                _currentPage++;
                RefreshPreview();
            };
        }

        private void InitUI()
        {
            // Load Font Sets
            cboFontSets.DataSource = null; // Reset
            cboFontSets.DataSource = _settings.FontSets;
            cboFontSets.DisplayMember = "Name";
            
            // Select current set
            var currentSet = _settings.FontSets.FirstOrDefault(s => s.Name == _settings.CurrentFontSetName);
            if (currentSet != null) 
                cboFontSets.SelectedItem = currentSet;
            else if (_settings.FontSets.Count > 0)
                cboFontSets.SelectedIndex = 0;

            // Load Installed Fonts
            _filteredInstalledFonts = _fontManager.GetInstalledFontNames();
            UpdateInstalledFontsList();

            // Init Settings Controls
            numFontSize.Value = (decimal)_settings.FontSize;
            txtInput.Text = _settings.SampleText;
            chkVertical.Checked = _settings.IsVertical;
            chkColorEmoji.Checked = _settings.UseColorEmoji;
            
            btnForeColor.ForeColor = _settings.ForeColor;
            btnBackColor.BackColor = _settings.BackColor;

            cboRenderingHint.DataSource = Enum.GetValues(typeof(TextRenderingHint));
            cboRenderingHint.SelectedItem = _settings.RenderingHint;

            RefreshSelectedFontsList();
            RefreshPreview();
        }

        private void UpdateSetting<T>(Action<T> setter, T value)
        {
            setter(value);
            RefreshPreview();
        }

        private void PickColor(Action<Color> onApply)
        {
            using (var dlg = new ColorDialog())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    onApply(dlg.Color);
                    RefreshPreview();
                }
            }
        }

        #region Font Management

        private void UpdateInstalledFontsList()
        {
            lstAllFonts.BeginUpdate();
            lstAllFonts.Items.Clear();
            lstAllFonts.Items.AddRange(_filteredInstalledFonts.ToArray());
            lstAllFonts.EndUpdate();
        }

        private void TxtSearchFont_TextChanged(object? sender, EventArgs e)
        {
            var query = txtSearchFont.Text.Trim();
            var all = _fontManager.GetInstalledFontNames();
            
            if (string.IsNullOrEmpty(query))
            {
                _filteredInstalledFonts = all;
            }
            else
            {
                _filteredInstalledFonts = all.Where(f => f.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            UpdateInstalledFontsList();
        }

        private void RefreshSelectedFontsList()
        {
            var currentSet = cboFontSets.SelectedItem as FontSet;
            if (currentSet == null) return;

            lstSelectedFonts.BeginUpdate();
            lstSelectedFonts.Items.Clear();
            lstSelectedFonts.Items.AddRange(currentSet.FontNames.ToArray());
            lstSelectedFonts.EndUpdate();
        }

        private void BtnAddAllFiltered_Click(object? sender, EventArgs e)
        {
            var currentSet = cboFontSets.SelectedItem as FontSet;
            if (currentSet == null) return;

            bool changed = false;
            // Add everything currently in the list (filtered view)
            foreach (var item in lstAllFonts.Items)
            {
                string? fontName = item.ToString();
                if (fontName != null && !currentSet.FontNames.Contains(fontName))
                {
                    currentSet.FontNames.Add(fontName);
                    changed = true;
                }
            }

            if (changed)
            {
                _previewFonts = null; 
                _currentPage = 0; // Reset
                RefreshSelectedFontsList();
                RefreshPreview();
            }
        }

        private void BtnAddFont_Click(object? sender, EventArgs e)
        {
            var currentSet = cboFontSets.SelectedItem as FontSet;
            if (currentSet == null) return;

            bool changed = false;
            foreach (var item in lstAllFonts.SelectedItems)
            {
                string? fontName = item.ToString();
                if (fontName != null && !currentSet.FontNames.Contains(fontName))
                {
                    currentSet.FontNames.Add(fontName);
                    changed = true;
                }
            }

            if (changed)
            {
                _previewFonts = null; // Revert to set view
                _currentPage = 0;
                RefreshSelectedFontsList();
                RefreshPreview();
            }
        }

        private void BtnRemoveFont_Click(object? sender, EventArgs e)
        {
            var currentSet = cboFontSets.SelectedItem as FontSet;
            if (currentSet == null) return;

            // Copy to list to avoid modification exception
            var toRemove = lstSelectedFonts.SelectedItems.Cast<string>().ToList();
            if (toRemove.Count > 0)
            {
                foreach (string fontName in toRemove)
                {
                    currentSet.FontNames.Remove(fontName);
                }
                RefreshSelectedFontsList();
                RefreshPreview();
            }
        }

        #endregion

        #region Create/Delete Sets

        private void CboFontSets_SelectedIndexChanged(object? sender, EventArgs e)
        {
            var currentSet = cboFontSets.SelectedItem as FontSet;
            if (currentSet != null)
            {
                _settings.CurrentFontSetName = currentSet.Name;
                _currentPage = 0; // Reset page
                RefreshSelectedFontsList();
                RefreshPreview();
            }
        }

        private void BtnNewSet_Click(object? sender, EventArgs e)
        {
            string name = Interaction.InputBox("Enter name for new font set:", "New Font Set", "New Set");
            if (!string.IsNullOrWhiteSpace(name))
            {
                if (_settings.FontSets.Any(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
                {
                    MessageBox.Show("A set with this name already exists.");
                    return;
                }
                
                var newSet = new FontSet(name);
                _settings.FontSets.Add(newSet);
                
                // Refresh binding
                cboFontSets.DataSource = null;
                cboFontSets.DataSource = _settings.FontSets;
                cboFontSets.DisplayMember = "Name";
                cboFontSets.SelectedItem = newSet;
            }
        }

        private void BtnDeleteSet_Click(object? sender, EventArgs e)
        {
            if (_settings.FontSets.Count <= 1)
            {
                MessageBox.Show("Cannot delete the last font set.");
                return;
            }

            var currentSet = cboFontSets.SelectedItem as FontSet;
            if (currentSet != null)
            {
                if (MessageBox.Show($"Delete font set '{currentSet.Name}'?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    _settings.FontSets.Remove(currentSet);
                     // Refresh binding
                    cboFontSets.DataSource = null;
                    cboFontSets.DataSource = _settings.FontSets;
                    cboFontSets.DisplayMember = "Name";
                    
                    if (_settings.FontSets.Count > 0)
                        cboFontSets.SelectedIndex = 0;
                }
            }
        }

        private void BtnSaveSet_Click(object? sender, EventArgs e)
        {
             // Currently "Rename" in designer, let's implement Rename logic
             var currentSet = cboFontSets.SelectedItem as FontSet;
            if (currentSet != null)
            {
                string newName = Interaction.InputBox("Enter new name:", "Rename Font Set", currentSet.Name);
                if (!string.IsNullOrWhiteSpace(newName) && newName != currentSet.Name)
                {
                     if (_settings.FontSets.Any(s => s != currentSet && s.Name.Equals(newName, StringComparison.OrdinalIgnoreCase)))
                    {
                        MessageBox.Show("Name already taken.");
                        return;
                    }
                    currentSet.Name = newName;
                    
                    // Trigger refresh
                    cboFontSets.DataSource = null;
                    cboFontSets.DataSource = _settings.FontSets;
                    cboFontSets.DisplayMember = "Name";
                    cboFontSets.SelectedItem = currentSet;
                }
            }
        }

        #endregion

        #region Preview Logic

        private void RefreshPreview(List<string>? overrideFonts = null)
        {
            // Suspend layout
            flowLayoutPanelPreview.SuspendLayout();
            
            // Manage Preview State
            if (overrideFonts != null)
            {
                // New preview request (e.g. from Available list)
                _previewFonts = overrideFonts;
                _currentPage = 0; // Reset page on new manual preview
            }
            
            List<string> allFonts;

            if (_previewFonts != null)
            {
                allFonts = _previewFonts;
            }
            else
            {
                var currentSet = cboFontSets.SelectedItem as FontSet;
                if (currentSet == null) 
                {
                    flowLayoutPanelPreview.Controls.Clear();
                    flowLayoutPanelPreview.ResumeLayout();
                    lblPageInfo.Text = "0 / 0";
                    btnPrevPage.Enabled = false;
                    btnNextPage.Enabled = false;
                    return;
                }
                allFonts = currentSet.FontNames;
            }

            // Pagination Logic
            int totalItems = allFonts.Count;
            int totalPages = (int)Math.Ceiling((double)totalItems / PAGE_SIZE);
            if (totalPages == 0) totalPages = 1;
            
            if (_currentPage >= totalPages) _currentPage = totalPages - 1;
            if (_currentPage < 0) _currentPage = 0;
            
            var pagedFonts = allFonts.Skip(_currentPage * PAGE_SIZE).Take(PAGE_SIZE).ToList();
            
            // Update UI
            lblPageInfo.Text = $"Page {_currentPage + 1} / {totalPages} ({totalItems} Fonts)";
            btnPrevPage.Enabled = _currentPage > 0;
            btnNextPage.Enabled = _currentPage < totalPages - 1;
            
            List<string> fontNames = pagedFonts; // Use paged list for rendering

            // Update Flow Logic based on Vertical Mode
            if (_settings.IsVertical)
            {
                // Vertical Writing: Side-by-side columns, Horizontal Scrolling
                flowLayoutPanelPreview.FlowDirection = FlowDirection.LeftToRight;
                flowLayoutPanelPreview.WrapContents = false;
                flowLayoutPanelPreview.AutoScroll = true;
            }
            else
            {
                // Horizontal Writing: Stacked rows, Vertical Scrolling
                flowLayoutPanelPreview.FlowDirection = FlowDirection.TopDown;
                flowLayoutPanelPreview.WrapContents = false;
                flowLayoutPanelPreview.AutoScroll = true;
            }

            // Sync controls
            // fontNames is already set above
            
            // Remove excess
            while (flowLayoutPanelPreview.Controls.Count > fontNames.Count)
            {
                var ctrl = flowLayoutPanelPreview.Controls[flowLayoutPanelPreview.Controls.Count - 1];
                flowLayoutPanelPreview.Controls.RemoveAt(flowLayoutPanelPreview.Controls.Count - 1);
                ctrl.Dispose();
            }

            // Add missing
            while (flowLayoutPanelPreview.Controls.Count < fontNames.Count)
            {
                var ctrl = new FontPreviewControl();
                flowLayoutPanelPreview.Controls.Add(ctrl);
            }

            // Configure
            for (int i = 0; i < fontNames.Count; i++)
            {
                var ctrl = flowLayoutPanelPreview.Controls[i] as FontPreviewControl;
                string fontName = fontNames[i];
                bool isInstalled = _fontManager.IsFontInstalled(fontName);

                if (ctrl != null)
                {
                    // Allow remove only if we are viewing the Set (not a temporary preview)
                    bool isSetView = (_previewFonts == null);

                    ctrl.Configure(
                        fontName, 
                        _settings.SampleText, 
                        _settings.FontSize, 
                        _settings.ForeColor, 
                        _settings.BackColor, 
                        _settings.IsVertical,
                        _settings.RenderingHint,
                        isInstalled,
                        _settings.UseColorEmoji,
                        isSetView // allowRemove
                    );
                    
                    // Unsubscribe old to prevent multiple fires
                    ctrl.RequestRemove -= OnControlRequestRemove;
                    ctrl.RequestRemove += OnControlRequestRemove;
                    
                    UpdateControlSize(ctrl);
                }
            }

            flowLayoutPanelPreview.ResumeLayout();
        }
        
        private void OnControlRequestRemove(object? sender, EventArgs e)
        {
            var ctrl = sender as FontPreviewControl;
            if (ctrl == null) return;
            
            string fontName = ctrl.FontName;
            var currentSet = cboFontSets.SelectedItem as FontSet;
            
            if (currentSet != null && currentSet.FontNames.Contains(fontName))
            {
                currentSet.FontNames.Remove(fontName);
                
                // Refresh View
                RefreshSelectedFontsList();
                RefreshPreview(); 
            }
        }
        
        private void MainForm_Resize(object? sender, EventArgs e)
        {
            flowLayoutPanelPreview.SuspendLayout();
            foreach (Control c in flowLayoutPanelPreview.Controls)
            {
                UpdateControlSize(c);
            }
            flowLayoutPanelPreview.ResumeLayout();
        }

        private void UpdateControlSize(Control ctrl)
        {
             if (_settings.IsVertical)
             {
                 // Vertical Mode: Fixed Width, Height stretches to fill panel (minus scrollbar room)
                 int h = Math.Max(200, flowLayoutPanelPreview.ClientSize.Height - 30);
                 ctrl.Size = new Size(180, h);
             }
             else
             {
                 // Horizontal Mode: Fixed Height (or adaptive), Width stretches to fill panel
                 int w = Math.Max(200, flowLayoutPanelPreview.ClientSize.Width - 30);
                 // Dynamically sizes height if needed, but for now fixed generous block or based on font size could work.
                 // Let's keep the previous logic or slightly better:
                 // Ideally height proportional to text but that's expensive to calc. 
                 // Let's use a heuristic: Font Size * Lines (approx) + Header + Padding
                 // For now, simpler fixed + scaler is safer to avoid layout thrashing.
                 int estimatedH = Math.Max(150, (int)(_settings.FontSize * 3) + 60);
                 ctrl.Size = new Size(w, estimatedH);
             }
        }

        #endregion

        private void MainForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            SettingsService.Save(_settings);
        }
    }
}
