using System;
using System.Collections.Generic;

namespace DataStructures
{
    public class TrieNode
    {
        public Dictionary<char, TrieNode> Children { get; }
        public string Item { get; }
        public bool IsTerminal { get; set; }

        public TrieNode() : this("") { }

        public TrieNode(string item)
        {
            Item = item;
            IsTerminal = false;
            Children = new Dictionary<char, TrieNode>();
        }

        public void AddLetter(char letter)
        {
            if (Children.ContainsKey(letter)) throw new ArgumentException();
            Children[letter] = new TrieNode(Item + letter);
        }
    }

    public class Trie
    {
        private readonly TrieNode _trie;

        public Trie(IEnumerable<string> words)
        {
            _trie = new TrieNode("");
            foreach (var letter in words)
                Insert(letter);
        }

        public int Count { get; private set; }

        public bool Contains(string word)
        {
            if (string.IsNullOrWhiteSpace(word)) return false;
            var current = _trie;
            foreach (var letter in word)
            {
                if (!current.Children.ContainsKey(letter)) return false;
                current = current.Children[letter];
            }

            return current.IsTerminal;
        }

        public void Insert(string word)
        {
            var current = _trie;
            foreach (var letter in word)
            {
                if (!current.Children.ContainsKey(letter))
                    current.AddLetter(letter);
                current = current.Children[letter];
            }

            current.IsTerminal = true;
            Count++;
        }

        public void Remove(string word)
        {
            var node = GetNode(word);
            if (!node.IsTerminal) return;
            node.IsTerminal = false;
            Count--;
        }

        public List<string> GetWordsWithPrefix(string prefix)
        {
            var results = new List<string>();
            var current = _trie;
            foreach (var letter in prefix.ToCharArray())
                if (current.Children.ContainsKey(letter))
                    current = current.Children[letter];
                else
                    return results;
            GetAllChildWords(current, results);
            return results;
        }

        private static void GetAllChildWords(TrieNode node, ICollection<string> result)
        {
            if (node.IsTerminal) result.Add(node.Item);
            foreach (var child in node.Children.Keys)
                GetAllChildWords(node.Children[child], result);
        }

        private TrieNode GetNode(string word)
        {
            var current = _trie;
            foreach (var letter in word)
            {
                if (!current.Children.ContainsKey(letter)) return null;
                current = current.Children[letter];
            }
            return current;
        }
    }
}