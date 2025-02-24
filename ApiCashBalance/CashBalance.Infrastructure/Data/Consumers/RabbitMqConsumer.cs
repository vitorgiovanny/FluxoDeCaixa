using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashBalance.Infrastructure.Data.Consumers
{
    public class RabbitMqConsumer
    {
        private readonly string _path = "localhost";
        private readonly string _queueName = "created";

        public RabbitMqConsumer(string queueName)
        {
            _queueName = queueName;
        }

        public async Task ListenQueue()
        {
            var factory = new ConnectionFactory() { HostName = _path };
            var connection = await factory.CreateConnectionAsync();
            var channel = await connection.CreateChannelAsync(); //.CreateModel()

            await channel.QueueDeclareAsync(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var pedido = JsonConvert.DeserializeObject<Object>(message); //Replace Object

                // Chama a camada de aplicação para processar o pedido
                //_pedidoService.ProcessarPedido(pedido);

                // Confirma que a mensagem foi processada com sucesso
                await channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false);
            };

            await channel.BasicConsumeAsync(queue: _queueName, autoAck: false, consumer: consumer);
        }
    }
}
