using Entities.MessageBroker;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentValidationMessageConsumerService _paymentValidationMessageConsumerService;
        public PaymentController(PaymentValidationMessageConsumerService paymentValidationMessageConsumerService)
        {
            _paymentValidationMessageConsumerService = paymentValidationMessageConsumerService;
        }
        [HttpPost("start-payment")]
        public IActionResult StartPayment([FromBody] PaymentValidation paymentValidation)
        {

            return Ok("Payment process started.");
        }
    }
}