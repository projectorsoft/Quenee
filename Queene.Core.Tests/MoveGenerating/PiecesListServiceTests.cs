//using FluentAssertions;
//using Queene.Core.Enums;
//using Queene.Core.Fen;
//using Queene.Core.Models;
//using Queene.Core.MovesGenerating.Pieces;
//using Queene.Core.MovesGenerating.PiecesList;
//using System.Linq;
//using Xunit;

//namespace Queene.Core.Tests.MoveGenerating
//{
//    public class PiecesListServiceTests : TestsBase
//    {
//        public PiecesListServiceTests() : base()
//        {
//        }

//        [Theory(DisplayName = "Should make normal move")]
//        [InlineData("r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 1")]
//        public void ShouldMakeNormalMove(string fen)
//        {
//            //Arrange
//            var boardState = FenHelper.CreateBoardState(fen);
//            _bitBoard.SetupBoard(boardState);

//            var move = new Move(36, 44, MoveTypeEnum.Move);
//            var extMove = new ExtendedMove(move, _bitBoard.Context);

//            var piecesListService = new PiecesListService(_bitBoard.Context);

//            //Act
//            piecesListService.MakeMove(extMove);

//            //Assert
//            var index = _bitBoard.Context.PieceIndices[_bitBoard.Context.Player.Current][extMove.From];
//            _bitBoard.Context.PieceIndices[_bitBoard.Context.Player.Current][extMove.To].Should().Be(index);

//            var piece = _bitBoard.Context.PieceTypeList[_bitBoard.Context.Player.Current][(byte)extMove.PieceType].GetAtIndex(index.Index);
//            piece.PieceType.Should().Be(extMove.PieceType);
//            piece.Square.Should().Be(extMove.To);
//        }

//        [Theory(DisplayName = "Should unmake normal move")]
//        [InlineData("r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 1")]
//        public void ShouldUnmakeNormalMove(string fen)
//        {
//            //Arrange
//            var boardState = FenHelper.CreateBoardState(fen);
//            _bitBoard.SetupBoard(boardState);

//            var move = new Move(36, 44, MoveTypeEnum.Move);
//            var extMove = new ExtendedMove(move, _bitBoard.Context);

//            var piecesListService = new PiecesListServiceTmp(_bitBoard.Context);

//            //Act
//            piecesListService.MakeMove(extMove);
//            piecesListService.UnmakeMove(extMove);

//            //Assert
//            var index = _bitBoard.Context.PiecesIndexesTmp[_bitBoard.Context.Player.Current][extMove.To];
//            _bitBoard.Context.PiecesIndexesTmp[_bitBoard.Context.Player.Current][extMove.From].Should().Be(index);

//            var piece = _bitBoard.Context.PiecesListTmp[_bitBoard.Context.Player.Current][(byte)extMove.PieceType].GetAtIndex(index);
//            piece.PieceType.Should().Be(extMove.PieceType);
//            piece.Square.Should().Be(extMove.From);
//        }

//        [Theory(DisplayName = "Should make capture")]
//        [InlineData("rnb1qk1r/pp1Pbppp/2p5/8/P1B5/8/1PP1NnPP/RNBQK2R w KQ - 0 2")]
//        public void ShouldMakeCapture(string fen)
//        {
//            //Arrange
//            var boardState = FenHelper.CreateBoardState(fen);
//            _bitBoard.SetupBoard(boardState);

//            var move = new Move(3, 10, MoveTypeEnum.Move);
//            var extMove = new ExtendedMove(move, _bitBoard.Context);

//            var piecesListService = new PiecesListServiceTmp(_bitBoard.Context);

//            //Act
//            piecesListService.MakeMove(extMove);

//            //Assert
//            var index = _bitBoard.Context.PiecesIndexesTmp[_bitBoard.Context.Player.Current][extMove.From];
//            _bitBoard.Context.PiecesIndexesTmp[_bitBoard.Context.Player.Current][extMove.To].Should().Be(index);

//            var piece = _bitBoard.Context.PiecesListTmp[_bitBoard.Context.Player.Current][(byte)extMove.PieceType].GetAtIndex(index);
//            piece.PieceType.Should().Be(extMove.PieceType);
//            piece.Square.Should().Be(extMove.To);

