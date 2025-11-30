using Iyzipay.Model;
using Iyzipay;
using Iyzipay.Request;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Entities.MessageBroker
{
    public class PaymentValidationMessageConsumerService : BackgroundService
    {
        private readonly RabbitMQSettings _rabbitMqSetting;
        private IConnection _connection;
        private IModel _channel;

        public PaymentValidationMessageConsumerService(IOptions<RabbitMQSettings> rabbitMqSetting)
        {
            _rabbitMqSetting = rabbitMqSetting.Value;

            var factory = new ConnectionFactory
            {
                HostName = _rabbitMqSetting.HostName,
                UserName = _rabbitMqSetting.UserName,
                Password = _rabbitMqSetting.Password
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            StartConsuming(RabbitMQQueues.OrderValidationQueue, stoppingToken);
            await Task.CompletedTask;
        }

        private void StartConsuming(string queueName, CancellationToken cancellationToken)
        {
            _channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var props = ea.BasicProperties;
                var replyProps = _channel.CreateBasicProperties();
                replyProps.CorrelationId = props.CorrelationId;

                var body = ea.Body.ToArray();
                var messageJson = Encoding.UTF8.GetString(body);
                var paymentMessage = JsonConvert.DeserializeObject<PaymentValidation>(messageJson);
                PaymentViewModel paymentViewModel = new PaymentViewModel
                {
                    Price = paymentMessage.Price,
                    CardHolderName = "akbank",
                    CardNumber = "5526080000000006",
                    ExpireMonth = "12",
                    ExpireYear = "2030",
                    Cvc = "123",
                    MemberId = paymentMessage.MemberId,
                    BookingId = paymentMessage.BookingId
                };
                await StartPayment(paymentViewModel);

                var responseModel = new ResponseModel
                {
                    IsSuccess = true,
                    Message = "Ödeme başarıyla tamamlandı."
                };

                var responseBody = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(responseModel));

                _channel.BasicPublish(
                    exchange: "",
                    routingKey: props.ReplyTo,
                    basicProperties: replyProps,
                    body: responseBody);

                _channel.BasicAck(ea.DeliveryTag, false);
            };

            _channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
        }

        public async Task StartPayment(PaymentViewModel message)
        {
            // StartPayment metodunda Options nesnesini şu şekilde oluşturun:
            Console.WriteLine("başarılı ödeme");
        }

        public override void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
            base.Dispose();
        }
    }
}