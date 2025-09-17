namespace PaymentReceipt.DTOs
{
    public class PaymentResultItem
    {
        public string ReferenceId { get; init; }    // e.g., InvoiceId or OrderId
        public string ReferenceType { get; init; }  // "Invoice", "Order"
        public decimal Amount { get; init; }
        public string Currency { get; init; }
    }
}
