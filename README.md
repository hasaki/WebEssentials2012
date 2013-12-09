[Web Essentials](http://vswebessentials.com) for Visual Studio 2012
=================

Web Essentials extends Visual Studio with lots of new features that web developers have been missing for many years. 

If you ever write CSS, HTML, JavaScript, TypeScript, CoffeeScript or LESS, then you will find many useful features that make your life as a developer easier. 

This is for all Web developers using Visual Studio.


##Getting started
To contribute to this project, you'll need to do a few things first:

 1. Fork the project on GitHub
 1. Clone it to your computer
 1. Install the [Visual Studio 2012 SDK](http://www.microsoft.com/en-us/download/details.aspx?id=30668).
 1. Open the solution in VS2012.
 2. This verison of Web Essentials uses the ClearScript library to host the V8 script engine.  If you wish to make modifications to [ClearScript](https://github.com/hasaki/clearscript) you'll also have to clone that repository.

To install your local fork into your main VS instance, you will first need to open `Source.extension.vsixmanifest` and bump the version number to make it overwrite the (presumably) already-installed production copy.

You can then build the project, then double-click the VSIX file from the bin folder to install it in Visual Studio.
