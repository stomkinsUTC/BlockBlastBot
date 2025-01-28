using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockBlastBot
{
    public class GameBoard
    {
        public readonly static Color trueColour = Color.Blue;
        public readonly static Color falseColour = Color.Gray;
        public readonly static Color pieceFalseColour = Color.LightGray;
        public readonly static Color[] pieceColours = {Color.Red, Color.Green, Color.Purple};

        public List<List<bool>> gameArea = new List<List<bool>>();

        public List<List<List<bool>>> currentPieces = new List<List<List<bool>>>
        {
        new List<List<bool>>
        {
            new List<bool> {true, true, true, true, true, true, true, false},
            new List<bool> {false, true, false, false, false, false, false, false}
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

        public GameBoard()
        {
            for (int j = 0; j < 8; j++)
            {
                List<bool> bools = new List<bool>();
                for (int i = 0; i < 8; i++)
                {
                    bools.Add(false);
                }
                gameArea.Add(bools);
            }
        }

        public void ClearRows()
        {
            List<int> colToClear = new List<int>();
            List<int> rowToClear = new List<int>();

            for (int i = 0; i < gameArea.Count; i++)
            {
                bool toClear = true;
                foreach (bool b in gameArea[i])
                {
                    if (!b)
                    {
                        toClear = false;
                    }
                }
                if (toClear)
                {
                    rowToClear.Add(i);
                }
            }

            for (int i = 0; i < gameArea.Count; i++)
            {
                List<bool> temp = new List<bool>();
                for (int j = 0; j < gameArea.Count; j++)
                {
                    temp.Add(gameArea[j][i]);
                }
                if (!temp.Contains(false))
                {
                    colToClear.Add(i);
                }
            }
            foreach (int i in rowToClear)
            {
                for (int j = 0; j < 8; j++)
                {
                    gameArea[j][i] = false;
                    BBBDisplay.ResetCellColour(j, i);
                }
            }
            foreach (int i in colToClear)
            {
                for (int j = 0; j < 8; j++)
                {
                    gameArea[i][j] = false;
                    BBBDisplay.ResetCellColour(i, j);
                }
            }
            /*Debug.WriteLine("Rows to clear:");
            foreach (int i in rowToClear)
            {
                Debug.WriteLine(i);
            }
            Debug.WriteLine("Cols to clear:");
            foreach (int i in colToClear)
            {
                Debug.WriteLine(i);
            }*/
        }

        //Returns the correct colour based on whether the cell is filled
        public Color GetColour(int row, int col)
        {
            if (gameArea[row][col])
            {
                return trueColour;
            }
            return falseColour;
        }

        //Returns the correct colour based on the piece and whether the cell is filled
        public Color GetPieceColour(int piece, int row, int col)
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

        public static List<List<int>> FindAllFits(List<List<bool>> gameBoard, List<List<bool>> currentPiece)
        {
            List<List<int>> fits = new List<List<int>>();
            int boardRows = gameBoard.Count;
            int boardCols = gameBoard[0].Count;
            int pieceRows = currentPiece.Count();
            int pieceCols = currentPiece[0].Count();

            // Loop through each starting position in gameBoard
            for (int startRow = 0; startRow <= boardRows - pieceRows; startRow++)
            {
                for (int startCol = 0; startCol <= boardCols - pieceCols; startCol++)
                {
                    if (CanFit(gameBoard, currentPiece, startRow, startCol))
                    {
                        List<int> temp = new List<int>();
                        temp.Add(startRow);
                        temp.Add(startCol);
                        fits.Add(temp);
                    }
                }
            }

            return fits;
        }

        private static bool CanFit(List<List<bool>> gameBoard, List<List<bool>> currentPiece, int startRow, int startCol)
        {
            int pieceRows = currentPiece.Count();
            int pieceCols = currentPiece[0].Count();

            // Check every cell in the currentPiece
            for (int row = 0; row < pieceRows; row++)
            {
                for (int col = 0; col < pieceCols; col++)
                {
                    if (currentPiece[row][col] && gameBoard[startRow + row][startCol + col])
                    {
                        return false; // Conflict: currentPiece can't fit here
                    }
                }
            }
            return true; // Fits without conflict
        }
    }
}
