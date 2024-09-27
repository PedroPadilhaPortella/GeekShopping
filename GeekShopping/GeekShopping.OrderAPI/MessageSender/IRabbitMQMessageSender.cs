using GeekShopping.MessageBus;

namespace GeekShopping.OrderAPI.MessageSender
{
    public interface IRabbitMQMessageSender
    {
        void SendMessage(BaseMessage baseMessage, string queueName);
    }
}
