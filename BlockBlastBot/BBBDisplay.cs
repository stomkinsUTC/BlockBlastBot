using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlockBlastBot
{
    public partial class BBBDisplay : Form
    {
        private static List<List<Panel>> displayGrid = new List<List<Panel>>();
        private static List<List<List<Panel>>> displayPieces = new List<List<List<Panel>>>();
        GameBoard gameBoard = new GameBoard();

        public BBBDisplay()
        {
            DebugSetSampleBoard();
            InitializeComponent();
            InitialiseDisplay();
        }

        private void InitialiseDisplay()
        {
            InitialiseGrid();
            InitialisePieces();
        }

        private void InitialiseGrid()
        {
            // Dimensions of the grid and squares
            int gridSize = 8;
            int squareSize = 50; // Size of each square in pixels
            int padding = 2; // Space between squares
            int edgePadding = 160; //Padding around the game board
            int gamePiecePadding = 200; //Padding to fit the three game pieces

            // Set the form size based on the grid
            this.ClientSize = new Size(
                gridSize * (squareSize + padding) + edgePadding * 2,
                gridSize * (squareSize + padding) + edgePadding + gamePiecePadding
            );
            this.Text = "BlockBlastBot";

            // Loop to create the grid

            for (int col = 0; col < gridSize; col++)
            {
                List<Panel> panelList = new List<Panel>();
                for (int row = 0; row < gridSize; row++)
                {
                    // Create a new panel for each square
                    Panel square = new Panel
                    {
                        Size = new Size(squareSize, squareSize),
                        BackColor = gameBoard.GetColour(row, col),
                        Location = new Point(
                        col * (squareSize + padding) + edgePadding,
                        row * (squareSize + padding)
                    )
                    };
                    // Add the square to the form
                    this.Controls.Add(square);
                    panelList.Add(square);
                }
                displayGrid.Add(panelList);
            }

        }

        private void InitialisePieces()
        {
            // Dimensions of the grid and squares
            int gridSize = 8;
            int squareSize = 25; // Size of each square in pixels
            int padding = 1; // Space between squares
            int edgePadding = 56; //Padding around the game board
            int boardpadding = 441; //Padding to cover the game board
            int piecePadding = 208; //Padding so the pieces don't overlap

            for (int piece = 0; piece < GameBoard.pieceColours.Length; piece++)
            {
                List<List<Panel>> piecePanels = new List<List<Panel>>();
                for (int col = 0; col < gridSize; col++)
                {
                    List<Panel> panelList = new List<Panel>();
                    for (int row = 0; row < gridSize; row++)
                    {
                        // Create a new panel for each square
                        Panel square = new Panel
                        {
                            Size = new Size(squareSize, squareSize),
                            BackColor = gameBoard.GetPieceColour(piece, row, col),
                            Location = new Point(
                            col * (squareSize + padding) + edgePadding + (piecePadding * piece),
                            row * (squareSize + padding) + boardpadding
                        )
                        };

                        // Add the square to the form
                        this.Controls.Add(square);
                        panelList.Add(square);
                    }
                    piecePanels.Add(panelList);
                }
                displayPieces.Add(piecePanels);
            }
        }

        //Visually adds the piece and updates the gameArea.
        //No idea why the grid seems to be rotated here...
        

        public static void ResetCellColour(int row, int col)
        {
            displayGrid[row][col].BackColor = GameBoard.falseColour;
        }

        private void UpdateUI()
        {
            for (int i = 0; i < gameBoard.gameArea.Count; i++)
            {
                for (int j = 0; j < gameBoard.gameArea[i].Count; j++)
                {
                    if (gameBoard.gameArea[i][j])
                    {
                        displayGrid[j][i].BackColor = GameBoard.trueColour;
                    }
                    else
                    {
                        displayGrid[j][i].BackColor = GameBoard.falseColour;
                    }
                }
            }

            for (int i = 0; i < gameBoard.currentPieces.Count; i++)
            {
                for (int j = 0; j < gameBoard.gameArea.Count; j++)
                {
                    for (int k = 0; k < gameBoard.gameArea[0].Count; k++)
                    {
                        displayPieces[i][k][j].BackColor = GameBoard.pieceFalseColour;
                    }
                }
                for (int j = 0; j < gameBoard.currentPieces[i].Count; j++)
                {
                    for (int k = 0; k < gameBoard.currentPieces[i][j].Count; k++)
                    {
                        if (gameBoard.currentPieces[i][j][k])
                        {
                            displayPieces[i][k][j].BackColor = GameBoard.pieceColours[i];
                        }
                    }
                }
            }
        }


        private void SolveProblem(object sender, EventArgs e)
        {
            List<List<int>> fits = gameBoard.FindAllFits(gameBoard.gameArea, gameBoard.currentPieces[0]);

            if (fits.Count > 0)
            {
                GameBoard tempBoard = (GameBoard) gameBoard.Clone();



                Debug.WriteLine("Placing at " + fits[0][0] + ", " + fits[0][1]);
                gameBoard.AddPiece(0, fits[0][0], fits[0][1]);
            }




            UpdateUI();



            /*Debug.WriteLine("Fits found at:");
            foreach (var fit in fits)
            {
                Debug.WriteLine($"Row: {fit[0]}, Col: {fit[1]}");
            }*/
        }

        //Sample board
        private void DebugSetSampleBoard()
        {
            gameBoard.gameArea = new List<List<bool>>{
                new List<bool> {false, false, false, true, true, false, false, true},
                new List<bool> {false, true, true, true, false, true, false, true},
                new List<bool> {true, true, false, true, false, true, true, true},
                new List<bool> {true, false, false, true, true, false, true, true},
                new List<bool> {true, true, false, false, false, false, false, false},
                new List<bool> {true, false, false, false, true, true, false, false},
                new List<bool> {true, true, false, true, false, false, true, false},
                new List<bool> {true, false, false, false, false, false, true, true}
            };

        }
    }
}
