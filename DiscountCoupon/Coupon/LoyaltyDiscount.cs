using DiscountCoupon.Factories;
using DiscountCoupon.Models;

namespace DiscountCoupon.Coupon
{
    internal class LoyaltyDiscount(decimal percent) : Coupon
    {
        public override string CouponName => "Loyalty Discount " + (int)percent + "% off";

        public override decimal GetDiscount(Cart cart)
        {
            return DiscountFactory.GetDiscountStrategy(StrategyType.PERCENT, percent)
                .Calculate(cart.OriginalTotal);
        }

        public override bool IsApplicable(Cart cart)
        {
            return cart.LoyaltyMember;
        }
    }
}
