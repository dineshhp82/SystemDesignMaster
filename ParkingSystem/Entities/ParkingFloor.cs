namespace ParkingSystem.Entities
{
    public class ParkingFloor
    {
        public int FloorNo { get; }

        public List<ParkingSpot> Spots { get; }

        public ParkingFloor(int floorNo, List<ParkingSpot> spots)
        {
            FloorNo = floorNo;
            Spots = spots;
        }

        public ParkingSpot? FindAvailableSpot(VehicleType vehicleType)
        {
            return Spots.FirstOrDefault(spot => spot.IsAvailable && spot.Type.ToString() == vehicleType.ToString());
        }
    }
}
