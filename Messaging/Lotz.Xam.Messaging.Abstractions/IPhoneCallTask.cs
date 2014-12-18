namespace Lotz.Xam.Messaging.Abstractions
{
    public interface IPhoneCallTask
    {
        void MakePhoneCall(string number, string name = null);
    }
}