using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace Queene.Core.MovesGenerating.Hashing
{
    public static class PerftTranspositionTable
    {
        public const int EntriesCount = 100000000;
        private static PerftTTEntry[] _entries;

        static Lock _lock = new();

        public static void Init()
        {
            _entries = new PerftTTEntry[EntriesCount];
        }

        public static void Add(ulong hash, int depth, ulong nodes)
        {
            var index = hash % (ulong)_entries.Length;
            var entry = _entries[index];

            using (var scope = _lock.EnterScope())
            {
                if (entry == null)
                {
                    _entries[index] = new PerftTTEntry
                    {
                        Hash = hash,
                        Depth = depth,
                        Nodes = nodes
                    };

                    return;
                }

                if (depth >= entry?.Depth)
                {
                    _entries[index] = new PerftTTEntry
                    {
                        Hash = hash,
                        Depth = depth,
                        Nodes = nodes
                    };
                }
            }
        }

        public static ulong? Get(ulong hash, int depth)
        {
            using (var scope = _lock.EnterScope())
            {
                var entry = _entries[hash % (ulong)_entries.Length];

                if (entry != null && entry.Hash == hash && entry.Depth == depth)
                    return entry.Nodes;

                return null;
            }
        }

        //------------------------------------------------

        //private static PerftTTEntry[] table = [];
        //private static ulong mask;

        //public static void Init(int sizePowerOfTwo = 33)
        //{
        //    int size = 1 << sizePowerOfTwo;
        //    table = new PerftTTEntry[size];
        //    mask = (ulong)size - 1UL;
        //}

        //public static bool TryGet(ulong key, int depth, out ulong nodes)
        //{
        //    int idx = (int)(key % (ulong)table.Length);
        //    var entry = table[idx];

        //    if (entry?.Hash == key && entry?.Depth == depth)
        //    {
        //        nodes = entry.Nodes; 
        //        return true;
        //    }

        //    nodes = 0;
        //    return false;
        //}

        //public static void Store(ulong key, int depth, ulong nodes)
        //{
        //    int idx = (int)((key % (ulong)table.Length));

        //    // simple replacement: prefer deeper entries
        //    if (table[idx] == null || depth >= table[idx].Depth)
        //        table[idx] = new PerftTTEntry
        //        {
        //            Hash = key, 
        //            Depth = depth, 
        //            Nodes = nodes
        //        };
        //}

        //------------------------------------------------

        //private static readonly ConcurrentDictionary<(ulong, int), ulong> table = new();

        //public static bool TryGet(ulong key, int depth, out ulong nodes)
        //    => table.TryGetValue((key, depth), out nodes);

        //public static void Store(ulong key, int depth, ulong nodes)
        //    => table[(key, depth)] = nodes;
    }
}
