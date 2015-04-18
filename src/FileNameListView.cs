using System;
using System.IO;
using System.Windows.Forms;

namespace Cyotek.VisualStudioExtensions.AddProjects
{
  internal class FileNameListView : ListView
  {
    #region Fields

    private ListViewColumnSorter _listViewColumnSorter;

    #endregion

    #region Constructors

    public FileNameListView()
    {
      this.View = View.Details;
      this.FullRowSelect = true;
      this.ShowItemToolTips = true;
      this.InitializeSorting();
    }

    #endregion

    #region Methods

    public ListViewItem AddFile(string fileName, bool autoCheck)
    {
      ListViewItem item;

      item = this.CreateListViewItem(fileName);
      item.Checked = autoCheck;

      this.Items.Add(item);

      return item;
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.ListView.ColumnClick"/> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.ColumnClickEventArgs"/> that contains the event data. </param>
    protected override void OnColumnClick(ColumnClickEventArgs e)
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

      this.Sort();

      base.OnColumnClick(e);
    }

    /// <summary>
    /// Raises the <see cref="M:System.Windows.Forms.Control.CreateControl"/> method.
    /// </summary>
    protected override void OnCreateControl()
    {
      base.OnCreateControl();

      if (!this.DesignMode)
      {
        this.AddColumnHeaders();
      }
    }

    protected override void OnHandleCreated(EventArgs e)
    {
      base.OnHandleCreated(e);

      if (!this.DesignMode && Environment.OSVersion.Platform == PlatformID.Win32NT && Environment.OSVersion.Version.Major >= 6)
      {
        NativeMethods.SetWindowTheme(this.Handle, "explorer", null);
      }
    }

    private void AddColumnHeaders()
    {
      if (this.Columns.Count == 0)
      {
        this.Columns.Add(new ColumnHeader
                         {
                           Name = "nameColumnHeader",
                           Text = "Name",
                           Width = 120
                         });

        this.Columns.Add(new ColumnHeader
                         {
                           Name = "extensionColumnHeader",
                           Text = "Extension",
                           Width = 90
                         });

        this.Columns.Add(new ColumnHeader
                         {
                           Name = "pathColumnHeader",
                           Text = "Path",
                           Width = 360
                         });
      }
    }

    private ListViewItem CreateListViewItem(string fileName)
    {
      ListViewItem item;
      string extension;

      extension = Path.GetExtension(fileName);
      if (!string.IsNullOrEmpty(extension) && extension[0] == '.')
      {
        extension = extension.Remove(0, 1);
      }

      item = new ListViewItem();
      item.Name = fileName;
      item.Text = Path.GetFileName(fileName);
      item.SubItems.Add(extension);
      item.SubItems.Add(Path.GetDirectoryName(fileName));

      return item;
    }

    private void InitializeSorting()
    {
      _listViewColumnSorter = new ListViewColumnSorter
                              {
                                SortOrder = SortOrder.Ascending,
                                SortColumn = 0
                              };

      this.ListViewItemSorter = _listViewColumnSorter;
    }

    #endregion
  }
}
