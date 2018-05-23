using FluentAssertions;
using NUnit.Framework;

namespace DataStructures.Tests
{
    [TestFixture]
    internal class RandomizedTreeShould
    {
        private RandomizedTree<int> _randomizedTree;

        [SetUp]
        public void SetUp() => _randomizedTree = new RandomizedTree<int>();

        [Test]
        public void AddFirstItem()
        {
            _randomizedTree.Insert(1);
            _randomizedTree.Count.Should().Be(1);
        }

        [Test]
        public void RemoveLief()
        {
            _randomizedTree.Insert(3);
            _randomizedTree.Insert(4);
            _randomizedTree.Remove(3);
            _randomizedTree.Contains(3).Should().BeFalse();
            _randomizedTree.Count.Should().Be(1);
        }

        [Test]
        public void EmptyTree()
        {
            _randomizedTree.Insert(10);
            _randomizedTree.Remove(10);
            _randomizedTree.Root.Should().Be(null);
            _randomizedTree.Count.Should().Be(0);
        } 
    }
}