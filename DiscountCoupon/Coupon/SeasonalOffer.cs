using DiscountCoupon.Factories;
using DiscountCoupon.Models;

namespace DiscountCoupon.Coupon
{
    public class SeasonalOffer(string category, decimal percent) : Coupon
    {
        public override string CouponName => $"Seasonal Offer {(int)percent} % off {category}";

        public override decimal GetDiscount(Cart cart)
        {
            decimal subTotal = cart.CartItems.Where(r => r.Product.Category == category).Sum(x => x.GetAmount());

            var percentStartgy = DiscountFactory.GetDiscountStrategy(StrategyType.PERCENT, percent);

            return percentStartgy.Calculate(subTotal);
        }

        public override bool IsApplicable(Cart cart) => cart.CartItems.Any(r => r.Product.Category.Equals(category));
    }
}
