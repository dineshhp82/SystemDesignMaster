- Least Recent Used (LRU) Cache

Requirements:	
- Put(key, value): Insert or update the value of the key. If the cache reaches its capacity, 
					it should invalidate the least recently used item before inserting a new item.)
- Get(key): Return the value of the key if it exists in the cache, otherwise return -1.
- Capacity: The cache should have a fixed capacity defined at the time of its creation.
- Cache should be thread safe and concurrent handling
- Cache should be optimized for both time and space complexity.


- Work flow for PUT
  -----------------------------------------------------------------------------
   [Check Dictionary]
     if Key exist  -> then update move node to head (mean when you use the node move to head so that it become most recent used node)
	 if key not exist -> then create new node
	   [Check Capacity]
		 if capacity full -> remove tail node and remove from dictionary (remove from tail which least recent used node)
		 if capacity not full -> add new node to head and add to dictionary	 (always add new node to head which is most recent used node)

- Work flow for GET
  -----------------------------------------------------------------------------
   [Check Dictionary]
	 if Key exist  -> then move node to head and return value
	 if key not exist -> then return -1

- Question  : Why doubly linked list ?
  Answer    : In a doubly linked list, each node contains a reference to both the next and the previous node. 
			  This allows for efficient insertion and deletion of nodes from both ends of the list, 
			  which is crucial for maintaining the order of usage in an LRU cache.
			  In an LRU cache, we need to quickly access and update the order of items based on their usage.
			  Allow to traverse in forward and reverse 

			  - Move most recent items to the front (HEAD)
			  - Remove least recent items from the back (TAIL)

			  Key Operations
			  - Add new node at the head  -> O(1)
			  - Remove node from the tail -> O(1)	
			  - Move node to the head     -> O(1)	(Update previous and next pointers of adjacent nodes and insert at head))

			  Node  -> Previous Pointer
			           Next Pointer
					   Data
			
			 First Node is call Head and head has previous ->NULL 
			 Last Node is call Tail and tail has next -> NULL	

			 While Insert or Update you need to update the pointer of both previous and next nodes(neighboring nodes)

			 Why not singly linked list ?

			  To remove a node from a singly linked list, you need to have a reference to the previous node to update its next pointer.
			  you need to traverse the list from the head to find the previous node, which takes O(n) time in the worst case.


			  Why not Array or List ?
			  Insert and delete in front or middle of array or list takes O(n) time as it requires shifting elements.

Q: Why we used the Dictionary
    - Dictionary provides O(1) average time complexity for "lookups", insertions, and deletions.
	- Direct Access by Key
	   - Instead of scanning the entire  doubly linked list to find an item, you can directly access it using its key.
			if (map.ContainsKey(key)) ... in O(1)
	   - This direct access is crucial for the efficiency of the LRU cache operations.
				Dictionary<int, DoublyLinkedNode> cacheMap;
				Key: int (the cache key, like bookId or userId).
				Value: DoublyLinkedNode (holds key, value, prev, next).
         - Combined with the doubly linked list, the dictionary allows for efficient updates to the order of items based on their usage.
		 -Enforce Capacity
			Dictionary’s Count property lets us check if the cache is full in O(1).
			If full → evict tail node from DLL → remove from Dictionary → done in O(1).

Suppose :
 
 Suppose cache capacity = 3.

Put(1, "A")
	Add new node to DLL head.
	Add 1 → node(1, "A") in Dictionary.

Put(2, "B")
	Add node to head.
	Add 2 → node(2, "B").

Get(1)
	Lookup Dictionary: key 1 found → O(1).
	Move node(1) to head of DLL.

Put(3, "C")
	Add node(3) at head.
	Cache full → Evict tail (node(2)).
	Remove key 2 from Dictionary.


+------------------+
|     LRUCache     |
+------------------+
| - capacity       |
| - dict: Dictionary<K, Node<K,V>> |
| - head: Node     |
| - tail: Node     |
| - locker         |
+------------------+
| + Get(K key): V  |
| + Put(K key,V val)|
| - MoveToHead(Node)|
| - RemoveTail(): Node |
+------------------+

+------------------+
|     Node<K,V>    |
+------------------+
| K Key            |
| V Value          |
| Node Prev        |
| Node Next        |
+------------------+



Why ReaderWriterLockSlim?
         
        It’s a lock designed for scenarios where reads are much more frequent than writes (like a cache):
            Multiple readers can enter concurrently (EnterReadLock) → good for Get.
            Only one writer at a time (EnterWriteLock) → good for Put and eviction.
            Writers block readers until done, ensuring correctness.
            Much faster and more scalable than a simple lock (Monitor) when there are many readers.

        Why Not Just lock?
            lock (Monitor) allows only one thread at a time, even for reads.
            In a cache, 90% of operations are reads.
            With ReaderWriterLockSlim, multiple readers can check the cache simultaneously, only blocking if a write happens.
            This gives better throughput under high concurrency.

What ConcurrentDictionary Gives Us

Thread-safe Add, Update, Remove, TryGetValue.
Optimized for multi-threaded read/write workloads.
Fine-grained locking inside (not a global lock), so multiple threads can safely operate on different keys simultaneously.

But Here’s the Catch for LRU

The Dictionary is only half the problem.
The other half is the Doubly Linked List that maintains usage order.


Option 1: Dictionary + Doubly Linked List + ReaderWriterLockSlim
	Dictionary: Dictionary<int, Node> (not concurrent).
	Use ReaderWriterLockSlim for both dictionary and list.
	Simpler, consistent, predictable.
	Reads are fast (concurrent).

Option 2: ConcurrentDictionary + Doubly Linked List + Lock for List
	Dictionary: ConcurrentDictionary<int, Node>.
	Only lock for list operations (move-to-head, remove-tail).
	Dictionary ops are lock-free (handled internally).
	But you still need synchronization because dictionary + list must stay consistent.




