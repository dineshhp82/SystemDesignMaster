using SplitBillAmongFriends.Strategies;
using SplitBillAmongFriends.ValueObjects;

namespace SplitBillAmongFriends.Service
{
    public class SplitService
    {
        private readonly ISplitStrategy _splitStrategy;

        public SplitService(ISplitStrategy splitStrategy)
        {
            _splitStrategy = splitStrategy;
        }

        public IEnumerable<Share> Execute(Bill bill)
        {
            return _splitStrategy.Split(bill);
        }
    }
}
