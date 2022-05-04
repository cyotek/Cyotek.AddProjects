using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyotek.VisualStudioExtensions.AddProjects
{
  public class SolutionFoldersStructureCreator
  {
    private readonly IVsSolution _currentSolution;
    private readonly DTE2 _dte80;

    public SolutionFoldersStructureCreator(IVsSolution solution, EnvDTE80.DTE2 dte80)
    {
      _currentSolution = solution;
      _dte80 = dte80;
    }

    public (string parentDir, Project project) CreateFoldersStructure(string fullFilePath)
    {
      string parentDir = null;
      Project project = null;

      ThreadHelper.ThrowIfNotOnUIThread();

      string solutionDir = System.IO.Path.GetDirectoryName(_dte80.Solution.FullName);
      string targetDir = System.IO.Path.GetDirectoryName(fullFilePath);

      int ndx = targetDir.IndexOf(solutionDir, StringComparison.OrdinalIgnoreCase);
      if (ndx == 0)
        targetDir = targetDir.Remove(0, solutionDir.Length);

      if (targetDir.Length == 0)
        return (parentDir, project);


      string[] dirParts = targetDir.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);

      // the last item is the folder which contains the project itself
      dirParts[dirParts.Length - 1] = string.Empty;

      // skip last item
      for (int cnt = 0; cnt < dirParts.Length - 1; cnt++)
      {
        if (dirParts[cnt].Contains(':'))
        {
          dirParts[cnt] = null;
          continue;
        }

        if (dirParts[cnt] != null)
          dirParts[cnt] = dirParts[cnt].Trim();

        Project foundItem = FindProjectItem(dirParts[cnt]);
        if (foundItem != null)
          break;
      }

      // get all folders after last null 
      List<string> dirPartsCopy = new List<string>(dirParts.Length);
      for (int cnt = dirParts.Length - 1; cnt >= 0; cnt--)
      {
        if (dirParts[cnt] == null)
          break;
        else if (dirParts[cnt].Length > 0)
          dirPartsCopy.Add(dirParts[cnt]);
      }

      // we have some folders to be created
      if (dirPartsCopy.Count > 0)
      {
        dirPartsCopy = ((IEnumerable<string>)dirPartsCopy).Reverse().ToList();
        Project solutionFolder = null;
        Solution2 solution = (Solution2)_dte80.Solution;

        foreach (string currentDir in dirPartsCopy)
        {
          if (solutionFolder == null)
          {
            solutionFolder = FindProjectItem(currentDir);
            if (solutionFolder == null)
              solutionFolder = solution.AddSolutionFolder(currentDir);
          }
          else
          {
            Project tmp1 = FindProjectItem(currentDir, solutionFolder);
            if (tmp1  != null)
              solutionFolder = tmp1;
            else
              solutionFolder = ((SolutionFolder)solutionFolder.Object).AddSolutionFolder(currentDir);
          }
          //ProjectList.Add(solutionFolder);
          parentDir = currentDir;
          project = solutionFolder;
        }
      }
      else
      {
        parentDir = dirParts.Last();
        project = FindProjectItem(parentDir);
      }

      return (parentDir, project);
    }

    //List<Project> ProjectList;
    private Project FindProjectItem(string currentDir, Project parentFolder = null)
    {
      Project foundItem = null;
      ThreadHelper.ThrowIfNotOnUIThread();

      if (string.IsNullOrWhiteSpace(currentDir))
      {
        return null;
      }

      Projects projects = null;

      if (parentFolder != null)
      {        
        foreach (ProjectItem item in parentFolder.ProjectItems)
          if (item.Name == currentDir)
          {
            foundItem = item.SubProject;
            break;
          }

      }
      else
      {
        Solution2 solution = (Solution2)_dte80.Solution;
        projects = solution.Projects;

        foreach (Project item in projects)
          if (item.Name == currentDir)
          {
            foundItem = item;
            break;
          }

      }

      return foundItem;
    }

  }
}
