namespace BookMeeting.DomainModel
{
    // 🔹 Interface Segregation Principle (ISP):
    // Each entity exposes only what it must, avoiding bloated interfaces.
    public interface IEntity
    {
        Guid Id { get; }
    }
}
