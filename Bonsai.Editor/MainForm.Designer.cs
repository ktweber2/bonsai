﻿namespace Bonsai.Editor
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Source");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Property");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Transform");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Sink");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Combinator");
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.examplesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.groupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.workflowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.packageManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.indexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.welcomeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openWorkflowDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveWorkflowDialog = new System.Windows.Forms.SaveFileDialog();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.fileToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.undoToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.redoToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.editToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.startToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.stopToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.restartToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.helpToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.directoryToolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.browseDirectoryToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusImageLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolboxGroupBox = new System.Windows.Forms.GroupBox();
            this.toolboxSplitContainer = new System.Windows.Forms.SplitContainer();
            this.toolboxTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.searchTextBox = new Bonsai.Design.CueBannerTextBox();
            this.toolboxTreeView = new System.Windows.Forms.TreeView();
            this.toolboxDescriptionPanel = new System.Windows.Forms.Panel();
            this.toolboxDescriptionTextBox = new System.Windows.Forms.RichTextBox();
            this.workflowGroupBox = new System.Windows.Forms.GroupBox();
            this.propertiesGroupBox = new System.Windows.Forms.GroupBox();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.propertyGridContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.resetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.descriptionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelSplitContainer = new System.Windows.Forms.SplitContainer();
            this.workflowSplitContainer = new System.Windows.Forms.SplitContainer();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.commandExecutor = new Bonsai.Design.CommandExecutor();
            this.menuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.toolboxGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.toolboxSplitContainer)).BeginInit();
            this.toolboxSplitContainer.Panel1.SuspendLayout();
            this.toolboxSplitContainer.Panel2.SuspendLayout();
            this.toolboxSplitContainer.SuspendLayout();
            this.toolboxTableLayoutPanel.SuspendLayout();
            this.toolboxDescriptionPanel.SuspendLayout();
            this.propertiesGroupBox.SuspendLayout();
            this.propertyGridContextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelSplitContainer)).BeginInit();
            this.panelSplitContainer.Panel1.SuspendLayout();
            this.panelSplitContainer.Panel2.SuspendLayout();
            this.panelSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.workflowSplitContainer)).BeginInit();
            this.workflowSplitContainer.Panel1.SuspendLayout();
            this.workflowSplitContainer.Panel2.SuspendLayout();
            this.workflowSplitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.workflowToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(684, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.examplesToolStripMenuItem,
            this.toolStripSeparator,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripMenuItem.Image")));
            this.newToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.newToolStripMenuItem.Text = "&New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
            this.openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // examplesToolStripMenuItem
            // 
            this.examplesToolStripMenuItem.Name = "examplesToolStripMenuItem";
            this.examplesToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.examplesToolStripMenuItem.Text = "&Examples";
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(143, 6);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
            this.saveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.saveAsToolStripMenuItem.Text = "Save &As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(143, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.toolStripSeparator3,
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.toolStripSeparator2,
            this.groupToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Enabled = false;
            this.undoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("undoToolStripMenuItem.Image")));
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.undoToolStripMenuItem.Text = "&Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Enabled = false;
            this.redoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("redoToolStripMenuItem.Image")));
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.redoToolStripMenuItem.Text = "&Redo";
            this.redoToolStripMenuItem.Click += new System.EventHandler(this.redoToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(146, 6);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("cutToolStripMenuItem.Image")));
            this.cutToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.cutToolStripMenuItem.Text = "Cu&t";
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copyToolStripMenuItem.Image")));
            this.copyToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.copyToolStripMenuItem.Text = "&Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pasteToolStripMenuItem.Image")));
            this.pasteToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.pasteToolStripMenuItem.Text = "&Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("deleteToolStripMenuItem.Image")));
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.ShortcutKeyDisplayString = "Del";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.deleteToolStripMenuItem.Text = "&Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(146, 6);
            // 
            // groupToolStripMenuItem
            // 
            this.groupToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("groupToolStripMenuItem.Image")));
            this.groupToolStripMenuItem.Name = "groupToolStripMenuItem";
            this.groupToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.groupToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.groupToolStripMenuItem.Text = "&Group";
            this.groupToolStripMenuItem.Click += new System.EventHandler(this.groupToolStripMenuItem_Click);
            // 
            // workflowToolStripMenuItem
            // 
            this.workflowToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.stopToolStripMenuItem,
            this.restartToolStripMenuItem});
            this.workflowToolStripMenuItem.Name = "workflowToolStripMenuItem";
            this.workflowToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.workflowToolStripMenuItem.Text = "&Workflow";
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("startToolStripMenuItem.Image")));
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.startToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.startToolStripMenuItem.Text = "&Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("stopToolStripMenuItem.Image")));
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F5)));
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.stopToolStripMenuItem.Text = "S&top";
            this.stopToolStripMenuItem.Visible = false;
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // restartToolStripMenuItem
            // 
            this.restartToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("restartToolStripMenuItem.Image")));
            this.restartToolStripMenuItem.Name = "restartToolStripMenuItem";
            this.restartToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.F5)));
            this.restartToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.restartToolStripMenuItem.Text = "&Restart";
            this.restartToolStripMenuItem.Visible = false;
            this.restartToolStripMenuItem.Click += new System.EventHandler(this.restartToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.packageManagerToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // packageManagerToolStripMenuItem
            // 
            this.packageManagerToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("packageManagerToolStripMenuItem.Image")));
            this.packageManagerToolStripMenuItem.Name = "packageManagerToolStripMenuItem";
            this.packageManagerToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.packageManagerToolStripMenuItem.Text = "&Manage Packages...";
            this.packageManagerToolStripMenuItem.Click += new System.EventHandler(this.packageManagerToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contentsToolStripMenuItem,
            this.indexToolStripMenuItem,
            this.searchToolStripMenuItem,
            this.toolStripSeparator5,
            this.welcomeToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // contentsToolStripMenuItem
            // 
            this.contentsToolStripMenuItem.Name = "contentsToolStripMenuItem";
            this.contentsToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.contentsToolStripMenuItem.Text = "&Contents";
            // 
            // indexToolStripMenuItem
            // 
            this.indexToolStripMenuItem.Name = "indexToolStripMenuItem";
            this.indexToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.indexToolStripMenuItem.Text = "&Index";
            // 
            // searchToolStripMenuItem
            // 
            this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            this.searchToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.searchToolStripMenuItem.Text = "&Search";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(121, 6);
            // 
            // welcomeToolStripMenuItem
            // 
            this.welcomeToolStripMenuItem.Name = "welcomeToolStripMenuItem";
            this.welcomeToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.welcomeToolStripMenuItem.Text = "&Welcome";
            this.welcomeToolStripMenuItem.Click += new System.EventHandler(this.welcomeToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.aboutToolStripMenuItem.Text = "&About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // openWorkflowDialog
            // 
            this.openWorkflowDialog.Filter = "Bonsai Files|*.bonsai";
            // 
            // saveWorkflowDialog
            // 
            this.saveWorkflowDialog.Filter = "Bonsai Files|*.bonsai";
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripButton,
            this.openToolStripButton,
            this.saveToolStripButton,
            this.fileToolStripSeparator,
            this.undoToolStripButton,
            this.redoToolStripButton,
            this.editToolStripSeparator,
            this.startToolStripButton,
            this.stopToolStripButton,
            this.restartToolStripButton,
            this.helpToolStripSeparator,
            this.directoryToolStripTextBox,
            this.browseDirectoryToolStripButton});
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(684, 25);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "toolStrip1";
            // 
            // newToolStripButton
            // 
            this.newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripButton.Image")));
            this.newToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripButton.Name = "newToolStripButton";
            this.newToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.newToolStripButton.Text = "&New";
            this.newToolStripButton.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripButton.Image")));
            this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.openToolStripButton.Text = "&Open";
            this.openToolStripButton.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripButton.Image")));
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.saveToolStripButton.Text = "&Save";
            this.saveToolStripButton.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // fileToolStripSeparator
            // 
            this.fileToolStripSeparator.Name = "fileToolStripSeparator";
            this.fileToolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // undoToolStripButton
            // 
            this.undoToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.undoToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("undoToolStripButton.Image")));
            this.undoToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.undoToolStripButton.Name = "undoToolStripButton";
            this.undoToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.undoToolStripButton.Text = "&Undo";
            this.undoToolStripButton.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            // 
            // redoToolStripButton
            // 
            this.redoToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.redoToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("redoToolStripButton.Image")));
            this.redoToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.redoToolStripButton.Name = "redoToolStripButton";
            this.redoToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.redoToolStripButton.Text = "&Redo";
            this.redoToolStripButton.Click += new System.EventHandler(this.redoToolStripMenuItem_Click);
            // 
            // editToolStripSeparator
            // 
            this.editToolStripSeparator.Name = "editToolStripSeparator";
            this.editToolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // startToolStripButton
            // 
            this.startToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.startToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("startToolStripButton.Image")));
            this.startToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.startToolStripButton.Name = "startToolStripButton";
            this.startToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.startToolStripButton.Text = "Sta&rt";
            this.startToolStripButton.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // stopToolStripButton
            // 
            this.stopToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.stopToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("stopToolStripButton.Image")));
            this.stopToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stopToolStripButton.Name = "stopToolStripButton";
            this.stopToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.stopToolStripButton.Text = "S&top";
            this.stopToolStripButton.Visible = false;
            this.stopToolStripButton.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // restartToolStripButton
            // 
            this.restartToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.restartToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("restartToolStripButton.Image")));
            this.restartToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.restartToolStripButton.Name = "restartToolStripButton";
            this.restartToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.restartToolStripButton.Text = "Restart";
            this.restartToolStripButton.Visible = false;
            this.restartToolStripButton.Click += new System.EventHandler(this.restartToolStripMenuItem_Click);
            // 
            // helpToolStripSeparator
            // 
            this.helpToolStripSeparator.Name = "helpToolStripSeparator";
            this.helpToolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // directoryToolStripTextBox
            // 
            this.directoryToolStripTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.directoryToolStripTextBox.Name = "directoryToolStripTextBox";
            this.directoryToolStripTextBox.Size = new System.Drawing.Size(200, 25);
            this.directoryToolStripTextBox.Leave += new System.EventHandler(this.directoryToolStripTextBox_Leave);
            this.directoryToolStripTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.directoryToolStripTextBox_KeyDown);
            this.directoryToolStripTextBox.DoubleClick += new System.EventHandler(this.directoryToolStripTextBox_DoubleClick);
            this.directoryToolStripTextBox.TextChanged += new System.EventHandler(this.directoryToolStripTextBox_TextChanged);
            // 
            // browseDirectoryToolStripButton
            // 
            this.browseDirectoryToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.browseDirectoryToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("browseDirectoryToolStripButton.Image")));
            this.browseDirectoryToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.browseDirectoryToolStripButton.Name = "browseDirectoryToolStripButton";
            this.browseDirectoryToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.browseDirectoryToolStripButton.Text = "&Browse Directory";
            this.browseDirectoryToolStripButton.Click += new System.EventHandler(this.browseDirectoryToolStripButton_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusImageLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 339);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(684, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip";
            // 
            // statusImageLabel
            // 
            this.statusImageLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.statusImageLabel.Image = global::Bonsai.Editor.Properties.Resources.StatusReadyImage;
            this.statusImageLabel.Name = "statusImageLabel";
            this.statusImageLabel.Size = new System.Drawing.Size(16, 17);
            this.statusImageLabel.Text = "statusImageLabel";
            // 
            // toolboxGroupBox
            // 
            this.toolboxGroupBox.Controls.Add(this.toolboxSplitContainer);
            this.toolboxGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolboxGroupBox.Location = new System.Drawing.Point(0, 0);
            this.toolboxGroupBox.Name = "toolboxGroupBox";
            this.toolboxGroupBox.Size = new System.Drawing.Size(200, 290);
            this.toolboxGroupBox.TabIndex = 0;
            this.toolboxGroupBox.TabStop = false;
            this.toolboxGroupBox.Text = "Toolbox";
            // 
            // toolboxSplitContainer
            // 
            this.toolboxSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolboxSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.toolboxSplitContainer.Location = new System.Drawing.Point(3, 16);
            this.toolboxSplitContainer.Name = "toolboxSplitContainer";
            this.toolboxSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // toolboxSplitContainer.Panel1
            // 
            this.toolboxSplitContainer.Panel1.Controls.Add(this.toolboxTableLayoutPanel);
            // 
            // toolboxSplitContainer.Panel2
            // 
            this.toolboxSplitContainer.Panel2.Controls.Add(this.toolboxDescriptionPanel);
            this.toolboxSplitContainer.Size = new System.Drawing.Size(194, 271);
            this.toolboxSplitContainer.SplitterDistance = 208;
            this.toolboxSplitContainer.TabIndex = 1;
            this.toolboxSplitContainer.TabStop = false;
            // 
            // toolboxTableLayoutPanel
            // 
            this.toolboxTableLayoutPanel.ColumnCount = 1;
            this.toolboxTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.toolboxTableLayoutPanel.Controls.Add(this.searchTextBox, 0, 0);
            this.toolboxTableLayoutPanel.Controls.Add(this.toolboxTreeView, 0, 1);
            this.toolboxTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolboxTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.toolboxTableLayoutPanel.Name = "toolboxTableLayoutPanel";
            this.toolboxTableLayoutPanel.RowCount = 2;
            this.toolboxTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.toolboxTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.toolboxTableLayoutPanel.Size = new System.Drawing.Size(194, 208);
            this.toolboxTableLayoutPanel.TabIndex = 2;
            // 
            // searchTextBox
            // 
            this.searchTextBox.CueBanner = null;
            this.searchTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchTextBox.Location = new System.Drawing.Point(0, 3);
            this.searchTextBox.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(194, 20);
            this.searchTextBox.TabIndex = 1;
            this.searchTextBox.TextChanged += new System.EventHandler(this.searchTextBox_TextChanged);
            this.searchTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchTextBox_KeyDown);
            // 
            // toolboxTreeView
            // 
            this.toolboxTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolboxTreeView.HideSelection = false;
            this.toolboxTreeView.Location = new System.Drawing.Point(0, 26);
            this.toolboxTreeView.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.toolboxTreeView.Name = "toolboxTreeView";
            treeNode1.Name = "Source";
            treeNode1.Text = "Source";
            treeNode2.Name = "Property";
            treeNode2.Text = "Property";
            treeNode3.Name = "Transform";
            treeNode3.Text = "Transform";
            treeNode4.Name = "Sink";
            treeNode4.Text = "Sink";
            treeNode5.Name = "Combinator";
            treeNode5.Text = "Combinator";
            this.toolboxTreeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5});
            this.toolboxTreeView.Size = new System.Drawing.Size(194, 182);
            this.toolboxTreeView.TabIndex = 0;
            this.toolboxTreeView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.toolboxTreeView_ItemDrag);
            this.toolboxTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.toolboxTreeView_AfterSelect);
            this.toolboxTreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.toolboxTreeView_NodeMouseDoubleClick);
            this.toolboxTreeView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.toolboxTreeView_KeyDown);
            // 
            // toolboxDescriptionPanel
            // 
            this.toolboxDescriptionPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.toolboxDescriptionPanel.Controls.Add(this.toolboxDescriptionTextBox);
            this.toolboxDescriptionPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolboxDescriptionPanel.Location = new System.Drawing.Point(0, 0);
            this.toolboxDescriptionPanel.Name = "toolboxDescriptionPanel";
            this.toolboxDescriptionPanel.Size = new System.Drawing.Size(194, 59);
            this.toolboxDescriptionPanel.TabIndex = 0;
            // 
            // toolboxDescriptionTextBox
            // 
            this.toolboxDescriptionTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.toolboxDescriptionTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolboxDescriptionTextBox.Location = new System.Drawing.Point(0, 0);
            this.toolboxDescriptionTextBox.Name = "toolboxDescriptionTextBox";
            this.toolboxDescriptionTextBox.ReadOnly = true;
            this.toolboxDescriptionTextBox.Size = new System.Drawing.Size(192, 57);
            this.toolboxDescriptionTextBox.TabIndex = 0;
            this.toolboxDescriptionTextBox.TabStop = false;
            this.toolboxDescriptionTextBox.Text = "";
            // 
            // workflowGroupBox
            // 
            this.workflowGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.workflowGroupBox.Location = new System.Drawing.Point(0, 0);
            this.workflowGroupBox.Name = "workflowGroupBox";
            this.workflowGroupBox.Size = new System.Drawing.Size(300, 290);
            this.workflowGroupBox.TabIndex = 1;
            this.workflowGroupBox.TabStop = false;
            this.workflowGroupBox.Text = "Workflow";
            // 
            // propertiesGroupBox
            // 
            this.propertiesGroupBox.Controls.Add(this.propertyGrid);
            this.propertiesGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertiesGroupBox.Location = new System.Drawing.Point(0, 0);
            this.propertiesGroupBox.Name = "propertiesGroupBox";
            this.propertiesGroupBox.Size = new System.Drawing.Size(176, 290);
            this.propertiesGroupBox.TabIndex = 2;
            this.propertiesGroupBox.TabStop = false;
            this.propertiesGroupBox.Text = "Properties";
            // 
            // propertyGrid
            // 
            this.propertyGrid.ContextMenuStrip = this.propertyGridContextMenuStrip;
            this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid.Location = new System.Drawing.Point(3, 16);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.Size = new System.Drawing.Size(170, 271);
            this.propertyGrid.TabIndex = 0;
            this.propertyGrid.Validated += new System.EventHandler(this.propertyGrid_Validated);
            // 
            // propertyGridContextMenuStrip
            // 
            this.propertyGridContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetToolStripMenuItem,
            this.toolStripSeparator7,
            this.descriptionToolStripMenuItem});
            this.propertyGridContextMenuStrip.Name = "propertyGridContextMenuStrip";
            this.propertyGridContextMenuStrip.Size = new System.Drawing.Size(135, 54);
            this.propertyGridContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.propertyGridContextMenuStrip_Opening);
            // 
            // resetToolStripMenuItem
            // 
            this.resetToolStripMenuItem.Name = "resetToolStripMenuItem";
            this.resetToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.resetToolStripMenuItem.Text = "&Reset";
            this.resetToolStripMenuItem.Click += new System.EventHandler(this.resetToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(131, 6);
            // 
            // descriptionToolStripMenuItem
            // 
            this.descriptionToolStripMenuItem.Checked = true;
            this.descriptionToolStripMenuItem.CheckOnClick = true;
            this.descriptionToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.descriptionToolStripMenuItem.Name = "descriptionToolStripMenuItem";
            this.descriptionToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.descriptionToolStripMenuItem.Text = "&Description";
            this.descriptionToolStripMenuItem.Click += new System.EventHandler(this.descriptionToolStripMenuItem_Click);
            // 
            // panelSplitContainer
            // 
            this.panelSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.panelSplitContainer.Location = new System.Drawing.Point(0, 49);
            this.panelSplitContainer.Name = "panelSplitContainer";
            // 
            // panelSplitContainer.Panel1
            // 
            this.panelSplitContainer.Panel1.Controls.Add(this.toolboxGroupBox);
            // 
            // panelSplitContainer.Panel2
            // 
            this.panelSplitContainer.Panel2.Controls.Add(this.workflowSplitContainer);
            this.panelSplitContainer.Size = new System.Drawing.Size(684, 290);
            this.panelSplitContainer.SplitterDistance = 200;
            this.panelSplitContainer.TabIndex = 4;
            this.panelSplitContainer.TabStop = false;
            // 
            // workflowSplitContainer
            // 
            this.workflowSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.workflowSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.workflowSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.workflowSplitContainer.Name = "workflowSplitContainer";
            // 
            // workflowSplitContainer.Panel1
            // 
            this.workflowSplitContainer.Panel1.Controls.Add(this.workflowGroupBox);
            // 
            // workflowSplitContainer.Panel2
            // 
            this.workflowSplitContainer.Panel2.Controls.Add(this.propertiesGroupBox);
            this.workflowSplitContainer.Size = new System.Drawing.Size(480, 290);
            this.workflowSplitContainer.SplitterDistance = 300;
            this.workflowSplitContainer.TabIndex = 0;
            this.workflowSplitContainer.TabStop = false;
            // 
            // commandExecutor
            // 
            this.commandExecutor.StatusChanged += new System.EventHandler(this.commandExecutor_StatusChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 361);
            this.Controls.Add(this.panelSplitContainer);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip;
            this.MinimumSize = new System.Drawing.Size(600, 343);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bonsai";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.toolboxGroupBox.ResumeLayout(false);
            this.toolboxSplitContainer.Panel1.ResumeLayout(false);
            this.toolboxSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.toolboxSplitContainer)).EndInit();
            this.toolboxSplitContainer.ResumeLayout(false);
            this.toolboxTableLayoutPanel.ResumeLayout(false);
            this.toolboxTableLayoutPanel.PerformLayout();
            this.toolboxDescriptionPanel.ResumeLayout(false);
            this.propertiesGroupBox.ResumeLayout(false);
            this.propertyGridContextMenuStrip.ResumeLayout(false);
            this.panelSplitContainer.Panel1.ResumeLayout(false);
            this.panelSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelSplitContainer)).EndInit();
            this.panelSplitContainer.ResumeLayout(false);
            this.workflowSplitContainer.Panel1.ResumeLayout(false);
            this.workflowSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.workflowSplitContainer)).EndInit();
            this.workflowSplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem workflowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem contentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem indexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openWorkflowDialog;
        private System.Windows.Forms.SaveFileDialog saveWorkflowDialog;
        private Design.CommandExecutor commandExecutor;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton newToolStripButton;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.ToolStripSeparator fileToolStripSeparator;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.GroupBox toolboxGroupBox;
        private System.Windows.Forms.TreeView toolboxTreeView;
        private System.Windows.Forms.GroupBox workflowGroupBox;
        private System.Windows.Forms.GroupBox propertiesGroupBox;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.SplitContainer panelSplitContainer;
        private System.Windows.Forms.SplitContainer workflowSplitContainer;
        private System.Windows.Forms.ToolStripSeparator editToolStripSeparator;
        private System.Windows.Forms.ToolStripSeparator helpToolStripSeparator;
        private System.Windows.Forms.ToolStripTextBox directoryToolStripTextBox;
        private System.Windows.Forms.ToolStripButton browseDirectoryToolStripButton;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.SplitContainer toolboxSplitContainer;
        private System.Windows.Forms.Panel toolboxDescriptionPanel;
        private System.Windows.Forms.RichTextBox toolboxDescriptionTextBox;
        private System.Windows.Forms.ToolStripMenuItem welcomeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem examplesToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip propertyGridContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem resetToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem descriptionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem packageManagerToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel toolboxTableLayoutPanel;
        private Bonsai.Design.CueBannerTextBox searchTextBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem groupToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton undoToolStripButton;
        private System.Windows.Forms.ToolStripButton redoToolStripButton;
        private System.Windows.Forms.ToolStripButton startToolStripButton;
        private System.Windows.Forms.ToolStripButton stopToolStripButton;
        private System.Windows.Forms.ToolStripButton restartToolStripButton;
        private System.Windows.Forms.ToolStripMenuItem restartToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel statusImageLabel;
    }
}

