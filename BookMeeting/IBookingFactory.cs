using BookMeeting.DomainModel;

namespace BookMeeting
{
    // 🔹 Factory Pattern:
    // Encapsulates booking creation logic, instead of having service directly construct.
    // Improves testability and open/closed principle.
    public interface IBookingFactory
    {
        Booking CreateBooking(MeetingRoom room, User user, string title, DateTime start, DateTime end, int attendees);
    }
}
