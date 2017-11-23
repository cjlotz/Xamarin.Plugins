using System;
using Tizen.Applications;
using Tizen.PhonenumberUtils;
using Tizen.System;

namespace Plugin.Messaging
{
	internal class PhoneCallTask : IPhoneCallTask
	{
		static PhonenumberUtils utils = null;
		static bool isTelephonySupported = false;

		public PhoneCallTask()
		{
			Information.TryGetValue("http://tizen.org/feature/network.telephony", out isTelephonySupported);
			try
			{
				utils = new PhonenumberUtils();
			}
			catch
			{
				Console.Write("Invalid NotSupportedException");
			}
		}

		public bool CanMakePhoneCall
		{
			get
			{
				return isTelephonySupported == true && utils != null;
			}
		}

		public void MakePhoneCall(string number, string name = null)
		{
			if (string.IsNullOrWhiteSpace(number))
				throw new ArgumentNullException(nameof(number));
			
			if (CanMakePhoneCall)
			{
				AppControl appControl = new AppControl();
				appControl.Operation = AppControlOperations.Dial;
				appControl.Uri = "tel:" + utils.GetNormalizedNumber(number);
				AppControl.SendLaunchRequest(appControl);
			}
		}
	}
}