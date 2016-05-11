![icon-64](resources/icon-32.png) Cyotek Add Existing Projects Visual Studio Extension
====================================================

The excitingly named **Add Existing Projects** extension is a simple Visual Studio extension that makes it easy to add multiple projects to your solution without wearing your fingers out manually adding projects one at a time.

Installing the extension
-------------------------
[Download](https://visualstudiogallery.msdn.microsoft.com/dc3adb4b-3b94-4ca0-97fd-3c817bd14a77/) the VSIX file from the Microsoft Gallery. Make sure Visual Studio isn't running, then run the VSIX file to install.


Debugging the extension
-----------------------

To enable debugging of the extension, you need to run it in the context of the Visual Studio Experimental Instance. To do this

* Open the Project Properties for the extension
* On the **Debug** tab, set the **Start Action** to be **Start External Program**
* Set the external program to be the Visual Studio developer environment - typically `C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\devenv.exe` (VS2015 on 64bit O/S) or `C:\Program Files\Microsoft Visual Studio 14.0\Common7\IDE\devenv.exe` (VS2015 on 32bit O/S) 
* Set the **Command Line Arguments** to be `/rootsuffix Exp`

You may also need the Visual Studio 2015 SDK. If this isn't already installed, Visual Studio 2015 will automatically prompt to install upon opening the solution.

License
-------


The Add Existing Projects extension is licensed under the MIT License. See `LICENSE.txt` for the full text. 

Further Reading
---------------

For more information, see article [tagged with vsix](http://www.cyotek.com/blog/tag/vsix) at www.cyotek.com.
