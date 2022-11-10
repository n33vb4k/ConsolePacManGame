using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA2
{
    class Pacman
    {
        private int startingrow;
        private int row;
        private int startingcol;
        private int col;
        public int lives { get; set; }
        public int score { get; set; }
        public bool kill { get; set; }

        public Pacman()
        {
            startingcol = 14;
            startingrow = 23;
            row = startingrow;
            col = startingcol;
            lives = 3;
            score = 0;
            kill = false;
        }
        public void ResetPos()
        {
            row = startingrow;
            col = startingcol;
        }
        
        public int GetScore()
        {
            return score;
        }
        public void AddScore(int n)
        {
            score += n;
        }
        


        public int GetRow()
        {
            return row;
        }
        public int GetCol()
        {
            return col;
        }

        public void MoveUp()
        {
            row--;
        }
        public void MoveDown()
        {
            row++;
        }
        public void MoveLeft()
        {
            col--;
        }
        public void MoveRight()
        {
            col++;
        }

        public void TpRight()
        {
            col = 27;
        }
        public void TpLeft()
        {
            col = 0;
        }
    }
}
