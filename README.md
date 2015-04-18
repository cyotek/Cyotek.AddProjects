Cyotek Add Projects Visual Studio Extension
===========================================

The Add Projects extension is a simple Visual Studio extension that makes it easy to add multiple projects to your solution.

Debugging the extension
-----------------------

To enable debugging of the extension, you need to run it in the context of the Visual Studio Experimental Instance. To do this

* Open the Project Properties for the extension
* On the **Debug** tab, set the **Start Action** to be **Start External Program**
* Set the external program to be the Visual Studio developer environment - typically `C:\Program Files (x86)\Microsoft Visual Studio 12.0\Common7\IDE\devenv.exe` (VS2013 on 64bit O/S) or `C:\Program Files\Microsoft Visual Studio 12.0\Common7\IDE\devenv.exe` (VS2013 on 32bit O/S) 
* Set the **Command Line Arguments** to be `/rootsuffix Exp`

You may also need the [Visual Studio 2013 SDK](http://www.microsoft.com/en-gb/download/details.aspx?id=40758).

License
-------

The Add Projects extension is licensed under the MIT License. See `LICENSE.md` for the full text. 

Further Reading
---------------

For more information, see article [tagged with vsix](http://www.cyotek.com/blog/tag/vsix) at www.cyotek.com.
