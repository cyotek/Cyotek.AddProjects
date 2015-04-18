using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cyotek
{
  internal sealed class FileDialogFilterBuilder : IEnumerable<string>
  {
    #region Constants

    private readonly List<Tuple<string, string>> _groups;

    #endregion

    #region Constructors

    public FileDialogFilterBuilder()
    {
      _groups = new List<Tuple<string, string>>();
    }

    public FileDialogFilterBuilder(string filter)
      : this()
    {
      this.Add(filter);
    }

    #endregion

    #region Static Methods

    public static string Combine(params string[] filters)
    {
      return Combine(FileDialogFilterBuilderOptions.AddAllFiles | FileDialogFilterBuilderOptions.AddCombined, filters);
    }

    public static string Combine(FileDialogFilterBuilderOptions options, params string[] filters)
    {
      FileDialogFilterBuilder builder;

      builder = new FileDialogFilterBuilder();
      foreach (string filter in filters)
      {
        builder.Add(filter);
      }

      if ((options & FileDialogFilterBuilderOptions.AddAllFiles) != 0)
      {
        builder.AddAllFiles();
      }

      if ((options & FileDialogFilterBuilderOptions.AddCombined) != 0)
      {
        builder.AddCombined();
      }

      return builder.ToString();
    }

    private static string SanatizeExtension(string mask)
    {
      int position;

      position = mask.LastIndexOf(".", StringComparison.InvariantCultureIgnoreCase);
      if (position != -1)
      {
        mask = mask.Substring(position + 1);
      }

      return mask.Trim();
    }

    private static string SanatizeName(string name)
    {
      int position;

      position = name.IndexOf("(", StringComparison.InvariantCultureIgnoreCase);
      if (position != -1)
      {
        name = name.Substring(0, position - 1);
      }

      return name.Trim();
    }

    #endregion

    #region Properties

    public int Count
    {
      get { return _groups.Count; }
    }

    public string Filter
    {
      get
      {
        StringBuilder result;

        result = new StringBuilder();

        foreach (Tuple<string, string> pair in _groups)
        {
          result.Append(pair.Item1);
          result.Append("|");
          result.Append(pair.Item2);
          result.Append("|");
        }

        if (result.Length != 0)
        {
          result.Remove(result.Length - 1, 1);
        }

        return result.ToString();
      }
    }

    public string this[int index]
    {
      get
      {
        Tuple<string, string> item;

        item = _groups[index];

        return string.Concat(item.Item1, "|", item.Item2);
      }
    }

    #endregion

    #region Methods

    public void Add(string name, IEnumerable<string> extensions)
    {
      this.Add(name, extensions, true);
    }

    public void Add(string name, IEnumerable<string> extensions, bool expandText)
    {
      this.Insert(this.Count, name, extensions, expandText);
    }

    public void Add(string text, string masks)
    {
      this.Insert(this.Count, text, masks);
    }

    public void Add(string filter)
    {
      this.Insert(this.Count, filter);
    }

    public void AddAllFiles()
    {
      this.Add("All Files", "*.*");
    }

    public void AddCombined()
    {
      this.AddCombined("All Supported Files");
    }

    public void AddCombined(string text)
    {
      List<string> extensions;

      extensions = new List<string>();

      foreach (string extension in _groups.SelectMany(pair => pair.Item2.Split(new[]
                                                                               {
                                                                                 ';'
                                                                               }, StringSplitOptions.RemoveEmptyEntries).Select(SanatizeExtension).Where(extension => extension != "*" && !extensions.Contains(extension))))
      {
        extensions.Add(extension);
      }

      this.Insert(0, text, extensions.ToArray(), true);
    }

    public void Remove(string name)
    {
      name = SanatizeName(name);

      for (int i = _groups.Count; i > 0; i--)
      {
        string groupName;

        groupName = _groups[i - 1].Item1;
        if (groupName.StartsWith(name, StringComparison.InvariantCultureIgnoreCase))
        {
          _groups.RemoveAt(i - 1);
        }
      }
    }

    public override string ToString()
    {
      return this.Filter;
    }

    private void Insert(int index, string name, IEnumerable<string> extensions, bool expandText)
    {
      string text;
      StringBuilder masks;
      string mask;

      masks = new StringBuilder();

      foreach (string extension in extensions)
      {
        if (masks.Length != 0)
        {
          masks.Append(';');
        }

        masks.Append('*');

        if (extension[0] != '.')
        {
          masks.Append('.');
        }

        masks.Append(extension);
      }

      mask = masks.ToString();
      text = expandText ? string.Format("{0} ({1})", name, mask) : name;

      this.Insert(index, text, mask);
    }

    private void Insert(int index, string text, string masks)
    {
      this.Remove(text);

      if (index > this.Count)
      {
        index = this.Count;
      }

      _groups.Insert(index, new Tuple<string, string>(text, masks));
    }

    private void Insert(int index, string filter)
    {
      string[] parts;

      parts = filter.Split(new[]
                           {
                             '|'
                           }, StringSplitOptions.RemoveEmptyEntries);

      for (int i = 0; i < parts.Length; i += 2)
      {
        string text;
        string[] masks;
        string[] extensions;

        text = SanatizeName(parts[i]);
        masks = parts[i + 1].Split(new[]
                                   {
                                     ';'
                                   }, StringSplitOptions.RemoveEmptyEntries);
        extensions = masks.Select(SanatizeExtension).ToArray();

        this.Insert(index, text, extensions, true);
        index++;
      }
    }

    #endregion

    #region IEnumerable<string> Interface

    public IEnumerator<string> GetEnumerator()
    {
      for (int i = 0; i < this.Count; i++)
      {
        yield return this[i];
      }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return this.GetEnumerator();
    }

    #endregion
  }
}