//            _bitBoard.Context.PiecesListTmp[_bitBoard.Context.Player.Oponnent][(byte)extMove.Captured].Get()
//                .ToList().Select(s => s.Square).Should().NotContain(extMove.To);
//        }

//        [Theory(DisplayName = "Should unmake capture")]
//        [InlineData("rnb1qk1r/pp1Pbppp/2p5/8/P1B5/8/1PP1NnPP/RNBQK2R w KQ - 0 2")]
//        public void ShouldUnmakeCapture(string fen)
//        {
//            //Arrange
//            var boardState = FenHelper.CreateBoardState(fen);
//            _bitBoard.SetupBoard(boardState);

//            var move = new Move(3, 10, MoveTypeEnum.Move);
//            var extMove = new ExtendedMove(move, _bitBoard.Context);

//            var piecesListService = new PiecesListServiceTmp(_bitBoard.Context);

//            //Act
//            piecesListService.MakeMove(extMove);
//            piecesListService.UnmakeMove(extMove);

//            //Assert
//            var index = _bitBoard.Context.PiecesIndexesTmp[_bitBoard.Context.Player.Current][extMove.To];
//            _bitBoard.Context.PiecesIndexesTmp[_bitBoard.Context.Player.Current][extMove.From].Should().Be(index);

//            var piece = _bitBoard.Context.PiecesListTmp[_bitBoard.Context.Player.Current][(byte)extMove.PieceType].GetAtIndex(index);
//            piece.PieceType.Should().Be(extMove.PieceType);
//            piece.Square.Should().Be(extMove.From);

//            _bitBoard.Context.PiecesListTmp[_bitBoard.Context.Player.Oponnent][(byte)extMove.Captured.Value].Get()
//                .ToList().Select(s => s.Square).Should().Contain(extMove.To);
//            _bitBoard.Context.PiecesIndexesTmp[_bitBoard.Context.Player.Oponnent][extMove.To].Should().Be((byte)(_bitBoard.Context.PiecesListTmp[_bitBoard.Context.Player.Oponnent][(byte)extMove.Captured.Value].Count - 1));
//        }

//        [Theory(DisplayName = "Should make normal promotion move")]
//        [InlineData("rnb1qk1r/pp1Pbppp/2p5/8/P1B5/8/1PP1NnPP/RNBQK2R w KQ - 0 2", PieceTypeEnum.Knight)]
//        [InlineData("rnb1qk1r/pp1Pbppp/2p5/8/P1B5/8/1PP1NnPP/RNBQK2R w KQ - 0 2", PieceTypeEnum.Bishop)]
//        [InlineData("rnb1qk1r/pp1Pbppp/2p5/8/P1B5/8/1PP1NnPP/RNBQK2R w KQ - 0 2", PieceTypeEnum.Rook)]
//        [InlineData("rnb1qk1r/pp1Pbppp/2p5/8/P1B5/8/1PP1NnPP/RNBQK2R w KQ - 0 2", PieceTypeEnum.Queen)]
//        public void ShouldMakeNormalPromotionMove(string fen, PieceTypeEnum promotedTo)
//        {
//            //Arrange
//            var boardState = FenHelper.CreateBoardState(fen);
//            _bitBoard.SetupBoard(boardState);

//            var move = new Move(52, 60, MoveTypeEnum.Promotion, promotedTo);
//            var extMove = new ExtendedMove(move, _bitBoard.Context);

//            var piecesListService = new PiecesListServiceTmp(_bitBoard.Context);

//            //Act
//            piecesListService.MakeMove(extMove);

//            //Assert
//            var index = _bitBoard.Context.PiecesListTmp[_bitBoard.Context.Player.Current][(byte)extMove.PromotedTo].Count - 1;
//            _bitBoard.Context.PiecesIndexesTmp[_bitBoard.Context.Player.Current][extMove.To].Should().Be((byte)index);

//            _bitBoard.Context.PiecesListTmp[_bitBoard.Context.Player.Current][(byte)extMove.PieceType].Get()
//                .ToList().Select(s => s.Square).Should().NotContain(extMove.From);

