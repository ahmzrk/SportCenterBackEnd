using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Events;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembershipController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public MembershipController(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost("simulate-failure")]
        public async Task<IActionResult> SimulateFailure(Guid userId, string transactionId)
        {
            // Üyelik işleminin başarısız olduğunu simüle ediyoruz
            Console.WriteLine($"Üyelik işlemi başarısız oldu. İşlem ID: {transactionId}");

            // Hata olayını yayınla
            await _publishEndpoint.Publish(new PaymentFailedEvent
            {
                UserId = userId,
                TransactionId = transactionId,
                Reason = "Simulated failure: could not create user in database.",
                Timestamp = DateTime.UtcNow
            });

            Console.WriteLine($"MembershipFailedEvent mesajı yayınlandı.");

            return Ok("Hata simülasyonu tamamlandı.");
        }
    }
}
