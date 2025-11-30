using MassTransit;
using Shared.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consumer.Payment
{
    public class PaymentSucceededConsumer : IConsumer<PaymentSucceededEvent>
    {
        // Veritabanı işlemleri için repository veya servis enjekte edilebilir.
        // private readonly IOrderRepository _orderRepository;

        public async Task Consume(ConsumeContext<PaymentSucceededEvent> context)
        {
            var paymentSucceededEvent = context.Message;

            // Buradaki sipariş ID'sini olay mesajına eklemeyi unutmayın.
            // var order = await _orderRepository.GetByIdAsync(paymentSucceededEvent.OrderId);

            // if (order != null)
            // {
            //     order.Status = "Payment Succeeded";
            //     await _orderRepository.UpdateAsync(order);
            // }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Ödeme Başarılı! Sipariş güncelleniyor: {paymentSucceededEvent.TransactionId}");
            Console.ResetColor();

            // Diğer iş akışı adımları tetiklenebilir, örneğin:
            // - Lojistik servisine mesaj gönderme
            // - Kullanıcıya e-posta bildirimi gönderme
        }
    }
}
