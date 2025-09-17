namespace PaymentReceipt.ValueObjects
{
    public record PaymentItem(string ReferenceId, string ReferenceType, Money Amount)
    {
        public void Deconstruct(out string referenceId, out string referenceType, out Money amount)
        {
            referenceId = ReferenceId;
            referenceType = ReferenceType;
            amount = Amount;
        }
    }
}
