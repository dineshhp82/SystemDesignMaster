- Parking System
  - Different kind of vehicles can be parked in the parking lot.
  - Each vehicle type has a different parking fee.
  - Parking fee calculation is based on the different strategies.
  - When a vehicle enters the parking lot, a ticket is generated with entry time.
  - When a vehicle exits, the exit time is recorded, and the total parking fee is calculated based on the duration of stay and vehicle type.
  - The system should be able to handle concurrent vehicle entries and exits.

Requirements
=========================================================================================================
- A Parking Lot has multiple Floors.
- Each floor has Parking Spots of different types (Car, Bike, Truck).
- A Vehicle can be parked if a matching spot is available. 
- On entry → assign a spot + generate a Ticket. (decrease the avalibilty)
- On exit → calculate fare + close ticket. (increase the avalibilty)
- Extendable: support multiple floors, different rates, reserved spots.

================================================================================================
Entity and VO
- Floor - Floor Id and ParkingSpot[], FindAvalibable()
- Vehicle - Vehicle Number,Type
- Ticket - TicketId,Spot Id,FloorId,entry time,exit time,vehicle,CalculateFare()
- ParkingSpot - Spot Id,FloorId,Type,Availability

VO
 - Parking Rate
 - Payment Receipt -> TickeId,Amount,PaidAt

Aggregated Route

<<Orachestor>>

-> Find 
   Book-> Issue Ticket  -> Occupied space
   Release -> Generate Receipt ->Vacate space

- ParkingLot    
   Floors :[]
   FindSpot()       
   ParkVehicle() 
   VacateSpot()




 Vehicle Arrives
     |
     v
ParkingLot.FindSpot(vehicle)
     |
     v
ParkingFloor.FindAvailableSpot()
     |
     v
ParkingSpot.AssignVehicle(vehicle)
     |
     v
Ticket Issued  ---> Vehicle Stays ---> Ticket Exit
     |                                   |
     v                                   v
 EntryTime                           ExitTime + Fare
     |
     v
PaymentReceipt Generated
