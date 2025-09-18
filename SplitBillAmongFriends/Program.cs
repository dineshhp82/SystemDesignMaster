using SplitBillAmongFriends.Factory;
using SplitBillAmongFriends.Service;
using SplitBillAmongFriends.ValueObjects;

namespace SplitBillAmongFriends
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Split Payment");

            var friends = new List<Friend>
            {
                Friend.Create(Guid.NewGuid(), "Alice"),
                Friend.Create(Guid.NewGuid(), "Bob"),
                Friend.Create(Guid.NewGuid(), "Charlie"),
            };

            var bill = Bill.Create(BillId.New(), 1800);

            foreach (var friend in friends)
                bill.AddFriend(friend);

            var split = new SplitService(SplitFactory.CreateSplitStrategy(SplitType.Equal));

            var equalShares = split.Execute(bill);

            Console.WriteLine("Equal Split:");

            foreach (var share in equalShares)
                Console.WriteLine($"{share.Friend.Name} owes {share.AmountOwed}");


            // Custom Split
            var customAmounts = new Dictionary<Guid, decimal>
             {
                    { friends[0].FriendId, 1000 },
                    { friends[1].FriendId, 500 },
                    { friends[2].FriendId, 1500 }
             };

            var customSplit = new SplitService(SplitFactory.CreateSplitStrategy(SplitType.Custom, customAmounts));

            var customShares = customSplit.Execute(bill);

            Console.WriteLine("\nCustom Split:");
            foreach (var share in customShares)
                Console.WriteLine($"{share.Friend.Name} owes {share.AmountOwed}");


        }
    }
}
