using LibraryManagementSystem.Entities;
using LibraryManagementSystem.Repositories;
using LibraryManagementSystem.ValueObjects;

namespace LibraryManagementSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Libaray Service");

            var bookRepo = new BookRepository();
            var memberRepo = new MemberRepository();

            var libraryService = new LibraryService(bookRepo, memberRepo);

            var book1 = new Book(new ISBN("1234567890"), "C# in Depth", "Jon Skeet", 2020);
            bookRepo.Add(book1);

            var member1 = new Member("Alice", new ContactInfo("alice@example.com", "123456"));
            memberRepo.Add(member1);

            libraryService.SubscribeMemberToBook(member1.MemberID, book1.ISBN.Value);

            libraryService.BorrowBook(member1.MemberID, book1.ISBN.Value); // Borrowing, book status changes
            libraryService.ReturnBook(member1.MemberID, book1.ISBN.Value); // Book becomes available, triggers notification

            libraryService.NotifyMembers([book1]);

            Console.ReadLine();

        }
    }
}
