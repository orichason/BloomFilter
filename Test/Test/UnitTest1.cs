using BloomFilter;

namespace BloomFilterTest
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]

        [DataRow(new string[] {"hi", "bob", "hello", "idk", "ok"}, 30, 1)]
        public void Contains(string[] words, int cap, int seed)
        {
            Random random = new();

            int offset1 = random.Next(100);
            int offset2 = random.Next(10);

            Func<string, int> hashOne = (item) => item.GetHashCode();
            Func<string, int> hashTwo = (item) => item.GetHashCode() + offset1;
            Func<string, int> hashThree = (item) => item.GetHashCode() * offset2 + offset2;

            HashSet<Func<string, int>> hashFunctions = new() { hashOne, hashTwo, hashThree };

            BloomFilter<string> bloomFilter = new(cap, hashFunctions);

            foreach(var word in words)
            {
                bloomFilter.Insert(word);
            }

            foreach(var word in words)
            {
                Assert.IsTrue(bloomFilter.ProbablyContains(word));
            }
        }
    }
}