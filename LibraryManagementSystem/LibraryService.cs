using LibraryManagementSystem.Entities;
using LibraryManagementSystem.Repositories;
using LibraryManagementSystem.Strategy;

namespace LibraryManagementSystem
{
    public class LibraryService
    {
        private readonly BookRepository _bookRepo;
        private readonly MemberRepository _memberRepo;

        public LibraryService(BookRepository bookRepo, MemberRepository memberRepo)
        {
            _bookRepo = bookRepo;
            _memberRepo = memberRepo;
        }

        public void AddBook(Book book) => _bookRepo.Add(book);

        public void UpdateBook(Book book, string title, string author, int year) => book.UpdateDetails(title, author, year);

        public void RemoveBook(Book book) => _bookRepo.Remove(book);


        public void BorrowBook(Guid memberId, string isbn)
        {
            var member = _memberRepo.GetById(memberId) ?? throw new Exception("Member not found");
            var book = _bookRepo.GetByISBN(isbn) ?? throw new Exception("Book not found");

            var rules = new List<IBorrowingRule>
                    {
                    new MaxBooksRule(5),
                    new BookAvailabilityRule()
                    };

            member.BorrowBook(book, rules);

            Console.WriteLine($"Book Borrowed {book.Title} by {member.Name}");
            // Optionally, member unsubscribes after borrowing
            // book.Unsubscribe(member);
        }

        public void ReturnBook(Guid memberId, string isbn)
        {
            var member = _memberRepo.GetById(memberId) ?? throw new Exception("Member not found");
            var book = _bookRepo.GetByISBN(isbn) ?? throw new Exception("Book not found");

            Console.WriteLine($"Book Returned {book.Title} by {member.Name}");
            member.ReturnBook(book);
        }


        public void SubscribeMemberToBook(Guid memberId, string isbn)
        {
            var member = _memberRepo.GetById(memberId) ?? throw new Exception("Member not found");
            var book = _bookRepo.GetByISBN(isbn) ?? throw new Exception("Book not found");

            Console.WriteLine($"Book Subscribe {book.Title} by {member.Name}");
            book.Subscribe(member);
        }

        public void NotifyMembers(Book[] books)
        {
            foreach (var book in books)
            {
                book.NotifyObservers(book.Status.ToString());
            }
        }
    }
}
