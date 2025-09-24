using LibraryManagementSystem.Entities;
using LibraryManagementSystem.ValueObjects;

namespace LibraryManagementSystem.Strategy
{
    public class BookAvailabilityRule : IBorrowingRule
    {
        public void ValidateBorrowing(Member member, Book book)
        {
            if (book.Status != BookStatus.Available)
                throw new InvalidOperationException("Book is not available to borrow.");
        }
    }
}
