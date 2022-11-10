using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA2
{
    class Cell
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public int G { get; set; } //distance from start to current node
        public int H { get; set; } //distance from current to end node
        public int F => G + H; //total cost(distance)

        public Cell parent { get; set; }
        public void CalculateH(int row, int col)
        {
            H = Math.Abs(row - Row) + Math.Abs(col - Col);
        }
        public bool WallRight { get; set; }
        public bool WallDown { get; set; }
        public Cell()
        {
            WallRight = true;
            WallDown = true;
        }
    }
}
