using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Hosting;
using CashBalance.Domain.RabbitMq;
using Microsoft.Extensions.Options;
using CashBalance.Interfaces;
using CashBalance.Domain.Model;
using CashBalance.Domain.Entities;

namespace CashBalance.Infrastructure.Data.Message
{
    public class RabbitMqConsumer : BackgroundService
    {
        private readonly string _path;
        private readonly List<string> _queueName;
        private readonly IServiceScopeFactory _servicesScopeFactory;
        private IConnection _connection;
        private IChannel _channel;
        private const string BALANCE_DEBIT = "balance_debit";
        public RabbitMqConsumer(IOptions<RabbitMqSettings> options, IServiceScopeFactory servicesScopeFactory)
        {
            _path = options.Value.HostName;
            _queueName = options.Value.QueueName;
            _servicesScopeFactory = servicesScopeFactory;
        }

        private async Task Connect()
        {
            var factory = new ConnectionFactory() { HostName = _path, ConsumerDispatchConcurrency = 1 };

            _connection = await factory.CreateConnectionAsync();
            _channel = await _connection.CreateChannelAsync();

            foreach (var _queue in _queueName) {
                await _channel.QueueDeclareAsync(queue: _queue, durable: true, exclusive: false, autoDelete: false, arguments: null);
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Connect();

            foreach (var _queue in _queueName)
            {
                var consumer = new AsyncEventingBasicConsumer(_channel);
                consumer.ReceivedAsync += async (model, ea) =>
                {

                    using (var scope = _servicesScopeFactory.CreateScope())
                    {
                        var _services = scope.ServiceProvider.GetRequiredService<IExtractServices>();

                        try
                        {
                            var body = ea.Body.ToArray();
                            var message = Encoding.UTF8.GetString(body);
                            var response = JsonConvert.DeserializeObject<ExtractModelSubscribe>(message);

                            if (response != null)
                            {
                                if (_queue == BALANCE_DEBIT) response.Cash = response.Cash * (-1);

                                await _services.CreateExtract(new Extract() { Amount = response.Cash, IdCash = response.IdCash, IdCashier = response.IdCashier, Description = response.Description, Register = DateTime.UtcNow });
                            
                            }

                            await _channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false);
                            //await Task.Delay(Timeout.Infinite, stoppingToken); 

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Erro ao processar mensagem: {ex.Message}");
                        }
                    };
                };

                await _channel.BasicConsumeAsync(queue: _queue, autoAck: false, consumer: consumer);
            }
	    await Task.Delay(Timeout.Infinite, stoppingToken); 
        }

        public override void Dispose()
        {
            _channel?.CloseAsync();
            _connection?.CloseAsync();
            base.Dispose();
        }
    }
}
