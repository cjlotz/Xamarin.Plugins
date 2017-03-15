
## Messaging Plugin for Xamarin and Windows

The Messaging plugin makes it possible to make a phone call, send a sms or send an e-mail using the default messaging applications on the different mobile platforms.

## Setup
* Available on NuGet: https://www.nuget.org/packages/Xam.Plugins.Messaging [![NuGet](https://img.shields.io/nuget/v/Plugin.Permissions.svg?label=NuGet)](https://www.nuget.org/packages/Xam.Plugins.Messaging/)
* Install into your PCL project and Client projects.

**Platform Support**

|Platform|Supported|Version|
| ------------------- | :-----------: | :------------------: |
|Xamarin.iOS|No||
|Xamarin.iOS Unified|Yes|iOS 7+|
|Xamarin.Android|Yes|API 14+|
|Windows Phone Silverlight|No||
|Windows Phone RT|No||
|Windows Store RT|Yes|8.1+|
|Windows 10 UWP|Yes|10+|
|Xamarin.Mac|No||

### Release Notes
Change log history [available here](ChangeLog.md)

### API Usage
Full details on the API [are available here](Details.md)

#### Android Nougat

If your application targets Android N (API 24) or newer, you must use version 4.0.0+ that has support for using a [File Provider](https://developer.android.com/reference/android/support/v4/content/FileProvider.html) for adding atttachments due to the file system permission changes introduced with Android N.

You also need to add a few additional configuration files to adhere to the new strict mode:

*  Add the following to your `AndroidManifest.xml` inside the `<application>` tags (**YOUR_APP_PACKAGE_NAME** must be set to your app package name):
```
<provider android:name="android.support.v4.content.FileProvider" 
          android:authorities="YOUR_APP_PACKAGE_NAME.fileprovider" 
          android:exported="false" 
          android:grantUriPermissions="true">
   <meta-data android:name="android.support.FILE_PROVIDER_PATHS" android:resource="@xml/file_paths"></meta-data>
</provider>
```  
* Add a new folder called `xml` into your *Resources* folder and add a new XML file called `file_paths.xml`.  Add the following code:
```
<?xml version="1.0" encoding="utf-8"?>
<paths xmlns:android="http://schemas.android.com/apk/res/android">
	<external-path name="external_files" path="." />
</paths>
```

### Contributors
* [cjlotz](https://github.com/cjlotz)
* [jamesmontemagno](https://github.com/jamesmontemagno)

### License
[The MIT License (MIT)](LICENSE.md)
