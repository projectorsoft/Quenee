using Queene.Core.Enums;
using QueeneEngine.Common;
using System;

namespace Queene.Core.MovesGenerating.Hashing
{
    public class ZorbistHash
    {
        public ulong[][][] Pieces { get; private set; } = []; //[player][pieceType][square]
        public ulong[][] EnPassante { get; private set; } = []; //[player][square]
        public ulong[] Player { get; private set; } = [];
        public ulong[] KingSideCastle { get; private set; } = [];
        public ulong[] QueenSideCastle { get; private set; } = [];

        private readonly Random _rnd = new();

        public ZorbistHash()
        {
            Init();
        }

        public void Init()
        {
            Pieces = new ulong[2][][];

            for (PlayerEnum player = PlayerEnum.Black; player <= PlayerEnum.White; player++)
            {
                Pieces[(byte)player] = new ulong[6][];
                for (PieceTypeEnum piece = PieceTypeEnum.Knight; piece <= PieceTypeEnum.King; piece++)
                {
                    Pieces[(byte)player][(byte)piece] = new ulong[64];

                    for (int square = 0; square < 64; square++)
                        Pieces[(byte)player][(byte)piece][square] = _rnd.NextULong();
                }
            }

            KingSideCastle = new ulong[2];
            KingSideCastle[0] = _rnd.NextULong();
            KingSideCastle[1] = _rnd.NextULong();

            QueenSideCastle = new ulong[2];
            QueenSideCastle[0] = _rnd.NextULong();
            QueenSideCastle[1] = _rnd.NextULong();

            EnPassante = new ulong[2][];

            for (PlayerEnum player = PlayerEnum.Black; player <= PlayerEnum.White; player++)
            {
                EnPassante[(byte)player] = new ulong[8];
                for (int i = 0; i < 8; i++)
                    EnPassante[(byte)player][i] = _rnd.NextULong();
            }

            Player = new ulong[2];

            for (PlayerEnum player = PlayerEnum.Black; player <= PlayerEnum.White; player++)
                Player[(byte)player] = _rnd.NextULong();
        }

        public ulong CreateHash(BoardContext context)
        {
            ulong hash = 0;

            hash ^= Player[context.Player.Current];

            for (PlayerEnum player = PlayerEnum.Black; player <= PlayerEnum.White; player++)
            {
                for (PieceTypeEnum piece = PieceTypeEnum.Knight; piece <= PieceTypeEnum.King; piece++)
                    for (int i = 0; i < context.PieceTypeList[(byte)player][(byte)piece].Count(); i++)
                    {
                        var square = context.PieceTypeList[(byte)player][(byte)piece].GetAtIndex(i);
                        hash ^= Pieces[(byte)player][(byte)piece][square];
                    }

                if (context.CanCastleKingSide[(byte)player])
                    hash ^= KingSideCastle[(byte)player];

                if (context.CanCastleQueenSide[(byte)player])
                    hash ^= QueenSideCastle[(byte)player];
            }

            if (context.EnPassantSquare.HasValue)
            {
                if (context.Player.Current == Models.Player.White)
                    hash ^= EnPassante[Models.Player.White][context.EnPassantSquare.Value - 40];
                else
                    hash ^= EnPassante[Models.Player.Black][context.EnPassantSquare.Value - 16];
            }

            return hash;
        }
    }
}
