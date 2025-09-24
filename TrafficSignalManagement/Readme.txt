------Traffic Singnal Management--------------

# Requirements for Traffic Signal Management System

- Model an intersection with two directions: North-South (NS) and East-West (EW).
- Each direction has a traffic signal that can be either Green, Yellow, or Red.
- The traffic signals should operate in a cycle: Green -> Yellow -> Red -> Green.
- The duration for each signal state should be configurable:
  - Green: 60 seconds
  - Yellow: 5 seconds
  - Red: 65 seconds (to allow for the other direction's Green and Yellow)
- IntersectionControl coordinates signals so no conflicting greens occur.
- Green duration is adjustable at runtime using traffic counts. 
- Support commands: start, stop, update traffic counts, force transition (operator override).
- Log state transitions and timing for audit.
- A Central Traffic Monitoring Center would like to observe all intersections and receive updates whenever signals change, traffic counts change, or emergencies occur.



NSGreen         -> NSYellow         -> EWGreen     -> EWYellow
Green-> Red     -> Yellow->Red      -> Red->Green  -> Red->Yellow    

NS: Green -> Red 
EW: Red   -> Green

NS: Yellow -> Red
EW: Red    -> Yellow


+-------------------------------+
| IntersectionControl           |
|-------------------------------|
| - currentState : IState       |
| - signals : List<Signal>      |
| - trafficData : Dictionary    |
| - timingFactory : ITimingFactory |
|-------------------------------|
| + Start()                     |
| + Stop()                      |
| + UpdateTraffic(signalId,int) |
| + ForceNext()                 |
+-------------------------------+
            |
            |  uses
            v
+-------------------------------+
| IState (interface)            |
|-------------------------------|
| + Enter() : Task              |
| + Exit() : Task               |
| + Next() : IState             |
+-------------------------------+
  ^       ^         ^         ^
  |       |         |         |
NSGreen  NSYellow  EWGreen  EWYellow   (concrete states)
(each implements IState)

+-------------------------------+
| Signal                        |
|-------------------------------|
| - id                          |
| - location                    |
| - lights : List<TrafficLight> |
| + SetColor(color)             |
+-------------------------------+

+-------------------------------+
| TrafficLight (VO)             |
|-------------------------------|
| - Color : LightColor          |
| - DurationInSeconds : int     |
+-------------------------------+

+-------------------------------+
| ITimingStrategy / Factory     |
|-------------------------------|
| + GetGreenDuration(signalId, trafficCount) : int |
+-------------------------------+



State pattern: each intersection state is an object (IState) that knows how to enter, exit, and what next state is. Clean transitions and easy to extend.

Strategy / Factory: ITimingStrategy (via factory) provides green durations based on traffic counts — pluggable algorithms.

Repository-like in-memory stores: signals and trafficData are kept in IntersectionControl (could be replaced with DB/telemetry).

Command/Operator override: ForceNext() to simulate operator action.




