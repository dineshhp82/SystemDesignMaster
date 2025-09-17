using PaymentReceipt.ValueObjects;

namespace PaymentReceipt.Events
{
    public record ReceiptCreatedEvent(ReceiptId Id, DateTimeOffset IssuedAt, Payer Payer, List<PaymentItem> Items, Money Total, PaymentMethod PaymentMethod);
}
