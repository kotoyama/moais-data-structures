using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace DataStructures.Tests
{
    [TestFixture]
    internal class TrieShould
    {
        private Trie _trie;

        [SetUp]
        public void SetUp() => _trie = new Trie(new List<string>());

        [Test]
        public void AddWord()
        {
            _trie.Insert("hello");
            _trie.Contains("hello").Should().BeTrue();
        }

        [Test]
        public void EmptyTree()
        {
            _trie.Insert("hello");
            _trie.Remove("hello");
            _trie.Count.Should().Be(0);
        }

        [Test]
        public void WithoutWordInPrefix()
        {
            const string word = "hello";
            var letters = word.ToCharArray();
            var current = "";
            _trie.Insert(word);
            foreach (var letter in letters)
            {
                _trie.Contains(current).Should().BeFalse();
                current += letter;
            }
            _trie.Contains(current).Should().BeTrue();
        }

        [Test]
        public void CheckPrefixEquivalency()
        {
            _trie.Insert("hello");
            _trie.Insert("world");
            _trie.GetWordsWithPrefix("").Should().BeEquivalentTo("hello", "world");
        }
    }
}