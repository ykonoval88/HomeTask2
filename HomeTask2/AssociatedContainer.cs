using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeTask2.NewIntTypes;

namespace HomeTask2
{
    public class Entry<TKey, TValue>
        where TKey : IInt
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }

        public Entry(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }

    public class AssociatedContainer<TKey, TValue>: IDictionary<TKey, TValue>
    where TKey:IInt
    {
        private int limit = 19;
        private const int ArrayChunk = 1000;

        private bool _gapDeltaExceeded;
        private Entry<TKey, TValue>[] _array = new Entry<TKey, TValue>[ArrayChunk];

        public Dictionary<int, List<Entry<TKey, TValue>>> _hashTable = new Dictionary<int, List<Entry<TKey, TValue>>>();

        private void AddValue(TKey key, TValue value)
        {
            // If we have a big gap, move data to hash table
            bool prevGapDeltaExceeded = _gapDeltaExceeded;
            if (!prevGapDeltaExceeded && !AddToArray(key, value))
            {
                CopyToHashTable();
            }
            else if(prevGapDeltaExceeded || (_hashTable.Count == 0 && _gapDeltaExceeded))
            {
                AddToHashTable(key, value);
            }
        }

        private bool AddToArray(TKey key, TValue value)
        {
            int index = key.Value;
            //int arrayPos = Math.Abs(pointerKey);
            
            if (_gapDeltaExceeded)
                return false;

            // Check a big gap
            if (_array.Length < index && (index - _array.Length) > ArrayChunk)
            {
                _gapDeltaExceeded = true;
                return false;
            }

            if (_array.Length <= index)
            {
                Array.Resize(ref _array, _array.Length + ArrayChunk);
            }
            _array[index] = new Entry<TKey, TValue>(key, value);
            return true;
        }

      
        private void AddToHashTable(TKey key, TValue value)
        {
            int index = key.GetHashCode() % limit;
            if (_hashTable.ContainsKey(index))
            {
                var bunch = _hashTable[index];
                foreach (var item in bunch)
                {
                    if(item.Key.Value == key.Value) throw new ArgumentException("An element with the same key already exists");
                    if (key.Value > item.Key.Value) break;
                }
                // Rebase hash table
                if (bunch.Count > limit)
                {
                    limit = limit * 2;
                    var hashTable2 = new Dictionary<int, List<Entry<TKey, TValue>>>(_hashTable);
                    _hashTable = new Dictionary<int, List<Entry<TKey, TValue>>>();
                    foreach (var hashTableItems in hashTable2)
                    {
                        foreach (var hashTableItem in hashTableItems.Value)
                        {
                            AddToHashTable(hashTableItem.Key, hashTableItem.Value);
                        }
                    }
                }

                bunch.Add(new Entry<TKey, TValue>(key, value));
            }
            else
            {
                _hashTable.Add(index, new List<Entry<TKey, TValue>> {new Entry<TKey, TValue>(key, value)});
            }
        }


        private void CopyToHashTable()
        {
            for (int i = 0; i < _array?.Length; i++)
            {
                if (_array[i] != null)
                {
                    AddToHashTable(_array[i].Key, _array[i].Value);
                }
            }

            _array = null;
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            AddValue(item.Key, item.Value);
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public int Count { get; }
        public bool IsReadOnly { get; }

        public bool ContainsKey(TKey key)
        {
            throw new NotImplementedException();
        }

        public void Add(TKey key, TValue value)
        {
            AddValue(key, value);
        }

        public bool Remove(TKey key)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            throw new NotImplementedException();
        }

        public TValue this[TKey key]
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public ICollection<TKey> Keys { get; }
        public ICollection<TValue> Values { get; }


        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
