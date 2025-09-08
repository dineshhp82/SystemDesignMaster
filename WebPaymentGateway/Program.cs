using WebPaymentGateway.Models;

namespace WebPaymentGateway
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Web World Payment System");


            PaymentRequest paymentRequest = new PaymentRequest("Raja", "Aditya", 2000, "INR");

            Console.WriteLine("Process vid Paytm");
            var res = new PaymentController();
            var paymentesult = res.HandlePayment(GatewayType.PAYTM, paymentRequest);

            var stringResult = paymentesult ? "Success" : "Failed";
            Console.WriteLine($"Result: {stringResult}");

            PaymentRequest paymentRequestRazor = new PaymentRequest("Amit", "Dinesh", 2000, "INR");

            Console.WriteLine("Process vid Razor");
            var res1 = new PaymentController();
            var paymentesult1 = res.HandlePayment(GatewayType.RAZORPAY, paymentRequest);

            var stringResult1 = paymentesult1 ? "Success" : "Failed";
            Console.WriteLine($"Result: {stringResult1}");

        }
    }
}
