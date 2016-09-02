using System;
using System.IO;
using System.Xml.Serialization;

namespace Cyotek.VisualStudioExtensions.AddProjects
{
  [Serializable]
  public class ExtensionSettings
  {
      private ExtensionSettingsProjectCollection _projectTypes;

      public static ExtensionSettingsProjectCollection DefaultProjectTypes = new ExtensionSettingsProjectCollection
      {
          "C# Projects|*.csproj",
          "Visual Basic Projects|*.vbproj",
          "C++ Projects|*.vcproj;*.vcxproj",
          "F# Projects|*.fsproj",
          "NuGet Packager Projects|*.nuproj"
      };

    #region Static Constructors

    static ExtensionSettings()
    {
      Serializer = new XmlSerializer(typeof(ExtensionSettings));
    }

    #endregion

    #region Constructors

    public ExtensionSettings()
    {
      this.Projects = new ExtensionSettingsProjectCollection();
      this.ExcludedFolders = new ExtensionSettingsProjectCollection();
    }

    #endregion

    #region Static Properties

    private static XmlSerializer Serializer { get; set; }

    #endregion

    #region Static Methods

    public static ExtensionSettings Load(string fileName)
    {
      ExtensionSettings settings;

      if (string.IsNullOrEmpty(fileName))
      {
        throw new ArgumentNullException(nameof(fileName));
      }

      settings = new ExtensionSettings();

      if (File.Exists(fileName))
      {
        try
        {
          using (FileStream stream = File.OpenRead(fileName))
          {
            settings = (ExtensionSettings)Serializer.Deserialize(stream);
          }
        }
        catch
        {
          // ignore exceptions
        }
      }

      return settings;
    }

    #endregion

    #region Properties

    public ExtensionSettingsProjectCollection ExcludedFolders { get; set; }

    public ExtensionSettingsProjectCollection ProjectTypes
    {
          get { return _projectTypes ?? (_projectTypes = DefaultProjectTypes); }
          set { _projectTypes = value; }
    }

    public ExtensionSettingsProjectCollection Projects { get; set; }

    #endregion

    #region Methods

    public void Save(string fileName)
    {
      if (string.IsNullOrEmpty(fileName))
      {
        throw new ArgumentNullException(nameof(fileName));
      }

      Directory.CreateDirectory(Path.GetDirectoryName(fileName));

      using (Stream stream = File.Create(fileName))
      {
        Serializer.Serialize(stream, this);
      }
    }

    #endregion
  }
}
