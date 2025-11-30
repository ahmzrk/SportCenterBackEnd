using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.MessageBroker
{
    public class PaymentValidation
    {
        public int MemberId { get; set; }
        public int BookingId { get; set; }
        public decimal Price { get; set; }
    }
}
