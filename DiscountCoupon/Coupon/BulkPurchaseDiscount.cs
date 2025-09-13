using DiscountCoupon.Factories;
using DiscountCoupon.Models;

namespace DiscountCoupon.Coupon
{
    internal class BulkPurchaseDiscount(decimal threshold, decimal flatoff) : Coupon
    {
        public override string CouponName => $"Bulk Purchase Discount {flatoff} off over {threshold}";

        public override decimal GetDiscount(Cart cart)
        {
            var strategy = DiscountFactory.GetDiscountStrategy(StrategyType.FLAT, flatoff);
            return strategy.Calculate(cart.OriginalTotal);
        }

        public override bool IsApplicable(Cart cart)
        {
            return cart.OriginalTotal >= threshold;
        }
    }
}
