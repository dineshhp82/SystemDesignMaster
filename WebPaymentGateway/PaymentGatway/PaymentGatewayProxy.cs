using WebPaymentGateway.Models;

namespace WebPaymentGateway.PaymentGatway
{
    public class PaymentGatewayProxy : PaymentGatway
    {
        private readonly PaymentGatway paymentGatway;
        int retries;

        public PaymentGatewayProxy(PaymentGatway paymentGatway, int maxRetries)
        {
            this.paymentGatway = paymentGatway;
            retries = maxRetries;
        }

        public bool ProcessPaymentWithRetry(PaymentRequest paymentRequest)
        {
            bool result = false;
            for (int attempt = 0; attempt < retries; attempt++)
            {
                if (attempt > 0)
                {
                    Console.WriteLine($"[Proxy] Retries Payment (attempt {attempt + 1}) for {paymentRequest.Sender}");
                }

                result = paymentGatway.ProcessPayment(paymentRequest);
                if (result)
                    break;
            }
            if (!result)
            {
                Console.WriteLine($"[Proxy] Failed  after {retries} attempt for {paymentRequest.Sender}");
            }

            return result;
        }

        public override bool Confirm(PaymentRequest paymentRequest)
        {
            return paymentGatway.Confirm(paymentRequest);
        }

        public override bool Initiate(PaymentRequest paymentRequest)
        {
            return paymentGatway.Initiate(paymentRequest);
        }

        public override bool Validate(PaymentRequest paymentRequest)
        {
            return paymentGatway.Validate(paymentRequest);
        }
    }
}
