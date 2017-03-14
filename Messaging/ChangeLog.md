##Change Log##

###[4.0.0]###
- Android: Add support for using `FileProvider` to add attachments using content Uri's on Android Nougat and later
- **Breaking Change**: Remove iOS Classic support
- **Breaking Change**: Remove Windows Phone 8.0 and 8.1 support

###[3.2.1]###
- Add `EmailMessageBuilder.WithAttachment` overload to add attachments directly from within PCL code (Android/iOS only)
- Add additional `IEmailTask.CanSendEmailAttachments`, `IEMailTask.CanSendEmailBodyAsHtml`
- Add `CrossMessaging` singleton as alternative to `MessagingPlugin` to access API features
- Rename assemblies to `Plugin.Messaging`

###[3.0.0]###
- Add UWP Support
- **Breaking Change**: Change namespace to `Plugin.Messaging`

###[2.3.0]###
- Allow specifying empty/null text and subject for Sms, Email
- Fix for finding correct `UIVIewController` on iOS

###[2.2.1]###
- Allow specifying multiple email attachments for Android
- Resolved issued with `CanSendEmail` not working on Android 5.0 and later

###[2.1.0]###
- Allow specifying empty/null recipient for Sms, Email

###[2.0.1]###
- Resolved issued with Bcc being added to Cc recipients

###[2.0]###
- Added support for attachments via `IEmailAttachment` abstraction (supported on Android, iOS and WinPhone RT only)
- Added `IEmailMessage` abstraction
- **Breaking change**: Deprecated `EmailMessageRequest`. Construct `IEmailMessage` using `EmailMessageBuilder` instead.
- **Breaking change**: Changed `IEmailTask.SendMail` overload to use `IEmailMessage`.
- **Breaking change**: Deprecated `Plugin.Xamarin.Messaging.Abstractions` namespace - use `Plugin.Messaging` instead

###[1.4]###
- Added HTML support (only supported on Android/iOS)

###[1.3]###
- Added new `EmailMessageBuilder`

###[1.2]###
- Added new `IPhoneCallTask.CanMakePhoneCall`
- Added `IEmailTask.SendEmail` overload to make it easier to send simple email request
- Added Windows Store assembly. Does not support making phone calls or sending sms and only partial e-mail support via `mailto` protocol.
