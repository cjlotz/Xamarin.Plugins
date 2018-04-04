## Getting Started

The Messaging plugin makes it possible to make a phone call, send a sms or send an e-mail using the default messaging applications on the different mobile platforms.

## API Usage

The Messaging Plugin makes use of `IEmailTask`, `ISmsTask` and `IPhoneCallTask` abstractions to send an e-mail, send a sms or make a phone call respectively.  These abstractions are defined within the `Plugin.Messaging.Abstractions` PCL library.  Platform specific implementations for these different abstractions are provided within a `Plugin.Messaging` library for the different platforms.

```csharp
public interface IEmailTask
{
    bool CanSendEmail { get; }
    bool CanSendEmailAttachments { get; }
    bool CanSendEmailBodyAsHtml { get; }
    void SendEmail(IEmailMessage email);
    void SendEmail(string to, string subject, string message);
}
```

```csharp
public interface ISmsTask
{
    bool CanSendSms { get; }
    bool CanSendSmsInBackground { get; }
    void SendSms(string recipient, string message);
    void SendSmsInBackground(string recipient, string message);
}
```

```csharp
public interface IPhoneCallTask
{
    bool CanMakePhoneCall { get; }
    void MakePhoneCall(string number, string name = null);
}
```

## Using the API

The messaging API's can be accessed on the different mobile platforms using the `CrossMessaging` singleton class.  Have a look at the `Plugin.Messaging.Samples.sln` for samples that illustrate using the API on the different platforms. Here are some snippets from the samples that illustrate how to access the API from within an `Activity`, `UIViewController` or Windows `Page`.

```csharp
// Make Phone Call
var phoneDialer = CrossMessaging.Current.PhoneDialer;
if (phoneDialer.CanMakePhoneCall)
   phoneDialer.MakePhoneCall("+27219333000");

// Send Sms
var smsMessenger = CrossMessaging.Current.SmsMessenger;
if (smsMessenger.CanSendSms)
   smsMessenger.SendSms("+27213894839493", "Well hello there from Xam.Messaging.Plugin");

var emailMessenger = CrossMessaging.Current.EmailMessenger;
if (emailMessenger.CanSendEmail)
{
    // Send simple e-mail to single receiver without attachments, bcc, cc etc.
    emailMessenger.SendEmail("to.plugins@xamarin.com", "Xamarin Messaging Plugin", "Well hello there from Xam.Messaging.Plugin");

    // Alternatively use EmailBuilder fluent interface to construct more complex e-mail with multiple recipients, bcc, attachments etc.
    var email = new EmailMessageBuilder()
      .To("to.plugins@xamarin.com")
      .Cc("cc.plugins@xamarin.com")
      .Bcc(new[] { "bcc1.plugins@xamarin.com", "bcc2.plugins@xamarin.com" })
      .Subject("Xamarin Messaging Plugin")
      .Body("Well hello there from Xam.Messaging.Plugin")
      .Build();

    emailMessenger.SendEmail(email);
}
```

## Permissions

The plugin requires the following sets of permissions on the different platforms:

### iOS

| Action | Supported | Permission
| --- | --- | --- |
| Phone | Yes | |
| Email | Yes | |
| Email (HTML) | Yes | |
| Sms | Yes | |
| Sms (Background) | No | |

### Android

| Action | Supported | Permission
| --- | --- | --- |
| Phone | Yes | |
| Email | Yes | |
| Email (HTML) | Yes | |
| Sms | Yes | `android.permission.SEND_SMS` |
| Sms (Background) | Yes | `android.permission.SEND_SMS`, `android.permission.READ_PHONE_STATE` |

### UWP

| Action | Supported | Permission
| --- | --- | --- |
| Phone | Yes | |
| Email | Yes | |
| Email (HTML) | No | |
| Sms | Yes | |
| Sms (Background) | Yes | `cellularMessaging` |

### Tizen

| Action | Supported | Permission
| --- | --- | --- |
| Phone | Yes | |
| Email | Yes | `email` |
| Email (HTML) | No | |
| Sms | Yes | `message.read`, `message.write` |
| Sms (Background) | No | |

## Attachments

To add attachments, use the ```EmailMessageBuilder.WithAttachment``` overloads.  There are platform specific overloads that will allow you to attach a `Windows.Storage.IStorageFile` (**UWP**), `Java.IO.File` (**Android**) and `Foundation.NSUrl` (**iOS**).  Alternatively use the `WithAttachment(string, string)` overload to attach a file from within a PCL project. 

> Please note that on the Windows platform, attaching from the PCL only works for files contained within the ApplicationData due to the security restrictions imposed by the platform.

```csharp
// Android
File file = new File("<path_to_file>");
var email = new EmailMessageBuilder()
  .To("to.plugins@xamarin.com")
  .Subject("Xamarin Messaging Plugin")
  .Body("Well hello there from Xam.Messaging.Plugin")
  .WithAttachment(file);
  .Build();

// iOS
NSUrl file = new NSUrl("<path_to_file>", false);
var email = new EmailMessageBuilder()
  .To("to.plugins@xamarin.com")
  .Subject("Xamarin Messaging Plugin")
  .Body("Well hello there from Xam.Messaging.Plugin")
  .WithAttachment(file);
  .Build();

// PCL
var email = new EmailMessageBuilder()
  .To("to.plugins@xamarin.com")
  .Subject("Xamarin Messaging Plugin")
  .Body("Well hello there from Xam.Messaging.Plugin")
  .WithAttachment("<path_to_picture>", "image/jpeg");
  .Build();
```

