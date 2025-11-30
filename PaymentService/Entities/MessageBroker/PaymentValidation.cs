namespace Entities.MessageBroker
{
    public class PaymentValidation
    {
        public int MemberId { get; set; }
        public int BookingId { get; set; }
        public decimal Price { get; set; }
    }
}
