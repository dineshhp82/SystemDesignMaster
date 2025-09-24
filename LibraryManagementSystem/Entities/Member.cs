using LibraryManagementSystem.Observer;
using LibraryManagementSystem.Strategy;
using LibraryManagementSystem.ValueObjects;

namespace LibraryManagementSystem.Entities
{
    public class Member : IMemberObserver
    {
        public Guid MemberID { get; } = Guid.NewGuid();
        public string Name { get; private set; }
        public ContactInfo Contact { get; private set; }

        private readonly List<BorrowingRecord> _borrowingHistory = new();

        public IReadOnlyList<BorrowingRecord> BorrowingHistory => _borrowingHistory;

        public Member(string name, ContactInfo contact)
        {
            Name = name;
            Contact = contact;
        }

        public void BorrowBook(Book book, List<IBorrowingRule> rules)
        {
            foreach (var rule in rules)
            {
                rule.ValidateBorrowing(this, book);
            }

            book.MarkAsBorrowed();
            _borrowingHistory.Add(new BorrowingRecord(book));
        }

        public void ReturnBook(Book book)
        {
            var record = _borrowingHistory.FirstOrDefault(r => r.Book.ISBN == book.ISBN && r.ReturnDate == null);
            if (record == null)
                throw new InvalidOperationException("This book was not borrowed by the member.");

            record.MarkAsReturned();
            book.MarkAsAvailable();
        }

        public void Notify(Book book, string message)
        {
            Console.WriteLine($"[Notification to {Name}]: '{book.Title}' - {message}");
        }
    }
}
