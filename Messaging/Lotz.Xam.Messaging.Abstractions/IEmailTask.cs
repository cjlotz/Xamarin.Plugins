using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lotz.Xam.Messaging.Abstractions
{
    public interface IEmailTask
    {
        Task SendEmailAsync(EmailMessageRequest email);
    }

    public class EmailMessageRequest
    {
        private List<string> _recipients;
        private List<string> _recipientsBcc;
        private List<string> _recipientsCc;

        #region Properties

        public string Message { get; set; }

        public List<string> Recipients
        {
            get { return _recipients ?? (_recipients = new List<string>()); }
        }

        public List<string> RecipientsBcc
        {
            get { return _recipientsBcc ?? (_recipientsBcc = new List<string>()); }
            set { _recipientsBcc = value; }
        }

        public List<string> RecipientsCc
        {
            get { return _recipientsCc ?? (_recipientsCc = new List<string>()); }
            set { _recipientsCc = value; }
        }

        public string Subject { get; set; }

        #endregion
    }
}