namespace DiscountCoupon.Discount
{
    public class FlatDiscountStrategy(decimal flatAmount) : IDiscountStrategy
    {
        public decimal Calculate(decimal baseAmount)
        {
            return Math.Min(baseAmount, flatAmount);
        }
    }
}
