using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace Cyotek.VisualStudioExtensions.AddProjects
{
  internal partial class AddProjectsDialog : BaseForm
  {
    #region Constants

    private readonly IVsSolution _currentSolution;
    private readonly EnvDTE80.DTE2 _dte80;

    #endregion

    #region Fields

    private FileDialogFilterBuilder _filter;

    private List<string> _loadedProjects;

    private ExtensionSettings _settings;

    private string _settingsFileName;

    #endregion

    #region Constructors

    public AddProjectsDialog()
    {
      this.InitializeComponent();
    }

    public AddProjectsDialog(IVsSolution solution, EnvDTE80.DTE2 dte80)
      : this()
    {
      _currentSolution = solution;
      _dte80 = dte80;
    }

    #endregion

    #region Methods

    protected override void OnLoad(EventArgs e)
    {
      ThreadHelper.ThrowIfNotOnUIThread();

      base.OnLoad(e);

      this.LoadSettings();
      this.PopulateLoadedProjectsList();
      this.UpdateFilteredList();

      if (projectsListView.Items.Count != 0)
      {
        projectsListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
      }
    }

    private void addFileButton_Click(object sender, EventArgs e)
    {
      this.EnsureFilterCreated();

      using (OpenFileDialog dialog = new OpenFileDialog
      {
        Filter = _filter.ToString(),
        DefaultExt = "csproj", // TODO: Calculate this based on the filter
        Title = "Add Project",
        Multiselect = true
      })
      {
        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
          this.AddFilesToMru(dialog.FileNames);
        }
      }
    }

    private void AddFilesToMru(string[] fileNames)
    {
      this.BeginAction();
      projectsListView.BeginUpdate();

      foreach (string fileName in fileNames)
      {
        this.AddProjectToMru(fileName);
      }

      projectsListView.EndUpdate();
      this.EndAction();
    }

    private void addFolderButton_Click(object sender, EventArgs e)
    {
      using (FindProjectsDialog dialog = new FindProjectsDialog(_settings))
      {
        if (dialog.ShowDialog() == DialogResult.OK)
        {
          this.AddFilesToMru(dialog.FileNames);
        }
      }
    }

    private ListViewItem AddProjectItem(string fileName, bool isChecked)
    {
      ListViewItem item;
      bool alreadyLoaded;

      alreadyLoaded = _loadedProjects.Contains(fileName);

      item = projectsListView.AddFile(fileName, isChecked);

      if (alreadyLoaded)
      {
        this.MarkProjectAsLoaded(item);
      }
      else if (!File.Exists(fileName))
      {
        this.MarkProjectAsNotFound(item);
      }
      else
      {
        item.Tag = false;
      }

      return item;
    }

    private void AddProjectToMru(string fileName)
    {
      if (!_settings.Projects.Contains(fileName, StringComparer.InvariantCultureIgnoreCase))
      {
        _settings.Projects.Add(fileName);
        this.AddProjectItem(fileName, true).EnsureVisible();
      }
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    private void EnsureFilterCreated()
    {
      if (_filter == null)
      {
        _filter = Utilities.GetProjectsFilter(_settings.ProjectTypes);
      }
    }

    private void filterTextBox_TextChanged(object sender, EventArgs e)
    {
      updateFilterTimer.Stop();
      updateFilterTimer.Start();
    }

    private IEnumerable<IVsProject> GetSolutionProjects()
    {
      IEnumHierarchies enumerator;
      Guid guid = Guid.Empty;
      IVsHierarchy[] hierarchy;
      uint fetched;

      ThreadHelper.ThrowIfNotOnUIThread();

      // http://stackoverflow.com/a/304376/148962

      _currentSolution.GetProjectEnum((uint)__VSENUMPROJFLAGS.EPF_LOADEDINSOLUTION, ref guid, out enumerator);

      hierarchy = new IVsHierarchy[]
                  {
                    null
                  };

      for (enumerator.Reset(); enumerator.Next(1, hierarchy, out fetched) == VSConstants.S_OK && fetched == 1;)
      {
        yield return (IVsProject)hierarchy[0];
      }
    }

    private void homeLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      try
      {
        System.Diagnostics.Process.Start("http://www.cyotek.com/");
      }
      catch (Exception ex)
      {
        Utilities.ShowExceptionMessage(ex);
      }
    }

    private void LoadSettings()
    {
      _settingsFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Cyotek\VisualStudioExtensions\AddProjects\config.xml");
      _settings = ExtensionSettings.Load(_settingsFileName);
    }

    private void MarkProjectAsLoaded(ListViewItem item)
    {
      item.ForeColor = SystemColors.GrayText;
      item.ToolTipText = "This project is already in the solution";
      item.Tag = true;
    }

    private void MarkProjectAsNotFound(ListViewItem item)
    {
      item.ForeColor = Color.Firebrick;
      item.ToolTipText = "The project file cannot be found";
    }

    private void okButton_Click(object sender, EventArgs e)
    {
      ThreadHelper.ThrowIfNotOnUIThread();

      try
      {
        StringBuilder errors;

        this.BeginAction();

        _settings.Save(_settingsFileName);

        errors = new StringBuilder();

        SolutionFoldersStructureCreator foldersCreator =
          new SolutionFoldersStructureCreator(_currentSolution, _dte80);

        foreach (ListViewItem item in projectsListView.CheckedItems)
        {
          string fileName;

          fileName = item.Name;

          if (!_loadedProjects.Contains(fileName))
          {
            if (!File.Exists(fileName))
            {
              errors.AppendLine($"Project file '{fileName}' not found, unable to add to solution.").AppendLine();
            }
            else
            {
              Guid projectType;
              Guid projectId;
              IntPtr project;
              int result = -1;

              projectType = Guid.Empty;
              projectId = Guid.Empty;

              try
              {
                Project existingProject = null;
                if (chkCreateSolutionFoldersStructure.Checked)
                {
                  (string parentDir, Project dteProject) = foldersCreator.CreateFoldersStructure(fileName);
                  if (dteProject != null)
                  {
                    SolutionFolder folder1 = (SolutionFolder)dteProject.Object;
                    existingProject = folder1.AddFromFile(fileName);
                  }
                }

                if (existingProject != null)
                {
                  result = VSConstants.S_OK;
                }
                else
                {
                  result = _currentSolution.CreateProject(ref projectType, fileName, null, null, (uint)(__VSCREATEPROJFLAGS.CPF_OPENFILE | __VSCREATEPROJFLAGS.CPF_SILENT), ref projectId, out project);
                }
              }
              catch (Exception ex)
              {
                //Utilities.ShowExceptionMessage(ex);
                errors.AppendLine($"Failed to add project: {fileName}");
                errors.AppendLine(ex.GetBaseException().Message).AppendLine();
              }


              if (result == VSConstants.S_OK)
              {
                _loadedProjects.Add(fileName);
                this.MarkProjectAsLoaded(item);
              }
            }
          }
        }

        this.EndAction();

        if (errors.Length != 0)
        {
          MessageBox.Show(errors.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        else
        {
          this.DialogResult = DialogResult.OK;
          this.Close();
        }
      }
      catch (Exception ex)
      {
        Utilities.ShowExceptionMessage(ex);
      }
    }

    private void PopulateLoadedProjectsList()
    {
      ThreadHelper.ThrowIfNotOnUIThread();

      _loadedProjects = new List<string>();

      foreach (IVsProject project in this.GetSolutionProjects())
      {
        string fileName;

        project.GetMkDocument((uint)VSConstants.VSITEMID.Root, out fileName);

        if (!string.IsNullOrEmpty(fileName))
        {
          _loadedProjects.Add(fileName);
        }
      }
    }

    private void projectsListView_ItemChecked(object sender, ItemCheckedEventArgs e)
    {
      if (e.Item.Checked)
      {
        bool projectAlreadyLoaded;

        projectAlreadyLoaded = e.Item.Tag != null && (bool)e.Item.Tag;

        if (projectAlreadyLoaded)
        {
          e.Item.Checked = false;
        }
      }
    }

    private void projectsListView_KeyUp(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Delete)
      {
        e.Handled = true;
        removeButton.PerformClick();
      }
    }

    private void projectsListView_SelectedIndexChanged(object sender, EventArgs e)
    {
      removeButton.Enabled = projectsListView.SelectedItems.Count != 0;
    }

    private void removeButton_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show("Are you sure you want to remove the selected projects from the list?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
      {
        List<string> names;

        names = projectsListView.SelectedItems.Cast<ListViewItem>().Select(i => i.Name).ToList();
        foreach (string name in names)
        {
          _settings.Projects.Remove(name);
          projectsListView.Items.RemoveByKey(name);
        }
      }
    }

    private void settingsButton_Click(object sender, EventArgs e)
    {
      if (Utilities.ShowSettingsDialog(this, _settings))
      {
        _filter = null;

        _settings.Save(_settingsFileName);
      }
    }

    private void UpdateFilteredList()
    {
      try
      {
        Regex expression;

        this.BeginAction();
        projectsListView.BeginUpdate();

        expression = !string.IsNullOrEmpty(filterTextBox.Text) ? new Regex(filterTextBox.Text, RegexOptions.IgnoreCase | RegexOptions.Singleline) : null;

        projectsListView.Items.Clear();

        // ReSharper disable once LoopCanBePartlyConvertedToQuery
        foreach (string fileName in _settings.Projects)
        {
          if (expression == null || expression.IsMatch(fileName))
          {
            this.AddProjectItem(fileName, false);
          }
        }

        projectsListView.Sort();

        projectsListView.EndUpdate();
        this.EndAction();
      }
      catch (Exception ex)
      {
        Utilities.ShowExceptionMessage(ex);
      }
    }

    private void updateFilterTimer_Tick(object sender, EventArgs e)
    {
      updateFilterTimer.Stop();

      this.UpdateFilteredList();
    }

    #endregion
  }
}
