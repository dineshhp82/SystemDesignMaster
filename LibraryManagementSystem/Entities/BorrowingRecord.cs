namespace LibraryManagementSystem.Entities
{
    public class BorrowingRecord
    {
        public Book Book { get; }
        public DateTime BorrowDate { get; }
        public DateTime? ReturnDate { get; private set; }

        public BorrowingRecord(Book book)
        {
            Book = book;
            BorrowDate = DateTime.UtcNow;
        }

        public void MarkAsReturned() => ReturnDate = DateTime.UtcNow;
    }
}
