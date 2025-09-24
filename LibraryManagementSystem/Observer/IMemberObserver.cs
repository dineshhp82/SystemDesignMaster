using LibraryManagementSystem.Entities;

namespace LibraryManagementSystem.Observer
{
    // Observer interface
    public interface IMemberObserver
    {
        void Notify(Book book, string message);
    }
}
