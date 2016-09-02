using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Cyotek.VisualStudioExtensions.AddProjects
{
  internal static class Utilities
  {
    #region Static Methods

    public static bool ShowSettingsDialog(IWin32Window owner, ExtensionSettings settings)
    {
      bool result;

      using (FolderExclusionsDialog dialog = new FolderExclusionsDialog(settings.ExcludedFolders, settings.ProjectTypes))
      {
        result = dialog.ShowDialog(owner) == DialogResult.OK;

        if (result)
        {
          // update the settings
          // TODO: note they currently won't be saved as we don't have access to the filename right now
          settings.ExcludedFolders.Clear();
          settings.ExcludedFolders.AddRange(dialog.ExcludedFolders);

          settings.ProjectTypes.Clear();
          settings.ProjectTypes.AddRange(dialog.ProjectTypes);
        }
      }

      return result;
    }

    internal static FileDialogFilterBuilder GetProjectsFilter(ExtensionSettingsProjectCollection projectTypes)
    {
      FileDialogFilterBuilder builder;

      builder = new FileDialogFilterBuilder();

      foreach (string projectType in projectTypes)
      {
        int endOfNamePosition;
        string projectName;
        string projectFilter;

        endOfNamePosition = projectType.IndexOf('|');
        if (endOfNamePosition == -1)
        {
          projectName = projectType;
          projectFilter = projectType;
        }
        else
        {
          projectName = projectType.Substring(0, endOfNamePosition);
          projectFilter = projectType.Substring(endOfNamePosition + 1);
        }

        builder.Add(projectName, projectFilter);
      }

      builder.AddAllFiles();

      return builder;
    }

    internal static string[] GetSearchMasks(ExtensionSettingsProjectCollection projectTypes)
    {
      return GetSearchMasks(GetProjectsFilter(projectTypes).ToString());
    }

    internal static string[] GetSearchMasks(string filter)
    {
      List<string> masks;
      string[] filterParts;

      filterParts = filter.Split('|');
      masks = new List<string>();

      for (int i = 1; i < filterParts.Length; i += 2)
      {
        masks.AddRange(filterParts[i].Split(';').Where(mask => mask != "*.*" && !masks.Contains(mask, StringComparer.InvariantCultureIgnoreCase)));
      }

      return masks.ToArray();
    }

    internal static void ShowExceptionMessage(Exception ex)
    {
      MessageBox.Show(ex.GetBaseException().Message, "Add Projects Extension", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    #endregion
  }
}
