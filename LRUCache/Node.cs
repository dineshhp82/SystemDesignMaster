namespace LRUCache
{
    public class Node<K, V>
    {
        public K Key { get; set; }

        public V Value { get; set; }

        public Node(K key, V value)
        {
            Key = key;
            Value = value;
        }   

        public Node<K, V>? Prev { get; set; }   

        public Node<K, V>? Next { get; set; } 

    }
}
