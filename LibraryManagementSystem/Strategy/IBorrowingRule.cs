using LibraryManagementSystem.Entities;

namespace LibraryManagementSystem.Strategy
{
    public interface IBorrowingRule
    {
        void ValidateBorrowing(Member member, Book book);
    }
}
