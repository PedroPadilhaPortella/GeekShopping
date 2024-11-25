using GeekShopping.Email.Messages;
using GeekShopping.Email.Repository;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.Json;

namespace GeekShopping.Email.MessageConsumer
{
  public class RabbitMQPaymentConsumer : BackgroundService
  {
    private readonly EmailRepository _repository;
    private readonly IConnection _connection;
    private readonly IModel _channel;

    private const string ExchangeName = "DirectPaymentUpdateExchange";
    private const string PaymentEmailUpdateQueueName = "PaymentEmailUpdateQueueName";

    public RabbitMQPaymentConsumer(EmailRepository repository)
    {
      _repository = repository;
      var factory = new ConnectionFactory
      {
        HostName = "localhost",
        UserName = "guest",
        Password = "guest"
      };
      _connection = factory.CreateConnection();
      _channel = _connection.CreateModel();

      _channel.ExchangeDeclare(ExchangeName, ExchangeType.Direct);
      _channel.QueueDeclare(PaymentEmailUpdateQueueName, false, false, false, null);
      _channel.QueueBind(PaymentEmailUpdateQueueName, ExchangeName, "PaymentEmail");
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
      stoppingToken.ThrowIfCancellationRequested();
      var consumer = new EventingBasicConsumer(_channel);
      consumer.Received += (chanel, evt) =>
      {
        var content = Encoding.UTF8.GetString(evt.Body.ToArray());
        PaymentResultDTO paymentResultDTO = JsonSerializer.Deserialize<PaymentResultDTO>(content);
        ProcessLogs(paymentResultDTO).GetAwaiter().GetResult();
        _channel.BasicAck(evt.DeliveryTag, false);
      };
      _channel.BasicConsume(PaymentEmailUpdateQueueName, false, consumer);
      return Task.CompletedTask;
    }

    private async Task ProcessLogs(PaymentResultDTO paymentResultDTO)
    {
      await _repository.LogEmail(paymentResultDTO);
      this.SendEmail(paymentResultDTO);
    }

    private void SendEmail(PaymentResultDTO paymentResultDTO)
    {
      SmtpClient smtpClient = new SmtpClient("sandbox.smtp.mailtrap.io", 2525)
      {
        Credentials = new NetworkCredential("23427d12493947", "1d387ba2cd6e0c"),
        EnableSsl = true
      };

      MailMessage mail = new MailMessage
      {
        From = new MailAddress("geekshopping@gmail.com"),
        Subject = "GeekShopping - Order Notification",
        Body = $"Order - {paymentResultDTO.OrderId} has been created successfully.",
        IsBodyHtml = false
      };

      mail.To.Add(paymentResultDTO.Email);

      smtpClient.Send(mail);
    }
  }
}
