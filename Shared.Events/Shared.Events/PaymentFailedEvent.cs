using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Events
{
    public class PaymentFailedEvent
    {
        public string TransactionId { get; set; }

        public Guid UserId { get; set; }

        public string Reason { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
