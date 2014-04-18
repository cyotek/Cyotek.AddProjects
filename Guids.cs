// Guids.cs
// MUST match guids.h

using System;

namespace Cyotek.VisualStudioExtensions.AddProjects
{
  internal static class GuidList
  {
    #region Constants

    public const string guidCyotek_AddProjectsPkgString = "af385d18-dd14-4c1b-a4ad-3e69c525312f";

    public const string guidCyotek_AddProjectsCmdSetString = "e9ea7dba-9b3b-431c-9fe8-6f61e4196e35";

    public static readonly Guid guidCyotek_AddProjectsCmdSet = new Guid(guidCyotek_AddProjectsCmdSetString);

    #endregion
  };
}
