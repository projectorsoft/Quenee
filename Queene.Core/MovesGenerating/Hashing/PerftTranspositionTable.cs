using System;

namespace Queene.Core.MovesGenerating.Hashing
{
    public static class PerftTranspositionTable
    {
        private static PerftTTEntry[] table;
        private static ulong mask;

        public static void Init(int sizePowerOfTwo = 20)
        {
            if (sizePowerOfTwo < 1 || sizePowerOfTwo > 30)
                throw new ArgumentOutOfRangeException(nameof(sizePowerOfTwo), "sizePowerOfTwo must be between 1 and 30 (table size fits into an int).");

            ulong size = 1UL << sizePowerOfTwo; // compute in 64-bit to avoid overflow
            if (size > int.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(sizePowerOfTwo), "Requested table size is too large to allocate.");

            table = new PerftTTEntry[(int)size];
            mask = size - 1UL;
        }

        public static bool TryGet(ulong key, int depth, out ulong nodes)
        {
            int idx = (int)(key & mask);
            var entry = table[idx];

            if (entry?.Hash == key && entry?.Depth == depth)
            {
                nodes = entry.Nodes;
                return true;
            }

            nodes = 0;
            return false;
        }

        public static void Store(ulong key, int depth, ulong nodes)
        {
            int idx = (int)(key & mask);

            // simple replacement: prefer deeper entries
            var existing = table[idx];
            if (existing == null || depth >= existing.Depth)
            {
                table[idx] = new PerftTTEntry
                {
                    Hash = key,
                    Depth = depth,
                    Nodes = nodes
                };
            }
        }
    }
}
