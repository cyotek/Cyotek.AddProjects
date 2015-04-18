using System;
using System.Runtime.InteropServices;

namespace Cyotek.VisualStudioExtensions.AddProjects
{
  internal static class NativeMethods
  {
    #region Externals

    [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
    internal static extern int SetWindowTheme(IntPtr hWnd, string pszSubAppName, string pszSubIdList);

    #endregion
  }
}
