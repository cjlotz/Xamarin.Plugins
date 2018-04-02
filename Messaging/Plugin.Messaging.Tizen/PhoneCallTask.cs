using System;
using Tizen.Applications;
using Tizen.PhonenumberUtils;
using Tizen.System;

namespace Plugin.Messaging
{
	internal class PhoneCallTask : IPhoneCallTask
	{
	    private static PhonenumberUtils _utils;
	    private static bool _isTelephonySupported;

		public PhoneCallTask()
		{
			Information.TryGetValue("http://tizen.org/feature/network.telephony", out _isTelephonySupported);
			try
			{
				_utils = new PhonenumberUtils();
			}
			catch
			{
				Console.Write("Invalid NotSupportedException");
			}
		}

		public bool CanMakePhoneCall => _isTelephonySupported && _utils != null;

	    public void MakePhoneCall(string number, string name = null)
		{
			if (string.IsNullOrWhiteSpace(number))
				throw new ArgumentNullException(nameof(number));
			
			if (CanMakePhoneCall)
			{
				AppControl appControl = new AppControl();
				appControl.Operation = AppControlOperations.Dial;
				appControl.Uri = "tel:" + _utils.GetNormalizedNumber(number);
				AppControl.SendLaunchRequest(appControl);
			}
		}
	}
}