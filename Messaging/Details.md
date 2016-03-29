
The Messaging plugin makes it possible to make a phone call, send a sms or send an e-mail using the default messaging applications on the different mobile platforms.

### API Usage

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
The messaging API's can be accessed on the different mobile platforms using the `CrossMessaging` singleton or ```MessagingPlugin``` static class.  Here are some snippets to illustrate how to access the API from within an `Activity`, `UIViewController` or Windows `Page`.  

```csharp
// Make Phone Call
var phoneDialer = CrossMessaging.Current.PhoneDialer;
if (phoneDialer.CanMakePhoneCall) 
	phoneDialer.MakePhoneCall("+272193343499");

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

### Platform specific API's

Sending HTML e-mail and adding e-mail attachments are only supported on some platforms.  Use the ```IEmailTask.CanSendEmailAttachments``` and ```IEmailTask.CanSendEmailBodyAsHtml``` API's to test whether the feature is available for the platform in your PCL code.  

To add HTML body content use ```EmailMessageBuilder.BodyAsHtml``` (**iOS, Android**).  

```csharp
// Construct HTML email (iOS and Android only)
var email = new EmailMessageBuilder()
  .To("to.plugins@xamarin.com")
  .Subject("Xamarin Messaging Plugin")
  .BodyAsHtml("Well hello there from <a>Xam.Messaging.Plugin</a>")
  .Build();
```

To add attachments, use the ```EmailMessageBuilder.WithAttachment``` overloads (**iOS, Android, WinPhone RT**).  

```csharp
// Construct email with attachment on Android (single file only)
var email = new EmailMessageBuilder()
  .To("to.plugins@xamarin.com")
  .Subject("Xamarin Messaging Plugin")
  .Body("Well hello there from Xam.Messaging.Plugin")
  .WithAttachment("/storage/emulated/0/Android/data/MyApp/files/Pictures/temp/IMG_20141224_114954.jpg");
  .Build();
```