//            var piece = _bitBoard.Context.PiecesListTmp[_bitBoard.Context.Player.Current][(byte)extMove.PromotedTo].GetAtIndex(index);
//            piece.PieceType.Should().Be(extMove.PromotedTo);
//            piece.Square.Should().Be(extMove.To);
//        }

//        [Theory(DisplayName = "Should unmake normal promotion move")]
//        [InlineData("rnb1qk1r/pp1Pbppp/2p5/8/P1B5/8/1PP1NnPP/RNBQK2R w KQ - 0 2", PieceTypeEnum.Knight)]
//        [InlineData("rnb1qk1r/pp1Pbppp/2p5/8/P1B5/8/1PP1NnPP/RNBQK2R w KQ - 0 2", PieceTypeEnum.Bishop)]
//        [InlineData("rnb1qk1r/pp1Pbppp/2p5/8/P1B5/8/1PP1NnPP/RNBQK2R w KQ - 0 2", PieceTypeEnum.Rook)]
//        [InlineData("rnb1qk1r/pp1Pbppp/2p5/8/P1B5/8/1PP1NnPP/RNBQK2R w KQ - 0 2", PieceTypeEnum.Queen)]
//        public void ShouldUnmakeNormalPromotionMove(string fen, PieceTypeEnum promotedTo)
//        {
//            //Arrange
//            var boardState = FenHelper.CreateBoardState(fen);
//            _bitBoard.SetupBoard(boardState);

//            var move = new Move(52, 60, MoveTypeEnum.Promotion, promotedTo);
//            var extMove = new ExtendedMove(move, _bitBoard.Context);

//            var piecesListService = new PiecesListServiceTmp(_bitBoard.Context);

//            //Act
//            piecesListService.MakeMove(extMove);
//            piecesListService.UnmakeMove(extMove);

//            //Assert
//            var index = _bitBoard.Context.PiecesListTmp[_bitBoard.Context.Player.Current][(byte)extMove.PieceType].Count - 1;
//            _bitBoard.Context.PiecesIndexesTmp[_bitBoard.Context.Player.Current][extMove.From].Should().Be((byte)index);

//            _bitBoard.Context.PiecesListTmp[_bitBoard.Context.Player.Current][(byte)extMove.PromotedTo].Get()
//                .ToList().Select(s => s.Square).Should().NotContain(extMove.To);

//            var piece = _bitBoard.Context.PiecesListTmp[_bitBoard.Context.Player.Current][(byte)extMove.PieceType].GetAtIndex(index);
//            piece.PieceType.Should().Be(extMove.PieceType);
//            piece.Square.Should().Be(extMove.From);
//        }

//        [Theory(DisplayName = "Should make promotion move with capture")]
//        [InlineData("rnb1qk1r/pp1Pbppp/2p5/8/P1B5/8/1PP1NnPP/RNBQK2R w KQ - 0 2", PieceTypeEnum.Knight)]
//        [InlineData("rnb1qk1r/pp1Pbppp/2p5/8/P1B5/8/1PP1NnPP/RNBQK2R w KQ - 0 2", PieceTypeEnum.Bishop)]
//        [InlineData("rnb1qk1r/pp1Pbppp/2p5/8/P1B5/8/1PP1NnPP/RNBQK2R w KQ - 0 2", PieceTypeEnum.Rook)]
//        [InlineData("rnb1qk1r/pp1Pbppp/2p5/8/P1B5/8/1PP1NnPP/RNBQK2R w KQ - 0 2", PieceTypeEnum.Queen)]
//        public void ShouldMakePromotionMoveWithCapture(string fen, PieceTypeEnum promotedTo)
//        {
//            //Arrange
//            var boardState = FenHelper.CreateBoardState(fen);
//            _bitBoard.SetupBoard(boardState);

//            var move = new Move(52, 61, MoveTypeEnum.Promotion, promotedTo);
//            var extMove = new ExtendedMove(move, _bitBoard.Context);

//            var piecesListService = new PiecesListServiceTmp(_bitBoard.Context);

//            //Act
//            piecesListService.MakeMove(extMove);

