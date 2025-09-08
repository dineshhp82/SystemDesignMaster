using WebPaymentGateway.BankingSystemAPI;
using WebPaymentGateway.Models;

namespace WebPaymentGateway.PaymentGatway
{
    public class RazorePayGateway : PaymentGatway
    {
        IBankingSystem _bankingSystem;

        public RazorePayGateway(IBankingSystem bankingSystem)
        {
            _bankingSystem = bankingSystem;
        }

        public override bool Validate(PaymentRequest paymentRequest)
        {
            Console.WriteLine($"[RazorPay] Validating payment for {paymentRequest.Sender}");

            return paymentRequest.Amount > 0;
        }

        public override bool Confirm(PaymentRequest paymentRequest)
        {
            Console.WriteLine($"[RazorPay] Confirm payment for {paymentRequest.Sender}");

            return true;
        }

        public override bool Initiate(PaymentRequest paymentRequest)
        {
            Console.WriteLine($"[RazorPay] Initiate payment for {paymentRequest.Amount}  {paymentRequest.Currency} {paymentRequest.Sender}");

            return _bankingSystem.ProcessPayment(paymentRequest.Amount);
        }
    }
}
