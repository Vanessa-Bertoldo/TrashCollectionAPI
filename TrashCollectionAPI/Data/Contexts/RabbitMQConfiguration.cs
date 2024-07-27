using RabbitMQ.Client;
using TrashCollectionAPI.Models;

namespace TrashCollectionAPI.Data.Contexts
{
    public static class RabbitMQConfiguration
    {
        public static void ConfigureRabbitMQ(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConnectionFactory>(sp =>
            {
                var rabbitMQConfig = configuration.GetSection("RabbitMQ").Get<RabbitMQConfigModel>();
                var factory = new ConnectionFactory
                {
                    HostName = rabbitMQConfig.HostName,
                    Port = rabbitMQConfig.Port,
                    UserName = rabbitMQConfig.UserName,
                    Password = rabbitMQConfig.Password
                };
                return factory;
            });
        }
    }
}
