using DiscountCoupon.Factories;
using DiscountCoupon.Models;

namespace DiscountCoupon.Coupon
{
    public class BankingCoupon(string paymentBank, decimal minSpend, decimal percent, decimal offcap) : Coupon
    {
        public override string CouponName => $"Bank {paymentBank} Bank of Rs {percent} off upto {offcap}";

        public override decimal GetDiscount(Cart cart)
        {
            var strategy = DiscountFactory.GetDiscountStrategy(StrategyType.PERCENT_WITH_CAP, percent, offcap);
            return strategy.Calculate(cart.OriginalTotal);
        }

        public override bool IsApplicable(Cart cart)
        {
            return cart.PaymentBank == paymentBank && cart.OriginalTotal >= minSpend;
        }
    }
}
