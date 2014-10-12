using System;
using System.Collections;
using System.Windows.Forms;

namespace Cyotek.Windows.Forms
{
  internal sealed class ListViewColumnSorter : IComparer
  {
    #region Instance Fields

    private readonly IComparer _comparer;

    #endregion

    #region Public Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ListViewColumnSorter"/> class.
    /// </summary>
    public ListViewColumnSorter()
      : this(StringComparer.InvariantCultureIgnoreCase /* StringLogicalComparer.Default */)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ListViewColumnSorter"/> class.
    /// </summary>
    /// <param name="comparer">The comparer.</param>
    /// <exception cref="System.ArgumentNullException">comparer</exception>
    public ListViewColumnSorter(IComparer comparer)
    {
      if (comparer == null)
      {
        throw new ArgumentNullException("comparer");
      }

      _comparer = comparer;
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets or sets the number of the column to which to apply the sorting operation (Defaults to '0').
    /// </summary>
    public int SortColumn { get; set; }

    /// <summary>
    /// Gets or sets the order of sorting to apply (for example, 'Ascending' or 'Descending').
    /// </summary>
    public SortOrder SortOrder { get; set; }

    #endregion

    #region IComparer Members

    /// <summary>
    /// This method is inherited from the IComparer interface.  It compares the two objects passed using a case insensitive comparison.
    /// </summary>
    /// <param name="x">First object to be compared</param>
    /// <param name="y">Second object to be compared</param>
    /// <returns>The result of the comparison. "0" if equal, negative if 'x' is less than 'y' and positive if 'x' is greater than 'y'</returns>
    int IComparer.Compare(object x, object y)
    {
      int compareResult;
      ListViewItem listviewX;
      ListViewItem listviewY;
      ListViewItem.ListViewSubItem subItemX;
      ListViewItem.ListViewSubItem subItemY;
      string textX;
      string textY;

      // Cast the objects to be compared to ListViewItem objects
      listviewX = (ListViewItem)x;
      listviewY = (ListViewItem)y;
      /*
      subItemX = this.SortColumn.Between(0, listviewX.SubItems.Count - 1) ? listviewX.SubItems[this.SortColumn] : null;
      subItemY = this.SortColumn.Between(0, listviewY.SubItems.Count - 1) ? listviewY.SubItems[this.SortColumn] : null;
       */
      subItemX = this.SortColumn >= 0 && this.SortColumn < listviewX.SubItems.Count ? listviewX.SubItems[this.SortColumn] : null;
      subItemY = this.SortColumn >= 0 && this.SortColumn < listviewY.SubItems.Count ? listviewY.SubItems[this.SortColumn] : null;
      textX = subItemX != null ? subItemX.Text : string.Empty;
      textY = subItemY != null ? subItemY.Text : string.Empty;

      // Compare the two items
      compareResult = _comparer.Compare(textX, textY);

      // Calculate correct return value based on object comparison
      if (this.SortOrder == SortOrder.Descending)
      {
        // Descending sort is selected, return negative result of compare operation
        compareResult = -compareResult;
      }

      return compareResult;
    }

    #endregion
  }
}
