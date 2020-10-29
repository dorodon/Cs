using NUnit.Framework;
using PrimeNumbers;

namespace PrimitiveNUnitTest
{
    [TestFixture]
    public class PrimitiveNUnitTests
    {
        private PrimeService primeService;
        [SetUp]
        public void Setup()
        {
            primeService = new PrimeService();
        }

        [Test]
        public void Test1()
        {
            var res = primeService.IsPrime(-1);
            Assert.IsFalse(res, "-1 should not be prime");
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(9)]
        [TestCase(10)]
        public void Test2(int number)
        {
            var res = primeService.IsPrime(number);
            Assert.IsFalse(res, $"{number} should not be prime");
        }

        [TestCase(2)]
        [TestCase(3)]
        [TestCase(5)]
        [TestCase(137)]
        public void Test3(int number)
        {
            var res = primeService.IsPrime(number);
            Assert.IsTrue(res, $"{number} should be prime");
        }
    }
}