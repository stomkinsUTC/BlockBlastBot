using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockBlastBot
{
    public static class GameBoard
    {
        public static List<(int, int)> FindAllFits(bool[,] gameBoard, bool[,] currentPiece)
        {
            List<(int, int)> fits = new List<(int, int)>();
            int boardRows = gameBoard.GetLength(0);
            int boardCols = gameBoard.GetLength(1);
            int pieceRows = currentPiece.GetLength(0);
            int pieceCols = currentPiece.GetLength(1);

            // Loop through each starting position in gameBoard
            for (int startRow = 0; startRow <= boardRows - pieceRows; startRow++)
            {
                for (int startCol = 0; startCol <= boardCols - pieceCols; startCol++)
                {
                    if (CanFit(gameBoard, currentPiece, startRow, startCol))
                    {
                        fits.Add((startRow, startCol));
                    }
                }
            }

            return fits;
        }

        private static bool CanFit(bool[,] gameBoard, bool[,] currentPiece, int startRow, int startCol)
        {
            int pieceRows = currentPiece.GetLength(0);
            int pieceCols = currentPiece.GetLength(1);

            // Check every cell in the currentPiece
            for (int row = 0; row < pieceRows; row++)
            {
                for (int col = 0; col < pieceCols; col++)
                {
                    if (currentPiece[row, col] && gameBoard[startRow + row, startCol + col])
                    {
                        return false; // Conflict: currentPiece can't fit here
                    }
                }
            }
            return true; // Fits without conflict
        }
    }
}
