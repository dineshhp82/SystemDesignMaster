using WebPaymentGateway.BankingSystemAPI;
using WebPaymentGateway.Factories;
using WebPaymentGateway.Models;

namespace WebPaymentGateway
{
    public class PaymentController
    {

        public bool HandlePayment(GatewayType gatewayType, PaymentRequest paymentRequest)
        {
            IBankingSystem bankingService = gatewayType == GatewayType.PAYTM
                ? new BankingSystemAPI.PaytmBankingSystem()
                : new BankingSystemAPI.RazorPayBankingSystem();

            var gateway = new PaymentGatewayFactory(bankingService);

            var paymentService = new PaymentService(gateway.CreatePaymentGateway(gatewayType));

            return paymentService.ProcessPayment(paymentRequest);
        }
    }
}
