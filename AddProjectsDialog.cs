using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Cyotek.Windows.Forms;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;

namespace Cyotek.VisualStudioExtensions.AddProjects
{
  internal partial class AddProjectsDialog : Form
  {
    #region Instance Fields

    private new ListViewColumnSorter _listViewColumnSorter;

    #endregion

    #region Public Constructors

    public AddProjectsDialog()
    {
      this.InitializeComponent();
    }

    public AddProjectsDialog(IVsSolution solution)
      : this()
    {
      this.CurrentSolution = solution;
    }

    #endregion

    #region Private Class Properties

    private IEnumerable<IVsProject> SolutionProjects
    {
      get
      {
        IEnumHierarchies enumerator;
        Guid guid = Guid.Empty;
        IVsHierarchy[] hierarchy;
        uint fetched;

        // http://stackoverflow.com/a/304376/148962

        this.CurrentSolution.GetProjectEnum((uint)__VSENUMPROJFLAGS.EPF_LOADEDINSOLUTION, ref guid, out enumerator);
        hierarchy = new IVsHierarchy[]
                    {
                      null
                    };

        for (enumerator.Reset(); enumerator.Next(1, hierarchy, out fetched) == VSConstants.S_OK && fetched == 1;)
        {
          yield return (IVsProject)hierarchy[0];
        }
      }
    }

    #endregion

    #region Overridden Methods

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);

      _listViewColumnSorter = new ListViewColumnSorter
      {
        SortOrder = SortOrder.Ascending
      };
      projectsListView.ListViewItemSorter = _listViewColumnSorter;

      this.MinimumSize = this.Size;
      this.Font = SystemFonts.DialogFont;

      this.SettingsFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Cyotek\VisualStudioExtensions\AddProjects\config.xml");
      this.Settings = ExtensionSettings.Load(this.SettingsFileName);

      this.LoadedProjects = new List<string>();
      foreach (IVsProject project in this.SolutionProjects)
      {
        string fileName;

        project.GetMkDocument((uint)VSConstants.VSITEMID.Root, out fileName);
        if (!string.IsNullOrEmpty(fileName))
        {
          this.LoadedProjects.Add(fileName);
        }
      }

      this.UpdateFilteredList();

