using ParkingSystem.Entities;

namespace ParkingSystem.Strategies
{
    public class HourlyRateStrategy : IParkingFeeStrategy
    {
        private readonly decimal _ratePerHour;

        public HourlyRateStrategy(decimal ratePerHour) => _ratePerHour = ratePerHour;

        public decimal CalculateFee(Ticket ticket)
        {
            if (ticket.ExitTime == null)
                throw new InvalidOperationException("Ticket still open.");

            var duration = ticket.ExitTime.Value - ticket.EntryTime;

            var hours = Math.Ceiling(duration.TotalHours);

            return (decimal)hours * _ratePerHour;
        }
    }
}
