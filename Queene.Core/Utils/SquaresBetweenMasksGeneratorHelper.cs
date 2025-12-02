using Queene.Core.Consts;
using QueeneEngine.Helpers.Bitwise;

namespace Queene.Core.Utils
{
    public static class SquaresBetweenMasksGeneratorHelper
    {
		public static readonly ulong[][] MasksBeetwenSquaresRanksAndFiles = new ulong[64][];
		public static readonly ulong[][] MasksBeetwenSquaresDiagonals = new ulong[64][];
		public static readonly ulong[][] MasksSquareBySquareDiagonals = new ulong[64][];
		public static readonly ulong[][] MasksSquareBySquareRanksAndFiles = new ulong[64][];

		static SquaresBetweenMasksGeneratorHelper()
        {
			for (int i = 0; i < 64; i++)
			{
				MasksBeetwenSquaresRanksAndFiles[i] = new ulong[64];
				MasksBeetwenSquaresDiagonals[i] = new ulong[64];
				MasksSquareBySquareDiagonals[i] = new ulong[64];
				MasksSquareBySquareRanksAndFiles[i] = new ulong[64];
			}
		}

		public static void GenerateMasksBeetwenSquares()
		{
			GenerateMasksBetweenSquaresInFiles();
			GenerateMasksBetweenSquaresInRanks();
			GenerateMasksBetweenSquaresInDiagonals();
			GenerateMasksBetweenSquaresInAntiDiagonals();
		}

		private static void GenerateMasksBetweenSquaresInFiles()
        {
			for (int file = 0; file < 8; file++)
			{
				int?[] squaresInFile = new int?[8];

				byte count = 0;
				byte[] tab = BitwiseHelper.GetAllSetBitsInMask(BoardConsts.FileFullState[file], out count);

				for (int i = 0; i < count; i++)
					squaresInFile[i] = tab[i];

				for (int i = 0; i < count; i++)
				{
					for (int j = 0; j < count; j++)
					{
						if (i == j)
							continue;

						ulong tmp = 0;

						if (i - j == 1 || j - i == 1)
						{
							tmp |= Powers.powersOfTwo[squaresInFile[i].Value];
							tmp |= Powers.powersOfTwo[squaresInFile[j].Value];
							MasksSquareBySquareRanksAndFiles[squaresInFile[i].Value][squaresInFile[j].Value] = tmp;
							continue;
						}

						if (i < j)
						{
							for (int k = i + 1; k < j; k++)
								if (squaresInFile[k].HasValue)
									tmp |= Powers.powersOfTwo[squaresInFile[k].Value];
						}
						else
						{
							for (int k = j + 1; k < i; k++)
								if (squaresInFile[k].HasValue)
									tmp |= Powers.powersOfTwo[squaresInFile[k].Value];
						}

						if (squaresInFile[i].HasValue && squaresInFile[j].HasValue)
							MasksBeetwenSquaresRanksAndFiles[squaresInFile[i].Value][squaresInFile[j].Value] = tmp;
					}
				}
			}
		}

		private static void GenerateMasksBetweenSquaresInRanks()
        {
			for (int rank = 0; rank < 8; rank++)
			{
				int?[] squaresInRank = new int?[8];

				byte count = 0;
				byte[] tab = BitwiseHelper.GetAllSetBitsInMask(BoardConsts.RankFullState[rank], out count);

				for (int i = 0; i < count; i++)
					squaresInRank[i] = tab[i];

				for (int i = 0; i < count; i++)
				{
					for (int j = 0; j < count; j++)
					{
						if (i == j)
							continue;

						ulong tmp = 0;

						if (i - j == 1 || j - i == 1)
						{
							tmp |= Powers.powersOfTwo[squaresInRank[i].Value];
							tmp |= Powers.powersOfTwo[squaresInRank[j].Value];
							MasksSquareBySquareRanksAndFiles[squaresInRank[i].Value][squaresInRank[j].Value] = tmp;
							continue;
						}

						if (i < j)
						{
							for (int k = i + 1; k < j; k++)
								if (squaresInRank[k].HasValue)
									tmp |= Powers.powersOfTwo[squaresInRank[k].Value];
						}
						else
						{
							for (int k = j + 1; k < i; k++)
								if (squaresInRank[k].HasValue)
									tmp |= Powers.powersOfTwo[squaresInRank[k].Value];
						}

						if (squaresInRank[i].HasValue && squaresInRank[j].HasValue)
							MasksBeetwenSquaresRanksAndFiles[squaresInRank[i].Value][squaresInRank[j].Value] = tmp;
					}
				}
			}
		}

