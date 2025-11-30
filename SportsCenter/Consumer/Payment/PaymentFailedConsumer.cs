using MassTransit;
using Shared.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consumer.Payment
{
    public class PaymentFailedConsumer : IConsumer<PaymentFailedEvent>
    {
        // Veritabanı işlemleri için repository veya servis enjekte edilebilir.

        public async Task Consume(ConsumeContext<PaymentFailedEvent> context)
        {
            var paymentFailedEvent = context.Message;

            // Buradaki sipariş ID'sini olay mesajına eklemeyi unutmayın.
            // var order = await _orderRepository.GetByIdAsync(paymentFailedEvent.OrderId);

            // if (order != null)
            // {
            //     order.Status = "Payment Failed";
            //     await _orderRepository.UpdateAsync(order);
            // }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Ödeme Başarısız Oldu! Sipariş güncelleniyor: {paymentFailedEvent.TransactionId}. Sebep: {paymentFailedEvent.Reason}");
            Console.ResetColor();

            // Dengeleme (compensating) işlemleri burada yapılır:
            // - Kullanıcıya e-posta veya SMS ile bildirim gönderme
            // - Kullanıcının sepetini eski haline getirme
        }
    }
}
