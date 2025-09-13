using DiscountCoupon.Models;

namespace DiscountCoupon.Factories
{
    public class CouponManager
    {
        private Coupon.Coupon head;

        public void RegisterCoupon(Coupon.Coupon coupon)
        {
            if (head == null)
            {
                head = coupon;
            }
            else
            {
                Coupon.Coupon cur = head;

                while (cur.Next != null)
                {
                    cur = cur.Next;
                }
                cur.Next = coupon;
            }
        }

        public IEnumerable<string> GetApplicableCoupons(Cart cart)
        {
            Coupon.Coupon cur = head;

            while (cur != null)
            {
                if (cur.IsApplicable(cart))
                {
                    yield return cur.CouponName;
                }
                cur = cur.Next;
            }
        }

        public decimal ApplyAll(Cart cart)
        {
            head?.ApplyDiscount(cart);
            return cart.CurrentTotal;
        }
    }
}
