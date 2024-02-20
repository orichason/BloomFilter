using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloomFilter
{
    internal class BloomFilter<T>
    {
        int cap;
        byte[] filter;
        HashSet<Func<T, int>> hashFunctions;
        public BloomFilter(int cap, HashSet<Func<T, int>> hashFunctions)
        {
            this.cap = cap;
            filter = new byte[cap];
            this.hashFunctions = hashFunctions;
        }

        public void Insert(T item)
        {
            foreach (var func in hashFunctions)
            {
                int hashCode = func.Invoke(item);
                int position = hashCode / 2;
                int byteIndex = hashCode % 8;

                //TODO: bitwise shift to change bits inside byte 
            }
        }

        public bool ProbablyContains(T item)
        {

        }
    }
}
