using System.Reflection.Metadata.Ecma335;

namespace BloomFilter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Func<string, int> hashOne = (item) => item.GetHashCode();
            Func<string, int> hashTwo = (item) => item.GetHashCode() + 9;
            Func<string, int> hashThree = (item) => item.GetHashCode() * 4 + 23;

            HashSet<Func<string, int>> hashFunctions = new() { hashOne , hashTwo, hashThree };

            BloomFilter<string> bloomFilter = new(40, hashFunctions);

            bloomFilter.Insert("hello");
            bloomFilter.Insert("hi");
            bloomFilter.Insert("bye");
            bloomFilter.Insert("apple");

            Console.WriteLine(bloomFilter.ProbablyContains("hello"));
            Console.WriteLine(bloomFilter.ProbablyContains("apple"));
            Console.WriteLine(bloomFilter.ProbablyContains("hi"));
            Console.WriteLine(bloomFilter.ProbablyContains("bye"));

        }
    }
}
