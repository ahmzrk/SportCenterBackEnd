using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Events
{
    public class PaymentSucceededEvent
    {
        // Ödeme işleminin benzersiz ID'si
        public string TransactionId { get; set; }

        // Ödemenin yapıldığı kullanıcının benzersiz ID'si
        public Guid UserId { get; set; }

        // Ödenen tutar
        public decimal Amount { get; set; }

        // İşlemin gerçekleştiği tarih ve saat
        public DateTime Timestamp { get; set; }
    }
}
