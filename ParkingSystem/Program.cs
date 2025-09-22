using ParkingSystem.Entities;
using ParkingSystem.Enums;
using ParkingSystem.Factory;
using ParkingSystem.Orachestrator;

namespace ParkingSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Parking Lot");

            var floor1 = new ParkingFloor(1, new List<ParkingSpot>
        {
            new ParkingSpot(1, SpotType.Car),
            new ParkingSpot(2, SpotType.Bike),
            new ParkingSpot(3, SpotType.Car)
        });

            var parkingLot = new ParkingLot(new List<ParkingFloor> { floor1 });

            var car = new Vehicle("MH12AB1234", VehicleType.Car);
            var ticket = parkingLot.ParkVehicle(car);

            Console.WriteLine($"Car parked at Spot {ticket?.Spot.SpotId}, TicketId={ticket?.TicketId}");

            // Simulate some time...
            System.Threading.Thread.Sleep(2000);

            var perHourPriceContext = new PricingContext(PriceType.Hourly, VehicleType.Car, DateTime.UtcNow.AddHours(2))
                .WithRatePerHour(50)
                .BuildForStrategy(PriceType.Hourly);


            var priceFactory = new ParkingFeeStrategyFactory()
                .CreateStrategy(perHourPriceContext);

            var receipt = parkingLot.VacateVehicle(ticket, priceFactory); // 50 per hour
            Console.WriteLine($"Receipt: TicketId={receipt.TicketId}, Amount={receipt.Amount}");
        }
    }
}
