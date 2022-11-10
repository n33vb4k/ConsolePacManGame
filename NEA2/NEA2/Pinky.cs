using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA2
{
    class Pinky : Ghost
    {
        public Pinky()
        {
            scatter = false;
            startingcol = 15;
            startingrow = 13;
            row = startingrow;
            col = startingcol;
        }
    }
}
