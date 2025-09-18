using PaymentReceipt.AggregateRoot;
using PaymentReceipt.ValueObjects;

namespace PaymentReceipt.Repository
{
    public class InMemoryReciptRepository : IReceiptRepository
    {
        List<Receipt> _receipts = new();

        public Receipt GetById(ReceiptId id) => _receipts.FirstOrDefault(r => r.ReceiptId == id);

        public void Save(Receipt receipt) => _receipts.Add(receipt);
    }
}
