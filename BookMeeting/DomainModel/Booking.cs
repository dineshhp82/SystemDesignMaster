
namespace BookMeeting.DomainModel
{
    public class Booking : IEntity
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        public Guid MeetingRoomId { get; private set; }

        public Guid UserId { get; private set; }

        public string Title { get; private set; }

        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public int AttendeesCount { get; private set; }
        public BookingStatus Status { get; private set; }

        public Booking(Guid meetingRoomId, Guid userId, string title, DateTime startTime, DateTime endTime, int attendeesCount)
        {
            MeetingRoomId = meetingRoomId;
            UserId = userId;
            Title = title;
            StartTime = startTime;
            EndTime = endTime;
            AttendeesCount = attendeesCount;
            Status = BookingStatus.Booked;
        }

        public void Cancel()
        {
            if (Status == BookingStatus.Cancelled)
                throw new InvalidOperationException("Booking already cancelled.");

            Status = BookingStatus.Cancelled;
        }
    }
}