//            //Assert
//            var index = _bitBoard.Context.PiecesListTmp[_bitBoard.Context.Player.Current][(byte)extMove.PromotedTo].Count - 1;
//            _bitBoard.Context.PiecesIndexesTmp[_bitBoard.Context.Player.Current][extMove.To].Should().Be((byte)index);

//            _bitBoard.Context.PiecesListTmp[_bitBoard.Context.Player.Current][(byte)extMove.PieceType].Get()
//                .ToList().Select(s => s.Square).Should().NotContain(extMove.From);

//            var piece = _bitBoard.Context.PiecesListTmp[_bitBoard.Context.Player.Current][(byte)extMove.PromotedTo].GetAtIndex(index);
//            piece.PieceType.Should().Be(extMove.PromotedTo);
//            piece.Square.Should().Be(extMove.To);

//            _bitBoard.Context.PiecesListTmp[_bitBoard.Context.Player.Oponnent][(byte)extMove.Captured].Get()
//                .ToList().Select(s => s.Square).Should().NotContain(extMove.To);
//        }

//        [Theory(DisplayName = "Should unmake promotion move with capture")]
//        [InlineData("rnb1qk1r/pp1Pbppp/2p5/8/P1B5/8/1PP1NnPP/RNBQK2R w KQ - 0 2", PieceTypeEnum.Knight)]
//        [InlineData("rnb1qk1r/pp1Pbppp/2p5/8/P1B5/8/1PP1NnPP/RNBQK2R w KQ - 0 2", PieceTypeEnum.Bishop)]
//        [InlineData("rnb1qk1r/pp1Pbppp/2p5/8/P1B5/8/1PP1NnPP/RNBQK2R w KQ - 0 2", PieceTypeEnum.Rook)]
//        [InlineData("rnb1qk1r/pp1Pbppp/2p5/8/P1B5/8/1PP1NnPP/RNBQK2R w KQ - 0 2", PieceTypeEnum.Queen)]
//        public void ShouldUnmakePromotionMoveWithCapture(string fen, PieceTypeEnum promotedTo)
//        {
//            //Arrange
//            var boardState = FenHelper.CreateBoardState(fen);
//            _bitBoard.SetupBoard(boardState);

//            var move = new Move(52, 61, MoveTypeEnum.Promotion, promotedTo);
//            var extMove = new ExtendedMove(move, _bitBoard.Context);

//            var piecesListService = new PiecesListServiceTmp(_bitBoard.Context);

//            //Act
//            piecesListService.MakeMove(extMove);
//            piecesListService.UnmakeMove(extMove);

//            //Assert
//            var index = _bitBoard.Context.PiecesListTmp[_bitBoard.Context.Player.Current][(byte)extMove.PieceType].Count - 1;
//            _bitBoard.Context.PiecesIndexesTmp[_bitBoard.Context.Player.Current][extMove.From].Should().Be((byte)index);

//            _bitBoard.Context.PiecesListTmp[_bitBoard.Context.Player.Current][(byte)extMove.PromotedTo].Get()
//                .ToList().Select(s => s.Square).Should().NotContain(extMove.To);

//            var piece = _bitBoard.Context.PiecesListTmp[_bitBoard.Context.Player.Current][(byte)extMove.PieceType].GetAtIndex(index);
//            piece.PieceType.Should().Be(extMove.PieceType);
//            piece.Square.Should().Be(extMove.From);

//            _bitBoard.Context.PiecesListTmp[_bitBoard.Context.Player.Oponnent][(byte)extMove.Captured.Value].Get()
//                .ToList().Select(s => s.Square).Should().Contain(extMove.To);
//            _bitBoard.Context.PiecesIndexesTmp[_bitBoard.Context.Player.Oponnent][extMove.To].Should().Be((byte)(_bitBoard.Context.PiecesListTmp[_bitBoard.Context.Player.Oponnent][(byte)extMove.Captured.Value].Count - 1));
//        }

