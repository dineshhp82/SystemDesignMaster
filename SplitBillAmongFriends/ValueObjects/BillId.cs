namespace SplitBillAmongFriends.ValueObjects
{
    public record BillId(Guid Value)
    {
        public static BillId New() => new(Guid.NewGuid());
    }
}
