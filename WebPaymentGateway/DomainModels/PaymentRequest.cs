namespace WebPaymentGateway.Models
{
    public class PaymentRequest(string sender, string reciver, decimal amount, string currency)
    {
        public string Sender { get; } = sender;

        public string Reciver { get; } = reciver;

        public decimal Amount { get; } = amount;

        public string Currency { get; } = currency;

    }
}
