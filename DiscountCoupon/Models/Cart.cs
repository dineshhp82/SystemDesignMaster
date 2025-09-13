namespace DiscountCoupon.Models
{
    public class Cart(bool loyaltyMember, string paymentBank)
    {
        private readonly List<CartItem> _items = [];
        public IReadOnlyList<CartItem> CartItems => _items;

        public decimal OriginalTotal { get; private set; } 

        public decimal CurrentTotal { get; private set; }

        public bool LoyaltyMember { get; } = loyaltyMember;

        public string PaymentBank { get; } = paymentBank;

        public void AddCartItem(Product product, int qty)
        {
            var cartItem = new CartItem(product, qty);
            _items.Add(cartItem);

            OriginalTotal += cartItem.GetAmount();
            CurrentTotal += cartItem.GetAmount();
        }

        public void ApplyDiscount(decimal discount)
        {
            CurrentTotal -= discount;
            if (CurrentTotal < 0)
            {
                CurrentTotal = 0;
            }

        }
    }
}
