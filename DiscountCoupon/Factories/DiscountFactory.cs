using DiscountCoupon.Discount;

namespace DiscountCoupon.Factories
{
    public class DiscountFactory
    {
        public static IDiscountStrategy GetDiscountStrategy(StrategyType strategyType, decimal value, decimal cap = 0)
        {
            return strategyType switch
            {
                StrategyType.FLAT => new FlatDiscountStrategy(value),
                StrategyType.PERCENT => new PercentageDiscountStrategy(value),
                StrategyType.PERCENT_WITH_CAP => new PercentageWithCapStrategy(value, cap),
                _ => throw new Exception($"Invalid strategy type {strategyType}"),
            };
        }
    }
}
