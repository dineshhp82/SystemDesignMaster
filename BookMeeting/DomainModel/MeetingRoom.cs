
namespace BookMeeting.DomainModel
{
    public class MeetingRoom : IEntity
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; private set; }
        public int Capacity { get; private set; }
        public Guid FloorId { get; private set; }

        private readonly List<Booking> _bookings = new();
        public IReadOnlyList<Booking> Bookings => _bookings;

        public MeetingRoom(string name, int capacity, Guid floorId)
        {
            Name = name;
            Capacity = capacity;
            FloorId = floorId;
        }

        public void AddBooking(Booking booking)
        {
            _bookings.Add(booking);
        }

        public bool IsAvailable(DateTime start, DateTime end)
        {
            // 🔹 Single Responsibility Principle (SRP):
            // Room only checks availability; booking creation is handled elsewhere.
            return !_bookings.Any(b =>b.Status == BookingStatus.Booked &&
                start < b.EndTime && end > b.StartTime);
        }
    }
}