		private static void GenerateMasksBetweenSquaresInDiagonals()
        {
			for (int diag = 0; diag < 15; diag++)
			{
				int?[] squaresInDiag = new int?[8];
				byte count = 0;

				byte[] tab = BitwiseHelper.GetAllSetBitsInMask(BoardConsts.DiagFullState[diag], out count);
				for (int i = 0; i < count; i++)
					squaresInDiag[i] = tab[i];

				for (int i = 0; i < count; i++)
				{
					for (int j = 0; j < count; j++)
					{
						if (i == j)
							continue;

						ulong tmp = 0;

						if (i - j == 1 || j - i == 1)
						{
							tmp |= Powers.powersOfTwo[squaresInDiag[i].Value];
							tmp |= Powers.powersOfTwo[squaresInDiag[j].Value];
							MasksSquareBySquareDiagonals[squaresInDiag[i].Value][squaresInDiag[j].Value] = tmp;
							continue;
						}


						if (i < j)
						{
							for (int k = i + 1; k < j; k++)
								if (squaresInDiag[k].HasValue)
									tmp |= Powers.powersOfTwo[squaresInDiag[k].Value];
						}
						else
						{
							for (int k = j + 1; k < i; k++)
								if (squaresInDiag[k].HasValue)
									tmp |= Powers.powersOfTwo[squaresInDiag[k].Value];
						}

						if (squaresInDiag[i].HasValue && squaresInDiag[j].HasValue)
						{
							if (MasksBeetwenSquaresDiagonals[squaresInDiag[i].Value][squaresInDiag[j].Value] == 0)
								MasksBeetwenSquaresDiagonals[squaresInDiag[i].Value][squaresInDiag[j].Value] = tmp;
						}
					}
				}
			}
		}

		private static void GenerateMasksBetweenSquaresInAntiDiagonals()
		{
			for (int diag = 0; diag < 15; diag++)
			{
				int?[] squaresInDiag = new int?[8];
				byte count = 0;

				byte[] tab = BitwiseHelper.GetAllSetBitsInMask(BoardConsts.AntyDiagFullState[diag], out count);
				for (int i = 0; i < count; i++)
					squaresInDiag[i] = tab[i];

				for (int i = 0; i < count; i++)
				{
					for (int j = 0; j < count; j++)
					{
						if (i == j)
							continue;

						ulong tmp = 0;

						if (i - j == 1 || j - i == 1)
						{
							tmp |= Powers.powersOfTwo[squaresInDiag[i].Value];
							tmp |= Powers.powersOfTwo[squaresInDiag[j].Value];
							MasksSquareBySquareDiagonals[squaresInDiag[i].Value][squaresInDiag[j].Value] = tmp;
							continue;
						}

						if (i < j)
						{
							for (int k = i + 1; k < j; k++)
								if (squaresInDiag[k].HasValue)
									tmp |= Powers.powersOfTwo[squaresInDiag[k].Value];
						}
						else
						{
							for (int k = j + 1; k < i; k++)
								if (squaresInDiag[k].HasValue)
									tmp |= Powers.powersOfTwo[squaresInDiag[k].Value];
						}


						if (squaresInDiag[i].HasValue && squaresInDiag[j].HasValue)
							if (MasksBeetwenSquaresDiagonals[squaresInDiag[i].Value][squaresInDiag[j].Value] == 0)
								MasksBeetwenSquaresDiagonals[squaresInDiag[i].Value][squaresInDiag[j].Value] = tmp;
					}
				}
			}
		}
	}
}
