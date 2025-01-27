using System.Drawing;
using System.Windows.Forms;

namespace BlockBlastBot
{
    partial class BBBDisplay
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            InitializeGrid();

        }
        
        #endregion

        private void InitializeGrid()
        {
            // Dimensions of the grid and squares
            int gridSize = 8;
            int squareSize = 50; // Size of each square in pixels
            int padding = 2; // Space between squares
            int edgePadding = 40; //Padding around the game board
            int gamePiecePadding = 200; //Padding to fit the three gamne pieces

            // Set the form size based on the grid
            this.ClientSize = new Size(
                gridSize * (squareSize + padding) + edgePadding * 2,
                gridSize * (squareSize + padding) + edgePadding + gamePiecePadding
            );
            this.Text = "BlockBlastBot";

            // Loop to create theint row = 0; row < gridSize; row++)
            {
                for (int col = 0; col < gridSize; col++)
                {
                    for (int row = 0; row < gridSize; row++)
                    {
                        // Create a new panel for each square
                        Panel square = new Panel
                        {
                            Size = new Size(squareSize, squareSize),
                            BackColor = Color.Blue,
                            Location = new Point(
                            col * (squareSize + padding) + edgePadding,
                            row * (squareSize + padding)
                        )
                        };

                        // Add the square to the form
                        this.Controls.Add(square);
                    }
                }
            }
        }
    }
}

