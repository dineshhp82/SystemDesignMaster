namespace PaymentReceipt.Events
{
    public interface IEventPublisher
    {
        void Publish<T>(T @event);
    }
}
