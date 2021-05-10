using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseWorkTetris
{
    class LiquidShape : Shape
    {
        public LiquidShape(int x, int y)
        {
            this.x = x;
            this.y = y;
        }


       public int[,] figure1 = new int[4, 4]
       {
             { 2, 0, 0, 0, },
             { 0, 2, 0, 0, },
             { 0, 0, 2, 0, },
             { 0, 0, 0, 2, }
       };


     

    }
}
