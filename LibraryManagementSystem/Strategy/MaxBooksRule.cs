using LibraryManagementSystem.Entities;

namespace LibraryManagementSystem.Strategy
{
    public class MaxBooksRule : IBorrowingRule
    {
        private readonly int _maxBooks;

        public MaxBooksRule(int maxBooks)
        {
            _maxBooks = maxBooks;
        }


        public void ValidateBorrowing(Member member, Book book)
        {
            if (member.BorrowingHistory.Count(r => r.ReturnDate == null) >= _maxBooks)
                throw new InvalidOperationException($"Member has already borrowed maximum {_maxBooks} books.");
        }
    }
}
