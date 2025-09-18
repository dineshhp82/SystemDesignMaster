namespace PaymentReceipt.Events
{
    public class EventPublisher : IEventPublisher
    {
        public void Publish<ReceiptCreatedEvent>(ReceiptCreatedEvent receiptCreated)
        {
            Console.WriteLine($"Event Publised :  {receiptCreated}");
        }
    }
}
