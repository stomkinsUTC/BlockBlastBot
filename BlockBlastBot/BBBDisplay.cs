using AForge.Video;
using AForge.Video.DirectShow;
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
            InitialiseCamera();
        }

        private void InitialiseCamera()
        {
            FilterInfoCollection fic;
            VideoCaptureDevice vcd;

            fic = new FilterInfoCollection(FilterCategory.VideoCompressorCategory);
            foreach (FilterInfo fi in fic)
            {
                comboBox1.Items.Add(fi.Name);
            }
            comboBox1.SelectedIndex = 0;
            vcd = new VideoCaptureDevice();
            vcd = new VideoCaptureDevice(fic[comboBox1.SelectedIndex].MonikerString);
            vcd.NewFrame += VideoCaptureDevice_NewFrame;
        }

        private void VideoCaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone();
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
            gameBoard.ClearLines();
        }


        //Checks all combinations of orders of pieces.
        private void SolveProblem(object sender, EventArgs e)
        {
            List<List<int>> placeOrders = new List<List<int>>()
            {
                new List<int>{ 0, 1, 2 },
                new List<int>{ 0, 2, 1 },
                new List<int>{ 1, 0, 2 },
                new List<int>{ 1, 2, 0 },
                new List<int>{ 2, 0, 1 },
                new List<int>{ 2, 1, 0 }
            };
            List<PieceOrder> solutions = new List<PieceOrder>();

            foreach(List<int> pO in placeOrders)
            {
                List<List<int>> piece1Fits = gameBoard.FindAllFits(gameBoard.gameArea, gameBoard.currentPieces[pO[0]]);
                for (int piece1 = 0; piece1 < piece1Fits.Count; piece1++)
                {
                    GameBoard piece1Board = (GameBoard)gameBoard.Clone();
                    piece1Board.AddPiece(pO[0], piece1Fits[piece1][0], piece1Fits[piece1][1]);

                    List<List<int>> piece2Fits = piece1Board.FindAllFits(piece1Board.gameArea, piece1Board.currentPieces[pO[1]]);
                    for (int piece2 = 0; piece2 < piece2Fits.Count; piece2++)
                    {
                        GameBoard piece2Board = (GameBoard)piece1Board.Clone();
                        piece2Board.AddPiece(pO[1], piece2Fits[piece2][0], piece2Fits[piece2][1]);

                        List<List<int>> piece3Fits = gameBoard.FindAllFits(piece2Board.gameArea, piece2Board.currentPieces[pO[2]]);
                        for (int piece3 = 0; piece3 < piece3Fits.Count; piece3++)
                        {
                            GameBoard piece3Board = (GameBoard)piece2Board.Clone();
                            piece3Board.AddPiece(pO[2], piece3Fits[piece3][0], piece3Fits[piece3][1]);

                            //Stores the valid placement as a PieceOrder object.
                            PieceOrder tempOrder = new PieceOrder();
                            tempOrder.order.Add(pO[0]);
                            tempOrder.order.Add(pO[1]);
                            tempOrder.order.Add(pO[2]);

                            tempOrder.coordinates.Add(new List<int>() { piece1Fits[piece1][0], piece1Fits[piece1][1] });
                            tempOrder.coordinates.Add(new List<int>() { piece2Fits[piece2][0], piece2Fits[piece2][1] });
                            tempOrder.coordinates.Add(new List<int>() { piece3Fits[piece3][0], piece3Fits[piece3][1] });

                            tempOrder.totalFree = piece3Board.GetTotalFree();

                            solutions.Add(tempOrder);
                        }
                    }
                }
            }
            //Rearrange the list to be in ascending order.
            //Higher number of free spaces is better.
            solutions = solutions.OrderBy(o=>o.totalFree).ToList();
            for (int i = 0; i < solutions.Count; i++)
            {
                Debug.WriteLine("Solution: " + i + ": " + solutions[i].totalFree + " free slots.");
            }
            if (solutions.Count > 0)
            {
                UpdateUI();
                Debug.WriteLine("Chosen solution: " + solutions[solutions.Count-1].totalFree + " free slots.");
                DisplaySteps(solutions[solutions.Count - 1]);
            }
            else
            {
                DisplaySteps(new PieceOrder());
            }
            
        }

        private void ClearRows(object sender, EventArgs e)
        {
            for (int i = 0; i < gameBoard.gameArea.Count; i++)
            {
                for (int j = 0; j < gameBoard.gameArea[0].Count; j++)
                {
                    if (displayGrid[j][i].BackColor != GameBoard.trueColour && displayGrid[j][i].BackColor != GameBoard.falseColour)
                    {
                        gameBoard.gameArea[i][j] = true;
                    }
                }
            }
            UpdateUI();
        }

        private void DisplaySteps(PieceOrder stepsToTake)
        {
            if (stepsToTake.coordinates.Count > 0)
            {
                for (int pieces = 0; pieces < gameBoard.currentPieces.Count; pieces++)
                {
                    for (int i = 0; i < gameBoard.currentPieces[stepsToTake.order[pieces]].Count; i++)
                    {
                        for (int j = 0; j < gameBoard.currentPieces[stepsToTake.order[pieces]][0].Count; j++)
                        {
                            if (gameBoard.currentPieces[stepsToTake.order[pieces]][i][j])
                            {
                                displayGrid[stepsToTake.coordinates[pieces][1] + j][stepsToTake.coordinates[pieces][0] + i].BackColor = GameBoard.pieceColours[stepsToTake.order[pieces]];
                            }
                        }
                    }
                }
                

                instructionLabel.Text = "Step 1: Place piece " + (stepsToTake.order[0] + 1) + " at X: " +
                    (stepsToTake.coordinates[0][1] + 1) + ", Y: " + (stepsToTake.coordinates[0][0] + 1) +
                    "\nStep 2: Place piece " + (stepsToTake.order[1] + 1) + " at X: " +
                    (stepsToTake.coordinates[1][1] + 1) + ", Y: " + (stepsToTake.coordinates[1][0] + 1) +
                    "\nStep 3: Place piece " + (stepsToTake.order[2] + 1) + " at X: " +
                    (stepsToTake.coordinates[2][1] + 1) + ", Y: " + (stepsToTake.coordinates[2][0] + 1);
            }
            else
            {
                instructionLabel.Text = "No solution found!";
            }
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
