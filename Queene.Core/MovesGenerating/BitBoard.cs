using Queene.Core.Consts;
using Queene.Core.Converters;
using Queene.Core.Enums;
using Queene.Core.Fen;
using Queene.Core.Models;
using Queene.Core.MovesGenerating.Hashing;
using Queene.Core.MovesGenerating.Pieces;
using Queene.Core.Utils;
using QueeneEngine.Engine.Magics;
using QueeneEngine.Helpers.Bitwise;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;

namespace Queene.Core.MovesGenerating
{
    public class BitBoard
	{
		private readonly BoardContext _context;
        private readonly ZorbistHash _zorbistHash;

        public BoardContext Context => _context;

		public BitBoard(IBitBoardContextConverter bitBoardContextConverter,
			ZorbistHash zorbistHash)
		{
			_context = new BoardContext(bitBoardContextConverter);
            _zorbistHash = zorbistHash;

            NewGame();
        }

		public void NewGame()
		{
			var boardState = FenHelper.CreateBoardState(FenHelper.FEN_INITIAL_START_POSITION);
			SetupBoard(boardState);
		}

		public void SetupBoard(BoardState state)
		{
			if (state is null)
				throw new ArgumentNullException($"{nameof(state)} cannot be null");

			ClearBoard();

			//TODO: setup rest board state information
			foreach (PieceTypeEnum piece in state.WhitePieces.Keys)
				SetupPieces(Player.White, piece, state.WhitePieces[piece]);

			foreach (PieceTypeEnum piece in state.BlackPieces.Keys)
				SetupPieces(Player.Black, piece, state.BlackPieces[piece]);

			if (!string.IsNullOrWhiteSpace(state.EnPassant))
				_context.EnPassantSquare = (byte)Array.IndexOf(BoardConsts.SQUARES_NAMES, state.EnPassant.ToUpper(CultureInfo.InvariantCulture));
			else
				_context.EnPassantSquare = null;

			_context.Player.Set(state.TurnToMove);

			UpdateAllPiecesMasks();
			UpdateBoardState();

            _context.SetOpponnentSliders();
			_context.InitPiecesList();

			var kingSquare = _context.PieceTypeList[_context.Player.Current][(byte)PieceTypeEnum.King].GetAtIndex(0);

			SetupCastlingRights(state);

            _context.Hash = _zorbistHash.CreateHash(_context);
            SetupCheckingSquaresAndAttackers(_context.Player, kingSquare);
            SetupPinnedPiecesAndPinners(_context.Player, kingSquare);
        }

		private void SetupCastlingRights(BoardState state)
		{
			_context.CanCastle[Player.Black] = _context.CanCastle[Player.White] = false;
			_context.CanCastleKingSide[Player.Black] = _context.CanCastleKingSide[Player.White] = false;
			_context.CanCastleQueenSide[Player.Black] = _context.CanCastleQueenSide[Player.White] = false;

			SetupCastlingRights(Player.Black, state.BlackCastlingRights);
			SetupCastlingRights(Player.White, state.WhiteCastlingRights);
			_context.FixCastlingRights();
		}

		private void SetupCastlingRights(byte player, CastleTypeEnum castleType)
		{
			switch (castleType)
			{
				case CastleTypeEnum.CastleKingSide:
					_context.CanCastle[player] = _context.CanCastleKingSide[player] = true;
					break;
				case CastleTypeEnum.CastleQueenSide:
					_context.CanCastle[player] = _context.CanCastleQueenSide[player] = true;
					break;
				case CastleTypeEnum.Both:
					_context.CanCastle[player] = _context.CanCastleQueenSide[player] = _context.CanCastleKingSide[player] = true;
					break;
			}
		}

