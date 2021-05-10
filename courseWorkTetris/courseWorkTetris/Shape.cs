using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseWorkTetris
{
   abstract class Shape
    {

        public int x;
        public int y;

        public int[,] body;


        public void Movedown()
        {
            y++;
        }

        public void Leght()
        {
            x--;
        }

        public void Right()
        {
            x++;
        }

    }
}
