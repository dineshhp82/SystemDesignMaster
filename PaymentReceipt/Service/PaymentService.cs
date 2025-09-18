using PaymentReceipt.AggregateRoot;
using PaymentReceipt.DTOs;
using PaymentReceipt.Events;
using PaymentReceipt.Repository;
using PaymentReceipt.ValueObjects;
using System;

namespace PaymentReceipt.Service
{
    //Application Service (Orchestrator)
    public class PaymentService
    {
        private readonly IReceiptRepository _repo;
        private readonly IEventPublisher _publisher;

        public PaymentService(IReceiptRepository repo, IEventPublisher publisher)
        {
            _repo = repo;
            _publisher = publisher;
        }

        public ReceiptId CreateReceipt(PaymentResult result)
        {
            var payer = new Payer(result.PayerName, result.PayerEmail, result.PayerTimeZone);

            var paymentItems = result.Items.Select(item => new PaymentItem(item.ReferenceId, item.ReferenceType, new Money(item.Amount, item.Currency)));

            var receipt = Receipt.Create(payer, paymentItems, result.Method);

            //persist
            _repo.Save(receipt);

            //publish event
            _publisher.Publish(receipt.ToCreatedEvent());

            return receipt.ReceiptId;
        }

        public Receipt GetById(string  receiptId)
        {
            if (string.IsNullOrWhiteSpace(receiptId))
                throw new ArgumentException("Receipt Id is required", nameof(receiptId));

            if (!Guid.TryParse(receiptId, out var guid))
                throw new ArgumentException("Invalid Receipt Id format", nameof(receiptId));

            var receipt = _repo.GetById(new ReceiptId(guid));

            if (receipt == null)
                return null; // Not found → API can return 404

            return receipt;
        }
    }
}
