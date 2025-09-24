using LibraryManagementSystem.Entities;

namespace LibraryManagementSystem.Repositories
{
    public class BookRepository
    {
        private readonly List<Book> _books = new();
        public void Add(Book book) => _books.Add(book);
        public void Remove(Book book) => _books.Remove(book);
        public Book? GetByISBN(string isbn) => _books.FirstOrDefault(b => b.ISBN.Value == isbn);
    }
}
