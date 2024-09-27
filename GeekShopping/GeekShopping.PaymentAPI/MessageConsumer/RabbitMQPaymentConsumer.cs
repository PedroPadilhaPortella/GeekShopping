using GeekShopping.PaymentAPI.Messages;
using GeekShopping.PaymentAPI.MessageSender;
using GeekShopping.PaymentProcessor;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace GeekShopping.PaymentAPI.MessageConsumer
{
    public class RabbitMQPaymentConsumer : BackgroundService
    {
        private IRabbitMQMessageSender _rabbitMQMessageSender;
        private readonly IProcessPayment _processPayment;
        private IConnection _connection;
        private IModel _channel;

        public RabbitMQPaymentConsumer(
            IProcessPayment processPayment,
            IRabbitMQMessageSender rabbitMQMessageSender
        )
        {
            _processPayment = processPayment;
            _rabbitMQMessageSender = rabbitMQMessageSender;

            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "orderpaymentprocessqueue", false, false, false, arguments: null);
        }


        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (chanel, evt) =>
            {
                var content = Encoding.UTF8.GetString(evt.Body.ToArray());
                PaymentMessage paymentMessage = JsonSerializer.Deserialize<PaymentMessage>(content);
                ProcessPayment(paymentMessage).GetAwaiter().GetResult();
                _channel.BasicAck(evt.DeliveryTag, false);
            };

            _channel.BasicConsume("orderpaymentprocessqueue", false, consumer);
            return Task.CompletedTask;
        }

        private Task ProcessPayment(PaymentMessage paymentMessage)
        {
            var result = _processPayment.PaymentProcessor();

            PaymentResultMessage paymentResultMessage = new() {
                Status = result,
                OrderId = paymentMessage.OrderId,
                Email = paymentMessage.Email
            };

            try {
                _rabbitMQMessageSender.SendMessage(paymentResultMessage, "orderpaymentresultqueue");
            }
            catch (Exception) {
                //Log
                throw;
            }

            return Task.CompletedTask;
        }
    }
}