		private void SetupPieces(byte player, PieceTypeEnum piece, List<byte> pieces)
		{
			switch (piece)
			{
				case PieceTypeEnum.Pawn:
					_context.Pieces[player][(byte)PieceTypeEnum.Pawn] = BitwiseHelper.SetMask(pieces);
					break;
				case PieceTypeEnum.Rook:
					_context.Pieces[player][(byte)PieceTypeEnum.Rook] = BitwiseHelper.SetMask(pieces);
					break;
				case PieceTypeEnum.Knight:
					_context.Pieces[player][(byte)PieceTypeEnum.Knight] = BitwiseHelper.SetMask(pieces);
					break;
				case PieceTypeEnum.Bishop:
					_context.Pieces[player][(byte)PieceTypeEnum.Bishop] = BitwiseHelper.SetMask(pieces);
					break;
				case PieceTypeEnum.Queen:
					_context.Pieces[player][(byte)PieceTypeEnum.Queen] = BitwiseHelper.SetMask(pieces);
					break;
				case PieceTypeEnum.King:
					_context.Pieces[player][(byte)PieceTypeEnum.King] = BitwiseHelper.SetMask(pieces);
					break;
			}
		}

		public void ClearBoard()
		{
			for (int i = (int)PlayerEnum.Black; i <= (int)PlayerEnum.White; i++)
				for (int j = (int)PieceTypeEnum.Pawn; j <= (int)PieceTypeEnum.Empty; j++)
					_context.Pieces[i][j] = 0;

			_context.OccupiedSquares = 0;
			_context.EmptySquares = 0;
			_context.Attackers = 0;
			_context.CheckedSquares = 0;
		}

		private void UpdateBoardState()
		{
			_context.OccupiedSquares = _context.Pieces[Player.White][(byte)PieceTypeEnum.All] | _context.Pieces[Player.Black][(byte)PieceTypeEnum.All];
			_context.EmptySquares = ~_context.OccupiedSquares;
		}

		private void UpdateAllPiecesMasks()
		{
			_context.Pieces[Player.White][(byte)PieceTypeEnum.All] = _context.Pieces[Player.White][(byte)PieceTypeEnum.Bishop] 
				| _context.Pieces[Player.White][(byte)PieceTypeEnum.King] 
				| _context.Pieces[Player.White][(byte)PieceTypeEnum.Knight] 
				| _context.Pieces[Player.White][(byte)PieceTypeEnum.Pawn] 
				| _context.Pieces[Player.White][(byte)PieceTypeEnum.Queen] 
				| _context.Pieces[Player.White][(byte)PieceTypeEnum.Rook];

			_context.Pieces[Player.Black][(byte)PieceTypeEnum.All] = _context.Pieces[Player.Black][(byte)PieceTypeEnum.Bishop] 
				| _context.Pieces[Player.Black][(byte)PieceTypeEnum.King] 
				| _context.Pieces[Player.Black][(byte)PieceTypeEnum.Knight] 
				| _context.Pieces[Player.Black][(byte)PieceTypeEnum.Pawn] 
				| _context.Pieces[Player.Black][(byte)PieceTypeEnum.Queen] 
				| _context.Pieces[Player.Black][(byte)PieceTypeEnum.Rook];
		}

		public void SetupCheckingSquaresAndAttackers(Player player, byte kingSquare)
		{
			_context.CheckedSquares = GetCheckedSquaresAndAttackers(player, kingSquare, out var attackers);
			_context.Attackers = attackers;
		}

		public void SetupPinnedPiecesAndPinners(Player player, byte kingSquare)
		{
			_context.PinnedSquares = GetPinnedPieces(player, kingSquare, out ulong pinners);
			_context.Pinners = pinners;
		}

        public static bool IsSquareAttacked(BoardContext context, Player player, byte square)
        {
            var opp = player.Oponnent;
            var oppPieces = context.Pieces[opp];
            ulong occupied = context.OccupiedSquares;

            if ((MovesContainer.KnightMoves[square] & oppPieces[(byte)PieceTypeEnum.Knight]) != 0)
                return true;

            var pawnAttackMask = player.Current == Player.White
                ? MovesContainer.PawnWhiteCaptures[square]
                : MovesContainer.PawnBlackCaptures[square];

            if ((pawnAttackMask & oppPieces[(byte)PieceTypeEnum.Pawn]) != 0)
                return true;

            if ((MovesContainer.KingMoves[square] & oppPieces[(byte)PieceTypeEnum.King]) != 0)
                return true;

            var diagAttacks = Slider.GetAttacks(occupied, MovesContainer.BishopMagics[square]);
            if ((diagAttacks & (oppPieces[(byte)PieceTypeEnum.Bishop] | oppPieces[(byte)PieceTypeEnum.Queen])) != 0)
                return true;

            var straightAttacks = Slider.GetAttacks(occupied, MovesContainer.RookMagics[square]);
            if ((straightAttacks & (oppPieces[(byte)PieceTypeEnum.Rook] | oppPieces[(byte)PieceTypeEnum.Queen])) != 0)
                return true;

            return false;
        }