## Platform specific API's

The following features are not supported on all platforms.

### HTML Content (iOS, Android)

To add HTML body content use ```EmailMessageBuilder.BodyAsHtml```.

```csharp
// Construct HTML email (iOS and Android only)
var email = new EmailMessageBuilder()
  .To("to.plugins@xamarin.com")
  .Subject("Xamarin Messaging Plugin")
  .BodyAsHtml("Well hello there from <a>Xam.Messaging.Plugin</a>")
  .Build();
```

Use the ```IEmailTask.CanSendEmailBodyAsHtml``` API's to test whether the feature is available for the platform in your PCL code.

### Strict Mode (Android)

By default when sending an email using the `IEmailTask`, the plugin presents a list of all apps capable of handling the `Send` intent. This presents all kinds of apps that are not pure email apps.  If you wish to filter the list to only include email apps, you can change the plugin behavior by:

```csharp
// Available ONLY in Android project (not in PCL/.NET Standard project)
CrossMessaging.Current.Settings().Email.UseStrictMode = true;
```

> Unfortunately StrictMode does not seems to play nicely with adding attachments, so sending attachments using StrictMode is currently not supported

### Formatting Phone Numbers (Android)

By default when making a phone call using the `IPhoneCallTask`, the plugin will format the number using the [`formatNumber`](https://developer.android.com/reference/android/telephony/PhoneNumberUtils.html#formatNumber(java.lang.String)) API that formats the number according to the country rules for the number. The API has been deprecated in API 21. To support formatting the number on API 21 or later a new `DefaultCountryIso` setting has been introduced to use the new [`formatNumber`](https://developer.android.com/reference/android/telephony/PhoneNumberUtils.html#formatNumber(java.lang.String,java.lang.String)) API.

```csharp
// Available ONLY in Android project (not in PCL/.NET Standard project)

// The ISO 3166-1 two letters country code whose convention will be used if the given number doesn't  have the country code.
CrossMessaging.Current.Settings().Phone.DefaultCountryIso = "ZA";
```

### AutoDial (Android)

By default phoning a number using the `IPhoneCallTask`, the plugin only shows a phone dialer with the number populated. If you want the plugin to automatically dial the number, you can change the plugin behavior by:

```csharp
// Available ONLY in Android project (not in PCL/.NET Standard project)

CrossMessaging.Current.Settings().Phone.AutoDial = true;
```

> Please note using this settings requires the `android.permission.CALL_PHONE` added to the manifest file.

### Send Background SMS (Android,UWP)

By default, when sending a SMS using the `ISmsTask.SendSms`, the plugin shows the default messaging user interface. Using the `ISmsTask.CanSendSmsInBackground` and `ISmsTask.SendSmsInBackground`, you can now send a sms silently in the background without showing the messaging user interface.

```csharp
var smsMessenger = CrossMessaging.Current.SmsMessenger;
if (smsMessenger.CanSendSmsInBackground)
   smsMessenger.SendSmsInBackground("+27213894839", "Well hello there from Xam.Messaging.Plugin");
```

> For Android, please add the `android.permission.SEND_SMS` and `android.permission.READ_PHONE_STATE` permissions to your Android manifest file.

> For UWP, please add the `cellularMessaging` restricted capability to your package manifest file.  Also [read more about submitting an app using this restricted permission](https://docs.microsoft.com/en-us/windows/uwp/packaging/app-capability-declarations#special-and-restricted-capabilities) on the UWP platform.

### Custom Navigation (iOS)

By default when sending an email using the `IEmailTask` or an sms using the `ISmsTask`, the plugin presents the `MFMailComposeViewController` or `MFMessageComposeViewController` using internal logic that first locates the current visible `ViewController` before using `ViewController.PresentViewController` to show the mail/message compose view.  To allow users to provide their own navigation logic, create your own implementation for the `IEmailPresenter` or `ISmsPresenter` interface.  Set this to replace the plugin's `DefaultEmailPresenter` or `DefaultSmsPresenter` as the default presenter to use for the plugin.

```csharp
// Available ONLY in iOS project (not in PCL/.NET Standard project)
public class MyCustomEmailPresenter : IEmailPresenter
{
  public void PresentMailComposeViewController(MFMailComposeViewController mailController)
  {
    ...
  }
}

// Available ONLY in iOS project (not in PCL/.NET Standard project)
public class MyCustomSmsPresenter : ISmsPresenter
{
  public void PresentMessageComposeViewController(MFMessageComposeViewController smsController)
  {
    ...
  }
}

CrossMessaging.Current.Settings().Email.EmailPresenter = new MyCustomEmailPresenter();
CrossMessaging.Current.Settings().Sms.SmsPresenter = new MyCustomSmsPresenter();
```