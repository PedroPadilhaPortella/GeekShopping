using GeekShopping.OrderAPI.Messages;
using GeekShopping.OrderAPI.MessageSender;
using GeekShopping.OrderAPI.Models;
using GeekShopping.OrderAPI.Repository;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace GeekShopping.OrderAPI.MessageConsumer
{
    public class RabbitMQCheckoutConsumer : BackgroundService
    {
        private readonly OrderRepository _repository;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private IRabbitMQMessageSender _rabbitMQMessageSender;

        public RabbitMQCheckoutConsumer(
            OrderRepository repository, 
            IRabbitMQMessageSender rabbitMQMessageSender
        )
        {
            _repository = repository;
            _rabbitMQMessageSender = rabbitMQMessageSender;

            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "checkoutqueue", false, false, false, arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (chanel, evt) =>
            {
                var content = Encoding.UTF8.GetString(evt.Body.ToArray());
                CheckoutHeaderDTO checkoutHeaderDTO = JsonSerializer.Deserialize<CheckoutHeaderDTO>(content);
                ProcessOrder(checkoutHeaderDTO).GetAwaiter().GetResult();
                _channel.BasicAck(evt.DeliveryTag, false);
            };
            _channel.BasicConsume("checkoutqueue", false, consumer);
            return Task.CompletedTask;
        }

        private async Task ProcessOrder(CheckoutHeaderDTO checkoutHeaderDTO)
        {
            OrderHeader order = new()
            {
                UserId = checkoutHeaderDTO.UserId,
                FirstName = checkoutHeaderDTO.FirstName,
                LastName = checkoutHeaderDTO.LastName,
                OrderDetails = new List<OrderDetail>(),
                CardNumber = checkoutHeaderDTO.CardNumber,
                CouponCode = checkoutHeaderDTO.CouponCode,
                CVV = checkoutHeaderDTO.CVV,
                DiscountAmount = checkoutHeaderDTO.DiscountAmount,
                Email = checkoutHeaderDTO.Email,
                ExpireDate = checkoutHeaderDTO.ExpireDate,
                OrderTime = DateTime.Now,
                PurchaseAmount = checkoutHeaderDTO.PurchaseAmount,
                PaymentStatus = false,
                Phone = checkoutHeaderDTO.Phone,
                DateTime = checkoutHeaderDTO.DateTime
            };

            foreach (var checkoutDetail in checkoutHeaderDTO.CartDetails)
            {
                OrderDetail detail = new()
                {
                    ProductId = checkoutDetail.ProductId,
                    ProductName = checkoutDetail.Product.Name,
                    Price = checkoutDetail.Product.Price,
                    Count = checkoutDetail.Count,
                };
                order.CartTotalItems += checkoutDetail.Count;
                order.OrderDetails.Add(detail);
            }

            await _repository.AddOrder(order);

            PaymentDTO payment = new() {
                Name = order.FirstName + " " + order.LastName,
                CardNumber = order.CardNumber,
                CVV = order.CVV,
                ExpireDate = order.ExpireDate,
                OrderId = order.Id,
                PurchaseAmount = order.PurchaseAmount,
                Email = order.Email
            };

            try {
                _rabbitMQMessageSender.SendMessage(payment, "orderpaymentprocessqueue");
            }
            catch (Exception) {
                //Log
                throw;
            }
        }
    }
}
