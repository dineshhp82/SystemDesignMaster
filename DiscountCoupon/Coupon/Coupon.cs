using DiscountCoupon.Models;

namespace DiscountCoupon.Coupon
{
    // COR
    public abstract class Coupon
    {
        public Coupon Next { get; set; }

        public abstract bool IsApplicable(Cart cart);
        public abstract decimal GetDiscount(Cart cart);
        public virtual bool IsCombineable { get; set; } = true;

        //Template method
        public void ApplyDiscount(Cart cart)
        {
            if (IsApplicable(cart))
            {
                decimal discount = GetDiscount(cart);
                cart.ApplyDiscount(discount);
                Console.WriteLine($"Discount applied {discount}");

                if (!IsCombineable)
                    return;

            }

            //Recursive call untill next coupon avalibale
            if (Next != null)
            {
                Next.ApplyDiscount(cart);
            }
        }

        public abstract string CouponName { get;}
    }
}
