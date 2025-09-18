using SplitBillAmongFriends.ValueObjects;

namespace SplitBillAmongFriends.Strategies
{
    public class EqualSplitStrategy : ISplitStrategy
    {
        public IEnumerable<Share> Split(Bill bill)
        {
            var perHead = bill.TotalAmount / bill.Friends.Count;

            return bill.Friends.Select(f=>new Share(f, perHead));  
        }
    }
}
