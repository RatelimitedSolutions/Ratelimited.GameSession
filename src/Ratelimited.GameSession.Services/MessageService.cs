using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ratelimited.GameSession.Services
{

    public class MessageService : RabbitMessageBroker, IMessageBroker
    {
        public MessageService(string hostName)
        {
            factory = new ConnectionFactory() { HostName = hostName };
            _connection = factory.CreateConnection();
        }

        public (IModel, EventingBasicConsumer) ConsumNewInstanceRequests()
        {
            var channel = CreateChannelModel("NewInstance");
            var consumer = CreateChannelConsumer(channel, "NewInstance");
            return (channel, consumer);
        }

        public void CreateNewInstance(CreateNewInstanceRequest createNewInstanceRequest)
        {
            Send("NewInstance", createNewInstanceRequest);
        }
    }

    public class RabbitMessageBroker
    {
        internal ConnectionFactory factory;
        internal IConnection _connection;

        internal void Send(string queueName, object data)
        {
            var channel = CreateChannelModel(queueName);

            channel.BasicPublish(
                exchange: "",
                routingKey: queueName,
                basicProperties: null,
                body: formatToBody(data));
        }

        private byte[] formatToBody(object data)
        {
            string jsonString = JsonConvert.SerializeObject(data);
            return Encoding.UTF8.GetBytes(jsonString);
        }


        internal EventingBasicConsumer CreateChannelConsumer(IModel channel, string queue)
        {
            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            var consumer = new EventingBasicConsumer(channel);

            channel.BasicConsume(queue: queue,
                autoAck: false,
                consumer: consumer);

            return consumer;
        }

        internal IModel CreateChannelModel(string name)
        {
            var channel = _connection.CreateModel();

            channel.QueueDeclare(
                queue: name,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            return channel;
        }
    }

    public interface IMessageBroker
    {
        void CreateNewInstance(CreateNewInstanceRequest createNewInstanceRequest);
        (IModel, EventingBasicConsumer) ConsumNewInstanceRequests();
    }

    public class CreateNewInstanceRequest
    {
        public ulong GuildId { get; set; }
        public string GuildName { get; set; }
        public ulong ChannelId { get; set; }
        public List<ulong> ContextMessages { get; set; } = new List<ulong>();
    }
}
