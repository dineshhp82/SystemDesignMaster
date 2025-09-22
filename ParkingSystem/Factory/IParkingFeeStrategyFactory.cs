using ParkingSystem.Strategies;

namespace ParkingSystem.Factory
{
    public interface IParkingFeeStrategyFactory
    {
        IParkingFeeStrategy CreateStrategy(PricingContext context);
    }
}
