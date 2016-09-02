namespace Cyotek.VisualStudioExtensions.AddProjects
{
  partial class FolderExclusionsDialog
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
      System.Windows.Forms.Label folderExclusionsLabel;
      System.Windows.Forms.Label projectTypesLabel;
      this.folderExclusionsTextBox = new System.Windows.Forms.TextBox();
      this.okButton = new System.Windows.Forms.Button();
      this.cancelButton = new System.Windows.Forms.Button();
      this.resetToDefaultLinkLabel = new System.Windows.Forms.LinkLabel();
      this.projectTypesTextBox = new System.Windows.Forms.TextBox();
      folderExclusionsLabel = new System.Windows.Forms.Label();
      projectTypesLabel = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // folderExclusionsLabel
      // 
      folderExclusionsLabel.AutoSize = true;
      folderExclusionsLabel.Location = new System.Drawing.Point(9, 9);
      folderExclusionsLabel.Name = "folderExclusionsLabel";
      folderExclusionsLabel.Size = new System.Drawing.Size(252, 13);
      folderExclusionsLabel.TabIndex = 0;
      folderExclusionsLabel.Text = "Enter &folder exclusions below, one exclusion per line";
      // 
      // folderExclusionsTextBox
      // 
      this.folderExclusionsTextBox.AcceptsReturn = true;
      this.folderExclusionsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.folderExclusionsTextBox.Location = new System.Drawing.Point(12, 25);
      this.folderExclusionsTextBox.Multiline = true;
      this.folderExclusionsTextBox.Name = "folderExclusionsTextBox";
      this.folderExclusionsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.folderExclusionsTextBox.Size = new System.Drawing.Size(284, 280);
      this.folderExclusionsTextBox.TabIndex = 1;
      // 
      // okButton
      // 
      this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.okButton.Location = new System.Drawing.Point(469, 311);
      this.okButton.Name = "okButton";
      this.okButton.Size = new System.Drawing.Size(75, 23);
      this.okButton.TabIndex = 5;
      this.okButton.Text = "OK";
      this.okButton.UseVisualStyleBackColor = true;
      this.okButton.Click += new System.EventHandler(this.okButton_Click);
      // 
      // cancelButton
      // 
      this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cancelButton.Location = new System.Drawing.Point(550, 311);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new System.Drawing.Size(75, 23);
      this.cancelButton.TabIndex = 6;
      this.cancelButton.Text = "Cancel";
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
      // 
      // resetToDefaultLinkLabel
      // 
      this.resetToDefaultLinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.resetToDefaultLinkLabel.AutoSize = true;
      this.resetToDefaultLinkLabel.Location = new System.Drawing.Point(9, 316);
      this.resetToDefaultLinkLabel.Name = "resetToDefaultLinkLabel";
      this.resetToDefaultLinkLabel.Size = new System.Drawing.Size(82, 13);
      this.resetToDefaultLinkLabel.TabIndex = 4;
      this.resetToDefaultLinkLabel.TabStop = true;
      this.resetToDefaultLinkLabel.Text = "Reset to default";
      this.resetToDefaultLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.resetToDefaultLinkLabel_LinkClicked);
      // 
      // projectTypesLabel
      // 
      projectTypesLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      projectTypesLabel.AutoSize = true;
      projectTypesLabel.Location = new System.Drawing.Point(321, 9);
      projectTypesLabel.Name = "projectTypesLabel";
      projectTypesLabel.Size = new System.Drawing.Size(129, 13);
      projectTypesLabel.TabIndex = 2;
      projectTypesLabel.Text = "Enter &project types below:";
      // 
      // projectTypesTextBox
      // 
      this.projectTypesTextBox.AcceptsReturn = true;
      this.projectTypesTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.projectTypesTextBox.Location = new System.Drawing.Point(324, 25);
      this.projectTypesTextBox.Multiline = true;
      this.projectTypesTextBox.Name = "projectTypesTextBox";
      this.projectTypesTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.projectTypesTextBox.Size = new System.Drawing.Size(301, 280);
      this.projectTypesTextBox.TabIndex = 3;
      // 
      // FolderExclusionsDialog
      // 
      this.AcceptButton = this.okButton;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.cancelButton;
      this.ClientSize = new System.Drawing.Size(637, 346);
      this.Controls.Add(this.projectTypesTextBox);
      this.Controls.Add(projectTypesLabel);
      this.Controls.Add(this.resetToDefaultLinkLabel);
      this.Controls.Add(this.cancelButton);
      this.Controls.Add(this.okButton);
      this.Controls.Add(this.folderExclusionsTextBox);
      this.Controls.Add(folderExclusionsLabel);
      this.Name = "FolderExclusionsDialog";
      this.Text = "Folder Exclusions";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.TextBox folderExclusionsTextBox;
    private System.Windows.Forms.Button okButton;
    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.LinkLabel resetToDefaultLinkLabel;
        private System.Windows.Forms.TextBox projectTypesTextBox;
    }
}