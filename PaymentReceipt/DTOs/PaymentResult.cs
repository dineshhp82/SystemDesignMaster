namespace PaymentReceipt.DTOs
{
    public class PaymentResult
    {
        public string PayerName { get; init; }
        public string PayerEmail { get; init; }
        public string PayerTimeZone { get; init; }
        public PaymentMethod Method { get; init; }
        public List<PaymentResultItem> Items { get; init; } = new();
    }
}
