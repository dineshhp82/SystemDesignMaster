using SplitBillAmongFriends.ValueObjects;

namespace SplitBillAmongFriends.Strategies
{
    public class CustomSplitStrategy : ISplitStrategy
    {

        private readonly Dictionary<Guid, decimal> _customAmounts;

        public CustomSplitStrategy(Dictionary<Guid, decimal> customAmounts)
        {
            _customAmounts = customAmounts;
        }

        public IEnumerable<Share> Split(Bill bill)
        {
            return bill.Friends.Select(r => new Share(r, _customAmounts.ContainsKey(r.FriendId)
                ? _customAmounts[r.FriendId]
                : 0));
        }
    }
}
