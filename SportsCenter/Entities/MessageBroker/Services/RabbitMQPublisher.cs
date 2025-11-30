using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Entities.MessageBroker.Services
{
    public class RabbitMQPublisher<T> : IRabbitMQPublisher<T>
    {
        private readonly RabbitMQSettings _rabbitMqSetting;
        private readonly ILogger<RabbitMQPublisher<T>> _logger;

        public RabbitMQPublisher(IOptions<RabbitMQSettings> rabbitMqSetting, ILogger<RabbitMQPublisher<T>> logger)
        {
            _rabbitMqSetting = rabbitMqSetting.Value;
            _logger = logger;
        }

        public Task<ResponseModel> PublishMessageAsync(T message, string queueName)
        {
            var factory = new ConnectionFactory
            {
                HostName = _rabbitMqSetting.HostName,
                UserName = _rabbitMqSetting.UserName,
                Password = _rabbitMqSetting.Password
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

            channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
            _logger.LogInformation("Mesaj kuyruğa gönderildi. Queue: {QueueName}", queueName);

            return Task.FromResult(new ResponseModel { IsSuccess = true, Message = "Mesaj gönderildi" });
        }

        public async Task<ResponseModel> PublishMessageWithReplyAsync(T message, string queueName)
        {
            var factory = new ConnectionFactory
            {
                HostName = _rabbitMqSetting.HostName,
                UserName = _rabbitMqSetting.UserName,
                Password = _rabbitMqSetting.Password
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            var replyQueue = channel.QueueDeclare("", durable: false, exclusive: true, autoDelete: true).QueueName;

            var props = channel.CreateBasicProperties();
            var correlationId = Guid.NewGuid().ToString();
            props.CorrelationId = correlationId;
            props.ReplyTo = replyQueue;

            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
            var tcs = new TaskCompletionSource<ResponseModel>();

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                if (ea.BasicProperties?.CorrelationId == correlationId)
                {
                    var responseJson = Encoding.UTF8.GetString(ea.Body.ToArray());
                    var response = JsonConvert.DeserializeObject<ResponseModel>(responseJson);
                    tcs.TrySetResult(response ?? new ResponseModel { IsSuccess = false, Message = "Geçersiz yanıt" });
                }
            };
            channel.BasicConsume(queue: replyQueue, autoAck: true, consumer: consumer);

            channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: props, body: body);
            _logger.LogInformation("Mesaj gönderildi, yanıt bekleniyor. Queue: {QueueName}, CorrelationId: {CorrelationId}", queueName, correlationId);

            var completedTask = await Task.WhenAny(tcs.Task, Task.Delay(TimeSpan.FromSeconds(30)));

            if (completedTask == tcs.Task)
                return await tcs.Task;

            _logger.LogWarning("Yanıt zaman aşımına uğradı. Queue: {QueueName}, CorrelationId: {CorrelationId}", queueName, correlationId);
            return new ResponseModel { IsSuccess = false, Message = "Yanıt zaman aşımına uğradı" };
        }
    }
}
