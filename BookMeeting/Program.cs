using BookMeeting.DomainModel;

namespace BookMeeting
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome Meeting Rooms");

            var user = new User("Alice", "alice@example.com");

            var floor = new Floor("First Floor");
            var room1 = new MeetingRoom("Room A", 10, floor.Id);

            floor.AddMeetingRoom(room1);

            var floorRepo = new InMemoryFloorRepository();
            floorRepo.Add(floor);

            var bookingFactory = new BookingFactory();
            var bookingService = new BookingService(floorRepo, bookingFactory);

            // Book a room
            var booking = bookingService.CreateBooking(
                floor.Id,
                room1.Id,
                user,
                "Project Meeting",
                DateTime.UtcNow.AddHours(1),
                DateTime.UtcNow.AddHours(2),
                5
            );

            Console.WriteLine($"Booking created: {booking.Title} [{booking.Status}]");

            // Cancel booking
            bookingService.CancelBooking(booking.Id);
            Console.WriteLine($"Booking cancelled: {booking.Status}");

            Console.ReadLine();

        }
    }
}
