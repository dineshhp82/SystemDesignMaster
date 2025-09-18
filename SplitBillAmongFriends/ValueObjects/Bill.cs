namespace SplitBillAmongFriends.ValueObjects
{
    public class Bill
    {
        public BillId BillId { get; }

        public decimal TotalAmount { get; }

        private readonly List<Friend> _friends = new();

        private Bill(BillId billId, decimal totalAmount)
        {
            BillId = billId;
            TotalAmount = totalAmount;
        }

        public IReadOnlyList<Friend> Friends => _friends.AsReadOnly();

        public void AddFriend(Friend friend)
        {
            if (friend == null)
                throw new ArgumentNullException(nameof(friend));
            if (_friends.Any(f => f.FriendId == friend.FriendId))
                throw new InvalidOperationException("Friend is already added to the bill.");
            _friends.Add(friend);
        }

        public static Bill Create(BillId billId, decimal totalAmount)
        {
            if (totalAmount <= 0)
                throw new ArgumentOutOfRangeException(nameof(totalAmount), "Total amount must be greater than zero.");

            return new Bill(billId, totalAmount);
        }
    }
}
