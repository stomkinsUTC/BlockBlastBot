using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockBlastBot
{
    internal static class BBBCore
    {
        //Board format: True = piece, False = empty
        public static List<List<bool>> gameBoard
        {
            get { return gameBoard; }
            set { gameBoard = value; }
        }

        //Index - Item, Row, Col
        public static List<List<List<bool>>> gamePieces = new List<List<List<bool>>>();
        
    }
}
