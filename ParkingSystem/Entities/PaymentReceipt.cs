namespace ParkingSystem.Entities
{
    public class PaymentReceipt
    {
        public Guid ReceiptId { get; } = Guid.NewGuid();
        public Guid TicketId { get; }
        public decimal Amount { get; }
        public DateTime PaidAt { get; } = DateTime.UtcNow;

        public PaymentReceipt(Guid ticketId, decimal amount)
        {
            TicketId = ticketId;
            Amount = amount;
        }

        public override string ToString()
        {
            return $"ReceiptId: {ReceiptId}, TicketId: {TicketId}, Amount: {Amount}, PaidAt: {PaidAt}";
        }   
    }
}
