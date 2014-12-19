# Xamarin Plugins

Welcome to my repository of cross-platform Xamarin Plugins.

## Xam.Plugins.Messaging

![Logo](https://github.com/cjlotz/Xamarin.Plugins/blob/master/Messaging/Common/Messaging.png)

The Messaging plugin makes it possible to make a phone call, send a sms or send an e-mail using the default messaging applications on the different mobile platforms.

### NuGet 

You can install the Messaging plugin from [NuGet](https://www.nuget.org/packages/Xam.Plugins.Messaging/)

### Examples 

Source code examples of using the Messaging plugin on the different mobile platforms can be found by opening the `Lotz.Xam.Messaging.Samples.sln`.

### API Design

The Messaging Pluging makes use of `IEmailTask`, `ISmsTask` and `IPhoneCallTask` abstractions to send an e-mail, send a sms or make a phone call respectively.  These abstractions are defined within the `Lotz.Xam.Messaging.Abstractions` PCL library.  Platform specific implementations for these different abstractions are provided within a `Lotz.Xam.Messaging` library for the different platforms.

```csharp
public interface IEmailTask
{
    bool CanSendEmail { get; }
    void SendEmail(EmailMessageRequest email);
}
```

```csharp
public interface ISmsTask
{
    bool CanSendSms { get; }
    void SendSms(SmsMessageRequest sms);
}
```

```csharp
public interface IPhoneCallTask
{
    void MakePhoneCall(string number, string name = null);
}
```

### Using the API 
The messaging API's can be accessed on the different mobile platforms using the `MessagingPlugin` container class and a platform specific implementation of the `IMessagingContext` interface.  The context is required to send through platform specific information like the current `Activity` on Android or the `UIViewController` on iOS.  Here are some snippets to illustrate how to access the API from within an `Activity`, `UIViewController` or Windows `Page`.  

```csharp
// Make a phone call
var dialer = MessagingPlugin.PhoneDialer(new MessagingContext(this));
dialer.MakePhoneCall("+272193343499");
```
```csharp
// Send sms
var smsTask = MessagingPlugin.SmsMessenger(new MessagingContext(this));
if (smsTask.CanSendSms)
{
   var sms = new SmsMessageRequest("+27213894839493", "Well hello there from Xam.Messaging.Plugin");
   smsTask.SendSms(sms);
}
``` 
```csharp
// Send e-mail
var emailTask = MessagingPlugin.EmailMessenger(new MessagingContext(this));
if (emailTask.CanSendEmail)
{
    var email = new EmailMessageRequest("to.plugins@xamarin.com", "Xamarin Messaging Plugin",
        "Well hello there from Xam.Messaging.Plugin");
    
	email.RecipientsCc.Add("cc.plugins@xamarin.com");

    emailTask.SendEmail(email);
}           
```
