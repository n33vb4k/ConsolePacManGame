using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA2
{
    abstract class Ghost
    {
        protected int startingrow;
        public bool scatter {get;set;}
        public int row { get; set; }
        protected int startingcol;
        public int col { get; set; }
        public int GetRow()
        {
            return row;
        }
        public int GetCol()
        {
            return col;
        }

        public void ResetPos()
        {
            row = startingrow;
            col = startingcol;
        }
    }
    
}
