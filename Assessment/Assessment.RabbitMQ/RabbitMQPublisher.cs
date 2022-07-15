using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Assessment.RabbitMQ
{
    public class RabbitMQPublisher
    {
        private readonly RabbitMQClientService _rabbitMQClientService;

        public RabbitMQPublisher(RabbitMQClientService rabbitMQClientService)
        {
            _rabbitMQClientService = rabbitMQClientService;
        }

        public void Publish(CustomerImageCreatedEvent productImageCreatedEvent)
        {
            var channel = _rabbitMQClientService.Connect();

            var bodyString = JsonSerializer.Serialize(productImageCreatedEvent);

            var bodyBytes = Encoding.UTF8.GetBytes(bodyString);

            var props = channel.CreateBasicProperties();
            props.Persistent = true;

            channel.BasicPublish(exchange: RabbitMQClientService.ExchangeName,
                                 routingKey: RabbitMQClientService.RoutingWatermark,
                                 basicProperties: props,
                                 body: bodyBytes
                                );
        }
    }
}
