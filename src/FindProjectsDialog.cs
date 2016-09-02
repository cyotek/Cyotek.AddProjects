using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cyotek.VisualStudioExtensions.AddProjects
{
  public partial class FindProjectsDialog : BaseForm
  {
    #region Constants

    private readonly ExtensionSettings _settings;

    #endregion

    #region Fields

    private string[] _searchMasks;

    private bool _searchPathChanged;

    #endregion

    #region Constructors

    public FindProjectsDialog()
    {
      this.InitializeComponent();
    }

    public FindProjectsDialog(ExtensionSettings settings)
      : this()
    {
      _settings = settings;
    }

    #endregion

    #region Properties

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

    #endregion

    #region Methods

    private void browseButton_Click(object sender, EventArgs e)
    {
      using (FolderBrowserDialog dialog = new FolderBrowserDialog
                                          {
                                            ShowNewFolderButton = true, Description = "Select the &folder to scan for projects:", SelectedPath = folderTextBox.Text
                                          })
      {
        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
          folderTextBox.Text = dialog.SelectedPath;
          this.SearchProjects();
        }
      }
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    private void excludeFoldersLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      using (FolderExclusionsDialog dialog = new FolderExclusionsDialog(_settings.ExcludedFolders, _settings.ProjectTypes))
      {
        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
          // update the settings
          // note they currently won't be saved as we don't have access to the filename right now
          _settings.ExcludedFolders.Clear();
          _settings.ExcludedFolders.AddRange(dialog.ExcludedFolders);

          _settings.ProjectTypes.Clear();
          _settings.ProjectTypes.AddRange(dialog.ProjectTypes);          

          // re-apply the search so we can exclude anything previously picked up
          this.SearchProjects();
        }
      }
    }

    private void folderTextBox_Leave(object sender, EventArgs e)
    {
      if (_searchPathChanged)
      {
        this.SearchProjects();
      }
    }

    private void folderTextBox_TextChanged(object sender, EventArgs e)
    {
      _searchPathChanged = true;
    }

    private bool IsExcluded(string fileName)
    {
      bool excluded;

      excluded = false;

      // ReSharper disable once LoopCanBeConvertedToQuery
      foreach (string pattern in _settings.ExcludedFolders)
      {
        // TODO: Can't be hassled with regex's right now, so it's just a contains match
        if (fileName.IndexOf(pattern, StringComparison.OrdinalIgnoreCase) != -1)
        {
          excluded = true;
          break;
        }
      }

      return excluded;
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

    private void SearchProjects()
    {
      try
      {
        string path;

        _searchPathChanged = false;

        path = folderTextBox.Text;

        if (!string.IsNullOrWhiteSpace(path) && path.Length > 3 && Directory.Exists(path))
        {
          HashSet<string> matchingfiles;

          matchingfiles = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

          if (_searchMasks == null)
          {
            _searchMasks = Utilities.GetSearchMasks(_settings.ProjectTypes);
          }

          if (path[path.Length - 1] != Path.DirectorySeparatorChar && path[path.Length - 1] != Path.AltDirectorySeparatorChar)
          {
            path = string.Concat(path, Path.DirectorySeparatorChar.ToString());
          }

          this.BeginAction();

          Parallel.ForEach(_searchMasks, mask =>
                                         {
                                           // ReSharper disable once LoopCanBePartlyConvertedToQuery
                                           foreach (string fileName in Directory.EnumerateFiles(path, mask, SearchOption.AllDirectories))
                                           {
                                             if (!matchingfiles.Contains(fileName) && !_settings.Projects.Contains(fileName) && !this.IsExcluded(fileName))
                                             {
                                               matchingfiles.Add(fileName);
                                             }
                                           }
                                         });

          projectsListView.BeginUpdate();
          projectsListView.Items.Clear();

          foreach (string fileName in matchingfiles)
          {
            projectsListView.AddFile(fileName, true);
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

    #endregion
  }
}
