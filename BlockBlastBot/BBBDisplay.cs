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

        public BBBDisplay()
        {
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
                        BackColor = GameBoard.GetColour(row, col),
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
                            BackColor = GameBoard.GetPieceColour(piece, row, col),
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

        private void AddPiece(int piece, int row, int col)
        {
            for (int i = 0; i < GameBoard.currentPieces[piece].Count; i++)
            {
                for (int j = 0; j < GameBoard.currentPieces[piece][i].Count; j++)
                {
                    if (GameBoard.currentPieces[piece][i][j])
                    {
                        displayGrid[col + j][row + i].BackColor = GameBoard.pieceColours[piece];
                    }
                }
            }
        }

        private void SolveProblem(object sender, EventArgs e)
        {
            //THIS IS DEBUG, TESTING ONLY
            List<List<int>> fits = GameBoard.FindAllFits(GameBoard.gameArea, GameBoard.currentPieces[0]);

            if (fits != null)
            {
                AddPiece(0, fits[0][0], fits[0][1]);
            }

            Debug.WriteLine("Fits found at:");
            foreach (var fit in fits)
            {
                Debug.WriteLine($"Row: {fit[0]}, Col: {fit[1]}");
            }
        }
    }
}
