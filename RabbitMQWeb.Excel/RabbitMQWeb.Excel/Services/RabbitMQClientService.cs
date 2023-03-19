using NuGet.Protocol.Plugins;
using RabbitMQ.Client;
using IConnection = RabbitMQ.Client.IConnection;
using IModel = RabbitMQ.Client.IModel;

namespace RabbitMQWeb.Excel.Services
{
    public class RabbitMQClientService
    {
        public static string ExchangeName = "ExcelDirectExchange";
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

            _channel.ExchangeDeclare(ExchangeName, type: "direct", true, false);

            _channel.QueueDeclare(QueueName, true, false, false, null);

            _channel.QueueBind(queue: QueueName, exchange: ExchangeName, routingKey: RoutingExcel);

            _logger.LogInformation("RabbitMQ ile bağlanti kuruldu");

            return _channel;
        }

    }
}
