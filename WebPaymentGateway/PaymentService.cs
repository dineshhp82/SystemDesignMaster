using WebPaymentGateway.Models;

namespace WebPaymentGateway
{
    public class PaymentService
    {
        private readonly PaymentGatway.PaymentGatway paymentGatway;

        public PaymentService(PaymentGatway.PaymentGatway paymentGatway)
        {
            this.paymentGatway = paymentGatway;
        }

        public bool ProcessPayment(PaymentRequest paymentRequest)
        {
            if (paymentRequest == null)
            {
                return false;
            }
            if (paymentGatway == null)
            {
                return false; ;
            }
            return paymentGatway.ProcessPayment(paymentRequest);
        }
    }

}