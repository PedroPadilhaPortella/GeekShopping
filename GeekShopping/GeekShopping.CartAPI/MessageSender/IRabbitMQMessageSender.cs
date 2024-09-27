using GeekShopping.MessageBus;

namespace GeekShopping.CartAPI.MessageSender
{
    public interface IRabbitMQMessageSender
    {
        void SendMessage(BaseMessage baseMessage, string queueName);
    }
}
