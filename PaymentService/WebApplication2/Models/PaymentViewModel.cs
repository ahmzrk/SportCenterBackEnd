namespace WebApplication2.Models
{
    public class PaymentViewModel
    {
        public string CardNumber { get; set; }
        public string ExpireMonth { get; set; }
        public string ExpireYear { get; set; }
        public string Cvc { get; set; }
        public decimal Price { get; set; }
    }
}
