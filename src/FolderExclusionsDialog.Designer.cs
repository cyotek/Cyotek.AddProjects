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
            this.label1 = new System.Windows.Forms.Label();
            this.exclusionsTextBox = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.resetToDefaultLinkLabel = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.projectTypesTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(252, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "&Enter folder exclusions below, one exclusion per line";
            // 
            // exclusionsTextBox
            // 
            this.exclusionsTextBox.AcceptsReturn = true;
            this.exclusionsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.exclusionsTextBox.Location = new System.Drawing.Point(12, 25);
            this.exclusionsTextBox.Multiline = true;
            this.exclusionsTextBox.Name = "exclusionsTextBox";
            this.exclusionsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.exclusionsTextBox.Size = new System.Drawing.Size(284, 284);
            this.exclusionsTextBox.TabIndex = 1;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Location = new System.Drawing.Point(469, 311);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 2;
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
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // resetToDefaultLinkLabel
            // 
            this.resetToDefaultLinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.resetToDefaultLinkLabel.AutoSize = true;
            this.resetToDefaultLinkLabel.Location = new System.Drawing.Point(12, 316);
            this.resetToDefaultLinkLabel.Name = "resetToDefaultLinkLabel";
            this.resetToDefaultLinkLabel.Size = new System.Drawing.Size(162, 13);
            this.resetToDefaultLinkLabel.TabIndex = 6;
            this.resetToDefaultLinkLabel.TabStop = true;
            this.resetToDefaultLinkLabel.Text = "Reset excluded folders to default";
            this.resetToDefaultLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.resetToDefaultLinkLabel_LinkClicked);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(321, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "&Enter project types below:";
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
            this.projectTypesTextBox.Size = new System.Drawing.Size(301, 284);
            this.projectTypesTextBox.TabIndex = 8;
            // 
            // FolderExclusionsDialog
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(637, 346);
            this.Controls.Add(this.projectTypesTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.resetToDefaultLinkLabel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.exclusionsTextBox);
            this.Controls.Add(this.label1);
            this.Name = "FolderExclusionsDialog";
            this.Text = "Folder Exclusions";
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox exclusionsTextBox;
    private System.Windows.Forms.Button okButton;
    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.LinkLabel resetToDefaultLinkLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox projectTypesTextBox;
    }
}