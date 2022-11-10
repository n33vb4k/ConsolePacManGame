using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA2
{
    class Inky : Ghost
    {
        public Inky()
        {
            scatter = false;
            startingcol = 12;
            startingrow = 13;
            row = startingrow;
            col = startingcol;
        }
    }
}
