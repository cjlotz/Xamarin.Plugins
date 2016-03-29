
## Messaging Plugin for Xamarin and Windows

The Messaging plugin makes it possible to make a phone call, send a sms or send an e-mail using the default messaging applications on the different mobile platforms.

## Setup
* Available on NuGet: https://www.nuget.org/packages/Xam.Plugins.Messaging [![NuGet](https://img.shields.io/nuget/v/Plugin.Permissions.svg?label=NuGet)](https://www.nuget.org/packages/Xam.Plugins.Messaging/)
* Install into your PCL project and Client projects.

**Platform Support**

|Platform|Supported|Version|
| ------------------- | :-----------: | :------------------: |
|Xamarin.iOS|Yes|iOS 7+|
|Xamarin.iOS Unified|Yes|iOS 7+|
|Xamarin.Android|Yes|API 9+|
|Windows Phone Silverlight|Yes|8.0+|
|Windows Phone RT|Yes|8.1+|
|Windows Store RT|Yes|8.1+|
|Windows 10 UWP|Yes|10+|
|Xamarin.Mac|No||

### API Usage
Full details on the API [are available here](Details.md)

### Release Notes
[3.2.1]
- Add ```EmailMessageBuilder.WithAttachment``` overload to add attachments directly from within PCL code (Android/iOS only)
- Add additional ```IEmailTask.CanSendEmailAttachments```, ```IEMailTask.CanSendEmailBodyAsHtml```
- Add ```CrossMessaging``` singleton as alternative to ```MessagingPlugin``` to access API features
- Rename assemblies to ```Plugin.Messaging```

[3.0.0]
- Add UWP Support
- **Breaking Change**: Change namespace to ```Plugin.Messaging```

[2.3.0]
- Allow specifying empty/null text and subject for Sms, Email
- Fix for finding correct ```UIVIewController``` on iOS

[2.2.1]
- Allow specifying multiple email attachments for Android
- Resolved issued with ```CanSendEmail``` not working on Android 5.0 and later

[2.1.0]
- Allow specifying empty/null recipient for Sms, Email

[2.0.1]
- Resolved issued with Bcc being added to Cc recipients

[2.0]
- Added support for attachments via ```IEmailAttachment``` abstraction (supported on Android, iOS and WinPhone RT only)
- Added ```IEmailMessage``` abstraction
- **Breaking change**: Deprecated ```EmailMessageRequest```. Construct ```IEmailMessage``` using ```EmailMessageBuilder``` instead.
- **Breaking change**: Changed ```IEmailTask.SendMail``` overload to use ```IEmailMessage```.

[1.4]
- Added HTML support (**Android/iOS only**)

[1.3]
- Added new ```EmailMessageBuilder```

[1.2]
- Added new ```IPhoneCallTask.CanMakePhoneCall```
- Added ```IEmailTask.SendEmail`` overload to make it easier to send simple email request
- Added Windows Store assembly. Does not support making phone calls or sending sms and only partial e-mail support via ```mailto``` protocol.

### Contributors
* [cjlotz](https://github.com/cjlotz)
* [jamesmontemagno](https://github.com/jamesmontemagno)

### License
[The MIT License (MIT)](LICENSE.md)