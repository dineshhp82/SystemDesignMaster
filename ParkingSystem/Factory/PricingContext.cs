using ParkingSystem.Enums;

namespace ParkingSystem.Factory
{
    public class PricingContext
    {
        public PricingContext(PriceType parkingFeeType, VehicleType vehicleType, DateTime exitTime)
        {
            PriceType = parkingFeeType;
            VehicleType = vehicleType;
            ExitTime = exitTime;
        }

        public PriceType PriceType { get; } = PriceType.Hourly;  // e.g. Mall, Airport
        public VehicleType VehicleType { get; }
        public DateTime ExitTime { get; }

        public decimal? RatePerHour { get; private set; }
        public decimal? FlatFee { get; private set; }
        public int? FlatHours { get; private set; }
        public decimal? DiscountPercent { get; private set; }

        public PricingContext WithRatePerHour(decimal rate)
        {
            RatePerHour = rate;
            return this;
        }

        public PricingContext WithFlatFee(decimal fee, int hours)
        {
            FlatFee = fee;
            FlatHours = hours;
            return this;
        }

        public PricingContext WithDiscount(decimal discount)
        {
            DiscountPercent = discount;
            return this;
        }

        // Finalize and validate
        public PricingContext BuildForStrategy(PriceType strategyType)
        {
            switch (strategyType)
            {
                case PriceType.Hourly:
                    if (!RatePerHour.HasValue || RatePerHour <= 0)
                        throw new InvalidOperationException("Hourly strategy requires a positive RatePerHour");
                    break;

                case PriceType.FlatPlusHourly:
                    if (!FlatFee.HasValue || FlatFee <= 0 || !FlatHours.HasValue || FlatHours < 0 || !RatePerHour.HasValue)
                        throw new InvalidOperationException("FlatPlusHourly requires FlatFee, FlatHours, and RatePerHour");
                    break;
                case PriceType.WeekendDiscount:
                    if (!RatePerHour.HasValue || !DiscountPercent.HasValue)
                        throw new InvalidOperationException("WeekendDiscount requires RatePerHour and DiscountPercent");
                    break;
                default:
                    throw new InvalidOperationException("Unknown strategy type");
            }

            return this;
        }
    }
}
