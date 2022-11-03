using NUnit.Framework;
using Source.Common.Utils;

namespace Source.Common.Tests.Utils
{
    [TestFixture]
    public class LoopedCollectionTests
    {
        private LoopedCollection<int> collection;

        [SetUp]
        public void SetUp()
        {
            collection = new LoopedCollection<int> {1, 2, 3, 4, 5, 6, 7};
        }

        [TestCase(0, 1)]
        [TestCase(1, 2)]
        [TestCase(7, 1)]
        [TestCase(14, 1)]
        public void LoopedCollection_When_Get_Value_By_Index_Should_Return_Expected_Value(int index, int expected)
        {
            var result = collection[index];
            Assert.AreEqual(expected, result);
        }
    }
}