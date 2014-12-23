// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using System;
using Foundation;
using UIKit;
using System.CodeDom.Compiler;

namespace Lotz.Xam.Messaging.iOSUnified.Sample
{
	[Register ("RootViewController")]
	partial class RootViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton ButtonMakePhoneCall { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton ButtonSendEmail { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton ButtonSendHtmlEmail { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton ButtonSendSms { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (ButtonMakePhoneCall != null) {
				ButtonMakePhoneCall.Dispose ();
				ButtonMakePhoneCall = null;
			}
			if (ButtonSendEmail != null) {
				ButtonSendEmail.Dispose ();
				ButtonSendEmail = null;
			}
			if (ButtonSendHtmlEmail != null) {
				ButtonSendHtmlEmail.Dispose ();
				ButtonSendHtmlEmail = null;
			}
			if (ButtonSendSms != null) {
				ButtonSendSms.Dispose ();
				ButtonSendSms = null;
			}
		}
	}
}
