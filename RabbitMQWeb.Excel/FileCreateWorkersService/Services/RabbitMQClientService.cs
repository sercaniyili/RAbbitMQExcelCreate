using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCreateWorkersService.Services
{
    public class RabbitMQClientService
    {

        public static string RoutingExcel = "excel-route-file";
        public static string QueueName = "queue-excel-file";

        private readonly IConnection _connection;
        private readonly ILogger<RabbitMQClientService> _logger;

        public RabbitMQClientService(ILogger<RabbitMQClientService> logger)
        {
            _logger = logger;
        }

        public IModel Connect()
        {
            var connectionFactory = new ConnectionFactory()
            {
                HostName = "localhost",
                VirtualHost = "/",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };

            var connection = connectionFactory.CreateConnection();

            var _channel = connection.CreateModel();

            _logger.LogInformation("RabbitMQ ile bağlanti kuruldu");

            return _channel;
        }

    }
}
