using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA2
{
    class KillBerry
    {
        public int row { get; set; }
        public int col { get; set; }    
        public bool visible { get; set; }

        public KillBerry()
        {
            visible = true;
        }
    }
}
