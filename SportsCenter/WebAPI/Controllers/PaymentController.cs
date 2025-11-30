using Business.Abstract;
using Core.Utilities.Observer.Abstract;
using Core.Utilities.Observer.Concrete;
using Entities.MessageBroker;
using Entities.MessageBroker.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client.Events;
using System.Threading.Channels;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IRabbitMQPublisher<PaymentValidation> _publisher;
        private readonly IBookingService _bookingService;
        private readonly IObserver _observable;
        private readonly ISubject _subject;     

        public PaymentController(
            IRabbitMQPublisher<PaymentValidation> publisher,
            IBookingService bookingService,
            IObserver observable,
            ISubject subject)
        {
            _publisher = publisher;
            _bookingService = bookingService;
            _observable = observable;
            _subject = subject;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendPaymentMessageAsync([FromBody] PaymentValidation message)
        {
            if (message == null || message.Price <= 0)
            {
                return BadRequest("Geçersiz ödeme bilgisi.");
            }

            try
            {
                var response = await _publisher.PublishMessageWithReplyAsync(message, "orderValidationQueue");
                var mailService = new MailService("redoes123@hotmail.com");
                _subject.Attach(mailService);
                if (response != null && response.IsSuccess)
                {
                    _subject.Attach(_observable);
                    _subject.Notify();

                    return Ok("Ödeme başarılı: " + response.Message);
                }
                else
                {
                    return BadRequest("Ödeme başarısız: " + (response?.Message ?? "Bilinmeyen bir hata oluştu."));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Sunucu hatası: " + ex.Message);
            }
        }
    }
}

