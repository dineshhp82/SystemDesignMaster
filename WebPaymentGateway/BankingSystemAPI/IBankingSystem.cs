namespace WebPaymentGateway.BankingSystemAPI
{
    public interface IBankingSystem
    {
        bool ProcessPayment(decimal Amount);
    }
}
