using EStockMarketStockService.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EStockMarketStockService.Message.Receive
{
    public class DeleteStockRabbitMqService : BackgroundService
    {
        private IModel _channel;
        private IConnection _connection;
        private readonly IStockRepository _stockRepository;
        private readonly IConfiguration _configuration;
        private readonly string _connectionUri;
        private readonly string _queueName;

        public DeleteStockRabbitMqService(IStockRepository stockRepository, IConfiguration configuration)
        {
            _configuration = configuration;
            _stockRepository = stockRepository;

            var configSection = _configuration.GetSection("RabbitMq");

            _connectionUri = configSection.GetSection("ConnectionUri").Value;
            _queueName = configSection.GetSection("QueueName").Value;

            InitializeRabbitMqListener();
        }

        private void InitializeRabbitMqListener()
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri(_connectionUri)
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                var deleteCompanyCode = JsonConvert.DeserializeObject<string>(content);

                await HandleMessage(deleteCompanyCode);

                _channel.BasicAck(ea.DeliveryTag, false);
            };

            _channel.BasicConsume(_queueName, false, consumer);

            return Task.CompletedTask;
        }

        private async Task HandleMessage(string companyCode)
        {
            await _stockRepository.DeleteStockbyCompanyCodeAsync(companyCode);
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
