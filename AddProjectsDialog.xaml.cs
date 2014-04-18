using System.Windows;
using Microsoft.VisualStudio.Shell.Interop;

namespace Cyotek.VisualStudioExtensions.AddProjects
{
  /// <summary>
  /// Interaction logic for AddProjectsDialog.xaml
  /// </summary>
  public partial class AddProjectsDialog
  {
    #region Constructors

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

    #region Properties

    protected IVsSolution CurrentSolution { get; set; }

    #endregion

    #region Event Handlers

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
      this.DialogResult = false;
      this.Close();
    }

    private void OkButton_Click(object sender, RoutedEventArgs e)
    {
      this.DialogResult = true;
      this.Close();
    }

    #endregion

    private void AddFileButton_Click(object sender, RoutedEventArgs e)
    {

    }

    private void AddFolderButton_Click(object sender, RoutedEventArgs e)
    {
      
    }
  }
}
