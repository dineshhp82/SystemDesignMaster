using ParkingSystem.Entities;

namespace ParkingSystem.Strategies
{
    public class WeekendDiscountStrategy : IParkingFeeStrategy
    {
        private readonly decimal _ratePerHour;
        private readonly decimal _discountPercent;

        public WeekendDiscountStrategy(decimal ratePerHour, decimal discountPercent)
        {
            _ratePerHour = ratePerHour;
            _discountPercent = discountPercent;
        }

        public decimal CalculateFee(Ticket ticket)
        {
            if (ticket.ExitTime == null)
                throw new InvalidOperationException("Ticket still open.");

            var duration = ticket.ExitTime.Value - ticket.EntryTime;
            var hours = Math.Ceiling(duration.TotalHours);
            var fee = (decimal)hours * _ratePerHour;

            // Apply discount if weekend
            if (ticket.ExitTime.Value.DayOfWeek == DayOfWeek.Saturday ||
                ticket.ExitTime.Value.DayOfWeek == DayOfWeek.Sunday)
            {
                fee -= fee * (_discountPercent / 100);
            }

            return fee;
        }
    }
}
