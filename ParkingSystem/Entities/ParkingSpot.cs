namespace ParkingSystem.Entities
{
    public class ParkingSpot
    {
        public int SpotId { get; }
        public SpotType SpotType { get; }
        public SpotType Type { get; }
        public bool IsAvailable { get; private set; } = true;

        public Vehicle? ParkedVehicle { get; private set; }

        public ParkingSpot(int spotId,SpotType spotType)
        {
            SpotId = spotId;
            SpotType = spotType;
        }

        public bool AssignVehicle(Vehicle vehicle)
        {
            if (!IsAvailable || vehicle.VehicleType.ToString() != Type.ToString())
                throw new InvalidOperationException("Parking spot is already occupied.");

            ParkedVehicle = vehicle;
            IsAvailable = false;

            return true;
        }

        public void VacateSpot()
        {
            IsAvailable = true;
            ParkedVehicle = null;
        }
    }
}
