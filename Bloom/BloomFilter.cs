using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloomFilter
{
    public class BloomFilter<T>
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
                int hashCode = Math.Abs(func.Invoke(item)) % cap;
                int indexInArray = hashCode / 8;
                int byteIndex = hashCode % 8;

                byte temp = 0b00000001;
                int amountToShift = 7 - byteIndex;
                filter[indexInArray] |= (byte)(temp << amountToShift);
            }
        }

        public bool ProbablyContains(T item)
        {
            foreach (var func in hashFunctions)
            {
                int hashCode = Math.Abs(func.Invoke(item)) % cap;
                int indexInArray = hashCode / 8;
                int byteIndex = hashCode % 8;

                int amountToShift = 7 - byteIndex;

               // byte mask = (byte)(1 << amountToShift);
                
                if ((byte)((filter[indexInArray] >> amountToShift) & 1) == 0) return false;
            }

            return true;
        }
    }
}
