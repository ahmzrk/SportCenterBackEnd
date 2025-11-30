using Microsoft.AspNetCore.Mvc;
using Entities.MessageBroker;

namespace WebApplication2.Controllers
{
    public class PaymentController : Controller
    {
        private readonly PaymentValidationMessageConsumerService _paymentValidationMessageConsumerService;
        public PaymentController(PaymentValidationMessageConsumerService paymentValidationMessageConsumerService)
        {
            _paymentValidationMessageConsumerService = paymentValidationMessageConsumerService;
        }
        [HttpPost("/payment/process")]
        public IActionResult ProcessPayment([FromBody] PaymentViewModel paymentValidation)
        {
            _paymentValidationMessageConsumerService.StartPayment(paymentValidation); 

            return Ok("Payment process started.");
        }
    }
}
