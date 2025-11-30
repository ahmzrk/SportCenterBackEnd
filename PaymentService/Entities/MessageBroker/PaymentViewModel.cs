using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.MessageBroker
{
    public class PaymentViewModel
    {
        public decimal Price { get; set; }
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public string ExpireMonth { get; set; }
        public string ExpireYear { get; set; }
        public string Cvc { get; set; }
        public int MemberId { get; set; }
        public int BookingId { get; set; }
    }
}
