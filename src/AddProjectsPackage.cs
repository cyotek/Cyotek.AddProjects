using System;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace Cyotek.VisualStudioExtensions.AddProjects
{
  /// <summary>
  /// This is the class that implements the package exposed by this assembly.
  ///
  /// The minimum requirement for a class to be considered a valid package for Visual Studio
  /// is to implement the IVsPackage interface and register itself with the shell.
  /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
  /// to do it: it derives from the Package class that provides the implementation of the 
  /// IVsPackage interface and uses the registration attributes defined in the framework to 
  /// register itself and its components with the shell.
  /// </summary>
  // This attribute tells the PkgDef creation utility (CreatePkgDef.exe) that this class is
  // a package.
  [PackageRegistration(UseManagedResourcesOnly = true)]
  // This attribute is used to register the information needed to show this package
  // in the Help/About dialog of Visual Studio.
  [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
  // This attribute is needed to let the shell know that this package exposes some menus.
  [ProvideMenuResource("Menus.ctmenu", 1)]
  [Guid(GuidList.guidCyotek_AddProjectsPkgString)]
  public sealed class AddProjectsPackage : Package
  {
    #region Methods

    /// <summary>
    /// Initialization of the package; this method is called right after the package is sited, so this is the place
    /// where you can put all the initialization code that rely on services provided by VisualStudio.
    /// </summary>
    protected override void Initialize()
    {
      OleMenuCommandService menuCommandService;

      base.Initialize();

      // Add our command handlers for menu (commands must exist in the .vsct file)
      menuCommandService = this.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
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
