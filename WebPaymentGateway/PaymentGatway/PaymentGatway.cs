using WebPaymentGateway.Models;

namespace WebPaymentGateway.PaymentGatway
{
    public abstract class PaymentGatway
    {
        //Template Method
        public virtual bool ProcessPayment(PaymentRequest paymentRequest)
        {
            if (!Validate(paymentRequest))
            {
                Console.WriteLine($"[PaymentGateway] Validate failed for {paymentRequest.Sender}");
                return false;
            }
            if (!Initiate(paymentRequest))
            {
                Console.WriteLine($"[PaymentGateway] Initiate failed for {paymentRequest.Sender}");
                return false;
            }

            if (!Confirm(paymentRequest))
            {
                Console.WriteLine($"[PaymentGateway] Confirm failed for {paymentRequest.Sender}");
                return false;
            }

            return true;
        }


        //Varient steps
        public abstract bool Initiate(PaymentRequest paymentRequest);
        public abstract bool Validate(PaymentRequest paymentRequest);
        public abstract bool Confirm(PaymentRequest paymentRequest);

    }
}
