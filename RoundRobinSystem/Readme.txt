-Round Robin System
===================	

Requirements :
 - AddServer(serverId): Add a new server to the pool.
 - RemoveServer(serverId): Remove an existing server from the pool.
 - GetNextServer(): Return the next server in the round-robin sequence.
 - Thread Safety: The system should be thread-safe to handle concurrent requests.
 - Track number of rquest handled by each server

 API
  - Add(Server)
  - Remove(ServerId)
  - GetNextServer()
  - Request Count

- High Level
 
  - Maintain a list of servers in concurrent dictionary (serverid and metadata)
  - Maintain a volatile snapshot array (immutable) of the currently active server IDs (only healthy ones).
  - When server list changes (add/remove/health change), rebuild snapshot and Interlocked.Exchange it in an atomic operation.
  - 



  Snapshot is just an immutable array of healthy server IDs:

  Fast, lock-free reads
	When a request comes in, GetNext() just:
	Reads the current snapshot (1 volatile read).
	Picks an index with modulo arithmetic.
	Returns that server.

No locks, no expensive data structures → just an array lookup.

If we only used a ConcurrentDictionary<Guid, Server>:
Iterating or selecting from it every request would be slow.
Its internal locking would become a bottleneck.

The snapshot array never changes after it’s built (immutable).


Example Walkthrough with Snapshot

Initial servers = S1, S2, S3
Snapshot = [S1, S2, S3]

Thread A calls GetNext()

Reads [S1, S2, S3]

Picks S2

At the same time, an admin removes S2

Registry rebuilds snapshot → [S1, S3]

Atomically swaps in new snapshot

Thread B calls GetNext()

Reads new snapshot [S1, S3]

Picks S3

No race condition, no broken list, no crash.

Old snapshot ([S1, S2, S3]) is garbage-collected when no thread references it anymore.

Think of the snapshot as a printed timetable for buses:
	Passengers (requests) just look at the current printed timetable — fast, no confusion.
	If routes change, the transport office prints a new timetable and replaces the old one.
	No one is scribbling on the timetable while passengers are reading it.

