using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.MessageBroker.Services
{
    public interface IRabbitMQPublisher<T>
    {
        Task<ResponseModel> PublishMessageAsync(T message, string queueName);
        Task<ResponseModel> PublishMessageWithReplyAsync(T message, string queueName);
    }
}
