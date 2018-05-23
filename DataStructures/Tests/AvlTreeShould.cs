using FluentAssertions;
using NUnit.Framework;

namespace DataStructures.Tests
{
    [TestFixture]
    internal class AvlTreeShould
    {
        private AvlTree<int> _avlTree;

        [SetUp]
        public void SetUp() => _avlTree = new AvlTree<int>();

        [Test]
        public void AddFirstItem()
        {
            _avlTree.Insert(1);
            _avlTree.Root.Item.Should().Be(1);
            _avlTree.Count.Should().Be(1);
        }

        [Test]
        public void AddSameItems()
        {
            var item = 42;
            for (var i = 0; i < 42; i++)
                _avlTree.Insert(item);
            _avlTree.Contains(item).Should().BeTrue();
            _avlTree.Count.Should().Be(42);
        }

        [Test]
        public void RemoveItem()
        {
            for (var i = 1; i < 10; i++)
                _avlTree.Insert(i);
            _avlTree.Remove(5);
            _avlTree.Contains(5).Should().BeFalse();
        }

        [Test]
        public void EmptyTree()
        {
            _avlTree.Insert(10);
            _avlTree.Remove(10);
            _avlTree.Root.Should().Be(null);
            _avlTree.Count.Should().Be(0);
        }
    }
}