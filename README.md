# Cyotek Add Existing Projects Visual Studio Extension

## Source and Support

[Source][source] | [Issues][issues]

## Background

My solutions used have lots of references to other projects as I
would load common code as source code libraries. When creating a
new solution (or retro-fitting an existing solution to use new
libraries), I would end up using **File | Add | Existing
project** _a lot_. As I was curious about how extensions in
Visual Studio worked, I decided to write a very simple one to
ease the grunt work of adding multiple common projects.

This simple extension, originally created for Visual Studio 2012
as I recall, is the result. I'm afraid it doesn't use WPF, so
it's a touch ugly but it gets the job done.

Note that in the intervening years I finally converted almost
100 common library projects into proper NuGet packages and so I
haven't actually needed this extension for some time. As some
people find it useful I tend to create a new version for each
new version of Visual Studio but that's about it at the moment -
the extension isn't really actively developed.

## Using the extension

![Accessing the extension][menuscreen]

To use the extension, open the **Tools** menu and choose **Add
Projects**. This will open a lovely unthemed Windows Forms
dialogue containing a list of projects that can be automatically
added.

Projects are colour coded as follows

* Black - no particular status
* Grey - the project is already part of the active solution
* Red - the source project cannot be found

Note that the first time you open this dialogue, the list will be
empty - the MRU must be populated by yourself.

![The main project window][mainscreen]

Check each project you want to add to your solution, then click
the **OK** button. It will then try and add each selected
project to your solution, skipping any that are already present.

You can limit the projects displayed in the list by entering
text into the **Filter** field. This filter text can be a
regular expression and is matched on the entire filename,
including the path.

### Configuring settings

![The setting dialogue][settingsscreen]

