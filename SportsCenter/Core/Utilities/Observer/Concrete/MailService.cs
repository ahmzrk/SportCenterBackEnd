using Core.Utilities.Observer.Abstract;

namespace Core.Utilities.Observer.Concrete
{
    public class MailService : IObserver
    {
        private readonly string _recipientEmail;

        public MailService(string recipientEmail)
        {
            _recipientEmail = recipientEmail;
        }

        public void Update(ISubject subject)
        {
            if (subject is Observe stock)
            {
                MailHelper.SendPaymentEmail(_recipientEmail, "Müşteri", "123456", 750);
                Console.WriteLine($"MailService: {_recipientEmail} adresine e-posta gönderildi.");
            }
        }
    }
    }

