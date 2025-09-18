namespace PaymentReceipt.ValueObjects
{

    /*
     Why Desconstruct 
      - you can add a Deconstruct method to a class/record/value object.It lets you “unpack” an object into variables (tuple style).
      - var item = new PaymentItem("PRD-001", "Product", new Money(500, "INR"));
      -  // Deconstruct into variables 
      -  var (refId, refType, amount) = item;


    Without Deconstruct, you’d have to do:
    var refId = item.ReferenceId;
    var refType = item.ReferenceType;
    var amount = item.Amount;

    Cleaner code in LINQ queries
    Domain readability
    Work in tuple-style

    Deconstruct in C# is not a built-in method automatically added — it’s a convention-based custom method
    The compiler looks for a method named Deconstruct with out parameters.
    For records, C# auto-generates the Deconstruct.
    But for class you need to custom implement it.
     */
    public record PaymentItem(string ReferenceId, string ReferenceType, Money Amount)
    {
        public void Deconstruct(out string referenceId, out string referenceType, out Money amount)
        {
            referenceId = ReferenceId;
            referenceType = ReferenceType;
            amount = Amount;
        }
    }
}
