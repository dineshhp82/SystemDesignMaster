using BookMeeting.DomainModel;

namespace BookMeeting
{
    // 🔹 Repository Pattern:
    // Abstracts persistence — decouples business logic from data storage.
    public interface IFloorRepository
    {
        void Add(Floor floor);
        Floor GetById(Guid floorId);
        IEnumerable<Floor> GetAll();
    }
}
