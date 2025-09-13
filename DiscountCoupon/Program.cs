using DiscountCoupon.Coupon;
using DiscountCoupon.Factories;
using DiscountCoupon.Models;

namespace DiscountCoupon
{
    /* 
     * Stratgey Applied bottom up mean start constructing small object mainly request
     * Manager Factory etc.
     Functional Requirements:
     ======================================================
     - We can add new coupons at runtime
     - Both Cart Level and Product level discount
     - Support multiple coupons type
       (Sessional offer,loyality discount,banking discount etc.)
     - Support both flat and percentage discount
     - One coupon can/cannot to applied on top of other coupons
     - Thread safe multiuser 
     - Plug and Play

     Happy Flow:
     ===========================================================
     User comes to website
     -> See Multiple Products 
     -> Choose products and add to cart
     -> Discount Coupons 
     -> Grand Total Amount
     -> Final Total Price after apply discount

     UML
     ==========================================================

     -------------------   --------------                   
     | Product         |      Cart Item    to handle this cart item totally mananged by cart externally no one know there is cart item object only know about product.                  
     -------------------   -------------                    
      string name            Product P;                     
      string category        int Qty
      decimal price          decimal GetPrice() ->(price*Qty)
     -----------------     ---------------

     |--------------
     |   Cart
     |------------
     |List<CartItem> Items (Cart has 1-M relationship with cart Item)
     |bool LoyalityMemeber
     |decimal orginal Total
     |decimal Final Total(After Discount)
     |AddProdct(Product p,int Q) => Add to Cart Item Collection
     |ApplyDiscount(decimal FinalAmount);
     |---------------------------------------
     |---------------------------
     | DiscountStrategy (abstarct)
     |-----------------------------
     | decimal Calculate(decimal amount)
     |--------------------------------------------------------------
     |     |                    |                             |  
     
       Flat Discount          Percent Discount          PercentWithCap        
       double discount amt     double percent             double percent,cap
       Calculate(decimal amt)  Calculate(Decimal amt)   calculate(double amt)
     

      |   Coupon (COR or  Decorator)(Abstract)
      ---------------------------------------------------
      |  -Coupon next(next coupon) (Chain Coupon)
      |  -SetCoupon(Coupon)  
      |  -ApplyDiscount(Cart) -> if(curr!=null) {if(curr. IsApplicable())
      |  -bool IsApplicable(Cart) (Abstract)
      |  -bool IsCombineable(Cart) (Abstract)
      |  -GetDiscount(Cart)(Abstract)
       ----------------------------------------------------------------
            |  (Has-a)              |
       Banking Coupon                 Loyality Coupon               BulkOrder Coupon            Sessional Coupon   
       --------------               -------------------       --------------------------  ------------------------
    string Bank,                        double percent             double threshold           string category
    double minspend,percent_off                                    double flatoff             double percentage
           IsApplicable
           IsCombineable
  override GetDiscount(Cart)
           Discount Startegy->Calculate()
    ---------------------------------

    ApplyDiscount(Cart)
      
    While(next!=null)
        if(next.Isapplicable())
    {
       return GetDiscount();(same class Coupon)//Template method pattern
    }
       
    }
    <<singleton>>
    --------------------------
    DiscountStarteyManager(Factory)
      DiscountStargey getCouponStartegy(string s)
    

    <<singleton>>
    ---------------------------------------
    CouponManager Has a many coupon (Entry Point)
       Coupon head
       Mutex mtx;
       RegisterCoupon(Coupon) - At the end of Coupon node
       ApplyAllCoupon(head)
       GetAllCoupon(Cart c)
     */


    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome To Discount Coupons");

            CouponManager couponManager = new CouponManager();
            couponManager.RegisterCoupon(new SeasonalOffer("Electronics", 10)); // 10% off on Electronics
            couponManager.RegisterCoupon(new LoyaltyDiscount(5)); // 5% off for loyalty members
            couponManager.RegisterCoupon(new BulkPurchaseDiscount(1000, 100));
            couponManager.RegisterCoupon(new BankingCoupon("ABC", 2000, 15, 500));

            Product p1 = new("Winter Jacket", "Clothing", 1000);
            Product p2 = new("Smartphone", "Electronics", 20000);
            Product p3 = new("Jeans", "Clothing", 1000);
            Product p4 = new("Headphones", "Electronics", 2000);

            Cart cart = new Cart(loyaltyMember: true, "HDFC");
            cart.AddCartItem(p1, 1);
            cart.AddCartItem(p2, 1);
            cart.AddCartItem(p3, 2);
            cart.AddCartItem(p4, 1);

            Console.WriteLine($"Original Cart Total: {cart.OriginalTotal} Rs");

            var applicable = couponManager.GetApplicableCoupons(cart);
            Console.WriteLine("Applicable Coupons:");

            foreach (var item in applicable)
            {
                Console.WriteLine(item);
            }

            decimal finalTotal = couponManager.ApplyAll(cart);
            Console.WriteLine("Final Cart Total after discounts: " + finalTotal + " Rs");

            Console.ReadLine();
        }
    }
}
