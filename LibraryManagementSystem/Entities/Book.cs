using LibraryManagementSystem.Observer;
using LibraryManagementSystem.ValueObjects;

namespace LibraryManagementSystem.Entities
{
    public class Book: IBookObservable
    {
        public ISBN ISBN { get; }

        public string Title { get; private set; }
        public string Author { get; private set; }
        public int PublicationYear { get; private set; }
        public BookStatus Status { get; private set; }

        private readonly List<IMemberObserver> _observers = new();

        public Book(ISBN isbn, string title, string author, int year)
        {
            ISBN = isbn;
            Title = title;
            Author = author;
            PublicationYear = year;
            Status = BookStatus.Available;
        }

        public void UpdateDetails(string title, string author, int year)
        {
            Title = title;
            Author = author;
            PublicationYear = year;
        }   

        public void MarkAsBorrowed()
        {
            if (Status == BookStatus.Borrowed)
                throw new InvalidOperationException("Book is already borrowed.");
            Status = BookStatus.Borrowed;
        }   

        public void MarkAsAvailable()
        {
            if (Status == BookStatus.Available)
                throw new InvalidOperationException("Book is already available.");
            Status = BookStatus.Available;
        }

        public void Subscribe(IMemberObserver member)
        {
            _observers.Add(member);
        }

        public void Unsubscribe(IMemberObserver member)
        {
            _observers.Remove(member);  
        }

        public void NotifyObservers(string message)
        {
            foreach (var observer in _observers)
                observer.Notify(this, message);
        }
    }
}
