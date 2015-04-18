using System;

namespace Cyotek
{
  [Flags]
  internal enum FileDialogFilterBuilderOptions
  {
    None = 0,

    AddAllFiles = 1,

    AddCombined = 2
  }
}
