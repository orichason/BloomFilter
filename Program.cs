using System.Reflection.Metadata.Ecma335;

namespace BloomFilter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Func<string, int> hashOne = (item) => item.GetHashCode();
            Func<string, int> hashTwo = (item) => item.GetHashCode() + 5;
            Func<string, int> hashThree = (item) => item.GetHashCode() * 3 - 21;

            HashSet<Func<string, int>> hashFunctions = new() { hashOne, hashTwo, hashThree };

            BloomFilter<string> bloomFilter = new(40, hashFunctions);
        }
    }
}
