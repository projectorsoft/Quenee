using MemoryPack;
using System;

namespace Queene.Core.Magics
{
    [Serializable]
    [MemoryPackable]
    public partial class MagicResult
    {
        public UInt64 Magic { get; set; }
        public UInt64[] Attacks { get; set; }
        public UInt64 Mask { get; set; }
        public byte Shift { get; set; }
    }
}
