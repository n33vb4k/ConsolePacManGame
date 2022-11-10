using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA2
{
    class Blinky : Ghost
    {
        public Blinky()
        {
            scatter = false;
            startingcol = 12;
            startingrow = 14;
            row = startingrow;
            col = startingcol;
        }
        

    }
}
