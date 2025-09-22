using ParkingSystem.Strategies;

namespace ParkingSystem.Entities
{
    public class Ticket
    {
        public Guid TicketId { get; } = Guid.NewGuid();
        public Vehicle Vehicle { get; }
        public ParkingSpot Spot { get; }
        public DateTime EntryTime { get; } = DateTime.UtcNow;
        public DateTime? ExitTime { get; private set; }

        private Ticket(Vehicle vehicle, ParkingSpot spot)
        {
            Vehicle = vehicle;
            Spot = spot;
        }

        public static Ticket Create(Vehicle vehicle, ParkingSpot spot)
        {
            if (vehicle == null)
                throw new ArgumentNullException(nameof(vehicle));
            if (spot == null)
                throw new ArgumentNullException(nameof(spot));
           
            return new Ticket(vehicle, spot);
        }   

        public void CloseTicket()
        {
            ExitTime = DateTime.UtcNow;
            Spot.VacateSpot();
        }

        public decimal CalculateFare(IParkingFeeStrategy  parkingFeeStrategy)
        {
            return parkingFeeStrategy.CalculateFee(this);
        }

    }
}
