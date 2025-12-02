using Queene.Core.Enums;
using System.Runtime.CompilerServices;

namespace Queene.Core.Models
{
    public class Player
    {
        public byte Current { get; private set; }

        public byte Oponnent { get; private set; }

        public static byte White => (byte)PlayerEnum.White;

        public static byte Black => (byte)PlayerEnum.Black;

        public Player(PlayerEnum player)
        {
            Set(player);
        }

        public void Set(PlayerEnum player)
        {
            Current = (byte)player;
            Oponnent = (byte)(Current ^ 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Change()
        {
            Oponnent = Current;
            Current = (byte)(Current ^ 1);
        }

        public override string ToString()
        {
            return Current == (byte)PlayerEnum.White ? "White" : "Black";
        }
    }
}