//        [Theory(DisplayName = "Should make enPassante")]
//        [InlineData("rnb1qk1r/p2Pbppp/2p5/Pp6/2B5/8/1PP1NnPP/RNBQK2R w KQ B6 0 3")]
//        public void ShouldMakeEnPassante(string fen)
//        {
//            //Arrange
//            var boardState = FenHelper.CreateBoardState(fen);
//            _bitBoard.SetupBoard(boardState);

//            var move = new Move(39, 46, MoveTypeEnum.EnPassante);
//            var extMove = new ExtendedMove(move, _bitBoard.Context);

//            var piecesListService = new PiecesListServiceTmp(_bitBoard.Context);

//            //Act
//            piecesListService.MakeMove(extMove);

//            //Assert
//            var index = _bitBoard.Context.PiecesIndexesTmp[_bitBoard.Context.Player.Current][extMove.From];
//            _bitBoard.Context.PiecesIndexesTmp[_bitBoard.Context.Player.Current][extMove.To].Should().Be(index);

//            var piece = _bitBoard.Context.PiecesListTmp[_bitBoard.Context.Player.Current][(byte)extMove.PieceType].GetAtIndex(index);
//            piece.PieceType.Should().Be(extMove.PieceType);
//            piece.Square.Should().Be(extMove.To);

//            var capturedSquare = _bitBoard.Context.Player.Current == Player.White ? (byte)(extMove.To - 8) : (byte)(extMove.To + 8);
//            _bitBoard.Context.PiecesListTmp[_bitBoard.Context.Player.Oponnent][(byte)extMove.Captured].Get()
//                .ToList().Select(s => s.Square).Should().NotContain(capturedSquare);
//        }

//        [Theory(DisplayName = "Should unmake enPassante")]
//        [InlineData("rnb1qk1r/p2Pbppp/2p5/Pp6/2B5/8/1PP1NnPP/RNBQK2R w KQ B6 0 3")]
//        public void ShouldUnmakeEnPassante(string fen)
//        {
//            //Arrange
//            var boardState = FenHelper.CreateBoardState(fen);
//            _bitBoard.SetupBoard(boardState);

//            var move = new Move(39, 46, MoveTypeEnum.EnPassante);
//            var extMove = new ExtendedMove(move, _bitBoard.Context);

//            var piecesListService = new PiecesListServiceTmp(_bitBoard.Context);

//            //Act
//            piecesListService.MakeMove(extMove);
//            piecesListService.UnmakeMove(extMove);

//            //Assert
//            var index = _bitBoard.Context.PiecesIndexesTmp[_bitBoard.Context.Player.Current][extMove.To];
//            _bitBoard.Context.PiecesIndexesTmp[_bitBoard.Context.Player.Current][extMove.From].Should().Be(index);

//            var piece = _bitBoard.Context.PiecesListTmp[_bitBoard.Context.Player.Current][(byte)extMove.PieceType].GetAtIndex(index);
//            piece.PieceType.Should().Be(extMove.PieceType);
//            piece.Square.Should().Be(extMove.From);

//            var capturedSquare = _bitBoard.Context.Player.Current == Player.White ? (byte)(extMove.To - 8) : (byte)(extMove.To + 8);
//            _bitBoard.Context.PiecesListTmp[_bitBoard.Context.Player.Oponnent][(byte)extMove.Captured.Value].Get()
//                .ToList().Select(s => s.Square).Should().Contain(capturedSquare);
//            _bitBoard.Context.PiecesIndexesTmp[_bitBoard.Context.Player.Oponnent][capturedSquare].Should().Be((byte)(_bitBoard.Context.PiecesListTmp[_bitBoard.Context.Player.Oponnent][(byte)extMove.Captured.Value].Count - 1));
//        }

//        [Theory(DisplayName = "Should make castle")]
//        [InlineData("r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 1", 3, 1)]
//        [InlineData("r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R b KQkq - 0 1", 59, 57)]
//        public void ShouldMakeCastle(string fen, byte from, byte to)
//        {
//            //Arrange
//            var boardState = FenHelper.CreateBoardState(fen);
//            _bitBoard.SetupBoard(boardState);

//            var move = new Move(from, to, MoveTypeEnum.Castle);
//            var extMove = new ExtendedMove(move, _bitBoard.Context);

//            var piecesListService = new PiecesListServiceTmp(_bitBoard.Context);

