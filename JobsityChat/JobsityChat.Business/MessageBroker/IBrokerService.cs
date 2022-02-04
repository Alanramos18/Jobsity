namespace JobsityChat.Business.MessageBroker
{
    public interface IBrokerService
    {
        void Publish(string message, string chatId, string roomName);
    }
}
