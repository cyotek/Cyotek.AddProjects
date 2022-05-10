namespace Cyotek.VisualStudioExtensions.AddProjects
{
  partial class AddProjectsDialog
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
            System.Windows.Forms.Label projectsLabel;
            System.Windows.Forms.Label filterLabel;
            this.projectsListView = new Cyotek.VisualStudioExtensions.AddProjects.FileNameListView();
            this.addFileButton = new System.Windows.Forms.Button();
            this.addFolderButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            this.filterTextBox = new System.Windows.Forms.TextBox();
            this.updateFilterTimer = new System.Windows.Forms.Timer(this.components);
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.homeLinkLabel = new System.Windows.Forms.LinkLabel();
            this.settingsButton = new System.Windows.Forms.Button();
            this.chkCreateSolutionFoldersStructure = new System.Windows.Forms.CheckBox();
            projectsLabel = new System.Windows.Forms.Label();
            filterLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // projectsLabel
            // 
            projectsLabel.AutoSize = true;
            projectsLabel.Location = new System.Drawing.Point(9, 22);
            projectsLabel.Name = "projectsLabel";
            projectsLabel.Size = new System.Drawing.Size(224, 13);
            projectsLabel.TabIndex = 0;
            projectsLabel.Text = "Select the &projects to add the current solution:";
            // 
            // filterLabel
            // 
            filterLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            filterLabel.AutoSize = true;
            filterLabel.Location = new System.Drawing.Point(424, 15);
            filterLabel.Name = "filterLabel";
            filterLabel.Size = new System.Drawing.Size(32, 13);
            filterLabel.TabIndex = 2;
            filterLabel.Text = "F&ilter:";
            // 
            // projectsListView
            // 
            this.projectsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.projectsListView.CheckBoxes = true;
            this.projectsListView.FullRowSelect = true;
            this.projectsListView.HideSelection = false;
            this.projectsListView.Location = new System.Drawing.Point(12, 38);
            this.projectsListView.Name = "projectsListView";
            this.projectsListView.ShowItemToolTips = true;
            this.projectsListView.Size = new System.Drawing.Size(660, 345);
            this.projectsListView.TabIndex = 1;
            this.projectsListView.UseCompatibleStateImageBehavior = false;
            this.projectsListView.View = System.Windows.Forms.View.Details;
            this.projectsListView.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.projectsListView_ItemChecked);
            this.projectsListView.SelectedIndexChanged += new System.EventHandler(this.projectsListView_SelectedIndexChanged);
            this.projectsListView.KeyUp += new System.Windows.Forms.KeyEventHandler(this.projectsListView_KeyUp);
            // 
            // addFileButton
            // 
            this.addFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addFileButton.Location = new System.Drawing.Point(678, 38);
            this.addFileButton.Name = "addFileButton";
            this.addFileButton.Size = new System.Drawing.Size(94, 23);
            this.addFileButton.TabIndex = 4;
            this.addFileButton.Text = "&Add File...";
            this.toolTip.SetToolTip(this.addFileButton, "Add projects from a single folder");
            this.addFileButton.UseVisualStyleBackColor = true;
            this.addFileButton.Click += new System.EventHandler(this.addFileButton_Click);
            // 
            // addFolderButton
            // 
            this.addFolderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addFolderButton.Location = new System.Drawing.Point(678, 67);
            this.addFolderButton.Name = "addFolderButton";
            this.addFolderButton.Size = new System.Drawing.Size(94, 23);
            this.addFolderButton.TabIndex = 5;
            this.addFolderButton.Text = "Add &Folder...";
            this.toolTip.SetToolTip(this.addFolderButton, "Add all projects in a folder and its children");
            this.addFolderButton.UseVisualStyleBackColor = true;
            this.addFolderButton.Click += new System.EventHandler(this.addFolderButton_Click);
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Location = new System.Drawing.Point(616, 389);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 8;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(697, 389);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 9;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // removeButton
            // 
            this.removeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.removeButton.Enabled = false;
            this.removeButton.Location = new System.Drawing.Point(678, 96);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(94, 23);
            this.removeButton.TabIndex = 6;
            this.removeButton.Text = "&Remove...";
            this.toolTip.SetToolTip(this.removeButton, "Remove the selected projects");
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // filterTextBox
            // 
            this.filterTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.filterTextBox.Location = new System.Drawing.Point(462, 12);
            this.filterTextBox.Name = "filterTextBox";
            this.filterTextBox.Size = new System.Drawing.Size(210, 20);
            this.filterTextBox.TabIndex = 3;
            this.toolTip.SetToolTip(this.filterTextBox, "Enter a regular expression to filter the project list");
            this.filterTextBox.TextChanged += new System.EventHandler(this.filterTextBox_TextChanged);
            // 
            // updateFilterTimer
            // 
            this.updateFilterTimer.Tick += new System.EventHandler(this.updateFilterTimer_Tick);
            // 
            // homeLinkLabel
            // 
            this.homeLinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.homeLinkLabel.AutoSize = true;
            this.homeLinkLabel.Location = new System.Drawing.Point(9, 394);
            this.homeLinkLabel.Name = "homeLinkLabel";
            this.homeLinkLabel.Size = new System.Drawing.Size(89, 13);
            this.homeLinkLabel.TabIndex = 10;
            this.homeLinkLabel.TabStop = true;
            this.homeLinkLabel.Text = "www.cyotek.com";
            this.toolTip.SetToolTip(this.homeLinkLabel, "Visit www.cyotek.com");
            this.homeLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.homeLinkLabel_LinkClicked);
            // 
            // settingsButton
            // 
            this.settingsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.settingsButton.Location = new System.Drawing.Point(678, 131);
            this.settingsButton.Margin = new System.Windows.Forms.Padding(3, 9, 3, 3);
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Size = new System.Drawing.Size(94, 23);
            this.settingsButton.TabIndex = 7;
            this.settingsButton.Text = "&Settings...";
            this.toolTip.SetToolTip(this.settingsButton, "Remove the selected projects");
            this.settingsButton.UseVisualStyleBackColor = true;
            this.settingsButton.Click += new System.EventHandler(this.settingsButton_Click);
            // 
            // chkCreateSolutionFoldersStructure
            // 
            this.chkCreateSolutionFoldersStructure.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkCreateSolutionFoldersStructure.AutoSize = true;
            this.chkCreateSolutionFoldersStructure.Location = new System.Drawing.Point(427, 393);
            this.chkCreateSolutionFoldersStructure.Name = "chkCreateSolutionFoldersStructure";
            this.chkCreateSolutionFoldersStructure.Size = new System.Drawing.Size(174, 17);
            this.chkCreateSolutionFoldersStructure.TabIndex = 11;
            this.chkCreateSolutionFoldersStructure.Text = "Create solution folders structure";
            this.chkCreateSolutionFoldersStructure.UseVisualStyleBackColor = true;
            // 
            // AddProjectsDialog
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(784, 424);
            this.Controls.Add(this.chkCreateSolutionFoldersStructure);
            this.Controls.Add(this.settingsButton);
            this.Controls.Add(this.homeLinkLabel);
            this.Controls.Add(this.filterTextBox);
            this.Controls.Add(filterLabel);
            this.Controls.Add(this.removeButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.addFolderButton);
            this.Controls.Add(this.addFileButton);
            this.Controls.Add(this.projectsListView);
            this.Controls.Add(projectsLabel);
            this.Name = "AddProjectsDialog";
            this.Text = "Add Existing Projects";
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion
    private FileNameListView projectsListView;
    private System.Windows.Forms.Button addFileButton;
    private System.Windows.Forms.Button addFolderButton;
    private System.Windows.Forms.Button okButton;
    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.Button removeButton;
    private System.Windows.Forms.TextBox filterTextBox;
    private System.Windows.Forms.Timer updateFilterTimer;
    private System.Windows.Forms.ToolTip toolTip;
    private System.Windows.Forms.LinkLabel homeLinkLabel;
    private System.Windows.Forms.Button settingsButton;
    private System.Windows.Forms.CheckBox chkCreateSolutionFoldersStructure;
  }
}
