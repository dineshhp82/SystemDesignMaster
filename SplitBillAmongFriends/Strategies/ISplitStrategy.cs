using SplitBillAmongFriends.ValueObjects;

namespace SplitBillAmongFriends.Strategies
{
    public interface ISplitStrategy
    {
        IEnumerable<Share> Split(Bill bill);
    }
}
