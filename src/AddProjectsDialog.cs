using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;

namespace Cyotek.VisualStudioExtensions.AddProjects
{
  internal partial class AddProjectsDialog : BaseForm
  {
    #region Constants

    private readonly IVsSolution _currentSolution;

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

    public AddProjectsDialog(IVsSolution solution)
      : this()
    {
      _currentSolution = solution;
    }

    #endregion

    #region Methods

    protected override void OnLoad(EventArgs e)
    {
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
                                       Filter = _filter.ToString(), DefaultExt = "csproj", Title = "Add Project", Multiselect = true
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
#if DEBUG
      else if (!File.Exists(fileName))
      {
        this.MarkProjectAsNotFound(item);
      }
#endif
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
        this.AddProjectItem(fileName, true).
             EnsureVisible();
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
        _filter = Utilities.GetProjectsFilter();
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
        Process.Start("http://www.cyotek.com/");
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
      try
      {
        if (projectsListView.CheckedItems.Count == 0)
        {
          this.DialogResult = DialogResult.None;
          MessageBox.Show("Please select one or more projects to add to the current solution.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        else
        {
          StringBuilder errors;

          this.BeginAction();

          _settings.Save(_settingsFileName);

          errors = new StringBuilder();

          foreach (ListViewItem item in projectsListView.CheckedItems)
          {
            string fileName;

            fileName = item.Name;

            if (!_loadedProjects.Contains(fileName))
            {
              if (!File.Exists(fileName))
              {
                errors.AppendLine($"Project file '{fileName}' not found, unable to add to solution.").
                       AppendLine();
              }
              else
              {
                Guid projectType;
                Guid projectId;
                int result;
                IntPtr project;

                projectType = Guid.Empty;
                projectId = Guid.Empty;

                result = _currentSolution.CreateProject(ref projectType, fileName, null, null, (uint)(__VSCREATEPROJFLAGS.CPF_OPENFILE | __VSCREATEPROJFLAGS.CPF_SILENT), ref projectId, out project);

                if (result != VSConstants.S_OK)
                {
                  errors.AppendLine($"Failed to add project: {fileName}").
                         AppendLine();
                }
                else
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

        names = projectsListView.SelectedItems.Cast<ListViewItem>().
                                 Select(i => i.Name).
                                 ToList();
        foreach (string name in names)
        {
          _settings.Projects.Remove(name);
          projectsListView.Items.RemoveByKey(name);
        }
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
