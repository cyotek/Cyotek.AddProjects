using System;
using System.Drawing;
using System.Windows.Forms;

namespace Cyotek.VisualStudioExtensions.AddProjects
{
  public partial class BaseForm : Form
  {
    #region Constructors

    public BaseForm()
    {
      this.InitializeComponent();
    }

    #endregion

    #region Methods

    protected void BeginAction()
    {
      Cursor.Current = Cursors.WaitCursor;

      this.UseWaitCursor = true;
    }

    protected void EndAction()
    {
      Cursor.Current = Cursors.Default;

      this.UseWaitCursor = false;
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Form.Load"/> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);

      if (!this.DesignMode)
      {
        // TODO: Even though we're not using WPF, surely we can get the VS font settings at the very least?

        this.MinimumSize = this.Size;
        this.Font = SystemFonts.DialogFont;
      }
    }

    #endregion
  }
}
