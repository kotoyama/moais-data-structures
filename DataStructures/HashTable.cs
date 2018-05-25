using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures
{
    public class HashNode<TKey, TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }

        public static HashNode<TKey, TValue> CreatePair(TKey key, TValue value) => new HashNode<TKey, TValue>(key, value);

        public HashNode(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }

    public class HashTable<TKey, TValue>
    {
        private Storage<TKey, TValue> _table;

        public int Count { get; private set; }

        public int Size => _table.Size;

        public HashTable() => _table = new Storage<TKey, TValue>(42);

        public bool ContainsKey(TKey key) => _table[GetIndex(key)].Any(x => x.Key.Equals(key));

        public void Insert(TKey key, TValue value)
        {
            if (ContainsKey(key)) throw new ArgumentException(nameof(key));
            var inStorage = GetIndex(key);
            _table[inStorage].Add(HashNode<TKey, TValue>.CreatePair(key, value));
            Count++;
            if (Count / Size >= 2) Rebuild();
        }

        public TValue Get(TKey key)
        {
            var result = _table[GetIndex(key)];
            foreach (var pair in result)
                if (pair.Key.Equals(key)) return pair.Value;
            throw new ArgumentException(nameof(key));
        }

        public void Remove(TKey key)
        {
            var result = _table[GetIndex(key)];
            foreach (var item in result)
            {
                if (!item.Key.Equals(key)) continue;
                result.Remove(item);
                Count--;
                break;
            }
        }

        private int GetIndex(TKey key) => Math.Abs(key.GetHashCode()) % Size;

        private void Rebuild()
        {
            var temp = _table;
            _table = new Storage<TKey, TValue>(Size * 2 + 1);
            foreach (var list in temp)
            foreach (var item in list)
                Insert(item.Key, item.Value);
        }
    }

    public class Storage<TKey, TValue> : IEnumerable<List<HashNode<TKey, TValue>>>
    {
        private readonly List<HashNode<TKey, TValue>>[] _storage;

        public int Size => _storage.Length;

        public Storage(int capacity) => _storage = new List<HashNode<TKey, TValue>>[capacity];

        public List<HashNode<TKey, TValue>> this[int index] => _storage[index] ?? (_storage[index] = new List<HashNode<TKey, TValue>>());

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<List<HashNode<TKey, TValue>>> GetEnumerator()
        {
            for (var i = 0; i < Size; i++)
            {
                if (_storage[i] == null) continue;
                yield return _storage[i];
            }
        }
    }
}