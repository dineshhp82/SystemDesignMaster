namespace PaymentReceipt.ValueObjects
{
    public sealed record Money
    {
        public decimal Amount { get; init; }
        public string Currency { get; init; } // ISO code like "USD", "INR"

        public Money(decimal amount, string currency)
        {
            if (amount < 0)
                throw new ArgumentException("Amount cannot be negative", nameof(amount));
            if (string.IsNullOrWhiteSpace(currency))
                throw new ArgumentException("Currency cannot be null or empty", nameof(currency));

            Amount = decimal.Round(amount, 2); // domain-specific rounding;
            Currency = currency.ToUpperInvariant();
        }

        public Money Add(Money other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));
            if (Currency != other.Currency)
                throw new InvalidOperationException("Cannot add amounts with different currencies");
            return new Money(Amount + other.Amount, Currency);
        }

        public Money Multiply(int factor) => new Money(Amount * factor, Currency);
    }
}