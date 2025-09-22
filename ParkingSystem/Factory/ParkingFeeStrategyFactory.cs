using ParkingSystem.Enums;
using ParkingSystem.Strategies;

namespace ParkingSystem.Factory
{
    public class ParkingFeeStrategyFactory : IParkingFeeStrategyFactory
    {
        public  IParkingFeeStrategy CreateStrategy(PricingContext context)
        {
            return context.PriceType switch
            {
                PriceType.FlatPlusHourly => new FlatPlusHourlyStrategy(context.FlatFee.Value, context.FlatHours.Value, context.RatePerHour.Value),
                PriceType.Hourly => new HourlyRateStrategy(context.RatePerHour.Value),
                PriceType.WeekendDiscount => new WeekendDiscountStrategy(context.RatePerHour.Value, context.DiscountPercent.Value),
                _ => throw new ArgumentException("Invalid Parking Fee Type"),
            };
        }
    }
}
