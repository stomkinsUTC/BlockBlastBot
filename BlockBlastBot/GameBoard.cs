using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockBlastBot
{
    public static class GameBoard
    {
        public readonly static Color trueColour = Color.Blue;
        public readonly static Color falseColour = Color.Gray;
        public readonly static Color pieceFalseColour = Color.LightGray;
        public readonly static Color[] pieceColours = {Color.Red, Color.Green, Color.Purple};

        //Initialised with sample data for now.
        public static bool[,] gameArea = {
            { true, true, false, false, false, false, false, true },
            { true, false, false, false, false, false, false, false },
            { true, false, false, false, false, false, false, false },
            { false, false, false, false, false, false, false, false },
            { false, false, false, false, false, false, false, false },
            { false, false, false, false, false, false, false, false },
            { false, false, false, false, false, false, false, false },
            { true, false, false, false, false, false, false, true }
            };

        //Initialised with sample data for now.
        /*public static bool[,,] currentPieces = {
            { { true, true }, { true, false }, { true, true } },
            { { true, true}, { true, true }, { false, false } },
            { { true, true}, { true, true }, { true, true } }
            };*/

        public static List<List<List<bool>>> currentPieces = new List<List<List<bool>>>
        {
        new List<List<bool>>
        {
            new List<bool> {true, true, true},
            new List<bool> {false, true, false},
            new List<bool> {false, true, false}
        },
        new List<List<bool>>
        {
            new List<bool> {false, true, false},
            new List<bool> {false, true, false},
            new List<bool> {false, true, false},
            new List<bool> {false, true, false},
            new List<bool> {false, true, false}
        },
        new List<List<bool>>
        {
            new List<bool> {true, true, true}
        }
        };

        /*List<(int, int)> fits = GameBoard.FindAllFits(gameArea, currentPiece);

        Debug.WriteLine("Fits found at:");
        foreach (var fit in fits)
        {
            Debug.WriteLine($"Row: {fit.Item1}, Col: {fit.Item2}");
        }*/

        //Returns the correct colour based on whether the cell is filled
        public static Color GetColour(int row, int col)
        {
            if (gameArea[row, col])
            {
                return trueColour;
            }
            return falseColour;
        }

        //Returns the correct colour based on the piece and whether the cell is filled
        public static Color GetPieceColour(int piece, int row, int col)
        {
            if (row >= currentPieces[piece].Count || col >= currentPieces[piece][row].Count)
            {
                return pieceFalseColour;
            }
            else if (currentPieces[piece][row][col])
            {
                return pieceColours[piece];
            }
            return pieceFalseColour;
        }

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
