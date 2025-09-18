namespace SplitBillAmongFriends.ValueObjects
{
    public class Friend
    {
        public Guid FriendId { get; }

        public string Name { get; }

        private Friend(string name, Guid friendId)
        {
            Name = name;
            FriendId = friendId;
        }

        public static Friend Create(Guid friendId, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Friend name is required", nameof(name));
            return new Friend(name, friendId);
        }
    }
}
