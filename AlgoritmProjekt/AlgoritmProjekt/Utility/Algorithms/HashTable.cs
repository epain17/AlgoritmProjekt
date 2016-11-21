using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Utility.Algorithms
{
    internal class Entry
    {
        internal object Key { get; set; }
        internal object Value { get; set; }

        /// <summary>
        /// Compares two keys
        /// </summary>
        /// <param name="obj">The instance of the object whose key is to be used</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            Entry keyValue = (Entry)obj;
            return Key.Equals(keyValue.Key);
        }

        /// <summary>
        /// Constructor containing key and value
        /// </summary>
        /// <param name="key">Unique key to this object</param>
        /// <param name="value">A value held by this object</param>
        public Entry(object key, object value)
        {
            this.Key = key;
            this.Value = value;
        }
    }
    class HashTable
    {
        LinkedList<object> insertionOrder = new LinkedList<object>();
        List<Entry> descOrder = new List<Entry>();
        LinkedList<Entry>[] table;

        /// <summary>
        /// The algorithm determining the index of desired key
        /// </summary>
        /// <param name="key">Unique key</param>
        /// <returns>The hashed position/index</returns>
        private int HashIndex(object key)
        {
            int hashCode = key.GetHashCode();
            hashCode = hashCode % table.Length;
            return (hashCode < 0) ? -hashCode : hashCode;
        }

        /// <summary>
        /// Constructor creates a new instance of the table
        /// </summary>
        /// <param name="size">The size of the new table</param>
        public HashTable(int size)
        {
            table = new LinkedList<Entry>[size];
            for (int i = 0; i < size; i++)
            {
                table[i] = new LinkedList<Entry>();
            }
        }

        /// <summary>
        /// Finds a specific value
        /// </summary>
        /// <param name="key">The key used to search for specific value</param>
        /// <returns>The value in a specific key</returns>
        public object GetValue(object key)
        {
            int hashIndex = HashIndex(key);
            if (table[hashIndex].Contains(new Entry(key, null)))
            {
                Entry entry = table[hashIndex].Find(new Entry(key, null)).Value;
                return entry.Value;
            }
            return null;
        }

        /// <summary>
        /// Counts all the objects in the table
        /// </summary>
        /// <returns>returns the length of the table</returns>
        public int Count()
        {
            return insertionOrder.Count();
        }

        /// <summary>
        /// Adds a new object where user determines key and value
        /// </summary>
        /// <param name="key">Unique identifier for the instance of this key</param>
        /// <param name="value">The value the new object will hold</param>
        public void Push(object key, object value)
        {
            int hashIndex = HashIndex(key);

            if (!table[hashIndex].Contains(new Entry(key, null)))
            {
                Entry entry = new Entry(key, value);
                table[hashIndex].AddFirst(entry);
                insertionOrder.AddLast(value);
            }
            else
            {
                while(table[hashIndex].Contains(new Entry(key, null)))
                {
                    hashIndex++;
                    if (hashIndex == table.Length - 1)
                        hashIndex = 0;
                }
                Entry entry = new Entry(key, value);
                table[hashIndex].AddFirst(entry);
                insertionOrder.AddLast(value);

            }
        }

        /// <summary>
        /// Removes the current key
        /// </summary>
        /// <param name="key"></param>
        public void Remove(object key)
        {
            int hashIndex = HashIndex(key);
            if (table[hashIndex].Contains(new Entry(key, null)))
            {
                insertionOrder.Remove(GetValue(key));
                table[hashIndex].Remove(new Entry(key, null));
            }
        }

        /// <summary>
        /// Returns the sorted order of current table
        /// </summary>
        /// <returns></returns>
        public LinkedList<object> GetValueList()
        {
            if (insertionOrder != null)
                return insertionOrder;
            return null;
        }

        
        public void SortDescending()
        {
            
        }
    }
}
