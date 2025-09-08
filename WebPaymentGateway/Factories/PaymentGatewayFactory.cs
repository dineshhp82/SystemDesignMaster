using WebPaymentGateway.BankingSystemAPI;
using WebPaymentGateway.PaymentGatway;

namespace WebPaymentGateway.Factories
{
    public class PaymentGatewayFactory
    {
        private readonly IBankingSystem bankingSystem;

        public PaymentGatewayFactory(IBankingSystem bankingSystem)
        {
            this.bankingSystem = bankingSystem;
        }

        public PaymentGatway.PaymentGatway CreatePaymentGateway(GatewayType gatewayType)
        {
            switch (gatewayType)
            {
                case GatewayType.PAYTM:
                    var paytm = new PaytmGateway(bankingSystem);
                    return new PaymentGatewayProxy(paytm, 3);
                case GatewayType.RAZORPAY:
                    var razorpay = new RazorePayGateway(bankingSystem);
                    return new PaymentGatewayProxy(razorpay, 1);
                default:
                    throw new Exception("Invalid gateway type");
            }
        }

    }
}
