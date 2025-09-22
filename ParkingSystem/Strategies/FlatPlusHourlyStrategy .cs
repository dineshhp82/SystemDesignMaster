using ParkingSystem.Entities;

namespace ParkingSystem.Strategies
{
    public class FlatPlusHourlyStrategy : IParkingFeeStrategy
    {
        private readonly decimal _flatFee;
        private readonly int _flatHours;
        private readonly decimal _ratePerHour;

        public FlatPlusHourlyStrategy(decimal flatFee, int flatHours, decimal ratePerHour)
        {
            _flatFee = flatFee;
            _flatHours = flatHours;
            _ratePerHour = ratePerHour;
        }

        public decimal CalculateFee(Ticket ticket)
        {
            if (ticket.ExitTime == null)
                throw new InvalidOperationException("Ticket still open.");

            var duration = ticket.ExitTime.Value - ticket.EntryTime;
            var totalHours = Math.Ceiling(duration.TotalHours);

            if (totalHours <= _flatHours)
                return _flatFee;

            return _flatFee + (decimal)(totalHours - _flatHours) * _ratePerHour;
        }
    }
}
