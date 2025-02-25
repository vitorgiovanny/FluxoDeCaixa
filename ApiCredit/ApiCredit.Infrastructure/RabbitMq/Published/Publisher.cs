using ApiCredit.Domain.Interfaces;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCredit.Infrastructure.RabbitMq.Pushed
{
    public class Publisher : IHostedService, IRabbitMqPublisher
    {
        private readonly ConnectionFactory _factory;

        public Publisher()
        {
            _factory = new ConnectionFactory() { HostName = "localhost" };
        }

        public async Task PublicarMensagem(string fila, string mensagem)
        {
            var connection = await _factory.CreateConnectionAsync();
            var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: fila, durable: true, exclusive: false, autoDelete: false);

            var body = Encoding.UTF8.GetBytes(mensagem);
            await channel.BasicPublishAsync(exchange: "", routingKey: fila, body: body);

            Console.WriteLine($"Mensagem publicada na fila {fila}: {mensagem}");
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Publisher iniciado.");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Publisher parado.");
            return Task.CompletedTask;
        }
    }
}
