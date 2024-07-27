using RabbitMQ.Client;
using System.Text;

namespace TrashCollectionAPI.Data
{
    public class RabbitMQService
    {
        private readonly IConnectionFactory _connectionFactory;

        public RabbitMQService(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public void CreateQueue(string queueName)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queueName, false, false, false, null);
        }

        public void SendMessage(string queueName, string message)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.BasicPublish("", queueName, null, Encoding.UTF8.GetBytes(message));
        }
    }
}
