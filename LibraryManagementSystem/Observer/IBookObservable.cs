namespace LibraryManagementSystem.Observer
{
    // Observable interface
    public interface IBookObservable
    {
        void Subscribe(IMemberObserver member);
        void Unsubscribe(IMemberObserver member);
        void NotifyObservers(string message);
    }
}
