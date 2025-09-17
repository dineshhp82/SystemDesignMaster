namespace PaymentReceipt.ValueObjects
{
    public record ReceiptId(Guid Value)
    {
        //Factory method
        /* public static ReceiptId New()
        {
            return new ReceiptId(Guid.NewGuid());
        }*/

        public static ReceiptId New() => new ReceiptId(Guid.NewGuid());
    }
}
