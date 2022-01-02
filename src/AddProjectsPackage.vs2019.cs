using System;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Task = System.Threading.Tasks.Task;

namespace Cyotek.VisualStudioExtensions.AddProjects
{
  /// <summary>
  /// This is the class that implements the package exposed by this assembly.
  /// </summary>
  /// <remarks>
  /// <para>
  /// The minimum requirement for a class to be considered a valid package for Visual Studio
  /// is to implement the IVsPackage interface and register itself with the shell.
  /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
  /// to do it: it derives from the Package class that provides the implementation of the
  /// IVsPackage interface and uses the registration attributes defined in the framework to
  /// register itself and its components with the shell. These attributes tell the pkgdef creation
  /// utility what data to put into .pkgdef file.
  /// </para>
  /// <para>
  /// To get loaded into VS, the package must be referred by &lt;Asset Type="Microsoft.VisualStudio.VsPackage" ...&gt; in .vsixmanifest file.
  /// </para>
  /// </remarks>
  [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
  // This attribute is used to register the information needed to show this package
  // in the Help/About dialog of Visual Studio.
  [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
  // This attribute is needed to let the shell know that this package exposes some menus.
  [ProvideMenuResource("Menus.ctmenu", 1)]
  [Guid(GuidList.guidCyotek_AddProjectsPkgString)]
  public sealed class AddProjectsPackage : AsyncPackage
  {

    #region Package Members

    /// <summary>
    /// Initialization of the package; this method is called right after the package is sited, so this is the place
    /// where you can put all the initialization code that rely on services provided by VisualStudio.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to monitor for initialization cancellation, which can occur when VS is shutting down.</param>
    /// <param name="progress">A provider for progress updates.</param>
    /// <returns>A task representing the async work of package initialization, or an already completed task if there is none. Do not return null from this method.</returns>
    protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
    {
      // When initialized asynchronously, the current thread may be a background thread at this point.
      // Do any initialization that requires the UI thread after switching to the UI thread.
      await this.JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);

      OleMenuCommandService menuCommandService;

      // Add our command handlers for menu (commands must exist in the .vsct file)
      menuCommandService = await this.GetServiceAsync(typeof(IMenuCommandService)).ConfigureAwait(false) as OleMenuCommandService;
      if (menuCommandService != null)
      {
        // Create the command for the menu item.
        CommandID menuCommandId;
        MenuCommand menuItem;

        menuCommandId = new CommandID(GuidList.guidCyotek_AddProjectsCmdSet, (int)PkgCmdIDList.cmdidCyotekAddProjects);
        menuItem = new MenuCommand(this.MenuItemCallback, menuCommandId);

        menuCommandService.AddCommand(menuItem);
      }
    }

    /// <summary>
    /// This function is the callback used to execute a command when the a menu item is clicked.
    /// See the Initialize method to see how the menu item is associated to this function using
    /// the OleMenuCommandService service and the MenuCommand class.
    /// </summary>
    private void MenuItemCallback(object sender, EventArgs e)
    {
      IVsSolution solution;
      
      ThreadHelper.ThrowIfNotOnUIThread();

      solution = this.GetService(typeof(SVsSolution)) as IVsSolution;

      if (solution != null)
      {
        string solutionDirectory;
        string solutionFileName;
        string solutionOptions;

        solution.GetSolutionInfo(out solutionDirectory, out solutionFileName, out solutionOptions);

        if (!(string.IsNullOrEmpty(solutionDirectory) || string.IsNullOrEmpty(solutionFileName)))
        {
          using (AddProjectsDialog dialog = new AddProjectsDialog(solution))
          {
            dialog.ShowDialog();
          }
        }
        else
        {
          MessageBox.Show("This command can only be used with a saved solution.", "Add Projects", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
      }
      else
      {
        MessageBox.Show("This command can only be used with an open solution.", "Add Projects", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    #endregion
  }
}
