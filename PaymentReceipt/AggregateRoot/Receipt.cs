using PaymentReceipt.Events;
using PaymentReceipt.ValueObjects;

namespace PaymentReceipt.AggregateRoot
{
    public class Receipt
    {
        public ReceiptId ReceiptId { get; init; }
        public DateTimeOffset IssuedAt { get; init; }
        public Money Total { get; private set; }
        public Payer Payer { get; init; }
        public PaymentMethod PaymentMethod { get; private set; }
        public ReceiptStatus Status { get; private set; }

        public readonly List<PaymentItem> _paymentItems = new();
        public IReadOnlyList<PaymentItem> PaymentItems => _paymentItems.AsReadOnly();

        // Private ctor for factory usage & ORM
        private Receipt() { }

        public static Receipt Create(Payer payer, IEnumerable<PaymentItem> paymentItems, PaymentMethod paymentMethod)
        {
            if (payer == null)
                throw new ArgumentNullException(nameof(payer));

            if (paymentItems is null || !paymentItems.Any())
                throw new ArgumentNullException(nameof(paymentItems));

            var currency = paymentItems.First().Amount.Currency;

            if (paymentItems.Any(pi => pi.Amount.Currency != currency))
                throw new ArgumentException("All payment items must have the same currency.", nameof(paymentItems));


            var receipt = new Receipt
            {
                ReceiptId = ReceiptId.New(),
                IssuedAt = DateTimeOffset.UtcNow,
                Payer = payer,
                PaymentMethod = paymentMethod,
                Status = ReceiptStatus.Issued,
                Total = new Money(0, "USD") // Default currency
            };

            foreach (var item in paymentItems)
            {
                receipt._paymentItems.Add(item);
            }

            receipt.Total = receipt.CalculateTotal();
            return receipt;
        }


        public void MarkRefunded()
        {
            if (Status == ReceiptStatus.Refunded)
                throw new InvalidOperationException("Receipt is already refunded.");

            Status = ReceiptStatus.Refunded;
        }

        public Money CalculateTotal()
        {
            if (!_paymentItems.Any())
                return new Money(0, "USD"); // Default currency 

            var currency = _paymentItems.First().Amount.Currency;

            var totalAmount = _paymentItems
                .Where(pi => pi.Amount.Currency == currency)
                .Sum(pi => pi.Amount.Amount);

            return new Money(totalAmount, currency);
        }

        // Domain event generation hook example (not implemented here)
        public ReceiptCreatedEvent ToCreatedEvent() => new ReceiptCreatedEvent(ReceiptId, IssuedAt, Payer, _paymentItems.ToList(), Total, PaymentMethod);
    }
}
