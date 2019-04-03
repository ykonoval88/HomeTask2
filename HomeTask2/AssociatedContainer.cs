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
        private const int ArrayChunk = 1000;

        private bool _gapDeltaExceeded;
        private Entry<TKey, TValue>[] _array = new Entry<TKey, TValue>[ArrayChunk];

        private readonly SortedDictionary<int, TValue[]> _hashTable = new SortedDictionary<int, TValue[]>();

        private void AddValue(TKey key, TValue value)
        {
            // If we have a big gap, move data to hash table
            bool prevGapDeltaExceeded = _gapDeltaExceeded;
            if (!prevGapDeltaExceeded && !AddToArray(key, value))
            {
                CopyToHashTable();
            }
            else if(prevGapDeltaExceeded)
            {
                AddToHashTable(key, value);
            }
        }

        private bool AddToArray(TKey key, TValue value)
        {
            int pointerKey = key.GetHashCode();
            int arrayPos = Math.Abs(pointerKey);
            
            if (_gapDeltaExceeded)
                return false;

            // Check a big gap
            if (_array.Length < arrayPos && (arrayPos - _array.Length) > ArrayChunk)
            {
                _gapDeltaExceeded = true;
                return false;
            }

            // Re-calculate next position. Required if another value already present in the same position.
            if (_array.Length > 0)
            {
                var lastPosition = Array.FindLastIndex(_array, a => a != null);
                arrayPos = lastPosition > 1 ? lastPosition + 1 : arrayPos;

                // Resize array if position greater that array.
                if (_array.Length <= arrayPos)
                {
                    Array.Resize(ref _array, _array.Length + ArrayChunk);
                }
            }

            _array[arrayPos] = new Entry<TKey, TValue>(key, value);
            return true;
        }

        private void AddToHashTable(TKey key, TValue value)
        {
            int pointerKey = key.GetHashCode();
            AddToHashTable(pointerKey, value);
        }

        private void AddToHashTable(int key, TValue value)
        {
            int pointerKey = key.GetHashCode();
            if (_hashTable.ContainsKey(pointerKey))
            {
                var values = _hashTable[pointerKey];
                Array.Resize(ref values, values.Length + 1);
                values[values.Length-1] = value;
                _hashTable[pointerKey] = values;
            }
            else
            {
                _hashTable.Add(pointerKey, new[] { value });
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