//            //Act
//            piecesListService.MakeMove(extMove);

//            //Assert
//            var index = _bitBoard.Context.PiecesIndexesTmp[_bitBoard.Context.Player.Current][extMove.From];
//            _bitBoard.Context.PiecesIndexesTmp[_bitBoard.Context.Player.Current][extMove.To].Should().Be(index);

//            var piece = _bitBoard.Context.PiecesListTmp[_bitBoard.Context.Player.Current][(byte)extMove.PieceType].GetAtIndex(index);
//            piece.PieceType.Should().Be(extMove.PieceType);
//            piece.Square.Should().Be(extMove.To);

//            var rookSrcSquare = extMove.IsCastleKingSideMove ? Rook.KingSideSourceSquare[_bitBoard.Context.Player.Current] : Rook.QueenSideSourceSquare[_bitBoard.Context.Player.Current];
//            var rookDstSquare = extMove.IsCastleKingSideMove ? Rook.KingSideDestinationSquare[_bitBoard.Context.Player.Current] : Rook.QueenSideDestinationSquare[_bitBoard.Context.Player.Current];

//            index = _bitBoard.Context.PiecesIndexesTmp[_bitBoard.Context.Player.Current][rookSrcSquare];
//            _bitBoard.Context.PiecesIndexesTmp[_bitBoard.Context.Player.Current][rookDstSquare].Should().Be(index);

//            piece = _bitBoard.Context.PiecesListTmp[_bitBoard.Context.Player.Current][(byte)PieceTypeEnum.Rook].GetAtIndex(index);
//            piece.PieceType.Should().Be(PieceTypeEnum.Rook);
//            piece.Square.Should().Be(rookDstSquare);
//        }

//        [Theory(DisplayName = "Should unmake castle")]
//        [InlineData("r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 1", 3, 5)]
//        [InlineData("r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R b KQkq - 0 1", 59, 61)]
//        public void ShouldUnmakeCastle(string fen, byte from, byte to)
//        {
//            //Arrange
//            var boardState = FenHelper.CreateBoardState(fen);
//            _bitBoard.SetupBoard(boardState);

//            var move = new Move(from, to, MoveTypeEnum.Castle);
//            var extMove = new ExtendedMove(move, _bitBoard.Context);

//            var piecesListService = new PiecesListServiceTmp(_bitBoard.Context);

//            //Act
//            piecesListService.MakeMove(extMove);
//            piecesListService.UnmakeMove(extMove);

//            //Assert
//            var index = _bitBoard.Context.PiecesIndexesTmp[_bitBoard.Context.Player.Current][extMove.To];
//            _bitBoard.Context.PiecesIndexesTmp[_bitBoard.Context.Player.Current][extMove.From].Should().Be(index);

//            var piece = _bitBoard.Context.PiecesListTmp[_bitBoard.Context.Player.Current][(byte)extMove.PieceType].GetAtIndex(index);
//            piece.PieceType.Should().Be(extMove.PieceType);
//            piece.Square.Should().Be(extMove.From);

//            var rookSrcSquare = extMove.IsCastleKingSideMove ? Rook.KingSideSourceSquare[_bitBoard.Context.Player.Current] : Rook.QueenSideSourceSquare[_bitBoard.Context.Player.Current];
//            var rookDstSquare = extMove.IsCastleKingSideMove ? Rook.KingSideDestinationSquare[_bitBoard.Context.Player.Current] : Rook.QueenSideDestinationSquare[_bitBoard.Context.Player.Current];

//            index = _bitBoard.Context.PiecesIndexesTmp[_bitBoard.Context.Player.Current][rookDstSquare];
//            _bitBoard.Context.PiecesIndexesTmp[_bitBoard.Context.Player.Current][rookSrcSquare].Should().Be(index);

//            piece = _bitBoard.Context.PiecesListTmp[_bitBoard.Context.Player.Current][(byte)PieceTypeEnum.Rook].GetAtIndex(index);
//            piece.PieceType.Should().Be(PieceTypeEnum.Rook);
//            piece.Square.Should().Be(rookSrcSquare);
//        }
//    }
//}
