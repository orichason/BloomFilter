using BloomFilter;

using Newtonsoft.Json.Bson;

namespace BloomFilterTest
{
    [TestClass]
    public class UnitTest1
    {
        BloomFilter<string> GetBloomFilter(int cap)
        {
            Random random = new();

            int offset1 = random.Next(100);
            int offset2 = random.Next(10);

            Func<string, int> hashOne = (item) => item.GetHashCode();
            Func<string, int> hashTwo = (item) => item.GetHashCode() + offset1;
            Func<string, int> hashThree = (item) => item.GetHashCode() * offset2 + offset2;

            return new(cap, new() { hashOne, hashTwo, hashThree });
        }
        string[] GenerateRandomWords(int amountToGenerate)
        {
            Random random = new(1);

            int wordsGenerated = 0;

            var randomWords = Enumerable.Repeat<string>("", amountToGenerate).ToArray();

            while (wordsGenerated < amountToGenerate)
            {
                int letterCount = random.Next(1, 10);
                for (int i = 0; i < letterCount; i++)
                {
                    randomWords[wordsGenerated] += (char)random.Next(97, 123);
                }
                wordsGenerated++;
            }

            return randomWords;
        }

        [TestMethod]
        [DataRow(4, 30, 2)]
        [DataRow(6, 50, 1)]
        [DataRow(5, 40, 5)]

        public void CheckContains(int wordCount, int cap, int seed) => Contains(GenerateRandomWords(wordCount), cap, seed);

        [TestMethod]

        [DataRow(new string[] { "ok, bob, joe, loop, apple" }, 30, 1)]
        [DataRow(new string[] { "bla", "man", "yo", "okay", "bye" }, 40, 3)]
        public void Contains(string[] words, int cap, int seed)
        {
            var bloomFilter = GetBloomFilter(cap);

            foreach (var word in words)
            {
                bloomFilter.Insert(word);
            }

            foreach (var word in words)
            {
                Assert.IsTrue(bloomFilter.ProbablyContains(word));
            }
        }

        [TestMethod]
        [DataRow(new string[] { "ok, bob, joe, loop, apple" }, "hello", 30)]
        [DataRow(new string[] { "bla", "man", "yo", "okay", "bye" }, "banana", 40)]
        public void DoesntContain(string[] words, string notContainedWord, int cap)
        {
            var bloomFilter = GetBloomFilter(cap);

            foreach (var word in words)
            {
                bloomFilter.Insert(word);
            }

            Assert.IsFalse(bloomFilter.ProbablyContains(notContainedWord));

        }
    }
}