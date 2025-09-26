namespace LRUCache
{
    public class LRUCache<K, V>
    {
        private readonly int _capacity;
        private readonly Dictionary<K, Node<K, V>> _cacheMap;


        private Node<K, V>? _head;
        private Node<K, V>? _tail;

        private readonly ReaderWriterLockSlim _lock = new();

        public LRUCache(int capacity)
        {
            _capacity = capacity;
            _cacheMap = new Dictionary<K, Node<K, V>>();
        }

        public V Get(K key)
        {
            _lock.EnterUpgradeableReadLock();
            try
            {
                if (_cacheMap.TryGetValue(key, out Node<K, V> node))
                {
                    _lock.EnterWriteLock();
                    try
                    {
                        MoveToHead(node);
                    }
                    finally
                    {
                        _lock.ExitWriteLock();
                    }
                    return node.Value;
                }
                return default;
            }
            finally
            {
                _lock.ExitUpgradeableReadLock();
            }
        }

        public void Set(K key, V value)
        {
            _lock.EnterWriteLock();
            try
            {
                if (_cacheMap.TryGetValue(key, out Node<K, V> node))
                {
                    //Update value if already exists
                    node.Value = value;
                    MoveToHead(node);
                }

                var newNode = new Node<K, V>(key, value);
                _cacheMap.Add(key, newNode);

                AddNodeToHead(newNode);

                if (_cacheMap.Count > _capacity)
                {
                    //find tail node 
                    //remove tail node from _CacheMap
                    var tailNode = RemoveTailNode();
                    _cacheMap.Remove(tailNode.Key);
                }
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }


        /*
         
           [null,1]<->[1,2]<->[2,3]<->[3,null]
           
           suppose move [1,2] node
           update previous [null,1] next node [2,3] -> [null,2]<->[2,3]
           
        head node [3,null]  update next node [1,2] -> [3,1]->[1,null]

        final

        [null,2]<->[2,3]<->[3,1]<->[1,null]
         */
        private void MoveToHead(Node<K, V> node)
        {
            //mean only one node
            if (node == _head)
                return;
            //update previous node and next mode with current node

            if (node.Prev != null)
                node.Prev.Next = node.Next;

            if (node.Next != null)
                node.Next.Prev = node.Prev;

            // only tail node
            if (node == _tail)
            {
                _tail = node.Prev;
            }

            //insert at head
            node.Prev = null;
            node.Next = _head;

            if (_head != null)
                _head.Prev = node;


            _head = node;
        }

        /*
         [null,1]<->[1,2]<->[2,3]<->[3,null]
         [4,null] 
         
         HeadNode [3,null] next = node
         [4,null] -> set next null
         set new node head
             PRev,Next
             Head <-> Tail
             Prev<-> Next
        

        NULL <- [HEAD] <-> [X] <-> [TAIL] -> NULL

        HEAD.Next = X
        X.Prev = HEAD
        X.Next = TAIL
        TAIL.Prev = X
         */
        private void AddNodeToHead(Node<K, V> node)
        {
            /*
           [P,NewNode,N]  [P(NULL),HEAD,N] 
             */

            // new  node k next existing head node (where head previously pointed to null)
            node.Next = _head;
            node.Prev = null; // new node k previous null

            if (_head != null)  // head not is not null then update previous of existing head node to new node
                _head.Prev = node;

            _head = node; // make head node to new node

            if (_tail == null) // if tail is null means only one node then make tail to new node
                _tail = node;
        }

        private Node<K, V> RemoveTailNode()
        {
            /*
          [P,NewNode,N]  [P(NULL),HEAD,N] 

            [P,TAIL,NULL(NEXT)]
            */

            if (_tail == null)
                return null;

            var node = _tail;
            _tail = _tail.Prev;

            //mean disconnted the previous node from tail node

            if (_tail != null)
            {
                _tail.Next = null;
            }
            else
            {
                // this is the case when one node only tail and head both point to same node null prev and next
                _head = null;
            }

            node.Prev = null; // set null to prev and next of removed node
            node.Next = null;
            return node;
        }
    }
}
