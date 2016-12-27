﻿namespace Bonsai.NuGet
{
    partial class GalleryDialog
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
            this.components = new System.ComponentModel.Container();
            this.mainLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.packageViewLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.filterLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.releaseFilterComboBox = new System.Windows.Forms.ComboBox();
            this.sortLabel = new System.Windows.Forms.Label();
            this.sortComboBox = new System.Windows.Forms.ComboBox();
            this.pageSelectorPanel = new System.Windows.Forms.Panel();
            this.packagePageSelector = new Bonsai.NuGet.PackagePageSelector();
            this.packageViewPanel = new System.Windows.Forms.TableLayoutPanel();
            this.packageView = new Bonsai.NuGet.PackageView();
            this.packageIcons = new System.Windows.Forms.ImageList(this.components);
            this.detailsLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.searchLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.searchComboBox = new Bonsai.NuGet.CueBannerComboBox();
            this.packageDetails = new Bonsai.NuGet.PackageDetails();
            this.closePanel = new System.Windows.Forms.Panel();
            this.closeButton = new System.Windows.Forms.Button();
            this.settingsPanel = new System.Windows.Forms.Panel();
            this.settingsButton = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.mainLayoutPanel.SuspendLayout();
            this.packageViewLayoutPanel.SuspendLayout();
            this.filterLayoutPanel.SuspendLayout();
            this.pageSelectorPanel.SuspendLayout();
            this.packageViewPanel.SuspendLayout();
            this.detailsLayoutPanel.SuspendLayout();
            this.searchLayoutPanel.SuspendLayout();
            this.closePanel.SuspendLayout();
            this.settingsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainLayoutPanel
            // 
            this.mainLayoutPanel.ColumnCount = 2;
            this.mainLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 253F));
            this.mainLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.mainLayoutPanel.Controls.Add(this.packageViewLayoutPanel, 0, 0);
            this.mainLayoutPanel.Controls.Add(this.detailsLayoutPanel, 1, 0);
            this.mainLayoutPanel.Controls.Add(this.closePanel, 1, 1);
            this.mainLayoutPanel.Controls.Add(this.settingsPanel, 0, 1);
            this.mainLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.mainLayoutPanel.Name = "mainLayoutPanel";
            this.mainLayoutPanel.RowCount = 2;
            this.mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.mainLayoutPanel.Size = new System.Drawing.Size(804, 546);
            this.mainLayoutPanel.TabIndex = 0;
            // 
            // packageViewLayoutPanel
            // 
            this.packageViewLayoutPanel.ColumnCount = 1;
            this.packageViewLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.packageViewLayoutPanel.Controls.Add(this.filterLayoutPanel, 0, 0);
            this.packageViewLayoutPanel.Controls.Add(this.pageSelectorPanel, 0, 2);
            this.packageViewLayoutPanel.Controls.Add(this.packageViewPanel, 0, 1);
            this.packageViewLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.packageViewLayoutPanel.Location = new System.Drawing.Point(3, 3);
            this.packageViewLayoutPanel.Name = "packageViewLayoutPanel";
            this.packageViewLayoutPanel.RowCount = 3;
            this.packageViewLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.packageViewLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.packageViewLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.packageViewLayoutPanel.Size = new System.Drawing.Size(545, 490);
            this.packageViewLayoutPanel.TabIndex = 2;
            // 
            // filterLayoutPanel
            // 
            this.filterLayoutPanel.Controls.Add(this.releaseFilterComboBox);
            this.filterLayoutPanel.Controls.Add(this.sortLabel);
            this.filterLayoutPanel.Controls.Add(this.sortComboBox);
            this.filterLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filterLayoutPanel.Location = new System.Drawing.Point(3, 3);
            this.filterLayoutPanel.Name = "filterLayoutPanel";
            this.filterLayoutPanel.Size = new System.Drawing.Size(539, 24);
            this.filterLayoutPanel.TabIndex = 1;
            // 
            // releaseFilterComboBox
            // 
            this.releaseFilterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.releaseFilterComboBox.FormattingEnabled = true;
            this.releaseFilterComboBox.Items.AddRange(new object[] {
            "Stable Only",
            "Include Prerelease"});
            this.releaseFilterComboBox.Location = new System.Drawing.Point(3, 3);
            this.releaseFilterComboBox.Name = "releaseFilterComboBox";
            this.releaseFilterComboBox.Size = new System.Drawing.Size(121, 21);
            this.releaseFilterComboBox.TabIndex = 0;
            this.releaseFilterComboBox.SelectedIndexChanged += new System.EventHandler(this.filterComboBox_SelectedIndexChanged);
            // 
            // sortLabel
            // 
            this.sortLabel.Location = new System.Drawing.Point(130, 0);
            this.sortLabel.Name = "sortLabel";
            this.sortLabel.Size = new System.Drawing.Size(43, 22);
            this.sortLabel.TabIndex = 1;
            this.sortLabel.Text = "Sort by:";
            this.sortLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // sortComboBox
            // 
            this.sortComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sortComboBox.FormattingEnabled = true;
            this.sortComboBox.Location = new System.Drawing.Point(179, 3);
            this.sortComboBox.Name = "sortComboBox";
            this.sortComboBox.Size = new System.Drawing.Size(121, 21);
            this.sortComboBox.TabIndex = 2;
            this.sortComboBox.SelectedIndexChanged += new System.EventHandler(this.filterComboBox_SelectedIndexChanged);
            // 
            // pageSelectorPanel
            // 
            this.pageSelectorPanel.Controls.Add(this.packagePageSelector);
            this.pageSelectorPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pageSelectorPanel.Location = new System.Drawing.Point(3, 453);
            this.pageSelectorPanel.Name = "pageSelectorPanel";
            this.pageSelectorPanel.Size = new System.Drawing.Size(539, 34);
            this.pageSelectorPanel.TabIndex = 2;
            // 
            // packagePageSelector
            // 
            this.packagePageSelector.AutoSize = true;
            this.packagePageSelector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.packagePageSelector.Location = new System.Drawing.Point(0, 0);
            this.packagePageSelector.Name = "packagePageSelector";
            this.packagePageSelector.PageCount = 0;
            this.packagePageSelector.SelectedIndex = -1;
            this.packagePageSelector.Size = new System.Drawing.Size(539, 34);
            this.packagePageSelector.TabIndex = 0;
            this.packagePageSelector.SelectedIndexChanged += new System.EventHandler(this.packagePageSelector_SelectedIndexChanged);
            // 
            // packageViewPanel
            // 
            this.packageViewPanel.BackColor = System.Drawing.SystemColors.Control;
            this.packageViewPanel.ColumnCount = 1;
            this.packageViewPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.packageViewPanel.Controls.Add(this.packageView, 0, 1);
            this.packageViewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.packageViewPanel.Location = new System.Drawing.Point(3, 33);
            this.packageViewPanel.Name = "packageViewPanel";
            this.packageViewPanel.RowCount = 2;
            this.packageViewPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.packageViewPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.packageViewPanel.Size = new System.Drawing.Size(539, 414);
            this.packageViewPanel.TabIndex = 3;
            // 
            // packageView
            // 
            this.packageView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.packageView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.packageView.CanSelectNodes = false;
            this.packageView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.packageView.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.packageView.FullRowSelect = true;
            this.packageView.ImageIndex = 0;
            this.packageView.ImageList = this.packageIcons;
            this.packageView.ItemHeight = 64;
            this.packageView.Location = new System.Drawing.Point(3, 3);
            this.packageView.Name = "packageView";
            this.packageView.OperationText = null;
            this.packageView.SelectedImageIndex = 0;
            this.packageView.ShowLines = false;
            this.packageView.ShowRootLines = false;
            this.packageView.Size = new System.Drawing.Size(533, 408);
            this.packageView.TabIndex = 1;
            this.packageView.OperationClick += new System.Windows.Forms.TreeViewEventHandler(this.packageView_OperationClick);
            this.packageView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.packageView_AfterSelect);
            // 
            // packageIcons
            // 
            this.packageIcons.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.packageIcons.ImageSize = new System.Drawing.Size(32, 32);
            this.packageIcons.TransparentColor = System.Drawing.Color.Violet;
            // 
            // detailsLayoutPanel
            // 
            this.detailsLayoutPanel.ColumnCount = 1;
            this.detailsLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.detailsLayoutPanel.Controls.Add(this.searchLayoutPanel, 0, 0);
            this.detailsLayoutPanel.Controls.Add(this.packageDetails, 0, 1);
            this.detailsLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.detailsLayoutPanel.Location = new System.Drawing.Point(554, 3);
            this.detailsLayoutPanel.Name = "detailsLayoutPanel";
            this.detailsLayoutPanel.RowCount = 2;
            this.detailsLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.detailsLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.detailsLayoutPanel.Size = new System.Drawing.Size(247, 490);
            this.detailsLayoutPanel.TabIndex = 3;
            // 
            // searchLayoutPanel
            // 
            this.searchLayoutPanel.Controls.Add(this.searchComboBox);
            this.searchLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchLayoutPanel.Location = new System.Drawing.Point(3, 3);
            this.searchLayoutPanel.Name = "searchLayoutPanel";
            this.searchLayoutPanel.Size = new System.Drawing.Size(241, 24);
            this.searchLayoutPanel.TabIndex = 0;
            // 
            // searchComboBox
            // 
            this.searchComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.searchComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.searchComboBox.CueBanner = null;
            this.searchComboBox.FormattingEnabled = true;
            this.searchComboBox.Location = new System.Drawing.Point(3, 3);
            this.searchComboBox.Name = "searchComboBox";
            this.searchComboBox.Size = new System.Drawing.Size(147, 21);
            this.searchComboBox.TabIndex = 0;
            // 
            // packageDetails
            // 
            this.packageDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.packageDetails.Location = new System.Drawing.Point(3, 33);
            this.packageDetails.Name = "packageDetails";
            this.packageDetails.Size = new System.Drawing.Size(241, 454);
            this.packageDetails.TabIndex = 1;
            // 
            // closePanel
            // 
            this.closePanel.Controls.Add(this.closeButton);
            this.closePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.closePanel.Location = new System.Drawing.Point(554, 499);
            this.closePanel.Name = "closePanel";
            this.closePanel.Size = new System.Drawing.Size(247, 44);
            this.closePanel.TabIndex = 5;
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeButton.Location = new System.Drawing.Point(163, 12);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 0;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // settingsPanel
            // 
            this.settingsPanel.Controls.Add(this.settingsButton);
            this.settingsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingsPanel.Location = new System.Drawing.Point(3, 499);
            this.settingsPanel.Name = "settingsPanel";
            this.settingsPanel.Size = new System.Drawing.Size(545, 44);
            this.settingsPanel.TabIndex = 6;
            // 
            // settingsButton
            // 
            this.settingsButton.Location = new System.Drawing.Point(6, 12);
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Size = new System.Drawing.Size(75, 23);
            this.settingsButton.TabIndex = 0;
            this.settingsButton.Text = "Settings";
            this.settingsButton.UseVisualStyleBackColor = true;
            this.settingsButton.Click += new System.EventHandler(this.settingsButton_Click);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.OverwritePrompt = false;
            this.saveFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog_FileOk);
            // 
            // GalleryDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.closeButton;
            this.ClientSize = new System.Drawing.Size(804, 546);
            this.Controls.Add(this.mainLayoutPanel);
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(820, 585);
            this.Name = "GalleryDialog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bonsai - Gallery";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PackageManagerDialog_KeyDown);
            this.mainLayoutPanel.ResumeLayout(false);
            this.packageViewLayoutPanel.ResumeLayout(false);
            this.filterLayoutPanel.ResumeLayout(false);
            this.pageSelectorPanel.ResumeLayout(false);
            this.pageSelectorPanel.PerformLayout();
            this.packageViewPanel.ResumeLayout(false);
            this.detailsLayoutPanel.ResumeLayout(false);
            this.searchLayoutPanel.ResumeLayout(false);
            this.closePanel.ResumeLayout(false);
            this.settingsPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel mainLayoutPanel;
        private System.Windows.Forms.ImageList packageIcons;
        private System.Windows.Forms.FlowLayoutPanel filterLayoutPanel;
        private System.Windows.Forms.ComboBox releaseFilterComboBox;
        private System.Windows.Forms.Label sortLabel;
        private System.Windows.Forms.ComboBox sortComboBox;
        private System.Windows.Forms.TableLayoutPanel packageViewLayoutPanel;
        private System.Windows.Forms.TableLayoutPanel detailsLayoutPanel;
        private System.Windows.Forms.FlowLayoutPanel searchLayoutPanel;
        private Bonsai.NuGet.CueBannerComboBox searchComboBox;
        private PackageDetails packageDetails;
        private System.Windows.Forms.Panel pageSelectorPanel;
        private PackagePageSelector packagePageSelector;
        private System.Windows.Forms.Panel closePanel;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button settingsButton;
        private System.Windows.Forms.TableLayoutPanel packageViewPanel;
        private PackageView packageView;
        private System.Windows.Forms.Panel settingsPanel;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}