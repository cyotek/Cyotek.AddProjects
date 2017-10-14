using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Cyotek.VisualStudioExtensions.AddProjects
{
  [Serializable]
  public class ExtensionSettingsProjectCollection : Collection<string>
  {
    #region Methods

    public void AddRange(IEnumerable<string> items)
    {
      foreach (string item in items)
      {
        this.Add(item);
      }
    }

    #endregion
  }
}
