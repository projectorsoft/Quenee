using Queene.Core.Consts;
using Queene.Core.Enums;
using System;
using System.Runtime.CompilerServices;

namespace Queene.Core.Models
{
    public struct Move : IEquatable<Move>
    {
        private  ushort _data;

        public Move(byte from, byte to, MoveTypeEnum moveType, PieceTypeEnum promotionPieceType = PieceTypeEnum.Knight)
        {
            _data = (ushort)(to | (from << 6) | (byte)moveType << 12 | (byte)(promotionPieceType) << 14);
        }

        public void Set(byte from, byte to, MoveTypeEnum moveType, PieceTypeEnum promotionPieceType = PieceTypeEnum.Knight)
        {
            _data = (ushort)(to | (from << 6) | (byte)moveType << 12 | (byte)(promotionPieceType) << 14);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public byte GetFromSquare() => 
            (byte)((_data >> 6) & 0x3F);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public byte GetToSquare() => 
            (byte)(_data & 0x3F);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MoveTypeEnum GetMoveType() => 
            (MoveTypeEnum)((_data >> 12) & 3);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PieceTypeEnum GetPromotionPieceType() =>
            (PieceTypeEnum)(((_data >> 14) & 3));

        public static bool operator ==(Move left, Move right) => 
            left._data == right._data;

        public static bool operator !=(Move left, Move right) => 
            left._data != right._data;

        public bool IsType(MoveTypeEnum moveType) => GetMoveType() == moveType;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEnPassantMove() => GetMoveType() == MoveTypeEnum.EnPassante;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsCastleMove() => GetMoveType() == MoveTypeEnum.Castle;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsPromotionMove() => GetMoveType() == MoveTypeEnum.Promotion;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNullMove()
            => _data == 0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValidMove()
            => GetFromSquare() != GetToSquare();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Move other)
            => _data == other._data;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
            => obj is Move move && Equals(move);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
            => _data;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return $"{BoardConsts.SQUARES_NAMES[GetFromSquare()]}-{BoardConsts.SQUARES_NAMES[GetToSquare()]}";
        }
    }
}
