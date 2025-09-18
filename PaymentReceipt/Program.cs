using PaymentReceipt.Events;
using PaymentReceipt.Repository;
using PaymentReceipt.Service;

namespace PaymentReceipt
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Payment Receipt Desgin");

            var repo = new InMemoryReciptRepository();
            var eventPublisher = new EventPublisher();

            var service = new PaymentService(repo, eventPublisher);

            var result = service.CreateReceipt(new DTOs.PaymentResult
            {
                PayerName = "Dinesh",
                PayerEmail = "test@gmail.com",
                Method = PaymentMethod.CreditCard,
                PayerTimeZone = TimeZoneInfo.Local.Id,
                Items =
                   [
                       new DTOs.PaymentResultItem
                    {
                        Amount=100,
                        Currency="USD",
                        ReferenceId="ORD-001",
                        ReferenceType="Order"
                    },
                     new DTOs.PaymentResultItem
                    {
                        Amount=150,
                        Currency="USD",
                        ReferenceId="ORD-002",
                        ReferenceType="Order"
                    }
                   ]
            });

            Console.WriteLine(result);

            var receipt = service.GetById(result.Value.ToString());

            Console.WriteLine($"{receipt.ReceiptId} - {receipt.Total} -  {receipt.Payer.TimeZoneId}");

            Console.ReadLine();
        }
    }
}
