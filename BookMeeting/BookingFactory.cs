using BookMeeting.DomainModel;

namespace BookMeeting
{
    public class BookingFactory : IBookingFactory
    {
        public Booking CreateBooking(MeetingRoom room, User user, string title, DateTime start, DateTime end, int attendees)
        {
            if(attendees > room.Capacity)
                throw new InvalidOperationException("Room capacity exceeded.");

            if (!room.IsAvailable(start, end))
                throw new InvalidOperationException("Room is not available in the given timeslot.");


            var booking = new Booking(room.Id, user.Id, title, start, end, attendees);
            room.AddBooking(booking);

            return booking;
        }
    }
}
