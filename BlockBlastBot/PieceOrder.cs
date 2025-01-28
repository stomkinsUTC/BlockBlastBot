using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockBlastBot
{
    internal class PieceOrder
    {
        public List<int> order = new List<int>();
        public List<List<int>> coordinates = new List<List<int>>();
        public int totalFree = 0;
    }
}
