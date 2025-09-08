using WebPaymentGateway.BankingSystemAPI;
using WebPaymentGateway.Models;

namespace WebPaymentGateway.PaymentGatway
{
    public class PaytmGateway : PaymentGatway
    {
        private readonly IBankingSystem _bankingSystem;

        public PaytmGateway(IBankingSystem bankingSystem)
        {
            _bankingSystem = bankingSystem;
        }

        public override bool Validate(PaymentRequest paymentRequest)
        {
            Console.WriteLine($"[Paytm] Validating payment for {paymentRequest.Sender}");

            return paymentRequest.Amount > 0 && paymentRequest.Currency == "INR";
        }

        public override bool Confirm(PaymentRequest paymentRequest)
        {
            Console.WriteLine($"[Paytm] Confirm payment for {paymentRequest.Sender}");

            return true;
        }

        public override bool Initiate(PaymentRequest paymentRequest)
        {
            Console.WriteLine($"[Paytm] Initiate payment for {paymentRequest.Amount}  {paymentRequest.Currency} {paymentRequest.Sender}");

            return _bankingSystem.ProcessPayment(paymentRequest.Amount);
        }

    }
}