The settings dialog allows you to configure folder exclusions
used by the **Find Projects** dialogue (for example you probably
don't want to scan `node_modules`) and the project types used by
both the **Find Projects** dialog and the manual file browser
dialogue.

To display the settings dialog, click **Settings** from the main
window.

#### Configuring folder exclusions

Enter the names of any folders you want to exclude from
automatic scanning, one per line. These matches are a simple
text match on a full path, so use directory separators to avoid
partial matches - e.g. `\bin\` to ignore all folders named
`bin`, but not folders that contain `bin`.

#### Configuring project types

Enter a simple description and file mask for each type, one type
per line. Separate the description and mask with the pipe (`|`)
character. Multiple masks can be used, separate each with a
semi-colon (`;`).

See the [Filter][filedialogfilter] property documentation for
the `FileDialog` class for more information on this method of
specifying filter options.

### Adding a single project to the MRU

To add a single project to the list, click the **Add File**
button then select the project you want to include.

> Note: Currently changes won't be saved unless you click **OK**
> from this dialogue, _or_ click the **Settings** button and then
> click **OK** from the Settings dialogue.

### Adding multiple projects to the MRU

![Scanning a folder for projects][addfolderscreen]

To add multiple projects to the list, click the **Add Folder**
button, then select a folder. The folder (and its sub folders)
will then be scanned for any projects not already in the main
MRU. Tick the projects you want to add, then click **OK** to update
the main list.

### Removing projects from the MRU

To remove projects from the MRU, select one or more projects,
click the **Remove button** and then confirm the removal.

> Note: Currently changes won't be saved unless you click **OK**
> from this dialogue, _or_ click the **Settings** button and then
> click **OK** from the Settings dialogue.

## Configuration Settings

The settings for the extension are saved into an XML file
located at
`%AppData%\Cyotek\VisualStudioExtensions\AddProjects\config.xml`.

```xml
<!--?xml version="1.0"?-->
<ExtensionSettings xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <ExcludedFolders>
    <string>\bower_components\</string>
    <string>\node_modules\</string>
    <string>\bin\</string>
    <string>\obj\</string>
  </ExcludedFolders>
  <ProjectTypes>
    <string>C# Projects|*.csproj</string>
    <string>Visual Basic Projects|*.vbproj</string>
    <string>C++ Projects|*.vcproj;*.vcxproj</string>
    <string>F# Projects|*.fsproj</string>
    <string>NuGet Packager Projects|*.nuproj</string>
  </ProjectTypes>
  <Projects>
    <string>C:\Checkout\cyotek\source\Libraries\Cyotek.ApplicationServices\Cyotek.ApplicationServices.csproj</string>
    <string>C:\Checkout\cyotek\source\Libraries\Cyotek.ApplicationServices.Commands\Cyotek.ApplicationServices.Commands.csproj</string>
    <!-- SNIP -->
    <string>C:\Checkout\cyotek\source\Libraries\Cyotek.Windows.Runtime.Support\Cyotek.Windows.Runtime.Support.csproj</string>
    <string>C:\Checkout\cyotek\source\Libraries\Cyotek.Windows.Runtime.Testing\Cyotek.Windows.Runtime.Testing.csproj</string>
  </Projects>
</ExtensionSettings> 
```

The `Projects` element stores the MRU list of projects.

The `ExcludedFolders` element stores a list of folder names to
ignore when automatically scanning a folder tree, for example
`node_modules`.

The `ProjectTypes` element stores a set of filters that are used
by file and folder dialogues.

## Known Issues

These are the issues I currently know about - please let me know
if you find anything else.

* The extension is using Windows Forms instead of WPF, so it
  won't theme with VS
* There's little error handling and it's not hooked up with the
  normal automatic error submission library I use. So if it
  crashes, it won't tell me automatically (so please raise an
  issue if it does!)
* As I already mentioned, if you make changes to the core
  project list, it will only save it you select something to add
  to your solution.
* It doesn't honour the current selection in the Project
  Explorer. For example, if you create and select a Solution
  Folder, using File | Add | Existing Project will add the
  project as a child. This extension won't, as by the time I'd
  finished writing it I could have filled several swear jars
* The source code is awful, but it does work
* It was written for an older version of Visual Studio. While it
  was updated slightly to support the new async model in 2019, I
  have no idea if it follows best practices for extensions
* It can be a bit slow when adding lots of projects. I never did
  get around to seeing if it was possible to batch add projects
  to Visual Studio... it is still far quicker than doing it by
  hand

More information can be found on the [Cyotek Blog][blog].

## Change Log

Please view the change log for this extension on the
[GitHub][source] page for this project.

## Installing the extension

When newer versions of Visual Studio were released, I discovered
I couldn't compile the extension to support both the previous
version(s) and the new... no doubt it is possible, but I wasn't
willing to devote the time to it back then, and am even less so
now.

* [Visual Studio 2012][vs2015dl] (Actually, I can't remember
  what version this particular download supports. It claims to
  support 2012 onwards, but this is an error in how I defined
  the manifest)
* [Visual Studio 2017][vs2017dl]
* [Visual Studio 2019][vs2019dl]
* [Visual Studio 2022][vs2022dl]

## Debugging the extension

Unlike with previous versions of Visual Studio, with VS2022 you
can simply start debugging without having to make manual changes
to the debug configuration - it will automatically open in the
"Experimental" Visual Studio instance. You will need the Visual
Studio 2022 SDK however. If this isn't already installed, Visual
Studio will automatically prompt to install upon opening the
solution.

## License

The Add Existing Projects extension is licensed under the MIT
License. See `LICENSE.txt` for the full text.

[source]: https://github.com/cyotek/Cyotek.AddProjects
[issues]: https://github.com/cyotek/Cyotek.AddProjects/issues
[blog]: https://www.cyotek.com/blog/tag/vsix

[vs2015dl]: https://marketplace.visualstudio.com/items?itemName=RichardJMoss.AddExistingProjects
[vs2017dl]: https://marketplace.visualstudio.com/items?itemName=RichardJMoss.AddExistingProjects-19444
[vs2019dl]: https://github.com/cyotek/Cyotek.AddProjects/releases/tag/1.0.8.1
[vs2022dl]: https://marketplace.visualstudio.com/items?itemName=cyotek.cyotekaddprojectsvs2022

[filedialogfilter]: https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.filedialog.filter?view=windowsdesktop-6.0#remarks

[menuscreen]: res/addprojects-menu.png
[mainscreen]: res/addprojects-main.png
[addfolderscreen]: res/addprojects-findprojects.png
[settingsscreen]: res/addprojects-settings.png
