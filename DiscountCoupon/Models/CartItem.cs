namespace DiscountCoupon.Models
{
    public class CartItem(Product product, int qty)
    {
        public Product Product { get; } = product;

        public decimal GetAmount() => Product.Price * qty;
    }
}
