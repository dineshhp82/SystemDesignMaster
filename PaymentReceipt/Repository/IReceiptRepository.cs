using PaymentReceipt.AggregateRoot;
using PaymentReceipt.ValueObjects;

namespace PaymentReceipt.Repository
{
    public interface IReceiptRepository
    {
        void Save(Receipt receipt);
        Receipt GetById(ReceiptId id);
    }
}
