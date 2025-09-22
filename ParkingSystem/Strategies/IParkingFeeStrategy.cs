using ParkingSystem.Entities;

namespace ParkingSystem.Strategies
{
    public interface IParkingFeeStrategy
    {
        decimal CalculateFee(Ticket ticket);
    }
}
