namespace KabooFontDiff.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            splitContainer1 = new SplitContainer();
            panelSettings = new Panel();
            grpInput = new GroupBox();
            txtInput = new TextBox();
            flowLayoutPanelPresets = new FlowLayoutPanel();
            btnPresetNum = new Button();
            btnPresetAlpha = new Button();
            btnPresetKanji = new Button();
            btnPresetSent = new Button();
            btnPresetEmoji = new Button();
            grpDisplay = new GroupBox();
            chkVertical = new CheckBox();
            label4 = new Label();
            numFontSize = new NumericUpDown();
            btnForeColor = new Button();
            btnBackColor = new Button();
            label5 = new Label();
            cboRenderingHint = new ComboBox();
            chkColorEmoji = new CheckBox();
            grpFonts = new GroupBox();
            btnAddAllFiltered = new Button();
            btnRemoveFont = new Button();
            btnAddFont = new Button();
            lstSelectedFonts = new ListBox();
            label3 = new Label();
            lstAllFonts = new ListBox();
            label2 = new Label();
            txtSearchFont = new TextBox();
            grpFontSets = new GroupBox();
            btnSaveSet = new Button();
            btnDeleteSet = new Button();
            btnNewSet = new Button();
            cboFontSets = new ComboBox();
            label1 = new Label();
            
            // Pagination
            pnlPagination = new Panel();
            btnPrevPage = new Button();
            btnNextPage = new Button();
            lblPageInfo = new Label();
            
            flowLayoutPanelPreview = new FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            panelSettings.SuspendLayout();
            grpInput.SuspendLayout();
            flowLayoutPanelPresets.SuspendLayout();
            grpDisplay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numFontSize).BeginInit();
            grpFonts.SuspendLayout();
            grpFontSets.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.FixedPanel = FixedPanel.Panel1;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(panelSettings);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(flowLayoutPanelPreview);
            splitContainer1.Size = new Size(1008, 729);
            splitContainer1.SplitterDistance = 386;
            splitContainer1.TabIndex = 0;
            // 
            // panelSettings
            // 
            panelSettings.AutoScroll = true;
            panelSettings.Controls.Add(grpInput);
            panelSettings.Controls.Add(grpDisplay);
            panelSettings.Controls.Add(grpFonts);
            panelSettings.Controls.Add(grpFontSets);
            panelSettings.Dock = DockStyle.Fill;
            panelSettings.Location = new Point(0, 0);
            panelSettings.Name = "panelSettings";
            panelSettings.Padding = new Padding(10);
            panelSettings.Size = new Size(386, 729);
            panelSettings.TabIndex = 0;
            // 
            // grpInput
            // 
            grpInput.Controls.Add(txtInput);
            grpInput.Controls.Add(flowLayoutPanelPresets);
            grpInput.Dock = DockStyle.Fill;
            grpInput.Location = new Point(10, 576);
            grpInput.Name = "grpInput";
            grpInput.Padding = new Padding(10);
            grpInput.Size = new Size(366, 143);
            grpInput.TabIndex = 3;
            grpInput.TabStop = false;
            grpInput.Text = "Text";
            // 
            // txtInput
            // 
            txtInput.Dock = DockStyle.Fill;
            txtInput.Location = new Point(10, 60);
            txtInput.Multiline = true;
            txtInput.Name = "txtInput";
            txtInput.ScrollBars = ScrollBars.Vertical;
            txtInput.Size = new Size(346, 73);
            txtInput.TabIndex = 0;
            txtInput.Text = "Sample Text";
            // 
            // flowLayoutPanelPresets
            // 
            flowLayoutPanelPresets.Controls.Add(btnPresetNum);
            flowLayoutPanelPresets.Controls.Add(btnPresetAlpha);
            flowLayoutPanelPresets.Controls.Add(btnPresetKanji);
            flowLayoutPanelPresets.Controls.Add(btnPresetSent);
            flowLayoutPanelPresets.Controls.Add(btnPresetEmoji);
            flowLayoutPanelPresets.Dock = DockStyle.Top;
            flowLayoutPanelPresets.Location = new Point(10, 26);
            flowLayoutPanelPresets.Name = "flowLayoutPanelPresets";
            flowLayoutPanelPresets.Size = new Size(346, 34);
            flowLayoutPanelPresets.TabIndex = 1;
            // 
            // btnPresetNum
            // 
            btnPresetNum.AutoSize = true;
            btnPresetNum.Location = new Point(3, 3);
            btnPresetNum.Name = "btnPresetNum";
            btnPresetNum.Size = new Size(45, 25);
            btnPresetNum.TabIndex = 0;
            btnPresetNum.Text = "123";
            // 
            // btnPresetAlpha
            // 
            btnPresetAlpha.AutoSize = true;
            btnPresetAlpha.Location = new Point(54, 3);
            btnPresetAlpha.Name = "btnPresetAlpha";
            btnPresetAlpha.Size = new Size(46, 25);
            btnPresetAlpha.TabIndex = 1;
            btnPresetAlpha.Text = "ABC";
            // 
            // btnPresetKanji
            // 
            btnPresetKanji.AutoSize = true;
            btnPresetKanji.Location = new Point(106, 3);
            btnPresetKanji.Name = "btnPresetKanji";
            btnPresetKanji.Size = new Size(49, 25);
            btnPresetKanji.TabIndex = 2;
            btnPresetKanji.Text = "漢";
            // 
            // btnPresetSent
            // 
            btnPresetSent.AutoSize = true;
            btnPresetSent.Location = new Point(161, 3);
            btnPresetSent.Name = "btnPresetSent";
            btnPresetSent.Size = new Size(49, 25);
            btnPresetSent.TabIndex = 3;
            btnPresetSent.Text = "Text";
            // 
            // btnPresetEmoji
            // 
            btnPresetEmoji.AutoSize = true;
            btnPresetEmoji.Location = new Point(216, 3);
            btnPresetEmoji.Name = "btnPresetEmoji";
            btnPresetEmoji.Size = new Size(58, 25);
            btnPresetEmoji.TabIndex = 4;
            btnPresetEmoji.Text = "Emoji";
            // 
            // grpDisplay
            // 
            grpDisplay.Controls.Add(chkVertical);
            grpDisplay.Controls.Add(label4);
            grpDisplay.Controls.Add(numFontSize);
            grpDisplay.Controls.Add(btnForeColor);
            grpDisplay.Controls.Add(btnBackColor);
            grpDisplay.Controls.Add(label5);
            grpDisplay.Controls.Add(cboRenderingHint);
            grpDisplay.Controls.Add(chkColorEmoji);
            grpDisplay.Dock = DockStyle.Top;
            grpDisplay.Location = new Point(10, 452);
            grpDisplay.Name = "grpDisplay";
            grpDisplay.Size = new Size(366, 124);
            grpDisplay.TabIndex = 2;
            grpDisplay.TabStop = false;
            grpDisplay.Text = "Display Settings";
            // 
            // chkVertical
            // 
            chkVertical.AutoSize = true;
            chkVertical.Location = new Point(228, 27);
            chkVertical.Name = "chkVertical";
            chkVertical.Size = new Size(103, 19);
            chkVertical.TabIndex = 0;
            chkVertical.Text = "Vertical Layout";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(10, 25);
            label4.Name = "label4";
            label4.Size = new Size(52, 15);
            label4.TabIndex = 1;
            label4.Text = "Size (pt):";
            // 
            // numFontSize
            // 
            numFontSize.Location = new Point(80, 23);
            numFontSize.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
            numFontSize.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            numFontSize.Name = "numFontSize";
            numFontSize.Size = new Size(120, 23);
            numFontSize.TabIndex = 2;
            numFontSize.Value = new decimal(new int[] { 24, 0, 0, 0 });
            // 
            // btnForeColor
            // 
            btnForeColor.Location = new Point(10, 55);
            btnForeColor.Name = "btnForeColor";
            btnForeColor.Size = new Size(90, 25);
            btnForeColor.TabIndex = 3;
            btnForeColor.Text = "Text Color";
            btnForeColor.UseVisualStyleBackColor = true;
            // 
            // btnBackColor
            // 
            btnBackColor.Location = new Point(106, 55);
            btnBackColor.Name = "btnBackColor";
            btnBackColor.Size = new Size(95, 25);
            btnBackColor.TabIndex = 4;
            btnBackColor.Text = "Back Color";
            btnBackColor.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(10, 90);
            label5.Name = "label5";
            label5.Size = new Size(48, 15);
            label5.TabIndex = 5;
            label5.Text = "Quality:";
            // 
            // cboRenderingHint
            // 
            cboRenderingHint.DropDownStyle = ComboBoxStyle.DropDownList;
            cboRenderingHint.Location = new Point(64, 87);
            cboRenderingHint.Name = "cboRenderingHint";
            cboRenderingHint.Size = new Size(190, 23);
            cboRenderingHint.TabIndex = 6;
            // 
            // chkColorEmoji
            // 
            chkColorEmoji.AutoSize = true;
            chkColorEmoji.Location = new Point(228, 52);
            chkColorEmoji.Name = "chkColorEmoji";
            chkColorEmoji.Size = new Size(86, 19);
            chkColorEmoji.TabIndex = 7;
            chkColorEmoji.Text = "Color Emoji";
            // 
            // grpFonts
            // 
            grpFonts.Controls.Add(btnAddAllFiltered);
            grpFonts.Controls.Add(btnRemoveFont);
            grpFonts.Controls.Add(btnAddFont);
            grpFonts.Controls.Add(lstSelectedFonts);
            grpFonts.Controls.Add(label3);
            grpFonts.Controls.Add(lstAllFonts);
            grpFonts.Controls.Add(label2);
            grpFonts.Controls.Add(txtSearchFont);
            grpFonts.Dock = DockStyle.Top;
            grpFonts.Location = new Point(10, 110);
            grpFonts.Name = "grpFonts";
            grpFonts.Size = new Size(366, 342);
            grpFonts.TabIndex = 1;
            grpFonts.TabStop = false;
            grpFonts.Text = "Manage Fonts";
            // 
            // btnAddAllFiltered
            // 
            btnAddAllFiltered.Location = new Point(13, 309);
            btnAddAllFiltered.Name = "btnAddAllFiltered";
            btnAddAllFiltered.Size = new Size(167, 24);
            btnAddAllFiltered.TabIndex = 8;
            btnAddAllFiltered.Text = "Add All >>>";
            btnAddAllFiltered.UseVisualStyleBackColor = true;
            // 
            // btnRemoveFont
            // 
            btnRemoveFont.Location = new Point(183, 276);
            btnRemoveFont.Name = "btnRemoveFont";
            btnRemoveFont.Size = new Size(173, 30);
            btnRemoveFont.TabIndex = 0;
            btnRemoveFont.Text = "<< Remove";
            // 
            // btnAddFont
            // 
            btnAddFont.Location = new Point(13, 276);
            btnAddFont.Name = "btnAddFont";
            btnAddFont.Size = new Size(167, 30);
            btnAddFont.TabIndex = 1;
            btnAddFont.Text = "Add >>";
            // 
            // lstSelectedFonts
            // 
            lstSelectedFonts.Location = new Point(183, 68);
            lstSelectedFonts.Name = "lstSelectedFonts";
            lstSelectedFonts.SelectionMode = SelectionMode.MultiExtended;
            lstSelectedFonts.Size = new Size(173, 199);
            lstSelectedFonts.TabIndex = 2;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(204, 50);
            label3.Name = "label3";
            label3.Size = new Size(39, 15);
            label3.TabIndex = 3;
            label3.Text = "In Set:";
            // 
            // lstAllFonts
            // 
            lstAllFonts.Location = new Point(10, 68);
            lstAllFonts.Name = "lstAllFonts";
            lstAllFonts.SelectionMode = SelectionMode.MultiExtended;
            lstAllFonts.Size = new Size(167, 199);
            lstAllFonts.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(10, 50);
            label2.Name = "label2";
            label2.Size = new Size(58, 15);
            label2.TabIndex = 5;
            label2.Text = "Available:";
            // 
            // txtSearchFont
            // 
            txtSearchFont.Location = new Point(10, 22);
            txtSearchFont.Name = "txtSearchFont";
            txtSearchFont.PlaceholderText = "Search...";
            txtSearchFont.Size = new Size(240, 23);
            txtSearchFont.TabIndex = 6;
            // 
            // grpFontSets
            // 
            grpFontSets.Controls.Add(btnSaveSet);
            grpFontSets.Controls.Add(btnDeleteSet);
            grpFontSets.Controls.Add(btnNewSet);
            grpFontSets.Controls.Add(cboFontSets);
            grpFontSets.Controls.Add(label1);
            grpFontSets.Dock = DockStyle.Top;
            grpFontSets.Location = new Point(10, 10);
            grpFontSets.Name = "grpFontSets";
            grpFontSets.Size = new Size(366, 100);
            grpFontSets.TabIndex = 0;
            grpFontSets.TabStop = false;
            grpFontSets.Text = "Font Sets";
            // 
            // btnSaveSet
            // 
            btnSaveSet.Location = new Point(170, 55);
            btnSaveSet.Name = "btnSaveSet";
            btnSaveSet.Size = new Size(80, 23);
            btnSaveSet.TabIndex = 0;
            btnSaveSet.Text = "Rename";
            // 
            // btnDeleteSet
            // 
            btnDeleteSet.Location = new Point(90, 55);
            btnDeleteSet.Name = "btnDeleteSet";
            btnDeleteSet.Size = new Size(75, 23);
            btnDeleteSet.TabIndex = 1;
            btnDeleteSet.Text = "Delete";
            // 
            // btnNewSet
            // 
            btnNewSet.Location = new Point(10, 55);
            btnNewSet.Name = "btnNewSet";
            btnNewSet.Size = new Size(75, 23);
            btnNewSet.TabIndex = 2;
            btnNewSet.Text = "New";
            // 
            // cboFontSets
            // 
            cboFontSets.DropDownStyle = ComboBoxStyle.DropDownList;
            cboFontSets.Location = new Point(60, 22);
            cboFontSets.Name = "cboFontSets";
            cboFontSets.Size = new Size(190, 23);
            cboFontSets.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(10, 25);
            label1.Name = "label1";
            label1.Size = new Size(41, 15);
            label1.TabIndex = 3;
            label1.Text = "Select:";
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(flowLayoutPanelPreview);
            splitContainer1.Panel2.Controls.Add(pnlPagination);
            splitContainer1.Size = new Size(1008, 729);
            splitContainer1.SplitterDistance = 386;
            splitContainer1.TabIndex = 0;
            // 
            // flowLayoutPanelPreview
            // 
            flowLayoutPanelPreview.AutoScroll = true;
            flowLayoutPanelPreview.BackColor = SystemColors.ControlLight;
            flowLayoutPanelPreview.Dock = DockStyle.Fill;
            flowLayoutPanelPreview.Location = new Point(0, 0);
            flowLayoutPanelPreview.Name = "flowLayoutPanelPreview";
            flowLayoutPanelPreview.Padding = new Padding(10);
            flowLayoutPanelPreview.Size = new Size(618, 689);
            flowLayoutPanelPreview.TabIndex = 0;
            // 
            // pnlPagination
            // 
            pnlPagination.Controls.Add(lblPageInfo);
            pnlPagination.Controls.Add(btnNextPage);
            pnlPagination.Controls.Add(btnPrevPage);
            pnlPagination.Dock = DockStyle.Bottom;
            pnlPagination.Location = new Point(0, 689);
            pnlPagination.Name = "pnlPagination";
            pnlPagination.Size = new Size(618, 40);
            pnlPagination.TabIndex = 1;
            // 
            // btnPrevPage
            // 
            btnPrevPage.Location = new Point(10, 5);
            btnPrevPage.Name = "btnPrevPage";
            btnPrevPage.Size = new Size(80, 30);
            btnPrevPage.TabIndex = 0;
            btnPrevPage.Text = "< Prev";
            btnPrevPage.UseVisualStyleBackColor = true;
            // 
            // btnNextPage
            // 
            btnNextPage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnNextPage.Location = new Point(528, 5);
            btnNextPage.Name = "btnNextPage";
            btnNextPage.Size = new Size(80, 30);
            btnNextPage.TabIndex = 1;
            btnNextPage.Text = "Next >";
            btnNextPage.UseVisualStyleBackColor = true;
            // 
            // lblPageInfo
            // 
            lblPageInfo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblPageInfo.TextAlign = ContentAlignment.MiddleCenter;
            lblPageInfo.Location = new Point(100, 5);
            lblPageInfo.Name = "lblPageInfo";
            lblPageInfo.Size = new Size(418, 30);
            lblPageInfo.TabIndex = 2;
            lblPageInfo.Text = "Page 1 / 1";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1008, 729);
            Controls.Add(splitContainer1);
            Name = "MainForm";
            Text = "Kaboo Font Diff";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            panelSettings.ResumeLayout(false);
            grpInput.ResumeLayout(false);
            grpInput.PerformLayout();
            flowLayoutPanelPresets.ResumeLayout(false);
            flowLayoutPanelPresets.PerformLayout();
            grpDisplay.ResumeLayout(false);
            grpDisplay.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numFontSize).EndInit();
            grpFonts.ResumeLayout(false);
            grpFonts.PerformLayout();
            grpFontSets.ResumeLayout(false);
            grpFontSets.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        // Declare controls
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panelSettings;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelPreview;
        
        // Pagination
        private System.Windows.Forms.Panel pnlPagination;
        private System.Windows.Forms.Button btnPrevPage;
        private System.Windows.Forms.Button btnNextPage;
        private System.Windows.Forms.Label lblPageInfo;
        
        // Font Sets
        private System.Windows.Forms.GroupBox grpFontSets;
        private System.Windows.Forms.ComboBox cboFontSets;
        private System.Windows.Forms.Button btnNewSet;
        private System.Windows.Forms.Button btnDeleteSet;
        private System.Windows.Forms.Button btnSaveSet;
        private System.Windows.Forms.Label label1;

        // Fonts
        private System.Windows.Forms.GroupBox grpFonts;
        private System.Windows.Forms.TextBox txtSearchFont;
        private System.Windows.Forms.ListBox lstAllFonts;
        private System.Windows.Forms.ListBox lstSelectedFonts;
        private System.Windows.Forms.Button btnAddFont;
        private System.Windows.Forms.Button btnRemoveFont;
        private System.Windows.Forms.Button btnAddAllFiltered; // New
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;

        // Display
        private System.Windows.Forms.GroupBox grpDisplay;
        private System.Windows.Forms.NumericUpDown numFontSize;
        private System.Windows.Forms.Button btnForeColor;
        private System.Windows.Forms.Button btnBackColor;
        private System.Windows.Forms.CheckBox chkVertical;
        private System.Windows.Forms.CheckBox chkColorEmoji;
        private System.Windows.Forms.ComboBox cboRenderingHint;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;

        // Input
        private System.Windows.Forms.GroupBox grpInput;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelPresets;
        private System.Windows.Forms.Button btnPresetNum;
        private System.Windows.Forms.Button btnPresetAlpha;
        private System.Windows.Forms.Button btnPresetKanji;
        private System.Windows.Forms.Button btnPresetSent;
        private System.Windows.Forms.Button btnPresetEmoji;
    }
}
