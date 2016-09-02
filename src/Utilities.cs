using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Cyotek.VisualStudioExtensions.AddProjects
{
  internal static class Utilities
  {
    #region Static Methods

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

    internal static FileDialogFilterBuilder GetProjectsFilter(ExtensionSettingsProjectCollection projectTypes)
    {
      FileDialogFilterBuilder builder;

      builder = new FileDialogFilterBuilder();
        foreach (var projectType in projectTypes)
        {
            var data = projectType.Split(new [] { '|'}, 1);
            var projectName = data[0];
            var projectFilter = data.Length > 1
                ? data[1]
                : projectName;
            builder.Add(projectName, projectFilter);
        }
      builder.AddAllFiles();

      return builder;
    }

    internal static void ShowExceptionMessage(Exception ex)
    {
      MessageBox.Show(ex.GetBaseException().Message, "Add Projects Extension", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    #endregion
  }
}
