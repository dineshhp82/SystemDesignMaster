namespace DiscountCoupon.Discount
{
    public class PercentageDiscountStrategy(decimal percentage) : IDiscountStrategy
    {
        public decimal Calculate(decimal baseAmount)
        {
            return (percentage / 100.0m) * baseAmount;
        }
    }
}
