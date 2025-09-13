namespace DiscountCoupon.Discount
{
    public class PercentageWithCapStrategy(decimal percent, decimal cap) : IDiscountStrategy
    {
        public decimal Calculate(decimal baseAmount)
        {
            decimal disc = (percent / 100.0m) * baseAmount;
            return disc > cap ? cap : disc;
        }
    }
}
