// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace Plugin.Messaging.Sample.iOSUnified
{
    [Register ("RootViewController")]
    partial class RootViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ButtonMakePhoneCall { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ButtonSendAttachmentsEmail { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ButtonSendAttachmentsEmailPcl { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ButtonSendEmail { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ButtonSendHtmlEmail { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ButtonSendMultipleSMS { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ButtonSendSms { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ButtonMakePhoneCall != null) {
                ButtonMakePhoneCall.Dispose ();
                ButtonMakePhoneCall = null;
            }

            if (ButtonSendAttachmentsEmail != null) {
                ButtonSendAttachmentsEmail.Dispose ();
                ButtonSendAttachmentsEmail = null;
            }

            if (ButtonSendAttachmentsEmailPcl != null) {
                ButtonSendAttachmentsEmailPcl.Dispose ();
                ButtonSendAttachmentsEmailPcl = null;
            }

            if (ButtonSendEmail != null) {
                ButtonSendEmail.Dispose ();
                ButtonSendEmail = null;
            }

            if (ButtonSendHtmlEmail != null) {
                ButtonSendHtmlEmail.Dispose ();
                ButtonSendHtmlEmail = null;
            }

            if (ButtonSendMultipleSMS != null) {
                ButtonSendMultipleSMS.Dispose ();
                ButtonSendMultipleSMS = null;
            }

            if (ButtonSendSms != null) {
                ButtonSendSms.Dispose ();
                ButtonSendSms = null;
            }
        }
    }
}