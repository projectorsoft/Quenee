using System.Collections;
using System.Collections.Generic;

namespace Queene.Core.Tests.Perft.TestData
{
    public class PerftTestData : IEnumerable<object[]>
    {
        //From https://www.chessprogramming.org/Perft_Results
        public IEnumerator<object[]> GetEnumerator()
        {
            //Initial Position
            yield return new object[] {
                "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 1, 20
            };
            yield return new object[] {
                "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 2, 400
            };
            yield return new object[] {
                "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 3, 8902
            };
            yield return new object[] {
                "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 4, 197281
            };
            yield return new object[] {
                "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 5, 4865609
            };
            yield return new object[] {
                "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 6, 119060324
            };
            //yield return new object[] {
            //    "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 7, 3195901860
            //};

            //Position 2
            yield return new object[] {
                "r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 1", 1, 48
            };
            yield return new object[] {
                "r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 1", 2, 2039
            };
            yield return new object[] {
                "r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 1", 3, 97862
            };
            yield return new object[] {
                "r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 1", 4, 4085603
            };
            yield return new object[] {
                "r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 1", 5, 193690690
            };
            //yield return new object[] {
            //    "r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 1", 6, 8031647685 //bad result
            //};

            //Position 3
            yield return new object[] {
                "8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w KQkq - 0 1", 1, 14
            };
            yield return new object[] {
                "8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w KQkq - 0 1", 2, 191
            };
            yield return new object[] {
                "8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w KQkq - 0 1", 3, 2812
            };
            yield return new object[] {
                "8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w KQkq - 0 1", 4, 43238
            };
            yield return new object[] {
                "8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w KQkq - 0 1", 5, 674624
            };
            yield return new object[] {
                "8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w KQkq - 0 1", 6, 11030083
            };
            yield return new object[] {
                "8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w KQkq - 0 1", 7, 178633661
            };
            //yield return new object[] {
            //    "8/2p5/3p4/KP5r/1R3p1k/8/4P1P1/8 w KQkq - 0 1", 8, 3009794393
            //};

            //Position 4
            yield return new object[] {
                "r3k2r/Pppp1ppp/1b3nbN/nP6/BBP1P3/q4N2/Pp1P2PP/R2Q1RK1 w kq - 0 1", 1, 6
            };
            yield return new object[] {
                "r3k2r/Pppp1ppp/1b3nbN/nP6/BBP1P3/q4N2/Pp1P2PP/R2Q1RK1 w kq - 0 1", 2, 264
            };
            yield return new object[] {
                "r3k2r/Pppp1ppp/1b3nbN/nP6/BBP1P3/q4N2/Pp1P2PP/R2Q1RK1 w kq - 0 1", 3, 9467
            };
            yield return new object[] {
                "r3k2r/Pppp1ppp/1b3nbN/nP6/BBP1P3/q4N2/Pp1P2PP/R2Q1RK1 w kq - 0 1", 4, 422333
            };
            yield return new object[] {
                "r3k2r/Pppp1ppp/1b3nbN/nP6/BBP1P3/q4N2/Pp1P2PP/R2Q1RK1 w kq - 0 1", 5, 15833292
            };
            //yield return new object[] {
            //    "r3k2r/Pppp1ppp/1b3nbN/nP6/BBP1P3/q4N2/Pp1P2PP/R2Q1RK1 w kq - 0 1", 6, 706045033
            //};

            //Position 5
            yield return new object[] {
                "rnbq1k1r/pp1Pbppp/2p5/8/2B5/8/PPP1NnPP/RNBQK2R w KQ - 1 8", 1, 44
            };
            yield return new object[] {
                "rnbq1k1r/pp1Pbppp/2p5/8/2B5/8/PPP1NnPP/RNBQK2R w KQ - 1 8", 2, 1486
            };
            yield return new object[] {
                "rnbq1k1r/pp1Pbppp/2p5/8/2B5/8/PPP1NnPP/RNBQK2R w KQ - 1 8", 3, 62379
            };
            yield return new object[] {
                "rnbq1k1r/pp1Pbppp/2p5/8/2B5/8/PPP1NnPP/RNBQK2R w KQ - 1 8", 4, 2103487
            };
            yield return new object[] {
                "rnbq1k1r/pp1Pbppp/2p5/8/2B5/8/PPP1NnPP/RNBQK2R w KQ - 1 8", 5, 89941194
            };

            //Position 6
            yield return new object[] {
                "r4rk1/1pp1qppp/p1np1n2/2b1p1B1/2B1P1b1/P1NP1N2/1PP1QPPP/R4RK1 w - - 0 10", 1, 46
            };
            yield return new object[] {
                "r4rk1/1pp1qppp/p1np1n2/2b1p1B1/2B1P1b1/P1NP1N2/1PP1QPPP/R4RK1 w - - 0 10", 2, 2079
            };
            yield return new object[] {
                "r4rk1/1pp1qppp/p1np1n2/2b1p1B1/2B1P1b1/P1NP1N2/1PP1QPPP/R4RK1 w - - 0 10", 3, 89890
            };
            yield return new object[] {
                "r4rk1/1pp1qppp/p1np1n2/2b1p1B1/2B1P1b1/P1NP1N2/1PP1QPPP/R4RK1 w - - 0 10", 4, 3894594
            };
            yield return new object[] {
                "r4rk1/1pp1qppp/p1np1n2/2b1p1B1/2B1P1b1/P1NP1N2/1PP1QPPP/R4RK1 w - - 0 10", 5, 164075551
            };
            //yield return new object[] {
            //    "r4rk1/1pp1qppp/p1np1n2/2b1p1B1/2B1P1b1/P1NP1N2/1PP1QPPP/R4RK1 w - - 0 10", 6, 6923051137
            //};
            //yield return new object[] {
            //    "r4rk1/1pp1qppp/p1np1n2/2b1p1B1/2B1P1b1/P1NP1N2/1PP1QPPP/R4RK1 w - - 0 10", 7, 287188994746
            //};
            //yield return new object[] {
            //    "r4rk1/1pp1qppp/p1np1n2/2b1p1B1/2B1P1b1/P1NP1N2/1PP1QPPP/R4RK1 w - - 0 10", 8, 11923589843526
            //};
            //yield return new object[] {
            //    "r4rk1/1pp1qppp/p1np1n2/2b1p1B1/2B1P1b1/P1NP1N2/1PP1QPPP/R4RK1 w - - 0 10", 9, 490154852788714
            //};

            //Other
            //--Illegal ep move #1
            yield return new object[] {
                "3k4/3p4/8/K1P4r/8/8/8/8 b - - 0 1", 6, 1134888
            };
            //--Illegal ep move #2
            yield return new object[] {
                "8/8/4k3/8/2p5/8/B2P2K1/8 w - - 0 1", 6, 1015133
            };
            //--EP Capture Checks Opponent
            yield return new object[] {
                "8/8/1k6/2b5/2pP4/8/5K2/8 b - d3 0 1", 6, 1440467
            };
            //--Short Castling Gives Check
            yield return new object[] {
                "5k2/8/8/8/8/8/8/4K2R w K - 0 1", 6, 661072
            };
            //--Long Castling Gives Check
            yield return new object[] {
                "3k4/8/8/8/8/8/8/R3K3 w Q - 0 1", 6, 803711
            };
            //--Castle Rights
            yield return new object[] {
                "r3k2r/1b4bq/8/8/8/8/7B/R3K2R w KQkq - 0 1", 4, 1274206
            };
            //--Castling Prevented
            yield return new object[] {
                "r3k2r/8/3Q4/8/8/5q2/8/R3K2R b KQkq - 0 1", 4, 1720476
            };
            //--Promote out of Check
            yield return new object[] {
                "2K2r2/4P3/8/8/8/8/8/3k4 w - - 0 1", 6, 3821001
            };
            //--Discovered Check
            yield return new object[] {
                "8/8/1P2K3/8/2n5/1q6/8/5k2 b - - 0 1", 5, 1004658
            };
            //--Promote to give check
            yield return new object[] {
                "4k3/1P6/8/8/8/8/K7/8 w - - 0 1", 6, 217342
            };
            //--Under Promote to give check
            yield return new object[] {
                "8/P1k5/K7/8/8/8/8/8 w - - 0 1", 6, 92683
            };
            //--Self Stalemate
            yield return new object[] {
                "K1k5/8/P7/8/8/8/8/8 w - - 0 1", 6, 2217
            };
            //--Stalemate & Checkmate
            yield return new object[] {
                "8/k1P5/8/1K6/8/8/8/8 w - - 0 1", 7, 567584
            };
            //--Stalemate & Checkmate
            yield return new object[] {
                "8/8/2k5/5q2/5n2/8/5K2/8 b - - 0 1", 4, 23527
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
