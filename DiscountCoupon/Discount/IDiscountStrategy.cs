namespace DiscountCoupon.Discount
{
    public interface IDiscountStrategy
    {
        decimal Calculate(decimal baseAmount);
    }
}
