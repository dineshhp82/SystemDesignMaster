namespace WebPaymentGateway.BankingSystemAPI
{
    public class RazorPayBankingSystem : IBankingSystem
    {
        public bool ProcessPayment(decimal Amount)
        {
            Console.WriteLine($"Process payment of {Amount} via Paytm");

            int r = new Random().Next(0, 100);
            return r < 90;
        }
    }
}
