# Xamarin Plugins

Welcome to my repository of cross-platform Xamarin Plugins.

## Xam.Plugins.Messaging

![Logo](https://github.com/cjlotz/Xamarin.Plugins/blob/master/Messaging/Common/Messaging.png)

The Messaging plugin makes it possible to make a phone call, send a sms or send an e-mail using the default messaging applications on the different mobile platforms.

### NuGet 

You can install the Messaging plugin from [NuGet](https://www.nuget.org/packages/Xam.Plugins.Messaging/)

### Release Notes

v3.0.0
- Add UWP Support
- **Breaking Change**: Change namespace to Plugin.Messaging for improves API discovery

v2.3.0
- Allow specifying empty/null text and subject for Sms, Email
- Fix for finding correct UIVIewController on iOS

v2.2.1
- Allow specifying multiple email attachments for Android
- Resolved issued with CanSendEmail not working on Android 5.0 and later

v2.1
- Allow specifying empty/null recipient for Sms, Email

v2.0
- Added support for attachments via ```IEmailAttachment``` abstraction (supported on Android, iOS and WinPhone RT)
- Added ```IEmailMessage``` abstraction
- **Breaking change**: Deprecated ```EmailMessageRequest```.  Construct ```IEmailMessage``` using ```EmailMessageBuilder``` instead.
- **Breaking change**: Changed ```IEmailTask.SendMail``` overload to use ```IEmailMessage```.
- **Breaking change**: Deprecated ```Lotz.Xamarin.Messaging.Abstractions``` namespace. Use ```Lotz.Xamarin.Messaging``` instead.

v1.4
- Added HTML support (only supported on Android,iOS)

v1.3
- Added new ```EmailMessageBuilder```

v1.2
- Added new ```IPhoneCallTask.CanMakePhoneCall```
- Added ```IEmailTask.SendEmail``` overload to make it easier to send simple email request
- Added Windows Store assembly. Does not support making phone calls or sending sms and only partial e-mail support via ```mailto``` protocol.


### Examples 

Source code examples of using the Messaging plugin on the different mobile platforms can be found by opening the `Lotz.Xam.Messaging.Samples.sln`.

### API Design

The Messaging Pluging makes use of `IEmailTask`, `ISmsTask` and `IPhoneCallTask` abstractions to send an e-mail, send a sms or make a phone call respectively.  These abstractions are defined within the `Lotz.Xam.Messaging.Abstractions` PCL library.  Platform specific implementations for these different abstractions are provided within a `Lotz.Xam.Messaging` library for the different platforms.

```csharp
public interface IEmailTask
{
    bool CanSendEmail { get; }
    void SendEmail(IEmailMessage email);
    void SendEmail(string to, string subject, string message);
}
```

```csharp
public interface ISmsTask
{
    bool CanSendSms { get; }
    void SendSms(string recipient, string message);
}
```

```csharp
public interface IPhoneCallTask
{
	bool CanMakePhoneCall { get; }
    void MakePhoneCall(string number, string name = null);
}
```

### Using the API 
The messaging API's can be accessed on the different mobile platforms using the `MessagingPlugin` container class.  Here are some snippets to illustrate how to access the API from within an `Activity`, `UIViewController` or Windows `Page`.  

```csharp
// Make Phone Call
var phoneCallTask = MessagingPlugin.PhoneDialer;
if (phoneCallTask.CanMakePhoneCall) 
	phoneCallTask.MakePhoneCall("+272193343499");

// Send Sms
var smsTask = MessagingPlugin.SmsMessenger;
if (smsTask.CanSendSms)
   smsTask.SendSms("+27213894839493", "Well hello there from Xam.Messaging.Plugin");

var emailTask = MessagingPlugin.EmailMessenger;
if (emailTask.CanSendEmail)
{
	// Send simple e-mail to single receiver without attachments, bcc, cc etc.
	emailTask.SendEmail("to.plugins@xamarin.com", "Xamarin Messaging Plugin", "Well hello there from Xam.Messaging.Plugin");

	// Alternatively use EmailBuilder fluent interface to construct more complex e-mail with multiple recipients, bcc, attachments etc. 
    var email = new EmailMessageBuilder()
      .To("to.plugins@xamarin.com")
      .Cc("cc.plugins@xamarin.com")
      .Bcc(new[] { "bcc1.plugins@xamarin.com", "bcc2.plugins@xamarin.com" })
      .Subject("Xamarin Messaging Plugin")
      .Body("Well hello there from Xam.Messaging.Plugin")
      .Build();

    emailTask.SendEmail(email);
}           
```

### Platform API Extensions

Sending HTML e-mail and adding e-mail attachments are only supported on some platforms.  These features are provided as platform specific extensions on the ```EmailMessageBuilder``` class.  To add HTML body content use the ```EmailMessageBuilder.BodyAsHtml``` extension (**iOS, Android**).  To add attachments, use the ```EmailMessageBuilder.WithAttachment``` platform specific overloads (**iOS, Android, WinPhone RT**).  Platforms that do not support these features won't have these extensions available to use.  

```csharp
// Construct HTML email (iOS and Android only)
var email = new EmailMessageBuilder()
  .To("to.plugins@xamarin.com")
  .Subject("Xamarin Messaging Plugin")
  .BodyAsHtml("Well hello there from <a>Xam.Messaging.Plugin</a>")
  .Build();

// Construct email with attachment on Android (single file only)
var email = new EmailMessageBuilder()
  .To("to.plugins@xamarin.com")
  .Subject("Xamarin Messaging Plugin")
  .Body("Well hello there from Xam.Messaging.Plugin")
  .WithAttachment("/storage/emulated/0/Android/data/MyApp/files/Pictures/temp/IMG_20141224_114954.jpg");
  .Build();
```

Complete examples of using these extensions on the different platforms are provided in the ```Lotz.Xam.Messaging.Samples.sln```.
