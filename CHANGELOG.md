Change Log
==========

1.0.8.1
-------

#### Added

* Missing projects are now highlighted in the list (this was previously only available in debug builds)

#### Fixed

* Default filters were continuously being duplicated
* Clicking OK in the Add Projects Dialog now continues even if no projects were selected to add (e.g. it saves any setting changes)

1.0.8.0
-------

#### Added

* Added support for Visual Studio 2019

1.0.7.0
-------

#### Added

* Added support for Visual Studio 2017

1.0.6.0
-------

#### Added
* Added the ability to customize the file dialog filters
* Added Nuget projects (*.nuproj) as a default file filter
* Added the ability to configure settings via the main dialog

#### Removed
* Removed manifest support for older versions of Visual Studio, this was broken by an earlier update that upgraded to use the Visual Studio 2015 SDK. For Visual Studio 2013 support, you can [download](<http://www.cyotek.com/files/vsix/addprojects/1.0.3.0/Cyotek.VisualStudioExtensions.AddProjects.vsix>) an older version

#### Fixed
* Correctly a duplicate accelerator in the main window

1.0.5.0
-------

#### Added
* Added the ability to exclude folders such as `bower_components`, `node_modules`, `bin`, `obj` etc from being automatically scanned

#### Changed
* Improved error messages when a project cannot be added as the source file can no longer be found

1.0.4.0
-------

#### Changed
* Improved the performance of searching folders for projects [[#2]](https://github.com/cyotek/Cyotek.AddProjects/issues/2)
* Changed .NET dependency to be 4.5 or higher, instead of being fixed to 4.5 only [[#1]](https://github.com/cyotek/Cyotek.AddProjects/issues/1)

1.0.3.0
-------

#### Changed
* Updated Add File browse dialog to include VB, C#, F# and VC++ projects
* When adding a new project to the dialog, the project list automatically scrolls to the new item
* The main project list now greys out projects which are already a part of your solution
* The Add Folder button now displays an interim dialog so that you can selected which projects you want to add the list, instead of blanket adding all matches then requiring you to remove anything you don't want
* Project list now includes an extension column so you can sort by project type

#### Fixed
* The link label for cyotek.com now correctly moves when the dialog is resized

1.0.2.0
-------

#### Added
* Added basic sorting to the project list. No GUI indicators though, sorry.

1.0.1.0
-------

#### Added
* "Add Project" dialog now allows multiple selections
* Added the ability to filter the project list via a regular expression

1.0.0.0
-------
* Initial release