        private ulong GetCheckedSquaresAndAttackers(Player player, byte square, out ulong attackers)
        {
            ulong result = 0;
            ulong occupied = _context.OccupiedSquares;

            ulong rookAttacks = Slider.GetAttacks(occupied, MovesContainer.RookMagics[square]) & _context.OpponentRooksAndQueens;
            ulong bishopAttacks = Slider.GetAttacks(occupied, MovesContainer.BishopMagics[square]) & _context.OpponentBishopsAndQueens;

            ulong knightAttacks = MovesContainer.KnightMoves[square] & _context.Pieces[player.Oponnent][(byte)PieceTypeEnum.Knight];
            ulong kingAttacks = MovesContainer.KingMoves[square] & _context.Pieces[player.Oponnent][(byte)PieceTypeEnum.King];

            ulong pawnAttacks = player.Current == Player.White
                ? MovesContainer.PawnWhiteCaptures[square] & _context.Pieces[player.Oponnent][(byte)PieceTypeEnum.Pawn]
                : MovesContainer.PawnBlackCaptures[square] & _context.Pieces[player.Oponnent][(byte)PieceTypeEnum.Pawn];

            attackers = rookAttacks | bishopAttacks | knightAttacks | kingAttacks | pawnAttacks;

			if (BitOperations.PopCount(attackers) > 1)
                return attackers;

            if (rookAttacks != 0)
            {
                ulong attacksToSquare = rookAttacks;
                while (attacksToSquare > 0)
                {
                    var toSquare = BitwiseHelper.FastBitScanForward(attacksToSquare);
                    result |= SquaresBetweenMasksGeneratorHelper.MasksBeetwenSquaresRanksAndFiles[toSquare][square];
                    attacksToSquare ^= Powers.powersOfTwo[toSquare];
                }
            }

            if (bishopAttacks != 0)
            {
                ulong attacksToSquare = bishopAttacks;
                while (attacksToSquare > 0)
                {
                    var toSquare = BitwiseHelper.FastBitScanForward(attacksToSquare);
                    result |= SquaresBetweenMasksGeneratorHelper.MasksBeetwenSquaresDiagonals[toSquare][square];
                    attacksToSquare ^= Powers.powersOfTwo[toSquare];
                }
            }

            result |= knightAttacks;
            result |= kingAttacks;
            result |= pawnAttacks;

            return result;
        }

        private ulong GetPinnedPieces(Player player, byte kingSquare, out ulong pinners)
        {
            var playerIdx = player.Current;
            ulong playerAll = _context.Pieces[playerIdx][(byte)PieceTypeEnum.All];
            ulong occupied = _context.OccupiedSquares;

            ulong rookPinners = Slider.GetXRayAttacks(occupied, playerAll, MovesContainer.RookMagics[kingSquare]) & _context.OpponentRooksAndQueens;

            ulong bishopPinners = Slider.GetXRayAttacks(occupied, playerAll, MovesContainer.BishopMagics[kingSquare]) & _context.OpponentBishopsAndQueens;

            pinners = rookPinners | bishopPinners;

            if (pinners == 0)
                return 0;

            ulong pinnedSquares = 0;

            ulong p = rookPinners;
            while (p != 0)
            {
                var toSquare = BitwiseHelper.FastBitScanForward(p);
                pinnedSquares |= SquaresBetweenMasksGeneratorHelper.MasksBeetwenSquaresRanksAndFiles[toSquare][kingSquare] & playerAll;
                p ^= Powers.powersOfTwo[toSquare];
            }

            p = bishopPinners;
            while (p != 0)
            {
                var toSquare = BitwiseHelper.FastBitScanForward(p);
                pinnedSquares |= SquaresBetweenMasksGeneratorHelper.MasksBeetwenSquaresDiagonals[toSquare][kingSquare] & playerAll;
                p ^= Powers.powersOfTwo[toSquare];
            }

            return pinnedSquares;
        }
    }
}
