using BookMeeting.DomainModel;

namespace BookMeeting
{
    public class InMemoryFloorRepository : IFloorRepository
    {
        private readonly List<Floor> _floors = new();

        public void Add(Floor floor)
        {
            _floors.Add(floor);
        }

        public IEnumerable<Floor> GetAll()
        {
            return _floors;
        }

        public Floor GetById(Guid floorId)
        {
            return _floors.FirstOrDefault(f => f.Id == floorId)
            ?? throw new ArgumentException("Invalid floor.");
        }
    }
}
