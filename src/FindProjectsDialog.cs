using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Cyotek.VisualStudioExtensions.AddProjects
{
  public partial class FindProjectsDialog : BaseForm
  {
    #region Constants

    private readonly string[] _existingFiles;

    #endregion

    #region Fields

    private Regex _searchPattern;

    #endregion

    #region Constructors

    public FindProjectsDialog()
    {
      this.InitializeComponent();
    }

    public FindProjectsDialog(string[] existingFiles)
      : this()
    {
      _existingFiles = existingFiles;
    }

    #endregion

    public string[] FileNames
    {
      get
      {
        List<string> results;

        results = new List<string>();

        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (ListViewItem item in projectsListView.CheckedItems)
        {
          results.Add(item.Name);
        }

        return results.ToArray();
      }
    }

    #region Methods

    private void browseButton_Click(object sender, EventArgs e)
    {
      using (FolderBrowserDialog dialog = new FolderBrowserDialog
                                          {
                                            ShowNewFolderButton = true,
                                            Description = "Select the &folder to scan for projects:",
                                            SelectedPath = folderTextBox.Text
                                          })
      {
        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
          folderTextBox.Text = dialog.SelectedPath;
          this.SearchProjects();
        }
      }
    }

    #endregion

    private void SearchProjects()
    {
      try
      {
        string path;

        _searchPathChanged = false;

        path = folderTextBox.Text;

        if (!string.IsNullOrWhiteSpace(path) && path.Length > 3 && Directory.Exists(path))
        {
          if (_searchPattern == null)
          {
            _searchPattern = Utilities.CreateDefaultSearchPattern();
          }

          if (path[path.Length - 1] != Path.DirectorySeparatorChar && path[path.Length - 1] != Path.AltDirectorySeparatorChar)
          {
            path = string.Concat(path, Path.DirectorySeparatorChar.ToString());
          }

          this.BeginAction();

          projectsListView.BeginUpdate();
          projectsListView.Items.Clear();

          // ReSharper disable once LoopCanBePartlyConvertedToQuery
          foreach (string fileName in Directory.GetFiles(path, "*.*", SearchOption.AllDirectories))
          {
            if (_searchPattern.IsMatch(fileName) && !_existingFiles.Any(f => string.Equals(f, fileName, StringComparison.InvariantCultureIgnoreCase)))
            {
              projectsListView.AddFile(fileName, true);
            }
          }

          projectsListView.EndUpdate();

          this.EndAction();
        }
      }
      catch (Exception ex)
      {
        Utilities.ShowExceptionMessage(ex);
      }
    }

    private void folderTextBox_Leave(object sender, EventArgs e)
    {
      if (_searchPathChanged)
      {
        this.SearchProjects();
      }
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    private void okButton_Click(object sender, EventArgs e)
    {
      if (projectsListView.CheckedItems.Count != 0)
      {
        this.DialogResult = DialogResult.OK;
        this.Close();
      }
      else
      {
        this.DialogResult = DialogResult.None;
        MessageBox.Show("Please select the projects to add.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
      }
    }

    private bool _searchPathChanged;

    private void folderTextBox_TextChanged(object sender, EventArgs e)
    {
      _searchPathChanged = true;
    }
  }
}
