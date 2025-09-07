using BookMeeting.DomainModel;

namespace BookMeeting
{
    // 🔹 Service orchestrates use cases (booking, cancelling, calendar).
    // Follows Dependency Inversion Principle (DIP) → depends on abstractions.
    public class BookingService
    {
        private readonly IFloorRepository _floorRepository;
        private readonly IBookingFactory _bookingFactory;

        public BookingService(IFloorRepository floorRepository, IBookingFactory bookingFactory)
        {
            _floorRepository = floorRepository;
            _bookingFactory = bookingFactory;
        }

        public Booking CreateBooking(Guid floorId, Guid roomId, User user, string title, DateTime start, DateTime end, int attendees)
        {
            var floor = _floorRepository.GetById(floorId);

            var room = floor.MeetingRooms.FirstOrDefault(r => r.Id == roomId)
                      ?? throw new ArgumentException("Invalid room.");

            return _bookingFactory.CreateBooking(room, user, title, start, end, attendees);
        }

        public IEnumerable<Booking> GetCalendar(Guid roomId, DateTime date)
        {
            return _floorRepository.GetAll()
                 .SelectMany(f => f.MeetingRooms)
                 .Where(r => r.Id == roomId)
                 .SelectMany(r => r.Bookings)
                 .Where(b => b.StartTime.Date == date.Date);
        }

        public void CancelBooking(Guid bookingId)
        {
            var booking = _floorRepository.GetAll()
                   .SelectMany(f => f.MeetingRooms)
                   .SelectMany(f => f.Bookings)
                   .FirstOrDefault(r => r.Id == bookingId);

            if (booking == null)
                throw new ArgumentException("Invalid booking.");

            booking.Cancel();
        }
    }
}
