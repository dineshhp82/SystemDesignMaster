namespace BookMeeting.DomainModel
{
    public class Floor : IEntity
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; private set; }

        private readonly List<MeetingRoom> _meetingRooms = new();
        public IReadOnlyList<MeetingRoom> MeetingRooms => _meetingRooms;

        public Floor(string name)
        {
            Name = name;
        }

        public void AddMeetingRoom(MeetingRoom room)
        {
            _meetingRooms.Add(room);
        }
    }
}