      if (projectsListView.Items.Count != 0)
      {
        projectsListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
      }
    }

    #endregion

    #region Protected Properties

    protected IVsSolution CurrentSolution { get; set; }

    protected List<string> LoadedProjects { get; set; }

    protected Regex SearchPattern { get; set; }

    protected ExtensionSettings Settings { get; set; }

    protected string SettingsFileName { get; set; }

    #endregion

    #region Private Members

    private void AddProjectItem(string fileName, bool isChecked)
    {
      ListViewItem item;

      item = new ListViewItem();
      item.Name = fileName;
      item.Text = Path.GetFileName(fileName);
      item.SubItems.Add(Path.GetDirectoryName(fileName));
      item.Checked = isChecked;

      projectsListView.Items.Add(item);
    }

    private void AddProjectToMru(string fileName)
    {
      if (!this.Settings.Projects.Contains(fileName, StringComparer.InvariantCultureIgnoreCase))
      {
        this.Settings.Projects.Add(fileName);
        this.AddProjectItem(fileName, true);
      }
    }

    private void BeginAction()
    {
      Cursor.Current = Cursors.WaitCursor;
    }

    private Regex CreateSearchPattern(string filter)
    {
      List<string> masks;
      string[] filterParts;

      filterParts = filter.Split(new[]
                                 {
                                   '|'
                                 });
      masks = new List<string>();

      for (int i = 1; i < filterParts.Length; i += 2)
      {
        masks.AddRange(filterParts[i].Split(new[]
                                            {
                                              ';'
                                            }).Where(mask => mask != "*.*" && !masks.Contains(mask, StringComparer.InvariantCultureIgnoreCase)));
      }

      return new Regex(string.Join("|", masks.Select(m => m.Replace(".", @"\.").Replace("*", ".*") + "$")));
    }

    private void EndAction()
    {
      Cursor.Current = Cursors.Default;
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

        foreach (string fileName in this.Settings.Projects.Where(fileName => expression == null || expression.IsMatch(fileName)))
        {
          this.AddProjectItem(fileName, false);
        }

        projectsListView.Sort();

        projectsListView.EndUpdate();
        this.EndAction();
      }
      catch (Exception ex)
      {
        MessageBox.Show(string.Format("Failed to populate projects list. {0}", ex.GetBaseException().Message), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    #endregion

    #region Event Handlers

    private void addFileButton_Click(object sender, EventArgs e)
    {
      using (OpenFileDialog dialog = new OpenFileDialog
                                     {
                                       Filter = this.Settings.Filter,
                                       DefaultExt = "csproj",
                                       Title = "Add Project",
                                       Multiselect = true
                                     })
      {
        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
          this.BeginAction();
          projectsListView.BeginUpdate();

          foreach (string fileName in dialog.FileNames)
          {
            this.AddProjectToMru(fileName);
          }

          projectsListView.EndUpdate();
          this.EndAction();
        }
      }
    }

    private void addFolderButton_Click(object sender, EventArgs e)
    {
      using (FolderBrowserDialog dialog = new FolderBrowserDialog
                                          {
                                            ShowNewFolderButton = true,
                                            Description = "Select the &folder to scan for projects:"
                                          })
      {
        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
          if (this.SearchPattern == null)
          {
            this.SearchPattern = this.CreateSearchPattern(this.Settings.Filter);
          }

          this.BeginAction();
          projectsListView.BeginUpdate();

          foreach (string fileName in Directory.GetFiles(dialog.SelectedPath, "*.*", SearchOption.AllDirectories).Where(fileName => this.SearchPattern.IsMatch(fileName)))
          {
            this.AddProjectToMru(fileName);
          }

          projectsListView.EndUpdate();
          this.EndAction();
        }
      }
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    private void filterTextBox_TextChanged(object sender, EventArgs e)
    {
      updateFilterTimer.Stop();
      updateFilterTimer.Start();
    }

    private void homeLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      try
      {
        Process.Start("http://www.cyotek.com/");
      }
      catch (Win32Exception ex)
      {
        MessageBox.Show(string.Format("Failed to open URI. {0}", ex.GetBaseException().Message), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
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
          this.Settings.Save(this.SettingsFileName);

          errors = new StringBuilder();

          foreach (ListViewItem item in projectsListView.CheckedItems)
          {
            string fileName;

            fileName = item.Name;

            if (!this.LoadedProjects.Contains(fileName))
            {
              Guid projectType;
              Guid projectId;
              int result;
              IntPtr project;

              projectType = Guid.Empty;
              projectId = Guid.Empty;

              result = this.CurrentSolution.CreateProject(ref projectType, fileName, null, null, (uint)(__VSCREATEPROJFLAGS.CPF_OPENFILE | __VSCREATEPROJFLAGS.CPF_SILENT), ref projectId, out project);
              if (result != VSConstants.S_OK)
              {
                errors.AppendFormat("Failed to add project: {0}\n", fileName);
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
        // slightly evil exception handler but crashes were getting silently ignored
        MessageBox.Show(ex.GetBaseException().Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void projectsListView_ColumnClick(object sender, ColumnClickEventArgs e)
    {
      if (e.Column == _listViewColumnSorter.SortColumn)
      {
        _listViewColumnSorter.SortOrder = _listViewColumnSorter.SortOrder == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
      }
      else
      {
        _listViewColumnSorter.SortColumn = e.Column;
        _listViewColumnSorter.SortOrder = SortOrder.Ascending;
      }

      projectsListView.Sort();
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
          this.Settings.Projects.Remove(name);
          projectsListView.Items.RemoveByKey(name);
        }
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
