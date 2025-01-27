using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlockBlastBot
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool[,] gameArea = {
            { false, false, false, false, false, false, false, false },
            { false, false, false, false, false, false, false, false },
            { false, false, false, false, false, false, false, false },
            { false, false, false, false, false, false, false, false },
            { false, false, false, false, false, false, false, false },
            { false, false, false, false, false, false, false, false },
            { false, false, false, false, false, false, false, false },
            { false, false, false, false, false, false, false, false }
            };

            bool[,] currentPiece = {
            { true, false },
            { true, true }
            };

            List<(int, int)> fits = GameBoard.FindAllFits(gameArea, currentPiece);

            Debug.WriteLine("Fits found at:");
            foreach (var fit in fits)
            {
                Debug.WriteLine($"Row: {fit.Item1}, Col: {fit.Item2}");
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new BBBDisplay());
        }
    }
}
