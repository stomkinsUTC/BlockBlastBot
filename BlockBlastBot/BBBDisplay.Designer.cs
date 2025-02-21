﻿using System.Drawing;
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
            this.solveButton = new System.Windows.Forms.Button();
            this.scanButton = new System.Windows.Forms.Button();
            this.instructionLabel = new System.Windows.Forms.Label();
            this.clearButton = new System.Windows.Forms.Button();
            this.camSelector = new System.Windows.Forms.ComboBox();
            this.camOutput = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.camOutput)).BeginInit();
            this.SuspendLayout();
            // 
            // solveButton
            // 
            this.solveButton.Location = new System.Drawing.Point(12, 689);
            this.solveButton.Name = "solveButton";
            this.solveButton.Size = new System.Drawing.Size(75, 23);
            this.solveButton.TabIndex = 0;
            this.solveButton.Text = "Solve";
            this.solveButton.UseVisualStyleBackColor = true;
            this.solveButton.Click += new System.EventHandler(this.SolveProblem);
            // 
            // scanButton
            // 
            this.scanButton.Location = new System.Drawing.Point(12, 660);
            this.scanButton.Name = "scanButton";
            this.scanButton.Size = new System.Drawing.Size(75, 23);
            this.scanButton.TabIndex = 1;
            this.scanButton.Text = "Scan";
            this.scanButton.UseVisualStyleBackColor = true;
            this.scanButton.Click += new System.EventHandler(this.scanPressed);
            // 
            // instructionLabel
            // 
            this.instructionLabel.AutoSize = true;
            this.instructionLabel.Location = new System.Drawing.Point(94, 669);
            this.instructionLabel.Name = "instructionLabel";
            this.instructionLabel.Size = new System.Drawing.Size(44, 39);
            this.instructionLabel.TabIndex = 2;
            this.instructionLabel.Text = "Step 1: \r\nStep 2: \r\nStep 3: ";
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(13, 719);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(75, 23);
            this.clearButton.TabIndex = 3;
            this.clearButton.Text = "ClearLines";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.ClearRows);
            // 
            // camSelector
            // 
            this.camSelector.FormattingEnabled = true;
            this.camSelector.Location = new System.Drawing.Point(595, 12);
            this.camSelector.Name = "camSelector";
            this.camSelector.Size = new System.Drawing.Size(121, 21);
            this.camSelector.TabIndex = 4;
            // 
            // camOutput
            // 
            this.camOutput.Location = new System.Drawing.Point(595, 40);
            this.camOutput.Name = "camOutput";
            this.camOutput.Size = new System.Drawing.Size(300, 400);
            this.camOutput.TabIndex = 5;
            this.camOutput.TabStop = false;
            // 
            // BBBDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(916, 759);
            this.Controls.Add(this.camOutput);
            this.Controls.Add(this.camSelector);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.instructionLabel);
            this.Controls.Add(this.scanButton);
            this.Controls.Add(this.solveButton);
            this.Name = "BBBDisplay";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.camOutput)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

       

        private Button solveButton;
        private Button scanButton;
        private Label instructionLabel;
        private Button clearButton;
        private ComboBox camSelector;
        private PictureBox camOutput;
    }
}

