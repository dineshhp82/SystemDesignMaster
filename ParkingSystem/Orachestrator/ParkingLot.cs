using ParkingSystem.Entities;
using ParkingSystem.Strategies;

namespace ParkingSystem.Orachestrator
{
    public class ParkingLot
    {
        public List<ParkingFloor> Floors { get; }

        public ParkingLot(List<ParkingFloor> floors)
        {
            Floors = floors;
        }

        public Ticket? ParkVehicle(Vehicle vehicle)
        {
            foreach (var floor in Floors)
            {
                var spot = floor.FindAvailableSpot(vehicle.VehicleType);
                if (spot != null && spot.AssignVehicle(vehicle))
                {
                    return Ticket.Create(vehicle, spot);
                }
            }

            return default;
        }

        public PaymentReceipt VacateVehicle(Ticket ticket, IParkingFeeStrategy parkingFeeStrategy)
        {
            ticket.CloseTicket();
            var amount = ticket.CalculateFare(parkingFeeStrategy);

            return new PaymentReceipt(ticket.TicketId, amount);
        }
    }
}
