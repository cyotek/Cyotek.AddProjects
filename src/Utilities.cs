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

    internal static Regex CreateDefaultSearchPattern()
    {
      return CreateSearchPattern(GetProjectsFilter().ToString());
    }

    internal static Regex CreateSearchPattern(string filter)
    {
      List<string> masks;
      string[] filterParts;

      filterParts = filter.Split('|');
      masks = new List<string>();

      for (int i = 1; i < filterParts.Length; i += 2)
      {
        masks.AddRange(filterParts[i].Split(';').Where(mask => mask != "*.*" && !masks.Contains(mask, StringComparer.InvariantCultureIgnoreCase)));
      }

      return new Regex(string.Join("|", masks.Select(m => m.Replace(".", @"\.").Replace("*", ".*") + "$")));
    }

    internal static FileDialogFilterBuilder GetProjectsFilter()
    {
      FileDialogFilterBuilder builder;

      builder = new FileDialogFilterBuilder();
      builder.Add("C# Projects", "*.csproj");
      builder.Add("Visual Basic Projects", "*.vbproj");
      builder.Add("C++ Projects", "*.vcproj;*.vcxproj");
      builder.Add("F# Projects", "*.fsproj");
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
